using System;

namespace NewTOAPIA
{
    public struct float3 : IEquatable<float3>
    {
        public float x, y, z;

        #region Constructors
        public float3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public float3(double x, double y, double z)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
        }

        public float3(float2 p, float z)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = z;
        }

        public float3(float2 p)
        {
            this.x = p.x;
            this.y = p.y;
            this.z = 1f;
        }
        #endregion

        public void Unit()
        {
            float l = 1f / Length;
            x *= l;
            y *= l;
            z *= l;
        }

        #region Static Operator Overloading
        public static float3 operator +(float3 a, float3 b)
        {
            return new float3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static float3 operator -(float3 a)
        {
            return new float3(-a.x, -a.y, -a.z);
        }

        public static float3 operator -(float3 a, float3 b)
        {
            return new float3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static float3 operator *(float s, float3 a)
        {
            return new float3(s * a.x, s * a.y, s * a.z);
        }

        public static float3 operator *(float3 a, float s)
        {
            return new float3(s * a.x, s * a.y, s * a.z);
        }

        public static float3 operator *(double r, float3 a)
        {
            float s = (float)r;
            return new float3(s * a.x, s * a.y, s * a.z);
        }

        public static float operator *(float3 a, float3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static float3 Cross(float3 u, float3 v)
        {
            float3 n = new float3();

            n.x = u.y * v.z - u.z * v.y;
            n.y = u.z * v.x - u.x * v.z;
            n.z = u.x * v.y - u.y * v.x;

            return n;
        }

        public static float3 operator /(float3 a, float d)
        {
            float s = 1f / d;
            return new float3(s * a.x, s * a.y, s * a.z);
        }

        public static float3 operator *(float3 v, float3x3 m)
        {
            return new float3(v.x * m.M11 + v.y * m.M21 + v.z * m.M31, v.x * m.M12 + v.y * m.M22 + v.z * m.M32, v.x * m.M13 + v.y * m.M23 + v.z * m.M33);
        }

        public static float3 operator *(float3x3 m, float3 v)
        {
            return new float3(v.x * m.M11 + v.y * m.M12 + v.z * m.M13, v.x * m.M21 + v.y * m.M22 + v.z * m.M23, v.x * m.M31 + v.y * m.M32 + v.z * m.M33);
        }

        public static bool operator !=(float3 a, float3 b)
        {
            return a.x != b.x || a.y != b.y || a.z != b.z ? true : false;
        }

        public static bool operator ==(float3 a, float3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z ? true : false;
        }

        public static explicit operator float[](float3 vec)
        {
            return new float[] { vec.x, vec.y, vec.z };
        }

        #endregion

        public float this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return x;
                    case 1:
                        return y;
                    case 2:
                        return z;
                    default:
                        return 0f;
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x = value;
                        break;
                    case 1:
                        y = value;
                        break;
                    case 2:
                        z = value;
                        break;
                }
            }
        }

        public float LengthSquared
        {
            get { return (float)(x * x + y * y + z * z); }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }


        public float3 Perp
        {
            get { return new float3(-y, x, 0); }
        }


        public float3x3 CRS
        {
            get
            {
                return new float3x3(0, -z, y,
                                z, 0, -x,
                               -y, x, 0);
            }
        }


        public float3 Homogenize
        {
            get { return new float3(x / z, y / z, 1); }
        }

        public float3 Normalize
        {
            get
            {
                float length = Length;
                if (length == 0)
                {
                    throw new DivideByZeroException("Can not normalize a vector when it's length is zero.");
                }
                float inv = 1 / length;

                return inv * this;
            }
        }

        public float2 xy
        {
            get { return new float2(x, y); }
        }

        // Cross product
        public static float3 operator ^(float3 a0, float3 a1)
        {
            return new float3(a0.y * a1.z - a0.z * a1.y, a0.z * a1.x - a0.x * a1.z, a0.x * a1.y - a0.y * a1.x);
        }

        public static float3 Zero
        {
            get { return new float3(0f, 0f, 0f); }
        }

        public bool Equals(float3 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object o)
        {
            if (!(o is float3))
                return false;
            return (this == (float3)o);
        }


        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public override string ToString()
        {
            return "{" + x + ", " + y + ", " + z + "}";
        }
    }
}
