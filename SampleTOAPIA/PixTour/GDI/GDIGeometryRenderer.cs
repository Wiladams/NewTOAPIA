using System;
using System.Runtime.InteropServices;
using System.Text;  // For StringBuilder
using System.Collections.Generic;

using TOAPI.GDI32;
using TOAPI.Types;


/// <summary>
/// A bit about drawing with GDI32.  There are several parameters that determine
/// exactly what's going to get drawn on the screen
///					RasterOp	Pen		Brush	Background Color		Foreground
/// Line Drawing		X		X
/// Curve Drawing
/// Framing
/// 
/// </summary>
public class GDIGeometryRenderer : IRenderGDI
{
	GDIDeviceContext fDeviceContext;
    Dictionary<Guid, IUniqueGDIObject> fObjectDictionary;

    public GDIGeometryRenderer(GDIDeviceContext devContext)
    {
        fObjectDictionary = new Dictionary<Guid,IUniqueGDIObject>();
        fDeviceContext = devContext;
        GDI32.SetGraphicsMode(fDeviceContext.Handle, GDI32.GM_ADVANCED);  // Use advanced graphics mode so we can use world transforms
    }

    //public GDIGeometryRenderer(IntPtr hDC)
    //{
    //    GDI32.SetGraphicsMode(hDC, GDI32.GM_ADVANCED);  // Use advanced graphics mode so we can use world transforms

    //    fDeviceContext = hDC;


    //    // Create the device context specific default pen and brush
    //    //fDCPen = new GDI32DCPen(DeviceContext, RGBColor.Black);
    //    //fDCBrush = new GDI32DCBrush(DeviceContext, RGBColor.Black);

    //    //BackgroundMixMode = BackgroundMixMode.TRANSPARENT;
    //}

    public virtual Brush CreateBrush(int aStyle, int hatch, uint colorref, Guid uniqueID)
    {
        GDIBrush aBrush = new GDIBrush(aStyle, hatch, colorref, uniqueID);
        
        return aBrush;
    }

    public virtual Pen CreatePen(int aStyle, int width, uint color, Guid uniqueID)
    {
        GDIPen aPen = new GDIPen(aStyle, width, color, uniqueID);
        fObjectDictionary.Add(uniqueID, aPen);

        return aPen;
    }

    public virtual PixelBuffer CreateBitmap(int width, int height)
    {
        PixelBuffer newOne = new PixelBuffer(width, height);
        
        return newOne;
    }



	public IntPtr HDC
	{
		get 
		{
			return fDeviceContext.Handle;
		}
	}

    public GDIDeviceContext DeviceContext
    {
        get { return fDeviceContext; }
    }


	/// Some GraphPort control things
    /// 

    public virtual void SaveState()
    {
        DeviceContext.SaveState();
    }

    public virtual void RestoreState()
    {
        DeviceContext.RestoreState();
    }

    public virtual void RestoreState(int relative)
    {
        DeviceContext.RestoreState(relative);
    }
    
    public virtual void Flush()
	{
		GDI32.GdiFlush();			
	}

    public virtual void SelectObject(IntPtr objectHandle)
    {
        GDI32.SelectObject(HDC, objectHandle);
    }

    public virtual void SelectObject(Pen aPen)
    {
        SelectObject(aPen.Handle);
    }

    public virtual void SelectObject(Brush aBrush)
    {
        SelectObject(aBrush.Handle);
    }

    public virtual void SelectObject(Font aFont)
    {
        SelectObject(aFont.Handle);
    }

    public virtual void SelectUniqueObject(Guid objectID)
    {
        // Lookup the object in the dictionary
        Pen aPen = (Pen)fObjectDictionary[objectID];

        // Select it based on it's handle
        if (aPen != null)
            SelectObject(aPen);
    }

    public virtual void SelectStockObject(int objectIndex)
    {
        // First get a handle on the object
        IntPtr objHandle = GDI32.GetStockObject(objectIndex);

        // Then select it into the device context
        GDI32.SelectObject(HDC, objHandle);
    }

	// Dealing with clipping region
	public virtual void SetClip(Rectangle clipRect)
	{
	    GDI32.SetBoundsRect(HDC, ref clipRect, 	GDI32.DCB_SET);
	}

	public virtual void SetClip(Region clipRegion)
	{
		
	}

	public virtual void ResetClip()
	{
        GDI32.SetBoundsRect(HDC, IntPtr.Zero, GDI32.DCB_RESET);
		
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
		GDI32.SetPixelV(HDC, x, y, colorref);
	}


    public virtual bool BlendPixel(Point aPoint, uint colorref, byte alpha)
    {
        return false;
    }

	public virtual uint GetPixel(int x, int y)
	{
		return GDI32.GetPixel(HDC, x, y);
	}

    public virtual void MoveTo(int x, int y)
    {
        GDI32.MoveToEx(HDC, x, y, IntPtr.Zero);
    }

    public virtual void MoveTo(Point aPoint)
    {
        GDI32.MoveToEx(HDC, aPoint.x, aPoint.y, IntPtr.Zero);
    }

    public virtual void LineTo(int x, int y)
    {
        GDI32.LineTo(HDC, x, y);
    }

    public virtual void LineTo(Point toPoint)
    {
        LineTo(toPoint.x, toPoint.y);
    }

	public virtual void DrawLine(LineG aLine, Pen pen)
	{
        SaveState();

		SelectObject(pen);

		MoveTo(aLine.StartPoint.x, aLine.StartPoint.y);
		LineTo(aLine.EndPoint);

        RestoreState();
    }

    public virtual void DrawLine(Point startPoint, Point endPoint)
    {
        MoveTo(startPoint.x, startPoint.y);
        LineTo(endPoint);
    }

	/// DrawLine
	// Assume we want to draw a line using the currently selected
	// Pen, whichever that may be, and the currently selected color
	public virtual void DrawLine(int x1, int y1, int x2, int y2)
	{
        MoveTo(x1, y1);
        LineTo(new Point(x2, y2));
	}

    public virtual void RoundRect(int left, int top, int right, int bottom, int width, int height)
    {
        GDI32.RoundRect(this.HDC, left, top, right, bottom, width, height);
    }

	/// <summary>
	/// Draw the frame of a rectangle using a pen.  Leave the interior alone.
	/// </summary>
	/// <param name="aRect">The rectangle to be drawn</param>
	/// <param name="pen">The pen used to draw the frame</param>
    /// 
    public virtual void Rectangle(int left, int top, int right, int bottom)
    {
        GDI32.Rectangle(this.HDC, left, top, right, bottom);
    }

    public virtual void Rectangle(Rectangle aRect)
    {
        Rectangle(aRect.Left, aRect.Top, aRect.Right, aRect.Bottom);
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

        // Draw the rectangle
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

	/// DrawEllipse
    /// 
    public virtual void Ellipse(int left, int top, int right, int bottom)
    {
        GDI32.Ellipse(HDC, left, top, right, bottom);
    }

    public virtual void FrameEllipse(EllipseG aEllipse, Pen aPen)
    {
        SaveState();
        Ellipse(
            aEllipse.Center.x - aEllipse.XRadius,
            aEllipse.Center.y - aEllipse.YRadius,
            aEllipse.Center.x + aEllipse.XRadius,
            aEllipse.Center.y + aEllipse.YRadius);
        
        RestoreState();
    }

    public virtual void FillEllipse(EllipseG aEllipse, Brush aBrush)
    {
        SaveState();
        Ellipse(
            aEllipse.Center.x - aEllipse.XRadius,
            aEllipse.Center.y - aEllipse.YRadius,
            aEllipse.Center.x + aEllipse.XRadius,
            aEllipse.Center.y + aEllipse.YRadius);

        RestoreState();
    }
    
    public virtual void DrawEllipse(EllipseG aEllipse, Pen aPen, Brush aBrush)
	{
        SaveState();

        // Set the frame pen
        SelectObject(aPen);

        // Set the fill brush
        SelectObject(aBrush);

        // Draw the ellipse
        Ellipse( 
            aEllipse.Center.x - aEllipse.XRadius,
            aEllipse.Center.y - aEllipse.YRadius,
            aEllipse.Center.x + aEllipse.XRadius,
            aEllipse.Center.y + aEllipse.YRadius);
    
        RestoreState();
    }

    public virtual void GradientFill(TRIVERTEX[] pVertex, GRADIENT_RECT[] pMesh, uint dwMode)
    {
        GDI32.GradientFill(HDC, pVertex, (uint)pVertex.Length, pMesh, (uint)pMesh.Length, dwMode);
    }

    public virtual void GradientFill(TRIVERTEX[] pVertex, GRADIENT_TRIANGLE[] pMesh, uint dwMode)
    {
        GDI32.GradientFill(HDC, pVertex, (uint)pVertex.Length, pMesh, (uint)pMesh.Length, dwMode);
    }

    public virtual void SetTextColor(uint colorref)
    {
        GDI32.SetTextColor(HDC, colorref);
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
        GDI32.TextOut(HDC, x, y, aString, aString.Length);
    }

    //public virtual void DrawString(TextRunG textRun, Font aFont, Pen aPen, Brush aBrush)
    //{
    //    SaveState();

    //    // Set the Font
    //    GDI32.SelectObject(DeviceContext, aFont.Handle);

    //    // Set the brush
    //    // set the pen
    //    GDI32.TextOut(DeviceContext, textRun.Origin.X, textRun.Origin.Y, textRun.String, textRun.String.Length);

    //    RestoreState();
    //}

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
		int[] aBuffer = new int [1];

		if (GDI32.GetCharWidth32(HDC, (uint)c, (uint)c, out aBuffer))
			width = aBuffer[0];
		else
			width = 0;

		//MeasureString(new string(c,1), ref width, ref height);
	}

    public virtual void PolyBezier(Point[] points)
    {
        GDI32.PolyBezier(HDC, points, points.Length);
    }

    public virtual void PolyLine(Point[] points)
    {
        GDI32.Polyline(HDC, points, points.Length);
    }

    public virtual void PolyDraw(Point[] apt, byte[] aj)
    {
        GDI32.PolyDraw(HDC, apt, aj, apt.Length);
    }

	// Draw a Polygon
    public virtual void Polygon(Point[] points)
    {
        GDI32.Polygon(HDC, points, points.Length);
    }

    public virtual void FramePolygon(PolygonG poly, Pen aPen)
    {
        SaveState();

        Polygon(poly.Points);

        RestoreState();
    }
    
    public virtual void FillPolygon(PolygonG poly, Brush aBrush)
    {
        SaveState();

        // Select the current pen and brush
        GDI32.Polygon(HDC, poly.Points, poly.Points.Length);

        RestoreState();
    }

    public virtual void DrawPolygon(PolygonG poly, Pen aPen, Brush aBrush)
    {
        DrawPolygon(poly.Points, aPen, aBrush);
    }

    public virtual void DrawPolygon(Point[] points)
    {
        // Select the current pen and brush
        GDI32.Polygon(HDC, points, points.Length);
    }

    public virtual void DrawPolygon(Point[] points, Pen aPen, Brush aBrush)
    {
        SaveState();

        // Set the brush
        // Set the pen

        // Select the current pen and brush
        GDI32.Polygon(HDC, points, points.Length);

        RestoreState();
    }

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
    public virtual void BitBlt(int x, int y, PixelBuffer bitmap)
    {
        bool retValue = BitBlt(x, y, bitmap.Width, bitmap.Height, 
            bitmap.DCHandle, 0, 0, TernaryRasterOps.SRCCOPY);
    }

	public virtual bool BitBlt(int x, int y, int nWidth, int nHeight,
        IntPtr hSrcDC, int xSrc, int ySrc, TernaryRasterOps dwRop)
	{
		bool result = GDI32.BitBlt(this.HDC, x, y, nWidth, nHeight,
							 hSrcDC, xSrc, ySrc, dwRop);
		return result;
	}


	public virtual bool AlphaBlend(int x, int y, int width, int height,
				IntPtr srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
				byte alpha)
	{
		BLENDFUNCTION blender = new BLENDFUNCTION(GDI32.AC_SRC_OVER, 0, alpha, 0);

		bool result = GDI32.AlphaBlend(this.HDC, x, y, width, height,
			srchDC, srcX, srcY, srcWidth, srcHeight, blender);

		return result;
	}

	public virtual bool StretchBlt(int x, int y, int width, int height,
				IntPtr srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
                TernaryRasterOps dwRop)
	{
		bool result = GDI32.StretchBlt(this.HDC, x, y, width, height,
			srchDC, srcX, srcY, srcWidth, srcHeight, dwRop);
		return result;
	}

	public virtual bool PlgBlt(Point[] lpPoint,
		IntPtr hdcSrc, int srcX, int srcY, int width, int height,
		IntPtr hbmMask, int xMask, int yMask)
	{
		bool result = GDI32.PlgBlt(this.HDC, lpPoint, 
			hdcSrc, srcX, srcY, width, height, 
			hbmMask, xMask, yMask);

			return result;
	}


    // Dealing with a path
    public virtual bool BeginPath()
    {
        bool retValue = GDI32.BeginPath(HDC);
        return retValue;
    }

    public virtual bool EndPath()
    {
        bool retValue = GDI32.EndPath(HDC);
        return retValue;
    }

    public virtual bool FramePath()
    {
        bool retValue = GDI32.StrokePath(HDC);
        return retValue;
    }

    public virtual bool FillPath()
    {
        bool retValue = GDI32.FillPath(HDC);
        return retValue;
    }
    
    public virtual bool DrawPath()
    {
        bool retValue = GDI32.StrokeAndFillPath(HDC);
        return retValue;
    }

    public virtual bool SetPathAsClipRegion()
    {
        bool retValue = GDI32.SelectClipPath(HDC, RegionCombineStyles.RGN_COPY);
        return retValue;
    }

    public virtual void SetMappingMode(MappingModes aMode)
    {
        int result = GDI32.SetMapMode(HDC, aMode);
    }

    // ViewPort Calls
    public virtual void SetViewportOrigin(int x, int y)
    {
        GDI32.SetViewportOrgEx(HDC, x, y, IntPtr.Zero);
    }

    public virtual void SetViewportExtent(int width, int height)
    {
        GDI32.SetViewportExtEx(HDC, width, height, IntPtr.Zero);
    }


    // Window calls
    public virtual void SetWindowOrigin(int x, int y)
    {
        GDI32.SetWindowOrgEx(HDC, x, y, IntPtr.Zero);
    }

    public virtual void OffsetWindowOrigin(int x, int y)
    {
        GDI32.OffsetWindowOrgEx(HDC, x, y, IntPtr.Zero);
    }

    public virtual Size SetWindowExtent(int width, int height)
    {
        Size oldSize = new Size();
        GDI32.SetWindowExtEx(HDC, width, height, ref oldSize);

        return oldSize;
    }

    public XFORM WorldTransform
    {
        get { return GetWorldTransform(); }

        set { SetWorldTransform(value); }
    }

    private XFORM GetWorldTransform()
    {
        XFORM aTransform = new XFORM();
        bool result = GDI32.GetWorldTransform(HDC, out aTransform);

        return aTransform;
    }

    private  bool SetWorldTransform(XFORM aTransform)
    {
        bool result = GDI32.SetWorldTransform(HDC, ref aTransform);

        return result;
    }

    public virtual bool TranslateTransform(int dx, int dy)
    {
        // Get the current transform
        XFORM oldTransform = GetWorldTransform();

        // Put it into an Affine so that we can change it
        Affine anAffine = new Affine();
        anAffine.fMatrix = oldTransform;
        anAffine.Translate((float)dx, (float)dy);

        // Set it back on the graph port
        bool result = SetWorldTransform(anAffine.fMatrix);

        return result;
    }

    public virtual void SetBkColor(uint colorref)
    {
        GDI32.SetBkColor(HDC, colorref);
    }

    public virtual void SetBkMode(int bkMode)
    {
        GDI32.SetBkMode(HDC, bkMode);
    }

    public virtual void SetPolyFillMode(int fillMode)
    {
        GDI32.SetPolyFillMode(HDC, fillMode);
    }


    public virtual void SetDefaultPenColor(uint colorref)
    {
        GDI32.SetDCPenColor(HDC, colorref);
    }

    public virtual void SetPen(Pen aPen)
    {
        SelectObject(aPen);
    }

    public virtual void UseDefaultPen()
    {
        SelectObject(GDI32.GetStockObject(GDI32.DC_PEN));
    }




    public virtual void SetDefaultBrushColor(uint colorref)
    {
        GDI32.SetDCBrushColor(HDC, colorref);
    }


    public virtual void UseDefaultBrush()
    {
        SelectObject(GDI32.GetStockObject(GDI32.DC_BRUSH));

    }
}
