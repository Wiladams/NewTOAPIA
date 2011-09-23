namespace NewTOAPIA.Graphics
{
    using System;
    using System.Runtime.InteropServices;

    #region RGB
    public struct RGBAb : IPixel
    {
        public static RGBAb Empty = new RGBAb();

        public byte red;
        public byte green;
        public byte blue;
        public byte alpha;

        #region Constructors
        public RGBAb(ColorRGBA aColor)
        {
            red = (byte)(aColor.R * byte.MaxValue);
            green = (byte)(aColor.G * byte.MaxValue);
            blue = (byte)(aColor.B * byte.MaxValue);
            alpha = (byte)(aColor.A * byte.MaxValue);
        }

        public RGBAb(IPixel pixel)
        {
            red = (byte)pixel.Red;
            green = (byte)pixel.Green;
            blue = (byte)pixel.Blue;
            alpha = (byte)pixel.Alpha;
        }

        public RGBAb(byte red, byte green, byte blue, byte alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public RGBAb(int red, int green, int blue, int alpha)
        {
            this.red = (byte)red;
            this.green = (byte)green;
            this.blue = (byte)blue;
            this.alpha = (byte)alpha;
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)(aColor.R * byte.MaxValue);
            Green = (byte)(aColor.G * byte.MaxValue);
            Blue = (byte)(aColor.B * byte.MaxValue);
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[3];
            result[0] = red;
            result[1] = green;
            result[2] = blue;

            return result;
        }

        public void SetBytes(byte[] bytes)
        {
            SetBytes(bytes, 0);
        }

        public void SetBytes(byte[] bytes, int startIndex)
        {
            red = bytes[startIndex + 0];
            green = bytes[startIndex + 1];
            blue = bytes[startIndex + 2];
        }
        #endregion

        #region IPixelInformation
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Double; } }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion

        public int Red { get { return (int)red; } set { red = (byte)value; } }
        public int Green { get { return (int)green; } set { green = (byte)value; } }
        public int Blue { get { return (int)blue; } set { blue = (byte)value; } }
        public int Alpha { get { return (int)alpha; } set { alpha = (byte)value; } }

        public RGBAb ToRGBAb() { return this; }

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBAb aPixel)
        {
            return aPixel.GetColor();
        }

        //public static explicit operator RGBAb(ColorRGBA aColor)
        //{
        //    RGBb newPixel = new RGBb();
        //    newPixel.SetColor(aColor);
        //    return newPixel;
        //}
        #endregion

    }

    public struct RGBAd : IPixel
    {
        public static RGBAd Empty = new RGBAd();

        public double red;
        public double green;
        public double blue;
        public double alpha;

        #region Constructors
        public RGBAd(double red, double green, double blue, double alpha)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
            this.alpha = alpha;
        }

        public RGBAd(double gray)
        {
            this.red = gray;
            this.green = gray;
            this.blue = gray;
            this.alpha = 1.0;
        }

        public RGBAd(byte[] bytes)
        {
            int startIndex = 0;
            red = BitConverter.ToDouble(bytes, startIndex + 0);
            green = BitConverter.ToDouble(bytes, startIndex + (1 * sizeof(double)));
            blue = BitConverter.ToDouble(bytes, startIndex + (2 * sizeof(double)));
            alpha = BitConverter.ToDouble(bytes, startIndex + (3 * sizeof(double)));


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
            red = (double)aColor.R * double.MaxValue;
            green = (double)aColor.G * double.MaxValue;
            blue = (double)aColor.B * double.MaxValue;
            alpha = (double)aColor.A * double.MaxValue;
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[3 * sizeof(double)];

            Array.Copy(BitConverter.GetBytes(red), 0, result, 0 * sizeof(double), sizeof(double));
            Array.Copy(BitConverter.GetBytes(green), 0, result, 1 * sizeof(double), sizeof(double));
            Array.Copy(BitConverter.GetBytes(blue), 0, result, 2 * sizeof(double), sizeof(double));

            return result;
        }

        public void SetBytes(byte[] bytes)
        {
            SetBytes(bytes, 0);
        }

        public void SetBytes(byte[] bytes, int startIndex)
        {
            red = BitConverter.ToDouble(bytes, startIndex + 0);
            green = BitConverter.ToDouble(bytes, startIndex + (1 * sizeof(double)));
            blue = BitConverter.ToDouble(bytes, startIndex + (2 * sizeof(double)));
        }
        #endregion

        #region IPixelInformation
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Double; } }
        #endregion

        #region Operator Overloading
        public static explicit operator ColorRGBA(RGBAd aPixel)
        {
            return aPixel.GetColor();
        }

        public static explicit operator RGBAd(ColorRGBA aColor)
        {
            RGBAd newPixel = new RGBAd();
            newPixel.SetColor(aColor);
            return newPixel;
        }
        #endregion
    }
    #endregion
}