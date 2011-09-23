namespace NewTOAPIA.Graphics
{
    using System;

    public struct bvec4 : IEquatable<bvec4>
    {
        public bool x, y, z, w;
        //r,g,b,a
        //s,t,p,q
        //public bool[] values = new bool[4];

        #region Constructors
        public bvec4(bool v1)
            : this(v1, v1, v1, v1)
        {
        }

        public bvec4(bool v1, bool v2, bool v3, bool v4)
        {
            x = v1;
            y = v2;
            z = v3;
            w = v4;
        }
        #endregion

        #region General Access
        public bool this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return x;
                    case 1: return y;
                    case 2: return z;
                    case 3: return w;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }

            set
            {
                switch (index)
                {
                    case 0: x = value; break;
                    case 1: y = value; break;
                    case 2: z = value; break;
                    case 3: w = value; break;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        #region Color
        public bool r { get { return x; } set { x = value; } }
        public bool g { get { return y; } set { y = value; } }
        public bool b { get { return z; } set { z = value; } }
        public bool a { get { return w; } set { w = value; } }
        #endregion

        #region Coordinate
        //public bool x { get { return values[0]; } set { values[0] = value; } }
        //public bool y { get { return values[1]; } set { values[1] = value; } }
        //public bool z { get { return values[2]; } set { values[2] = value; } }
        //public bool w { get { return values[3]; } set { values[3] = value; } }
        #endregion

        #region Texture Coordinate
        public bool s { get { return x; } set { x = value; } }
        public bool t { get { return y; } set { y = value; } }
        public bool p { get { return z; } set { z = value; } }
        public bool q { get { return w; } set { w = value; } }
        #endregion

        #region Operator Overloads

        #endregion

        #region Equality
        public static bool operator ==(bvec4 lhs, bvec4 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w ? true : false;
        }

        public static bool operator !=(bvec4 lhs, bvec4 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(bvec4 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is bvec4)
                return Equals((bvec4)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }
        #endregion

        #region Helpful Methods
        public bool any()
        {
            return x || y || z || w ? true : false;
        }

        public bool all()
        {
            return x && y && z && w;
        }

        #endregion

        #region Swizzling
        #endregion
    }
}