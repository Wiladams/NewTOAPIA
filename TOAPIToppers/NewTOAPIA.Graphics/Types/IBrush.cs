using System;

namespace NewTOAPIA.Graphics
{

    /// <summary>
    /// GBrush styles
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

    public interface IBrush : IUniqueObject
    {
        Colorref Color { get; }
        BrushStyle BrushStyle { get; }
        HatchStyle HatchStyle { get; }
    }
}
