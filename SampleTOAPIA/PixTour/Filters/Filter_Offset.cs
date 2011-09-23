using System;
using System.Runtime.InteropServices;

using TOAPI.Types;


public class OffsetFilter
{
	public static bool FilterAbs(PixelBuffer b, Point[,] offset)
	{
        // Make a pristine copy first so that we don't have any 
        // overlap when we're moving bits around
        int[] srcData = new int[b.Width * b.Height];
        Marshal.Copy(b.Pixels.Data, srcData, 0, b.Pixels.Stride * b.Height);

        // Local copies of the bitmap properties
		int scanline = b.Pixels.Stride;
		int nWidth = b.Width;
		int nHeight = b.Height;

		int xOffset, yOffset;

		for (int y = 0; y < nHeight; ++y)
		{
			for (int x = 0; x < nWidth; ++x)
			{
				xOffset = offset[x, y].x;
				yOffset = offset[x, y].y;

				if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
				{
                    uint aColor = (uint)srcData[(yOffset * scanline) + (xOffset)];
                    b.SetPixel(x, y, aColor);
				}
			}
		}
	
		return true;
	}

    //public static bool OffsetFilter(PixelBuffer b, Point[,] offset)
    //{
    //    Bitmap bSrc = (Bitmap)b.Clone();

    //    // GDI+ still lies to us - the return format is BGR, NOT RGB.
    //    BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
    //    BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

    //    int scanline = bmData.Stride;

    //    System.IntPtr Scan0 = bmData.Scan0;
    //    System.IntPtr SrcScan0 = bmSrc.Scan0;

    //    unsafe
    //    {
    //        //byte* p = (byte*)(void*)Scan0;
    //        //byte* pSrc = (byte*)(void*)SrcScan0;

    //        int nOffset = bmData.Stride - b.Width * 4;
    //        int nWidth = b.Width;
    //        int nHeight = b.Height;

    //        int xOffset, yOffset;

    //        for (int y = 0; y < nHeight; ++y)
    //        {
    //            for (int x = 0; x < nWidth; ++x)
    //            {
    //                xOffset = offset[x, y].X;
    //                yOffset = offset[x, y].Y;

    //                if (y + yOffset >= 0 && y + yOffset < nHeight && x + xOffset >= 0 && x + xOffset < nWidth)
    //                {
    //                    p[0] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3)];
    //                    p[1] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 1];
    //                    p[2] = pSrc[((y + yOffset) * scanline) + ((x + xOffset) * 3) + 2];
    //                }

    //                p += 3;
    //            }
    //            p += nOffset;
    //        }
    //    }


    //    return true;
    //}

    //public static bool OffsetFilterAntiAlias(Bitmap b, PointDouble[,] fp)
    //{
    //    Bitmap bSrc = (Bitmap)b.Clone();

    //    // GDI+ still lies to us - the return format is BGR, NOT RGB.
    //    BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
    //    BitmapData bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

    //    int scanline = bmData.Stride;

    //    System.IntPtr Scan0 = bmData.Scan0;
    //    System.IntPtr SrcScan0 = bmSrc.Scan0;

    //    unsafe
    //    {
    //        byte* p = (byte*)(void*)Scan0;
    //        byte* pSrc = (byte*)(void*)SrcScan0;

    //        int nOffset = bmData.Stride - b.Width * 3;
    //        int nWidth = b.Width;
    //        int nHeight = b.Height;

    //        double xOffset, yOffset;

    //        double fraction_x, fraction_y, one_minus_x, one_minus_y;
    //        int ceil_x, ceil_y, floor_x, floor_y;
    //        Byte p1, p2;

    //        for (int y = 0; y < nHeight; ++y)
    //        {
    //            for (int x = 0; x < nWidth; ++x)
    //            {
    //                xOffset = fp[x, y].X;
    //                yOffset = fp[x, y].Y;

    //                // Setup

    //                floor_x = (int)Math.Floor(xOffset);
    //                floor_y = (int)Math.Floor(yOffset);
    //                ceil_x = floor_x + 1;
    //                ceil_y = floor_y + 1;
    //                fraction_x = xOffset - floor_x;
    //                fraction_y = yOffset - floor_y;
    //                one_minus_x = 1.0 - fraction_x;
    //                one_minus_y = 1.0 - fraction_y;

    //                if (floor_y >= 0 && ceil_y < nHeight && floor_x >= 0 && ceil_x < nWidth)
    //                {
    //                    // Blue

    //                    p1 = (Byte)(one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 3]) +
    //                        fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 3]));

    //                    p2 = (Byte)(one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 3]) +
    //                        fraction_x * (double)(pSrc[ceil_y * scanline + 3 * ceil_x]));

    //                    p[x * 3 + y * scanline] = (Byte)(one_minus_y * (double)(p1) + fraction_y * (double)(p2));

    //                    // Green

    //                    p1 = (Byte)(one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 3 + 1]) +
    //                        fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 3 + 1]));

    //                    p2 = (Byte)(one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 3 + 1]) +
    //                        fraction_x * (double)(pSrc[ceil_y * scanline + 3 * ceil_x + 1]));

    //                    p[x * 3 + y * scanline + 1] = (Byte)(one_minus_y * (double)(p1) + fraction_y * (double)(p2));

    //                    // Red

    //                    p1 = (Byte)(one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 3 + 2]) +
    //                        fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 3 + 2]));

    //                    p2 = (Byte)(one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 3 + 2]) +
    //                        fraction_x * (double)(pSrc[ceil_y * scanline + 3 * ceil_x + 2]));

    //                    p[x * 3 + y * scanline + 2] = (Byte)(one_minus_y * (double)(p1) + fraction_y * (double)(p2));
    //                }
    //            }
    //        }
    //    }

    //    b.UnlockBits(bmData);
    //    bSrc.UnlockBits(bmSrc);

    //    return true;
    //}
}
