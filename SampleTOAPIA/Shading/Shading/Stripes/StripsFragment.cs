using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Stripes
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class StripsFragment : GPFragmentShader
    {
        // uniform
        [gpuniform]
        public vec3 StripeColor;
        [gpuniform]
        public vec3 BackColor;
        [gpuniform]
        public float Width;
        [gpuniform]
        public float Fuzz;
        [gpuniform]
        public float Scale;

        // in
        [gpin]
        public vec3 DiffuseColor;
        [gpin]
        public vec3 SpecularColor;
        [gpin]
        public float TexCoord;

        // out
        [gpout]
        public vec4 FragColor;

        public override void main()
        {
            float scaledT = fract(TexCoord * Scale);

            float frac1 = clamp(scaledT / Fuzz, 0, 1);
            float frac2 = clamp((scaledT - Width) / Fuzz, 0, 1);

            frac1 = frac1 * (1.0f - frac2);
            frac1 = frac1 * frac1 * (3.0f - (2.0f * frac1));    // Smoothing

            vec3 finalColor = mix(BackColor, StripeColor, frac1);
            finalColor = finalColor * DiffuseColor + SpecularColor;

            FragColor = new vec4(finalColor, 1);
        }
    }
}
