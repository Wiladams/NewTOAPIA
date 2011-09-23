using System;

namespace NewTOAPIA
{
    /// <summary>
    /// A representation of two consecutive floating point values.  It is primarily 
    /// used to facilitate the implementation of various graphics APIs.
    /// </summary>
    public struct float2 : IEquatable<float2>
    {
        public float x, y;

        #region Constructors
        public float2(float x, float y)
        {
            this.x = x; this.y = y;
        }

        public float2(double x, double y)
        {
            this.x = (float)x;
            this.y = (float)y;
        }
        #endregion

        public static float2 operator +(float2 a, float2 b)
        {
            return new float2(a.x + b.x, a.y + b.y);
        }

        public static float2 operator -(float2 a, float2 b)
        {
            return new float2(a.x - b.x, a.y - b.y);
        }

        public static float2 operator -(float2 a)
        {
            return new float2(-a.x, -a.y);
        }


        public static float2 operator *(float s, float2 a)
        {
            return new float2(s * a.x, s * a.y);
        }

        public static float2 operator /(float2 a, float d)
        {
            float s = 1f / d;
            return new float2(s * a.x, s * a.y);
        }

        public static float operator *(float2 a, float2 b)
        {
            return a.x * b.x + a.y * b.y;
        }


        public static float3 operator ^(float2 a0, float2 a1)
        {
            return new float3(a0.y - a1.y, a1.x - a0.x, a0.x * a1.y - a0.y * a1.x);
        }

        public float2 Normalize
        {
            get
            {
                float l = 1f / (float)Math.Sqrt(x * x + y * y);
                return l * this;
            }
        }

        public float LengthSquared
        {
            get { return (float)(x * x + y * y); }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        public float2 Perp
        {
            get { return new float2(-y, x); }
        }

        public bool Equals(float2 rhs)
        {
            return (this == rhs);
        }

        public override bool Equals(object o)
        {
            if (!(o is float2))
                return false;
            return (this == (float2)o);
        }

        public static bool operator !=(float2 a, float2 b)
        {
            return a.x != b.x || a.y != b.y ? true : false;
        }

        public static bool operator ==(float2 a, float2 b)
        {
            return a.x == b.x && a.y == b.y ? true : false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }

        public override string ToString()
        {
            return "<float2 x='" + x + "', y='" + y + "' />";
        }

    }
}
