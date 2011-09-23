using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace QuadVideo
{
    public class PowerLawTransform : UnaryTextureProcessor
    {
        float fGamma;

        public PowerLawTransform(GraphicsInterface gi, int width, int height, float gamma)
            : base(gi, width, height, Gamma_Frag)
        {
            fGamma = gamma;
        }

        public float Gamma
        {
            get { return fGamma; }
            set { fGamma = value; }
        }

        protected override void SetUniformVariables()
        {
            ShaderProgram["Gamma"].Set((float)Gamma);
            base.SetUniformVariables();
        }

        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Gamma_Frag = @"
uniform sampler2D Base;
uniform float Gamma;

const vec3 lumCoeff = vec3(.299, .587, 0.114);

void main(void)
{
    vec4 baseColor = texture2D(Base, gl_TexCoord[0].st);
    float Y = dot(baseColor.rgb, lumCoeff);    
    float corrected = pow(Y, Gamma);
   
    gl_FragColor = vec4(corrected, corrected, corrected, baseColor.a);
}
";
    }
}
