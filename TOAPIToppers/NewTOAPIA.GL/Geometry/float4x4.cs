using System;


namespace NewTOAPIA.GL
{
    public class float4x4Exception : Exception
    {
        public float4x4Exception(string message)
            : base(message)
        {
        }
    }


    public struct float4x4
    {
        public float M11, M12, M13, M14,
            M21, M22, M23, M24,
            M31, M32, M33, M34,
            M41, M42, M43, M44;

        public float4x4(float a00, float a01, float a02, float a03,
            float a10, float a11, float a12, float a13,
            float a20, float a21, float a22, float a23,
            float a30, float a31, float a32, float a33)
        {
            M11 = a00; M12 = a01; M13 = a02; M14 = a03;
            M21 = a10; M22 = a11; M23 = a12; M24 = a13;
            M31 = a20; M32 = a21; M33 = a22; M34 = a23;
            M41 = a30; M42 = a31; M43 = a32; M44 = a33;
        }

        public float4x4(double a00, double a01, double a02, double a03,
            double a10, double a11, double a12, double a13,
            double a20, double a21, double a22, double a23,
            double a30, double a31, double a32, double a33)
        {
            M11 = (float)a00; M12 = (float)a01; M13 = (float)a02; M14 = (float)a03;
            M21 = (float)a10; M22 = (float)a11; M23 = (float)a12; M24 = (float)a13;
            M31 = (float)a20; M32 = (float)a21; M33 = (float)a22; M34 = (float)a23;
            M41 = (float)a30; M42 = (float)a31; M43 = (float)a32; M44 = (float)a33;
        }



        public float4x4(float4 r0, float4 r1, float4 r2, float4 r3)
        {
            M11 = r0.x; M12 = r0.y; M13 = r0.z; M14 = r0.w;
            M21 = r1.x; M22 = r1.y; M23 = r1.z; M24 = r1.w;
            M31 = r2.x; M32 = r2.y; M33 = r2.z; M34 = r2.w;
            M41 = r3.x; M42 = r3.y; M43 = r3.z; M44 = r3.w;
        }

        public float4x4(float3x3 R, float3 t)
        {
            M11 = R.M11; M12 = R.M12; M13 = R.M13; M14 = 0;
            M21 = R.M21; M22 = R.M22; M23 = R.M23; M24 = 0;
            M31 = R.M31; M32 = R.M32; M33 = R.M33; M34 = 0;
            M41 = t.x; M42 = t.y; M43 = t.z; M44 = 1;
        }

        public static float4x4 operator *(float4x4 a, float4x4 b)
        {
            return new float4x4(
                a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
                a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
                a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
                a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

                a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
                a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
                a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
                a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

                a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
                a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
                a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
                a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

                a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
                a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
                a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
                a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44);
        }

        public static float4x4 operator *(float s, float4x4 a)
        {
            return new float4x4(
                s * a.M11, s * a.M12, s * a.M13, s * a.M14,
                s * a.M21, s * a.M22, s * a.M23, s * a.M24,
                s * a.M31, s * a.M32, s * a.M33, s * a.M34,
                s * a.M41, s * a.M42, s * a.M43, s * a.M44);
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
                            case 3:
                                return M14;
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
                            case 3:
                                return M24;
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
                            case 3:
                                return M34;
                            default:
                                return 0f;
                        }
                    case 3:
                        switch (j)
                        {
                            case 0:
                                return M41;
                            case 1:
                                return M42;
                            case 2:
                                return M43;
                            case 3:
                                return M44;
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
                            case 3:
                                M14 = value;
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
                            case 3:
                                M24 = value;
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
                            case 3:
                                M34 = value;
                                break;
                        }
                        break;
                    case 3:
                        switch (j)
                        {
                            case 0:
                                M41 = value;
                                break;
                            case 1:
                                M42 = value;
                                break;
                            case 2:
                                M43 = value;
                                break;
                            case 3:
                                M44 = value;
                                break;
                        }
                        break;
                }
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
                    case 2:
                        return new float4(M31, M32, M33, M34);
                    default:
                        return new float4(M41, M42, M43, M44);
                }
            }
        }



        //static
        // Locate the row in A with the largest value in the 
        // c'th column.  Only columns between min and max-1 are
        //considered.

        private void Pivot(ref float4x4 T, int c, int[] R, int min, int max)
        {
            int r, pivot, tmp;

            pivot = min;
            for (r = min + 1; r < max; r++)
                if (Math.Abs(T[R[r], c]) > Math.Abs(T[R[pivot], c]))
                    pivot = r;
            if (pivot != min)
            {
                tmp = R[pivot]; R[pivot] = R[min]; R[min] = tmp;
            }
        }



        //  This function returns the inverse of a 4D transform.
        public float4x4 Inverse
        {
            get
            {
                float4x4 T = new float4x4();
                float4x4 Result = new float4x4();
                int c, r, j, k;
                int[] R = new int[4];
                int i;

                // This is usual, by hand, method for inverting a matrix.
                //  We initialize the result matrix to the identity, then
                //  do Gaussian elimination on the source matrix, applying
                //  the row operations to the result matrix at the same time.
                //  When we're done, the source contains the identity and the
                //  result is the inverse.

                for (i = 0; i < 4; i++) R[i] = i;	// Initialize row table

                for (i = 0; i < 4; i++) // Initialize result
                    for (j = 0; j < 4; j++)
                    {
                        Result[i, j] = 0f;
                        T[i, j] = this[i, j];
                    }

                for (i = 0; i < 4; i++) Result[i, i] = 1f;

                // This is the forward elimination part
                for (r = 0, c = 0; c < 4 && r < 4; r++, c++)   // For each row ...
                {
                    // Find a pivot row and swap rows
                    Pivot(ref T, c, R, r, 4);

                    // If the diagonal entry is non-zero then use it to eliminate entries in
                    //this column in lower rows.

                    if (Math.Abs(T[R[r], c]) > 1e-6)
                    {
                        int row = R[r];
                        float div = T[row, c];

                        for (j = c + 1; j < 4; j++)
                            T[row, j] /= div;
                        for (j = 0; j < 4; j++)
                            Result[row, j] /= div;
                        T[row, c] = 1f;
                        for (j = r + 1; j < 4; j++)
                        {
                            float mult = T[R[j], c];

                            if (Math.Abs(mult) > 0)
                            {
                                for (k = c + 1; k < 4; k++)
                                    T[R[j], k] -= mult * T[row, k];
                                T[R[j], c] = 0f;
                                for (k = 0; k < 4; k++)
                                    Result[R[j], k] -= mult * Result[row, k];
                            }
                        }
                    }
                    else
                    {
                        throw (new float4x4Exception("attempt to invert singular matrix"));
                    }
                }

                // Now we do back substitution
                for (r = 3, c = 3; r >= 0; r--, c--)
                {
                    for (j = r - 1; j >= 0; j--)
                    {
                        float mult = T[R[j], c];
                        if (Math.Abs(mult) > 0f)
                        {
                            for (k = 0; k < 4; k++)
                                Result[R[j], k] -= mult * Result[R[r], k];
                        }
                    }
                }
                // Reorder the rows
                for (r = 0; r < 4; r++)
                    for (c = 0; c < 4; c++)
                        T[r, c] = Result[R[r], c];

                return T;
            }
        }



        public float4x4 Adjoint
        {
            get
            {
                float4x4 m = new float4x4(
                    float4.Crs4(this[3], this[2], this[1]),
                    float4.Crs4(this[0], this[2], this[3]),
                    float4.Crs4(this[3], this[1], this[0]),
                    float4.Crs4(this[0], this[1], this[2]));

                return m.Transpose;
            }
        }


        public float4x4 Transpose
        {
            get { return new float4x4(M11, M21, M31, M41, M12, M22, M32, M42, M13, M23, M33, M43, M14, M24, M34, M44); }
        }

        public static float4x4 Identity
        {
            get { return new float4x4(1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1); }
        }

        public static float4x4 Zero
        {
            get { return new float4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0); }
        }

        private static double DegreesToRadians = Math.PI / 180;

        public static float4x4 RotateX(float angle)
        {
            double theta = DegreesToRadians * angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new float4x4(
                1, 0, 0, 0,
                0, c, s, 0,
                0, -s, c, 0,
                0, 0, 0, 1
                );
        }

        public static float4x4 RotateY(float angle)
        {
            double theta = DegreesToRadians * angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new float4x4(
                c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1
                );
        }

        public static float4x4 RotateZ(float theta)
        {
            //			double theta = DegreesToRadians*angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new float4x4(
                c, s, 0, 0,
                -s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
        }

        public static float4x4 Translate(float tx, float ty, float tz)
        {
            return new float4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                tx, ty, tz, 1
                );
        }

        public static float4x4 Translate(ref float4 t)
        {
            return new float4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                t.x, t.y, t.z, 1
                );
        }

        public static float4x4 Scale(float sx, float sy, float sz)
        {
            return new float4x4(
                sx, 0, 0, 0,
                0, sy, 0, 0,
                0, 0, sz, 0,
                0, 0, 0, 1
                );
        }

        public static float4x4 Scale(ref float4 s)
        {
            return new float4x4(
                s.x, 0, 0, 0,
                0, s.y, 0, 0,
                0, 0, s.z, 0,
                0, 0, 0, 1
                );
        }

        public static float4x4 ViewOld(ref float4 from, ref float4 at, ref float4 world_up, float roll)
        {
            float4 view_dir = (at - from); view_dir.Unit();
            float4 right = (world_up ^ view_dir);
            float4 up = (view_dir ^ right);

            right.Unit();
            up.Unit();

            float4x4 view = new float4x4(
                right[0], right[1], right[2], 0,
                up[0], up[1], up[2], 0,
                view_dir[0], view_dir[1], view_dir[2], 0,
                -(right * (from - new float4(0, 0, 0, 1))),
                -(up * (from - new float4(0, 0, 0, 1))),
                -(view_dir * (from - new float4(0, 0, 0, 1))), 1);

            // Set roll
            if (roll != 0f)
            {
                view = float4x4.RotateZ(-roll) * view;
            }

            return view;
        }

        public static float4x4 View(ref float4 from, ref float4 at, ref float4 world_up, float roll)
        {
            //			float4 view_dir = (at - from);  view_dir.Unit();
            //			float4 right =(world_up^view_dir);
            //			float4 up = (view_dir^right);
            float4 view_dir = (at - from); view_dir.Unit();
            float4 right = (view_dir ^ world_up); right.Unit();
            float4 up = (right ^ view_dir);

            //			up.Unit();
            /*
                    float4x4 view = new float4x4(			
                        right[0],		right[1],		right[2],		0,
                        up[0],			up[1],			up[2],			0,
                        view_dir[0],	view_dir[1],	view_dir[2],	0,
                        -(right*(from-float4.set(0, 0, 0, 1))),
                        -(up*(from-float4.set(0, 0, 0, 1))),
                        -(view_dir*(from-float4.set(0, 0, 0, 1))), 1);
        */
            float4x4 view = new float4x4(
                right[0], up[0], view_dir[0], 0,
                right[1], up[1], view_dir[1], 0,
                right[2], up[2], view_dir[2], 0,
                -(right * from),
                -(up * from),
                -(view_dir * from), 1);

            // Set roll
            if (roll != 0f)
            {
                view = float4x4.RotateZ(-roll) * view;
            }

            return view;
        }


        public static float4x4 PerspectiveOld(float fov, float Zn, float Zf)
        {
            float c = (float)Math.Cos(0.5 * fov);
            float s = (float)Math.Sin(0.5 * fov);
            float Q = s / (1 - Zn / Zf);

            return new float4x4(
                c, 0, 0, 0,
                0, c, 0, 0,
                0, 0, Q, s,
                0, 0, -Q * Zn, 0
                );
        }




        public static float4x4 Perspective(float fov, float Zn, float Zf)
        {
            double theta = 0.5 * DegreesToRadians * fov;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);
            float Q = s / (1 - Zn / Zf);

            return new float4x4(
                c, 0, 0, 0,
                0, c, 0, 0,
                0, 0, Q, s,
                0, 0, -Q * Zn, 0
                );
        }

        public static float4x4 Perspective(float fov, float aspect, float hither, float yon)
        {
            float s = 1 / (float)Math.Tan(0.5 * DegreesToRadians * fov);

            return new float4x4(
                s / aspect, 0, 0, 0,
                0, s, 0, 0,
                0, 0, yon / (yon - hither), 1,
                0, 0, hither * yon / (hither - yon), 0
                );
        }


        public static float4x4 Ortho(float left, float right, float bottom, float top, float hither, float yon)
        {
            return new float4x4(
                2 / (right - left), 0, 0, 0,
                0, 2 / (top - bottom), 0, 0,
                0, 0, 1 / (yon - hither), 0,
                (left + right) / (left - right), (top + bottom) / (bottom - top), hither / (hither - yon), 1
                );
        }

        public static float4x4 RotateAbout(ref float4 P, ref float4 V, float angle)
        {
            double theta = DegreesToRadians * angle;
            float s = (float)Math.Sin(0.5 * theta) / V.Length;
            quaterion Q = new quaterion(s * V.x, s * V.y, s * V.z, (float)Math.Cos(0.5 * angle));
            return Q.GetMatrix();
        }

        public static float4x4 AffineFromToV4F(ref float4 F0, ref float4 F1, ref float4 F2, ref float4 F3,
            ref float4 T0, ref float4 T1, ref float4 T2, ref float4 T3)
        {
            float4x4 A = new float4x4(
                F0.x, F0.y, F0.z, F0.w,
                F1.x, F1.y, F1.z, F1.w,
                F2.x, F2.y, F2.z, F2.w,
                F3.x, F3.y, F3.z, F3.w
                );

            float4x4 B = new float4x4(
                T0.x, T0.y, T0.z, T0.w,
                T1.x, T1.y, T1.z, T1.w,
                T2.x, T2.y, T2.z, T2.w,
                T3.x, T3.y, T3.z, T3.w
                );

            return A.Inverse * B;
        }

        public static float4x4 ProjectiveFromToV4F(ref float4 F0, ref float4 F1, ref float4 F2, ref float4 F3, ref float4 F4,
            ref float4 T0, ref float4 T1, ref float4 T2, ref float4 T3, ref float4 T4)
        {
            float4x4 P = new float4x4(
                F1.x, F1.y, F1.z, F1.w,
                F2.x, F2.y, F2.z, F2.w,
                F3.x, F3.y, F3.z, F3.w,
                F4.x, F4.y, F4.z, F4.w
                );

            float4x4 Q = new float4x4(
                T1.x, T1.y, T1.z, T1.w,
                T2.x, T2.y, T2.z, T2.w,
                T3.x, T3.y, T3.z, T3.w,
                T4.x, T4.y, T4.z, T4.w
                );

            return float4x4.Zero;
        }

        public override string ToString()
        {
            return "{" + this[0].ToString() + "," + this[1].ToString() + "," + this[2].ToString() + "," + this[3].ToString() + "}";
        }


    }
}
