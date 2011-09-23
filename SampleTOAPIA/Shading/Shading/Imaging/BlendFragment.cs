using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shading.Imaging
{
    using NewTOAPIA;
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class BlendFragment : GPFragmentShader
    {
        // uniform
        public sampler2D BaseImage;
        public sampler2D BlendImage;
        public float Opacity;

        // in
        public vec2 TexCoord;

        // out
        public vec4 FragColor;


        protected ToLastType<vec4, vec4, vec4> ColorOp;

        #region Constructor
        public BlendFragment()
        {
            ColorOp = GetResult;
        }

        public BlendFragment(ToLastType<vec4, vec4, vec4> op)
        {
            ColorOp = op;
        }
        #endregion

        protected virtual vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 result = blend + baseColor;
            result = clamp(result, 0, 1);

            return result;
        }

        public override void main()
        {
            vec4 baseColor = texture(BaseImage, TexCoord.xy);
            vec4 blend = texture(BlendImage, TexCoord.xy);


            // Get the result of the operation
            vec4 color = ColorOp(baseColor, blend);

            // Deal with the opacity
            vec4 finalColor = mix(baseColor, color, Opacity);

            FragColor = finalColor;
        }
    }
}
