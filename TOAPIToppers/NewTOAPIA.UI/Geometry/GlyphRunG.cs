
using System;
using System.Text;

using NewTOAPIA.Drawing;

/// <summary>
/// A GlyphRun is a set of characters that are displayed in a given font.
/// The run only represents a single font, at a given size.  Multiple 
/// GlyphRuns may be assembled to represent a display of text.
/// </summary>
public class GlyphRunG
{
    GDIFont fFont;
    string fString;

    public GlyphRunG(string aString, GDIFont aFont)
    {
        fFont = aFont;
        fString = aString;
    }
}

