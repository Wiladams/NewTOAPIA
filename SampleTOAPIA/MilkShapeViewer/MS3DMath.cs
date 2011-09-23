using System;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace MS3D
{
    public class MS3DMath
    {
        public const double	Q_PI = 3.14159265358979323846;

        public static void ClearBounds (ref float3 mins, ref float3 maxs)
        {
            mins[0] = mins[1] = mins[2] = 99999;
            maxs[0] = maxs[1] = maxs[2] = -99999;
        }

        public static void AddPointToBounds (float3 v, ref float3 mins, ref float3 maxs)
        {
            int i;
            float val;

            for (i = 0; i < 3; i++)
            {
                val = v[i];
                if (val < mins[i])
                    mins[i] = val;
                if (val > maxs[i])
                    maxs[i] = val;
            }
        }

        public static void AngleMatrix (float3 angles, ref float4x4 matrix)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;

            angle = angles[2];
            sy = (float)Math.Sin(angle);
            cy = (float)Math.Cos(angle);
            angle = angles[1];
            sp = (float)Math.Sin(angle);
            cp = (float)Math.Cos(angle);
            angle = angles[0];
            sr = (float)Math.Sin(angle);
            cr = (float)Math.Cos(angle);

            // matrix = (Z * Y) * X
            //matrix[0,0] = cp * cy;
            //matrix[1][0] = cp * sy;
            //matrix[2][0] = -sp;
            //matrix[0][1] = sr * sp * cy + cr * -sy;
            //matrix[1][1] = sr * sp * sy + cr * cy;
            //matrix[2][1] = sr * cp;
            //matrix[0][2] = (cr * sp * cy + -sr * -sy);
            //matrix[1][2] = (cr * sp * sy + -sr * cy);
            //matrix[2][2] = cr * cp;
            //matrix[0][3] = 0.0;
            //matrix[1][3] = 0.0;
            //matrix[2][3] = 0.0;

        }

        public static void R_ConcatTransforms (float4x4 in1, float4x4 in2, float4x4 rout)
        {
        }

        public static void VectorIRotate(float3 in1, float4x4 in2, ref float3 vout)
        {
            vout[0] = in1[0] * in2[0][0] + in1[1] * in2[1][0] + in1[2] * in2[2][0];
            vout[1] = in1[0] * in2[0][1] + in1[1] * in2[1][1] + in1[2] * in2[2][1];
            vout[2] = in1[0] * in2[0][2] + in1[1] * in2[1][2] + in1[2] * in2[2][2];
        }

        public static void VectorRotate(float3 in1, float4x4 in2, ref float3 vout)
        {
            
            //vout[0] = in1 * in2[0];// DotProduct(in1, in2[0]);
            //vout[1] = in1 * in2[1];// DotProduct(in1, in2[1]);
            //vout[2] = in1 * in2[2];// DotProduct(in1, in2[2]);
        }

        public static void VectorTransform(float3 in1, float4x4 in2, ref float3 vout)
        {
            //vout[0] = in1 * in2[0] + in2[0][3];
            //vout[1] = in1 * in2[1] + in2[1][3];
            //vout[2] = in1 * in2[2] + in2[2][3];
        }

        public static void VectorITransform(float3 in1, float4x4 in2, ref float3 vout)
        {
            float3 tmp = new float3();
            tmp[0] = in1[0] - in2[0][3];
            tmp[1] = in1[1] - in2[1][3];
            tmp[2] = in1[2] - in2[2][3];
            VectorIRotate(tmp, in2, ref vout);
        }

        //void AngleQuaternion( const vec3_t angles, vec4_t quaternion );
        //void QuaternionMatrix( const vec4_t quaternion, float (*matrix)[4] );
        //void QuaternionSlerp( const vec4_t p, vec4_t q, float t, vec4_t qt );
    }
}
