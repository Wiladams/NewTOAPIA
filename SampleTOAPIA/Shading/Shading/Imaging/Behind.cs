using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class Behind : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 result = (baseColor.a == 0.0) ? blend : baseColor;

            return result;
        }
    }
}
