using System;
using System.Collections.Generic;
using System.Text;
using NewTOAPIA.GL;

    public class BrightnessShader : GLSLShaderProgram
    {
        int Alpha_Pos = -1;
        int TextureID_Pos = -1;

        public BrightnessShader(GraphicsInterface gi)
            : base(gi)
        {
            GLSLVertexShader vShader = new GLSLVertexShader(gi, Brightness_VertexSource);
            vShader.Compile();
            string tmpString = vShader.InfoLog;

            GLSLFragmentShader fShader = new GLSLFragmentShader(gi, Brightness_FragmentSource);
            fShader.Compile();
            tmpString = fShader.InfoLog;

            Link();
            // Setup variables
            bool isProgram = IsProgram;
            bool isValid = IsValid;

            Bind();
            Alpha_Pos = GetUniformLocation("bright");
            TextureID_Pos = GetUniformLocation("texture");
            Unbind();
        }

        public int TextureID
        {
            set
            {
                GI.Uniform1i(TextureID_Pos, value);
            }
        }

        public float Brighten
        {
            set
            {
                GI.Uniform1f(Alpha_Pos, value);
            }
        }

//   gl_Position = ftransform();
        public static string Brightness_VertexSource =
            @"
 void main()
 {
   gl_TexCoord[0] = gl_MultiTexCoord0;
   gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
 }
";

        public static string Brightness_FragmentSource =
            @"
uniform float bright;
uniform sampler2D texture;

void main()
{
  gl_FragColor =  texture2D(texture,gl_TexCoord[0].st) * bright;
}
";
    }

