using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.UI
{
    [Flags]
    public enum PositionReport
    {
        Report      = 0x01,
        Moved        = 0x02,
        Resized      = 0x04,
    }

    public class PositionActivity
    {
        public PositionReport Position { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public PositionActivity(int v1, int v2, PositionReport pType)
        {
            this.Position = pType;

            if ((pType & PositionReport.Moved) == PositionReport.Moved)
            {
                this.X = v1;
                this.Y = v2;
            }

            if ((pType & PositionReport.Resized) == PositionReport.Resized)
            {
                this.Width = v1;
                this.Height = v2;
            }
        }

        public PositionActivity(int x, int y, int width, int height, PositionReport pType)
        {
            this.Position = pType;
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }
    }
}
