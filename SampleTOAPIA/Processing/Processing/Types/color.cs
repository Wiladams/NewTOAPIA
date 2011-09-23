using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    using NewTOAPIA.Graphics;

    public class color
    {
        public byte red;
        public byte green;
        public byte blue;
        public byte alpha;

        public color(byte gray)
        {
            red = gray;
            green = gray;
            blue = gray;
            alpha = byte.MaxValue;
        }

        public color(byte gray, byte alpha_)
        {
            red = gray;
            green = gray;
            blue = gray;
            alpha = alpha_;
        }

        public color(byte red_, byte green_, byte blue_)
        {
            red = red_;
            green = green_;
            blue = blue_;
            alpha = byte.MaxValue;
        }

        public color(byte red_, byte green_, byte blue_, byte alpha_)
        {
            red = red_;
            green = green_;
            blue = blue_;
            alpha = alpha_;
        }

        public static implicit operator Colorref (color c)
        {
            return Colorref.FromRGB(c.red, c.green, c.blue);
        }
    }
}
