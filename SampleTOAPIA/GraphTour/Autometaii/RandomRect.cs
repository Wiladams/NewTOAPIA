using System;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class RandomRect : Autometus
    {
        Size2I fSize;
        GDIPen framePen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Black, Guid.NewGuid());

        public RandomRect(Size2I aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            int maxWidth = 320; //  fSize.cx - 10 - 10;
            int maxHeight = 240; // fSize.cy - 10 - 10;


            Random rnd = new Random();

            aPort.SaveState();

            aPort.Flush();

            for (int i = 0; i < 1000; i++)
            {
                GDIBrush aBrush = new GDISolidBrush(Colorref.FromRGB((byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255), (byte)rnd.Next(0, 255)));

                int width = rnd.Next(10, maxWidth);
                int height = rnd.Next(10, maxHeight);
                int orgX = rnd.Next(10, fSize.Width-width);
                int orgY = rnd.Next(10, fSize.Height-height);

                // Fill the rectangle
                aPort.FillRectangle(aBrush, orgX, orgY, width, height);

                // Frame the rectangle
                aPort.DrawRectangle(framePen, orgX, orgY, width, height);

                aBrush.Dispose();
            }
            aPort.Flush();


            aPort.ResetState();
        }
    }
}
