
namespace NewTOAPIA.Graphics
{
    using System;


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

    public interface IFont : IUniqueObject
    {
        string FaceName {get;}

        int Height {get;}

        Size2D MeasureString(string aString);

    }
}