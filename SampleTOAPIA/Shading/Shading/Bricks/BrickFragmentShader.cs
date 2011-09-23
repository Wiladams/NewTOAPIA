namespace Shading
{
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Graphics.Processor;

    public class BrickFragmentShader : GPFragmentShader
    {
        // uniform
        [gpuniform]
        vec2 BrickSize = new vec2(0.30f, 0.15f);
        [gpuniform]
        vec2 BrickPct = new vec2(0.90f, 0.85f);
        [gpuniform]
        vec3 BrickColor = new vec3(1.0f, 0.3f, 0.2f);
        [gpuniform]
        vec3 MortarColor = new vec3(0.85f, 0.86f, 0.84f);

        // in
        [gpin]
        vec2 MCPosition;
        [gpin]
        float LightIntensity;

        // out
        [gpout]
        vec4 FragColor;

        public BrickFragmentShader()
        {
        }
        public BrickFragmentShader(vec2 size, vec2 pct, vec3 brickColor, vec3 mortColor)
        {
            BrickSize = size;
            BrickPct = pct;
            BrickColor = brickColor;
            MortarColor = mortColor;
        }

        public override void main()
        {
            vec3 color;
            vec2 position;
            vec2 useBrick;

            position = MCPosition / BrickSize;

            if (fract(position.y * 0.5f) > 0.5f)
                position.x += 0.5f;

            position = fract(position);

            useBrick = step(position, BrickPct);

            color = mix(MortarColor, BrickColor, useBrick.x * useBrick.y);
            color *= LightIntensity;

            FragColor = new vec4(color, 1.0f);
        }
    }
}