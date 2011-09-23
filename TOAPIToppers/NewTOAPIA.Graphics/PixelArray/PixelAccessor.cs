using System;

namespace NewTOAPIA.Graphics
{
    using System.Runtime.InteropServices;

    public abstract class PixelAccessor : IPixelAccessor, IColorAccessor
    {
        public PixelRectangleInfo PixRectInfo { get; private set; }

        #region Instance Fields
        IntPtr fPixelPointer;
        #endregion

        #region Constructors
        protected PixelAccessor()
        {
        }

        protected PixelAccessor(int width, int height, IPixelInformation pixelInfo, int bytesPerRow, PixmapOrientation orientation,
            IntPtr pixelPointer)
        {
            PixRectInfo = new PixelRectangleInfo(width, height, pixelInfo, orientation, bytesPerRow);
            fPixelPointer = pixelPointer;
        }

        protected PixelAccessor(int width, int height, PixelLayout layout, PixelComponentType pType, PixmapOrientation orientation,
            IntPtr pixelPointer)
        {
            PixRectInfo = new PixelRectangleInfo(width, height, layout, pType, orientation, 1);

            fPixelPointer = pixelPointer;
        }
        #endregion

        #region Properties

        #region IPixelAccessor
        public IntPtr Pixels
        {
            get { return fPixelPointer; }
            protected set { fPixelPointer = value; }
        }

        public virtual IPixel GetPixel(int x, int y)
        {
            IntPtr pixelPtr = GetPixelPointer(x, y);

            byte[] buffer = new byte[PixRectInfo.GetPixelInformation().BytesPerPixel];
            Marshal.Copy(pixelPtr, buffer, 0, buffer.Length);

            Pixel newPixel = new Pixel(PixRectInfo.GetPixelInformation(), buffer);
            return newPixel;
        }

        public virtual void SetPixel(int x, int y, IPixel pel)
        {
            // If the pixel types match, then 
            PixelInformation pelInfo = new PixelInformation(pel.Layout, pel.ComponentType);
            if (PixRectInfo.GetPixelInformation().Equals(pelInfo))
            {
                // Find the byte offset for the pixel
                IntPtr pixelPtr = GetPixelPointer(x, y);

                // copy the color bytes into the array
                Marshal.Copy(pel.GetBytes(), 0, pixelPtr, pel.GetBytes().Length);
                return;
            }

            // If the pixel types don't match, then we have to go through
            // color instead of just copying the pixel directly.
            SetColor(x, y, Pixel.GetColor(pelInfo, pel.GetBytes()));

        }
        #endregion

        #region IColorAccessor
        public virtual ColorRGBA GetColor(int x, int y)
        {
            return new ColorRGBA();
        }

        public virtual void SetColor(int x, int y, ColorRGBA color)
        {
            // Create a pixel for the color
            Pixel pel = new Pixel(PixRectInfo.GetPixelInformation(), Pixel.GetBytesForColor(PixRectInfo.GetPixelInformation(), color));

            SetPixel(x, y, pel);
        }
        #endregion


        #region IPixelArray
        public int Alignment
        {
            get { return PixRectInfo.Alignment; }
        }

        public int BitsPerPixel
        {
            get { return PixRectInfo.BitsPerPixel; }
        }

        public int BytesPerRow
        {
            get { return PixRectInfo.BytesPerRow; }
        }


        public PixelLayout Layout
        {
            get { return PixRectInfo.Layout; }
        }

        public PixelComponentType ComponentType
        {
            get { return PixRectInfo.ComponentType; }
        }

        public PixmapOrientation Orientation
        {
            get { return PixRectInfo.Orientation; }
        }
        #endregion


        public int Width
        {
            get
            {
                return PixRectInfo.Width;
            }
        }

        public int Height
        {
            get
            {
                return PixRectInfo.Height;
            }
        }

        #endregion

        #region Methods
        public IntPtr GetRowPointer(int row)
        {
            int offset = 0;

            if (Orientation == PixmapOrientation.TopToBottom)
            {
                offset = BytesPerRow * row;
            }
            else
            {
                offset = (BytesPerRow * (Height - 1 - row));
            }

            UnmanagedPointer ump = new UnmanagedPointer(Pixels);
            ump += offset;

            return (IntPtr)ump;
        }

        public IntPtr GetPixelPointer(int column, int row)
        {
            IntPtr rowPtr = GetRowPointer(row);
            int columnoffset = PixRectInfo.GetPixelInformation().BytesPerPixel * column;
            UnmanagedPointer ump = new UnmanagedPointer(rowPtr);
            ump += columnoffset;

            return (IntPtr)ump;
        }

        public int CalculateOffset(int x, int y)
        {
            int offset = 0;

            if (Orientation == PixmapOrientation.TopToBottom)
            {
                offset = x + (Width * y);
            }
            else
            {
                offset = x + (Width * (Height - 1 - y));
            }

            return offset;
        }
        #endregion

    }
}
