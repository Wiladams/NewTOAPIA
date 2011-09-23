using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Graphics
{
    public struct Vector3<T> 
        where T : struct
    {
        #region Fields
        public T x;
        public T y;
        public T z;
        #endregion

        //#region Statics
        //public static readonly Vector3<T> UnitX = new Vector3<T>(1, 0, 0);
        //public static readonly Vector3<T> UnitY = new Vector3<T>(0, 1, 0);
        //public static readonly Vector3<T> UnitZ = new Vector3<T>(0, 0, 1);
        //public static readonly Vector3<T> Empty = new Vector3<T>(0, 0, 0);
        //public static readonly Vector3<T> Zero = new Vector3<T>(0, 0, 0);
        //#endregion

#region Constructors
        public Vector3(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(T[] values)
        {
            // if values.Length < 3, throw exception
            if (values.Length < 3)
                throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

            this.x = values[0];
            this.y = values[1];
            this.z = values[2];
        }

        //public Vector3(Vector2<T> vec)
        //{
        //    this.x = vec.x;
        //    this.y = vec.y;
        //    //this.z = 0;
        //}

        //public Vector3(float3 vec)
        //{
        //    this.x = vec.x;
        //    this.y = vec.y;
        //    this.z = vec.z;
        //}

        public Vector3(Vector3<T> vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
        }

        //public Vector3f(float4 vec)
        //{
        //    this.x = vec.x;
        //    this.y = vec.y;
        //    this.z = vec.z;
        //}
        #endregion Constructors

        public void Set(T x, T y, T z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //#region Functions
        //public T Length
        //{
        //    get { return (T)Math.Sqrt((double)LengthSquared); }
        //}

        //public T LengthSquared
        //{
        //    get { return x * x + y * y + z * z; }
        //}

        //public void Normalize()
        //{
        //    T d;

        //    d = Length;
        //    if (d == 0.0)
        //    {
        //        // throw exception
        //        x = 1.0f;
        //        d = 1.0;
        //    }
        //    d = 1 / d;
        //    x *= (float)d;
        //    y *= (float)d;
        //    z *= (float)d;
        //}

        //public static Vector3f Normalize(Vector3f vec)
        //{
        //    float scale = 1.0f / vec.Length;
        //    vec.x *= scale;
        //    vec.y *= scale;
        //    vec.z *= scale;

        //    return vec;
        //}

        //public void Scale(float sx, float sy, float sz)
        //{
        //    x *= sx;
        //    y *= sy;
        //    z *= sz;
        //}
        //#endregion Functions

    }

    #region Vector3d
    public struct Vector3d
    {
        public double x;
        public double y;
        public double z;


        public Vector3d(double[] values)
        {
            // if values.Length < 3, throw exception
            if (values.Length < 3)
                throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

            this.x = values[0];
            this.y = values[1];
            this.z = values[2];
        }

        public Vector3d(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void Set(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;            
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }

        public void Normalize()
        {
            double d;

            d = Length;
            if (d == 0.0)
            {
                // throw exception
                x = d = 1.0;
            }
            d = 1 / d;
            x *= d;
            y *= d;
            z *= d;
        }



        public static double Dot(Vector3d u, Vector3d v)
        {
            return u.x*v.x + u.y*v.y + u.z*v.z;
        }

        public static Vector3d Cross(Vector3d u, Vector3d v)
        {
            Vector3d n = new Vector3d();

            n.x = u.y * v.z - u.z * v.y;
            n.y = u.z * v.x - u.x * v.z;
            n.z = u.x * v.y - u.y * v.x;

            return n;
        }

        public static explicit operator double[](Vector3d vec)
        {
            return new double[] { vec.x, vec.y, vec.z };
        }

        public override bool Equals(object obj)
        {
            // If it's not of the right type, 
            // return immediately.
            if (!(obj is Vector3d))
            //if (!obj.GetType().IsInstanceOfType(typeof(Vector3d))
                return false;

            Vector3d v = (Vector3d)obj;

            float epsilon = 0.000001f;
            
            if (Math.Abs(this.x - v.x) < epsilon &&
                Math.Abs(this.y - v.y) < epsilon &&
                Math.Abs(this.z - v.z) < epsilon) 
            {
                return true;
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return (int)Math.Floor(Length);
        }

        public override string ToString()
        {
            return string.Format("<Vector3d x='{0}', y='{1}', z='{2}' />", x, y, z); 
        }
    }
    #endregion

    #region Vector3f
    public struct Vector3f
    {
        #region Fields
        public float x;
        public float y;
        public float z;
        #endregion

        #region Statics
        public static readonly Vector3f UnitX = new Vector3f(1.0f, 0.0f, 0.0f);
        public static readonly Vector3f UnitY = new Vector3f(0.0f, 1.0f, 0.0f);
        public static readonly Vector3f UnitZ = new Vector3f(0.0f, 0.0f, 1.0f);
        public static readonly Vector3f Empty = new Vector3f(0.0f, 0.0f, 0.0f);
        public static readonly Vector3f Zero = new Vector3f(0.0f, 0.0f, 0.0f);
        #endregion

        #region Constructors
        public Vector3f(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3f(float[] values)
        {
            // if values.Length < 3, throw exception
            if (values.Length < 3)
                throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

            this.x = values[0];
            this.y = values[1];
            this.z = values[2];
        }

        public Vector3f(float2 vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = 0.0f;
        }

        public Vector3f(float3 vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
        }

        public Vector3f(Vector3f vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
        }

        public Vector3f(float4 vec)
        {
            this.x = vec.x;
            this.y = vec.y;
            this.z = vec.z;
        }
        #endregion Constructors

        public void Set(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        #region Functions
        public float Length
        {
            get { return (float)Math.Sqrt(LengthSquared); }
        }

        public float LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }

        public void Normalize()
        {
            double d;

            d = Length;
            if (d == 0.0)
            {
                // throw exception
                x = 1.0f;
                d = 1.0;
            }
            d = 1 / d;
            x *= (float)d;
            y *= (float)d;
            z *= (float)d;
        }

        public static Vector3f Normalize(Vector3f vec)
        {
            float scale = 1.0f / vec.Length;
            vec.x *= scale;
            vec.y *= scale;
            vec.z *= scale;

            return vec;
        }

        public void Scale(float sx, float sy, float sz)
        {
            x *= sx;
            y *= sy;
            z *= sz;
        }
        #endregion Functions

        #region Operator Overloads
        public static Vector3f operator +(Vector3f left, Vector3f right)
        {
            left.x += right.x;
            left.y += right.y;
            left.z += right.z;

            return left;
        }

        public static Vector3f operator -(Vector3f left, Vector3f right)
        {
            left.x -= right.x;
            left.y -= right.y;
            left.z -= right.z;

            return left;
        }

        public static Vector3f operator -(Vector3f vec)
        {
            vec.x = -vec.x;
            vec.y = -vec.y;
            vec.z = -vec.z;

            return vec;
        }

        public static Vector3f operator *(Vector3f vec, float f)
        {
            vec.x *= f;
            vec.y *= f;
            vec.z *= f;

            return vec;
        }

        public static Vector3f operator *(float f, Vector3f vec)
        {
            vec.x *= f;
            vec.y *= f;
            vec.z *= f;

            return vec;
        }

        public static Vector3f operator /(Vector3f vec, float f)
        {
            // Multiplications are faster than divisions, so compute
            // the inverse value, the multiply instead of doing
            // three multiplications.
            float mult = 1.0f / f;

            vec.x *= mult;
            vec.y *= mult;
            vec.z *= mult;

            return vec;
        }

        public double this[int i]
        {
            get
            {
                double retValue = 0.0;

                switch (i)
                {
                    case 0:
                        retValue = x;
                        break;

                    case 1:
                        retValue = y;
                        break;

                    case 2:
                        retValue = z;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("indexer", i, "Value must be between 0 and 2 inclusive.");
                }

                return retValue;
            }
        }

        public static explicit operator float[](Vector3f vec)
        {
            return new float[] { vec.x, vec.y, vec.z };
        }
        #endregion

        #region Static Functions
        public static void Mult(ref Vector3f vec, float f, out Vector3f outvec)
        {
            outvec.x = vec.x * f;
            outvec.y = vec.y * f;
            outvec.z = vec.z * f;
        }

        public static float Dot(Vector3f u, Vector3f v)
        {
            return u.x * v.x + u.y * v.y + u.z * v.z;
        }

        public static Vector3f Cross(Vector3f u, Vector3f v)
        {
            Vector3f n = new Vector3f();

            n.x = u.y * v.z - u.z * v.y;
            n.y = u.z * v.x - u.x * v.z;
            n.z = u.x * v.y - u.y * v.x;

            return n;
        }

        /// <summary>
        /// A Linear blending between the two vectors.  When 'blend' is 0,
        /// returns the 'u' vector.  When blend is 1, returns the 'v' vector.
        /// Any value in between returns a linear interpolation between the two.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="v"></param>
        /// <param name="blend"></param>
        /// <returns></returns>
        public static Vector3f Lerp(Vector3f u, Vector3f v, float blend)
        {
            u.x = blend * (v.x - u.x) + u.x;
            u.y = blend * (v.y - u.y) + u.y;
            u.z = blend * (v.z - u.z) + u.z;

            return u;
        }

        public static Vector3f BaryCentric(Vector3f a, Vector3f b, Vector3f c, float u, float v)
        {
            return a + u * (b - a) + v * (c - a);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("<Vector3f x='{0}', y='{1}', z='{2}'/>",
                x, y, z);
        }
    }
    #endregion

    public struct Vector3i
    {
        public int x;
        public int y;
        public int z;

        public Vector3i(int[] values)
        {
            // if values.Length < 3, throw exception
            if (values.Length < 3)
                throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

            this.x = values[0];
            this.y = values[1];
            this.z = values[2];
        }

        public Vector3i(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double Length
        {
            get { return Math.Sqrt(LengthSquared); }
        }

        public double LengthSquared
        {
            get { return x * x + y * y + z * z; }
        }

        public void Normalize()
        {
            double d;

            d = Length;
            if (d == 0.0)
            {
                // throw exception
                x = 1;
                d = 1.0;
            }
            d = 1 / d;
            x *= (int)d;
            y *= (int)d;
            z *= (int)d;
        }

        public static explicit operator int[](Vector3i vec)
        {
            return new int[] { vec.x, vec.y, vec.z };
        }

        public override string ToString()
        {
            return string.Format("<Vector3i x='{0}', y='{1}', z='{2}'/>",
                x, y, z);
        }

    }

    //public struct Vector3s
    //{
    //    public short x;
    //    public short y;
    //    public short z;

    //    public Vector3s(short[] values)
    //    {
    //        // if values.Length < 3, throw exception
    //        if (values.Length < 3)
    //            throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

    //        this.x = values[0];
    //        this.y = values[1];
    //        this.z = values[2];
    //    }

    //    public Vector3s(short x, short y, short z)
    //    {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }

    //    public double Length
    //    {
    //        get { return Math.Sqrt(LengthSquared); }
    //    }

    //    public double LengthSquared
    //    {
    //        get { return x * x + y * y + z * z; }
    //    }

    //    public void Normalize()
    //    {
    //        double d;

    //        d = Length;
    //        if (d == 0.0)
    //        {
    //            // throw exception
    //            x = 1;
    //            d = 1.0;
    //        }
    //        d = 1 / d;
    //        x *= (short)d;
    //        y *= (short)d;
    //        z *= (short)d;
    //    }

    //    public static explicit operator short[](Vector3s vec)
    //    {
    //        return new short[] { vec.x, vec.y, vec.z };
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("<Vector3s x='{0}', y='{1}', z='{2}'/>",
    //            x, y, z);
    //    }

    //}

    //public struct Vector3b
    //{
    //    public byte x;
    //    public byte y;
    //    public byte z;

    //    public Vector3b(byte[] values)
    //    {
    //        // if values.Length < 3, throw exception
    //        if (values.Length < 3)
    //            throw new ArgumentOutOfRangeException("values", "There must be three values in the array to assign to x, y, and z components.");

    //        this.x = values[0];
    //        this.y = values[1];
    //        this.z = values[2];
    //    }

    //    public Vector3b(byte x, byte y, byte z)
    //    {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //    }

    //    public double Length
    //    {
    //        get { return Math.Sqrt(LengthSquared); }
    //    }

    //    public double LengthSquared
    //    {
    //        get { return x * x + y * y + z * z; }
    //    }

    //    public void Normalize()
    //    {
    //        double d;

    //        d = Length;
    //        if (d == 0.0)
    //        {
    //            // throw exception
    //            x = 1;
    //            d = 1.0;
    //        }
    //        d = 1 / d;
    //        x *= (byte)d;
    //        y *= (byte)d;
    //        z *= (byte)d;
    //    }

    //    public static explicit operator byte[](Vector3b vec)
    //    {
    //        return new byte[] { vec.x, vec.y, vec.z };
    //    }
    //}

}
