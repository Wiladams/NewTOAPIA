using System;

namespace NewTOAPIA.UI
{

	public enum Alignment
	{
		None = 0,
		Left = 0x01,
		Center = 0x02,
		Right = 0x04,
		Top = 0x10,
		Middle = 0x20,
		Bottom = 0x40
	}

	// The Position enum is meant to give relative position
	// between two graphics.  The numbers are derived by the 
	// layout of keys on a numeric keypad
	//	
	//		7  8  9
	//		4  5  6
	//		1  2  3
	//
	public enum Position
	{
		TopLeft = 0x07,
		Top = 0x08,
		TopRight = 0x09,
		Left = 0x04,
		Center = 0x05,
		Right = 0x06,
		BottomLeft = 0x01,
		Bottom = 0x02,
		BottomRight = 0x03
	}

    public enum Direction
    {
        LeftRight,
		RightLeft,
		TopBottom,
		BottomTop
    }

	public enum Orientation
	{
		Vertical,
		Horizontal
	}
}
