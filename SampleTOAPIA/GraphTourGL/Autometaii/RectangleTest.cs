

namespace Autometaii
{
    using System;
    using System.Drawing;


    using NewTOAPIA.Drawing;
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    [Serializable]
    public class RectangleTest : Autometus
    {
        Size fSize;

        public RectangleTest(Size aSize)
        {
            fSize = aSize;
        }

        public override void ReceiveCommand(ICommand command)
        {
            Command_Render renderCommand = command as Command_Render;
            
            if (renderCommand == null)
                return;

            RunOnce(renderCommand.GraphPort);
        }

        public void RunOnce(IDraw2D aPort)
        {
            //aPort.SaveState();
            GPen rectPen = new GPen(Colorrefs.Red);
            GDIBrush rectBrush = new GDISolidBrush(Colorrefs.Pink);

            for (int coord = 10; coord < fSize.Height; coord += 50)
            {
                aPort.FillRectangle(rectBrush, new RectangleI(coord, coord, 200, 200));
                aPort.DrawRectangle(rectPen, new RectangleI(coord, coord, 200, 200));
            }

            //aPort.ResetState();
        }
    }
}
