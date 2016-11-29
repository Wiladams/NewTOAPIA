using System;

namespace NewTOAPIA
{
    ////public struct Lum<T> : IPixelLum<T>
    ////        where T : struct
    ////{
    ////    public static Lum<T> Empty = new Lum<T>();

    ////    T data;

    ////    #region IPixel
    ////    public ColorRGBA GetColor()
    ////    {
    ////        return new ColorRGBA((float)Lum / 0xffff, (float)Lum / 0xffff, (float)Lum / 0xffff);
    ////    }

    ////    public void SetColor(ColorRGBA aColor)
    ////    {
    ////        float red = aColor.R * 0.299f;
    ////        float green = aColor.G * 0.587f;
    ////        float blue = aColor.B * 0.114f;
    ////        Lum = (short)(Math.Floor((red + green + blue) * (float)0xffff));
    ////    }
    ////    #endregion

    ////    #region IPixelInformation
    ////    public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
    ////    public PixelLayout Layout { get { return PixelLayout.Luminance; } }
    ////    abstract public PixelComponentType ComponentType;
    ////    #endregion

    ////    #region Component Access
    ////    public T Lum
    ////    {
    ////        get
    ////        {
    ////            return data;
    ////        }
    ////        set
    ////        {
    ////            data = value;
    ////        }
    ////    }
    ////    #endregion

    ////}

    #region Lums
    //public struct Lums : Lum<short>
    //{
    //    #region IPixel
    //    public override ColorRGBA GetColor()
    //    {
    //        return new ColorRGBA((float)Lum / 0xffff, (float)Lum / 0xffff, (float)Lum / 0xffff);
    //    }

    //    public override void SetColor(ColorRGBA aColor)
    //    {
    //        float red = aColor.R * 0.299f;
    //        float green = aColor.G * 0.587f;
    //        float blue = aColor.B * 0.114f;
    //        Lum = (short)(Math.Floor((red + green + blue) * (float)0xffff));
    //    }
    //    #endregion

    //    #region IPixelInformation
    //    public override PixelComponentType ComponentType { get { return PixelComponentType.Short; } }
    //    #endregion

    //}

    //unsafe public class PixelAccessorLums : PixelAccessor<Lums>
    //{

    //    public PixelAccessorLums(PixelArray<Lums> pixelArray)
    //        : base(pixelArray)
    //    {
    //    }

    //    public override void AssignPixel(int x, int y, Lums aPixel)
    //    {
    //        Lums* pixelPtr = (Lums*)Pixels.ToPointer();
    //        pixelPtr[CalculateOffset(x, y)] = aPixel;
    //    }

    //    public override Lums RetrievePixel(int x, int y)
    //    {
    //        Lums* pixelPtr = (Lums*)Pixels.ToPointer();
    //        return pixelPtr[CalculateOffset(x, y)];

    //    }
    
    //    #region Operator Overloading
    //    public Lums this[int index]
    //    {
    //        get
    //        {
    //            Lums* pixelPtr = (Lums*)Pixels.ToPointer();
    //            return pixelPtr[index];
    //        }

    //        set
    //        {
    //            Lums* pixelPtr = (Lums*)Pixels.ToPointer();
    //            pixelPtr[index] = value;
    //        }
    //    }
    //    #endregion
    //}
    #endregion

    #region Lumi
    unsafe public struct Lumi : IPixelLum<int>
    {
        public static Lumi Empty = new Lumi();

        int data;

        #region IPixel
        public ColorRGBA GetColor()
        {
            float lum = Lum / int.MaxValue;
            return new ColorRGBA(lum, lum, lum);
        }

        public void SetColor(ColorRGBA aColor)
        {
            float red = aColor.R * 0.299f;
            float green = aColor.G * 0.587f;
            float blue = aColor.B * 0.114f;
            Lum = (int)(Math.Floor((red + green + blue) * (float)int.MaxValue));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Luminance; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Access
        public int Lum
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
    }

    unsafe public class PixelAccessorLumi : PixelAccessor<Lumi>
    {

        public PixelAccessorLumi(PixelArray<Lumi> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Lumi aPixel)
        {
            Lumi* pixelPtr = (Lumi*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Lumi RetrievePixel(int x, int y)
        {
            Lumi* pixelPtr = (Lumi*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Lumi this[int index]
        {
            get
            {
                Lumi* pixelPtr = (Lumi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Lumi* pixelPtr = (Lumi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region Lumf
    unsafe public struct Lumf : IPixelLum<float>
    {
        public static Lumf Empty = new Lumf();

        float data;

        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Lum / 1, (float)Lum / 1, (float)Lum / 1);
        }

        public void SetColor(ColorRGBA aColor)
        {
            float red = aColor.R * 0.299f;
            float green = aColor.G * 0.587f;
            float blue = aColor.B * 0.114f;
            Lum = (byte)(Math.Floor((red + green + blue) * (float)1));
        }

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Luminance; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Float; } }
        #endregion

        #region Component Access
        public float Lum
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
    }

    unsafe public class PixelAccessorLumf : PixelAccessor<Lumf>
    {

        public PixelAccessorLumf(PixelArray<Lumf> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Lumf aPixel)
        {
            Lumf* pixelPtr = (Lumf*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Lumf RetrievePixel(int x, int y)
        {
            Lumf* pixelPtr = (Lumf*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Lumf this[int index]
        {
            get
            {
                Lumf* pixelPtr = (Lumf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Lumf* pixelPtr = (Lumf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

}