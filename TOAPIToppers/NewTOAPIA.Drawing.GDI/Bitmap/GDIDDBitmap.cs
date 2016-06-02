using System;

using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    public class GDIDDBitmap : GDIPixmap
    {
        public GDIDDBitmap(int width, int height)
            : base(width, height, BitCount.Unknown)
        {            
        }

        protected override IntPtr CreatePixmapHandle(int width, int height, BitCount bitsperpixel)
        {
            GDIContext context = GDIContext.CreateForDefaultDisplay();
            IntPtr theHandle = GDI32.CreateCompatibleBitmap(context, Width, Height);

            return theHandle;
        }

        //#region ICloneable
        //public override object Clone()
        //{
        //    // Create a new instance
        //    GDIDDBitmap newPixmap = new GDIDDBitmap(Width, Height);

        //    // Blt the current image into the new one
        //    newPixmap.PixBlt(this, 0, 0, new Rectangle(0, 0, Width, Height), TernaryRasterOps.SRCCOPY);

        //    return newPixmap;
        //}
        //#endregion

    }
}
