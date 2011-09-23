using System;
using System.Collections.Generic;
using System.Text;

    public class ShaderSources
    {
        public const string grayscale_fs = @"
// grayscale.fs
//
// convert RGB to grayscale
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);

    // Convert to grayscale using NTSC conversion weights
    float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114));

    // replicate grayscale to RGB components
    gl_FragColor = vec4(gray, gray, gray, 1.0);
}
";

        public const string sepia_fs = @"
// sepia.fs
//
// convert RGB to sepia tone
uniform sampler2D tex0;

void main(void)
{
    vec4 color = texture2D(tex0, gl_TexCoord[0].xy);
    
    // Convert to grayscale using NTSC conversion weights
    float gray = dot(color.rgb, vec3(0.299, 0.587, 0.114));

    // convert grayscale to sepia
    gl_FragColor = vec4(gray * vec3(1.2, 1.0, 0.8), 1.0);
}
";
    }

