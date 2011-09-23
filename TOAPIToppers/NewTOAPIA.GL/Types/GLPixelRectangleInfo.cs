using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.GL
{

    public class GLPixelRectangleInfo
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public GLPixelFormat PixelFormat { get; private set; }
        public PixelType PixelType { get; private set; }

        public int Alignment { get; set; }

        public GLPixelRectangleInfo(int width, int height, GLPixelFormat format, PixelType pType, int alignment)
        {
            Width = width;
            Height = height;
            PixelFormat = format;
            PixelType = pType;
            Alignment = alignment;
        }

        public int ImageSize
        {
            get
            {
                int bufferSize = GetAlignedRowStride(Width, GetBitsPerPixel(PixelFormat, PixelType), Alignment) * Height;

                return bufferSize;
            }
        }


        public static int GetBitsPerType(PixelType pType)
        {
            switch (pType)
            {
                case PixelType.Byte:
                case PixelType.UnsignedByte:
                case PixelType.UnsignedByte_332:
                case PixelType.UnsignedByte_233_Rev:
                    return 8;

                case PixelType.Short:
                case PixelType.UnsignedShort:
                case PixelType.UnsignedShort_565:
                case PixelType.UnsignedShort_565_Rev:
                case PixelType.UnsignedShort_4444:
                case PixelType.UnsignedShort_4444_Rev:
                case PixelType.UnsignedShort_5551:
                case PixelType.UnsignedShort_1555_Rev:
                    return 16;

                case PixelType.Int:
                case PixelType.UnsignedInt:
                case PixelType.Float:
                case PixelType.UnsignedInt_8888:
                case PixelType.UnsignedInt_8888_Rev:
                case PixelType.UnsignedInt_1010102:
                case PixelType.UnsignedInt_2101010_Rev:
                    return 32;

                case PixelType.Bitmap:
                default:
                    return 0;
            }
        }

        public static int GetComponentsPerFormat(GLPixelFormat format)
        {
            switch (format)
            {
                case GLPixelFormat.ColorIndex:
                case GLPixelFormat.StencilIndex:
                case GLPixelFormat.DepthComponent:
                case GLPixelFormat.Red:
                case GLPixelFormat.Green:
                case GLPixelFormat.Blue:
                case GLPixelFormat.Alpha:
                case GLPixelFormat.Luminance:
                    return 1;

                case GLPixelFormat.LuminanceAlpha:
                    return 2;

                case GLPixelFormat.Rgb:
                case GLPixelFormat.Bgr:
                    return 3;

                case GLPixelFormat.Rgba:
                case GLPixelFormat.Bgra:
                    return 4;
            }

            return 0;
        }

        public static int GetBitsPerPixel(GLPixelFormat format, PixelType pType)
        {
            int bitsPerComponent = GetBitsPerType(pType);
            int componentsPerPixel = GetComponentsPerFormat(format);

            int retValue = bitsPerComponent * componentsPerPixel;

            return retValue;
        }

        public static int GetBytesPerPixel(GLPixelFormat format, PixelType pType)
        {
            return GetBitsPerPixel(format, pType) / 8;
        }

        public static int GetAlignedRowStride(int width, int bitsperpixel, int alignment)
        {
            int bytesperpixel = (int)bitsperpixel / 8;
            int stride = (width * bytesperpixel + (alignment - 1)) & ~(alignment - 1);
            return stride;
        }

        public static int GetAlignedRowStride(int width, GLPixelFormat format, PixelType pType, int alignment)
        {
            return GetAlignedRowStride(width, GetBitsPerPixel(format, pType), alignment);
        }
    }
}

