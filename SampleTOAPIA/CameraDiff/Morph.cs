using System;

using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace NewTOAPIA.GL.Imaging
{
    public class Morph : BinaryTextureProcessor
    {
        float fAlpha;   // 1.0 == full blend image, 0.0 == full base image

        public Morph(GraphicsInterface gi, int width, int height)
            : base(gi, width, height, Average_Frag)
        {
        }

        #region Properties
        public float Alpha
        {
            get { return fAlpha; }
            set { fAlpha = value; }
        }
        #endregion

        protected override void SetUniformVariables()
        {
            ShaderProgram["Base"].Set((int)BaseTextureUnit.OrdinalForShaders);
            ShaderProgram["Blend"].Set((int)BlendTextureUnit.OrdinalForShaders);
            ShaderProgram["Alpha"].Set((float)Alpha);
        }

        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Average_Frag = @"
// Beginning of program
uniform sampler2D Base;
uniform sampler2D Blend;
uniform  float Alpha;

void main(void)
{
    vec3 baseColor = texture2D(Base, gl_TexCoord[0].st).rgb;
    vec3 blendColor = texture2D(Blend, gl_TexCoord[0].st).rgb;

    vec3 mixedColor = mix(baseColor, blendColor, Alpha);

    gl_FragColor = vec4(mixedColor, 1.0);
}
";
    }
}
