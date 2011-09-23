using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
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
    public class LuminanceBinarizer : UnaryTextureProcessor
    {
        float fThreshold;
        ColorRGBA fOverColor;
        ColorRGBA fUnderColor;

        public LuminanceBinarizer(GraphicsInterface gi, int width, int height, float threshold)
            : base(gi, width, height, Binarizer_Frag)
        {
            fThreshold = threshold;
            OverColor = ColorRGBA.White;
            UnderColor = ColorRGBA.Invisible;
        }

        public ColorRGBA OverColor
        {
            get { return fOverColor; }
            set { fOverColor = value; }
        }

        public ColorRGBA UnderColor
        {
            get { return fUnderColor; }
            set { fUnderColor = value; }
        }

        public float Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        protected override void SetUniformVariables()
        {
            ShaderProgram["Threshold"].Set((float)Threshold);

            ShaderProgram["OverRed"].Set((float)OverColor.R);
            ShaderProgram["OverGreen"].Set((float)OverColor.G);
            ShaderProgram["OverBlue"].Set((float)OverColor.B);
            ShaderProgram["OverAlpha"].Set((float)OverColor.A);

            ShaderProgram["UnderRed"].Set((float)UnderColor.R);
            ShaderProgram["UnderGreen"].Set((float)UnderColor.G);
            ShaderProgram["UnderBlue"].Set((float)UnderColor.B);
            ShaderProgram["UnderAlpha"].Set((float)UnderColor.A);

        }

        /// <summary>
        /// This fragment shader will separate an incoming texture, as 
        /// specified in the tex0 uniform variable, into a Y, Cr, and Cb
        /// channel.  Its operation is dependent on the grahics driver support
        /// of the GL_ARB_draw_buffers extension.  This will not operate on 
        /// all machines.
        /// </summary>
        public static string Binarizer_Frag = @"
uniform sampler2D Base;
uniform float Threshold;

uniform float OverRed;
uniform float OverGreen;
uniform float OverBlue;
uniform float OverAlpha;

uniform float UnderRed;
uniform float UnderGreen;
uniform float UnderBlue;
uniform float UnderAlpha;


const vec3 lumCoeff = vec3(.299, .587, 0.114);
const vec4 white = vec4(1,1,1,1);
const vec4 transparent = vec4(0,0,0,0);

void main(void)
{
    vec4 OverColor = vec4(OverRed, OverGreen, OverBlue, OverAlpha);
    vec4 UnderColor = vec4(UnderRed, UnderGreen, UnderBlue, UnderAlpha);

    vec4 baseColor = texture2D(Base, gl_TexCoord[0].st);
    
    // Calculate the luminance of the base color
    float Y = dot(baseColor.rgb, lumCoeff);    

    // Based on whether the luminance is over or under the threshold
    // value, output the Over, or Under color.
    if (Y > Threshold)
        gl_FragColor = OverColor;
    else
        gl_FragColor = UnderColor;
}
";
    }
}
