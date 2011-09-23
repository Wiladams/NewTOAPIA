using System;
using System.Collections.Generic;
using System.Drawing;


using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

namespace Autometaii
{
    [Serializable]
    public class GraphTest : Autometus
    {
        Size fSize;
        Rectangle[] rects;

        public GraphTest(Size aSize)
        {
            fSize = aSize;

            rects = new Rectangle[4];
            rects[0] = new Rectangle(0, 10, 40, 200);
            rects[1] = new Rectangle(60, 10, 40, 300);
            rects[2] = new Rectangle(120, 10, 40, 150);
            rects[3] = new Rectangle(180, 10, 40, 400);
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

            aPort.SetDefaultBrushColor(RGBColor.DarkGreen);
            aPort.SetDefaultPenColor(RGBColor.Black);

            // Flip the coordinate system so 0,0 is in the lower left
            aPort.SetMappingMode(MappingModes.LoEnglish);
            //aPort.SetViewportOrigin(0, fSize.Width);
            aPort.ScaleTransform(1, -1);

            aPort.Flush();
            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, RGBColor.Red, Guid.NewGuid());
            GDIBrush rectBrush = new GDISolidBrush(RGBColor.DarkCyan);
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
