namespace NewTOAPIA.Graphics.Processor
{
    using NewTOAPIA.Graphics;

    abstract public class GPFragmentShader : GPShader
    {
        public bool Discarded { get; protected set; }
        public ColorRGBA FragmentColor { get; protected set; }

        void discard()
        {
            Discarded = true;
        }

        #region Fragment Processing Functions (5.8)
        #region dFdx
        public static float dFdx(float p)
        {
            return 0;
        }

        public static vec2 dFdx(vec2 p)
        {
            vec2 r = new vec2();

            return r;
        }

        public static vec3 dFdx(vec3 p)
        {
            vec3 r = new vec3();

            return r;
        }

        public static vec4 dFdx(vec4 p)
        {
            vec4 r = new vec4();

            return r;
        }
        #endregion dFdx

        #region dFdy
        public static float dFdy(float p)
        {
            return 0;
        }

        public static vec2 dFdy(vec2 p)
        {
            vec2 r = new vec2();

            return r;
        }

        public static vec3 dFdy(vec3 p)
        {
            vec3 r = new vec3();

            return r;
        }

        public static vec4 dFdy(vec4 p)
        {
            vec4 r = new vec4();

            return r;
        }
        #endregion dFdy

        #region fWidth
        public static float fWidth(float p)
        {
            return abs(dFdx(p)) + abs(dFdy(p));
        }

        public static vec2 fWidth(vec2 p)
        {
            return abs(dFdx(p)) + abs(dFdy(p));
        }

        public static vec3 fWidth(vec3 p)
        {
            return abs(dFdx(p)) + abs(dFdy(p));
        }

        public static vec4 fWidth(vec4 p)
        {
            return abs(dFdx(p)) + abs(dFdy(p));
        }
        #endregion fWidth

        #endregion

    }
}