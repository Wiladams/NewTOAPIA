namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;

    public class Darken : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 r = min(blend, baseColor);
            return r;
        }
    }
}