using System;
using System.Drawing;

namespace NewTOAPIA.UI
{
    public class GraphicElement
    {
        public Point fLocation;
        public IGraphic fGraphic;

        public GraphicElement(IGraphic graphic, Point location)
        {
            fLocation = location;
            fGraphic = graphic;
        }
    }
}
