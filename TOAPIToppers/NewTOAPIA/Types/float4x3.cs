

namespace NewTOAPIA
{
    using System;

    public struct float4x3
    {
        public float M11, M12, M13,
            M21, M22, M23,
            M31, M32, M33,
            M41, M42, M43;

        public float4x3(float a00, float a01, float a02,
            float a10, float a11, float a12,
            float a20, float a21, float a22,
            float a30, float a31, float a32)
        {
            M11 = a00; M12 = a01; M13 = a02;
            M21 = a10; M22 = a11; M23 = a12;
            M31 = a20; M32 = a21; M33 = a22;
            M41 = a30; M42 = a31; M43 = a32;
        }

        public float4x3(float3x3 R, float3 t)
        {
            M11 = R.M11; M12 = R.M12; M13 = R.M13;
            M21 = R.M21; M22 = R.M22; M23 = R.M23;
            M31 = R.M31; M32 = R.M32; M33 = R.M33;
            M41 = t.x; M42 = t.y; M43 = t.z;
        }

        public float4x3(float4 r0, float4 r1, float4 r2)
        {
            M11 = r0.x; M12 = r1.x; M13 = r2.x;
            M21 = r0.y; M22 = r1.y; M23 = r2.y;
            M31 = r0.z; M32 = r1.z; M33 = r2.z;
            M41 = r0.w; M42 = r1.w; M43 = r2.w;
        }

        public float4x3(float3 r0, float3 r1, float3 r2, float3 r3)
        {
            M11 = r0.x; M12 = r0.y; M13 = r0.z;
            M21 = r1.x; M22 = r1.y; M23 = r1.z;
            M31 = r2.x; M32 = r2.y; M33 = r2.z;
            M41 = r3.x; M42 = r3.y; M43 = r3.z;
        }


        public float3 r0
        {
            get { return new float3(M11, M12, M13); }
            set { M11 = value.x; M12 = value.x; M13 = value.x; }
        }

        public float3 r1
        {
            get { return new float3(M21, M22, M23); }
            set { M21 = value.x; M22 = value.x; M23 = value.x; }
        }

        public float3 r2
        {
            get { return new float3(M31, M32, M33); }
            set { M31 = value.x; M32 = value.x; M33 = value.x; }
        }

        public float3 r3
        {
            get { return new float3(M41, M42, M43); }
            set { M41 = value.x; M42 = value.x; M43 = value.x; }
        }

        public float4 NullSpace
        {
            get
            {
                return new float4(
                    M23 * M32 * M41 - M22 * M33 * M41 - M23 * M31 * M42 + M21 * M33 * M42 + M22 * M31 * M43 - M21 * M32 * M43,
                    -(M13 * M32 * M41) + M12 * M33 * M41 + M13 * M31 * M42 - M11 * M33 * M42 - M12 * M31 * M43 + M11 * M32 * M43,
                    M13 * M22 * M41 - M12 * M23 * M41 - M13 * M21 * M42 + M11 * M23 * M42 + M12 * M21 * M43 - M11 * M22 * M43,
                    -(M13 * M22 * M31) + M12 * M23 * M31 + M13 * M21 * M32 - M11 * M23 * M32 - M12 * M21 * M33 + M11 * M22 * M33);
            }
        }

        public static float4x4 operator *(float4x3 a, float3x4 b)
        {
            return new float4x4(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,
                a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,
                a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33,
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34,

                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33,
                a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34);
        }

        public static float4x3 operator *(float4x4 a, float4x3 b)
        {
            return new float4x3(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,

                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43);
        }

        public static float4x3 operator *(float4x3 a, float3x3 b)
        {
            return new float4x3(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33,

                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33);
        }

        public static float4x3 operator /(float4x3 a, float d)
        {
            float s = 1f / d;

            return new float4x3(s * a.M11, s * a.M12, s * a.M13,
                s * a.M21, s * a.M22, s * a.M23,
                s * a.M31, s * a.M32, s * a.M33,
                s * a.M41, s * a.M42, s * a.M43);
        }

        public static float3 operator *(float4 v, float4x3 m)
        {
            return new float3(
                v.x * m.M11 + v.y * m.M21 + v.z * m.M31 + v.w * m.M41,
                v.x * m.M12 + v.y * m.M22 + v.z * m.M32 + v.w * m.M42,
                v.x * m.M13 + v.y * m.M23 + v.z * m.M33 + v.w * m.M43);
        }

        public static float4 operator *(float4x3 m, float3 v)
        {
            return new float4(
                v.x * m.M11 + v.y * m.M12 + v.z * m.M13,
                v.x * m.M21 + v.y * m.M22 + v.z * m.M23,
                v.x * m.M31 + v.y * m.M32 + v.z * m.M33,
                v.x * m.M41 + v.y * m.M42 + v.z * m.M43);
        }

        public static float4x3 Zero
        {
            get
            {
                return new float4x3(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }


        public float3x4 Transpose
        {
            get { return new float3x4(M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43); }
        }


        public float3 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return new float3(M11, M12, M13);
                    case 1:
                        return new float3(M21, M22, M23);
                    case 2:
                        return new float3(M31, M32, M33);
                    default:
                        return new float3(M41, M42, M43);
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        M11 = value.x;
                        M12 = value.y;
                        M13 = value.z;
                        break;
                    case 1:
                        M21 = value.x;
                        M22 = value.y;
                        M23 = value.z;
                        break;
                    case 2:
                        M31 = value.x;
                        M32 = value.y;
                        M33 = value.z;
                        break;
                    default:
                        M41 = value.x;
                        M42 = value.y;
                        M43 = value.z;
                        break;
                }
            }
        }


    }
}
