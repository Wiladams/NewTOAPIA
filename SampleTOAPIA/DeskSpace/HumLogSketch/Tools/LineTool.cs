using System;

using TOAPI.Types;
using TOAPI.GDI32;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch.Tools
{
    public class LineTool : Digitizer
    {
        uint fColor;
        //int fWidth;
        Pen fPen;

        public LineTool(uint colorref)
            :base(2)
        {
            fColor = colorref;
            fPen = new CosmeticPen(PenStyle.Solid, fColor, Guid.NewGuid());

            base.PointsAvailableEvent += new OnPointArrayToolFinished(DigitizerFinished);
        }

        public virtual void DigitizerFinished(Point[] points)
        {
            //GraphPort.SelectUniqueObject(fPen.UniqueID);
            
            // Create the pen and select it
            Guid aGuid = Guid.NewGuid();
            GraphPort.CreateCosmeticPen(PenStyle.Solid, fColor, aGuid);
            GraphPort.SelectUniqueObject(aGuid);


            GraphPort.PolyLine(points);
            RECT drawingBounds = RECT.FromExtents(points[0].x, points[0].y, points[1].x, points[1].y);

            UIWindow.Invalidate(drawingBounds);
        }

        protected override void OnGraphPortSet()
        {
            GraphPort.CreateCosmeticPen(PenStyle.Solid, fColor, fPen.UniqueID);
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            // start a line at the starting point
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Erase the old line and place a new line at the current
            // position.
        }
    }
}
