using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.Graphics
{
    public class PixelRectangleInfo : IPixelInformation
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PixelLayout Layout { get; private set; }
        public PixelComponentType ComponentType { get; private set; }
        public int Dimension { get { return PixelInformation.GetComponentsPerLayout(Layout); } }

        public int BytesPerPixel { get { return PixelInformation.GetBytesPerPixel(Layout, ComponentType); } }
        public PixmapOrientation Orientation { get; set; }
        public int Alignment { get; set; }
        public int BytesPerRow { get; private set; }

        #region Constructors
        public PixelRectangleInfo(int width, int height, PixelLayout layout, PixelComponentType compType, int alignment)
            : this(width, height, layout, compType, PixmapOrientation.TopToBottom, alignment)
        {
        }

        public PixelRectangleInfo(int width, int height, PixelLayout layout, PixelComponentType compType, PixmapOrientation orient, int alignment)
        {
            Width = width;
            Height = height;
            Layout = layout;
            ComponentType = compType;
            Orientation = orient;
            Alignment = alignment;
            BytesPerRow = GetBytesPerRow();
        }

        public PixelRectangleInfo(int width, int height, IPixelInformation pixelInfo, PixmapOrientation orient, int bytesPerRow)
        {
            Width = width;
            Height = height;
            Layout = pixelInfo.Layout;
            ComponentType = pixelInfo.ComponentType;
            Orientation = orient;
            Alignment = 1;
            BytesPerRow = bytesPerRow;
        }

        #endregion

        #region Properties
        public int BitsPerPixel
        {
            get { return PixelInformation.GetBitsPerPixel(GetPixelInformation()); }
        }

        int GetBytesPerRow()
        {
            int rowStride = GetAlignedRowStride(Width, BitsPerPixel, Alignment);
            return rowStride;

        }

        public int GetImageSize()
        {
            int bufferSize = GetAlignedRowStride(Width, PixelInformation.GetBitsPerPixel(GetPixelInformation()), Alignment) * Height;

            return bufferSize;
        }

        public PixelInformation GetPixelInformation()
        {
            PixelInformation info = new PixelInformation(Layout, ComponentType);
            return info;
        }

        public int GetPixelByteOffset(int column, int row)
        {
            int rowOffset = row * BytesPerRow;
            int columnoffset = GetPixelInformation().BytesPerPixel * column;

            return rowOffset + columnoffset;
        }

        #endregion

        #region Static Helpers
        public static int GetAlignedRowStride(int width, int bitsperpixel, int alignment)
        {
            int bytesperpixel = (int)bitsperpixel / 8;
            int stride = (width * bytesperpixel + (alignment - 1)) & ~(alignment - 1);
            return stride;
        }

        public static int GetAlignedRowStride(int width, PixelLayout format, PixelComponentType pType, int alignment)
        {
            return GetAlignedRowStride(width, PixelInformation.GetBitsPerPixel(format, pType), alignment);
        }
        #endregion
    }
}

