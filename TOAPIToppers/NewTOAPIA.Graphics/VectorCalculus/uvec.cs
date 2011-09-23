namespace NewTOAPIA.Graphics
{
    public struct uvec2
    {
        public uint x;
        public uint y;

        public uvec2(uint i)
        {
            x = i;
            y = i;
        }

        public uvec2(uvec3 xyz)
        {
            x = xyz.x;
            y = xyz.y;
        }

        public uvec2(uint x_, uint y_)
        {
            x = x_;
            y = y_;
        }
    }

    public struct uvec3
    {
        public uint x;
        public uint y;
        public uint z;

        #region Constructors
        public uvec3(uint i)
        {
            x = i;
            y = i;
            z = i;
        }

        public uvec3(uvec2 xy, uint z_)
        {
            x = xy.x;
            y = xy.y;
            z = z_;
        }

        public uvec3(uvec4 xyzw)
        {
            x = xyzw.x;
            y = xyzw.y;
            z = xyzw.z;
        }

        public uvec3(uint x_, uint y_, uint z_)
        {
            x = x_;
            y = y_;
            z = z_;
        }
        #endregion
    }


    public struct uvec4
    {
        public uint x;
        public uint y;
        public uint z;
        public uint w;

        public uvec4(uint i)
        {
            x = i;
            y = i;
            z = i;
            w = i;
        }

        public uvec4(uint x_, uint y_, uint z_, uint w_)
        {
            x = x_;
            y = y_;
            z = z_;
            w = w_;
        }
    }

}