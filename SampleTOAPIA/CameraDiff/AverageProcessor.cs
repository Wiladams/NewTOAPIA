using System;

using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace NewTOAPIA.GL.Imaging
{
    public class AverageProcessor : BinaryTextureProcessor
    {
        int fNumberOfSamples;
        int maxSamples = 8;

        public AverageProcessor(GraphicsInterface gi, int width, int height)
            : base(gi, width, height, Average_Frag)
        {
        }

        public int NumberOfSamples
        {
            get { return fNumberOfSamples; }
            set { fNumberOfSamples = value; }
        }

        protected override void SetUniformVariables()
        {
            //if (fNumberOfSamples < maxSamples)
            //    fNumberOfSamples++;
            fNumberOfSamples = maxSamples;

            ShaderProgram["Base"].Set((int)BaseTextureUnit.OrdinalForShaders);
            ShaderProgram["Blend"].Set((int)BlendTextureUnit.OrdinalForShaders);
            ShaderProgram["numSamples"].Set((float)fNumberOfSamples);
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
uniform  float numSamples;

void main(void)
{
    vec3 baseColor = texture2D(Base, gl_TexCoord[0].st).rgb;
    vec3 blendColor = texture2D(Blend, gl_TexCoord[0].st).rgb;

    vec3 mixedColor = mix(baseColor, blendColor, 1.0 / numSamples);
    gl_FragColor = vec4(mixedColor, 1.0);
}
";   }
}
