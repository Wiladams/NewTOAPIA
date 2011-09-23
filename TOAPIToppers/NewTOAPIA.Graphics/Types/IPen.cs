

namespace NewTOAPIA.Graphics
{
    using System;
    using NewTOAPIA.Graphics;

    // Type of pen
    public enum PenType : int
    {
        Cosmetic = 0x00000000,
        Geometric = 0x00010000,
    }

    // Pen Styles
    public enum PenStyle : int
    {
        Solid = 0,
        Dash = 1,       /* -------  */
        Dot = 2,       /* .......  */
        DashDot = 3,       /* _._._._  */
        DashDotDot = 4,       /* _.._.._  */
        Null = 5,
        InsideFrame = 6,
        UserStyle = 7,
        Alternate = 8,
        StyleMask = 0x0000000F
    }

    // Pen End Cap
    public enum PenEndCap : int
    {
        Round = 0x00000000,
        Square = 0x00000100,
        Flat = 0x00000200,
        EndcapMask = 0x00000F00
    }

    // Style of joining lines
    public enum PenJoinStyle : int
    {
        Round = 0x00000000,
        Bevel = 0x00001000,
        Miter = 0x00002000,
        JoinMask = 0x0000F000
    }


    /// <summary>
    /// StockSolidPen
    /// </summary>
    public enum PenStockSolid
    {
        Black = 7,
        Null = 8,
        White = 6,
        DC = 19		// From WinGdi.h
    }

    public interface IPen : IUniqueObject
    {
        PenType TypeOfPen {get;}
        PenStyle Style {get;}
        PenJoinStyle JoinStyle {get;}
        PenEndCap EndCap {get;}
        int Width {get;}
        Colorref Color {get;}
    }
}