using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;
using TOAPI.User32;

public class PixelBuffer : IPixelBuffer
{
    IntPtr fMemoryDC;
    IntPtr fBitmapHandle;
    IntPtr fOldBitmapHandle;
    private BITMAPINFO fBitmapInfo;
    private PixelData fPixelData;

    IntPtr fBits;
    byte fAlpha;


    public PixelBuffer(int width, int height)
    {
        // Create a MemoryDC to hold the bitmap.  Use IntPtr.Zero 
        // to create a device context that is compatible with the screen.
        
        fMemoryDC = GDI32.CreateCompatibleDC(IntPtr.Zero);


        // Create a bitmap compatible with the screen
        fBitmapInfo = new BITMAPINFO();
        fBitmapInfo.Init();


        fBitmapInfo.bmiHeader.biWidth = width;
        fBitmapInfo.bmiHeader.biHeight = -height;
        fBitmapInfo.bmiHeader.biPlanes = 1;
        fBitmapInfo.bmiHeader.biBitCount = 32;
        fBitmapInfo.bmiHeader.biClrImportant = 0;
        fBitmapInfo.bmiHeader.biClrUsed = 0;
        fBitmapInfo.bmiHeader.biCompression = GDI32.BI_RGB;
        fBitmapInfo.bmiColors = IntPtr.Zero;
        
        fBitmapHandle = GDI32.CreateDIBSection(User32.GetDC(IntPtr.Zero),
            ref fBitmapInfo, GDI32.DIB_RGB_COLORS, ref fBits, IntPtr.Zero, 0);

        fPixelData = new PixelData(width, height, 32, width * 4, fBits);

        // Get the bitmap structure back out so we can 
        // get our hands on the created pointer and whatnot
        //GDI32.GetBitmap(fBitmapHandle, ref fBitmapStructure);
        //fBits = fBitmapStructure.bmBits;

        // Select the bitmap into the memoryDC
        fOldBitmapHandle = GDI32.SelectObject(fMemoryDC, fBitmapHandle);
        fAlpha = 255;
    }

    public byte Alpha
    {
        get { return fAlpha; }
    }

    public IntPtr DCHandle
    {
        get { return fMemoryDC; }
    }

    public int Height
    {
        get { return fPixelData.Height; }
    }

    public int Width
    {
        get { return fPixelData.Width; }
    }

    public IPixelData Pixels
    {
        get { return fPixelData; }
    }


    /// <summary>
    /// Calculate the byte offset of a particular pixel.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    int CalculateByteOffset(int x, int y)
    {
        int rowoffset = (y * fPixelData.Stride) + (x * 4);

        return rowoffset;
    }

    public virtual bool SetPixel(int x, int y, uint color)
    {
        // First calculate the offset
        int offset = CalculateByteOffset(x, y);
        Marshal.WriteInt32(fBits, offset, (int)color);

        return true;
    }

    public virtual uint GetPixel(int x, int y)
    {
        int offset = CalculateByteOffset(x,y);
        UInt32 color = (uint)Marshal.ReadInt32(fBits, offset);

        return color;
    }
}

