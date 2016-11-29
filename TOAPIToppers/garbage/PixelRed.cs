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
    public interface IPixelRed<T> : IPixel
        where T : struct
    {
        T Red { get; set; }
    }

    #region Redb
    unsafe public struct Redb : IPixelRed<byte>
    {
        public static Redb Empty = new Redb();

        byte data;


        #region Constructors
        public Redb(byte red)
        {
            data = red;
        }

        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / byte.MaxValue, 0, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)(Math.Floor((aColor.R * byte.MaxValue) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(byte); } }
        public PixelLayout Layout { get { return PixelLayout.Red; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        #endregion

        #region Component Access
        public byte Red
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
        public static implicit operator ColorRGBA(Redb aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / byte.MaxValue, 0, 0);
        }

        public static implicit operator Redb(ColorRGBA aPixel)
        {
            return new Redb((byte)Math.Floor((aPixel.R * byte.MaxValue) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorRedb : PixelAccessor<Redb>
    {
        public PixelAccessorRedb(IPixelArray pixelArray)
            : base(pixelArray)
        {
        }

        public PixelAccessorRedb(PixelArray<Redb> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Redb aPixel)
        {
            Redb* pixelPtr = (Redb*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }


        public override Redb RetrievePixel(int x, int y)
        {
            Redb* pixelPtr = (Redb*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Redb this[int index]
        {
            get
            {
                Redb* pixelPtr = (Redb*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Redb* pixelPtr = (Redb*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion
    }
    #endregion

    #region Reds
    unsafe public struct Reds : IPixelRed<short>
    {
        public static Reds Empty = new Reds();

        fixed short data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffff, 0, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (short)(Math.Floor((aColor.R * 0xffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(short); } }
        public PixelLayout Layout { get { return PixelLayout.Red; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Short; } }
        #endregion

        #region Component Access
        public short Red
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

    unsafe public class PixelAccessorReds : PixelAccessor<Reds>
    {
        public PixelAccessorReds(IPixelArray pixelArray)
            : base(pixelArray)
        {
        }

        public PixelAccessorReds(PixelArray<Reds> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Reds aPixel)
        {
            Reds* pixelPtr = (Reds*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }


        public override Reds RetrievePixel(int x, int y)
        {
            Reds* pixelPtr = (Reds*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Reds this[int index]
        {
            get
            {
                Reds* pixelPtr = (Reds*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Reds* pixelPtr = (Reds*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region Redi
    unsafe public struct Redi : IPixelRed<int>
    {
        public static Redi Empty = new Redi();

        fixed int data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffffffff, 0, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (int)(Math.Floor((aColor.R * 0xffffffff) + 0.5));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(int); } }
        public PixelLayout Layout { get { return PixelLayout.Red; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Access
        public int Red
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

    unsafe public class PixelAccessorRedi : PixelAccessor<Redi>
    {

        public PixelAccessorRedi(PixelArray<Redi> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Redi aPixel)
        {
            Redi* pixelPtr = (Redi*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Redi RetrievePixel(int x, int y)
        {
            Redi* pixelPtr = (Redi*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public Redi this[int index]
        {
            get
            {
                Redi* pixelPtr = (Redi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Redi* pixelPtr = (Redi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region Redf
    unsafe public struct Redf : IPixelRed<float>
    {
        public static Redf Empty = new Redf();

        fixed float data[1];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 1, 0, 0);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (float)((aColor.R * 1));
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(float); } }
        public PixelLayout Layout { get { return PixelLayout.Red; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Float; } }
        #endregion

        #region Component Access
        public float Red
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

    unsafe public class PixelAccessorRedf : PixelAccessor<Redf>
    {

        public PixelAccessorRedf(PixelArray<Redf> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, Redf aPixel)
        {
            Redf* pixelPtr = (Redf*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override Redf RetrievePixel(int x, int y)
        {
            Redf* pixelPtr = (Redf*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public Redf this[int index]
        {
            get
            {
                Redf* pixelPtr = (Redf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                Redf* pixelPtr = (Redf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

}
