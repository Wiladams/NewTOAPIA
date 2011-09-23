using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.GDI32;
using TOAPI.Types;

namespace NewTOAPIA.Drawing.GDI
{
    using NewTOAPIA.Graphics;

    public partial class GDIContext
    {
        BLENDFUNCTION blender = new BLENDFUNCTION(GDI32.AC_SRC_OVER, 0, 255, 0);
        IGraphPort m_GraphPort;

        public IGraphPort GraphPort
        {
            get
            {
                if (m_GraphPort == null)
                    m_GraphPort = new GDIRenderer(this);

                return m_GraphPort;
            }
        }

        #region Pen Movement Management
        public virtual void MoveTo(int x, int y)
        {
            GDI32.MoveToEx(this, x, y, IntPtr.Zero);
        }
        #endregion

        #region Drawing Pixels
        // Although these pixel accessing functions are included in
        // the API, they probably should not be used.  They use
        // a temporary bitmap, and do a bitblt for each pixel, which
        // is fairly expensive.

        /// <summary>
        /// Retrieves a single pixel value from the Device Context.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public virtual UInt32 GetPixel(int x, int y)
        {
            return GDI32.GetPixel(this, x, y);
        }

        public virtual UInt32 SetPixel(int x, int y, UInt32 colorref)
        {
            return (UInt32)GDI32.SetPixel(this, x, y, colorref);
        }
        #endregion

        #region Drawing Lines
        public virtual void LineTo(int x, int y)
        {
            GDI32.LineTo(this, x, y);
        }

        public virtual void PolyLine(POINT[] points)
        {
            GDI32.Polyline(this, points, points.Length);
        }

        public virtual void PolyLineTo(POINT[] points)
        {
            GDI32.PolylineTo(this, points, points.Length);
        }

        public virtual void PolyPolyLine(POINT[] points, int[] polypoints, int nCount)
        {
            GDI32.PolyPolyline(this, points, polypoints, nCount);
        }
        #endregion

        #region Drawing Bezier
        public virtual void PolyBezier(POINT[] pts)
        {
            bool retValue = GDI32.PolyBezier(this, pts, (uint)pts.Length);
        }

        public virtual void PolyBezierTo(POINT[] pts)
        {
            bool retValue = GDI32.PolyBezierTo(this, pts, (uint)pts.Length);
        }
        #endregion

        #region Drawing Arcs
        public virtual void SetArcDirection(int direction)
        {
            GDI32.SetArcDirection(this, direction);
        }

        public virtual void AngleArc(int cx, int cy, int radius, float startD, float sweepD)
        {
            
            GDI32.AngleArc(this, cx, cy, (uint)radius, startD, sweepD);
        }
        #endregion

        public virtual bool FloodFill(int x, int y, uint colorref, FloodFillType floodtype)
        {
            bool success = GDI32.ExtFloodFill(this, x, y, colorref, (uint)floodtype);
            return success;
        }




        #region Drawing Rectangles
        public virtual void Rectangle(int left, int top, int right, int bottom)
        {
            GDI32.Rectangle(this, left, top, right, bottom);
        }

        public virtual void RoundRect(int left, int top, int right, int bottom, int xRadius, int yRadius)
        {
            GDI32.RoundRect(this, left, top, right, bottom, xRadius, yRadius);
        }
        #endregion

        #region Drawing Bitmaps
        public virtual bool AlphaBlend(GDIContext srcDC, RectangleI srcRect, RectangleI dstRect, byte opacity)
        {
            blender.SourceConstantAlpha = opacity;

            bool success = GDI32.AlphaBlend(this, dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
                srcDC, srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height, blender);

            return success;
        }

        public virtual bool BitBlt(GDIContext srcDC, int srcX, int srcY, RectangleI dstRect, TernaryRasterOps dwRop)
        {
            bool success = GDI32.BitBlt(this, dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
                                 srcDC, srcX, srcY, (int)dwRop);

            return success;
        }

        public virtual bool PatBlt(int x, int y, int width, int height, TernaryRasterOps rasterOp)
        {
            bool success = GDI32.PatBlt(this, x, y, width, height, (int)rasterOp);

            return success;
        }

        public virtual bool PlgBlt(GDIContext srcDC, RectangleI srcRect,
            POINT[] dstParallelogramPoints,
            IntPtr hbmMask, int xMask, int yMask)
        {
            bool result = GDI32.PlgBlt(this, dstParallelogramPoints,
                srcDC, srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height,
                hbmMask, xMask, yMask);

            return result;
        }

        public virtual bool StretchBlt(GDIContext srcDC, RectangleI srcRect, RectangleI dstRect, TernaryRasterOps dwRop)
        {
            bool result = GDI32.StretchBlt(this, dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
                srcDC, srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height, (int)dwRop);

            return result;
        }

        public virtual int PixelBlt(RectangleI srcRect, RectangleI dstRect, IntPtr pixelPtr, BitCount bitsPerPixel)
        {

            BITMAPINFO bmInfo = new BITMAPINFO();
            bmInfo.Init();
            bmInfo.bmiHeader.biWidth = srcRect.Width;
            bmInfo.bmiHeader.biHeight = srcRect.Height;
            bmInfo.bmiHeader.biPlanes = 1;
            bmInfo.bmiHeader.biBitCount = (ushort)bitsPerPixel;
            bmInfo.bmiHeader.biClrImportant = 0;
            bmInfo.bmiHeader.biClrUsed = 0;
            bmInfo.bmiHeader.biCompression = GDI32.BI_RGB;
            //bmInfo.bmiColors = IntPtr.Zero;

            int rowsWritten = GDI32.StretchDIBits(this,
                dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
                srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height,
                pixelPtr,
                ref bmInfo,
                GDI32.DIB_RGB_COLORS,
                (uint)TernaryRasterOps.SRCCOPY);

            return rowsWritten;
        }

        //public virtual int StretchDIBits(GDIDIBitmap bitmap, Rectangle srcRect, Rectangle dstRect, TernaryRasterOps rasterOp)
        //{
        //    if (null == bitmap)
        //        return 0;

        //    int scansDrawn = GDI32.StretchDIBits(GetHdc(), dstRect.Left, dstRect.Top, dstRect.Width, dstRect.Height,
        //        srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height,
        //        bitmap.Pixels, ref bitmap.fBitmapInfo,
        //        0, (uint)rasterOp);

        //    return scansDrawn;
        //}


        #endregion



        #region Clearing to Black and White
        /// <summary>
        /// Fills the entire context with white pixels.  This is most useful when clearing a bitmap, but 
        /// it can be used for any context.
        /// </summary>
        public void ClearToWhite()
        {
            FillRectangleWithWhite(new RectangleI(new Point2I(0, 0), SizeInPixels));
        }

        /// <summary>
        /// Fill a particular rectangle with white pixels.  Similar to the ClearToWhite call,
        /// but takes a specific Rectangle as the area to be filled.
        /// </summary>
        /// <param name="rect"></param>
        public void FillRectangleWithWhite(RectangleI rect)
        {
            GDI32.PatBlt(this, rect.Left, rect.Top, rect.Width, rect.Height, (int)PatternBlitOps.WHITENESS);
        }

        public void ClearToBlack()
        {
            FillRectangleWithBlack(new RectangleI(new Point2I(0, 0), SizeInPixels));
        }

        public void FillRectangleWithBlack(RectangleI rect)
        {
            GDI32.PatBlt(this, rect.Left, rect.Top, rect.Width, rect.Height, (int)PatternBlitOps.BLACKNESS);
        }

        #endregion

        #region Path Management
        public virtual void BeginPath()
        {
            bool retValue = GDI32.BeginPath(this);
        }

        public virtual void EndPath()
        {
            bool retValue = GDI32.EndPath(this);
        }

        public virtual void FillPath()
        {
            bool retValue = GDI32.FillPath(this);
        }

        public virtual void StrokePath()
        {
            bool retValue = GDI32.StrokePath(this);
        }

        public virtual void StrokeAndFillPath()
        {
            bool retValue = GDI32.StrokeAndFillPath(this);
        }



        public virtual void FlattenPath()
        {
            bool retValue = GDI32.FlattenPath(this);
        }

        public virtual void SetPathAsClipRegion()
        {
            bool retValue = GDI32.SelectClipPath(this, (int)RegionCombineStyles.Copy);
        }

        public virtual void ReplayPath(GPath aPath)
        {
            BeginPath();

            bool closeFigure = false;

            for (int i = 0; i < aPath.Commands.Length; i++)
            {
                byte command = aPath.Commands[i];
                closeFigure = (command & GDI32.PT_CLOSEFIGURE) != 0;
                if (closeFigure)
                    command -= GDI32.PT_CLOSEFIGURE;

                switch (command)
                {
                    case GDI32.PT_MOVETO:
                        GDI32.MoveToEx(this, (int)aPath.Vertices[i].x, (int)aPath.Vertices[i].y, IntPtr.Zero);
                        break;

                    case GDI32.PT_LINETO:
                        GDI32.LineTo(this, (int)aPath.Vertices[i].x, (int)aPath.Vertices[i].y);
                        break;

                    case GDI32.PT_BEZIERTO:
                        // Consume 2 more points to call BezierTo
                        POINT[] pts = new POINT[3];
                        pts[0] = new POINT((int)aPath.Vertices[i].x, (int)aPath.Vertices[i].y);
                        pts[1] = new POINT((int)aPath.Vertices[i + 1].x, (int)aPath.Vertices[i + 1].y);
                        pts[2] = new POINT((int)aPath.Vertices[i + 2].x, (int)aPath.Vertices[i + 1].y);
                        PolyBezierTo(pts);
                        i += 2;
                        break;
                }
            }

            if (closeFigure)
                EndPath();
        }

        #endregion

        #region Drawing Regions
        public enum RegionType
        {
            CLIPRGN = 1,
            METARGN = 2,
            APIRGN = 3,
            SYSRGN = 4,
        }

        public GDIRegion GetRegion(RegionType rgn)
        {
            IntPtr rgnHandle = IntPtr.Zero;

            //rgnHandle = GDI32.GetRandomRgn(this, rgnHandle, (int)rgn);
            //GDI32.GetRegionData(rgnHandle, 

            return null;
        }

        public virtual void DrawRegion(GDIRegion region)
        {
            bool success = GDI32.PaintRgn(this, region);
        }

        public virtual void FillRegion(GDIRegion region, GDIBrush aBrush)
        {
            bool success = GDI32.FillRgn(this, region, aBrush.DangerousGetHandle());
        }

        public virtual void FrameRegion(GDIRegion region, GDIBrush aBrush, Size2I strokeSize)
        {
            bool success = GDI32.FrameRgn(this, region, aBrush.DangerousGetHandle(), strokeSize.width, strokeSize.height);
        }

        public virtual void InvertRegion(GDIRegion region)
        {
            bool success = GDI32.InvertRgn(this, region);
        }
        #endregion


        #region Viewport Management
        // ViewPort Calls
        public virtual void SetViewportOrigin(int x, int y)
        {
            GDI32.SetViewportOrgEx(this, x, y, IntPtr.Zero);
        }

        public virtual void SetViewportExtent(int width, int height)
        {
            GDI32.SetViewportExtEx(this, width, height, IntPtr.Zero);
        }


        // Window calls
        public virtual void SetWindowOrigin(int x, int y)
        {
            GDI32.SetWindowOrgEx(this, x, y, IntPtr.Zero);
        }

        public virtual void SetWindowExtent(int width, int height)
        {
            GDI32.SetWindowExtEx(this, width, height, IntPtr.Zero);
        }

        public virtual void OffsetWindowOrigin(int x, int y)
        {
            GDI32.OffsetWindowOrgEx(this, x, y, IntPtr.Zero);
        }

        private GDI32Transform GetWorldTransform()
        {
            XFORM aTransform = new XFORM();
            bool result = GDI32.GetWorldTransform(this, out aTransform);

            GDI32Transform trans = new GDI32Transform();
            trans.SetTransform(aTransform);

            return trans;
        }

        public virtual void SetWorldTransform(GDI32Transform aTransform)
        {
            bool result = GDI32.SetWorldTransform(this, ref aTransform.fMatrix);
        }
        #endregion


    }
}
