namespace NewTOAPIA.Graphics
{
    using NewTOAPIA;
    using System;

    public struct vec2 : IVector, IEquatable<vec2>
    {
        public float x; // { get { return (float)this[0]; } set { this[0] = value; } }
        public float y; // { get { return (float)this[1]; } set { this[1] = value; } }

        public int Dimension { get { return 2; } }

        #region Constructors

        public vec2(float v1)
            : this(v1, v1)
        {
        }

        public vec2(vec2 rhs)
        {
            x = rhs.x;
            y = rhs.y;
        }

        public vec2(vec3 v)
            : this(v.x, v.y)
        {
        }

        public vec2(vec4 v)
            :this(v.x, v.y)
        {
        }


        public vec2(IVector v)
        {
            x = (float)v[0];
            y = (float)v[1];
        }

        public vec2(float v1, float v2)
        {
            x = v1;
            y = v2;
        }
        #endregion

        #region General Access
        public double this[int index]
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
                    case 0: x = (float)value; break;
                    case 1: y = (float)value; break;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        #region Color
        public float r { get { return x; } set { x = value; } }
        public float g { get { return y; } set { y = value; } }
        #endregion

        #region Texture Coordinate
        public float s { get { return x; } set { x = value; } }
        public float t { get { return y; } set { y = value; } }
        #endregion

        #region Operator Overloads
        public static vec2 operator +(vec2 v1, vec2 v2)
        {
            vec2 result = new vec2(v1+v2);

            return result;
        }

        public static vec2 operator +(vec2 v1, float scalar)
        {
            vec2 result = new vec2(v1.x + scalar, v1.y + scalar);

            return result;
        }

        // Negate
        public static vec2 operator -(vec2 a)
        {
            vec2 r = new vec2(-a.x, -a.y);

            return r;
        }

        public static vec2 operator -(vec2 v1, vec2 v2)
        {
            vec2 result = new vec2(v1.x - v2.x, v1.y - v2.y);

            return result;
        }

        public static vec2 operator -(vec2 v1, float scalar)
        {
            vec2 result = new vec2(v1.x - scalar, v1.y - scalar);

            return result;
        }

        public static vec2 operator *(vec2 v1, vec2 v2)
        {
            vec2 result = new vec2(v1.x * v2.x, v1.y * v2.y);

            return result;
        }

        public static vec2 operator *(vec2 v1, float scalar)
        {
            vec2 result = new vec2(v1.x * scalar, v1.y * scalar);

            return result;
        }

        public static vec2 operator *(float scalar, vec2 v1)
        {
            return v1 * scalar;
        }

        public static vec2 operator /(vec2 v1, vec2 v2)
        {
            vec2 result = new vec2(v1.x / v2.x, v1.y / v2.y);

            return result;
        }

        public static vec2 operator /(vec2 v1, float scalar)
        {
            float s = 1 / scalar;
            return v1 * scalar;
        }

        public static vec2 operator /(float scalar, vec2 v1)
        {
            vec2 r = new vec2(scalar / v1.x, scalar / v1.y);
            return r;
        }

        #endregion

        #region Equality
        public static bool operator ==(vec2 lhs, vec2 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y  ? true : false;
        }

        public static bool operator !=(vec2 lhs, vec2 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(vec2 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is vec2)
                return Equals((vec2)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ;
        }
        #endregion

        #region Helpful Methods
        public static float Dot(vec2 a, vec2 b)
        {
            return a.x * b.x + a.y * b.y ;
        }

        public float LengthSquared
        {
            get { return (float)(x * x + y * y); }
        }

        public double Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        #endregion

        #region Swizzling
        public vec2 xy
        {
            get { return this; }
            set
            {
                x = value.x;
                y = value.y;
            }
        }
        #endregion
    }
}