using System;
using TOAPI.Types;
using TOAPI.GDI32;

public class Pen : IUniqueGDIObject
{
    Guid fGuid;
    public LOGPEN fLogPen;

    public Pen()
        : this(GDI32.PS_SOLID,0,RGBColor.Black,Guid.NewGuid())
    {
    }

    public Pen(int penStyle, int width, uint colorref, Guid aGuid)
    {
        fLogPen = new LOGPEN();
        fLogPen.lopnColor = colorref;
        fLogPen.lopnStyle = penStyle;
        fLogPen.lopnWidth = width;
        fGuid = aGuid;
    }

    public virtual Guid UniqueID
    {
        get { return fGuid; }
    }

    public virtual IntPtr Handle
    {
        get { return IntPtr.Zero; }
        set { }
    }

    public LOGPEN LogicalPen
    {
        get { return fLogPen; }
    }

}

///// <summary>
///// StockSolidPen
///// </summary>
//public enum StockSolid
//{
//    Black = 7,
//    Null = 8,
//    White = 6,
//    DC = 19		// From WinGdi.h
//}

//public enum BrushStyle : int
//{
//    Solid = 0,
//    Dash = 1,
//    Dot = 2,
//    DashDot = 3,
//    DashDotDot = 4,
//    Null = 5,
//    InsideFrame = 6,
//    UserStyle = 7,
//    Alternate = 8,
//}