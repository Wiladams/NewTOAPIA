using System;
using System.Globalization;     // For NumberFormatInfo, CultureInfo

namespace NewTOAPIA.GL
{
    public struct float4
    {
        public float x, y, z, w;

        public static float4 UnitX = new float4(1.0f, 0.0f, 0.0f, 0.0f);
        public static float4 UnitY = new float4(0.0f, 1.0f, 0.0f, 0.0f);
        public static float4 UnitZ = new float4(0.0f, 0.0f, 1.0f, 0.0f);
        public static float4 UnitW = new float4(0.0f, 0.0f, 0.0f, 1.0f);
        public static float4 Zero = new float4(0.0f, 0.0f, 0.0f, 0.0f);
        public static float4 Empty = new float4();

        public float4(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public float4(double x, double y, double z, double w)
        {
            this.x = (float)x;
            this.y = (float)y;
            this.z = (float)z;
            this.w = (float)w;
        }

        public float4(float3 a, float w)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = a.z;
            this.w = w;
        }


        public float3 xyz
        {
            get { return new float3(x, y, z); }
        }




        public static float4 operator +(float4 a, float4 b)
        {
            float4 r;
            r.x = a.x + b.x;
            r.y = a.y + b.y;
            r.z = a.z + b.z;
            r.w = a.w + b.w;
            return r;
        }

        public static float4 operator -(float4 a)
        {
            float4 r;
            r.x = -a.x;
            r.y = -a.y;
            r.z = -a.z;
            r.w = -a.w;
            return r;
        }

        public static float4 operator -(float4 a, float4 b)
        {
            float4 r;
            r.x = a.x - b.x;
            r.y = a.y - b.y;
            r.z = a.z - b.z;
            r.w = a.w - b.w;
            return r;
        }

        public static float4 operator *(float s, float4 a)
        {
            float4 r;
            r.x = s * a.x;
            r.y = s * a.y;
            r.z = s * a.z;
            r.w = s * a.w;
            return r;
        }

        public static float4 operator *(double s, float4 a)
        {
            float4 r;
            r.x = (float)s * a.x;
            r.y = (float)s * a.y;
            r.z = (float)s * a.z;
            r.w = (float)s * a.w;
            return r;
        }


        public static float operator *(float4 a, float4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        //public static float dot(float4 a, float4 b)
        //{
        //    return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        //}

        public static float4 operator /(float4 a, float d)
        {
            float4 r; float s = 1f / d;
            r.x = s * a.x;
            r.y = s * a.y;
            r.z = s * a.z;
            r.w = s * a.w;
            return r;
        }

        public static float4 operator *(float4 v, float4x4 m)
        {
            float4 r;
            r.x = v.x * m.M11 + v.y * m.M21 + v.z * m.M31 + v.w * m.M41;
            r.y = v.x * m.M12 + v.y * m.M22 + v.z * m.M32 + v.w * m.M42;
            r.z = v.x * m.M13 + v.y * m.M23 + v.z * m.M33 + v.w * m.M43;
            r.w = v.x * m.M14 + v.y * m.M24 + v.z * m.M34 + v.w * m.M44;
            return r;
        }

        public static float4 operator *(float4x4 m, float4 v)
        {
            float4 r;
            r.x = v.x * m.M11 + v.y * m.M12 + v.z * m.M13 + v.w * m.M14;
            r.y = v.x * m.M21 + v.y * m.M22 + v.z * m.M23 + v.w * m.M24;
            r.z = v.x * m.M31 + v.y * m.M32 + v.z * m.M33 + v.w * m.M34;
            r.w = v.x * m.M41 + v.y * m.M42 + v.z * m.M43 + v.w * m.M44;
            return r;
        }


        public static float4 operator ^(float4 a, float4 b)
        {
            float4 r;
            r.x = a.y * b.z - b.y * a.z;
            r.y = a.z * b.x - b.z * a.x;
            r.z = a.x * b.y - b.x * a.y;
            r.w = 0f;
            return r;
        }

        public static float4 Crs4(float4 a, float4 b, float4 c)
        {
            return new float4(
                a.z * b.y * c.w - a.y * b.z * c.w - a.z * b.w * c.y + a.w * b.z * c.y + a.y * b.w * c.z - a.w * b.y * c.z,
                -(a.z * b.x * c.w) + a.x * b.z * c.w + a.z * b.w * c.x - a.w * b.z * c.x - a.x * b.w * c.z + a.w * b.x * c.z,
                a.y * b.x * c.w - a.x * b.y * c.w - a.y * b.w * c.x + a.w * b.y * c.x + a.x * b.w * c.y - a.w * b.x * c.y,
                -(a.z * b.y * c.x) + a.y * b.z * c.x + a.z * b.x * c.y - a.x * b.z * c.y - a.y * b.x * c.z + a.x * b.y * c.z);
        }

        public float LengthSquared
        {
            get { return (float)(x * x + y * y + z * z + w*w); }
        }

        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        public float PlaneNorm
        {
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        public float4 Normalize
        {
            get { return this / PlaneNorm; }
        }

        public void Unit()
        {
            float l = 1f / Length;
            x *= l;
            y *= l;
            z *= l;
            w *= l;
        }

        public float4 Homogenize
        {
            get { return this / this.w; }
        }

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
                    case 3:
                        return w;
                    default:
                        // Should throw index out of range exception.
                        // or not.  The GPU treats index out of range
                        // as undefined behavior.  Here, we want to allow
                        // the developer to catch such instances and correct them
                        // so we'll throw.
                        throw new IndexOutOfRangeException();
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
                    case 3:
                        w = value;
                        break;

                    default:
                        // Should throw index out of range exception.
                        // or not.  The GPU treats index out of range
                        // as undefined behavior.  Here, we want to allow
                        // the developer to catch such instances and correct them
                        // so we'll throw.
                        throw new IndexOutOfRangeException();
                }
            }
        }

        public static explicit operator float[](float4 vec)
        {
            return new float[] { vec.x, vec.y, vec.z, vec.w };
        }

        public static float4 Plane(ref float4 p1, ref float4 p2, ref float4 p3)
        {
            float4 a = p2 - p1;
            float4 b = p2 - p3;
            float4 pln = b ^ a;
            pln.w = -pln * p1;

            return pln;
        }

        public static float4 VectorFrom3Points(ref float4 p1, ref float4 p2, ref float4 p3)
        {
            float4 a = p2 - p1;
            float4 b = p2 - p3;
            float4 n = b ^ a;

            n.Unit();
            return n;
        }


        public override string ToString()
        {
            // Gets a NumberFormatInfo associated with the en-US culture.
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            // Displays the same value with four decimal digits.
            nfi.NumberDecimalDigits = 6;

            return "{" + x.ToString("F", nfi) + "," + y.ToString("F", nfi) + "," + z.ToString("F", nfi) + "}";

            //			return String.Format("{0},{1},{2},{3}", x, y, z, w);
        }

        public static float4 ParsePoint(string cdata)
        {
            string[] str = cdata.Split(new char[] { ',' });
            return new float4(Single.Parse(str[0]), Single.Parse(str[1]), Single.Parse(str[2]), 1f);
        }

        public static float4 ParseVector(string cdata)
        {
            string[] str = cdata.Split(new char[] { ',' });
            return new float4(Single.Parse(str[0]), Single.Parse(str[1]), Single.Parse(str[2]), 0f);
        }

    }

    //public class V4FConverter : ExpandableObjectConverter
    //{
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        if (sourceType == typeof(string))
    //        {
    //            return true;
    //        }
    //        return base.CanConvertFrom(context, sourceType);
    //    }

    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (destinationType == typeof(string))
    //        {
    //            return ((float4)value).x + "," + ((float4)value).y + "," + ((float4)value).z;
    //        }
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }

    //}


    //public class V4FPointConverter : V4FConverter
    //{
    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        if (value is string)
    //        {
    //            string[] v = ((string)value).Split(new char[] { ',' });
    //            return new float4(Single.Parse(v[0]), Single.Parse(v[1]), Single.Parse(v[2]), 1f);
    //        }
    //        return base.ConvertFrom(context, culture, value);
    //    }
    //}


    //public class V4FVectorConverter : V4FConverter
    //{
    //    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    //    {
    //        if (value is string)
    //        {
    //            string[] v = ((string)value).Split(new char[] { ',' });
    //            return new float4(Single.Parse(v[0]), Single.Parse(v[1]), Single.Parse(v[2]), 0f);
    //        }
    //        return base.ConvertFrom(context, culture, value);
    //    }
    //}
}
