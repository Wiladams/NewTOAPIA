namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;

    public class Multiply : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 r = blend * baseColor;

            return r;
        }
    }
}