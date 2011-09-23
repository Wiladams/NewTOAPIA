using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace QuadVideo
{
    /// <summary>
    /// This processor performs a color filtering based on a threshold luminance
    /// value.
    /// 
    /// The luminance Property (which defaults to 0) is used to determine whether a color
    /// will be passed along, or will be set to transparent (0,0,0,0).  This will work
    /// with grayscale as well as color images.
    /// </summary>
    public class LuminanceThreshold : UnaryTextureProcessor
    {
        float fThreshold;

        public LuminanceThreshold(GraphicsInterface gi, int width, int height, float threshold)
            : base(gi, width, height, Threshold_Frag)
        {
            fThreshold = threshold;
        }

        public float Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        protected override void SetUniformVariables()
        {
            ShaderProgram["Threshold"].Set((float)Threshold);
        }

        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Threshold_Frag = @"
uniform sampler2D Base;
uniform float Threshold;

const vec3 lumCoeff = vec3(.299, .587, 0.114);

void main(void)
{
    vec4 baseColor = texture2D(Base, gl_TexCoord[0].st);
    float Y = dot(baseColor.rgb, lumCoeff);    
    
    if (Y > Threshold)
        gl_FragColor = baseColor;
    else
        gl_FragColor = vec4(0,0,0,0);
}
";
    }
}
