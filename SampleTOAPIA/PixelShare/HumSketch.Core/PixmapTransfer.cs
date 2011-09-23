using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace NewTOAPIA
{
    using NewTOAPIA.Drawing.GDI;

    public class PixmapTransfer
    {
        /// <summary>
        /// Copy from a Luminance image to the BGRb pixel buffer.
        /// </summary>
        /// <param name="dst">The destination of the copy.</param>
        /// <param name="src">The source of the copy.</param>
        public static GDIDIBSection Copy(GDIDIBSection dst, PixelArray<Lumb> src)
        {
            //Lumb* srcPointer = (Lumb*)src.Pixels.ToPointer();
            //BGRb* dstPointer = (BGRb*)dst.Pixels.ToPointer();
            
            for (int row = 0; row < src.Height; row++)
                for (int column = 0; column < src.Width; column++)
                {
                    dst.SetColor(column, row, src.GetColor(column, row));
                    //Lumb lumPixel = srcPointer[src.CalculateOffset(column, row)];
                    //byte lum = lumPixel.Lum;

                    //dstPointer[dst.Accessor.CalculateOffset(column, row)] = new BGRb(lum, lum, lum);
                }

            return dst;
        }

        public static PixelArray<Lumb> Copy(PixelArray<Lumb> dst, GDIDIBSection src)
        {
            if (dst.Orientation != src.Orientation)
                return null;


            int imageSize = src.Width * src.Height;

            //Lumb* dstPointer = (Lumb*)dst.Pixels.ToPointer();
            //BGRb* srcPointer = (BGRb*)src.Pixels.ToPointer();

            for (int row = 0; row < src.Height; row++)
                for (int column =0; column < src.Width; column++)
            {

                dst.SetColor(column, row, src.GetColor(column, row));
                //*dstPointer = new Lumb(NTSCGray.ToLuminance(*srcPointer));

                //dstPointer++;
                //srcPointer++;
            }

            //for (int row = 0; row < src.Height; row++)
            //    for (int column = 0; column < src.Width; column++)
            //    {
            //        BGRb srcPixel = srcPointer[src.CalculateOffset(column, row)];
            //        byte lum = NTSCGray.ToLuminance(srcPointer[src.CalculateOffset(column, row)]);

            //        dstPointer[dst.CalculateOffset(column, row)] = new Lumb(lum);
            //    }

            return dst;
        }

    }
}
