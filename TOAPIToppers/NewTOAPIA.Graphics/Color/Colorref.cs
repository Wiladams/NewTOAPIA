using System;

namespace NewTOAPIA.Graphics
{

    /*
        The Colorref class helps in the creation of unsigned integers that 
        represent color for usage in the GDI drawing system.  From Windows,
        This is Correlated to the  COLORREF type.
  
      The representation
     * 
     * 0			Red		Green	Blue		RGB(Red, Green, Blue)
     * 1			0		Index				PALETTEINDEX(index)
     * 2			Red		Green	Blue		PALETTERGB(Red, Green, Blue)
     * 32 bits	24		16		8		0

        That is, depending on on the value of the byte from bits 24-31, the color
     * value represents either an RGB color (0), a PALETTEINDEX color (1), 
     * or a PALETTERGB color (2).
     * 
        The RGBColor object can represent any of these types of colors.  Use
     * the appropriate static function to get the one you want.  
     * The most common and easily used one is the RGB() function.
     * */

    public struct Colorref
    {
        public const int
            OPAQUE = 2,
            TRANSPARENT = 1;

        uint colorref;

        #region Constructors
        public Colorref(uint colorref_)
        {
            // Need to take off the alpha?
            colorref = colorref_;
        }

        public Colorref(Colorref color)
        {
            colorref = color.colorref;
        }

        public Colorref(byte r, byte g, byte b)
        {
            colorref = (((r | ((uint)g << 8)) | (((uint)b) << 16)));
        }
        #endregion

        #region Properties
        public byte Red
        {
            get 
            {
                return (byte)(colorref & 0xff);
            }
        }

        public byte Green
        {
            get 
            {
                return (byte)((colorref >> 8) & 0xff);
            }
        }

        public byte Blue
        {
            get 
            {
                return (byte)((colorref >> 16) & 0xff);
            }
        }

        // 16 -bit colors for TRIVERTEX
        public ushort Red16
        {
            get
            {
                ushort aShort = (ushort)((colorref & 0xff) << 8);
                return aShort;
            }
        }

        public ushort Green16
        {
            get
            {
                return (ushort)(((colorref >> 8) & 0xff) << 8);
            }
        }

        public ushort Blue16
        {
            get
            {
                return (ushort)(((colorref >> 16) & 0xff) << 8);
            }
        }
        #endregion

        #region Operator Overloads
        public static implicit operator uint (Colorref color)
        {
            return color.colorref;
        }

        public static explicit operator Colorref(uint color)
        {
            return new Colorref(color);
        }
        #endregion


        #region Static Helpers
        public static Colorref FromRGB(byte r, byte g, byte b)
        {
            return new Colorref(r, g, b);
        }
        private static uint RGB(byte r, byte g, byte b)
        {
            return (((r | ((uint)g << 8)) | (((uint)b) << 16)));
        }

        private static uint PALETTERGB(byte r, byte g, byte b)
        {
            return (0x02000000 | RGB(r, g, b));
        }


        // 16 -bit interpolated colors for TRIVERTEX
        //public static ushort R16(uint c0, uint c1)
        //{
        //    ushort aShort = (ushort)(((Colorref.R(c0) + Colorref.R(c1)) / 2) << 8);
        //    return aShort;
        //}

        //public static ushort G16(uint c0, uint c1)
        //{
        //    ushort aShort = (ushort)(((Colorref.G(c0) + Colorref.G(c1)) / 2) << 8);
        //    return aShort;
        //}

        //public static ushort B16(uint c0, uint c1)
        //{
        //    ushort aShort = (ushort)(((Colorref.B(c0) + Colorref.B(c1)) / 2) << 8);
        //    return aShort;
        //}
        #endregion

    }

    public static class Colorrefs
    {
        #region Useful Colors
        /* 0xAABBGGRR */
        // The alpha component must be 0 or win32 will not display the
        // color properly.  It will show up as black.
        public static readonly Colorref Black = Colorref.FromRGB(0, 0, 0);
        public static readonly Colorref Red = Colorref.FromRGB(0xff, 0, 0);
        public static readonly Colorref Green = Colorref.FromRGB(0, 0xff, 0);
        public static readonly Colorref Blue = Colorref.FromRGB(0, 0, 0xff);
        public static readonly Colorref LtGray = Colorref.FromRGB(0xc0,0xc0,0xc0);
        public static readonly Colorref White = Colorref.FromRGB(0xff, 0xff, 0xff);

        public const uint
            DarkRed = 0x00000080,
            Pink = 0x00cbc0ff,
            DarkOrange = 0x00008cff,
            Tomato = 0x004763ff,
            DarkGreen = 0x00008000,
            DarkBlue = 0x00800000,
            DarkYellow = 0x00008080,
            Yellow = 0x0000ffff,
            DarkCyan = 0x00808000,
            Cyan = 0x00ffff00,
            DarkMagenta = 0x00800080,
            Magenta = 0x00ff00ff,
            Gray = 0x00bebebe,
            LightGray = 0x00d3d3d3,
            MedGray = 0x00A4A0A4,
            DarkGray = 0x00808080,
            Gray25 = 0x00404040,
            Cream = 0x00F0FBFF;
        #endregion
    }
}
