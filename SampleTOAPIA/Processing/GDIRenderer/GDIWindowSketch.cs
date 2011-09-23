using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing.GDI
{
    using NewTOAPIA.UI;

    using Processing;

    public class GDIWindowSketch : PSketch
    {
        protected override PRenderer GetRenderer(Window win)
        {
            return new PGDIRenderer(win.DeviceContextClientArea);
        }
    }
}
