using System;
using System.Runtime.InteropServices;
using System.Drawing;

using NewTOAPIA;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Graphics;

    public class GDIDIBSection : GDIPixmap
    {
        #region Internal Fields
        IntPtr m_pixelPointer = IntPtr.Zero;
        int m_BytesPerRow;
        PixelAccessor m_PixelAccessor;

        BITMAPINFO fBitmapInfo;
        #endregion

        #region Constructors
        public GDIDIBSection(int width, int height)
            : this(width, height, BitCount.Bits24)
        {
        }

        public GDIDIBSection(int width, int height, BitCount bitsperpixel)
            : base(width, height, bitsperpixel)
        {
        }
        #endregion

        #region Internal Construction Helper
        protected override IntPtr CreatePixmapHandle(int width, int height, BitCount bitsperpixel)
        {
            //Orientation = orient;
            PixmapOrientation orient = PixmapOrientation.BottomToTop;

            // Create a bitmap compatible with the screen
            fBitmapInfo = new BITMAPINFO();
            fBitmapInfo.Init();


            m_BytesPerRow = GDIPixmap.GetAlignedRowStride(Width, bitsperpixel, Alignment);
            fBitmapInfo.bmiHeader.biWidth = Width;

            if (PixmapOrientation.TopToBottom == orient)
                fBitmapInfo.bmiHeader.biHeight = -Height;   // Indicates a top-to-bottom orientation
            else
                fBitmapInfo.bmiHeader.biHeight = Height;

            fBitmapInfo.bmiHeader.biPlanes = 1;
            fBitmapInfo.bmiHeader.biBitCount = (ushort)bitsperpixel;
            fBitmapInfo.bmiHeader.biSizeImage = (uint)(m_BytesPerRow * Height);
            fBitmapInfo.bmiHeader.biClrImportant = 0;
            fBitmapInfo.bmiHeader.biClrUsed = 0;
            fBitmapInfo.bmiHeader.biCompression = GDI32.BI_RGB;
            //fBitmapInfo.bmiColors = IntPtr.Zero;

            m_pixelPointer = IntPtr.Zero;

            IntPtr bitmapHandle = GDI32.CreateDIBSection(GDIContext.CreateForDefaultDisplay(),
                ref fBitmapInfo, GDI32.DIB_RGB_COLORS, ref m_pixelPointer, IntPtr.Zero, 0);

            return bitmapHandle;
        }
        #endregion

        #region SafeHandle
        public override bool IsInvalid
        {
            get
            {
                return IsClosed || (IntPtr.Zero == base.handle);
            }
        }

        protected override bool ReleaseHandle()
        {
            bool retValue = GDI32.DeleteObject(base.handle) != 0;
            
            return retValue;
        }
        #endregion

        //#region ICloneable
        //public override object Clone()
        //{
        //    // Create a new instance
        //    GDIDIBSection newDIB = new GDIDIBSection(Width, Height, this.BitCount);
            
        //    // Blt the current image into the new one
        //    newDIB.PixBlt(this, 0, 0, new Rectangle(0, 0, Width, Height), TernaryRasterOps.SRCCOPY);

        //    return newDIB;
        //} 
        //#endregion

        #region IPixelAccessor
        public override IPixel GetPixel(int x, int y)
        {
            return Accessor.GetPixel(x, y);
        }

        public override void SetPixel(int x, int y, IPixel aPixel)
        {
            Accessor.SetPixel(x, y, aPixel);
        }
        #endregion

        #region IColorAccessor
        public virtual ColorRGBA GetColor(int x, int y)
        {
            return Accessor.GetColor(x, y);
        }

        public virtual void SetColor(int x, int y, ColorRGBA aColor)
        {
            Accessor.SetColor(x, y, aColor);
        }

        //public virtual void ApplyUnaryColorOperator(IUnaryColorOperator op)
        //{
        //    for (int y = 0; y < Height; y++)
        //        for (int x = 0; x < Width; x++)
        //        {
        //            SetColor(x, y, op.PerformUnaryOperation(GetColor(x, y)));
        //        }
        //}

        //public virtual void ApplyBinaryColorOperator(IBinaryColorOperator op, IColorAccessor srcAccess)
        //{
        //    ColorRGBA srcColor;
        //    ColorRGBA dstColor;

        //    for (int y = 0; y < srcAccess.Height; y++)
        //        for (int x = 0; x < srcAccess.Width; x++)
        //        {
        //            dstColor = GetColor(x, y);
        //            srcColor = srcAccess.GetColor(x, y);
        //            SetColor(x, y, op.ApplyBinaryOperator(dstColor, srcColor));
        //        }
        //}
        #endregion

        #region IPixelInformation
        public int BitsPerPixel
        {
            get { return (int)BitCount; }
        }

        public PixelLayout Layout
        {
            get
            {
                PixelLayout llayout = PixelLayout.Bgr;

                switch (BitCount)
                {
                    case BitCount.Bits24:
                        llayout = PixelLayout.Bgr;
                        break;

                    case BitCount.Bits32:
                        llayout = PixelLayout.Bgra;
                        break;
                }

                return llayout;
            }
        }

        public PixelComponentType ComponentType
        {
            get
            {
                return PixelComponentType.Byte;
            }
        }

        #endregion

        #region Properties

        public PixelAccessor Accessor
        {
            get
            {
                if (null == m_PixelAccessor)
                {
                    switch (Layout)
                    {
                        case PixelLayout.Bgr:
                            m_PixelAccessor = new PixelAccessorBGRb(Width, Height, Orientation, Pixels, BytesPerRow);
                            break;

                        case PixelLayout.Bgra:
                            m_PixelAccessor = new PixelAccessorBGRAb(Width, Height, Orientation, Pixels, BytesPerRow);
                            break;
                    }

                }
                return m_PixelAccessor;
            }
        }

        public int BytesPerRow
        {
            get { return m_BytesPerRow; }
        }

        public PixmapOrientation Orientation
        {
            get
            {
                if (fBitmapInfo.bmiHeader.biHeight < 0)
                    return PixmapOrientation.TopToBottom;
                else
                    return PixmapOrientation.BottomToTop;
            }
        }

        public override IntPtr Pixels
        {
            get { return m_pixelPointer; }
        }
        #endregion

    }
}
