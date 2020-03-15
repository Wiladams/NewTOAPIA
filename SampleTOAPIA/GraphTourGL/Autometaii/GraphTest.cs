using System;
using System.Drawing;


using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    using NewTOAPIA.Drawing.GDI;

    [Serializable]
    public class GraphTest : Autometus
    {
        Size fSize;
        RectangleI[] rects;

        public GraphTest(Size aSize)
        {
            fSize = aSize;

            rects = new RectangleI[4];
            rects[0] = new RectangleI(0, 10, 40, 200);
            rects[1] = new RectangleI(60, 10, 40, 300);
            rects[2] = new RectangleI(120, 10, 40, 150);
            rects[3] = new RectangleI(180, 10, 40, 400);
        }

        public override void ReceiveCommand(ICommand command)
        {
            Command_Render renderCommand = command as Command_Render;

            if (renderCommand == null)
                return;

            RunOnce(renderCommand.GraphPort);
        }

        public void RunOnce(IGraphPort aPort)
        {

            aPort.SaveState();

            // Select a gray brush to draw with
            //aPort.UseDefaultBrush();
            //aPort.UseDefaultPen();

            //aPort.SetDefaultBrushColor(RGBColor.DarkGreen);
            //aPort.SetDefaultPenColor(RGBColor.Black);

            // Flip the coordinate system so 0,0 is in the lower left
            aPort.SetMappingMode(MappingModes.LoEnglish);
            //aPort.SetViewportOrigin(0, fSize.Width);
            aPort.ScaleTransform(1, -1);

            aPort.Flush();
            GDIPen rectPen = new GDICosmeticPen(PenStyle.Solid, Colorrefs.Red, Guid.NewGuid());
            GDIBrush rectBrush = new GDISolidBrush(Colorrefs.DarkCyan);
            for (int i = 0; i < 4; i++)
            {
                aPort.DrawRectangle(rectPen, rects[i]);
                aPort.FillRectangle(rectBrush, rects[i]);
                //aPort.StrokeAndFillRectangle(rectPen, rectBrush, rects[i]);
            }

            aPort.Flush();
            aPort.ResetState();
        
        }
    }
}
