
using TOAPI.Types;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class TextRunG
    {
        string fString;
        System.Drawing.Point fOrigin;
        GDIFont fFont;

        public TextRunG(string aString, System.Drawing.Point origin)
        {
            fOrigin = origin;
            fString = aString;
        }

        public string String
        {
            get { return fString; }
        }

        public System.Drawing.Point Origin
        {
            get { return fOrigin; }
            set { fOrigin = value; }
        }

        public GDIFont Font
        {
            get { return fFont; }
            set { fFont = value; }
        }

    }
}
