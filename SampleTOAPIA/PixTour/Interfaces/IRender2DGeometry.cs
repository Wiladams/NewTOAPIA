using System;

using TOAPI.Types;

public interface IRender2DGeometry : IRenderGDI
{
    /// DrawLine
    void DrawLine(Point startPoint, Point endPoint);
    void DrawLine(LineG aLine, Pen aPen);

    /// DrawRectangle
    void FrameRectangle(Rectangle rect, Pen aPen);
    void FillRectangle(Rectangle rect, Brush aBrush);
    void DrawRectangle(Rectangle rect, Pen aPen, Brush aBrush);

    /// DrawEllipse
    void FrameEllipse(EllipseG anElipse, Pen aPen);
    void FillEllipse(EllipseG anEllipse, Brush aBrush);
    void DrawEllipse(EllipseG anEllipse, Pen aPen, Brush aBrush);

    /// Drawing glyphs
    /// 
    //void DrawGlyphRun(GlyphRunG aGlyphRun, Pen aPen, Brush aBrush);

    // Draw a Polygon
    void FramePolygon(PolygonG poly, Pen aPen);
    void FillPolygon(PolygonG poly, Brush aBrush);
    void DrawPolygon(PolygonG poly, Pen aPen, Brush aBrush);

    /// Draw bitmaps
    /// 
    void DrawBitmap(PixelBuffer anImage, Point location);
    void DrawBitmap(PixelBuffer anImage, Rectangle srcRect, Rectangle dstRect);

}
