using System;
using System.Collections.Generic;

using TOAPI.Types;
using TOAPI.GDI32;

namespace Autometaii
{
    [Serializable]
    public class LineDemo1 : Autometus
    {
        Size fSize;

        public LineDemo1(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            int cxClient = fSize.cx;
            int cyClient = fSize.cy;

            aPort.SaveState();
            aPort.Flush();

            aPort.SetMappingMode(MappingModes.LOENGLISH);
            aPort.UseDefaultPen();
            aPort.UseDefaultBrush();
            aPort.SetDefaultBrushColor(RGBColor.Blue);
            aPort.SetDefaultPenColor(RGBColor.Yellow);
            aPort.Flush();

            aPort.Rectangle(cxClient / 8, cyClient / 8,
                (7 * cxClient / 8), (7 * cyClient / 8));

            Pen aPen = aPort.CreatePen(GDI32.PS_DASHDOTDOT, 1, RGBColor.Blue, Guid.NewGuid());

            aPort.SetPen(aPen);
            aPort.MoveTo(0, 0);
            aPort.LineTo(cxClient, cyClient);

            aPort.MoveTo(0, cyClient);
            aPort.LineTo(cxClient, 0);

            aPort.Ellipse(cxClient / 8, cyClient / 8,
                (7 * cxClient / 8), (7 * cyClient / 8));

            aPort.RoundRect(cxClient / 4, cyClient / 4,
                3 * cxClient / 4, 3 * cyClient / 4,
                cxClient / 4, cyClient / 4);

            aPort.RestoreState();
  
        }
    }
}
