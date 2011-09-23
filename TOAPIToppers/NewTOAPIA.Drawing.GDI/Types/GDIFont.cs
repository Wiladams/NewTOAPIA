using System;

using TOAPI.Types;
using TOAPI.GDI32;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GDIFont : GDIObject, IFont
    {
        public LOGFONT LogicalFont { get; set; }
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

            LogicalFont = new LOGFONT();
            //fLogFont.Init();
            LogicalFont.lfCharSet = lfCharSet;
            LogicalFont.lfClipPrecision = lfClipPrecision;
            LogicalFont.lfEscapement = lfEscapement;
            LogicalFont.lfFaceName = lfFaceName;
            LogicalFont.lfHeight = lfHeight;
            LogicalFont.lfItalic = (byte)(lfItalic ? 1 : 0);
            LogicalFont.lfOrientation = lfOrientation;
            LogicalFont.lfOutPrecision = lfOutPrecision;
            LogicalFont.lfPitchAndFamily = lfPitchAndFamily;
            LogicalFont.lfQuality = (byte)lfQuality;
            LogicalFont.lfStrikeOut = (byte)(lfStrikeOut ? 1 : 0);
            LogicalFont.lfUnderline = (byte)(lfUnderline ? 1 : 0);
            LogicalFont.lfWeight = (int)lfWeight;
            LogicalFont.lfWidth = lfWidth;

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
        public Size2D MeasureString(string aString)
        {
            // Create a screen context to do the measuring
            GDIContext dc = GDIContext.CreateForDefaultDisplay();
            dc.SelectObject(this);

            SIZE aSize = new SIZE();
            GDI32.GetTextExtentPoint32(dc, aString, aString.Length, out aSize);

            // Unselect the font from the DC
            // Destroy the DC

            // Return the size
            return new Size2D(aSize.cx, aSize.cy);
        }
    }
}