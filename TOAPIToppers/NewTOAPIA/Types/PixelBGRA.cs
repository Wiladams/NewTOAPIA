using System;
using System.Collections.Generic;

namespace NewTOAPIA
{
    /// <summary>
    /// Basic interface for a pixel value.  This template allows for
    /// the creation of pixels of any type.  The constraint is that 'T'
    /// must be a struct type.  This ensures that the type is a non-nullable
    /// value type, and thus can be assigned easily.
    /// </summary>
    /// <typeparam name="T">Is restricted to being a value type.</typeparam>

    #region BGRAs
    unsafe public struct BGRAs : IPixelBGRA<short>
    {
        public static BGRAs Empty = new BGRAs();

        fixed short data[4];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffff, (float)Green / 0xffff, (float)Blue / 0xffff, (float)Alpha / 0xffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xffff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xffff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xffff) + 0.5f);
            Alpha = (byte)Math.Floor((aColor.A * 0xffff) + 0.5f);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgra; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Short; } }
        #endregion

        #region Component Access
        public short Red
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

    unsafe public class PixelAccessorBGRAs : PixelAccessor<BGRAs>
    {
        public PixelAccessorBGRAs(PixelArray<BGRAs> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRAs aPixel)
        {
                BGRAs* pixelPtr = (BGRAs*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override BGRAs RetrievePixel(int x, int y)
        {
                BGRAs* pixelPtr = (BGRAs*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x,y)];

        }

        #region Operator Overloading
        public BGRAs this[int index]
        {
            get
            {
                BGRAs* pixelPtr = (BGRAs*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRAs* pixelPtr = (BGRAs*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region BGRAi
    unsafe public struct BGRAi : IPixelBGRA<int>
    {
        public static BGRAi Empty = new BGRAi();

        fixed int data[4];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 0xffffffff, (float)Green / 0xffffffff, (float)Blue / 0xffffffff, (float)Alpha / 0xffffffff);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = (byte)Math.Floor((aColor.R * 0xffffffff) + 0.5f);
            Green = (byte)Math.Floor((aColor.G * 0xffffffff) + 0.5f);
            Blue = (byte)Math.Floor((aColor.B * 0xffffffff) + 0.5f);
            Alpha = (byte)Math.Floor((aColor.A * 0xffffffff) + 0.5f);
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgra; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Int; } }
        #endregion

        #region Component Accessors
        public int Red
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

    unsafe public class PixelAccessorBGRAi : PixelAccessor<BGRAi>
    {
        public PixelAccessorBGRAi(PixelArray<BGRAi> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRAi aPixel)
        {
                BGRAi* pixelPtr = (BGRAi*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override BGRAi RetrievePixel(int x, int y)
        {
                BGRAi* pixelPtr = (BGRAi*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x,y)];

        }

        #region Operator Overloading
        public BGRAi this[int index]
        {
            get
            {
                BGRAi* pixelPtr = (BGRAi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRAi* pixelPtr = (BGRAi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region BGRAf
    unsafe public struct BGRAf : IPixelBGRA<float>
    {
        public static BGRAf Empty = new BGRAf();

        fixed float data[4];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 1, (float)Green / 1, (float)Blue / 1, Alpha/1);
        }

        public void SetColor(ColorRGBA color)
        {
            Red = color.R * 1;
            Green = color.G * 1;
            Blue = color.B * 1;
            Alpha = color.A * 1;
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgra; } }
        public PixelComponentType ComponentType { get { return PixelComponentType.Float; } }
        #endregion

        #region Component Access
        public float Red
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

    unsafe public class PixelAccessorBGRAf : PixelAccessor<BGRAf>
    {

        public PixelAccessorBGRAf(PixelArray<BGRAf> pixelArray)
            :base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRAf aPixel)
        {
                BGRAf* pixelPtr = (BGRAf*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override BGRAf RetrievePixel(int x, int y)
        {
                BGRAf* pixelPtr = (BGRAf*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];

        }

        #region Operator Overloading
        public BGRAf this[int index]
        {
            get
            {
                BGRAf* pixelPtr = (BGRAf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRAf* pixelPtr = (BGRAf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion
}
