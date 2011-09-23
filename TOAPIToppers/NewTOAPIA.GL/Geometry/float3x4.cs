using System;

namespace NewTOAPIA.GL
{
    public struct float3x4
    {
        public float M11, M12, M13, M14,
                     M21, M22, M23, M24,
                     M31, M32, M33, M34;

        public float3x4(float a00, float a01, float a02, float a03,
            float a10, float a11, float a12, float a13,
            float a20, float a21, float a22, float a23)
        {
            M11 = a00; M12 = a01; M13 = a02; M14 = a03;
            M21 = a10; M22 = a11; M23 = a12; M24 = a13;
            M31 = a20; M32 = a21; M33 = a22; M34 = a23;
        }

        public float3x4(float3x3 R, float3 t)
        {
            M11 = R.M11; M12 = R.M12; M13 = R.M13; M14 = t.x;
            M21 = R.M21; M22 = R.M22; M23 = R.M23; M24 = t.y;
            M31 = R.M31; M32 = R.M32; M33 = R.M33; M34 = t.z;
        }


        public float3x4(float4 R1, float4 R2, float4 R3)
        {
            M11 = R1.x; M12 = R1.y; M13 = R1.z; M14 = R1.w;
            M21 = R2.x; M22 = R2.y; M23 = R2.z; M24 = R2.w;
            M31 = R3.x; M32 = R3.y; M33 = R3.z; M34 = R3.w;
        }

        public static float3x3 operator *(float3x4 a, float4x3 b)
        {
            float3x3 r;
            r.M11 = a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41;
            r.M12 = a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42;
            r.M13 = a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43;

            r.M21 = a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41;
            r.M22 = a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42;
            r.M23 = a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43;

            r.M31 = a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41;
            r.M32 = a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42;
            r.M33 = a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43;
            return r;
        }

        public static float3x4 operator *(float3x3 a, float3x4 b)
        {
            return new float3x4(
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
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34);
        }

        public static float3x4 operator *(float3x4 a, float4x4 b)
        {
            return new float3x4(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M14 * b.M41,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M14 * b.M42,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M14 * b.M43,
                a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M14 * b.M44,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M14 * b.M41,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M14 * b.M42,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M14 * b.M43,
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M14 * b.M44);
        }

        public static float3x4 operator /(float3x4 a, float d)
        {
            float s = 1f / d;

            return new float3x4(s * a.M11, s * a.M12, s * a.M13, s * a.M14,
                s * a.M21, s * a.M22, s * a.M23, s * a.M24,
                s * a.M31, s * a.M32, s * a.M33, s * a.M34);
        }


        public static float4 operator *(float3 v, float3x4 m)
        {
            return new float4(
                v.x * m.M11 + v.y * m.M21 + v.z * m.M31,
                v.x * m.M12 + v.y * m.M22 + v.z * m.M32,
                v.x * m.M13 + v.y * m.M23 + v.z * m.M33,
                v.x * m.M14 + v.y * m.M24 + v.z * m.M34);
        }

        public static float3 operator *(float3x4 m, float4 v)
        {
            return new float3(
                v.x * m.M11 + v.y * m.M12 + v.z * m.M13 + v.w * m.M14,
                v.x * m.M21 + v.y * m.M22 + v.z * m.M23 + v.w * m.M24,
                v.x * m.M31 + v.y * m.M32 + v.z * m.M33 + v.w * m.M34);
        }


        public static float3x4 Zero
        {
            get
            {
                return new float3x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
        }


        public float4x3 Transpose
        {
            get { return new float4x3(M11, M21, M31, M12, M22, M32, M13, M23, M33, M14, M24, M34); }
        }

        public float4x3 PseudoInverse
        {
            get
            {
                return this.Transpose * ((this * this.Transpose).Inverse);
            }
        }


        public float4 this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        return new float4(M11, M12, M13, M14);
                    case 1:
                        return new float4(M21, M22, M23, M24);
                    default:
                        return new float4(M31, M32, M33, M34);
                }
            }
        }

        public float this[int col, int row]
        {
            get
            {
                int index = col * 4 + row;
                switch (index)
                {
                        // First column
                    case 0:
                        return M11;
                    case 1:
                        return M12;
                    case 2:
                        return M13;
                    case 3:
                        return M14;

                    // Second column
                    case 4:
                        return M21;
                    case 5:
                        return M22;
                    case 6:
                        return M23;
                    case 7:
                        return M24;

                    // Third column
                    case 8:
                        return M31;
                    case 9:
                        return M32;
                    case 10:
                        return M33;
                    case 11:
                        return M34;
                    default:
                        return 0.0f;
                }
            }

            set
            {
                int index = col * 4 + row;
                switch (index)
                {
                        // First column
                    case 0:
                        M11 = value;
                        break;
                    case 1:
                        M12 = value;
                        break;
                    case 2:
                        M13 = value;
                        break;
                    case 3:
                        M14 = value;
                        break;

                    // Second column
                    case 4:
                        M21 = value ;
                        break;
                    case 5:
                        M22 = value;
                        break;
                    case 6:
                        M23 = value;
                        break;
                    case 7:
                        M24 = value ;
                        break;

                    // Third column
                    case 8:
                        M31 = value ;
                        break;
                    case 9:
                        M32 = value;
                        break;
                    case 10:
                        M33 = value;
                        break;
                    case 11:
                        M34 = value;
                        break;
                }
            }

        }

        public override string ToString()
        {
            return "{{" +
                M11 + ", " + M12 + ", " + M13 + ", " + M14 + "},{" +
                M21 + ", " + M22 + ", " + M23 + ", " + M24 + "},{" +
                M31 + ", " + M32 + ", " + M33 + ", " + M34 + "}}";
        }


    }
}
