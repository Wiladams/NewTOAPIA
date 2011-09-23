
namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class Normal : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            return blend;
        }
    }
}
