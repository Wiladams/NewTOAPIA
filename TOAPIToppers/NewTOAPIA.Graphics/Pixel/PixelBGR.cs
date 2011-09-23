namespace NewTOAPIA.Graphics
{
    using System;

    #region BGRb
    public struct BGRb : IPixel, IEquatable<BGRb>
    {
        public static BGRb Empty = new BGRb();

        byte blue;
        byte green;
        byte red;

        #region Constructor
        public BGRb(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public BGRb(byte[] pixelBytes)
        {
            blue = pixelBytes[0];
            green = pixelBytes[1];
            red = pixelBytes[2];
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
        public int Alpha { get { return (int)255; } set {  } }

        public ColorRGBA GetColor()
        {
            return Pixel.GetColor(Layout, ComponentType, GetBytes());
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)((aColor.R * 0xff) + 0.5f);
            Green = (byte)((aColor.G * 0xff) + 0.5f);
            Blue = (byte)((aColor.B * 0xff) + 0.5f);
        }

        public byte[] GetBytes()
        {
            byte[] result = new byte[3];
            result[0] = blue;
            result[1] = green;
            result[2] = red;

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
        }
        #endregion

        #region IPixelInformation
        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgr; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }
        #endregion

        //#region Component Access
        //public byte Red
        //{
        //    get
        //    {
        //        return red;
        //    }
        //    set
        //    {
        //        red = value;
        //    }
        //}

        //public byte Green
        //{
        //    get
        //    {
        //        return green;
        //    }
        //    set
        //    {
        //        green = value;
        //    }
        //}

        //public byte Blue
        //{
        //    get
        //    {
        //        return blue;
        //    }
        //    set
        //    {
        //        blue = value;
        //    }
        //}
        //#endregion

        #region IEquatable<BGRAb>
        public bool Equals(BGRb rhs)
        {
            if ((this.blue != rhs.blue) || (this.green != rhs.green) ||
                (this.red != rhs.red))
                return false;

            return true;
        }
        #endregion

        public static bool operator ==(BGRb p1, BGRb p2)
        {
            return (p1.red == p2.red && p1.green == p2.green && p1.blue == p2.blue);
        }

        public static bool operator !=(BGRb p1, BGRb p2)
        {
            return (p1.red != p2.red || p1.green != p2.green || p1.blue != p2.blue);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            return (this == (BGRb)obj);
        }

        public override int GetHashCode()
        {
            return red.GetHashCode() ^ green.GetHashCode() ^ blue.GetHashCode();
        }
    }

    #endregion
}