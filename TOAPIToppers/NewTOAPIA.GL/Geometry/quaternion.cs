using System;

namespace NewTOAPIA.GL
{
    public struct quaterion
    {
        public float w, x, y, z;

        #region Constructor
        public quaterion(float x, float y, float z, float w)
        {
            this.x = x; this.y = y; this.z = z; this.w = w;
        }
        #endregion

        public void SetMatrix(ref float4x4 q)
        {
            float xx, xy, xz, xw, yy, yz, yw, zz, zw, ww;

            xx = x * x; xy = x * y; xz = x * z; xw = x * w;
            yy = y * y; yz = y * z; yw = y * w;
            zz = z * z; zw = z * w;
            ww = w * w;

            q.M11 = ww + xx - yy - zz;
            q.M12 = 2.0f * (xy + zw);
            q.M13 = 2.0f * (xz - yw);
            q.M14 = 0.0f;

            q.M21 = 2.0f * (xy - zw);
            q.M22 = ww - xx + yy - zz;
            q.M23 = 2.0f * (yz + xw);
            q.M24 = 0.0f;

            q.M31 = 2.0f * (xz + yw);
            q.M32 = 2.0f * (yz - xw);
            q.M33 = ww - xx - yy + zz;
            q.M34 = 0.0f;

            q.M41 = 0.0f;
            q.M42 = 0.0f;
            q.M43 = 0.0f;
            q.M44 = 1.0f;
        }

        public float4x4 GetMatrix()
        {
            float xx, xy, xz, xw, yy, yz, yw, zz, zw, ww;

            xx = x * x; xy = x * y; xz = x * z; xw = x * w;
            yy = y * y; yz = y * z; yw = y * w;
            zz = z * z; zw = z * w;
            ww = w * w;

            return new float4x4(
                ww + xx - yy - zz, 2.0f * (xy + zw), 2.0f * (xz - yw), 0.0f,
                2.0f * (xy - zw), ww - xx + yy - zz, 2.0f * (yz + xw), 0.0f,
                2.0f * (xz + yw), 2.0f * (yz - xw), ww - xx - yy + zz, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f
                );
        }

        #region Operator Overloading
        public static quaterion operator *(quaterion Q1, quaterion Q2)
        {
            quaterion Qr;

            Qr.w = Q1.w * Q2.w - Q1.x * Q2.x - Q1.y * Q2.y - Q1.z * Q2.z;
            Qr.x = Q1.x * Q2.w + Q1.w * Q2.x - Q1.z * Q2.y + Q1.y * Q2.z;
            Qr.y = Q1.y * Q2.w + Q1.z * Q2.x + Q1.w * Q2.y - Q1.x * Q2.z;
            Qr.z = Q1.z * Q2.w - Q1.y * Q2.x + Q1.x * Q2.y + Q1.w * Q2.z;

            return Qr;

        }
        #endregion

    }
}
