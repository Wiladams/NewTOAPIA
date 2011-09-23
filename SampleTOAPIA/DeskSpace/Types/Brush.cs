using System;
using TOAPI.GDI32;

public class Brush : IUniqueGDIObject
{
    Guid fGuid;
    public LOGBRUSH32 fLogBrush;


    public Brush()
        :this(GDI32.BS_SOLID, 0, RGBColor.White, Guid.NewGuid())
    {
    }

    public Brush(int aStyle, int hatch, uint colorref, Guid aGuid)
    {
        fLogBrush = new LOGBRUSH32();
        fLogBrush.lbColor = colorref;
        fLogBrush.lbHatch = hatch;
        fLogBrush.lbStyle = aStyle;

        fGuid = aGuid;
    }


    public virtual IntPtr Handle
    {
        get {return IntPtr.Zero;}

        // This empty set must exist or subclasses won't
        // be able to implement it.
        set { }
    }

    public virtual Guid UniqueID
    {
        get { return fGuid; }
    }

    public virtual LOGBRUSH32 LogicalBrush
    {
        get { return fLogBrush; }
    }
}

/// <summary>
/// Brush styles
/// BS_ constants from WinGDI.h
/// </summary>
public enum BrushStyle : int
{
    Solid = 0,
    Hollow = 1,
    Hatched = 2,
    Pattern = 3,
    Indexed = 4,
    DIBPattern = 5,
    DIBPatternPT = 6,
    Pattern8x8 = 7,
    DIBPattern8X8 = 8,
    MonoPattern = 9
}

// From HS_ constants
public enum HatchStyle
{
    Horizontal = 0,       /* ----- */
    Vertical = 1,       /* ||||| */
    FDiagonal = 2,       /* \\\\\ */
    BDiagonal = 3,       /* ///// */
    Cross = 4,       /* +++++ */
    DiagCross = 5,       /* xxxxx */
}
