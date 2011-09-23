using System;
using System.Drawing;

namespace NewTOAPIA.Drawing
{

    public class GraphPortDelegate : IGraphPort
    {
        #region Events
        public event NewTOAPIA.Drawing.SetPixel SetPixelHandler;

        public event NewTOAPIA.Drawing.DrawLine DrawLineHandler;
        public event NewTOAPIA.Drawing.DrawLines DrawLinesHandler;

        public event NewTOAPIA.Drawing.DrawRectangle DrawRectangleHandler;
        public event NewTOAPIA.Drawing.DrawRectangles DrawRectanglesHandler;
        public event NewTOAPIA.Drawing.FillRectangle FillRectangleHandler;

        public event NewTOAPIA.Drawing.DrawEllipse DrawEllipseHandler;
        public event NewTOAPIA.Drawing.FillEllipse FillEllipseHandler;

        public event NewTOAPIA.Drawing.DrawRoundRect DrawRoundRectHandler;

        public event NewTOAPIA.Drawing.Polygon PolygonHandler;
        public event NewTOAPIA.Drawing.DrawBeziers DrawBeziersHandler;

        // Gradient fills
        //public event NewTOAPIA.Drawing.DrawGradientRectangle DrawGradientRectangleHandler;

        // Drawing Text
        public event NewTOAPIA.Drawing.DrawString DrawStringHandler;

        /// Draw bitmaps
        public event NewTOAPIA.Drawing.PixBlt PixBltHandler;
        //public event NewTOAPIA.Drawing.PixmapShardBlt PixmapShardBltHandler;
        //public event NewTOAPIA.Drawing.AlphaBlend AlphaBlendHandler;

        // Path drawing
        public event NewTOAPIA.Drawing.FillPath FillPathHandler;
        public event NewTOAPIA.Drawing.DrawPath DrawPathHandler;
        //public event NewTOAPIA.Drawing.SetPathAsClipRegion SetPathAsClipRegionHandler;



        // Setting some objects
        public event NewTOAPIA.Drawing.SetPen SetPenHandler;
        public event NewTOAPIA.Drawing.SetBrush SetBrushHandler;
        public event NewTOAPIA.Drawing.SetFont SetFontHandler;

        //public event NewTOAPIA.Drawing.SelectStockObject SelectStockObjectHandler;
        public event NewTOAPIA.Drawing.SelectUniqueObject SelectUniqueObjectHandler;

        // State Management
        public event NewTOAPIA.Drawing.Flush FlushHandler;
        public event NewTOAPIA.Drawing.SaveState SaveStateHandler;
        public event NewTOAPIA.Drawing.ResetState ResetStateHandler;
        public event NewTOAPIA.Drawing.RestoreState RestoreStateHandler;

        // Setting Attributes and modes
        public event NewTOAPIA.Drawing.SetTextColor SetTextColorHandler;
        public event NewTOAPIA.Drawing.SetBkColor SetBkColorHandler;

        // Setting some modes
        public event NewTOAPIA.Drawing.SetBkMode SetBkModeHandler;
        public event NewTOAPIA.Drawing.SetMappingMode SetMappingModeHandler;
        public event NewTOAPIA.Drawing.SetPolyFillMode SetPolyFillModeHandler;
        public event NewTOAPIA.Drawing.SetROP2 SetROP2Handler;

        public event NewTOAPIA.Drawing.SetClipRectangle SetClipRectangleHandler;

        public event NewTOAPIA.Drawing.SetWorldTransform SetWorldTransformHandler;
        public event NewTOAPIA.Drawing.TranslateTransform TranslateTransformHandler;
        public event NewTOAPIA.Drawing.ScaleTransform ScaleTransformHandler;
        public event NewTOAPIA.Drawing.RotateTransform RotateTransformHandler;
        #endregion


        public GraphPortDelegate()
        {
        }

        public void AddGraphPort(IGraphPort aPort)
        {
            SetPixelHandler += new NewTOAPIA.Drawing.SetPixel(aPort.SetPixel);

            DrawLineHandler += new DrawLine(aPort.DrawLine);
            DrawLinesHandler += new DrawLines(aPort.DrawLines);

            DrawRectangleHandler += new NewTOAPIA.Drawing.DrawRectangle(aPort.DrawRectangle);
            DrawRectanglesHandler += new NewTOAPIA.Drawing.DrawRectangles(aPort.DrawRectangles);
            FillRectangleHandler += new NewTOAPIA.Drawing.FillRectangle(aPort.FillRectangle);

            DrawEllipseHandler += new NewTOAPIA.Drawing.DrawEllipse(aPort.DrawEllipse);
            FillEllipseHandler += new NewTOAPIA.Drawing.FillEllipse(aPort.FillEllipse);

            DrawRoundRectHandler += new NewTOAPIA.Drawing.DrawRoundRect(aPort.DrawRoundRect);

            PolygonHandler += new NewTOAPIA.Drawing.Polygon(aPort.Polygon);
            DrawBeziersHandler += new NewTOAPIA.Drawing.DrawBeziers(aPort.DrawBeziers);
            
            DrawPathHandler += new NewTOAPIA.Drawing.DrawPath(aPort.DrawPath);
            FillPathHandler += new FillPath(aPort.FillPath);

            //// Gradient fills
            //DrawGradientRectangleHandler += new NewTOAPIA.Drawing.DrawGradientRectangle(aPort.DrawGradientRectangle);

            //// Drawing Text
            DrawStringHandler += new NewTOAPIA.Drawing.DrawString(aPort.DrawString);

            ///// Draw bitmaps
            PixBltHandler += new NewTOAPIA.Drawing.PixBlt(aPort.PixBlt);
            //PixmapShardBltHandler += new NewTOAPIA.Drawing.PixmapShardBlt(aPort.PixmapShardBlt);
            //AlphaBlendHandler += new NewTOAPIA.Drawing.AlphaBlend(aPort.AlphaBlend);

            // Path handling
            //DrawPathHandler += new NewTOAPIA.Drawing.DrawPath(aPort.DrawPath);
            //SetPathAsClipRegionHandler += new NewTOAPIA.Drawing.SetPathAsClipRegion(aPort.SetPathAsClipRegion);

            //// Setting some objects
            SetPenHandler += new NewTOAPIA.Drawing.SetPen(aPort.SetPen);
            SetBrushHandler += new SetBrush(aPort.SetBrush);
            SetFontHandler += new SetFont(aPort.SetFont);

            //SelectStockObjectHandler += new NewTOAPIA.Drawing.SelectStockObject(aPort.SelectStockObject);
            SelectUniqueObjectHandler += new NewTOAPIA.Drawing.SelectUniqueObject(aPort.SelectUniqueObject);

            //// State Management
            FlushHandler += new NewTOAPIA.Drawing.Flush(aPort.Flush);
            SaveStateHandler += new NewTOAPIA.Drawing.SaveState(aPort.SaveState);
            ResetStateHandler += new NewTOAPIA.Drawing.ResetState(aPort.ResetState);
            RestoreStateHandler += new NewTOAPIA.Drawing.RestoreState(aPort.RestoreState);

            //// Setting Attributes and modes
            SetTextColorHandler += new NewTOAPIA.Drawing.SetTextColor(aPort.SetTextColor);

            //// Setting some modes
            SetBkColorHandler += new NewTOAPIA.Drawing.SetBkColor(aPort.SetBkColor);
            SetBkModeHandler += new NewTOAPIA.Drawing.SetBkMode(aPort.SetBkMode);

            SetMappingModeHandler += new NewTOAPIA.Drawing.SetMappingMode(aPort.SetMappingMode);
            SetPolyFillModeHandler += new NewTOAPIA.Drawing.SetPolyFillMode(aPort.SetPolyFillMode);
            SetROP2Handler += new NewTOAPIA.Drawing.SetROP2(aPort.SetROP2);

            SetClipRectangleHandler += new SetClipRectangle(aPort.SetClipRectangle);

            // World transform management
            SetWorldTransformHandler += new NewTOAPIA.Drawing.SetWorldTransform(aPort.SetWorldTransform);
            TranslateTransformHandler += new TranslateTransform(aPort.TranslateTransform);
            ScaleTransformHandler += new ScaleTransform(aPort.ScaleTransform);
            RotateTransformHandler += new RotateTransform(aPort.RotateTransform);
        }

        #region Drawing Primitives
        public virtual void SetPixel(int x, int y, System.Drawing.Color colorref)
        {
            if (null != SetPixelHandler)
                SetPixelHandler(x, y, colorref);
        }

        public virtual void DrawLine(IPen aPen, System.Drawing.Point startPoint, System.Drawing.Point endPoint)
        {
            if (null != DrawLineHandler)
                DrawLineHandler(aPen, startPoint, endPoint);
        }

        public virtual void DrawLines(IPen aPen, Point[] points)
        {
            if (null != DrawLinesHandler)
                DrawLinesHandler(aPen, points);
        }

        public virtual void DrawRectangle(IPen aPen, Rectangle rect)
        {
            if (null != DrawRectangleHandler)
                DrawRectangleHandler(aPen, rect);
        }

        public virtual void DrawRectangles(IPen aPen, Rectangle[] rects)
        {
            if (null != DrawRectanglesHandler)
                DrawRectanglesHandler(aPen, rects);
        }


        public virtual void FillRectangle(IBrush aBrush, Rectangle rect)
        {
            if (null != FillRectangleHandler)
                FillRectangleHandler(aBrush, rect);
        }

        // Gradient Fills
        //public virtual void DrawGradientRectangle(GradientRect aGradient)
        //{
        //    if (null != DrawGradientRectangleHandler)
        //        DrawGradientRectangleHandler(aGradient);
        //}

        /// DrawEllipse
        /// 
        public virtual void DrawEllipse(IPen aPen, Rectangle rect)
        {
            if (null != DrawEllipseHandler)
                DrawEllipseHandler(aPen, rect);
        }

        public virtual void FillEllipse(IBrush aBrush, Rectangle rect)
        {
            if (null != FillEllipseHandler)
                FillEllipseHandler(aBrush, rect);
        }

        public virtual void DrawString(int x, int y, string aString)
        {
            if (null != DrawStringHandler)
                DrawStringHandler(x, y, aString);
        }


        public virtual void DrawBeziers(IPen aPen, Point[] points)
        {
            if (null != DrawBeziersHandler)
                DrawBeziersHandler(aPen, points);
        }

        public virtual void DrawPath(IPen aPen, GPath aPath)
        {
            if (null != DrawPathHandler)
                DrawPathHandler(aPen, aPath);
        }

        public virtual void FillPath(IBrush aBrush, GPath aPath)
        {
            if (null != FillPathHandler)
                FillPathHandler(aBrush, aPath);
        }

        // Draw a Polygon
        public virtual void Polygon(System.Drawing.Point[] points)
        {
            if (null != PolygonHandler)
                PolygonHandler(points);
        }

        #endregion

        #region Drawing Pixmaps
        // Generalized bit block transfer
        // Can transfer from any device context to this one.
        public virtual void PixBlt(PixelArray pixBuff, int x, int y)
        {
            if (null != PixBltHandler)
                PixBltHandler(pixBuff, x, y);
        }

        //public virtual void PixmapShardBlt(IPixelArray pixmap, Rectangle srcRect, Rectangle dstRect)
        //{
        //    if (null != PixmapShardBltHandler)
        //        PixmapShardBltHandler(pixmap, srcRect, dstRect);
        //}

        //public virtual void AlphaBlend(int x, int y, int width, int height,
        //        GDIPixmap bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
        //        byte alpha)
        //{
        //    if (null != AlphaBlendHandler)
        //        AlphaBlendHandler(x, y, width, height,
        //            bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        //}

        //public virtual void ScaleBitmap(GDIPixmap aBitmap, Rectangle aFrame)
        //{
        //    AlphaBlend(aFrame.Left, aFrame.Top, aFrame.Width, aFrame.Height,
        //        aBitmap, 0, 0, aBitmap.Width, aBitmap.Height, 255);
        //}

        //public virtual void DrawImage(GDIPixmap bitmap, System.Drawing.Point[] destinationParallelogram, System.Drawing.Rectangle srcRect, System.Drawing.GraphicsUnit units)
        //{
        //}
        #endregion

        #region State Management
        // Modes and attributes
        public virtual void SetTextColor(uint colorref)
        {
            if (SetTextColorHandler != null)
                SetTextColorHandler(colorref);
        }



        /// Some GraphPort control things
        /// 

        public virtual void SaveState()
        {
            if (null != SaveStateHandler)
                SaveStateHandler();
        }

        /// <summary>
        /// Pop all the current state information back to the defaults
        /// </summary>
        public virtual void ResetState()
        {
            if (null != ResetStateHandler)
                ResetStateHandler();
        }

        public virtual void RestoreState(int relative)
        {
            if (null != RestoreStateHandler)
                RestoreStateHandler(relative);
        }

        public virtual void Flush()
        {
            if (null != FlushHandler)
                FlushHandler();
        }


        public virtual void SetROP2(BinaryRasterOps rasOp)
        {
            if (null != SetROP2Handler)
                SetROP2Handler(rasOp);
        }

        public virtual void SetFont(IFont aFont)
        {
            if (null != SetFontHandler)
                SetFontHandler(aFont);
        }

        public virtual void SetPen(IPen aPen)
        {
            if (null != SetPenHandler)
                SetPenHandler(aPen);
        }

        public virtual void SetBrush(IBrush aBrush)
        {
            if (null != SetBrushHandler)
                SetBrushHandler(aBrush);
        }

        public virtual void SelectUniqueObject(Guid objectID)
        {
            if (null != SelectUniqueObjectHandler)
                SelectUniqueObjectHandler(objectID);
        }

        //public virtual void SelectStockObject(int objIndex)
        //{
        //    if (null != SelectStockObjectHandler)
        //        SelectStockObjectHandler(objIndex);
        //}

        public virtual void DrawRoundRect(IPen aPen, Rectangle rect, int xRadius, int yRadius)
        {
            if (null != DrawRoundRectHandler)
                DrawRoundRectHandler(aPen, rect, xRadius, yRadius);
        }
        #endregion

        #region Managing Modes

        public virtual void SetMappingMode(MappingModes aMode)
        {
            if (null != SetMappingModeHandler)
                SetMappingModeHandler(aMode);
        }

        public virtual void SetBkColor(uint colorref)
        {
            if (null != SetBkColorHandler)
                SetBkColorHandler(colorref);
        }

        public virtual void SetBkMode(int bkMode)
        {
            if (null != SetBkModeHandler)
                SetBkModeHandler(bkMode);
        }

        public virtual void SetPolyFillMode(PolygonFillMode aMode)
        {
            if (null != SetPolyFillModeHandler)
                SetPolyFillModeHandler(aMode);
        }
        #endregion

        public virtual void SetClipRectangle(Rectangle clipRect)
        {
            if (null != SetClipRectangleHandler)
                SetClipRectangleHandler(clipRect);
        }

        #region Viewport Management


        public virtual void SetWorldTransform(Transformation aTransform)
        {
            if (null != SetWorldTransformHandler)
                SetWorldTransformHandler(aTransform);
        }

        public virtual void RotateTransform(float angle, int x, int y)
        {
            if (null != RotateTransformHandler)
                RotateTransformHandler(angle, x, y);
        }

        public virtual void ScaleTransform(float scaleX, float scaleY)
        {
            if (null != ScaleTransformHandler)
                ScaleTransformHandler(scaleX, scaleY);
        }

        public virtual void TranslateTransform(int dx, int dy)
        {
            if (null != TranslateTransformHandler)
                TranslateTransformHandler(dx, dy);
        }
#endregion

    }
}