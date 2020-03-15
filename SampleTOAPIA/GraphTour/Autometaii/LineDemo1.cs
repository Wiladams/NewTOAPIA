using System;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class LineDemo1 : Autometus
    {
        Size2I fSize;

        public LineDemo1(Size2I aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            int cxClient = fSize.Width;
            int cyClient = fSize.Height;

            aPort.SaveState();

            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Black, Guid.NewGuid());
            GDISolidBrush rectBrush = new GDISolidBrush(Colorrefs.White);

            // Do a rectangle
            RectangleI rect = RectangleI.FromLTRB(cxClient / 8, cyClient / 8,
                (7 * cxClient / 8), (7 * cyClient / 8));
            aPort.FillRectangle(rectBrush, rect.Left, rect.Top, rect.Width, rect.Height);
            aPort.DrawRectangle(rectPen, rect.Left, rect.Top, rect.Width, rect.Height);

            // Now do a couple of lines using a dash/dot/dot pen
            GDIPen aPen = new GDIPen(PenType.Cosmetic, PenStyle.DashDotDot, PenJoinStyle.Round, PenEndCap.Round, Colorrefs.Black, 1, Guid.NewGuid());
            aPort.DrawLine(aPen, new Point2I(0, 0), new Point2I(cxClient, cyClient));
            aPort.DrawLine(aPen, new Point2I(0, cyClient), new Point2I(cxClient, 0));

            // Now an ellipse
            aPort.DrawEllipse(aPen, rect);

            // Last, a rounded rectangle
            RectangleI rRect = RectangleI.FromLTRB(cxClient / 4, cyClient / 4,
                3 * cxClient / 4, 3 * cyClient / 4);
            aPort.DrawRoundRect(aPen, rRect, cxClient / 4, cyClient / 4);

            aPort.ResetState();
  
        }
    }
}
