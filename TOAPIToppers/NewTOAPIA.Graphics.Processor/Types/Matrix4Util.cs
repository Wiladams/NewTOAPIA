namespace NewTOAPIA.Graphics.Processor
{
    using System;

    public class Matrix4Util : GPLang
    {
        private static double DegreesToRadians = Math.PI / 180;

        public static mat4 IdentityMatrix()
        {
            return new mat4(1.0f);
        }

        #region Scale
        public static mat4 ScaleMatrix(ref vec3 s)
        {
            mat4 r = new mat4(s.x, 0, 0, 0,
                0, s.y, 0, 0,
                0, 0, s.z, 0,
                0, 0, 0, 1);

            return r;
        }

        public static mat4 Scale(ref vec4 s)
        {
            return new mat4(
                s.x, 0, 0, 0,
                0, s.y, 0, 0,
                0, 0, s.z, 0,
                0, 0, 0, 1
                );
        }

        public static mat4 Scale(float sx, float sy, float sz)
        {
            return new mat4(
                sx, 0, 0, 0,
                0, sy, 0, 0,
                0, 0, sz, 0,
                0, 0, 0, 1
                );
        }
        #endregion

        #region Translate
        public static mat4 Translate(float tx, float ty, float tz)
        {
            return new mat4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                tx, ty, tz, 1
                );
        }

        public static mat4 Translate(ref vec3 t)
        {
            return new mat4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                t.x, t.y, t.z, 1
                );
        }

        public static mat4 Translate(ref vec4 t)
        {
            return new mat4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                t.x, t.y, t.z, 1
                );
        }
        #endregion

        #region Rotate
        
        public static mat4 RotateMatrix(float angle, vec3 axis)
        {
            vec3 n = normalize(axis);
            float theta = radians(angle);
            float c = cos(theta);
            float s = sin(theta);
            mat3 R = new mat3();

            R[0] = n * n.x * (1.0f - c) + new vec3(c, n.z * s, -n.y * s);
            R[1] = n * n.y * (1.0f - c) + new vec3(-n.z * s, c, n.x * s);
            R[2] = n * n.z * (1.0f - c) + new vec3(n.y * s, -n.x * s, c);

            mat4 rValue = new mat4(
                new vec4(R[0], 0.0f),
                new vec4(R[1], 0.0f),
                new vec4(R[2], 0.0f),
                new vec4(0, 0, 0, 1));

            return rValue;
        }

        public static mat4 RotateX(float angle)
        {
            double theta = DegreesToRadians * angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new mat4(
                1, 0, 0, 0,
                0, c, s, 0,
                0, -s, c, 0,
                0, 0, 0, 1
                );
        }

        public static mat4 RotateY(float angle)
        {
            double theta = DegreesToRadians * angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new mat4(
                c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1
                );
        }

        public static mat4 RotateZ(float theta)
        {
            //			double theta = DegreesToRadians*angle;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);

            return new mat4(
                c, s, 0, 0,
                -s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
                );
        }

        #endregion

        #region Perspective
        public static mat4 Perspective(float fov, float Zn, float Zf)
        {
            double theta = 0.5 * DegreesToRadians * fov;
            float c = (float)Math.Cos(theta);
            float s = (float)Math.Sin(theta);
            float Q = s / (1 - Zn / Zf);

            return new mat4(
                c, 0, 0, 0,
                0, c, 0, 0,
                0, 0, Q, s,
                0, 0, -Q * Zn, 0
                );
        }

        public static mat4 Perspective(float fov, float aspect, float hither, float yon)
        {
            float s = 1 / (float)Math.Tan(0.5 * DegreesToRadians * fov);

            return new mat4(
                s / aspect, 0, 0, 0,
                0, s, 0, 0,
                0, 0, yon / (yon - hither), 1,
                0, 0, hither * yon / (hither - yon), 0
                );
        }

        #endregion Perspective

        #region Ortho
        public static mat4 OrthoMatrix(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            vec3 delta = new vec3(right, top, zFar) - new vec3(left, bottom, zNear);
            vec3 sum = new vec3(right, top, zFar) + new vec3(left, bottom, zNear);

            vec3 ratio = sum / delta;
            vec3 twoRatio = 2.0f / delta;

            mat4 rValue = new mat4(
                twoRatio.x, 0, 0, 0,
                0, twoRatio.y, 0, 0,
                0, 0, -twoRatio.z, 0,
                -ratio.x, -ratio.y, -ratio.z, 1);

            return rValue;
        }

        public static mat4 Ortho(float left, float right, float bottom, float top, float hither, float yon)
        {
            return new mat4(
                2 / (right - left), 0, 0, 0,
                0, 2 / (top - bottom), 0, 0,
                0, 0, 1 / (yon - hither), 0,
                (left + right) / (left - right), (top + bottom) / (bottom - top), hither / (hither - yon), 1
                );
        }
        #endregion

        #region Frustum
        public static mat4 FrustumMatrix(float left, float right, float bottom, float top, float zNear, float zFar)
        {
            vec3 delta = new vec3(right, top, zFar) - new vec3(left, bottom, zNear);
            vec3 sum = new vec3(right, top, zFar) + new vec3(left, bottom, zNear);

            vec3 ratio = sum / delta;
            vec3 twoRatio = 2.0f * zNear / delta;

            mat4 rValue = new mat4(
                twoRatio.x, 0, 0, 0,
                0, twoRatio.y, 0, 0,
                ratio.x, ratio.y, -ratio.z, -1,
                0, 0, -zNear*twoRatio.z, 0);

            return rValue;
        }

        #endregion

        //public static mat4 View(ref vec4 from, ref vec4 at, ref vec4 world_up, float roll)
        //{
        //    //			float4 right =(world_up^view_dir);
        //    //			float4 up = (view_dir^right);
        //    vec4 view_dir = (at - from); view_dir.Unit();
        //    vec4 right = (vec4.Cross(view_dir, world_up)); right.Unit();
        //    vec4 up = (vec4.Cross(right, view_dir));

        //    //			up.Unit();
        //    /*
        //            float4x4 view = new float4x4(			
        //                right[0],		right[1],		right[2],		0,
        //                up[0],			up[1],			up[2],			0,
        //                view_dir[0],	view_dir[1],	view_dir[2],	0,
        //                -(right*(from-float4.set(0, 0, 0, 1))),
        //                -(up*(from-float4.set(0, 0, 0, 1))),
        //                -(view_dir*(from-float4.set(0, 0, 0, 1))), 1);
        //*/
        //    mat4 view = new mat4(
        //        right[0], up[0], view_dir[0], 0,
        //        right[1], up[1], view_dir[1], 0,
        //        right[2], up[2], view_dir[2], 0,
        //        -(vec4.Dot(right, from)),
        //        -(vec4.Dot(up, from)),
        //        -(vec4.Dot(view_dir, from)), 1);

        //    // Set roll
        //    if (roll != 0f)
        //    {
        //        view = (mat4)(Matrix4Util.RotateZ(-roll) * view);
        //    }

        //    return view;
        //}



        //public static float4x4 RotateAbout(ref float4 P, ref float4 V, float angle)
        //{
        //    double theta = DegreesToRadians * angle;
        //    float s = (float)Math.Sin(0.5 * theta) / V.Length;
        //    Quaternion Q = new Quaternion(s * V.x, s * V.y, s * V.z, (float)Math.Cos(0.5 * angle));
        //    return Q.GetMatrix();
        //}

        //public static mat4 AffineFromToV4F(ref float4 F0, ref float4 F1, ref float4 F2, ref float4 F3,
        //    ref float4 T0, ref float4 T1, ref float4 T2, ref float4 T3)
        //{
        //    mat4 A = new mat4(
        //        F0.x, F0.y, F0.z, F0.w,
        //        F1.x, F1.y, F1.z, F1.w,
        //        F2.x, F2.y, F2.z, F2.w,
        //        F3.x, F3.y, F3.z, F3.w
        //        );

        //    mat4 B = new mat4(
        //        T0.x, T0.y, T0.z, T0.w,
        //        T1.x, T1.y, T1.z, T1.w,
        //        T2.x, T2.y, T2.z, T2.w,
        //        T3.x, T3.y, T3.z, T3.w
        //        );

        //    return A.Inverse * B;
        //}

        //public static mat4 ProjectiveFromToV4F(ref float4 F0, ref float4 F1, ref float4 F2, ref float4 F3, ref float4 F4,
        //    ref float4 T0, ref float4 T1, ref float4 T2, ref float4 T3, ref float4 T4)
        //{
        //    mat4 P = new mat4(
        //        F1.x, F1.y, F1.z, F1.w,
        //        F2.x, F2.y, F2.z, F2.w,
        //        F3.x, F3.y, F3.z, F3.w,
        //        F4.x, F4.y, F4.z, F4.w
        //        );

        //    mat4 Q = new mat4(
        //        T1.x, T1.y, T1.z, T1.w,
        //        T2.x, T2.y, T2.z, T2.w,
        //        T3.x, T3.y, T3.z, T3.w,
        //        T4.x, T4.y, T4.z, T4.w
        //        );

        //    return mat4.Zero;
        //}
    }
}