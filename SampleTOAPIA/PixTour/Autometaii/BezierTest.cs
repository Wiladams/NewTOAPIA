using System;
using System.Collections.Generic;

using TOAPI.Types;
using TOAPI.GDI32;

namespace Autometaii
{
    [Serializable]
    public class BezierTest : Autometus
    {
        Size fSize;
        Point[] fFigure;

        public BezierTest(Size aSize)
        {
            fSize = aSize;

            fFigure = new Point[4];
            fFigure[0] = new Point(fSize.Width/4,fSize.Height/2);
            fFigure[1] = new Point(fSize.Width/2,fSize.Height/4);
            fFigure[2] = new Point(fSize.Width/2,3*fSize.Height/4);
            fFigure[3] = new Point(3*fSize.Width/4,fSize.Height/2);
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            aPort.SaveState();

            aPort.SetDefaultPenColor(RGBColor.Black);
            aPort.UseDefaultPen();

            aPort.SetMappingMode(MappingModes.TEXT);
            aPort.PolyBezier(fFigure);

            // Draw the control lines
            aPort.SetDefaultPenColor(RGBColor.Red);
            aPort.MoveTo(fFigure[0]);
            aPort.LineTo(fFigure[1]);

            aPort.MoveTo(fFigure[2]);
            aPort.LineTo(fFigure[3]);
            aPort.Flush();
            aPort.RestoreState();
        }
    }
}
