

namespace Papyrus.Types
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    public class TextRunG
    {
        string fString;
        PointG fOrigin;
        Font fFont;

        public TextRunG(string aString, PointG origin)
        {
            fOrigin = origin;
            fString = aString;
        }

        public string String
        {
            get { return fString; }
        }

        public PointG Origin
        {
            get { return fOrigin; }
            set { fOrigin = value; }
        }

        public Font Font
        {
            get { return fFont; }
            set { fFont = value; }
        }

    }
}
