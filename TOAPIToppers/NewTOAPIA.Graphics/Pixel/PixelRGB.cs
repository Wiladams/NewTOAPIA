namespace NewTOAPIA.Graphics
{
    using System;

    #region RGB
    public struct RGBb : IPixel
    {
        public static RGBb Empty = new RGBb();

        public byte red;
        public byte green;
        public byte blue;

        #region Constructors
        public RGBb(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public RGBb(byte gray)
        {
            this.red = gray;
            this.green = gray;
            this.blue = gray;
        }
        #endregion

        #region IPixel
        public RGBAb ToRGBAb()
        {
            return new RGBAb(this);
        }

        public int Red { get { return (int)red; } set { red = (byte)value; } }
        public int Green { get { return (int)green; } set { green = (byte)value; } }
        public int Blue { get { return (int)blue; } set { blue = (byte)value; } }
        public int Alpha { get { return 255; } set { } }

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


        #region Operator Overloading
        public static explicit operator ColorRGBA(RGBb aPixel)
        {
            return aPixel.GetColor();
        }

        public static explicit operator RGBb(ColorRGBA aColor)
        {
            RGBb newPixel = new RGBb();
            newPixel.SetColor(aColor);
            return newPixel;
        }
        #endregion

    }

    public struct RGBd : IPixel
    {
        public static RGBd Empty = new RGBd();

        public double red;
        public double green;
        public double blue;

        #region Constructors
        public RGBd(double red, double green, double blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public RGBd(double gray)
        {
            this.red = gray;
            this.green = gray;
            this.blue = gray;
        }

        public RGBd(byte[] bytes)
        {
            int startIndex = 0;
            red = BitConverter.ToDouble(bytes, startIndex + 0);
            green = BitConverter.ToDouble(bytes, startIndex + (1 * sizeof(double)));
            blue = BitConverter.ToDouble(bytes, startIndex + (2 * sizeof(double)));

        }
        #endregion

        #region IPixel
        public RGBAb ToRGBAb()
        {
            return new RGBAb(this);
        }

        public int Red { get { return (int)red; } set { red = (double)value; } }
        public int Green { get { return (int)green; } set { green = (double)value; } }
        public int Blue { get { return (int)blue; } set { blue = (double)value; } }
        public int Alpha { get { return 255; } set { } }

        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA aColor)
        {
            red = (double)aColor.R * double.MaxValue;
            green = (double)aColor.G * double.MaxValue;
            blue = (double)aColor.B * double.MaxValue;
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
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Double; } }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion


        #region Operator Overloading
        public static explicit operator ColorRGBA(RGBd aPixel)
        {
            return aPixel.GetColor();
        }

        public static explicit operator RGBd(ColorRGBA aColor)
        {
            RGBd newPixel = new RGBd();
            newPixel.SetColor(aColor);
            return newPixel;
        }
        #endregion

    }
    #endregion
}