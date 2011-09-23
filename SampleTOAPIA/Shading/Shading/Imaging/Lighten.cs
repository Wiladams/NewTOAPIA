namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;

    public class Lighten : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 r = max(blend, baseColor);
            return r;
        }
    }
}