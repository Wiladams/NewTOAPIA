namespace NewTOAPIA.Graphics
{
    using System;

    public struct vec3 : IVector, IEquatable<vec3>
    {
        public float x, y, z;

        public int Dimension { get { return 3; } }

        #region Constructors
        public vec3(float v1)
        {
            x = v1;
            y = v1;
            z = v1;
        }

        public vec3(vec2 v)
        {
            x = v.x;
            y = v.y;
            z = 0;
        }

        public vec3(vec4 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public vec3(IVector v)
        {
            x = (float)v[0];
            y = (float)v[1];
            z = (float)v[2];
        }
        
        public vec3(float v1, float v2, float v3)
        {
            x = v1;
            y = v2;
            z = v3;
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
                    case 2: return z;

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
                    case 2: z = (float)value; break;

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        #region Color
        public float r { get { return x; } set { x = value; } }
        public float g { get { return y; } set { y = value; } }
        public float b { get { return z; } set { z = value; } }
        #endregion

        #region Texture Coordinate
        public float s { get { return x; } set { x = value; } }
        public float t { get { return y; } set { y = value; } }
        public float p { get { return z; } set { z = value; } }
        #endregion

        #region Operator Overloads

        public static vec3 operator +(vec3 v1, vec3 v2)
        {
            vec3 result;
            result.x = v1.x + v2.x;
            result.y = v1.y + v2.y;
            result.z = v1.z + v2.z;

            return result;
        }

        public static vec3 operator +(vec3 v1, float scalar)
        {
            vec3 result;
            result.x = v1.x + scalar;
            result.y = v1.y + scalar;
            result.z = v1.z + scalar;

            return result;
        }

        // Negate
        public static vec3 operator -(vec3 a)
        {
            vec3 r;
            r.x = -a.x;
            r.y = -a.y;
            r.z = -a.z;

            return r;
        }

        public static vec3 operator -(vec3 v1, vec3 v2)
        {
            vec3 result;
            result.x = v1.x - v2.x;
            result.y = v1.y - v2.y;
            result.z = v1.z - v2.z;

            return result;
        }

        public static vec3 operator -(vec3 v1, float scalar)
        {
            vec3 result;
            result.x = v1.x - scalar;
            result.y = v1.y - scalar;
            result.z = v1.z - scalar;

            return result;
        }

        public static vec3 operator *(vec3 v1, vec3 v2)
        {
            vec3 result;
            result.x = v1.x * v2.x;
            result.y = v1.y * v2.y;
            result.z = v1.z * v2.z;

            return result;
        }

        public static vec3 operator *(vec3 v1, float scalar)
        {
            vec3 result;
            result.x = v1.x * scalar;
            result.y = v1.y * scalar;
            result.z = v1.z * scalar;

            return result;
        }

        public static vec3 operator *(float scalar, vec3 v1)
        {
            vec3 result;
            result.x = v1.x * scalar;
            result.y = v1.y * scalar;
            result.z = v1.z * scalar;

            return result;
        }

        public static vec3 operator /(vec3 v1, vec3 v2)
        {
            vec3 result;
            result.x = v1.x / v2.x;
            result.y = v1.y / v2.y;
            result.z = v1.z / v2.z;

            return result;
        }

        public static vec3 operator /(vec3 v1, float scalar)
        {
            vec3 result;
            float tmp = 1f / scalar;

            result.x = v1.x * tmp;
            result.y = v1.y * tmp;
            result.z = v1.z * tmp;

            return result;
        }

        public static vec3 operator /(float scalar, vec3 v1)
        {
            vec3 result;

            result.x = scalar / v1.x;
            result.y = scalar / v1.y;
            result.z = scalar / v1.z;

            return result;
        }

        #endregion

        #region Equality
        public static bool operator ==(vec3 lhs, vec3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z  ? true : false;
        }

        public static bool operator !=(vec3 lhs, vec3 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(vec3 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is vec3)
                return Equals((vec3)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }
        #endregion

        #region Helpful Methods
        //public static float Dot(vec3 a, vec3 b)
        //{
        //    return a.x * b.x + a.y * b.y + a.z * b.z;
        //}

        public float LengthSquared
        {
            get { return (float)(x * x + y * y + z * z); }
        }

        public double Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        #endregion

        #region Swizzling
        #endregion
    }
}