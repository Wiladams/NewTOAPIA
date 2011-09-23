using System;

namespace NewTOAPIA.GL
{
    public struct float3x3
    {
        public float M11, M12, M13,
                      M21, M22, M23,
                      M31, M32, M33;

        public float3x3(float a00, float a01, float a02,
            float a10, float a11, float a12,
            float a20, float a21, float a22)
        {
            M11 = a00; M12 = a01; M13 = a02;
            M21 = a10; M22 = a11; M23 = a12;
            M31 = a20; M32 = a21; M33 = a22;
        }

        public float3x3(double a00, double a01, double a02,
            double a10, double a11, double a12,
            double a20, double a21, double a22)
        {
            M11 = (float)a00; M12 = (float)a01; M13 = (float)a02;
            M21 = (float)a10; M22 = (float)a11; M23 = (float)a12;
            M31 = (float)a20; M32 = (float)a21; M33 = (float)a22;
        }

        public float3x3(float2 r0, float2 r1, float2 r2)
        {
            M11 = r0.x; M12 = r0.y; M13 = 1f;
            M21 = r1.x; M22 = r1.y; M23 = 1f;
            M31 = r2.x; M32 = r2.y; M33 = 1f;
        }

        public float3x3(float3 r0, float3 r1, float3 r2)
        {
            M11 = r0.x; M12 = r0.y; M13 = r0.z;
            M21 = r1.x; M22 = r1.y; M23 = r1.z;
            M31 = r2.x; M32 = r2.y; M33 = r2.z;
        }


        public static float3x3 operator *(float3x3 a, float3x3 b)
        {
            return new float3x3(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33,
                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33,
                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33);
        }

        public static float3x3 operator +(float3x3 a, float3x3 b)
        {
            return new float3x3(
                a.M11 + b.M11,
                a.M12 + b.M12,
                a.M13 + b.M13,
                a.M21 + b.M21,
                a.M22 + b.M22,
                a.M23 + b.M23,
                a.M31 + b.M31,
                a.M32 + b.M32,
                a.M33 + b.M33);
        }

        public static float3x3 operator -(float3x3 a, float3x3 b)
        {
            return new float3x3(
                a.M11 - b.M11,
                a.M12 - b.M12,
                a.M13 - b.M13,
                a.M21 - b.M21,
                a.M22 - b.M22,
                a.M23 - b.M23,
                a.M31 - b.M31,
                a.M32 - b.M32,
                a.M33 - b.M33);
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
                    default:
                        return new float3(M31, M32, M33);
                }
            }
        }


        public float this[int i, int j]
        {
            get
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0:
                                return M11;
                            case 1:
                                return M12;
                            case 2:
                                return M13;
                            default:
                                return 0f;
                        }
                    case 1:
                        switch (j)
                        {
                            case 0:
                                return M21;
                            case 1:
                                return M22;
                            case 2:
                                return M23;
                            default:
                                return 0f;
                        }
                    case 2:
                        switch (j)
                        {
                            case 0:
                                return M31;
                            case 1:
                                return M32;
                            case 2:
                                return M33;
                            default:
                                return 0f;
                        }
                    default:
                        return 0f;
                }
            }
            set
            {
                switch (i)
                {
                    case 0:
                        switch (j)
                        {
                            case 0:
                                M11 = value;
                                break;
                            case 1:
                                M12 = value;
                                break;
                            case 2:
                                M13 = value;
                                break;
                        }
                        break;
                    case 1:
                        switch (j)
                        {
                            case 0:
                                M21 = value;
                                break;
                            case 1:
                                M22 = value;
                                break;
                            case 2:
                                M23 = value;
                                break;
                        }
                        break;
                    case 2:
                        switch (j)
                        {
                            case 0:
                                M31 = value;
                                break;
                            case 1:
                                M32 = value;
                                break;
                            case 2:
                                M33 = value;
                                break;
                        }
                        break;
                }
            }
        }

        public static float3x3 operator /(float3x3 a, float d)
        {
            float s = 1f / d;

            return new float3x3(s * a.M11, s * a.M12, s * a.M13,
                s * a.M21, s * a.M22, s * a.M23,
                s * a.M31, s * a.M32, s * a.M33);
        }

        public static float3x3 operator *(float s, float3x3 a)
        {
            return new float3x3(s * a.M11, s * a.M12, s * a.M13,
                s * a.M21, s * a.M22, s * a.M23,
                s * a.M31, s * a.M32, s * a.M33);
        }



        public static float3x3 Identity
        {
            get { return new float3x3(1, 0, 0, 0, 1, 0, 0, 0, 1); }
        }

        public static float3x3 Zero
        {
            get { return new float3x3(0, 0, 0, 0, 0, 0, 0, 0, 0); }
        }

        public float Determinant
        {
            get { return (M12 * M23 * M31 - M13 * M22 * M31 + M13 * M21 * M32 - M11 * M23 * M32 - M12 * M21 * M33 + M11 * M22 * M33); }
        }

        public static float3x3 Cross(float3 v)
        {
            return new float3x3(0, -v.z, v.y, v.z, 0, -v.x, -v.y, v.x, 0);
        }

        public void translate(float tx, float ty)
        {
            M31 = tx; M32 = ty;
        }

        public void scale(float sx, float sy)
        {
            M11 *= sx;
            M12 *= sy;
            M21 *= sx;
            M22 *= sy;

            M31 *= sx;
            M32 *= sy;
        }

        public void scale(float s)
        {
            M11 *= s;
            M12 *= s;
            M21 *= s;
            M22 *= s;

            M31 *= s;
            M32 *= s;
        }


        /// <summary>
        /// Rotation by angle theta about unit axis u
        /// </summary>
        public static float3x3 Rotate(float theta, float3 u)
        {
            if (theta == 0f) return Identity;

            float3 w = new float3(theta * u.x, theta * u.y, theta * u.z);

            float3x3 wx = w.CRS;

            return Identity + (((float)Math.Sin(theta)) / theta) * wx + ((1 - ((float)Math.Cos(theta))) / (theta * theta)) * (wx * wx);
        }

        /// <summary>
        /// Scale by sx, sy
        /// </summary>
        public static float3x3 Scale(float sx, float sy)
        {
            return new float3x3(sx, 0f, 0f,
                            0f, sy, 0f,
                            0f, 0f, 1f);
        }

        public float3x3 Inverse
        {
            get
            {
                float oneoverdet = 1.0f / Determinant;

                return new float3x3((-(M23 * M32) + M22 * M33) * oneoverdet,
                    (M13 * M32 - M12 * M33) * oneoverdet,
                    (-(M13 * M22) + M12 * M23) * oneoverdet,

                    (M23 * M31 - M21 * M33) * oneoverdet,
                    (-(M13 * M31) + M11 * M33) * oneoverdet,
                    (M13 * M21 - M11 * M23) * oneoverdet,

                    (-(M22 * M31) + M21 * M32) * oneoverdet,
                    (M12 * M31 - M11 * M32) * oneoverdet,
                    (-(M12 * M21) + M11 * M22) * oneoverdet);
            }
        }

        public float3x3 Adjoint
        {
            get
            {
                return new float3x3((-(M23 * M32) + M22 * M33),
                    (M13 * M32 - M12 * M33),
                    (-(M13 * M22) + M12 * M23),

                    (M23 * M31 - M21 * M33),
                    (-(M13 * M31) + M11 * M33),
                    (M13 * M21 - M11 * M23),

                    (-(M22 * M31) + M21 * M32),
                    (M12 * M31 - M11 * M32),
                    (-(M12 * M21) + M11 * M22));
            }
        }

        public float3x3 Transpose
        {
            get { return new float3x3(M11, M21, M31, M12, M22, M32, M13, M23, M33); }
        }

        public float3 Row(int i)
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
                    return new float3(0, 0, 0);
            }
        }

        public float3 Col(int i)
        {
            switch (i)
            {
                case 0:
                    return new float3(M11, M21, M31);
                case 1:
                    return new float3(M12, M22, M32);
                case 2:
                    return new float3(M13, M23, M33);
                default:
                    return new float3(0, 0, 0);
            }
        }


        public override string ToString()
        {
            return "{" + Row(0).ToString() + "," + Row(1).ToString() + "," + Row(2).ToString() + "}";
        }

    }
}
