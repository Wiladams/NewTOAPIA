using System;
using System.Collections.Generic;

using TOAPI.Types;
using TOAPI.GDI32;

namespace Autometaii
{
    [Serializable]
    public class PolygonTest : Autometus
    {
        Size fSize;
        Point2I[] fFigure;

        public PolygonTest(Size aSize)
        {
            fSize = aSize;

            fFigure = new Point[10];
            fFigure[0] = new Point(10,70);
            fFigure[1] = new Point(50,70);
            fFigure[2] = new Point(50,10);
            fFigure[3] = new Point(90,10);
            fFigure[4] = new Point(90,50);
            fFigure[5] = new Point(30,50);
            fFigure[6] = new Point(30,90);
            fFigure[7] = new Point(70,90);
            fFigure[8] = new Point(70,30);
            fFigure[9] = new Point(10,30);

        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            Point[] points = new Point[10];

            for (int i = 0; i < 10; i++)
            {
                points[i].x = fSize.cx * fFigure[i].x / 200;
                points[i].y = fSize.cy * fFigure[i].y / 100;
            }

            // Select a gray brush to draw with
            aPort.SelectStockObject(GDI32.GRAY_BRUSH);

            // First draw with ALTERNATE method
            aPort.SetPolyFillMode(GDI32.ALTERNATE);
            aPort.Polygon(points);


            // Translate the x coordinates by half the screen
            for (int i = 0; i < 10; i++)
            {
                points[i].x += fSize.Width / 2;
            }

            // Now draw with WINDING method
            aPort.SetPolyFillMode(GDI32.WINDING);
            aPort.Polygon(points);
        }
    }
}
