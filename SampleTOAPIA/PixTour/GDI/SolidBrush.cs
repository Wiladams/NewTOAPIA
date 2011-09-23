using System;
using TOAPI.Types;


/// <summary>
///    Summary description for Win32Brush.
/// </summary>
public class SolidBrush : GDIBrush
{
	public static SolidBrush NullBrush = new SolidBrush(StockSolidBrush.Null);
	public static SolidBrush BlackBrush = new SolidBrush(StockSolidBrush.Black);
	public static SolidBrush GrayBrush = new SolidBrush(StockSolidBrush.Gray);
	public static SolidBrush LightGrayBrush = new SolidBrush(StockSolidBrush.LightGray);
	public static SolidBrush WhiteBrush = new SolidBrush(StockSolidBrush.White);
	public static SolidBrush DarkGrayBrush = new SolidBrush(StockSolidBrush.DarkGray);
    public static SolidBrush DeviceContextBrush = new SolidBrush(StockSolidBrush.DC);

	private SolidBrush(StockSolidBrush index)
		: base(index)
	{
	}

    public SolidBrush(uint color)
		: base(color)
    {
    }
}

