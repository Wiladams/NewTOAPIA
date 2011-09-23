
namespace NewTOAPIA.Graphics
{
    using System;

    [Serializable]
    public struct ColorRGBA
    {
        private float r, g, b, a;

        #region Constructors
        public ColorRGBA(RGBAb pixel)
        {
            r = (float)pixel.red / 255f;
            g = (float)pixel.green / 255f;
            b = (float)pixel.blue / 255f;
            a = (float)pixel.alpha/255;
        }

        public ColorRGBA(Colorref colorref)
        {
            r = (float)colorref.Red / 255f;
            g = (float)colorref.Green / 255f;
            b = (float)colorref.Blue / 255f;
            a = 1.0f;
        }

        public ColorRGBA(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = 1.0f;
        }

        public ColorRGBA(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }

        public ColorRGBA(float4 colors)
        {
            r = colors.x;
            g = colors.y;
            b = colors.z;
            a = colors.w;
        }

        public ColorRGBA(float[] floats)
        {
            // Four cases to deal with
            // length == 1, grayscale
            // length == 2, luma + alpha
            // length == 3, rgb
            // length == 4, rgba

            switch (floats.Length)
            {
                // Luminance
                case 1:
                    r = floats[0];
                    g = floats[0];
                    b = floats[0];
                    a = 1.0f;
                    break;

                // Luminance + alpha
                case 2:
                    r = floats[0];
                    g = floats[0];
                    b = floats[0];
                    a = floats[1];
                    break;

                // RGB
                case 3:
                    r = floats[0];
                    g = floats[1];
                    b = floats[2];
                    a = 1.0f;
                    break;

                // RGBA
                case 4:
                    r = floats[0];
                    g = floats[1];
                    b = floats[2];
                    a = floats[3];
                    break;

                default:
                    throw new ArgumentOutOfRangeException("length");
            }
        }
        #endregion

        #region Properties
        public float R
        {
            get { return r; }
            set { r = value; }
        }

        public float G
        {
            get { return g; }
            set { g = value; }
        }

        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public float A
        {
            get { return a; }
            set { a = value; }
        }

        public uint RGB
        {
            get
            {
                byte rb = (byte)(R * 0xff + 0.5);
                byte gb = (byte)(G * 0xff + 0.5);
                byte bb = (byte)(B * 0xff + 0.5);

                uint color = (((rb | ((uint)gb << 8)) | (((uint)bb) << 16)));

                return color;
            }
        }

        #endregion

        #region Methods
        public void Set(float r, float g, float b)
        {
            Set(r, g, b, 1.0f);
        }

        public void Set(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        #endregion

        #region Operator Overloading
        //public static explicit operator Color(ColorRGBA aPixel)
        //{
        //    byte red = (byte)((aPixel.R * 0xff) + 0.5);
        //    byte green = (byte)((aPixel.G * 0xff) + 0.5);
        //    byte blue = (byte)((aPixel.B * 0xff) + 0.5);
        //    byte alpha = (byte)((aPixel.A * 0xff) + 0.5);
        //    Color newColor = Color.FromArgb(alpha, red, green, blue);

        //    return newColor;
        //}

        //public static implicit operator ColorRGBA(Color aColor)
        //{
        //    float red = aColor.R * 0xff;
        //    float green = aColor.G * 0xff;
        //    float blue = aColor.B * 0xff;
        //    float alpha = aColor.A * 0xff;

        //    return new ColorRGBA(red, green, blue, alpha);
        //}


        public static explicit operator float[](ColorRGBA aColor)
        {
            return new float[] { aColor.r, aColor.g, aColor.b, aColor.a };
        }

        public static implicit operator Colorref(ColorRGBA color)
        {
            return new Colorref(color.RGB);
        }

        public static explicit operator RGBAb(ColorRGBA color)
        {
            return new RGBAb(color);
        }
        #endregion

        public override string ToString()
        {
            return string.Format("r={0}, g={1}, b={2}, a={3}", r, g, b, a);
        }

        #region Static Functions
        public static ColorRGBA Interpolate(ColorRGBA srcColor, ColorRGBA dstColor, float percentComplete)
        {
            float srcContribution = percentComplete;
            float dstContribution = 1 - srcContribution;

            float red = dstColor.R * dstContribution + srcColor.R * srcContribution;
            float green = dstColor.G * dstContribution + srcColor.G * srcContribution;
            float blue = dstColor.B * dstContribution + srcColor.B * srcContribution;
            float alpha = dstColor.A * dstContribution + srcColor.A * srcContribution;

            return new ColorRGBA(red, green, blue, alpha);
        }
        #endregion

        #region Static Colors
        public static ColorRGBA Empty = new ColorRGBA();

        public static ColorRGBA Invisible = new ColorRGBA(0, 0, 0, 0);

        public static ColorRGBA Black = new ColorRGBA(0, 0, 0, 255);
        public static ColorRGBA White = new ColorRGBA(1.0f, 1.0f, 1.0f, 1.0f);

        public static ColorRGBA Red = new ColorRGBA(255, 0, 0, 255);
        public static ColorRGBA Green = new ColorRGBA(0, 255, 0, 255);
        public static ColorRGBA Blue = new ColorRGBA(0, 0, 255, 255);
        public static ColorRGBA Magenta = new ColorRGBA(1.0f, 0, 1.0f, 1.0f);
        public static ColorRGBA Cyan = new ColorRGBA(0, 1.0f, 1.0f, 1.0f);
        public static ColorRGBA Yellow = new ColorRGBA(1.0f, 1.0f, 0.0f, 1.0f);

        public static ColorRGBA MediumRed = new ColorRGBA(0.5f, 0.0f, 0.0f, 1.0f);
        public static ColorRGBA MediumGreen = new ColorRGBA(0.0f, 0.5f, 0, 1.0f);
        public static ColorRGBA MediumBlue = new ColorRGBA(0, 0, 0.5f, 1.0f);
        public static ColorRGBA MediumCyan = new ColorRGBA(0, 0.5f, 0.5f, 1.0f);
        public static ColorRGBA MediumYellow = new ColorRGBA(0.5f, 0.5f, 0.0f, 1.0f);
        public static ColorRGBA MediumMagenta = new ColorRGBA(0.5f, 0, 0.5f, 1.0f);
        #endregion
    }
}
