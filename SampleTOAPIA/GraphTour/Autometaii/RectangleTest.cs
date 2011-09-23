using System;
using System.Collections.Generic;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

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

        public void RunOnce(GDIRenderer aPort)
        {
            aPort.SaveState();

            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, RGBColor.Red, Guid.NewGuid());
            GDIBrush rectBrush = new GDISolidBrush(RGBColor.Pink);

            for (int coord = 10; coord < fSize.Height; coord += 50)
            {
                aPort.FillRectangle(rectBrush, coord, coord, 200, 200);
                aPort.DrawRectangle(rectPen, coord, coord, 200, 200);
            }

            aPort.ResetState();
        }
    }
}
