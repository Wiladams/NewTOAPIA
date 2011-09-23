using System;
using System.Drawing;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

namespace Autometaii
{
    using NewTOAPIA;

    [Serializable]
    public class LineDemo1 : Autometus
    {
        Size fSize;

        public LineDemo1(Size aSize)
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

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IDraw2D aPort)
        {
            int cxClient = fSize.Width;
            int cyClient = fSize.Height;

            //aPort.SaveState();

            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, RGBColor.Black, Guid.NewGuid());
            GDISolidBrush rectBrush = new GDISolidBrush(RGBColor.White);

            // Do a rectangle
            Rectangle rect = Rectangle.FromLTRB(cxClient / 8, cyClient / 8,
                (7 * cxClient / 8), (7 * cyClient / 8));
            aPort.FillRectangle(rectBrush, rect);
            aPort.DrawRectangle(rectPen, rect);

            // Now do a couple of lines using a dash/dot/dot pen
            GDIPen aPen = new GDIPen(PenType.Cosmetic, PenStyle.DashDotDot, PenJoinStyle.Round, PenEndCap.Round, RGBColor.Black, 1, Guid.NewGuid());
            aPort.DrawLine(aPen, new Point(0, 0), new Point(cxClient, cyClient));
            aPort.DrawLine(aPen, new Point(0, cyClient), new Point(cxClient, 0));

            // Now an ellipse
            aPort.DrawEllipse(aPen, rect);

            // Last, a rounded rectangle
            Rectangle rRect = Rectangle.FromLTRB(cxClient / 4, cyClient / 4,
                3 * cxClient / 4, 3 * cyClient / 4);
            aPort.DrawRoundRect(aPen, rRect, cxClient / 4, cyClient / 4);

            //aPort.ResetState();
  
        }
    }
}
