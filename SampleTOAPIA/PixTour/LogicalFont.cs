using System;
using System.Collections.Generic;
using System.Text;

namespace Papyrus.Types
{
    public class LogicalFont
    {
        public int fHeight;
        public int fWidth;
        public int fEscapement;
        public int fOrientation;
        public Font.FontWeight fWeight;
        public byte fItalic;
        public byte fUnderline;
        public byte fStrikeOut;
        public byte fCharSet;
        public byte fOutPrecision;
        public byte fClipPrecision;
        public Font.Quality fQuality;
        public byte fPitchAndFamily;
        public string fFaceName;

        IntPtr fHandle;


        public LogicalFont(string typeface, int height)
        {
            fHeight = height;
            fWeight = 0;
            fEscapement = 0;
            fOrientation = 0;
            fWeight = Font.FontWeight.NORMAL;
            fItalic = 0;
            fUnderline = 0;
            fStrikeOut = 0;
            fCharSet = Font.DEFAULT_CHARSET;
            fOutPrecision = Font.OUT_TT_PRECIS;
            fClipPrecision = Font.CLIP_DEFAULT_PRECIS;
            fQuality = Font.Quality.DEFAULT;
            fPitchAndFamily = (byte)((int)Font.Pitch.Default | Font.FF_DONTCARE);
            fFaceName = typeface;

            fHandle = IntPtr.Zero;
        }

        public IntPtr Handle
        {
            get { return fHandle; }
        }

        /// <summary>
        /// Create a font object given a typeface and size.
        /// </summary>
        /// <param name="typeface"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static IntPtr CreateFont(string typeface, int height)
        {
            return IntPtr.Zero;
            //return GDI.CreateFontIndirect(fLF);
        }
    }
}
