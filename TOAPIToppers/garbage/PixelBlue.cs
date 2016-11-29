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
    public interface IPixelBlue<T> : IPixel
        where T : struct
    {
        T Blue { get; set; }
    }

    #region Blueb
    unsafe public struct Blueb : IPixelBlue<byte>
    {
        public static Blueb Empty = new Blueb();

        unsafe fixed byte data[1];


        #region Constructors
        public Blueb(byte lum)
        {
            fixed (byte* dataPtr = data)
            {
                dataPtr[0] = lum;
            }
        }

        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, 0, (float)Blue / 0xff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Blue = (byte)(Math.Floor((aColor.B * 0xff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(byte); } }
        public PixelLayout Layout { get { return PixelLayout.Blue; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        #endregion

        #region Component Access
        public byte Blue
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
        public static implicit operator ColorRGBA(Blueb aPixel)
        {
            return new ColorRGBA(0, 0, (float)aPixel.Blue / 0xff);
        }

        public static implicit operator Blueb(ColorRGBA aPixel)
        {
            return new Blueb((byte)Math.Floor((aPixel.B * 0xff) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorBlueb : PixelAccessor<Blueb>
    {
        public PixelAccessorBlueb(PixelArray<Blueb> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Blueb aPixel)
        {
            Blueb* pixelPtr = (Blueb*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Blueb RetrievePixel(int x, int y)
        {
            Blueb* pixelPtr = (Blueb*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public Blueb this[int index]
        {
            get
            {
                Blueb* pixelPtr = (Blueb*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Blueb* pixelPtr = (Blueb*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region Blues
    unsafe public struct Blues : IPixelBlue<short>
    {
        public static Blues Empty = new Blues();

        fixed short data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, 0, (float)Blue / 0xffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Blue = (short)(Math.Floor((aColor.B * 0xffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(short); } }
        public PixelLayout Layout { get { return PixelLayout.Blue; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Short; } }
        #endregion

        #region Component Access
        public short Blue
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

    unsafe public class PixelAccessorBlues : PixelAccessor<Blues>
    {
        public PixelAccessorBlues(PixelArray<Blues> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Blues aPixel)
        {
            Blues* pixelPtr = (Blues*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Blues RetrievePixel(int x, int y)
        {
            Blues* pixelPtr = (Blues*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public Blues this[int index]
        {
            get
            {
                Blues* pixelPtr = (Blues*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Blues* pixelPtr = (Blues*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region Bluei
    unsafe public struct Bluei : IPixelBlue<int>
    {
        public static Bluei Empty = new Bluei();

        fixed int data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, 0, (float)Blue / 0xffffffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Blue = (int)(Math.Floor((aColor.B * 0xffffffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(int); } }
        public PixelLayout Layout { get { return PixelLayout.Blue; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Access
        public int Blue
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

    unsafe public class PixelAccessorBluei : PixelAccessor<Bluei>
    {

        public PixelAccessorBluei(PixelArray<Bluei> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Bluei aPixel)
        {
            Bluei* pixelPtr = (Bluei*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Bluei RetrievePixel(int x, int y)
        {
            Bluei* pixelPtr = (Bluei*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public Bluei this[int index]
        {
            get
            {
                Bluei* pixelPtr = (Bluei*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Bluei* pixelPtr = (Bluei*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region Bluef
    unsafe public struct Bluef : IPixelBlue<float>
    {
        public static Bluef Empty = new Bluef();

        fixed float data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(0, 0, (float)Blue / 1);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Blue = (aColor.B * 1);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(float); } }
        public PixelLayout Layout { get { return PixelLayout.Blue; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Float; } }
        #endregion

        #region Component Access
        public float Blue
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

    unsafe public class PixelAccessorBluef : PixelAccessor<Bluef>
    {

        public PixelAccessorBluef(PixelArray<Bluef> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Bluef aPixel)
        {
            Bluef* pixelPtr = (Bluef*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Bluef RetrievePixel(int x, int y)
        {
            Bluef* pixelPtr = (Bluef*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x,y)];
        }

        #region Operator Overloading
        public Bluef this[int index]
        {
            get
            {
                Bluef* pixelPtr = (Bluef*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Bluef* pixelPtr = (Bluef*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

}
