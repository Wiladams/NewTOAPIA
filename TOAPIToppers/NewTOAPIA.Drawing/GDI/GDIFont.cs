using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing
{

    public class GDIFont : GDIObject
    {
        [Flags]
        public enum FontStyle : int
        {
            Regular = 0x00,
            Bold = 0x01,
            Italic = 0x02,
            Underline = 0x04,
            Strikethrough = 0x10
        }

        public enum FontWeight : short
        {
            DontCare = 0,
            THIN = 100,
            EXTRALIGHT = 200,
            ULTRALIGHT = 200,
            LIGHT = 300,
            NORMAL = 400,
            REGULAR = 400,
            MEDIUM = 500,
            SEMIBOLD = 600,
            DEMIBOLD = 600,
            BOLD = 700,
            EXTRABOLD = 800,
            ULTRABOLD = 800,
            HEAVY = 900,
        }

        // font quality constants used for LOGFONT quality
        public enum FontQuality : byte
        {
            Default = 0,
            Draft = 1,
            Proof = 2,
            NonAntialiased = 3,
            Antialiased = 4,
            ClearType = 5,
            ClearTypeNatural = 6
        }

        // Pitch constants for LOGFONT
        public enum FontPitch : int
        {
            Default = 0,
            Fixed = 1,
            Variable = 2,
            Mono = 8
        }

        public LOGFONT fLogFont;
        string fFaceName;
        int fHeight;

        #region Constructors
        public GDIFont(string faceName, int lfHeight)
            : this(faceName, lfHeight, 0, GDI32.DEFAULT_CHARSET, FontQuality.Default, FontWeight.NORMAL, false, false, false, 0, 0, 0, 0, 0, Guid.NewGuid())
        {
        }

        public GDIFont(string faceName, int lfHeight, Guid aGuid)
            : this(faceName, lfHeight, 0, GDI32.DEFAULT_CHARSET, FontQuality.Default, FontWeight.NORMAL, false, false, false, 0, 0, 0, 0, 0, aGuid)
        {
        }

        public GDIFont(string lfFaceName, int lfHeight,
            byte lfPitchAndFamily, byte lfCharSet,
            FontQuality lfQuality, FontWeight lfWeight, bool lfItalic, bool lfStrikeOut, bool lfUnderline,
            byte lfClipPrecision, byte lfOutPrecision,
            int lfEscapement, int lfOrientation,
            int lfWidth, Guid aGuid)
            : base(true, aGuid)
        {
            fFaceName = lfFaceName;
            fHeight = lfHeight;

            fLogFont = new LOGFONT();
            //fLogFont.Init();
            fLogFont.lfCharSet = lfCharSet;
            fLogFont.lfClipPrecision = lfClipPrecision;
            fLogFont.lfEscapement = lfEscapement;
            fLogFont.lfFaceName = lfFaceName;
            fLogFont.lfHeight = lfHeight;
            fLogFont.lfItalic = (byte)(lfItalic ? 1 : 0);
            fLogFont.lfOrientation = lfOrientation;
            fLogFont.lfOutPrecision = lfOutPrecision;
            fLogFont.lfPitchAndFamily = lfPitchAndFamily;
            fLogFont.lfQuality = (byte)lfQuality;
            fLogFont.lfStrikeOut = (byte)(lfStrikeOut ? 1 : 0);
            fLogFont.lfUnderline = (byte)(lfUnderline ? 1 : 0);
            fLogFont.lfWeight = (int)lfWeight;
            fLogFont.lfWidth = lfWidth;

            //fFontHandle = GDI32.CreateFontIndirect(ref fLogFont);
            
            IntPtr fontHandle = GDI32.CreateFontW(
                lfHeight, lfWidth,
                lfEscapement, lfOrientation,
                (int)lfWeight,
                (uint)(lfItalic ? 1 : 0), 
                (uint)(lfUnderline ? 1 : 0), 
                (uint)(lfStrikeOut ? 1 : 0),
                lfCharSet, 
                lfOutPrecision, 
                lfClipPrecision,
                (uint)lfQuality, 
                lfPitchAndFamily, 
                lfFaceName);

            SetHandle(fontHandle);
        }
        #endregion

        public string FaceName
        {
            get { return fFaceName; }
        }

        public int Height
        {
            get { return fHeight; }
        }

        // Measure how wide and high the string is standing on its own
        /// <summary>
        /// This is the way to measure a string.
        /// </summary>
        /// <param name="aString"></param>
        /// <returns></returns>
        public System.Drawing.Size MeasureString(string aString)
        {
            // Create a screen context to do the measuring
            GDIContext dc = GDIContext.CreateForDefaultDisplay();
            dc.SelectObject(this);

            SIZE aSize = new SIZE();
            GDI32.GetTextExtentPoint32(dc, aString, aString.Length, out aSize);

            // Unselect the font from the DC
            // Destroy the DC

            // Return the size
            return new System.Drawing.Size(aSize.cx, aSize.cy);
        }

        public LOGFONT LogicalFont
        {
            get { return fLogFont; }
        }
    }
}