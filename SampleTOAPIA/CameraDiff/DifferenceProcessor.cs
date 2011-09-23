using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;

namespace NewTOAPIA.GL.Imaging
{
    public class DifferenceProcessor : BinaryTextureProcessor
    {
        float fThreshold;

        public DifferenceProcessor(GraphicsInterface gi, int width, int height)
            : base(gi, width, height, Difference_Frag)
        {
            fThreshold = 55 / 255;
        }

        public float Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        protected override void SetUniformVariables()
        {
            ShaderProgram["Base"].Set((int)BaseTextureUnit.OrdinalForShaders);
            ShaderProgram["Blend"].Set((int)BlendTextureUnit.OrdinalForShaders);
            ShaderProgram["Threshold"].Set((float)Threshold);
        }

        public static string Difference_Frag = @"
// Beginning of program
uniform sampler2D Base;
uniform sampler2D Blend;
uniform float Threshold;

const vec3 lumCoeff = vec3(.299, .587, 0.114);

void main(void)
{
    vec4 baseColor = texture2D(Base, gl_TexCoord[0].st);
    vec4 blendColor = texture2D(Blend, gl_TexCoord[0].st);

    float baseLuminance = dot(baseColor.rgb, lumCoeff);    
    float blendLuminance = dot(blendColor.rgb, lumCoeff);

    float luminanceDiff = abs(blendLuminance = baseLuminance);

    vec3 colorDifference = abs(baseColor.rgb - blendColor.rgb);

    if (luminanceDiff > Threshold)
        gl_FragColor = vec4(colorDifference, 1.0);
    else
        gl_FragColor = vec4(0,0,0,0);
}
";
    }
}
