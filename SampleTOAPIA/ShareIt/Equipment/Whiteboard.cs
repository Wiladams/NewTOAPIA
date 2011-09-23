using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace ShowIt
{
    public class Whiteboard : DisplayMonitor
    {
        public Whiteboard(GraphicsInterface gi, AABB boundary)
            :base(gi, boundary, new Resolution(1024,768))
        {
        }
    }
}
