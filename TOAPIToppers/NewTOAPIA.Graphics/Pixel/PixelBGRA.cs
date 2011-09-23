namespace NewTOAPIA.Graphics
{
    using System;

    #region BGRAb
    public struct BGRAb : IPixel, IEquatable<BGRAb>
    {
        public static BGRAb Empty = new BGRAb();

        public byte blue;
        public byte green;
        public byte red;
        public byte alpha;

        #region Constructors
        public BGRAb(byte gray)
        {
            this.red = gray;
            this.green = gray;
            this.blue = gray;
            this.alpha = byte.MaxValue;
        }

        public BGRAb(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = byte.MaxValue;
        }

        public BGRAb(byte red, byte green, byte blue, byte alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public BGRAb(int red, int green, int blue, int alpha)
        {
            this.red = (byte)red;
            this.green = (byte)green;
            this.blue = (byte)blue;
            this.alpha = (byte)alpha;
        }

        public BGRAb(ColorRGBA aColor)
        {
            red = (byte)Math.Floor((aColor.R * 0xff) + 0.5f);
            green = (byte)Math.Floor((aColor.G * 0xff) + 0.5f);
            blue = (byte)Math.Floor((aColor.B * 0xff) + 0.5f);
            alpha = (byte)Math.Floor((aColor.A * 0xff) + 0.5f);
        }

        #endregion

        #region IPixel
        public int Red { get { return (int)red; } set { red = (byte)value; } }
        public int Green { get { return (int)green; } set { green = (byte)value; } }
        public int Blue { get { return (int)blue; } set { blue = (byte)value; } }
        public int Alpha { get { return (int)alpha; } set { alpha = (byte)value; } }

        public RGBAb ToRGBAb()
        {
            return new RGBAb(this);
        }

        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xff) + 0.5f);
            Alpha = (byte)Math.Floor((aColor.A * 0xff) + 0.5f);
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[4];
            result[0] = blue;
            result[1] = green;
            result[2] = red;
            result[3] = alpha;

            return result;
        }

        public void SetBytes(byte[] bytes)
        {
            SetBytes(bytes, 0);
        }

        public void SetBytes(byte[] bytes, int startIndex)
        {
            blue = bytes[startIndex + 0];
            green = bytes[startIndex + 1];
            red = bytes[startIndex + 2];
            alpha = bytes[startIndex + 3];
        }
        #endregion

        #region IPixelInformation
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgra; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion

        #region IEquatable<BGRAb>
        public bool Equals(BGRAb rhs)
        {
            if ((this.blue != rhs.blue) || (this.green != rhs.green) ||
                (this.red != rhs.red) || (this.alpha != rhs.alpha))
                return false;

            return true;
        }
        #endregion

        #region Operator Overloading
        public static explicit operator ColorRGBA(BGRAb aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / 0xff, (float)aPixel.Green / 0xff, (float)aPixel.Blue / 0xff, (float)aPixel.Alpha / 0xff);
        }

        public static explicit operator BGRAb(ColorRGBA aPixel)
        {
            return new BGRAb((byte)Math.Floor((aPixel.R * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.G * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.B * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.A * 0xff) + 0.5f));
        }
        #endregion
    }
    #endregion
}