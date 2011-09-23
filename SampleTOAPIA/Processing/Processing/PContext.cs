using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    public class PContext : PLang
    {
        PRenderer Renderer { get; set; }

        public PContext(PRenderer renderer_)
        {
            Renderer = renderer_;
        }
    }
}
