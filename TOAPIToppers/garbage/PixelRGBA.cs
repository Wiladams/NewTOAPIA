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
    public interface IRGBAPixel<T> : IPixel
        where T : struct
    {
        T Red { get; set; }
        T Green { get; set; }
        T Blue { get; set; }
        T Alpha { get; set; }
    }

    #region RGBAb
    unsafe public struct RGBAb : IRGBAPixel<byte>
    {
        public static RGBAb Empty = new RGBAb();

        fixed byte data[4];

        #region Constructors
        public RGBAb(byte red, byte green, byte blue, byte alpha)
        {
            fixed (byte* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = alpha;
            }
        }

        public RGBAb(byte red, byte green, byte blue)
        {
            fixed (byte* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = 255;
            }
        }

        public RGBAb(byte gray)
        {
            fixed (byte* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
                dataPtr[3] = 255;
            }
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xff, (float)Green / 0xff, (float)Blue / 0xff, (float)Alpha / 0xff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xff) + 0.5f);
            Alpha = (byte)Math.Floor((aColor.A * 0xff) + 0.5f);
        }
        #endregion
        
        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(byte)*4;} }
        public PixelLayout Layout { get { return PixelLayout.Rgba;} }
        public PixelComponentType ComponentType { get { return PixelComponentType.Byte;} }
        #endregion

        #region Component Access
        public byte Red
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

        public byte Green
        {
            get
            {
                fixed (byte* dataPtr = data)
                {
                    return dataPtr[1];
                }
            }
            set
            {
                fixed (byte* dataPtr = data)
                {
                    dataPtr[1] = value;
                }
            }
        }

        public byte Blue
        {
            get
            {
                fixed (byte* dataPtr = data)
                {
                    return dataPtr[2];
                }
            }
            set
            {
                fixed (byte* dataPtr = data)
                {
                    dataPtr[2] = value;
                }
            }
        }

        public byte Alpha
        {
            get
            {
                fixed (byte* dataPtr = data)
                {
                    return dataPtr[3];
                }
            }
            set
            {
                fixed (byte* dataPtr = data)
                {
                    dataPtr[3] = value;
                }
            }
        }
        #endregion

        #region Operator Overloading
        public static implicit operator ColorRGBA(RGBAb aPixel)
        {
            return new ColorRGBA((float)aPixel.Red / 0xff, (float)aPixel.Green / 0xff, (float)aPixel.Blue / 0xff, (float)aPixel.Alpha / 0xff);
        }

        public static implicit operator RGBAb(RGBAf aPixel)
        {
            return new RGBAb((byte)Math.Floor((aPixel.Red * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.Green * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.Blue * 0xff) + 0.5f),
                (byte)Math.Floor((aPixel.Alpha * 0xff) + 0.5f));
        }
        #endregion

    }

    unsafe public class PixelAccessorRGBAb : PixelAccessor<RGBAb>
    {

        public PixelAccessorRGBAb(PixelArray<RGBAb> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBAb aPixel)
        {
            RGBAb* pixelPtr = (RGBAb*)Pixels.ToPointer();
            pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBAb RetrievePixel(int x, int y)
        {
            RGBAb* pixelPtr = (RGBAb*)Pixels.ToPointer();
            return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBAb this[int index]
        {
            get
            {
                RGBAb* pixelPtr = (RGBAb*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBAb* pixelPtr = (RGBAb*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }
    #endregion

    #region RGBAs
    unsafe public struct RGBAs : IRGBAPixel<short>
    {
        public static RGBAs Empty = new RGBAs();

        fixed short data[4];

        #region Constructors
        public RGBAs(short red, short green, short blue, short alpha)
        {
            fixed (short* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = alpha;
            }
        }

        public RGBAs(short red, short green, short blue)
        {
            fixed (short* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = unchecked((short)0xffff);
            }
        }

        public RGBAs(short gray)
        {
            fixed (short* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
                dataPtr[3] = unchecked((short)0xffff);
            }
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffff, (float)Green / 0xffff, (float)Blue / 0xffff, (float)Alpha / 0xffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (short)Math.Floor((aColor.R * 0xffff) + 0.5f);
            Green = (short)Math.Floor((aColor.G * 0xffff) + 0.5f);
            Blue = (short)Math.Floor((aColor.B * 0xffff) + 0.5f);
            Alpha = (short)Math.Floor((aColor.A * 0xffff) + 0.5f);
        }
        #endregion
        
        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(short) * 4; } }
        public PixelLayout Layout { get { return PixelLayout.Rgba; } }
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

        public short Alpha
        {
            get
            {
                fixed (short* dataPtr = data)
                {
                    return dataPtr[3];
                }
            }
            set
            {
                fixed (short* dataPtr = data)
                {
                    dataPtr[3] = value;
                }
            }
        }
        #endregion
    }

    unsafe public class PixelAccessorRGBAs : PixelAccessor<RGBAs>
    {

        public PixelAccessorRGBAs(PixelArray<RGBAs> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBAs aPixel)
        {
                RGBAs* pixelPtr = (RGBAs*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBAs RetrievePixel(int x, int y)
        {
                RGBAs* pixelPtr = (RGBAs*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBAs this[int index]
        {
            get
            {
                RGBAs* pixelPtr = (RGBAs*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBAs* pixelPtr = (RGBAs*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region RGBAi
    unsafe public struct RGBAi : IRGBAPixel<int>
    {
        public static RGBAi Empty = new RGBAi();

        fixed int data[4];

        #region Constructors
        public RGBAi(int red, int green, int blue, int alpha)
        {
            fixed (int* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = alpha;
            }
        }

        public RGBAi(int red, int green, int blue)
        {
            fixed (int* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = unchecked((int)0xffffffff);
            }
        }

        public RGBAi(int gray)
        {
            fixed (int* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
                dataPtr[3] = unchecked((int)0xffffffff);
            }
        }
        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffffffff, (float)Green / 0xffffffff, (float)Blue / 0xffffffff, (float)Alpha / 0xffffffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (int)Math.Floor((aColor.R * 0xffffffff) + 0.5f);
            Green = (int)Math.Floor((aColor.G * 0xffffffff) + 0.5f);
            Blue = (int)Math.Floor((aColor.B * 0xffffffff) + 0.5f);
            Alpha = (int)Math.Floor((aColor.A * 0xffffffff) + 0.5f);
        }
        #endregion
        
        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(int) * 4; } }
        public PixelLayout Layout { get { return PixelLayout.Rgba; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Accessors
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

        public int Alpha
        {
            get
            {
                fixed (int* dataPtr = data)
                {
                    return dataPtr[3];
                }
            }
            set
            {
                fixed (int* dataPtr = data)
                {
                    dataPtr[3] = value;
                }
            }
        }
        #endregion
    }

    unsafe public class PixelAccessorRGBAi : PixelAccessor<RGBAi>
    {

        public PixelAccessorRGBAi(PixelArray<RGBAi> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBAi aPixel)
        {
                RGBAi* pixelPtr = (RGBAi*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override RGBAi RetrievePixel(int x, int y)
        {
                RGBAi* pixelPtr = (RGBAi*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBAi this[int index]
        {
            get
            {
                RGBAi* pixelPtr = (RGBAi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBAi* pixelPtr = (RGBAi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region RGBAf
    unsafe public struct RGBAf : IRGBAPixel<float>
    {
        public static RGBAf Empty = new RGBAf();

        fixed float data[4];

        #region Constructors
        public RGBAf(float red, float green, float blue, float alpha)
        {
            fixed (float* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = alpha;
            }
        }

        public RGBAf(float red, float green, float blue)
        {
            fixed (float* dataPtr = data)
            {
                dataPtr[0] = red;
                dataPtr[1] = green;
                dataPtr[2] = blue;
                dataPtr[3] = 1;
            }
        }

        public RGBAf(float gray)
        {
            fixed (float* dataPtr = data)
            {
                dataPtr[0] = gray;
                dataPtr[1] = gray;
                dataPtr[2] = gray;
                dataPtr[3] = 1;
            }
        }

        #endregion

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA(Red, Green, Blue, Alpha);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = aColor.R;
            Green = aColor.G;
            Blue = aColor.B;
            Alpha = aColor.A;
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return sizeof(float) * 4; } }
        public PixelLayout Layout { get { return PixelLayout.Rgba; } }
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

        public float Alpha
        {
            get
            {
                fixed (float* dataPtr = data)
                {
                    return dataPtr[3];
                }
            }
            set
            {
                fixed (float* dataPtr = data)
                {
                    dataPtr[3] = value;
                }
            }
        }
        #endregion
    }

    unsafe public class PixelAccessorRGBAf : PixelAccessor<RGBAf>
    {

        public PixelAccessorRGBAf(PixelArray<RGBAf> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, RGBAf aPixel)
        {
                RGBAf* pixelPtr = (RGBAf*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override RGBAf RetrievePixel(int x, int y)
        {
                RGBAf* pixelPtr = (RGBAf*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public RGBAf this[int index]
        {
            get
            {
                RGBAf* pixelPtr = (RGBAf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                RGBAf* pixelPtr = (RGBAf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion
    }

    #endregion
}
