using System;
using System.Collections.Generic;
using System.Text;
using NewTOAPIA.GL;

namespace RingCamView.Shaders
{
    public class GrayscaleShader : GLSLShaderProgram
    {
        public GrayscaleShader(GraphicsInterface gi)
            : base(gi)
        {
            //Grayscale_FragmentSource
        }

        public static string Grayscale_FragmentSource =
            @"
void main(void)
{
    float gray = dot(gl_color.rgb, vec3(0.299, 0.587, 0.114));

    // replicate grayscale to RGB components
    gl_FragColor = vec4(gray, gray, gray, 1.0);
}
";

        public static string Blur_FragmentSource =
            @"
// blur.fs
//
// blur (low-pass) 3x3 kernel

uniform sampler2D sampler0;
uniform vec2 tc_offset[9];

void main(void)
{
    vec4 sample[9];

    for (int i = 0; i<9; i++)
    {
        sample[i] = texture2D(sampler0, gl_TexCoord[0].st + tc_offset[i];
    }

    // 1 2 1
    // 2 1 2
    // 1 2 1

    gl_FragColor = (sample[0] + (2.0*sample[1]) + sample[2] +
                    (2.0*sample[3]) + sample[4] + (2.0*sample[5]) +
                    sample[6] + (2.0*sample[7]) + sample[8]) / 13.0;
}     
";
    }
}
