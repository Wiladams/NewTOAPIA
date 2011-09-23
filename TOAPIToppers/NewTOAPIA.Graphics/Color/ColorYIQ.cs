

namespace NewTOAPIA.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Converting to RGB is accomplished with the following 
    /// matrix multiplication.
    /// 
    /// |R|       |1.000   0.956   0.620|     |Y|
    /// |G|  =    |1.000  -0.272  -0.647| *   |I|
    /// |B|       |1.000  -1.108   1.705|     |Q|
    /// 
    /// Converting from RGB to YIQ is accomplished with the following 
    /// matrix multiplication.
    /// 
    /// |Y|       |0.299   0.587   0.144|     |R|
    /// |I|  =    |0.596  -0.275  -0.321| *   |G|
    /// |Q|       |0.212  -0.528   0.311|     |B|

    /// </summary>
    /// <returns></returns>

    public interface IColorYIQ<T> : IColorModel
    {
        T Y { get; set; }
        T I { get; set; }
        T Q { get; set; }
    }

    public struct ColorYIQf : IColorYIQ<float>
    {
        static float3x3 gTransformToRGB;
        static float3x3 gTransformFromRGB;

        #region Fields
        float y;
        float i;
        float q;
        #endregion

        #region Constructors
        static ColorYIQf()
        {
            gTransformToRGB = new float3x3(
                new float3(1.000f, 0.956f, 0.620f),
                new float3(1.000f, -0.272f, -0.647),
                new float3(1.000f, -1.108, 1.705));

            gTransformFromRGB = new float3x3(
                new float3(0.299f, 0.587f, 0.144f),
                new float3(0.596f, -0.275f, -0.321f),
                new float3(0.212f, -0.528f, 0.311f));
        }

        ColorYIQf(float y, float i, float q)
        {
            this.y = y;
            this.i = i;
            this.q = q;
        }
        #endregion

        #region IColorYIQ
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        public float I
        {
            get { return i; }
            set { i = value; }
        }

        public float Q
        {
            get { return q; }
            set { q = value; }
        }
        #endregion

        #region IColorModel
        public ColorRGB GetRGB()
        {
            float3 rgb = gTransformToRGB * new float3(Y, I, Q);
            return new ColorRGB(rgb.x, rgb.y, rgb.z);
        }

        public void SetRGB(ColorRGB aColor)
        {
            float3 yiq = gTransformFromRGB * new float3(aColor.r, aColor.g, aColor.b);
            Y = yiq.x;
            i = yiq.y;
            q = yiq.z;
        }

        #endregion
    }

}
