namespace NewTOAPIA.Graphics
{
    using System;

    public struct vec4 : IVector, IEquatable<vec4>
    {
        public float x, y, z, w;

        public int Dimension { get { return 4; } }

        #region Constructors
        public vec4(float v1)
            : this(v1, v1, v1, v1)
        {
        }

        public vec4(IVector v)
        {
            x = (float)v[0];
            y = (float)v[1];
            z = (float)v[2];
            w = (float)v[3];
        }

        public vec4(vec3 v1, float w_)
        {
            x = v1.x;
            y = v1.y;
            z = v1.z;
            w = w_;
        }

        public vec4(float v1, float v2, float v3, float v4)
        {
            x = v1;
            y = v2;
            z = v3;
            w = v4;
        }
        #endregion

        #region General Access
        // Use varying parameter list, return IVector
        public vec3 Swizzle(SWZL s1, SWZL s2, SWZL s3)
        {
            vec3 r;

            r.x = (float)this[(int)s1];
            r.y = (float)this[(int)s2];
            r.z = (float)this[(int)s3];

            return r;
        }

        public double this[int index]
        {
            get {
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

            set {
                switch (index)
                {
                    case 0: x = (float)value; break;
                    case 1: y = (float)value; break;
                    case 2: z = (float)value; break;
                    case 3: w = (float)value; break; 

                    default:
                        throw new ArgumentOutOfRangeException("index");
                }
            }
        }
        #endregion

        #region Color
        public vec3 rgb { get { return new vec3(r, g, b); } set { x = value.r; y = value.g; z = value.b; a = 1; } }
        public float r { get { return x; } set { x = value; } }
        public float g { get { return y; } set { y = value; } }
        public float b { get { return z; } set { z = value; } }
        public float a { get { return w; } set { w = value; } }
        #endregion

        #region Coordinate
        //public float x { get { return values[0]; } set { values[0] = value; } }
        //public float y { get { return values[1]; } set { values[1] = value; } }
        //public float z { get { return values[2]; } set { values[2] = value; } }
        //public float w { get { return values[3]; } set { values[3] = value; } }
        #endregion

        #region Texture Coordinate
        public float s { get { return x; } set { x = value; } }
        public float t { get { return y; } set { y = value; } }
        public float p { get { return z; } set { z = value; } }
        public float q { get { return w; } set { w = value; } }
        #endregion

        #region Arithmetic Operators
        public static vec4 operator+(vec4 v1, vec4 v2)
        {
            vec4 result;
            result.x = v1.x + v2.x;
            result.y = v1.y + v2.y;
            result.z = v1.z + v2.z;
            result.w = v1.w + v2.w;

            return result;
        }

        public static vec4 operator +(vec4 v1, float scalar)
        {
            vec4 result;
            result.x = v1.x + scalar;
            result.y = v1.y + scalar;
            result.z = v1.z + scalar;
            result.w = v1.w + scalar;

            return result;
        }

        // Negate
        public static vec4 operator -(vec4 a)
        {
            vec4 r;
            r.x = -a.x;
            r.y = -a.y;
            r.z = -a.z;
            r.w = -a.w;

            return r;
        }

        public static vec4 operator -(vec4 v1, vec4 v2)
        {
            vec4 result;
            result.x = v1.x - v2.x;
            result.y = v1.y - v2.y;
            result.z = v1.z - v2.z;
            result.w = v1.w - v2.w;

            return result;
        }

        public static vec4 operator -(vec4 v1, float scalar)
        {
            vec4 result;
            result.x = v1.x - scalar;
            result.y = v1.y - scalar;
            result.z = v1.z - scalar;
            result.w = v1.w - scalar;

            return result;
        }

        public static vec4 operator *(vec4 v1, vec4 v2)
        {
            vec4 result;
            result.x = v1.x * v2.x;
            result.y = v1.y * v2.y;
            result.z = v1.z * v2.z;
            result.w = v1.w * v2.w;

            return result;
        }

        public static vec4 operator *(vec4 v1, float scalar)
        {
            vec4 result;
            result.x = v1.x * scalar;
            result.y = v1.y * scalar;
            result.z = v1.z * scalar;
            result.w = v1.w * scalar;

            return result;
        }

        public static vec4 operator *(float scalar, vec4 v1)
        {
            vec4 result;
            result.x = v1.x * scalar;
            result.y = v1.y * scalar;
            result.z = v1.z * scalar;
            result.w = v1.w * scalar;

            return result;
        }

        public static vec4 operator /(vec4 v1, vec4 v2)
        {
            vec4 result;
            result.x = v1.x / v2.x;
            result.y = v1.y / v2.y;
            result.z = v1.z / v2.z;
            result.w = v1.w / v2.w;

            return result;
        }

        public static vec4 operator /(vec4 v1, float scalar)
        {
            vec4 result;
            float tmp = 1f / scalar;

            result.x = v1.x * tmp;
            result.y = v1.y * tmp;
            result.z = v1.z * tmp;
            result.w = v1.w * tmp;

            return result;
        }

        
        #endregion

        public static implicit operator vec3(vec4 v1)
        {
            return new vec3(v1);
        }

        #region Equality
        public static bvec4 operator <(vec4 lhs, vec4 rhs)
        {
            bvec4 r;
            r.x = lhs.x < rhs.x;
            r.y = lhs.y < rhs.y;
            r.z = lhs.z < rhs.z;
            r.w = lhs.w < rhs.w;
            return r;
        }

        public static bvec4 operator >(vec4 lhs, vec4 rhs)
        {
            bvec4 r;
            r.x = lhs.x > rhs.x;
            r.y = lhs.y > rhs.y;
            r.z = lhs.z > rhs.z;
            r.w = lhs.w > rhs.w;
            return r;
        }

        public static bool operator ==(vec4 lhs, vec4 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w ? true : false;
        }

        public static bool operator !=(vec4 lhs, vec4 rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(vec4 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object obj)
        {
            if (obj is vec4)
                return Equals((vec4)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode() ^ w.GetHashCode();
        }
        #endregion

        #region Helpful Methods

        // Cross Product
        public static vec4 Cross(vec4 a, vec4 b)
        {
            vec4 r;
            r.x = a.y * b.z - b.y * a.z;
            r.y = a.z * b.x - b.z * a.x;
            r.z = a.x * b.y - b.x * a.y;
            r.w = 0f;

            return r;
        }

        public static float Dot(vec4 a, vec4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public double LengthSquared
        {
            get { return (x * x + y * y + z * z + w * w); }
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public void Unit()
        {
            float l = (float)(1 / Length);
            x *= l;
            y *= l;
            z *= l;
            w *= l;
        }

        #endregion

    }
}