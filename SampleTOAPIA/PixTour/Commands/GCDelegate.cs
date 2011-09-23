using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.GDI32;
using TOAPI.Types;

public class GCDelegate : IRenderGDI
{
    public delegate int DrawCommandProc(IntPtr hdc, IntPtr lpht, EMR lpmr, int hHandles, IntPtr data);

    public event DrawCommandProc CommandPacked;


    Dictionary<Guid, IUniqueGDIObject> fObjectDictionary;
    IntPtr fDeviceContext;
    Size fSize;

    public GCDelegate(Size aSize)
    {
        fObjectDictionary = new Dictionary<Guid, IUniqueGDIObject>();
        fSize = aSize;
        fDeviceContext = IntPtr.Zero;
    }

    public IntPtr DeviceContext
    {
        get
        {
            return fDeviceContext;
        }
    }

    void PackCommand(EMR aCommand)
    {
        if (CommandPacked != null)
            CommandPacked(IntPtr.Zero, IntPtr.Zero, aCommand, 0, IntPtr.Zero);
    }

    public Size Size
    {
        get { return fSize; }
    }

    // Modes and attributes
    public virtual void SetTextColor(uint colorref)
    {
        EMRSETTEXTCOLOR textcolor = new EMRSETTEXTCOLOR();
        textcolor.crColor = colorref;

        PackCommand(textcolor);
    }

    // Various drawing commands
    public virtual void MoveTo(int x, int y)
    {
        MoveTo(new Point(x, y));
    }

    public virtual void MoveTo(Point aPoint)
    {
        EMRMOVETO moveTo = new EMRMOVETO();
        moveTo.ptl = aPoint;
        PackCommand(moveTo);
    }

    public virtual void LineTo(int x, int y)
    {
        LineTo(new Point(x, y));
    }

    public virtual void LineTo(Point aPoint)
    {
        EMRLINETO lineTo = new EMRLINETO();
        lineTo.ptl = aPoint;
        
        PackCommand(lineTo);
    }

    public virtual void DrawLine(Point startPoint, Point endPoint)
    {
        MoveTo(startPoint);
        LineTo(endPoint);
    }

    public virtual void DrawLine(LineG aLine, Pen pen)
    {
        SaveState();

        SelectObject(pen);

        DrawLine(aLine.StartPoint, aLine.EndPoint);

        RestoreState();
    }




    /// Some GraphPort control things
    /// 

    public virtual void SaveState()
    {
        EMRSAVEDC saveDC = new EMRSAVEDC();
        
        PackCommand(saveDC);
    }

    /// <summary>
    /// Pop all the current state information back to the defaults
    /// </summary>
    public virtual void RestoreState()
    {
        RestoreState(-1);
    }

    public virtual void RestoreState(int relative)
    {
        EMRRESTOREDC restoreDC = new EMRRESTOREDC();
        restoreDC.iRelative = relative;

        PackCommand(restoreDC);
    }

    public virtual void Flush()
    {
        EMREOF eof = new EMREOF();

        PackCommand(eof);
    }

    // Dealing with clipping region
    public virtual void SetClip(Rectangle clipRect)
    {
        // This needs to be redone as modifications of the device context
        // directly.
        //GDI32.SetBoundsRect(DeviceContext, ref clipRect, GDI32.DCB_SET);
    }

    public virtual void SetClip(Region clipRegion)
    {
        // Not implemented
    }

    public virtual void ResetClip()
    {
        //GDI32.SetBoundsRect(DeviceContext, IntPtr.Zero, GDI32.DCB_RESET);

    }

    public virtual bool SetROP2(BinaryRasterOps rasOp)
    {
        EMRSETROP2 setOp = new EMRSETROP2();
        setOp.iMode = (uint)rasOp;

        PackCommand(setOp);

        return true;
    }

    // Drawing Primitives
    /// Our first pass will be to implement everything as 
    /// taking explicit int parameters.
    /// We'll assume that drawing with a different pen will 
    /// be accomplished by setting the Pen property on the 
    /// port before drawing.  This will keep our API count 
    /// low and alleviate the need to pass the same 
    /// parameter every time.  We can add more later.
    ///
    public virtual void SetPixel(int x, int y, uint colorref)
    {
        SetPixel(new Point(x, y), colorref);
    }

    public virtual void SetPixel(Point aPoint, uint colorref)
    {
        EMRSETPIXELV setPixel = new EMRSETPIXELV();
        setPixel.ptlPixel = aPoint;
        setPixel.crColor = colorref;

        PackCommand(setPixel);
    }

    public virtual void SetPixel(Point aPoint, uint colorref, BinaryRasterOps rasOp)
    {
        SaveState();
        SetROP2(rasOp);
        SetPixel(aPoint, colorref);
        RestoreState();
    }

    public virtual uint GetPixel(int x, int y)
    {
        //return GDI32.GetPixel(DeviceContext, x, y);
        return 0;
    }


    public virtual void UseDefaultBrush()
    {
        SelectStockObject(GDI32.DC_BRUSH);
    }

    public virtual void SetDefaultPenColor(uint colorref)
    {
        EMRSETDCPENCOLOR dcPenColor = new EMRSETDCPENCOLOR();
        dcPenColor.crColor = colorref;

        PackCommand(dcPenColor);
    }

    public virtual void SetPen(Pen aPen)
    {
        EMRSELECTUNIQUEOBJECT selectObject = new EMRSELECTUNIQUEOBJECT();
        selectObject.uniqueID = aPen.UniqueID;

        PackCommand(selectObject);
    }

    public virtual void UseDefaultPen()
    {
        SelectStockObject(GDI32.DC_PEN);
    }

    public virtual void SetDefaultBrushColor(uint colorref)
    {
        EMRSETDCBRUSHCOLOR dcBrushColor = new EMRSETDCBRUSHCOLOR();
        dcBrushColor.crColor = colorref;

        PackCommand(dcBrushColor);
    }

    public virtual void SelectUniqueObject(Guid objectID)
    {
        EMRSELECTUNIQUEOBJECT selectUnique = new EMRSELECTUNIQUEOBJECT();
        selectUnique.uniqueID = objectID;

        PackCommand(selectUnique);
    }

    public virtual void SelectStockObject(int objIndex)
    {
        EMRSELECTSTOCKOBJECT selectObject = new EMRSELECTSTOCKOBJECT();
        selectObject.ihObject = objIndex;

        PackCommand(selectObject);
    }

    void SelectObject(IHandle aObject)
    {
        
        EMRSELECTOBJECT selectObject = new EMRSELECTOBJECT();
        selectObject.ihObject = aObject.Handle.ToInt32();

        PackCommand(selectObject);
    }

    public virtual void RoundRect(int left, int top, int right, int bottom, int width, int height)
    {
        EMRROUNDRECT round = new EMRROUNDRECT();
        round.rclBox = TOAPI.Types.Rectangle.FromLTRB(left, top, right, bottom);
        round.szlCorner = new Size(width, height);

        PackCommand(round);
    }

    public virtual void Rectangle(int left, int top, int right, int bottom)
    {
        Rectangle(TOAPI.Types.Rectangle.FromLTRB(left, top, right, bottom));
    }

    /// <summary>
    /// Draw the frame of a rectangle using a pen.  Leave the interior alone.
    /// </summary>
    /// <param name="aRect">The rectangle to be drawn</param>
    /// <param name="pen">The pen used to draw the frame</param>
    public virtual void Rectangle(Rectangle aRect)
    {
        EMRRECTANGLE newRect = new EMRRECTANGLE();
        newRect.rclBox = aRect;

        PackCommand(newRect);
    }

    public virtual void FrameRectangle(Rectangle aRect, Pen pen)
    {
        SaveState();

        //SelectObject(SolidBrush.NullBrush);
        SelectObject(pen);

        Rectangle(aRect);

        RestoreState();
    }


    public virtual void DrawRectangle(Rectangle aRect, Pen aPen, Brush aBrush)
    {
        SaveState();

        // Select the pen and brush into the context
        SelectObject(aPen);
        SelectObject(aBrush);

        Rectangle(aRect);

        RestoreState();
    }

    public virtual void FillRectangle(Rectangle aRect, Brush aBrush)
    {
        SaveState();

        //UseDefaultBrush();
        //GDI32.FillRect(DeviceContext, ref aRect, aBrush.Handle);

        RestoreState();
    }

    // Gradient Fills
    public virtual void GradientFill(TRIVERTEX[] pVertex, GRADIENT_RECT[] pMesh, uint dwMode)
    {
        EMRGRADIENTFILL gradFill = new EMRGRADIENTFILL();
        gradFill.rclBounds = TOAPI.Types.Rectangle.Empty;
        gradFill.nVer = (uint)pVertex.Length;
        gradFill.nTri = (uint)pMesh.Length;
        gradFill.ulMode = dwMode;
        gradFill.Ver = pVertex;

        PackCommand(gradFill);
    }

    public virtual void GradientFill(TRIVERTEX[] pVertex, GRADIENT_TRIANGLE[] pMesh, uint dwMode)
    {
        EMRGRADIENTFILL gradFill = new EMRGRADIENTFILL();
        gradFill.rclBounds = TOAPI.Types.Rectangle.Empty;
        gradFill.nVer = (uint)pVertex.Length;
        gradFill.nTri = (uint)pMesh.Length;
        gradFill.ulMode = dwMode;
        gradFill.Ver = pVertex;

        PackCommand(gradFill);
    }

    /// DrawEllipse
    /// 
    public virtual void Ellipse(int left, int top, int right, int bottom)
    {
        EMRELLIPSE ellipse = new EMRELLIPSE();
        ellipse.rclBox = TOAPI.Types.Rectangle.FromLTRB(left,top,right,bottom);

        PackCommand(ellipse);
    }

    public virtual void Ellipse(EllipseG aEllipse)
    {
        Ellipse(aEllipse.Center.x - aEllipse.XRadius,
            aEllipse.Center.y - aEllipse.YRadius,
            aEllipse.Center.x + aEllipse.XRadius,
            aEllipse.Center.y + aEllipse.YRadius);

    }

    public virtual void FrameEllipse(EllipseG aEllipse, Pen aPen)
    {
        SaveState();
        SelectObject(aPen);
        Ellipse(aEllipse);
        RestoreState();
    }

    public virtual void FillEllipse(EllipseG aEllipse, Brush aBrush)
    {
        SaveState();

        SelectObject(aBrush);
        Ellipse(aEllipse);
        
        RestoreState();
    }

    public virtual void DrawEllipse(EllipseG aEllipse, Pen aPen, Brush aBrush)
    {
        SaveState();

        SelectObject(aPen);
        SelectObject(aBrush);

        Ellipse(aEllipse);
        
        RestoreState();
    }


    /// Draw a String
    //public virtual void DrawText(string s, int x, int y, int width, int height, int flags)
    //{
    //    RectangleG rect = new RectangleG(x,y,width,height);
    //    User32.DrawText(DeviceContext, new StringBuilder(s), -1, ref rect, (uint)flags);
    //    //DrawText(DeviceContext, s, -1, ref rect, 
    //    //	GDI32.DT_NOCLIP|GDI32.DT_NOPREFIX|
    //    //	GDI32.DT_SINGLELINE|GDI32.DT_TOP);
    //}

    //public virtual void DrawGlyphRun(GlyphRunG aGlyphRun, Pen aPen, Brush aBrush)
    //{
    //    SaveState();

    //    RestoreState();
    //}

    public virtual void DrawString(int x, int y, string aString)
    {
        EMREXTTEXTOUTA textout = new EMREXTTEXTOUTA();
        textout.emrtext.text = aString;
        textout.emrtext.nChars = (uint)aString.Length;
        textout.emrtext.ptlReference = new Point(x, y);
        textout.rclBounds = TOAPI.Types.Rectangle.Empty;
        textout.iGraphicsMode = GDI32.GM_ADVANCED;

        PackCommand(textout);
    }

    //public virtual void MeasureString(string s, ref int width, ref int height)
    //{
    //    Rectangle rect = new Rectangle(0,0,0,0);
    //    User32.DrawText(DeviceContext, new StringBuilder(s), -1, ref rect, 
    //        GDI32.DT_CALCRECT|GDI32.DT_NOCLIP|GDI32.DT_NOPREFIX|GDI32.DT_SINGLELINE|GDI32.DT_TOP);
    //    width = rect.Right+1;
    //    height = rect.Bottom+1;
    //}

    public virtual void MeasureCharacter(char c, ref int width, ref int height)
    {
        int[] aBuffer = new int[1];

        if (GDI32.GetCharWidth32(DeviceContext, (uint)c, (uint)c, out aBuffer))
            width = aBuffer[0];
        else
            width = 0;

        //MeasureString(new string(c,1), ref width, ref height);
    }

    public virtual void PolyBezier(Point[] points)
    {
        EMRPOLYBEZIER poly = new EMRPOLYBEZIER();
        poly.cptl = points.Length;
        poly.aptl = points;
        poly.rclBounds = TOAPI.Types.Rectangle.Empty;

        PackCommand(poly);
    }

    public virtual void PolyLine(Point[] points)
    {
        EMRPOLYLINE poly = new EMRPOLYLINE();
        poly.cptl = points.Length;
        poly.aptl = points;
        poly.rclBounds = TOAPI.Types.Rectangle.Empty;

        PackCommand(poly);
    }

    public virtual void PolyDraw(Point[] points, byte[] ptMeanings)
    {
        EMRPOLYDRAW polydraw = new EMRPOLYDRAW();
        polydraw.abTypes = ptMeanings;
        polydraw.aptl = points;
        polydraw.cptl = points.Length;
        polydraw.rclBounds = TOAPI.Types.Rectangle.Empty;

        PackCommand(polydraw);
    }

    // Draw a Polygon
    public virtual void Polygon(Point[] points)
    {
        EMRPOLYGON polygon = new EMRPOLYGON();
        polygon.cptl = points.Length;
        polygon.aptl = points;
        polygon.rclBounds = TOAPI.Types.Rectangle.Empty;

        PackCommand(polygon);
    }

    //public virtual void FramePolygon(PolygonG poly, Pen aPen)
    //{
    //    SaveState();

    //    Polygon(poly.Points);

    //    RestoreState();
    //}

    //public virtual void FillPolygon(PolygonG poly, Brush aBrush)
    //{
    //    SaveState();

    //    // Set the fill brush
    //    SelectObject(aBrush);

    //    Polygon(poly.Points);

    //    RestoreState();
    //}

    //public virtual void DrawPolygon(PolygonG poly, Pen aPen, Brush aBrush)
    //{
    //    // Set the frame pen
    //    SelectObject(aPen);

    //    // Set the fill brush
    //    SelectObject(aBrush);

    //    Polygon(poly.Points);
    //}


    //public virtual void DrawPolygon(Point[] points)
    //{
    //    // Select the current pen and brush
    //    Polygon(points);
    //}

    //public virtual void DrawPolygon(Point[] points, Pen aPen, Brush aBrush)
    //{
    //    SaveState();

    //    // Set the frame pen
    //    SelectObject(aPen);

    //    // Set the fill brush
    //    SelectObject(aBrush);

    //    // Select the current pen and brush
    //    Polygon(points);

    //    RestoreState();
    //}

    /// <summary>
    /// DrawImage, will draw the supplied image graphic on the screen.  It will use
    /// the Image's frame and size to determine where to display it.
    /// </summary>
    /// <param name="img"></param>
    public virtual void DrawBitmap(PixelBuffer img, Point origin)
    {
        DrawBitmap(img, new Rectangle(0, 0, img.Width, img.Height),
            new Rectangle(origin.x, origin.y, img.Width, img.Height));
    }

    public virtual void DrawBitmap(PixelBuffer img, Rectangle srcRect, Rectangle dstRect)
    {
        AlphaBlend(dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
                img.DCHandle, srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height,
                img.Alpha);
    }


    // Generalized bit block transfer
    // Can transfer from any device context to this one.
    public virtual void BitBlt(int x, int y, PixelBuffer pixBuff)
    {
        BitBlt(x, y, pixBuff.Width, pixBuff.Height,
            pixBuff.DCHandle, 0, 0, TernaryRasterOps.SRCCOPY);
    }

    public virtual bool BitBlt(int x, int y, int nWidth, int nHeight,
        IntPtr hSrcDC, int xSrc, int ySrc, TernaryRasterOps dwRop)
    {
        EMRBITBLT bitblt = new EMRBITBLT();

        bitblt.rclBounds = new TOAPI.Types.Rectangle(x,y,nWidth,nHeight);     // Bounding Rectangle in device coordinates
        bitblt.xDest = x;                       // Left edge of destination
        bitblt.yDest = y;                       // Top edge of destination
        bitblt.cxDest = nWidth;                 // Logical width of destination
        bitblt.cyDest = nHeight;                // Logical height of destination
        bitblt.dwRop = (uint)dwRop;             // Raster operation
        bitblt.xSrc = xSrc;                     // Logical X of source rectangle
        bitblt.ySrc = ySrc;                     // Logical Y of source rectangle
        bitblt.xformSrc = new XFORM();          // World space to page space transform
        bitblt.crBkColorSrc = RGBColor.Black;   // Background color of the source device context
        bitblt.iUsageSrc = GDI32.DIB_RGB_COLORS;// Either DIB_RGB_COLORS, or DBI_PAL_COLORS
        bitblt.offBitsSrc = 0;                  // Offset to source BITMAPINFO structure. 
        bitblt.cbBmiSrc = 0;                    // Size of source BITMAPINFO structure.
        bitblt.offBmiSrc = 0;                   // Offset to source bitmap bits.
        bitblt.cbBitsSrc = 0;                   // Size of source bitmap bits.

        PackCommand(bitblt);

        return true ;
    }


    public virtual bool AlphaBlend(int x, int y, int width, int height,
                IntPtr srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
    {
        BLENDFUNCTION blender = new BLENDFUNCTION(GDI32.AC_SRC_OVER, 0, alpha, 0);

        bool result = GDI32.AlphaBlend(this.DeviceContext, x, y, width, height,
            srchDC, srcX, srcY, srcWidth, srcHeight, blender);

        return result;
    }

    public virtual bool StretchBlt(int x, int y, int width, int height,
                IntPtr srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
                TernaryRasterOps dwRop)
    {
        bool result = GDI32.StretchBlt(this.DeviceContext, x, y, width, height,
            srchDC, srcX, srcY, srcWidth, srcHeight, dwRop);
        return result;
    }

    public virtual bool PlgBlt(Point[] lpPoint,
        IntPtr hdcSrc, int srcX, int srcY, int width, int height,
        IntPtr hbmMask, int xMask, int yMask)
    {
        bool result = GDI32.PlgBlt(this.DeviceContext, lpPoint,
            hdcSrc, srcX, srcY, width, height,
            hbmMask, xMask, yMask);

        return result;
    }


    // Dealing with a path
    public virtual bool BeginPath()
    {
        EMRBEGINPATH begin = new EMRBEGINPATH();
        PackCommand(begin);
        
        return true;
    }

    public virtual bool EndPath()
    {
        EMRENDPATH endPath = new EMRENDPATH();
        PackCommand(endPath);

        return true;
    }

    public virtual bool FramePath()
    {
        EMRSTROKEPATH strokePath = new EMRSTROKEPATH();
        strokePath.rclBounds = TOAPI.Types.Rectangle.Empty; // bounds for current closed path
        PackCommand(strokePath);

        return true;
    }

    public virtual bool FillPath()
    {
        EMRFILLPATH fillPath = new EMRFILLPATH();
        fillPath.rclBounds = TOAPI.Types.Rectangle.Empty;   // Bounds for path

        PackCommand(fillPath);

        return true;
    }

    public virtual bool DrawPath()
    {
        bool retValue = GDI32.StrokeAndFillPath(DeviceContext);
        return retValue;
    }

    public virtual bool SetPathAsClipRegion()
    {
        bool retValue = GDI32.SelectClipPath(DeviceContext, RegionCombineStyles.RGN_COPY);
        return retValue;
    }

    public virtual void SetMappingMode(MappingModes aMode)
    {
        EMRSETMAPMODE mapMode = new EMRSETMAPMODE();
        mapMode.iMode = (uint)aMode;

        PackCommand(mapMode);
    }

    public virtual void SetBkColor(uint colorref)
    {
        EMRSETBKCOLOR bkColor = new EMRSETBKCOLOR();
        bkColor.crColor = colorref;

        PackCommand(bkColor);
    }

    public virtual void SetBkMode(int bkMode)
    {
        EMRSETBKMODE bkmode = new EMRSETBKMODE();
        bkmode.iMode = (uint)bkMode;

        PackCommand(bkmode);
    }

    public virtual void SetPolyFillMode(int aMode)
    {
        EMRSETPOLYFILLMODE fillMode = new EMRSETPOLYFILLMODE();
        fillMode.iMode = (uint)aMode;

        PackCommand(fillMode);
    }

    // ViewPort Calls
    public virtual void SetViewportOrigin(int x, int y)
    {
        EMRSETVIEWPORTORGEX viewportorigin = new EMRSETVIEWPORTORGEX();
        viewportorigin.ptlOrigin = new Point(x, y);

        PackCommand(viewportorigin);
    }

    public virtual void SetViewportExtent(int width, int height)
    {
        EMRSETVIEWPORTEXTEX viewportext = new EMRSETVIEWPORTEXTEX();
        viewportext.szlExtent = new Size(width, height);

        PackCommand(viewportext);
    }


    // Window calls
    public virtual void SetWindowOrigin(int x, int y)
    {
        EMRSETWINDOWORGEX setwinorigin = new EMRSETWINDOWORGEX();
        setwinorigin.ptlOrigin = new Point(x, y);
        
        PackCommand(setwinorigin);
    }

    public virtual void OffsetWindowOrigin(int x, int y)
    {
        Point oldPoint = new Point();
        GDI32.OffsetWindowOrgEx(DeviceContext, x, y, ref oldPoint);
    }

    public virtual void SetWindowExtent(int width, int height)
    {
        EMRSETWINDOWEXTEX setwinext = new EMRSETWINDOWEXTEX();
        setwinext.szlExtent = new Size(width, height);

        PackCommand(setwinext);
    }

    public XFORM WorldTransform
    {
        get { return GetWorldTransform(); }

        set { SetWorldTransform(value); }
    }

    private XFORM GetWorldTransform()
    {
        XFORM aTransform = new XFORM();
        bool result = GDI32.GetWorldTransform(DeviceContext, out aTransform);

        return aTransform;
    }

    private bool SetWorldTransform(XFORM aTransform)
    {
        //bool result = GDI32.SetWorldTransform(DeviceContext, ref aTransform);

        //return result;
        return true;
    }

    public virtual Brush CreateBrush(int aStyle, int hatch, uint color, Guid uniqueID)
    {
        // Create an actual pen locally so we can add something
        // to the dictionary.
        Brush newBrush = new GDIBrush(aStyle, hatch, color, uniqueID);

        // Add the pen to a local dictionary based on the uniqueID
        // This is used when a SelectUniqueObject() is needed
        fObjectDictionary.Add(uniqueID, newBrush);

        EMRCREATEBRUSHINDIRECT emrBrush = new EMRCREATEBRUSHINDIRECT();
        emrBrush.ihBrush = (uint)newBrush.Handle.ToInt32();
        emrBrush.lb = newBrush.LogicalBrush;
        emrBrush.uniqueID = newBrush.UniqueID;

        PackCommand(emrBrush);

        return newBrush;
    }

    public virtual Pen CreatePen(int aStyle, int width, uint color, Guid uniqueID)
    {
        // Create an actual pen locally so we can add something
        // to the dictionary.
        Pen newPen = new GDIPen(aStyle, width, color, uniqueID);

        // Add the pen to a local dictionary based on the uniqueID
        // This is used when a SelectUniqueObject() is needed
        fObjectDictionary.Add(uniqueID, newPen);

        EMRCREATEPEN emrPen = new EMRCREATEPEN();
        emrPen.ihPen = newPen.Handle.ToInt32();
        emrPen.lopn = newPen.LogicalPen;
        emrPen.uniqueID = newPen.UniqueID;

        PackCommand(emrPen);

        return newPen;
    }


}
