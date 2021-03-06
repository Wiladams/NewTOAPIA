using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;  // For StringBuilder
using System.Collections.Generic;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing
{
    public class GDIRenderer : IGraphPort
    {
        GDIContext fDeviceContext;
        Dictionary<Guid, GDIObject> fObjectDictionary;

        public GDIRenderer(GDIContext devContext)
        {
            fObjectDictionary = new Dictionary<Guid, GDIObject>();
            fDeviceContext = devContext;
            fDeviceContext.SetGraphicsMode(GDI32.GM_ADVANCED);  // Use advanced graphics mode so we can use world transforms
        }

        #region Static Methods

        //public static GDIRenderer CreateFromPixelBuffer(GDIDIBSection32 aBuffer)
        //{
        //    GDIRenderer renderer = new GDIRenderer(aBuffer.DeviceContext);

        //    return renderer;
        //}
        
        #endregion

        #region Properties

        public GDIContext DeviceContext
        {
            get { return fDeviceContext; }
            set
            {
                fDeviceContext = value;
            }
        }
        #endregion



        #region Drawing Pixels
        public virtual void SetPixel(int x, int y, Color colorref)
        {
            DeviceContext.SetPixel(x, y, (uint)colorref.ToArgb());
        }
        #endregion

        #region Drawing Lines
        //public virtual void Line(int x1, int y1, int x2, int y2)
        //{
        //    Point[] points = new Point[]{new Point(x1,y1), new Point(x2,y2)};
        //    DrawLines(points);
        //}

        //public virtual void LineTo(int x, int y)
        //{
        //    GDI32.LineTo(HDC, x, y);
        //}

        //public virtual void LineTo(Point toPoint)
        //{
        //    LineTo(toPoint.X, toPoint.Y);
        //}

        public virtual void DrawLine(GDIPen aPen, Point startPOINT, Point endPoint)
        {
            Point[] points = new Point[2];
            points[0] = startPOINT;
            points[1] = endPoint;

            DrawLines(aPen, points);
        }

        //public virtual void DrawLine(Point startPOINT, Point endPoint)
        //{
        //    MoveTo(startPOINT.X, startPOINT.Y);
        //    LineTo(endPoint);

        //}

        /// DrawLine
        // Assume we want to draw a line using the currently selected
        // GDIPen, whichever that may be, and the currently selected color
        //public virtual void DrawLine(GDIPen aPen, int x1, int y1, int x2, int y2)
        //{
        //    SetPen(aPen);
        //    MoveTo(x1, y1);
        //    LineTo(x2, y2);
        //}

        /// DrawLine
        // Assume we want to draw a line using the currently selected
        // GDIPen, whichever that may be, and the currently selected color
        //public virtual void DrawLine(int x1, int y1, int x2, int y2)
        //{
        //    MoveTo(x1, y1);
        //    LineTo(new System.Drawing.Point(x2, y2));
        //}

        public virtual void DrawLines(GDIPen aPen, System.Drawing.Point[] points)
        {
            SetPen(aPen);
            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            DeviceContext.PolyLine(pts);
        }
        #endregion

        #region Drawing Rectangles
        public virtual void DrawRectangle(GDIPen aPen, Rectangle rect)
        {
            // 1. Select a hollow brush so the inside does 
            // not get filled.
            SelectStockObject(GDI32.HOLLOW_BRUSH);

            // 2. Set the pen
            SetPen(aPen);

            // 3. And draw the rectangle
            DeviceContext.Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public virtual void DrawRectangles(GDIPen aPen, Rectangle[] rects)
        {
            // 1. Select a hollow brush so the inside does 
            // not get filled.
            SelectStockObject(GDI32.HOLLOW_BRUSH);

            // 2. Set the pen
            SetPen(aPen);

            // 3. And draw the rectangles
            foreach (Rectangle r in rects)
            {
                DeviceContext.Rectangle(r.Left, r.Top, r.Right, r.Bottom);
            }
        }


        /// <summary>
        /// Draw the frame of a rectangle using the specified pen.
        /// </summary>
        /// <param name="aPen"></param>
        /// <param name="x">left</param>
        /// <param name="y">top</param>
        /// <param name="width">Width of rectangle.</param>
        /// <param name="height">Height of rectangle.</param>
        public virtual void DrawRectangle(GDIPen aPen, int x, int y, int width, int height)
        {
            // 1. Select a hollow brush so the inside does 
            // not get filled.
            SelectStockObject(GDI32.HOLLOW_BRUSH);
            
            // 2. Set the pen
            SetPen(aPen);

            // 3. And draw the rectangle
            DeviceContext.Rectangle(x, y, x + width - 1, y + height - 1);
        }

        public virtual void StrokeAndFillRectangle(GDIPen aPen, GDIBrush aBrush, Rectangle aRect)
        {
            SaveState();

            // Select the pen and brush into the context
            SetPen(aPen);
            SetBrush(aBrush);

            // Draw the rectangle
            DeviceContext.Rectangle(aRect.Left, aRect.Top, aRect.Right, aRect.Bottom);
            
            ResetState();
        }

        public virtual void DrawRectangle(Rectangle aRect)
        {
            DeviceContext.Rectangle(aRect.Left, aRect.Top, aRect.Right, aRect.Bottom);
        }

        /// <summary>
        /// Draw the frame of a rectangle using a pen.  Leave the interior alone.
        /// </summary>
        /// <param name="aRect">The rectangle to be drawn</param>
        /// <param name="pen">The pen used to draw the frame</param>
        /// 
        public virtual void DrawRectangle(int left, int top, int right, int bottom)
        {
            DeviceContext.Rectangle(left, top, right, bottom);
        }


        public virtual void FillRectangle(GDIBrush aBrush, int x, int y, int width, int height)
        {
            SelectStockObject(GDI32.NULL_PEN);

            SetBrush(aBrush);

            DeviceContext.Rectangle(x, y, x + width, y + height);
        }

        public virtual void FrameRectangle(GDIPen pen, Rectangle aRect)
        {
            SaveState();

            SetPen(pen);
            DrawRectangle(aRect);

            ResetState();
        }

        public virtual void FillRectangle(GDIBrush aBrush, Rectangle aRect)
        {
            SelectStockObject(GDI32.NULL_PEN);

            SetBrush(aBrush);

            DeviceContext.Rectangle(aRect.X, aRect.Y, aRect.Right, aRect.Bottom);
        }

        public virtual void DrawRoundRect(GDIPen aPen, Rectangle rect, int xRadius, int yRadius)
        {
            SetPen(aPen);

            DeviceContext.RoundRect(rect.Left, rect.Top, rect.Right, rect.Bottom, xRadius, yRadius);
        }
        #endregion

        #region Drawing Ellipses
        public virtual void DrawEllipse(GDIPen aPen, Rectangle rect)
        {
            // 1. Select a hollow brush so the inside does 
            // not get filled.
            SelectStockObject(GDI32.HOLLOW_BRUSH);

            // 2. Set the pen
            SetPen(aPen);

            // 3. Draw the ellipse
            GDI32.Ellipse(DeviceContext, rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public virtual void FillEllipse(GDIBrush aBrush, Rectangle rect)
        {
            // 1. Select a null pen so there's no border
            SelectStockObject(GDI32.NULL_PEN);

            // 2. Set the brush
            SetBrush(aBrush);
            
            // 3. Draw the ellipse
            GDI32.Ellipse(DeviceContext, rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
        #endregion

        #region Drawing Gradients
        public virtual void DrawGradientRectangle(GradientRect aGradient)
        {
            GDI32.GradientRectangle(DeviceContext, 
                aGradient.Vertices, aGradient.Vertices.Length, 
                aGradient.Boundary, aGradient.Boundary.Length, 
                (int)aGradient.Direction);
        }

        public virtual void GradientTriangle(TRIVERTEX[] pVertex, GRADIENT_TRIANGLE[] pMesh, int dwMode)
        {
            GDI32.GradientTriangle(DeviceContext, pVertex, pVertex.Length, pMesh, pMesh.Length, dwMode);
        }
        #endregion

        #region Drawing Text
        public virtual void SetTextColor(uint colorref)
        {
            GDI32.SetTextColor(DeviceContext, colorref);
        }

        public virtual void DrawString(int x, int y, string aString)
        {
            GDI32.TextOut(DeviceContext, x, y, aString, aString.Length);
        }

        public virtual void MeasureCharacter(char c, ref int width, ref int height)
        {
            int[] aBuffer = new int[1];

            if (GDI32.GetCharWidth32(DeviceContext, (uint)c, (uint)c, out aBuffer))
                width = aBuffer[0];
            else
                width = 0;

            //MeasureString(new string(c,1), ref width, ref height);
        }
        #endregion

        #region Drawing Beziers
        public virtual void DrawBeziers(GDIPen aPen, Point[] points)
        {
            SetPen(aPen);

            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            GDI32.PolyBezier(DeviceContext, pts, (uint)pts.Length);
        }
        #endregion

        #region Drawing Paths

        public virtual void DrawPath(GDIPen aPen, GPath aPath)
        {
            // 1. Set the path on the context
            DeviceContext.ReplayPath(aPath);

            // 2. Stroke the path using the specified pen
            DeviceContext.StrokePath();
        }

        public virtual void FramePath(GDIPen aPen, GPath aPath)
        {
            // 1. Set the path on the context
            DeviceContext.ReplayPath(aPath);

            // 2. Set the pen
            SetPen(aPen);

            // 2. Stroke the path using the specified pen
            DeviceContext.StrokePath();
        }

        public virtual void FillPath(GDIBrush aBrush, GPath aPath)
        {
            // 1. Set the path on the context
            DeviceContext.ReplayPath(aPath);

            // 2. Set the brush as the active brush
            SetBrush(aBrush);

            // 3. call FillPath()
            DeviceContext.FillPath();
        }

        #endregion

        #region Drawing Polygons
        public virtual void Polygon(System.Drawing.Point[] points)
        {
            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            GDI32.Polygon(DeviceContext, pts, pts.Length);
        }

        //public virtual void FramePolygon(PolygonG poly, GDIPen aPen)
        //{
        //    SaveState();

        //    Polygon(poly.POINTs);

        //    ResetState();
        //}

        //public virtual void FillPolygon(PolygonG poly, GDIBrush aBrush)
        //{
        //    SaveState();

        //    // Select the current pen and brush
        //    GDI32.Polygon(HDC, poly.POINTs, poly.POINTs.Length);

        //    ResetState();
        //}

        //public virtual void DrawPolygon(PolygonG poly, GDIPen aPen, GDIBrush aBrush)
        //{
        //    DrawPolygon(poly.POINTs, aPen, aBrush);
        //}

        public virtual void DrawPolygon(System.Drawing.Point[] points)
        {
            // Select the current pen and brush

            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            GDI32.Polygon(DeviceContext, pts, pts.Length);
        }

        public virtual void DrawPolygon(System.Drawing.Point[] points, GDIPen aPen, GDIBrush aBrush)
        {
            SaveState();

            // Set the brush
            // Set the pen

            // Select the current pen and brush
            TOAPI.Types.POINT[] pts = new POINT[points.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = points[i].X;
                pts[i].Y = points[i].Y;
            }

            GDI32.Polygon(DeviceContext, pts, pts.Length);

            ResetState();
        }
        #endregion

        #region Drawing Pixel Arrays
        public virtual void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
        {
            GDIContext screenContext = GDIContext.CreateForDefaultDisplay();

            //GDI32.BitBlt(DeviceContext, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize.Width, blockRegionSize.Height,
            //    screenContext, upperLeftSource.X, upperLeftSource.Y, (int)TernaryRasterOps.SRCCOPY);

            DeviceContext.BitBlt(screenContext, upperLeftSource, new Rectangle(upperLeftDestination, blockRegionSize), TernaryRasterOps.SRCCOPY);
        }

        /// <summary>
        /// DrawImage, will draw the supplied image graphic on the screen.  It will use
        /// the Image's frame and size to determine where to display it.
        /// </summary>
        /// <param name="img"></param>
        public virtual void DrawBitmap(GDIPixmap pixmap, Point origin)
        {
            // If no bitmap, fail silently
            if (null == pixmap)
                return;

            bool result = DeviceContext.AlphaBlend(pixmap.DeviceContext,
                new Rectangle(0, 0, pixmap.Width, pixmap.Height), 
                new Rectangle(origin.X, origin.Y, pixmap.Width, pixmap.Height), 
                255);
        }

        public virtual void DrawBitmap(GDIPixmap pixmap, System.Drawing.Rectangle srcRect, System.Drawing.Rectangle dstRect)
        {
            // If no bitmap, fail silently
            if (null == pixmap)
                return;

            bool result = DeviceContext.AlphaBlend(pixmap.DeviceContext, srcRect, dstRect, 255);
        }


        // Generalized bit block transfer
        // Can transfer from any device context to this one.
        public virtual void PixBlt(IPixelArray pixmap, int x, int y)
        {
            if (null == pixmap)
                return;

            BITMAPINFO bmi = new BITMAPINFO();
            bmi.Init();
            
            bmi.bmiHeader.biBitCount = (ushort)pixmap.BitsPerPixel;
            bmi.bmiHeader.biWidth = pixmap.Width;

            if (pixmap.Orientation == PixmapOrientation.TopToBottom)
                bmi.bmiHeader.biHeight = -pixmap.Height;
            else
                bmi.bmiHeader.biHeight = pixmap.Height;

            bmi.bmiHeader.biSizeImage = (uint)(pixmap.BytesPerRow * pixmap.Height);
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biClrImportant = 0;
            bmi.bmiHeader.biClrUsed = 0;
            bmi.bmiHeader.biCompression = 0;

            int result = GDI32.StretchDIBits(DeviceContext, x, y, pixmap.Width, pixmap.Height, 0, 0, pixmap.Width, pixmap.Height, pixmap.Pixels,
                ref bmi, GDI32.DIB_RGB_COLORS, (uint)TernaryRasterOps.SRCCOPY);

            //Console.WriteLine("Result: {0}", result);
        }

        public virtual void PixmapShardBlt(IPixelArray pixmap, Rectangle srcRect, Rectangle dstRect)
        {
            if (null == pixmap)
                return;

            int result = DeviceContext.PixelBlt(srcRect, dstRect, pixmap.Pixels, (BitCount)pixmap.BitsPerPixel);
            
            //Console.WriteLine("Result: {0}", result);
        }

        //public virtual void BitBlt(int x, int y, GDIDIBitmap bitmap)
        //{
        //    int scansDrawn = DeviceContext.StretchDIBits(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
        //        new Rectangle(x, y, bitmap.Width, bitmap.Height), TernaryRasterOps.SRCCOPY);
        //}

        public virtual bool BitBlt(int x, int y, int nWidth, int nHeight,
            GDIContext hSrcDC, int xSrc, int ySrc, TernaryRasterOps dwRop)
        {
            bool success = DeviceContext.BitBlt(hSrcDC, new Point(xSrc, ySrc), new Rectangle(x, y, nWidth, nHeight), dwRop);

            return success;
        }

        public virtual void ScaleBitmap(GDIPixmap bitmap, Rectangle aFrame)
        {
            // If no bitmap, fail silently
            if (null == bitmap)
                return;

            bool result = DeviceContext.AlphaBlend(bitmap.DeviceContext, new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                aFrame, 255);
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                    GDIPixmap bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                    byte opacity)
        {
            // If no bitmap, fail silently
            if (null == bitmap)
                return;

            bool result = DeviceContext.AlphaBlend(bitmap.DeviceContext, new Rectangle(srcX, srcY, srcWidth, srcHeight),
                new Rectangle(x, y, width, height), opacity);
        }

        public virtual void StretchBlt(int x, int y, int width, int height,
                    GDIContext srcDC, int srcX, int srcY, int srcWidth, int srcHeight,
                    TernaryRasterOps dwRop)
        {
            bool success = DeviceContext.StretchBlt(srcDC, new Rectangle(srcX, srcY, srcWidth, srcHeight),
                new Rectangle(x, y, width, height), dwRop);
        }

        public virtual void DrawImage(GDIPixmap bitmap,
            System.Drawing.Point[] destinationParallelogram,
            System.Drawing.Rectangle srcRect,
            System.Drawing.GraphicsUnit units)
        {
            TOAPI.Types.POINT[] pts = new POINT[destinationParallelogram.Length];
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i].X = destinationParallelogram[i].X;
                pts[i].Y = destinationParallelogram[i].Y;
            }

            bool result = DeviceContext.PlgBlt(bitmap.DeviceContext, srcRect, pts,
                IntPtr.Zero, 0, 0);
        }

        //public virtual void PlgBlt(GDIContext srcDC, Rectangle srcRect, 
        //    System.Drawing.Point[] lpPOINT,
        //    IntPtr hbmMask, int xMask, int yMask)
        //{
        //    bool result = DeviceContext.PlgBlt(srcDC, srcRect, lpPOINT, hbmMask, xMask, yMask);
        //}
        #endregion

        #region Drawing Regions
        public virtual void DrawRegion(GDIRegion region)
        {
            DeviceContext.DrawRegion(region);
        }

        public virtual void FillRegion(GDIRegion region, GDIBrush aBrush)
        {
            DeviceContext.FillRegion(region, aBrush);
        }

        public virtual void FrameRegion(GDIRegion region, GDIBrush aBrush, Size strokeSize)
        {
            DeviceContext.FrameRegion(region, aBrush, strokeSize);
        }

        public virtual void InvertRegion(GDIRegion region)
        {
            DeviceContext.InvertRegion(region);
        }
        #endregion



        #region Brush Management
        public virtual void CreateBrush(BrushStyle aStyle, HatchStyle hatch, uint colorref, Guid uniqueID)
        {
            GDIBrush aBrush = DeviceContext.CreateBrush(aStyle, hatch, colorref, uniqueID);
            fObjectDictionary.Add(uniqueID, aBrush);
        }

        public virtual void SetBrush(GDIBrush aBrush)
        {
            // 1. Check to see if the brush object already exists in our
            // list of objects
            bool containsObject = fObjectDictionary.ContainsKey(aBrush.UniqueID);
            
            // 2. If the object is already in the dictionary, then select
            // it as the current brush and return.
            if (containsObject) // and the object is a brush
            {
                // Get a hold of the actual object in the dictionary
                GDIObject aUniqueObject = fObjectDictionary[aBrush.UniqueID];

                // Select it based on it's handle
                if (aUniqueObject != null)
                    DeviceContext.SelectObject(aUniqueObject);

                return;
            }

            // 3. If the brush is not found, then create a new brush, 
            // and put it into the table.
            CreateBrush(aBrush.Style, aBrush.Hatching, aBrush.Color, aBrush.UniqueID);

            // 4. Select the new brush
            SelectUniqueObject(aBrush.UniqueID);
        }

        public virtual void SetDefaultBrushColor(uint colorref)
        {
            GDI32.SetDCBrushColor(DeviceContext, colorref);
        }

        public virtual void UseDefaultBrush()
        {
            DeviceContext.SelectObject((SafeHandle)GDISolidBrush.DeviceContextBrush);
        }
        #endregion

        #region Clip Management
        public virtual void SetClipRectangle(Rectangle clipRect)
        {
            RectangleG rectPath = new RectangleG(clipRect.X, clipRect.Y, clipRect.Width, clipRect.Height);
            DeviceContext.ReplayPath(rectPath);

            DeviceContext.SetPathAsClipRegion();
        }

        public virtual void SetClip(GDIRegion clipRegion)
        {
            GDI32.SelectClipRgn(DeviceContext, clipRegion.DangerousGetHandle());
        }

        public virtual void ResetClip()
        {
            GDI32.SetBoundsRect(DeviceContext, IntPtr.Zero, GDI32.DCB_RESET);

        }
        #endregion

        #region Font Management
        public virtual void CreateFont(string faceName, int height, Guid aGuid)
        {
            GDIFont aFont = new GDIFont(faceName, height, aGuid);
            fObjectDictionary.Add(aGuid, aFont);
        }

        public virtual void SetFont(GDIFont aFont)
        {
            // 1. Check to see if the font object already exists in our
            // list of objects
            bool containsObject = fObjectDictionary.ContainsKey(aFont.UniqueID);

            // 2. If the object is already in the dictionary, then select
            // it as the current font and return.
            if (containsObject) // and the object is a brush
            {
                // Get a hold of the actual object in the dictionary
                GDIObject aUniqueObject = fObjectDictionary[aFont.UniqueID];

                // Select it based on it's handle
                if (aUniqueObject != null)
                    DeviceContext.SelectObject(aUniqueObject);

                return;
            }

            // 3. If the font is not found, then create a new font, 
            // and put it into the table.
            CreateFont(aFont.FaceName, aFont.Height, aFont.UniqueID);

            // 4. Select the new font
            SelectUniqueObject(aFont.UniqueID);
        }
        #endregion

        #region Object Creation
        public virtual void CreateCosmeticPen(PenStyle aStyle, uint color, Guid uniqueID)
        {
            GDICosmeticPen aPen = new GDICosmeticPen(aStyle, color, uniqueID);
            fObjectDictionary.Add(uniqueID, aPen);
        }

        public virtual GDIDIBSection CreateBitmap(int width, int height)
        {
            GDIDIBSection newOne = new GDIDIBSection(width, height);

            return newOne;
        }

        #endregion

        #region Object Management
        public virtual void SelectUniqueObject(Guid objectID)
        {
            // Lookup the object in the dictionary
            // If the object doesn't exist in the dictionary
            // return immediately.
            if (!fObjectDictionary.ContainsKey(objectID))
                return;

            GDIObject aUniqueObject = fObjectDictionary[objectID];

            // Select it based on it's handle
            if (aUniqueObject != null)
                DeviceContext.SelectObject(aUniqueObject); 
        }

        public virtual void SelectStockObject(int objectIndex)
        {
            DeviceContext.SelectStockObject(objectIndex);
        }
        #endregion

        #region Path Management
        public virtual GPath CreatePath(Guid uniqueID)
        {
            GDIPath newPath = new GDIPath(fDeviceContext, uniqueID);

            return newPath;
        }

        public virtual void SetPathAsClipRegion(GPath aPath)
        {
            // 1. Replay the path on the device context
            DeviceContext.ReplayPath(aPath);

            // 2. Call the right routine
            GDI32.SelectClipPath(DeviceContext, GDI32.RGN_COPY);
        }

        #endregion

        #region Pen Management
        public virtual void CreatePen(PenType aType, PenStyle aStyle, PenJoinStyle aJoinStyle, PenEndCap aEndCap, uint colorref, int width, Guid uniqueID)
        {
            GDIPen aPen = DeviceContext.CreatePen(aType, aStyle, aJoinStyle, aEndCap, colorref, width, uniqueID);
            fObjectDictionary.Add(uniqueID, aPen);
        }

        public virtual void SetDefaultPenColor(uint colorref)
        {
            DeviceContext.SetDefaultPenColor(colorref);
        }

        public virtual void SetPen(GDIPen aPen)
        {
            // 1. Check to see if the pen object already exists in our
            // list of objects
            bool containsObject = fObjectDictionary.ContainsKey(aPen.UniqueID);

            // 2. If the object is already in the dictionary, then select
            // it as the current brush and return.
            if (containsObject) // and the object is a brush
            {
                // Get a hold of the actual object in the dictionary
                GDIObject aUniqueObject = fObjectDictionary[aPen.UniqueID];

                // Select it based on it's handle
                if (aUniqueObject != null)
                    DeviceContext.SelectObject(aUniqueObject);

                return;
            }

            // 3. If the brush is not found, then create a new brush, 
            // and put it into the table.
            CreatePen(aPen.TypeOfPen, aPen.Style, aPen.JoinStyle, aPen.EndCap, aPen.Color, aPen.Width, aPen.UniqueID);

            // 4. Select the new brush
            SelectUniqueObject(aPen.UniqueID);
        }

        public virtual void UseDefaultPen()
        {
            SelectStockObject(GDI32.DC_PEN);
        }
        #endregion

        #region Pen Movement
        public virtual void MoveTo(int x, int y)
        {
            DeviceContext.MoveTo(x, y);
        }

        public virtual void MoveTo(System.Drawing.Point aPOINT)
        {
            DeviceContext.MoveTo(aPOINT.X, aPOINT.Y);
        }
        #endregion

        #region State Management
        public virtual void SaveState()
        {
            DeviceContext.SaveState();
        }

        public virtual void ResetState()
        {
            DeviceContext.ResetState();
        }

        public virtual void RestoreState(int toState)
        {
            DeviceContext.RestoreState(toState);
        }

        public virtual void Flush()
        {
            DeviceContext.Flush();
        }

        public virtual void SetMappingMode(MappingModes aMode)
        {
            DeviceContext.SetMappingMode(aMode);
        }

        public virtual void SetBkColor(uint colorref)
        {
            DeviceContext.SetBkColor(colorref);
        }

        public virtual void SetBkMode(int bkMode)
        {
            DeviceContext.SetBkMode(bkMode);
        }

        public virtual void SetROP2(BinaryRasterOps rasOp)
        {
            DeviceContext.SetROP2(rasOp);
        }

        public virtual void SetPolyFillMode(PolygonFillMode fillMode)
        {
            DeviceContext.SetPolyFillMode(fillMode);
        }

        #endregion

        #region Viewport Management
        private Transform2D GetWorldTransform()
        {
            XFORM aTransform = new XFORM();
            
            bool result = GDI32.GetWorldTransform(DeviceContext, out aTransform);

            Transform2D trans = new Transform2D();
            trans.SetTransform(aTransform);

            return trans;
        }

        public virtual void SetWorldTransform(Transform2D aTransform)
        {
            DeviceContext.SetWorldTransform(aTransform);
        }

        public virtual void RotateTransform(float angle, int x, int y)
        {
            // Get the current transform
            Transform2D oldTransform = GetWorldTransform();

            // Perform the scaling
            oldTransform.Rotate(angle, (float)x, (float)y);

            //  Set it back on the graph port
            SetWorldTransform(oldTransform);
        }

        public virtual void ScaleTransform(float scaleX, float scaleY)
        {
            // Get the current transform
            Transform2D oldTransform = GetWorldTransform();

            // Perform the scaling
            oldTransform.Scale(scaleX, scaleY);

            //  Set it back on the graph port
            SetWorldTransform(oldTransform);

        }

        public virtual void TranslateTransform(int dx, int dy)
        {
            // Get the current transform
            Transform2D oldTransform = GetWorldTransform();

            // Perform the translation
            oldTransform.Translate((float)dx, (float)dy);

           //  Set it back on the graph port
           SetWorldTransform(oldTransform);

        }
        #endregion

    }
}