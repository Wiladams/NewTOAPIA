using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class Dissolve : BlendFragment
    {
        // uniform 
        float noiseScale = 1.0f;

        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            float noise = (noise1(new vec2(TexCoord * noiseScale)) + 1.0f) * 0.5f;
            vec4 result = (noise < Opacity) ? blend : baseColor;

            return result;
        }
    }
}
