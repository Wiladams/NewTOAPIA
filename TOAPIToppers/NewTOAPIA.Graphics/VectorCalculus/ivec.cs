namespace NewTOAPIA.Graphics
{
    public struct ivec2
    {
        public int x;
        public int y;

        public ivec2(int i)
        {
            x = i;
            y = i;
        }

        public ivec2(ivec3 xyz)
        {
            x = xyz.x;
            y = xyz.y;
        }

        public ivec2(int x_, int y_)
        {
            x = x_;
            y = y_;
        }
    }

    public struct ivec3
    {
        public int x;
        public int y;
        public int z;

        #region Constructors
        public ivec3(int i)
        {
            x = i;
            y = i;
            z = i;
        }

        public ivec3(ivec2 xy, int z_)
        {
            x = xy.x;
            y = xy.y;
            z = z_;
        }

        public ivec3(ivec4 xyzw)
        {
            x = xyzw.x;
            y = xyzw.y;
            z = xyzw.z;
        }

        public ivec3(int x_, int y_, int z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }
        #endregion
    }

    public struct ivec4
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public ivec4(int i)
        {
            x = i;
            y = i;
            z = i;
            w = i;
        }

        public ivec4(int x_, int y_, int z_, int w_)
        {
            x = x_;
            y = y_;
            z = z_;
            w = w_;
        }
    }

}