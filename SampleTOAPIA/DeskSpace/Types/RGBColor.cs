using System;


/*
    The RGBColor class helps in the creation of unsinged integers that 
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

/// <summary>
/// Enum to determine how pixels are combined with the background for 
/// some drawing calls.  This is used for text drawing, as well as
/// some of the bitmap drawing functions.
/// </summary>
public enum BackgroundMixMode : int
{
	OPAQUE = 2,
	TRANSPARENT = 1
};

public class RGBColor
{
	public const int         
		OPAQUE = 2,
		TRANSPARENT = 1;

	/* 0xAABBGGRR */
	// The alpha component must be 0 or win32 will not display the
	// color properly.  It will show up as black.
    public const uint
        Black       = 0x00000000,
        DarkRed = 0x00000080,
        Red = 0x000000ff,
        Pink = 0x00cbc0ff,
        DarkOrange = 0x00008cff,
        Tomato = 0x004763ff,
        DarkGreen = 0x00008000,
        Green       = 0x0000ff00,
        DarkBlue = 0x00800000,
        Blue = 0x00ff0000,
        DarkYellow = 0x00008080,
        Yellow = 0x0000ffff,
        DarkCyan = 0x00808000,
        Cyan        = 0x0000ffff,
        DarkMagenta = 0x00800080,
        Magenta     = 0x00ff00ff,
        Gray = 0x00bebebe,
        LtGray = 0x00c0c0c0,
        LightGray      = 0x00d3d3d3,
        MedGray     = 0x00A4A0A4,
        DarkGray    = 0x00808080,
        Gray25 = 0x00404040,
        Cream       = 0x00F0FBFF,
        White       = 0x00ffffff;

	public static uint RGB(byte r, byte g, byte b)  
	{
		return (((r|((uint)g<<8))|(((uint)b)<<16)));
	}



	public static byte R(uint colorref)
	{
		return (byte)(colorref & 0xff);
	}

	public static byte G(uint colorref)
	{
		return (byte)((colorref >> 8) & 0xff);
	}

	public static byte B(uint colorref)
	{
		return (byte)((colorref >> 16) & 0xff);
	}

	// 16 -bit colors for TRIVERTEX
	public static ushort R16(uint colorref)
	{
		ushort aShort = (ushort)((colorref & 0xff) << 8);
		return aShort;
	}

	public static ushort G16(uint colorref)
	{
		return (ushort)(((colorref >> 8) & 0xff)<<8);
	}

	public static ushort B16(uint colorref)
	{
		return (ushort)(((colorref >> 16) & 0xff)<<8);
	}

	// 16 -bit interpolated colors for TRIVERTEX
	public static ushort R16(uint c0, uint c1)
	{
		ushort aShort = (ushort)(((RGBColor.R(c0)+RGBColor.R(c1))/2) << 8);
		return aShort;
	}

	public static ushort G16(uint c0, uint c1)
	{
		ushort aShort = (ushort)(((RGBColor.G(c0) + RGBColor.G(c1)) / 2) << 8);
		return aShort;
	}

	public static ushort B16(uint c0, uint c1)
	{
		ushort aShort = (ushort)(((RGBColor.B(c0) + RGBColor.B(c1)) / 2) << 8);
		return aShort;
	}


}

