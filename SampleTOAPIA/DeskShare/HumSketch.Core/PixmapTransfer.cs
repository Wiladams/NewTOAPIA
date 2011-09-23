using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace NewTOAPIA
{
    public class PixmapTransfer
    {
        /// <summary>
        /// Copy from a Luminance image to the BGRb pixel buffer.
        /// </summary>
        /// <param name="dst">The destination of the copy.</param>
        /// <param name="src">The source of the copy.</param>
        unsafe public static GDIDIBSection Copy(GDIDIBSection dst, PixelArray<Lumb> src)
        {
            int imageSize = src.Width * src.Height;
            
            for (int y = 0; y < src.Height; y++)
                for (int x = 0; x < src.Width; x++)
                {
                    dst.SetColor(x, y, src.GetColor(x, y));
                }

            return dst;
        }

        unsafe public static PixelArray<Lumb> Copy(PixelArray<Lumb> dst, GDIDIBSection src)
        {
            if (dst.Orientation != src.Orientation)
                return null;


            int imageSize = src.Width * src.Height;

            Lumb* dstPointer = (Lumb*)dst.Pixels.ToPointer();
            BGRb* srcPointer = (BGRb*)src.Pixels.ToPointer();

            for (int y = 0; y < src.Height; y++)
                for (int x = 0; x < src.Width; x++)
                {
                    dst.AssignPixel(x, y, new Lumb(NTSCGray.ToLuminance(*srcPointer)));
                }

            return dst;
        }

    }
}
