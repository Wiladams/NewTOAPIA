using System;

using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;

namespace Autometaii
{
    [Serializable]
    public class PathTest : Autometus
    {
        Size2I fSize;

        public PathTest(Size2I aSize)
        {
            fSize = aSize;
        }

        public void ReceiveCommand(Command_Render command)
        {
            RunOnce(command.GraphPort);
        }

        public void RunOnce(GDIRenderer aPort)
        {
            // Do some path stuff
            GPath aPath = new GPath();
            
            aPath.Begin();
            aPath.MoveTo(10, 10, false);
            aPath.LineTo(10, 100, false);
            aPath.LineTo(100, 100, true);
            aPath.End();

            GDIBrush pathBrush = new GDIBrush(BrushStyle.Solid, HatchStyle.BDiagonal, Colorrefs.Cyan, Guid.NewGuid());
            aPort.FillPath(pathBrush, aPath);

            GDIPen pathPen = new GDIPen(PenType.Geometric, 
                PenStyle.Solid, 
                PenJoinStyle.Round, 
                PenEndCap.Round, 
                Colorrefs.Black, 
                10, 
                Guid.NewGuid());
            //aPort.DrawPath(pathPen, aPath);

            // Now use a GDIPath
            aPort.SetBkMode((int)BackgroundMixMode.Transparent);
            aPort.SetTextColor(Colorrefs.Black);

            GDIFont aFont = new GDIFont("Impact", 96, Guid.NewGuid());

            GDIContext dc = aPort.DeviceContext;
            GDIPath gdipath = new GDIPath(dc, Guid.NewGuid());
            gdipath.Begin();
            aPort.SetFont(aFont);
            aPort.DrawString(200, 200, "The Scaled Text");
            gdipath.End();
            aPort.Flush();

            // First fill the text
            aPort.FillPath(pathBrush, gdipath);

            // Then stroke the path around it
            GDIPen textPen = new GDIPen(PenType.Geometric,
                PenStyle.Solid,
                PenJoinStyle.Round,
                PenEndCap.Round,
                Colorrefs.Black,
                2,
                Guid.NewGuid());
            aPort.DrawPath(textPen, gdipath);
            aPort.Flush();

        }
    }
}
