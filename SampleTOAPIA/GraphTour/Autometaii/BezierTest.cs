using System;
using System.Drawing;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class BezierTest : Autometus
    {
        Size2I fSize;
        Point2I[] fFigure;
        GDIPen fBlackPen;
        GDIPen fRedPen;

        public BezierTest(Size2I aSize)
        {
            fFigure = new Point2I[4];
            Dimension = aSize;

            fBlackPen = new GDIPen(Colorrefs.Black);
            fRedPen = new GDIPen(Colorrefs.Red);
        }

        public Size2I Dimension
        {
            get { return fSize; }
            set
            {
                fSize = value;

                fFigure[0] = new Point2I(fSize.Width / 4, fSize.Height / 2);
                fFigure[1] = new Point2I(fSize.Width / 2, fSize.Height / 4);
                fFigure[2] = new Point2I(fSize.Width / 2, 3 * fSize.Height / 4);
                fFigure[3] = new Point2I(3 * fSize.Width / 4, fSize.Height / 2);
            }
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            aPort.SaveState();

            // Orient the axis with origin at upper 
            // left hand corner, y-axis positive going down the screen
            aPort.SetMappingMode(MappingModes.Text);

            // Draw the curve
            aPort.DrawBeziers(fBlackPen, fFigure);

            // Draw the control lines
            // Control line 1
            aPort.DrawLine(fRedPen, new Point2I(fFigure[0].X, fFigure[0].Y), new Point2I(fFigure[1].X, fFigure[1].Y));

            // Control line 2
            aPort.DrawLine(fRedPen, new Point2I(fFigure[2].X, fFigure[2].Y), new Point2I(fFigure[3].X, fFigure[3].Y));

            aPort.Flush();
            aPort.ResetState();
        }
    }
}
