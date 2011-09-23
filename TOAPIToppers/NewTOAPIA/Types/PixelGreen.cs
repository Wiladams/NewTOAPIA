using System;

namespace NewTOAPIA
{
    /// <summary>
    /// Basic interface for a pixel value.  This template allows for
    /// the creation of pixels of any type.  The constraint is that 'T'
    /// must be a struct type.  This ensures that the type is a non-nullable
    /// value type, and thus can be assigned easily.
    /// </summary>
    /// <typeparam name="T">Is restricted to being a value type.</typeparam>
    public interface IPixelGreen<T> : IPixel
        where T : struct
    {
        T Green { get; set; }
    }

    #region Greenb
    unsafe public struct Greenb : IPixelGreen<byte>
    {
        public static Greenb Empty = new Greenb();

        unsafe fixed byte data[1];
       //byte data;


        #region Constructors
        public Greenb(byte lum)
        {
            //data = lum;

            fixed (byte* dataPtr = data)
            {
                dataPtr[0] = lum;
            }
        }

        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, (float)Green / 0xff, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Green = (byte)(Math.Floor((aColor.G * 0xff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(byte); } }
        public PixelLayout Layout { get { return PixelLayout.Green; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        #endregion

        #region Component Access
        public byte Green
        {
            get
            {
                fixed (byte* dataPtr = data)
                {
                    return dataPtr[0];
                }
            }
            set
            {
                fixed (byte* dataPtr = data)
                {
                    dataPtr[0] = value;
                }
            }
        }
        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(Greenb aPixel)
        {
            return new ColorRGBA(0, (float)aPixel.Green / 0xff, 0);
        }

        public static implicit operator Greenb(ColorRGBA aPixel)
        {
            return new Greenb((byte)Math.Floor((aPixel.G * 0xff) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorGreenb : PixelAccessor<Greenb>
    {

        public PixelAccessorGreenb(PixelArray<Greenb> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Greenb aPixel)
        {
            Greenb* pixelPtr = (Greenb*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override Greenb RetrievePixel(int x, int y)
        {
            Greenb* pixelPtr = (Greenb*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x,y)];
        }

        #region Operator Overloading
        public Greenb this[int index]
        {
            get
            {
                Greenb* pixelPtr = (Greenb*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Greenb* pixelPtr = (Greenb*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region Greens
    unsafe public struct Greens : IPixelGreen<short>
    {
        public static Greens Empty = new Greens();

        fixed short data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, (float)Green / 0xffff, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Green = (short)(Math.Floor((aColor.G * 0xffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(short); } }
        public PixelLayout Layout { get { return PixelLayout.Green; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Short; } }
        #endregion

        #region Component Access
        public short Green
        {
            get
            {
                fixed (short* dataPtr = data)
                {
                    return dataPtr[0];
                }
            }
            set
            {
                fixed (short* dataPtr = data)
                {
                    dataPtr[0] = value;
                }
            }
        }
        #endregion
    }

    unsafe public class PixelAccessorGreens : PixelAccessor<Greens>
    {

        public PixelAccessorGreens(PixelArray<Greens> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Greens aPixel)
        {
                Greens* pixelPtr = (Greens*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override Greens RetrievePixel(int x, int y)
        {
                Greens* pixelPtr = (Greens*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public Greens this[int index]
        {
            get
            {
                Greens* pixelPtr = (Greens*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Greens* pixelPtr = (Greens*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region Greeni
    unsafe public struct Greeni : IPixelGreen<int>
    {
        public static Greeni Empty = new Greeni();

        fixed int data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, (float)Green / 0xffffffff, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Green = (int)(Math.Floor((aColor.G * 0xffffffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(int); } }
        public PixelLayout Layout { get { return PixelLayout.Green; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Access
        public int Green
        {
            get
            {
                fixed (int* dataPtr = data)
                {
                    return dataPtr[0];
                }
            }
            set
            {
                fixed (int* dataPtr = data)
                {
                    dataPtr[0] = value;
                }
            }
        }
        #endregion
    }

    unsafe public class PixelAccessorGreeni : PixelAccessor<Greeni>
    {

        public PixelAccessorGreeni(PixelArray<Greeni> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Greeni aPixel)
        {
                Greeni* pixelPtr = (Greeni*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Greeni RetrievePixel(int x, int y)
        {
                Greeni* pixelPtr = (Greeni*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x,y)];
        }

        #region Operator Overloading
        public Greeni this[int index]
        {
            get
            {
                Greeni* pixelPtr = (Greeni*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Greeni* pixelPtr = (Greeni*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region Greenf
    unsafe public struct Greenf : IPixelGreen<float>
    {
        public static Greenf Empty = new Greenf();

        fixed float data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Green / 1, 0, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Green = (float)((aColor.G * 1));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(float); } }
        public PixelLayout Layout { get { return PixelLayout.Green; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Float; } }
        #endregion

        #region Component Access
        public float Green
        {
            get
            {
                fixed (float* dataPtr = data)
                {
                    return dataPtr[0];
                }
            }
            set
            {
                fixed (float* dataPtr = data)
                {
                    dataPtr[0] = value;
                }
            }
        }

        #endregion
    }

    unsafe public class PixelAccessorGreenf : PixelAccessor<Greenf>
    {

        public PixelAccessorGreenf(PixelArray<Greenf> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Greenf aPixel)
        {
                Greenf* pixelPtr = (Greenf*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Greenf RetrievePixel(int x, int y)
        {
                Greenf* pixelPtr = (Greenf*)Pixels.ToPointer();
                    return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Greenf this[int index]
        {
            get
            {
                Greenf* pixelPtr = (Greenf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Greenf* pixelPtr = (Greenf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

}
