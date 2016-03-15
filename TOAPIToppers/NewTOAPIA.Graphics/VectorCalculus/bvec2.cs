namespace NewTOAPIA.Graphics
{
    using System;

    public struct bvec2 : IEquatable<bvec2>
    {
        public bool x, y;

        #region Constructors
        public bvec2(bool v1)
            : this(v1, v1)
        {
        }

        public bvec2(bool v1, bool v2)
        {
            x = v1;
            y = v2;
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

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        #region Color
        public bool r { get { return x; } set { x = value; } }
        public bool g { get { return y; } set { y = value; } }
        #endregion

        #region Coordinate
        //public bool x { get { return values[0]; } set { values[0] = value; } }
        //public bool y { get { return values[1]; } set { values[1] = value; } }
        #endregion

        #region Texture Coordinate
        public bool s { get { return x; } set { x = value; } }
        public bool t { get { return y; } set { y = value; } }
        #endregion

        #region Operator Overloads

        #endregion

        #region Equality
        public static bool operator ==(bvec2 lhs, bvec2 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y ? true : false;
        }

        public static bool operator !=(bvec2 lhs, bvec2 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(bvec2 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is bvec2)
                return Equals((bvec2)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
        #endregion

        #region Helpful Methods
        public bool any()
        {
            return x || y ? true : false;
        }

        public bool all()
        {
            return x && y;
        }

        #endregion

    }
}