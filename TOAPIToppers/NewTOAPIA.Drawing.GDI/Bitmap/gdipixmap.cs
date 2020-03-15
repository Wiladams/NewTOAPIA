using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Graphics;

    public enum BitCount
    {
        Unknown = 0,
        Bits24 = 24,
        Bits32 = 32
    }

    abstract public class GDIPixmap : SafeHandle, IPixelAccessor
    {
        #region Fields
        GDIContext fMemoryContext;
        //IGraphPort m_GraphPort;
        IntPtr m_OldBMHandle;
        BitCount fBitCount;
        protected PixelRectangleInfo RectInfo { get; set; }
        #endregion

        #region Constructors
        protected GDIPixmap(int width, int height, BitCount bitsperpixel)
            : base(IntPtr.Zero, true)
        {
            if (bitsperpixel == BitCount.Bits24)
                RectInfo = new PixelRectangleInfo(width, height, PixelLayout.Bgr, PixelComponentType.Byte, Alignment);
            else
                RectInfo = new PixelRectangleInfo(width, height, PixelLayout.Bgra, PixelComponentType.Byte, Alignment);
            
            this.fBitCount = bitsperpixel;
    
            GDIContext context = GDIContext.CreateForDefaultDisplay();

            IntPtr theHandle = CreatePixmapHandle(Width, Height, bitsperpixel);
            SetHandle(theHandle);

            fMemoryContext = GDIContext.CreateForMemory();
            m_OldBMHandle = fMemoryContext.SelectObject(this);

           
        }

        protected abstract IntPtr CreatePixmapHandle(int width, int height, BitCount bitsperpixel);
        #endregion

        #region Properties
        public virtual int Alignment
        {
            get { return 2; }
        }

        public IGraphPort GraphPort
        {
            get
            {
                return fMemoryContext.GraphPort;
            }
        }

        public BitCount BitCount
        {
            get { return fBitCount; }
        }

        public GDIContext DeviceContext
        {
            get { return fMemoryContext; }
        }

        public int Width
        {
            get { return RectInfo.Width; }
        }

        public int Height
        {
            get { return RectInfo.Height; }
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
            // Select the old Bitmap handle so the 
            // bitmap is 'deselected' from the context
            //m_MemoryContext.SelectObject(m_OldBMHandle);

            // Now that the bitmap handle is deselected from the context,
            // we can delete the memory context
            fMemoryContext.Dispose();

            // And finally, delete the bitmap object itself
            bool retValue = GDI32.DeleteObject(base.handle) != 0;
            return retValue;
        }
        #endregion

        //#region ICloneable
        //abstract public object Clone();
        //#endregion

        #region IPixelAccessor
        public virtual IntPtr Pixels { get { return IntPtr.Zero; } }

        public virtual IPixel GetPixel(int x, int y)
        {
            throw new NotImplementedException("GDIPixmap::GetPixel");
        }

        public virtual void SetPixel(int x, int y, IPixel pixel)
        {
            throw new NotImplementedException("GDIPixmap::SetPixel");
        }
        #endregion

        //#region IRenderGDIPixmap
        //public void CopyPixels(GDIPixmap pixMap, int srcX, int srcY)
        //{
        //    Rectangle dstRect = new Rectangle(0, 0, pixMap.Width, pixMap.Height);
        //    DeviceContext.BitBlt(pixMap.DeviceContext, srcX, srcY, dstRect, TernaryRasterOps.SRCCOPY);
        //}

        //public void PixBlt(GDIPixmap pixMap, int srcX, int srcY, Rectangle dstRect, TernaryRasterOps rasterOp)
        //{
        //    DeviceContext.BitBlt(pixMap.DeviceContext, srcX, srcY, dstRect, rasterOp);
        //}

        //public void AlphaBlend(GDIPixmap pixMap, Rectangle srcRect, Rectangle dstRect, byte opacity)
        //{
        //    DeviceContext.AlphaBlend(pixMap.DeviceContext, srcRect, dstRect, opacity);
        //}
        //#endregion

        #region Static Helper for Pixel Alignment
        public static int GetAlignedRowStride(int width, BitCount bitsperpixel, int alignment)
        {
            int bytesperpixel = (int)bitsperpixel / 8;
            int stride = (width * bytesperpixel + (alignment - 1)) & ~(alignment - 1);
            return stride;
        }
        #endregion

    }
}
