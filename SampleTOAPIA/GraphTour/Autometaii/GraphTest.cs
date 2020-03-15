using System;

using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class GraphTest : Autometus
    {
        Size2I fSize;
        RectangleI[] rects;

        public GraphTest(Size2I aSize)
        {
            fSize = aSize;

            rects = new RectangleI[4];
            rects[0] = new RectangleI(0, 10, 40, 200);
            rects[1] = new RectangleI(60, 10, 40, 300);
            rects[2] = new RectangleI(120, 10, 40, 150);
            rects[3] = new RectangleI(180, 10, 40, 400);
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {

            aPort.SaveState();

            // Select a gray brush to draw with
            aPort.UseDefaultBrush();
            aPort.UseDefaultPen();

            aPort.SetDefaultBrushColor(Colorrefs.DarkGreen);
            aPort.SetDefaultPenColor(Colorrefs.Black);

            // Flip the coordinate system so 0,0 is in the lower left
            aPort.SetMappingMode(MappingModes.LoEnglish);
            //aPort.SetViewportOrigin(0, fSize.Width);
            aPort.ScaleTransform(1, -1);

            aPort.Flush();
            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Red, Guid.NewGuid());
            GDIBrush rectBrush = new GDISolidBrush(Colorrefs.DarkCyan);
            for (int i = 0; i < 4; i++)
            {

                //aPort.DrawRectangle(rectPen, rects[i].Left, rects[i].Top, rects[i].Width, rects[i].Height);
                aPort.StrokeAndFillRectangle(rectPen, rectBrush, rects[i]);
            }

            aPort.Flush();
            aPort.ResetState();
        
        }
    }
}
