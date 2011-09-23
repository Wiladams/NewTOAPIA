using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing.GDI
{
    //public class GDIDIBitmap : PixelArray
    //{
    //    public BITMAPINFO fBitmapInfo;

    //    public GDIDIBitmap(PixelArray pix)
    //    {
    //        // Create a bitmap compatible with the screen
    //        fBitmapInfo = new BITMAPINFO();
    //        fBitmapInfo.Init();


    //        fBitmapInfo.bmiHeader.biWidth = pix.Width;
    //        fBitmapInfo.bmiHeader.biHeight = -pix.Height;
    //        fBitmapInfo.bmiHeader.biPlanes = 1;
    //        fBitmapInfo.bmiHeader.biBitCount = (ushort)pix.BitsPerPixel;
    //        fBitmapInfo.bmiHeader.biClrImportant = 0;
    //        fBitmapInfo.bmiHeader.biClrUsed = 0;
    //        fBitmapInfo.bmiHeader.biCompression = GDI32.BI_RGB;
    //        fBitmapInfo.bmiColors = IntPtr.Zero;

    //        Pixels = pix.Pixels;
    //        Layout = PixelLayout.Bgra;
    //        ComponentType = PixelComponentType.Byte;
    //        Height = pix.Height;
    //        Width = pix.Width;
    //    }

    //    #region Properties

    //    public BITMAPINFO BitmapInfo
    //    {
    //        get { return fBitmapInfo; }
    //    }

    //    #endregion
    //}
}
