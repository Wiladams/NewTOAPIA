using System;
using System.Collections.Generic;

using TOAPI.Types;
using TOAPI.GDI32;

namespace Autometaii
{
    [Serializable]
    public class GraphTest : Autometus
    {
        Size fSize;

        public GraphTest(Size aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(IRenderGDI aPort)
        {
            Rectangle[] rects = new Rectangle[4];

            rects[0] = new Rectangle(0, 10, 40, 200);
            rects[1] = new Rectangle(60, 10, 40, 300);
            rects[2] = new Rectangle(120, 10, 40, 150);
            rects[3] = new Rectangle(180, 10, 40, 400);

            aPort.SaveState();

            // Select a gray brush to draw with
            aPort.UseDefaultBrush();
            aPort.UseDefaultPen();

            aPort.SetDefaultBrushColor(RGBColor.DarkGreen);
            aPort.SetDefaultPenColor(RGBColor.Red);

            // Flip the coordinate system so 0,0 is in the lower left
            aPort.SetMappingMode(MappingModes.LOENGLISH);
            aPort.SetViewportOrigin(0, fSize.cy);

            aPort.Flush();

            for (int i = 0; i < 4; i++)
            {
                aPort.Rectangle(rects[i]);
            }
        
            
            aPort.Flush();
            aPort.RestoreState();
        
        }
    }
}
