using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NewTOAPIA
{

    #region BGRs
    unsafe public struct BGRs : IPixelBGR<short>
    {
        public static BGRs Empty = new BGRs();

        fixed short data[3];

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
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgr; } }
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
        #endregion
    }

    unsafe public class PixelAccessorBGRs : PixelAccessor<BGRs>
    {

        public PixelAccessorBGRs(PixelArray<BGRs> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRs aPixel)
        {
                BGRs* pixelPtr = (BGRs*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override BGRs RetrievePixel(int x, int y)
        {
                BGRs* pixelPtr = (BGRs*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public BGRs this[int index]
        {
            get
            {
                BGRs* pixelPtr = (BGRs*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRs* pixelPtr = (BGRs*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region BGRi
    unsafe public struct BGRi : IPixelBGR<int>
    {
        public static BGRi Empty = new BGRi();

        fixed int data[3];

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
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgr; } }
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

        #endregion
    }

    unsafe public class PixelAccessorBGRi : PixelAccessor<BGRi>
    {

        public PixelAccessorBGRi(PixelArray<BGRi> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRi aPixel)
        {
                BGRi* pixelPtr = (BGRi*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x, y)] = aPixel;
        }

        public override BGRi RetrievePixel(int x, int y)
        {
                BGRi* pixelPtr = (BGRi*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public BGRi this[int index]
        {
            get
            {
                BGRi* pixelPtr = (BGRi*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRi* pixelPtr = (BGRi*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion

    #region BGRf
    unsafe public struct BGRf : IPixelBGR<float>
    {
        public static BGRf Empty = new BGRf();

        fixed float data[3];

        #region IPixel
        public ColorRGBA GetColor()
        {
            return new ColorRGBA((float)Red / 1, (float)Green / 1, (float)Blue / 1);
        }

        public void SetColor(ColorRGBA aColor)
        {
            Red = aColor.R * 1;
            Green = aColor.G * 1;
            Blue = aColor.B * 1;
        }
        #endregion

        #region IPixelInformation
        public int BitsPerPixel { get { return PixelInformation.GetBitsPerPixel(Layout, ComponentType); } }
        public PixelLayout Layout { get { return PixelLayout.Bgr; } }
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
        #endregion
    }

    unsafe public class PixelAccessorBGRf : PixelAccessor<BGRf>
    {

        public PixelAccessorBGRf(PixelArray<BGRf> pixelArray)
            : base(pixelArray)
        {
        }

        public override void AssignPixel(int x, int y, BGRf aPixel)
        {
                BGRf* pixelPtr = (BGRf*)Pixels.ToPointer();
                pixelPtr[CalculateOffset(x,y)] = aPixel;
        }

        public override BGRf RetrievePixel(int x, int y)
        {
                BGRf* pixelPtr = (BGRf*)Pixels.ToPointer();
                return pixelPtr[CalculateOffset(x, y)];
        }

        #region Operator Overloading
        public BGRf this[int index]
        {
            get
            {
                BGRf* pixelPtr = (BGRf*)Pixels.ToPointer();
                return pixelPtr[index];
            }

            set
            {
                BGRf* pixelPtr = (BGRf*)Pixels.ToPointer();
                pixelPtr[index] = value;
            }
        }
        #endregion

    }

    #endregion
}
