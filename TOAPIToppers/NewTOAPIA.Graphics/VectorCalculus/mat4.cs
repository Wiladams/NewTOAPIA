namespace NewTOAPIA.Graphics
{
    using System;

    public class mat4 : MatrixSquare
    {
        #region Constructors
        public mat4()
            : base(4)
        {
        }

        public mat4(float id)
            : base(4)
        {
            SetDiagonal(id);
        }

        public mat4(double[,] x)
            :base(x)
        {
        }

        public mat4(IMatrix x)
            : base(4)
        {
            CopyFrom(x);
        }

        public mat4(float a00, float a10, float a20, float a30,
            float a01, float a11, float a21, float a31,
            float a02, float a12, float a22, float a32,
            float a03, float a13, float a23, float a33)
            :base(4)
        {
            this[0, 0] = a00; this[1, 0] = a10; this[2, 0] = a20; this[3, 0] = a30;
            this[0, 1] = a01; this[1, 1] = a11; this[2, 1] = a21; this[3, 1] = a31;
            this[0, 2] = a02; this[1, 2] = a12; this[2, 2] = a22; this[3, 2] = a32;
            this[0, 3] = a03; this[1, 3] = a13; this[2, 3] = a23; this[3, 3] = a33;
        }

        public mat4(double a00, double a10, double a20, double a30,
            double a01, double a11, double a21, double a31,
            double a02, double a12, double a22, double a32,
            double a03, double a13, double a23, double a33)
            :base(4)
        {
            this[0, 0] = a00; this[1, 0] = a10; this[2, 0] = a20; this[3, 0] = a30;
            this[0, 1] = a01; this[1, 1] = a11; this[2, 1] = a21; this[3, 1] = a31;
            this[0, 2] = a02; this[1, 2] = a12; this[2, 2] = a22; this[3, 2] = a32;
            this[0, 3] = a03; this[1, 3] = a13; this[2, 3] = a23; this[3, 3] = a33;
        }



        public mat4(vec4 r0, vec4 r1, vec4 r2, vec4 r3)
            :base(4)
        {
            this[0, 0] = r0.x; this[1, 0] = r0.y; this[2, 0] = r0.z; this[3, 0] = r0.w;
            this[0, 1] = r1.x; this[1, 1] = r1.y; this[2, 1] = r1.z; this[3, 1] = r1.w;
            this[0, 2] = r2.x; this[1, 2] = r2.y; this[2, 2] = r2.z; this[3, 2] = r2.w;
            this[0, 3] = r3.x; this[1, 3] = r3.y; this[2, 3] = r3.z; this[3, 3] = r3.w;
        }

        public mat4(mat3 R, vec3 t)
            :base(4)
        {
            CopyFrom(R);
            this[0, 3] = t.x; this[1, 3] = t.y; this[2, 3] = t.z; this[3, 3] = 1;
         }
        #endregion Constructors

        #region Operator Overloads
        //public static mat4 operator *(mat4 a, mat4 b)
        //{
        //    return new mat4(
        //        a.M11 * b.M11 + a.M12 * b.M21 + a.M13 * b.M31 + a.M14 * b.M41,
        //        a.M11 * b.M12 + a.M12 * b.M22 + a.M13 * b.M32 + a.M14 * b.M42,
        //        a.M11 * b.M13 + a.M12 * b.M23 + a.M13 * b.M33 + a.M14 * b.M43,
        //        a.M11 * b.M14 + a.M12 * b.M24 + a.M13 * b.M34 + a.M14 * b.M44,

        //        a.M21 * b.M11 + a.M22 * b.M21 + a.M23 * b.M31 + a.M24 * b.M41,
        //        a.M21 * b.M12 + a.M22 * b.M22 + a.M23 * b.M32 + a.M24 * b.M42,
        //        a.M21 * b.M13 + a.M22 * b.M23 + a.M23 * b.M33 + a.M24 * b.M43,
        //        a.M21 * b.M14 + a.M22 * b.M24 + a.M23 * b.M34 + a.M24 * b.M44,

        //        a.M31 * b.M11 + a.M32 * b.M21 + a.M33 * b.M31 + a.M34 * b.M41,
        //        a.M31 * b.M12 + a.M32 * b.M22 + a.M33 * b.M32 + a.M34 * b.M42,
        //        a.M31 * b.M13 + a.M32 * b.M23 + a.M33 * b.M33 + a.M34 * b.M43,
        //        a.M31 * b.M14 + a.M32 * b.M24 + a.M33 * b.M34 + a.M34 * b.M44,

        //        a.M41 * b.M11 + a.M42 * b.M21 + a.M43 * b.M31 + a.M44 * b.M41,
        //        a.M41 * b.M12 + a.M42 * b.M22 + a.M43 * b.M32 + a.M44 * b.M42,
        //        a.M41 * b.M13 + a.M42 * b.M23 + a.M43 * b.M33 + a.M44 * b.M43,
        //        a.M41 * b.M14 + a.M42 * b.M24 + a.M43 * b.M34 + a.M44 * b.M44);
        //}

        public static vec4 operator *(mat4 m, vec4 v)
        {
            vec4 r = new vec4(Matrix.Multiply(m, v));

            return r;
        }

        //public static mat4 operator *(float s, mat4 a)
        //{
        //    return new mat4(
        //        s * a.M11, s * a.M12, s * a.M13, s * a.M14,
        //        s * a.M21, s * a.M22, s * a.M23, s * a.M24,
        //        s * a.M31, s * a.M32, s * a.M33, s * a.M34,
        //        s * a.M41, s * a.M42, s * a.M43, s * a.M44);
        //}

        #endregion

    }
}
