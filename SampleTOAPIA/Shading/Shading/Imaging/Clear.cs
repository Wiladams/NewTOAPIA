namespace Shading.Imaging 
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class Clear : BlendFragment
    {
        protected override vec4 GetResult(vec4 baseColor, vec4 blend)
        {
            vec4 result = new vec4();
            result.rgb = baseColor.rgb;
            result.a = 0;

            return result;
        }
    }
}