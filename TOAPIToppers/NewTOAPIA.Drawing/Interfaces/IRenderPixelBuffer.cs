

namespace NewTOAPIA.Drawing
{
    using System;
    using System.ServiceModel;

    using NewTOAPIA.Graphics;

    #region Delegate Definitions
    /// Draw bitmaps
    public delegate void PixBlt(PixelArray aBitmap, int x, int y);
    public delegate void PixmapShardBlt(PixelArray pixmap, RectangleI srcRect, RectangleI dstRect);

    //public delegate void BitBlt(int x, int y, PixelBuffer aBitmap);

    //public delegate void AlphaBlend(int x, int y, int width, int height,
    //            GDIPixmap bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
    //            byte alpha);

    //public delegate void ScaleBitmap(GDIPixmap aBitmap, Rectangle aFrame);


    //public delegate void OnBitBltEventHandler(int x, int y, GDIPixmap aBitmap);
    //public delegate void OnAlphaBlendEventHandler(int x, int y, int width, int height,
    //            GDIPixmap bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
    //            byte alpha);
    //public delegate void OnScaleBitmapEventHandler(GDIPixmap aBitmap, Rectangle aFrame);
    #endregion


    [ServiceContract]
    public interface IRenderPixelBuffer
    {
        /// Draw bitmaps
        /// 
        [OperationContract(IsOneWay = true)]
        void PixBlt(PixelArray aBitmap, int x, int y);

        //[OperationContract(IsOneWay = true)]
        //void PixmapShardBlt(IPixelArray pixmap, Rectangle srcRect, Rectangle dstRect);

        //[OperationContract(IsOneWay = true)]
        //void AlphaBlend(int x, int y, int width, int height,
        //    GDIPixmap srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
        //    byte alpha);

        //[OperationContract(IsOneWay = true)]
        //void ScaleBitmap(GDIPixmap aBitmap, Rectangle aFrame);

        //[OperationContract(IsOneWay = true)]
        //void DrawImage(GDIPixmap bitmap, Point[] destinationParallelogram, 
        //    Rectangle srcRect, GraphicsUnit units);
    }

    //[ServiceContract]
    //public interface IRenderGDIPixmap
    //{
    //    [OperationContract(IsOneWay = true)]
    //    void CopyPixels(GDIPixmap pixMap, Point srcPoint);

    //    [OperationContract(IsOneWay = true)]
    //    void PixBlt(GDIPixmap pixMap, Point srcPoint, Rectangle dstRect, TernaryRasterOps rasterOp);

    //    [OperationContract(IsOneWay = true)]
    //    void AlphaBlend(GDIPixmap pixMap, Rectangle srcRect, Rectangle dstRect, byte alpha);
    //}
}
