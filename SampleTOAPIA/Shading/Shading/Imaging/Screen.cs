namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;

    public class Screen : BlendFragment
    {
        static vec4 white = new vec4(1,1,1,1);

        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 white = new vec4(1, 1, 1, 1);

            vec4 r = white - ((white - blend) * (white - baseColor));

            return r;
        }
    }
}