

namespace NewTOAPIA
{
    //public struct Vector4d
    //{
    //    public double v1;
    //    public double v2;
    //    public double v3;
    //    public double v4;

    //    public static Vector4d Empty = new Vector4d();

    //    public Vector4d(double[] values)
    //    {
    //        v1 = values[0];
    //        v2 = values[1];
    //        v3 = values[2];
    //        v4 = values[3];
    //    }

    //    public Vector4d(float[] values)
    //    {
    //        v1 = values[0];
    //        v2 = values[1];
    //        v3 = values[2];
    //        v4 = values[3];
    //    }

    //    public Vector4d(double a, double b, double c, double d)
    //    {
    //        v1 = a;
    //        v2 = b;
    //        v3 = c;
    //        v4 = d;
    //    }

    //    public static explicit operator double[](Vector4d aVector)
    //    {
    //        return new double[] { aVector.v1, aVector.v2, aVector.v3, aVector.v4 };
    //    }
    
    //    public static explicit operator float[](Vector4d aVector)
    //    {
    //        return new float[] { (float)aVector.v1, (float)aVector.v2, (float)aVector.v3, (float)aVector.v4 };
    //    }
    //}


    //public struct float4
    //{
    //    public float x;
    //    public float y;
    //    public float z;
    //    public float w;

    //    public static float4 UnitX = new float4(1.0f, 0.0f, 0.0f, 0.0f);
    //    public static float4 UnitY = new float4(0.0f, 1.0f, 0.0f, 0.0f);
    //    public static float4 UnitZ = new float4(0.0f, 0.0f, 1.0f, 0.0f);
    //    public static float4 UnitW = new float4(0.0f, 0.0f, 0.0f, 1.0f);
    //    public static float4 Zero = new float4(0.0f, 0.0f, 0.0f, 0.0f);
    //    public static float4 Empty = new float4();

    //    public float4(float[] values)
    //    {
    //        x = values[0];
    //        y = values[1];
    //        z = values[2];
    //        w = values[3];
    //    }

    //    public float4(float x, float y, float z)
    //    {
    //        this.x = x;
    //        this.y = y;
    //        this.z = z;
    //        this.w = 0.0f;
    //    }

    //    public float4(float a, float b, float c, float d)
    //    {
    //        z = a;
    //        y = b;
    //        x = c;
    //        w = d;
    //    }
    //    #region Operator overloads

    //    public static float4 operator +(float4 left, float4 right)
    //    {
    //        left.x += right.x;
    //        left.y += right.y;
    //        left.z += right.z;
    //        left.w += right.w;
    //        return left;
    //    }

    //    public static float4 operator -(float4 left, float4 right)
    //    {
    //        left.x -= right.x;
    //        left.y -= right.y;
    //        left.z -= right.z;
    //        left.w -= right.w;
    //        return left;
    //    }

    //    public static float4 operator -(float4 vec)
    //    {
    //        vec.x = -vec.x;
    //        vec.y = -vec.y;
    //        vec.z = -vec.z;
    //        vec.w = -vec.w;
    //        return vec;
    //    }

    //    public static float4 operator *(float4 vec, float f)
    //    {
    //        vec.x *= f;
    //        vec.y *= f;
    //        vec.z *= f;
    //        vec.w *= f;
    //        return vec;
    //    }

    //    public static float4 operator *(float f, float4 vec)
    //    {
    //        vec.x *= f;
    //        vec.y *= f;
    //        vec.z *= f;
    //        vec.w *= f;
    //        return vec;
    //    }

    //    public static float4 operator /(float4 vec, float f)
    //    {
    //        float mult = 1.0f / f;
    //        vec.x *= mult;
    //        vec.y *= mult;
    //        vec.z *= mult;
    //        vec.w *= mult;
    //        return vec;
    //    }


    //    #endregion

    //    public static float Dot(float4 left, float4 right)
    //    {
    //        return left.x * right.x + left.y * right.y + left.z * right.z + left.w * right.w;
    //    }

    //    public static explicit operator float[](float4 vec)
    //    {
    //        return new float[] { vec.x, vec.y, vec.z, vec.w };
    //    }
    //}

    //public struct Vector4i
    //{
    //    public int v1;
    //    public int v2;
    //    public int v3;
    //    public int v4;

    //    public static Vector4i Empty = new Vector4i();

    //    public Vector4i(int[] values)
    //    {
    //        v1 = values[0];
    //        v2 = values[1];
    //        v3 = values[2];
    //        v4 = values[3];
    //    }

    //    public Vector4i(int a, int b, int c, int d)
    //    {
    //        v1 = a;
    //        v2 = b;
    //        v3 = c;
    //        v4 = d;
    //    }

    //    public static explicit operator int[](Vector4i vec)
    //    {
    //        return new int[] { vec.v1, vec.v2, vec.v3, vec.v4 };
    //    }
    //}

    //public struct Vector4s
    //{
    //    public short v1;
    //    public short v2;
    //    public short v3;
    //    public short v4;

    //    public static Vector4s Empty = new Vector4s();

    //    public Vector4s(short[] values)
    //    {
    //        v1 = values[0];
    //        v2 = values[1];
    //        v3 = values[2];
    //        v4 = values[3];
    //    }

    //    public Vector4s(short a, short b, short c, short d)
    //    {
    //        v1 = a;
    //        v2 = b;
    //        v3 = c;
    //        v4 = d;
    //    }


    //    public static explicit operator short[](Vector4s vec)
    //    {
    //        return new short[] { vec.v1, vec.v2, vec.v3, vec.v4 };
    //    }
    //}

}
