﻿namespace NewTOAPIA.Graphics
{
    using System;

    public struct bvec3 : IEquatable<bvec3>
    {
        public bool x, y, z;

        #region Constructors
        public bvec3(bool v1)
            : this(v1, v1, v1)
        {
        }

        public bvec3(bool v1, bool v2, bool v3)
        {
            x = v1;
            y = v2;
            z = v3;
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
        #endregion

        #region Coordinate
        //public bool x { get { return values[0]; } set { values[0] = value; } }
        //public bool y { get { return values[1]; } set { values[1] = value; } }
        //public bool z { get { return values[2]; } set { values[2] = value; } }
        #endregion

        #region Texture Coordinate
        public bool s { get { return x; } set { x = value; } }
        public bool t { get { return y; } set { y = value; } }
        public bool p { get { return z; } set { z = value; } }
        #endregion

        #region Operator Overloads

        #endregion

        #region Equality
        public static bool operator ==(bvec3 lhs, bvec3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z ? true : false;
        }

        public static bool operator !=(bvec3 lhs, bvec3 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(bvec3 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is bvec3)
                return Equals((bvec3)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }
        #endregion

        #region Helpful Methods
        public bool any()
        {
            return x || y || z ? true : false;
        }

        public bool all()
        {
            return x && y && z;
        }

        #endregion

        #region Swizzling
        #endregion
    }
}