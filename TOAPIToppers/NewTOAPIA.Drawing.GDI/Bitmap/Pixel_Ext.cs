using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Drawing.GDI
{
    using TOAPI.Types;

    using NewTOAPIA;
    using NewTOAPIA.Graphics;

    public static class Pixel_Ext
    {
        public static BITMAPINFO ToBitmapInfo(this PixelRectangleInfo pixmap)
        {
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.Init();

            bmi.bmiHeader.biBitCount = (ushort)pixmap.BitsPerPixel;
            bmi.bmiHeader.biWidth = pixmap.Width;

            if (pixmap.Orientation == PixmapOrientation.TopToBottom)
                bmi.bmiHeader.biHeight = -pixmap.Height;
            else
                bmi.bmiHeader.biHeight = pixmap.Height;

            bmi.bmiHeader.biSizeImage = (uint)pixmap.GetImageSize();
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biClrImportant = 0;
            bmi.bmiHeader.biClrUsed = 0;
            bmi.bmiHeader.biCompression = 0;

            return bmi;
        }

        public static PixelArray ToPixelArray(this GDIDIBSection pixmap)
        {
            //PixelArray newArray = new PixelArray(pixmap.Width, pixmap.Height, new PixelInformation(pixmap.Layout, pixmap.ComponentType),
            //    PixmapOrientation.TopToBottom, pixmap.Alignment, pixmap.Pixels); 

            //return newArray;

            return null;
        }
    }
}
