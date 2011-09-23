using System;
using System.Collections.Generic;

using System.Drawing;
using System.Drawing.Drawing2D;


namespace NewTOAPIA.Drawing
{


    public class GDIPlusRenderer : IGraphPort
    {
        Graphics fDeviceContext;
        Dictionary<Guid, Object> fObjectDictionary;
        
        System.Drawing.Drawing2D.GraphicsState fSavedState;
        System.Drawing.Drawing2D.GraphicsContainer fCurrentContainer;

        System.Drawing.Pen fCurrentPen;
        System.Drawing.Brush fCurrentBrush;
        System.Drawing.Font fCurrentFont;

        public GDIPlusRenderer(Graphics devContext)
        {
            fObjectDictionary = new Dictionary<Guid, Object>();
            fDeviceContext = devContext;
        
            // Set some default state
            fCurrentBrush = Brushes.White;
            fCurrentPen = Pens.Black;
            fCurrentFont = new System.Drawing.Font(FontFamily.GenericMonospace, 12.0f);
        }

        //public virtual void CreateBrush(BrushStyle aStyle, HatchStyle hatch, uint colorref, Guid uniqueID)
        //{
        //    GDISolidBrush aBrush = new GDISolidBrush(colorref);
        //    fObjectDictionary.Add(uniqueID, aBrush);
        //}

        //public virtual void CreateFont(string faceName, int height, Guid aGuid)
        //{
        //    System.Drawing.Font aFont = new System.Drawing.Font(faceName, (float)height);
        //    fObjectDictionary.Add(aGuid, aFont);
        //}

        //public virtual void CreateCosmeticPen(PenStyle aStyle, uint color, Guid uniqueID)
        //{
            
        //    System.Drawing.Pen aPen = new System.Drawing.Pen(Color.FromArgb((int)color));
        //    fObjectDictionary.Add(uniqueID, aPen);
        //}

        //public virtual PixelBuffer CreateBitmap(int width, int height)
        //{
        //    PixelBuffer newOne = new PixelBuffer(width, height);

        //    return newOne;
        //}

        public IntPtr HDC
        {
            get
            {
                return fDeviceContext.GetHdc();
            }
        }

        /// Some GraphPort control things
        /// 

        public virtual void SaveState()
        {
            fSavedState = fDeviceContext.Save();
        }

        public virtual void ResetState()
        {
            fDeviceContext.Restore(fSavedState);
        }

        public virtual void RestoreState(int toState)
        {
            ResetState();
        }

        public virtual void Flush()
        {
            fDeviceContext.Flush();
        }

        public virtual IntPtr SelectObject(IntPtr objectHandle)
        {
            return IntPtr.Zero;
        }

        public virtual void SelectObject(GDIPen aPen)
        {
        }

        public virtual void SelectObject(GDIBrush aBrush)
        {
        }

        public virtual void SelectObject(GDIFont aFont)
        {
        }

        public virtual void SelectObject(IUniqueGDIObject aObject)
        {
        }

        public virtual void SelectUniqueObject(Guid objectID)
        {
            // Lookup the object in the dictionary
            // If the object doesn't exist in the dictionary
            // return immediately.
            if (!fObjectDictionary.ContainsKey(objectID))
                return;

            IUniqueGDIObject aUniqueObject = (IUniqueGDIObject)fObjectDictionary[objectID];

            // Select it based on it's handle
            if (aUniqueObject != null)
                SelectObject(aUniqueObject);
        }

        public virtual void SelectStockObject(int objectIndex)
        {
        }

        // Dealing with clipping region
        public virtual void SetClipRectangle(Rectangle clipRect)
        {
            fDeviceContext.SetClip(clipRect);
        }


        public virtual void ResetClip()
        {
            fDeviceContext.ResetClip();
        }

        public virtual void SetPixel(int x, int y, Color colorref)
        {
        }

        public virtual uint GetPixel(int x, int y)
        {
            return 0;
        }

        public virtual void MoveTo(int x, int y)
        {
        }


        public virtual void DrawLine(GDIPen aPen, Point startPoint, Point endPoint)
        {
        }

        public virtual void DrawLines(GDIPen aPen, Point[] points)
        {
            fDeviceContext.DrawLines(fCurrentPen, points);
        }

        public virtual void DrawRoundRect(GDIPen aPen, Rectangle rect, int xRadius, int yRadius)
        {
        }

        /// <summary>
        /// Draw the frame of a rectangle using a pen.  Leave the interior alone.
        /// </summary>
        /// <param name="aRect">The rectangle to be drawn</param>
        /// <param name="pen">The pen used to draw the frame</param>
        /// 
        public virtual void DrawRectangle(GDIPen aPen, Rectangle rect)
        {
           // fDeviceContext.DrawRectangle(aPen, x, y, width, height);
        }

        public virtual void DrawRectangles(GDIPen aPen, Rectangle[] rects)
        {
            // fDeviceContext.DrawRectangle(aPen, x, y, width, height);
        }

        public virtual void FillRectangle(GDIBrush aBrush, Rectangle rect)
        {
            // fDeviceContext.FillRectangle(aBrush, x, y, width, height);
        }


        /// DrawEllipse
        /// 
        public virtual void DrawEllipse(GDIPen aPen, Rectangle rect)
        {
        }

        public virtual void FillEllipse(GDIBrush aBrush, Rectangle rect)
        {
        }

        public virtual void DrawGradientRectangle(GradientRect aGradient)
        {
        }

        public virtual void SetTextColor(uint colorref)
        {
        }


        public virtual void DrawString(int x, int y, string aString)
        {
            fDeviceContext.DrawString(aString, fCurrentFont, fCurrentBrush, (float)x, (float)y);
        }


        public virtual void DrawBeziers(GDIPen aPen, Point[] points)
        {
            fDeviceContext.DrawBezier(fCurrentPen, points[0], points[1], points[2], points[3]);
        }


        public virtual void DrawPath(GDIPen aPen, GPath aPath)
        {
        }

        public virtual void FillPath(GDIBrush aBrush, GPath aPath)
        {
        }

        // Draw a Polygon
        public virtual void Polygon(System.Drawing.Point[] points)
        {
            fDeviceContext.DrawPolygon(fCurrentPen, points);
        }

        // Generalized bit block transfer
        // Can transfer from any device context to this one.
        public virtual void PixBlt(IPixelArray bitmap, int x, int y)
        {
            // Convert to a Bitmap object
            // Blit that
        }

        public virtual void PixmapShardBlt(IPixelArray bitmap, Rectangle srcRect, Rectangle dstRect)
        {
            // Convert to a Bitmap object
            // Blit that
        }

        // Can transfer from any device context to this one.
        public virtual void BitBlt(int x, int y, GDIDIBSection bitmap)
        {
            // Convert to a Bitmap object
            // Blit that
        }


        public virtual void ScaleBitmap(GDIPixmap aBitmap, Rectangle aFrame)
        {
        }

        public virtual void DrawImage(GDIPixmap bitmap, Point[] destinationParallelogram, Rectangle srcRect, GraphicsUnit units)
        {
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                    GDIPixmap bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                    byte alpha)
        {
        }


        public virtual void SetFont(GDIFont aFont)
        {
        }

        //// Dealing with a path
        //public virtual void BeginPath()
        //{
        //    fCurrentContainer = fDeviceContext.BeginContainer();
        //}

        //public virtual void EndPath()
        //{
        //    fDeviceContext.EndContainer(fCurrentContainer);
        //}

        //public virtual void FramePath()
        //{
        //}

        //public virtual void FillPath()
        //{
        //}

        //public virtual void DrawPath()
        //{
        //}

        //public virtual void SetPathAsClipRegion()
        //{
        //}

        public virtual void SetMappingMode(MappingModes aMode)
        {
        }

        public virtual void SetBkColor(uint colorref)
        {
        }

        public virtual void SetBkMode(int bkMode)
        {
        }

        public virtual void SetROP2(BinaryRasterOps rasOp)
        {
        }

        public virtual void SetPolyFillMode(PolygonFillMode fillMode)
        {
        }

        public virtual void SetPen(IPen aPen)
        {
        }

        public virtual void SetBrush(GDIBrush aBrush)
        {
        }


        public virtual void SetWorldTransform(Transform2D aTransform)
        {
        }

        public virtual void RotateTransform(float angle, int x, int y)
        {
            fDeviceContext.RotateTransform(angle);
        }

        public virtual void ScaleTransform(float scaleX, float scaleY)
        {
            fDeviceContext.ScaleTransform(scaleX, scaleY);
        }

        public virtual void TranslateTransform(int dx, int dy)
        {
            fDeviceContext.TranslateTransform(dx, dy);
        }
    }
}