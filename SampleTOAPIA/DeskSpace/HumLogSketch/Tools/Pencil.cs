using System;

using TOAPI.Types;
using TOAPI.GDI32;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch.Tools
{
    public class Pencil : StrokeTool
    {
        uint fColor;
        Pen fPen;
        RECT fDrawingBounds;

        public Pencil(uint colorref)
        {
            fColor = colorref;
            //fPen = new Pen(Guid.NewGuid(), colorref, PenStyle.Solid, PenType.Cosmetic, PenJoinStyle.Round, PenEndCap.Round, 1);
            fPen = new CosmeticPen(PenStyle.Solid, fColor, Guid.NewGuid());
            fDrawingBounds = RECT.Empty;
        }

        public override void OnStrokeFinished(TOAPI.Types.Point[] points)
        {
            base.OnStrokeFinished(points);

            GraphPort.UseDefaultPen();
            GraphPort.SetDefaultPenColor(fColor);
            
            GraphPort.PolyLine(points);
            fDrawingBounds.Inset(-1, -1);
            UIWindow.Invalidate(fDrawingBounds);
        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            GraphPort.SelectStockObject(GDI32.BLACK_PEN);
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            int lastX = xMouse;
            int lastY = yMouse;

            base.OnMouseMove(e);

            if (IsTracking)
            {
                UIWindow.GraphPort.Line(lastX, lastY, xMouse, yMouse);

                // Get the rect associated with the last line segment
                RECT segmentBounds = RECT.FromExtents(lastX, lastY, xMouse, yMouse);

                // Union that segment with our existing overall bounds
                if (RECT.Empty == fDrawingBounds)
                    fDrawingBounds = segmentBounds;

                fDrawingBounds.Union(segmentBounds);

                // Make sure what we are drawing is displayed
                UIWindow.RefreshDisplay(segmentBounds);
            }
        }
    }
}
