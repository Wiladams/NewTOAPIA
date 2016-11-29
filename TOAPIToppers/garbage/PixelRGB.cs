using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA
{
    /// <summary>
    /// Basic interface for a pixel value.  This template allows for
    /// the creation of pixels of any type.  The constraint is that 'T'
    /// must be a struct type.  This ensures that the type is a non-nullable
    /// value type, and thus can be assigned easily.
    /// </summary>
    /// <typeparam name="T">Is restricted to being a value type.</typeparam>
    public interface IPixelRGB<T> : IPixel
        where T : struct
    {
        T Red { get; set; }
        T Green { get; set; }
        T Blue { get; set; }
    }

    #region RGBb
    public struct RGBb : IPixelRGB<byte>
    {
        public static RGBb Empty = new RGBb();

        //unsafe fixed byte data[3];
        byte red;
        byte green;
        byte blue;

        #region Constructors
        public RGBb(byte red, byte green, byte blue)
        {
            this.red = red;
            this.green = green;
            this.blue = blue;

            //fixed (byte* dataPtr = data)
            //{
            //    dataPtr[0] = red;
            //    dataPtr[1] = green;
            //    dataPtr[2] = blue;
            //}
        }

        public RGBb(byte gray)
        {
            red = gray;
            green = gray;
            blue = gray;
            //fixed (byte* dataPtr = data)
            //{
            //    dataPtr[0] = gray;
            //    dataPtr[1] = gray;
            //    dataPtr[2] = gray;
            //}
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xff, (float)Green / 0xff, (float)Blue / 0xff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xff) + 0.5f);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte; } }
        #endregion

        #region Component Access
        public byte Red
        {
            get
            {
                return red;
                //fixed (byte* dataPtr = data)
                //{
                //    return dataPtr[0];
                //}
            }
            set
            {
                red = value;
                //fixed (byte* dataPtr = data)
                //{
                //    dataPtr[0] = value;
                //}
            }
        }

        public byte Green
        {
            get
            {
                return green;
                //fixed (byte* dataPtr = data)
                //{
                //    return dataPtr[1];
                //}
            }
            set
            {
                green = value;
                //fixed (byte* dataPtr = data)
                //{
                //    dataPtr[1] = value;
                //}
            }
        }

        public byte Blue
        {
            get
            {
                return blue;
                //fixed (byte* dataPtr = data)
                //{
                //    return dataPtr[2];
                //}
            }
            set
            {
                blue = value;
                //fixed (byte* dataPtr = data)
                //{
                //    dataPtr[2] = value;
                //}
            }
        }

        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBb aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / 0xff, (float)aPixel.Green / 0xff, (float)aPixel.Blue / 0xff);
        }

        public static explicit operator RGBb(ColorRGBA aPixel)
        {
            return new RGBb((byte)Math.Floor((aPixel.R * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.G * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.B * 0xff) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorRGBb : PixelAccessor<RGBb>
    {

        public PixelAccessorRGBb(PixelArray<RGBb> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBb aPixel)
        {
                RGBb* pixelPtr = (RGBb*)Pixels.ToPointer();
                    pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBb RetrievePixel(int x, int y)
        {
                RGBb* pixelPtr = (RGBb*)Pixels.ToPointer();
                    return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBb this[int index]
        {
            get
            {
                RGBb* pixelPtr = (RGBb*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBb* pixelPtr = (RGBb*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region RGBs
    unsafe public struct RGBs : IPixelRGB<short>
    {
        public static RGBs Empty = new RGBs();

        fixed short data[3];

        public RGBs(short red, short green, short blue)
        {
            fixed (short* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
            }
        }

         public RGBs(short gray)
        {
            fixed (short* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
            }
        }

        #region IPixel
         public ColorRGBA GetColor()
         {
             return new ColorRGBA((float)Red / 0xffff, (float)Green / 0xffff, (float)Blue / 0xffff);
         }

         public void SetColor(ColorRGBA aColor)
         {
             Red = (byte)Math.Floor((aColor.R * 0xffff) + 0.5f);
             Green = (byte)Math.Floor((aColor.G * 0xffff) + 0.5f);
             Blue = (byte)Math.Floor((aColor.B * 0xffff) + 0.5f);
         }
         #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(short) * 3; } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
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

        public short Green
        {
            get
            {
                fixed (short* dataPtr = data)
                {
                    return dataPtr[1];
                }
            }
            set
            {
                fixed (short* dataPtr = data)
                {
                    dataPtr[1] = value;
                }
            }
        }

        public short Blue
        {
            get
            {
                fixed (short* dataPtr = data)
                {
                    return dataPtr[2];
                }
            }
            set
            {
                fixed (short* dataPtr = data)
                {
                    dataPtr[2] = value;
                }
            }
        }
        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBs aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / 0xffff, (float)aPixel.Green / 0xffff, (float)aPixel.Blue / 0xffff);
        }

        public static explicit operator RGBs(ColorRGBA aPixel)
        {
            return new RGBs((short)Math.Floor((aPixel.R * 0xffff) + 0.5f),
                (short)Math.Floor((aPixel.G * 0xffff) + 0.5f),
                (short)Math.Floor((aPixel.B * 0xffff) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorRGBs : PixelAccessor<RGBs>
    {

        public PixelAccessorRGBs(PixelArray<RGBs> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBs aPixel)
        {
                RGBs* pixelPtr = (RGBs*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBs RetrievePixel(int x, int y)
        {
                RGBs* pixelPtr = (RGBs*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBs this[int index]
        {
            get
            {
                RGBs* pixelPtr = (RGBs*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBs* pixelPtr = (RGBs*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region RGBi
    unsafe public struct RGBi : IPixelRGB<int>
    {
        public static RGBi Empty = new RGBi();

        fixed int data[3];

        #region Constructors
        public RGBi(int red, int green, int blue)
        {
            fixed (int* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
            }
        }

        public RGBi(int gray)
        {
            fixed (int* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
            }
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffffffff, (float)Green / 0xffffffff, (float)Blue / 0xffffffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xffffffff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xffffffff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xffffffff) + 0.5f);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(int) * 3; } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
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

        public int Green
        {
            get
            {
                fixed (int* dataPtr = data)
                {
                    return dataPtr[1];
                }
            }
            set
            {
                fixed (int* dataPtr = data)
                {
                    dataPtr[1] = value;
                }
            }
        }

        public int Blue
        {
            get
            {
                fixed (int* dataPtr = data)
                {
                    return dataPtr[2];
                }
            }
            set
            {
                fixed (int* dataPtr = data)
                {
                    dataPtr[2] = value;
                }
            }
        }
        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBi aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / 0xffffffff, (float)aPixel.Green / 0xffffffff, (float)aPixel.Blue / 0xffffffff);
        }

        public static explicit operator RGBi(ColorRGBA aPixel)
        {
            return new RGBi((int)Math.Floor((aPixel.R * 0xffffffff) + 0.5f),
                (int)Math.Floor((aPixel.G * 0xffffffff) + 0.5f),
                (int)Math.Floor((aPixel.B * 0xffffffff) + 0.5f));
        }
        #endregion
    }

    unsafe public class PixelAccessorRGBi : PixelAccessor<RGBi>
    {

        public PixelAccessorRGBi(PixelArray<RGBi> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBi aPixel)
        {
                RGBi* pixelPtr = (RGBi*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBi RetrievePixel(int x, int y)
        {
                RGBi* pixelPtr = (RGBi*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public RGBi this[int index]
        {
            get
            {
                RGBi* pixelPtr = (RGBi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBi* pixelPtr = (RGBi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region RGBf
    unsafe public struct RGBf : IPixelRGB<float>
    {
        public static RGBf Empty = new RGBf();

        fixed float data[3];

        #region Constructors
        public RGBf(float red, float green, float blue)
        {
            fixed (float* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
            }
        }

        public RGBf(float gray)
        {
            fixed (float* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
            }
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / float.MaxValue, (float)Green / float.MaxValue, (float)Blue / float.MaxValue);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (float)aColor.R * float.MaxValue;
            Green = (float)aColor.G * float.MaxValue;
            Blue = (float)aColor.B * float.MaxValue;
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(float) * 3; } }
        public PixelLayout Layout { get { return PixelLayout.Rgb; } }
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

        public float Green
        {
            get
            {
                fixed (float* dataPtr = data)
                {
                    return dataPtr[1];
                }
            }
            set
            {
                fixed (float* dataPtr = data)
                {
                    dataPtr[1] = value;
                }
            }
        }

        public float Blue
        {
            get
            {
                fixed (float* dataPtr = data)
                {
                    return dataPtr[2];
                }
            }
            set
            {
                fixed (float* dataPtr = data)
                {
                    dataPtr[2] = value;
                }
            }
        }
        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBf aPixel)
        {
            return aPixel.GetColor();
        }

        public static explicit operator RGBf(ColorRGBA aColor)
        {
            RGBf newPixel = new RGBf();
            newPixel.SetColor(aColor);
            return newPixel;
        }
        #endregion

    }

    unsafe public class PixelAccessorRGBf : PixelAccessor<RGBf>
    {

        public PixelAccessorRGBf(PixelArray<RGBf> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBf aPixel)
        {
                RGBf* pixelPtr = (RGBf*)Pixels.ToPointer();
                    pixelPtr[CalculateOffset(x,y)] = aPixel;
        }


        public override RGBf RetrievePixel(int x, int y)
        {
                RGBf* pixelPtr = (RGBf*)Pixels.ToPointer();
                    return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBf this[int index]
        {
            get
            {
                RGBf* pixelPtr = (RGBf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBf* pixelPtr = (RGBf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region RGBd

    unsafe public class PixelAccessorRGBd : PixelAccessor<RGBd>
    {

        public PixelAccessorRGBd(PixelArray<RGBd> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBd aPixel)
        {
            RGBd* pixelPtr = (RGBd*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }


        public override RGBd RetrievePixel(int x, int y)
        {
            RGBd* pixelPtr = (RGBd*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBd this[int index]
        {
            get
            {
                RGBd* pixelPtr = (RGBd*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBd* pixelPtr = (RGBd*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

}