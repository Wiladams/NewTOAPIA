﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{
    using TOAPI.GDI32;

    using NewTOAPIA.Graphics;
    using NewTOAPIA.Drawing.GDI;

    public class PGDIRenderer : PRenderer
    {
        GDIRenderer Graphics { get; set; }
        IPen CurrentPen { get; set; }
        IBrush CurrentBrush { get; set; }
        IBrush HollowBrush { get; set; }

        public PGDIRenderer(GDIContext gdi)
        {
            Graphics = new GDIRenderer(gdi);

            HollowBrush = new GDIBrush(BrushStyle.Hollow, HatchStyle.Horizontal, (Colorref)0, Guid.NewGuid());

            Graphics.UseDefaultPen();
            Graphics.UseDefaultBrush();
        }

        Colorref GetColorref(color c)
        {
            return Colorref.FromRGB(c.red, c.green, c.blue);
        }

        public override void Resize(int w, int h)
        {
        }

        public override void SetBrush(IBrush brush)
        {
            if (brush.BrushStyle == BrushStyle.Hollow)
            {
                Graphics.SetBrush(HollowBrush);
                return;
            }

            Graphics.UseDefaultBrush();
            Graphics.SetDefaultBrushColor(brush.Color);
        }

        public override void SetPen(IPen pen)
        {
            if (pen.Width <= 1)
            {
                Graphics.UseDefaultPen();
                Graphics.SetDefaultPenColor(pen.Color);
                return;
            }

            CurrentPen = new GDIPen(PenType.Geometric, PenStyle.Solid, pen.JoinStyle, pen.EndCap,
                pen.Color, pen.Width, Guid.NewGuid());
            Graphics.SetPen(CurrentPen);
        }

        public override void ClearToColor(color c)
        {
            Size2I size = Graphics.DeviceContext.SizeInPixels;
            Graphics.UseDefaultBrush();
            Graphics.SetDefaultBrushColor(GetColorref(c));
            Graphics.DeviceContext.Rectangle(0, 0, size.width, size.height);
        }

        #region Pixel
        public override void SetPixel(float x, float y, color c)
        {
            Graphics.DeviceContext.SetPixel((int)x, (int)y, GetColorref(c)); 
        }
        #endregion

        public override void Bezier(int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2)
        {
            Point2I[] pts = { 
                                new Point2I(x1,y1), 
                                new Point2I(cx1, cy1),
                                new Point2I(cx2, cy2),
                                new Point2I(x2, y2)};

            Graphics.Beziers(pts);
        }

        public override void Arc(float x, float y, float width, float height, float startRadians, float endRadians)
        {
            int dx = (int)Math.Round(width);
            int dy = (int)Math.Round(height);
            int radius = (int)Math.Round(width / 2);

            int centerX = (int)Math.Round(x+radius);
            int centerY = (int)Math.Round(y+radius);

            //float startDegrees = startRadians;
            //float sweepDegrees = endRadians - startRadians;

            float startDegrees = PLang.degrees(startRadians);
            float sweepDegrees = PLang.degrees(endRadians - startRadians);

            Graphics.DeviceContext.MoveTo(centerX, centerY);
            //Graphics.DeviceContext.SetArcDirection(GDI32.AD_CLOCKWISE);
            Graphics.DeviceContext.SetArcDirection(GDI32.AD_COUNTERCLOCKWISE);
            Graphics.DeviceContext.AngleArc(centerX, centerY, radius, startDegrees, sweepDegrees);
        }

        public override void Ellipse(int left, int top, int right, int bottom)
        {
            Graphics.Ellipse(left, top, right, bottom);
        }

        #region Line
        public override void Line(float x1, float y1, float x2, float y2)
        {
            int x0 = (int)Math.Round(x1);
            int y0 = (int)Math.Round(y1);

            int x = (int)Math.Round(x2);
            int y = (int)Math.Round(y2);

            Graphics.DeviceContext.MoveTo(x0, y0);
            Graphics.DeviceContext.LineTo(x, y);
        }

        public override void PolyLine(Point2I[] pts)
        {
            Graphics.PolyLine(pts);
        }
        #endregion

        #region Triangle
        public override void StrokeAndFillTriangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
            Point2I[] pts = {
                            new Point2I((int)Math.Round(x1),(int)Math.Round(y1)),
                            new Point2I((int)Math.Round(x2),(int)Math.Round(y2)),
                            new Point2I((int)Math.Round(x3),(int)Math.Round(y3))};

            Graphics.Polygon(pts);
        }
        #endregion

        #region Rectangle
        public override void Rectangle(float x, float y, float width, float height)
        {
            Graphics.DeviceContext.Rectangle((int)x, (int)y, (int)(x + width), (int)(y + height));
        }
        #endregion

        #region Polygon
        public override void Polygon(Point2I[] pts)
        {
            Graphics.Polygon(pts);
        }
        #endregion
    }
}
