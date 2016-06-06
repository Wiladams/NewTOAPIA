
namespace NewTOAPIA.Drawing
{
    using System;
    using System.ServiceModel;
    
    using NewTOAPIA.Graphics;

    #region Delegates
    // Drawing a single pixel
    public delegate void SetPixel(int x, int y, ColorRGBA colorref);

    // Drawing Lines
    public delegate void DrawLine(IPen aPen, Point2I startPoint, Point2I endPoint);
    public delegate void DrawLines(IPen aPen, Point2I[] points);


    // Drawing Rectangles
    public delegate void DrawRectangle(IPen aPen, RectangleI rect);
    public delegate void DrawRectangles(IPen aPen, RectangleI[] rects);

    public delegate void FillRectangle(IBrush aBrush, RectangleI rect);
    public delegate void FillRectangles(IBrush aBrush, RectangleI[] rects);

    // Drawing Rounded Rectangle
    public delegate void DrawRoundRect(IPen aPen, RectangleI rect, int xRadius, int yRadius);
    public delegate void FillRoundRect(IPen aPen, RectangleI rect, int xRadius, int yRadius);

    // Drawing Ellipse
    public delegate void DrawEllipse(IPen aPen, RectangleI rect);
    public delegate void FillEllipse(IBrush aBrush, RectangleI rect);


    // Drawing Bezier
    public delegate void DrawBezier(IPen aPen, Point2I startPoint, Point2I control1, Point2I control2, Point2I endPoint);
    public delegate void DrawBeziers(IPen aPen, Point2I[] points);

    // Drawing Polygon
    public delegate void DrawPolygon(IPen aPen, Point2I[] points);
    public delegate void FillPolygon(IBrush aBrush, Point2I[] points);
    public delegate void Polygon(Point2I[] points);

    // Drawing Paths
    public delegate void DrawPath(IPen aPen, GPath aPath);
    public delegate void FramePath(GPath aPath);
    public delegate void FillPath(IBrush aBrush, GPath aPath);

    // Drawing Regions
    //public delegate void FillRegion(IBrush brush, Region region);

    // Gradient fills
    //public delegate void DrawGradientRectangle(GradientRect aRect);

    // Drawing Text
    public delegate void DrawString(int x, int y, string aString);




    // Creating some objects
    public delegate void CreateCosmeticPen(PenStyle aStyle, uint color, Guid uniqueID);
    public delegate void CreateBrush(BrushStyle aStyle, HatchStyle hatch, uint color, Guid uniqueID);
    public delegate void CreateFont(string faceName, int height, Guid aGuid);

    // Setting some objects
    public delegate void SetBrush(IBrush aBrush);
    public delegate void SetPen(IPen aPen);
    public delegate void SetFont(IFont aFont);
    public delegate void SelectStockObject(int objectIndex);
    public delegate void SelectUniqueObject(Guid objectID);

    // State Management
    public delegate void Flush();
    public delegate void SaveState();
    public delegate void ResetState();
    public delegate void RestoreState(int relative);

    // Setting Attributes and modes
    public delegate void SetTextColor(uint colorref);
    public delegate void SetBkColor(uint colorref);
    public delegate void SetDefaultPenColor(uint colorref);
    public delegate void SetDefaultBrushColor(uint colorref);
    public delegate void UseDefaultPen();
    public delegate void UseDefaultBrush();

    // Setting some modes
    public delegate void SetBkMode(int aMode);
    public delegate void SetMappingMode(MappingModes aMode);
    public delegate void SetPolyFillMode(PolygonFillMode aMode);
    public delegate void SetROP2(BinaryRasterOps rasOp);

    // Viewport management
    public delegate void SetViewportExtent(int width, int height);
    public delegate void SetViewportOrigin(int x, int y);

    public delegate void SetWindowExtent(int width, int height);
    public delegate void SetWindowOrigin(int x, int y);
    public delegate void OffsetWindowOrigin(int cx, int cy);

    public delegate void SetClipRectangle(RectangleI clipRect);

    public delegate void SetWorldTransform(Transformation wTransform);
    public delegate void RotateTransform(float angle, int x, int y);
    public delegate void TranslateTransform(int dx, int dy);
    public delegate void ScaleTransform(float scaleX, float scaleY);
    #endregion

    [ServiceContract]
    public interface IDraw2D
    {
        // Drawing individual pixel
        [OperationContract(IsOneWay= true)]
        void SetPixel(int x, int y, ColorRGBA color);


        // Drawing Line
        [OperationContract(IsOneWay = true)]
        void DrawLine(IPen aPen, Point2I startPoint, Point2I endPoint);

        [OperationContract(IsOneWay = true)]
        void DrawLines(IPen aPen, Point2I[] points);

        // Drawing Rectangle
        [OperationContract(IsOneWay = true)]
        void DrawRectangle(IPen aPen, RectangleI rect);

        [OperationContract(IsOneWay = true)]
        void DrawRectangles(IPen aPen, RectangleI[] rects);

        [OperationContract(IsOneWay = true)]
        void FillRectangle(IBrush aPen, RectangleI rect);

        // Drawing Ellipse
        [OperationContract(IsOneWay = true)]
        void DrawEllipse(IPen aPen, RectangleI rect);

        [OperationContract(IsOneWay = true)]
        void FillEllipse(IBrush aBrush, RectangleI rect);

        [OperationContract(IsOneWay = true)]
        void DrawRoundRect(IPen aPen, RectangleI rect, int xRadius, int yRadius);


        // Drawing Bezier
        [OperationContract(IsOneWay = true)]
        void DrawBeziers(IPen aPen, Point2I[] points);

        // Drawing Polygon
        [OperationContract(IsOneWay = true)]
        void Polygon(Point2I[] points);

        // Drawing Paths
        [OperationContract(IsOneWay = true)]
        void DrawPath(IPen aPen, GPath aPath);
        
        [OperationContract(IsOneWay = true)]
        void FillPath(IBrush aBrush, GPath aPath);

        // These are nice to have, but a little complicated for a simple renderer
        //[OperationContract(IsOneWay = true)]
        //void DrawGradientRectangle(GradientRect aRect);

        // Drawing Text
        [OperationContract(IsOneWay = true)]
        void DrawString(int x, int y, string aString);
    }
}