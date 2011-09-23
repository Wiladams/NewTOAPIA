using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.AviCap32;
using TOAPI.Types;

namespace NewTOAPIA.Media
{
    public class VfwDeCompressor
    {
        IntPtr fHandle;

        public VfwDeCompressor(IntPtr handle)
        {
            fHandle = handle;
        }

        public IntPtr Handle
        {
            get { return fHandle; }
        }

        public IntPtr DeCompress(ref BITMAPINFO bmiIn, IntPtr bits, ref BITMAPINFO bmiOut)
        {
            IntPtr newhandle = Vfw.ICImageDecompress(IntPtr.Zero, 0, ref bmiIn, bits, ref bmiOut);

            return newhandle;
        }


        public static VfwDeCompressor Create(UInt32 fourcc)
        {
            IntPtr handle = Vfw.ICOpen(fourcc, 0, Vfw.ICMODE_FASTDECOMPRESS);
            
            if (IntPtr.Zero == handle)
                return null;

            VfwDeCompressor compressor = new VfwDeCompressor(handle);
            return compressor;
        }
    }
}
