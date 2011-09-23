using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Processing
{

    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    abstract public class PRenderer
    {
        #region Attributes
        public float strokeWidth { get; set; }

        public abstract void SetBrush(IBrush brush);
        public abstract void SetPen(IPen pen);
        #endregion

        public abstract void Resize(int w, int h);

        public abstract void ClearToColor(color c);

        public abstract void SetPixel(float x, float y, color c);

        public abstract void Line(float x1, float y1, float x2, float y2);
        public abstract void PolyLine(Point2I[] pts);

        public abstract void Bezier(int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2);

        public abstract void Ellipse(int left, int top, int right, int bottom);

        public abstract void Arc(float x, float y, float width, float height, float startRadians, float endRadians);

        // Triangle
        public abstract void StrokeAndFillTriangle(float x1, float y1, float x2, float y2, float x3, float y3);

        public abstract void Rectangle(float x, float y, float width, float height);
        //public abstract void StrokeRectangle(float x, float y, float width, float height);
        //public abstract void FillRectangle(float x, float y, float width, float height, color fillColor);
        //public abstract void StrokeAndFillRectangle(float x, float y, float width, float height, color fillColor);

        // Polygon
        public abstract void Polygon(Point2I[] pts);
    }
}
