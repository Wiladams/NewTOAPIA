
using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using NewTOAPIA.Graphics;

    public class TextRunG
    {
        string fString;
        Point3D fOrigin;
        IFont fFont;

        public TextRunG(string aString, Point3D origin)
        {
            fOrigin = origin;
            fString = aString;
        }

        public string String
        {
            get { return fString; }
        }

        public Point3D Origin
        {
            get { return fOrigin; }
            set { fOrigin = value; }
        }

        public IFont Font
        {
            get { return fFont; }
            set { fFont = value; }
        }

    }
}
