using System;
using System.Text;

namespace NewTOAPIA.Graphics
{
    /// <summary>
    /// Converting to RGB is accomplished with the following 
    /// matrix multiplication.
    /// 
    /// |R|       |1.000   0        1.402  |     |Y |
    /// |G|  =    |1.000  -0.34413 -0.71414| *   |Cr|
    /// |B|       |1.000  -1.772    0      |     |Cb|
    /// 
    /// Converting from RGB to YIQ is accomplished with the following 
    /// matrix multiplication.
    /// 
    /// |Y |       | 0.299    0.587   0.144|     |R|
    /// |Cr|  =    |-0.16875 -0.33126 0.5  | *   |G|
    /// |Cb|       | 0.212   -0.528   0.311|     |B|

    /// </summary>

    public interface IColorYCrCb<T> : IColorModel
    {
        T Y { get; set; }
        T Cr { get; set; }
        T Cb { get; set; }
    }

    public struct ColorYCrCbf : IColorYCrCb<float>
    {
        static float3x3 gTransformToRGB;
        static float3x3 gTransformFromRGB;

        #region Fields
        float y;
        float cr;
        float cb;
        #endregion


        #region Constructors
        static ColorYCrCbf()
        {
            gTransformToRGB = new float3x3(
                new float3(1.000f, 0f, 1.402f),
                new float3(1.000f, -0.34413f, -0.71414f),
                new float3(1.000f, -1.772, 0));

            gTransformFromRGB = new float3x3(
                new float3(0.299f, 0.587f, 0.144f),
                new float3(-0.16875f, -0.33126f, 0.5f),
                new float3(0.212f,   -0.528f,   0.311f));
        }

        ColorYCrCbf(float y, float cr, float cb)
        {
            this.y = y;
            this.cr = cr;
            this.cb = cb;
        }
        #endregion

        #region IColorYCrCb
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float Cr
        {
            get { return cr; }
            set { cr = value; }
        }

        public float Cb
        {
            get { return cb; }
            set { cb = value; }
        }
        #endregion

        #region IColorModel
        public ColorRGB GetRGB()
        {
            float3 rgb = gTransformToRGB * new float3(Y, Cr, Cb);
            return new ColorRGB(rgb.x, rgb.y, rgb.z);
        }

        public void SetRGB(ColorRGB aColor)
        {
            float3 ycrcb = gTransformFromRGB * new float3(aColor.r, aColor.g, aColor.b);
            Y = ycrcb.x;
            Cr = ycrcb.y;
            Cb = ycrcb.z;
        }

        #endregion
    }

}
