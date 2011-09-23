using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Graphics
{
    public struct ColorYUV
    {
        public static readonly ColorYUV Empty = new ColorYUV();
        public const float gMinComposite = -0.436f;
        public const float gMaxComposite = 0.436f;

        #region Fields
        public float y;
        public float u;
        public float v;
        #endregion

        #region Constructor
        public ColorYUV(float y, float u, float v)
        {
            this.y = y;
            this.u = u;
            this.v = v;
        }
        #endregion

        #region Properties
        public float Y
        {
            get { return Y; }
        }

        public float U
        {
            get { return U; }
        }

        public float V
        {
            get { return V; }
        }
        #endregion

        #region Operator Overloads
        /// <summary>
        /// Converts RGB to YUV.
        /// </summary>
        /// <param name="red">Red must be in [0, 255].</param>
        /// <param name="green">Green must be in [0, 255].</param>
        /// <param name="blue">Blue must be in [0, 255].</param>
        public static explicit operator ColorYUV(ColorRGBA color)
        {
            ColorYUV outyuv = new ColorYUV();


            outyuv.y = 0.299f * color.R + 0.587f * color.G + 0.114f * color.B;
            outyuv.u = -0.14713f * color.R - 0.28886f * color.G + 0.436f * color.B;
            outyuv.v = 0.615f * color.R - 0.51499f * color.G - 0.10001f * color.B;

            return outyuv;
        }

        public static bool operator ==(ColorYUV color1, ColorYUV color2)
        {
            return ((color1.Y == color2.Y) && (color1.U == color2.U) && (color1.V == color2.V));
        }

        public static bool operator !=(ColorYUV color1, ColorYUV color2)
        {
            return ((color1.Y != color2.Y) || (color1.U != color2.U) || (color1.V != color2.V));
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType()) 
                return false;

            return (this == (ColorYUV)obj);
        }

        public override int GetHashCode()
        {
            return Y.GetHashCode() ^ U.GetHashCode() ^ V.GetHashCode();
        }

        #endregion
    }
}
