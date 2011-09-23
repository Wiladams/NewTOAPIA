namespace Shading.Imaging
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class Average : BlendFragment
    {
        public Average()
            : base(GetResult)
        {
        }

        static vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            return (baseColor + blend) * 0.5f;
        }
    }
}
