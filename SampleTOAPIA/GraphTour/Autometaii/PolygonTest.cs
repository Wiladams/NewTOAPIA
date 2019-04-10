using System;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class PolygonTest : Autometus
    {
        Size2I fSize;
        Point2I[] fFigure;

        public PolygonTest(Size2I aSize)
        {
            fSize = aSize;

            fFigure = new Point2I[10];
            fFigure[0] = new Point2I(10, 70);
            fFigure[1] = new Point2I(50, 70);
            fFigure[2] = new Point2I(50, 10);
            fFigure[3] = new Point2I(90, 10);
            fFigure[4] = new Point2I(90, 50);
            fFigure[5] = new Point2I(30, 50);
            fFigure[6] = new Point2I(30, 90);
            fFigure[7] = new Point2I(70, 90);
            fFigure[8] = new Point2I(70, 30);
            fFigure[9] = new Point2I(10, 30);

        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            Point2I[] points = new Point2I[10];

            for (int i = 0; i < 10; i++)
            {
                points[i].X = fSize.Width * fFigure[i].X / 200;
                points[i].Y = fSize.Height * fFigure[i].Y / 100;
            }

            // Select a gray brush to draw with
            //aPort.SelectStockObject(GDI32.GRAY_BRUSH);
            GDIBrush aBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.Vertical, Colorrefs.Blue, Guid.NewGuid());
            aPort.SetBrush(aBrush);

            // First draw with ALTERNATE method
            aPort.SetPolyFillMode(PolygonFillMode.Alternate);
            aPort.Polygon(points);


            // Translate the x coordinates by half the screen
            for (int i = 0; i < 10; i++)
            {
                points[i].X += fSize.Width / 2;
            }

            // Now draw with WINDING method
            aPort.SetPolyFillMode(PolygonFillMode.Winding);
            aPort.Polygon(points);
        }
    }
}
