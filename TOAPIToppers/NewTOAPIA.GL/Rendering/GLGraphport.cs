using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLGraphport : IGraphPort
    {
        public GraphicsInterface GI { get; set; }

        public GLGraphport(GraphicsInterface gi)
        {
            GI = gi;
        }

        // Drawing individual pixel
       public void SetPixel(int x, int y, ColorRGBA aColor)
        {
            GI.Color(aColor);

            GI.Drawing.Points.Begin();
            GI.Vertex(new float2(x, y));
            GI.Drawing.Points.End();
        }


        // Drawing Line
        public void DrawLine(IPen aPen, Point2I startPoint, Point2I endPoint)
        {
            ColorRGBA aColor = new ColorRGBA(aPen.Color);
            GI.Color(aColor);

            // If it's a cosmetic pen, deal with that
            switch (aPen.Style)
            {
                case PenStyle.DashDotDot:
                    GI.Enable(GLOption.LineStipple);
                    GI.LineStipple(1, 0x0CCF);
                    break;
            }

            GI.Drawing.Lines.Begin();
            GI.Vertex(new float2(startPoint.x, startPoint.y));
            GI.Vertex(new float2(endPoint.x, endPoint.y));
            GI.Drawing.Lines.End();

            // Turn off line stippline
            GI.Disable(GLOption.LineStipple);

        }

        public void DrawLines(IPen aPen, Point2I[] points)
        {
            ColorRGBA aColor = new ColorRGBA(aPen.Color);
            GI.Color(aColor);

            GI.Drawing.LineStrip.Begin();
            foreach (Point2I p in points)
            {
                GI.Vertex(new float2(p.x, p.y));
            }
            GI.Drawing.LineStrip.End();
        }

        // Drawing Rectangle
        public void DrawRectangle(IPen aPen, RectangleI rect)
        {
            ColorRGBA aColor = new ColorRGBA(aPen.Color);
            GI.Color(aColor);

            GI.Drawing.LineLoop.Begin();
            GI.Vertex(rect.Left, rect.Bottom);
            GI.Vertex(rect.Left, rect.Top);
            GI.Vertex(rect.Right, rect.Top);
            GI.Vertex(rect.Right, rect.Bottom);
            GI.Vertex(rect.Left, rect.Bottom);
            GI.Drawing.LineLoop.End();
        }

        public void DrawRectangles(IPen aPen, RectangleI[] rects)
        {
            ColorRGBA aColor = new ColorRGBA(aPen.Color);
            GI.Color(aColor);

            foreach (RectangleI rect in rects)
            {
                DrawRectangle(aPen, rect);
            }
        }

        public void FillRectangle(IBrush aBrush, RectangleI rect)
        {
            ColorRGBA aColor = new ColorRGBA(aBrush.Color);
            GI.Color(aColor);

            GL.Rect(rect.Left, rect.Bottom, rect.Right, rect.Top);
        }

        // Drawing Ellipse
        public void DrawEllipse(IPen aPen, RectangleI rect)
        {
        }

        public void FillEllipse(IBrush aBrush, RectangleI rect)
        {
        }

        public void DrawRoundRect(IPen aPen, RectangleI rect, int xRadius, int yRadius)
        {
        }


        // Drawing Bezier
        public void DrawBeziers(IPen aPen, Point2I[] points)
        {
        }

        // Drawing Polygon
        public void Polygon(Point2I[] points)
        {
        }

        // Drawing Paths
        public void DrawPath(IPen aPen, GPath aPath)
        {
        }

        public void FillPath(IBrush aBrush, GPath aPath)
        {
        }

        // These are nice to have, but a little complicated for a simple renderer
        //public void DrawGradientRectangle(GradientRect aRect)
        //{
        //}

        // Drawing Text
        public void DrawString(int x, int y, string aString)
        {
        }

        #region IRenderPixelBuffer
        public virtual void PixBlt(PixelArray aBitmap, int x, int y)
        {
            GI.RasterPos2i(x, y);
            GI.DrawPixels(aBitmap.Width, aBitmap.Height, aBitmap.Layout, aBitmap.ComponentType, aBitmap.Pixels);
        }
        #endregion

        #region IGraphState
        public void Flush()
        {
        }

        public void SaveState()
        { 
        }

        public void ResetState()
        {
        }

        public void RestoreState(int relative)
        {
        }

        // Setting Attributes and modes
        public void SetTextColor(uint colorref)
        {
        }

        public void SetBkColor(uint colorref)
        {
        }

        // Setting pens and brushes objects
        public void SetPen(IPen aPen)
        {
        }

        public void SetBrush(IBrush aBrush)
        {
        }

        public void SetFont(IFont aFont)
        {
        }

        //void SelectStockObject(int objectIndex);
        public void SelectUniqueObject(Guid objectID)
        {
        }

        // Setting some modes
        public void SetBkMode(int aMode)
        {
        }

        public void SetMappingMode(MappingModes aMode)
        {
        }

        public void SetPolyFillMode(PolygonFillMode aMode)
        {
        }

        public void SetROP2(BinaryRasterOps rasOp)
        {
        }

        public void SetClipRectangle(RectangleI rect)
        {
        }

        public void SetWorldTransform(Transformation wTransform)
        {
        }

        public void TranslateTransform(int dx, int dy)
        {
        }

        public void ScaleTransform(float xFactor, float yFactor)
        {
        }

        public void RotateTransform(float angle, int x, int y)
        {
        }
        #endregion
    }
}
