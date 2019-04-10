using System;


using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class RectangleTest : Autometus
    {
        Size2I fSize;

        public RectangleTest(Size2I aSize)
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

            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Red, Guid.NewGuid());
            GDIBrush rectBrush = new GDISolidBrush(Colorrefs.Pink);

            for (int coord = 10; coord < fSize.Height; coord += 50)
            {
                aPort.FillRectangle(rectBrush, coord, coord, 200, 200);
                aPort.DrawRectangle(rectPen, coord, coord, 200, 200);
            }

            aPort.ResetState();
        }
    }
}
