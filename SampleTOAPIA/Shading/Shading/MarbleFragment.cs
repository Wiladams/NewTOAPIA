using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class MarbleFragment : GPFragmentShader
    {
        [gpuniform]
        public sampler3D Noise;

        [gpuniform]
        public vec3 MarbleColor;
        [gpuniform]
        public vec3 VeinColor;

        [gpin]
        public float LightIntensity;
        [gpin]
        public vec3 MCPosition;

        [gpout]
        public vec4 FragColor;

        public override void main()
        {
            vec4 noisevec = texture(Noise, MCPosition);

            float intensity = abs(noisevec[0] - 0.25f) +
                abs(noisevec[1] - 0.125f) +
                abs(noisevec[2] - 0.0625f) +
                abs(noisevec[3] - 0.03125f);

            float sineval = sin(MCPosition.y * 6.0f + intensity * 12.0f) * 0.5f + 0.5f;

            vec3 color = mix(VeinColor, MarbleColor, sineval) * LightIntensity;

            FragColor = new vec4(color, 1.0f);
        }
    }
}
