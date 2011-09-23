
namespace NewTOAPIA.Drawing.GDI
{
    using System;
    using System.Drawing;
    
    using NewTOAPIA;
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    class RenderPixelBuffer : IRenderPixelBuffer
    {
        IPixelArray fDstAccess;

        public RenderPixelBuffer(IPixelArray dstAccess)
        {
            fDstAccess = dstAccess;
        }

        #region IRenderPixelBuffer
        public virtual void PixBlt(PixelArray pixArray, int x, int y)
        {
            // 1. Calculate the intersection intended destination rectangle
            // of the pixArray and the boundary of the pixelArray we're
            // holding onto.
            Rectangle srcRect = new Rectangle(x, y, pixArray.Width, pixArray.Height);

            // Create the boundary rectangle for our destination
            Rectangle dstRect = new Rectangle(0, 0, fDstAccess.Width, fDstAccess.Height);

            // Create the intersection of the dstRect and the srcRect
            srcRect.Intersect(dstRect);

            // If there is no intersection, then just return
            if (srcRect.IsEmpty)
                return;

            Rectangle srcBoundary = srcRect;
            srcBoundary.Offset(-x, -y);
            PixmapShardBlt(pixArray, srcBoundary, srcRect);

        }

        public virtual void PixmapShardBlt(PixelArray pixmap, Rectangle srcBoundary, Rectangle destinationRect)
        {
            int srcX = srcBoundary.X;
            int srcY = srcBoundary.Y;
            int width = srcBoundary.Width;
            int height = srcBoundary.Height;

            int dstX = destinationRect.X;
            int dstY = destinationRect.Y;

            // Now we have srcRect representing the fraction of the pixArray that we want to display.
            // It's been clipped to the boundary of the IPixelArray that we're holding onto

            // 2.  Copy the pixels from the source to our destination
            // Perform a simple color copy
            for (int column = 0; column < width; column++)
                for (int row = 0; row < height; row++)
                {
                    //ColorRGBA aColor = pixmap.GetColor(column + srcX, row + srcY);
                    //fDstAccess.SetColor(column + dstX, row + dstY, aColor);
                }
        }

        public virtual void PixmapShardBlend(IPixelArray pixmap, Rectangle srcBoundary, Rectangle destinationRect, float completion)
        {
            int srcX = srcBoundary.X;
            int srcY = srcBoundary.Y;
            int width = srcBoundary.Width;
            int height = srcBoundary.Height;

            int dstX = destinationRect.X;
            int dstY = destinationRect.Y;

            // Now we have srcRect representing the fraction of the pixArray that we want to display.
            // It's been clipped to the boundary of the IPixelArray that we're holding onto

            // 2.  Copy the pixels from the source to our destination
            // Perform a simple color copy
            for (int column = 0; column < width; column++)
                for (int row = 0; row < height; row++)
                {
                    ColorRGBA dstColor = fDstAccess.GetColor(column + dstX, row + dstY);
                    ColorRGBA srcColor = pixmap.GetColor(column + srcX, row + srcY);
                    ColorRGBA blendedColor = ColorRGBA.Interpolate(srcColor, dstColor, completion);
                    fDstAccess.SetColor(column + dstX, row + dstY, blendedColor);
                }
        }


        //public virtual void AlphaBlend(int x, int y, int width, int height,
        //            IPixelArray pixArray, int srcX, int srcY, int srcWidth, int srcHeight,
        //            byte alpha)
        public virtual void AlphaBlend(int x, int y, int width, int height,
                    GDIPixmap srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
                    byte alpha)
        {
            // The boundary rectangle is specified by the user
            //Rectangle srcBoundary = new Rectangle(srcX, srcY, srcWidth, srcHeight);

            //// Create the destination rectangle
            //Rectangle dstRect = new Rectangle(x, y, width, height);

            //// Create the intersection of the dstRect and the srcRect
            ////srcRect.Intersect(dstRect);

            //// If there is no intersection, then just return
            ////if (srcRect.IsEmpty)
            ////    return;

            //PixmapShardBlend(pixArray, srcBoundary, dstRect, alpha / 255.0f);
        }


        public virtual void ScaleBitmap(GDIPixmap aBitmap, Rectangle aFrame)
        {
        }

        public virtual void DrawImage(GDIPixmap bitmap, System.Drawing.Point[] destinationParallelogram,
            System.Drawing.Rectangle srcRect,
            System.Drawing.GraphicsUnit units)
        {
        }
        #endregion

    }
}
