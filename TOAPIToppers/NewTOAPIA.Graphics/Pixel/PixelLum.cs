namespace NewTOAPIA.Graphics
{
    using System;

    #region Lumb
    public struct Lumb : IPixel
    {
        public static Lumb Empty = new Lumb();

        public byte data;

        #region Constructors
        public Lumb(byte lum)
        {
            data = lum;
        }

        #endregion

        #region IPixel
        public int Red { get { return (int)data; } set { data = (byte)value; } }
        public int Green { get { return (int)data; } set { data = (byte)value; } }
        public int Blue { get { return (int)data; } set { data = (byte)value; } }
        public int Alpha { get { return (int)data; } set { data = (byte)value; } }

        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA aColor)
        {
            float red = aColor.R * 0.299f;
            float green = aColor.G * 0.587f;
            float blue = aColor.B * 0.114f;
            data = (byte)(Math.Floor((red + green + blue) * (float)0xff));
        }

        public RGBAb ToRGBAb()
        {
            return new RGBAb(this);
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[1];
            result[0] = data;

            return result;
        }

        public void SetBytes(byte[] bytes)
        {
            SetBytes(bytes, 0);
        }

        public void SetBytes(byte[] bytes, int startIndex)
        {
            data = bytes[startIndex + 0];
        }
        #endregion

        #region IPixelInformation
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Luminance; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion

        #region Component Access
        public byte Lum
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        #endregion

        #region Operator Overloading
        public static explicit operator ColorRGBA(Lumb aPixel)
        {
            return new ColorRGBA((float)aPixel.data / 0xff, (float)aPixel.data / 0xff, (float)aPixel.data / 0xff);
        }

        public static implicit operator Lumb(ColorRGBA aPixel)
        {
            float red = aPixel.R * 0.299f;
            float green = aPixel.G * 0.587f;
            float blue = aPixel.B * 0.114f;
            byte lum = (byte)(Math.Floor((red + green + blue) * (float)0xff));

            return new Lumb(lum);
        }
        #endregion

    }


    #endregion
}