using System;
using System.Collections.Generic;

using TOAPI.Types;

namespace Autometaii
{
    [Serializable]
    public class RectangleTest : Autometus
    {
        Size fSize;

        public RectangleTest(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            aPort.SaveState();

            aPort.UseDefaultBrush();
            aPort.UseDefaultPen();
            aPort.SetDefaultBrushColor(RGBColor.Pink);
            aPort.SetDefaultPenColor(RGBColor.Red);
            aPort.Flush();

            for (int coord = 10; coord < fSize.Height; coord += 50)
            {
                aPort.Rectangle(coord, coord, coord + 200, coord + 200);
            }

            aPort.RestoreState();
        }
    }
}
