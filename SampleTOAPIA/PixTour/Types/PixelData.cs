using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.GDI32;
//using TOAPI.User32;

public class PixelData : IPixelData
{
    int fWidth;
    int fHeight;
    int fBitsPerPixel;
    int fStride;
    IntPtr fBits;

    public PixelData(IPixelData somePixels)
    {
        fWidth = somePixels.Width;
        fHeight = somePixels.Height;
        fBitsPerPixel = somePixels.BitsPerPixel;
        fStride = somePixels.Stride;
        // Allocate some memory in a byte array
       
        // Pin it, 
        // and then get the address of the pinned array
    }

    public PixelData(int width, int height, int bitsPerPixel, int stride, IntPtr bits)
    {
        // Create a bitmap compatible with the screen
        fWidth = width;
        fHeight = height;
        fBitsPerPixel = bitsPerPixel;
        fStride = stride;
        fBits = bits;
    }

    public int BitsPerPixel
    {
        get { return fBitsPerPixel; }
    }

    public int Height
    {
        get { return fHeight; }
    }

    public int Width
    {
        get { return fWidth; }
    }

    public IntPtr Data
    {
        get { return fBits; }
    }

    public int Stride
    {
        get { return fStride; }
    }
}

