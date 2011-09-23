using System;

using TOAPI.Types;

public interface IRenderGDI
{
    void SetPixel(int x, int y, uint colorref);

    void MoveTo(int x, int y);
    void MoveTo(Point aPoint);

    void LineTo(int x, int y);
    void LineTo(Point aPoint);

    void Rectangle(int left, int top, int right, int bottom);
    void Rectangle(Rectangle aRect);

    void Ellipse(int left, int top, int right, int bottom);

    void RoundRect(int left, int top, int right, int bottom, int width, int height);
    //void Arc(int left, int top, int right, int bottom, 
    //    int xStartArc, int yStartArc, int xEndArc, int yEndArc);
    //void ArcTo(int left, int top, int right, int bottom,
    //    int xRadial1, int yRadial1, int xRadial2, int yRadial2);

    void Polygon(Point[] points);
    void PolyLine(Point[] points);
    void PolyBezier(Point[] points);
    void PolyDraw(Point[] apt, byte[] aj);

    // Gradient fills
    void GradientFill(TRIVERTEX[] pVertex, GRADIENT_RECT[] pMesh, uint dwMode);
    void GradientFill(TRIVERTEX[] pVertex, GRADIENT_TRIANGLE[] pMesh, uint dwMode);

    // Drawing Text
    void DrawString(int x, int y, string aString);

    /// Draw bitmaps
    void BitBlt(int x, int y, PixelBuffer aBitmap);
    //void DrawBitmap(PixelBuffer anImage, Rectangle srcRect, Rectangle dstRect);

    // Creating some objects
    Pen CreatePen(int aStyle, int width, uint color, Guid uniqueID);
    Brush CreateBrush(int aStyle, int hatch, uint color, Guid uniqueID);

    // Setting some objects
    //void SetBrush(Brush aBrush);
    void SetPen(Pen aPen);
    //void SetFont(Font aFont);
    void SelectStockObject(int objectIndex);
    void SelectUniqueObject(Guid objectID);

    // State Management
    void Flush();
    void SaveState();
    void RestoreState();
    void RestoreState(int relative);

    // Setting Attributes and modes
    void SetTextColor(uint colorref);
    void SetBkColor(uint colorref);
    void SetDefaultPenColor(uint colorref);
    void SetDefaultBrushColor(uint colorref);
    void UseDefaultPen();
    void UseDefaultBrush();

    // Setting some modes
    void SetBkMode(int aMode);
    void SetMappingMode(MappingModes aMode);
    void SetPolyFillMode(int aMode);

    // Viewport management
    void SetViewportExtent(int width, int height);
    void SetViewportOrigin(int x, int y);

    void OffsetWindowOrigin(int x, int y);

}
