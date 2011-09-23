
namespace NewTOAPIA.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public struct ColorRGB
    {
        public float r, g, b;

        #region Constructor
                public ColorRGB(Colorref colorref)
        {
            r = (float)colorref.Red / 255f;
            g = (float)colorref.Green / 255f;
            b = (float)colorref.Blue / 255f;
        }

        public ColorRGB(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        #endregion

        #region Type Cast
        public static implicit operator ColorRGB(Colorref aColor)
        {
            return new ColorRGB(aColor);
        }

        public static implicit operator Colorref(ColorRGB aColor)
        {
            byte red = (byte)((aColor.r * 0xff) + 0.5);
            byte green = (byte)((aColor.g * 0xff) + 0.5);
            byte blue = (byte)((aColor.b * 0xff) + 0.5);

            return new Colorref(red, green, blue);
        }
        #endregion

        #region Operator overloads
        public static ColorRGB operator +(ColorRGB c1, ColorRGB c2)
        {
            ColorRGB c3 = new ColorRGB(c1.r + c2.r, c1.g + c2.g, c1.b + c2.b);
            return c3;
        }

        public static ColorRGB operator *(ColorRGB c, float a)
        {
            ColorRGB c2 = new ColorRGB(c.r * a, c.g * a, c.b * a);
            return c2;
        }

        public static ColorRGB operator *(float a, ColorRGB c)
        {
            ColorRGB c2 = new ColorRGB(c.r * a, c.g * a, c.b * a);
            return c2;
        }

        public static ColorRGB operator *(ColorRGB c1, ColorRGB c2)
        {
            ColorRGB c3 = new ColorRGB(c1.r * c2.r, c1.g * c2.g, c1.b * c2.b);
            return c3;
        }

        public static ColorRGB operator /(ColorRGB c, float a)
        {
            ColorRGB c2 = new ColorRGB(c.r / a, c.g / a, c.b / a);
            return c2;
        }
        

        #endregion
    }
}
