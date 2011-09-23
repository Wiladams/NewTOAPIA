using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Processing
{
    using TOAPI.GDI32;
    using TOAPI.Types;
    using TOAPI.User32;

    using NewTOAPIA.Graphics;


    using Microsoft.WindowsAPICodePack.DirectX.Direct2D1;
    using Microsoft.WindowsAPICodePack.DirectX.DirectWrite;
    using Microsoft.WindowsAPICodePack.DirectX.DXGI;
    using Microsoft.WindowsAPICodePack.DirectX.WindowsImagingComponent;

    #region enum RenderModes
    public enum RenderModes
    {
        /// <summary>
        /// Draw through device context in OnPaint (when the window gets invalidated)
        /// </summary>
        DCRenderTarget = 0,
        /// <summary>
        /// Use BitmapRenderTarget to draw on updates, copy the bitmap to DCRenderTarget in OnPaint (when the window gets invalidated)
        /// </summary>
        BitmapRenderTargetOnPaint,
        /// <summary>
        /// Use BitmapRenderTarget to draw on updates, copy the bitmap to HwndRenderTarget in real time
        /// </summary>
        BitmapRenderTargetRealTime,
        /// <summary>
        /// Draw directly on window in real time
        /// </summary>
        HwndRenderTarget,
    };
    #endregion

    public class PDirect2DRenderer: PRenderer
    {
        #region Direct2D Specifics
        internal static D2DFactory sharedD2DFactory;
        internal static ImagingFactory sharedWicFactory;
        internal static DWriteFactory sharedDwriteFactory;
        internal static object sharedSyncObject = new object();
        internal static int sharedRefCount;

        //object used for synchronization, so shape list changes, (de)initialization, configuration changes and rendering are not done concurrently
        private readonly Object renderSyncObject = new object();

        //factory objects
        internal D2DFactory d2DFactory;
        internal ImagingFactory wicFactory;
        internal DWriteFactory dwriteFactory;

        //render target used in real time rendering modes (can also be used OnPaint, but does not use a device context)
        private HwndRenderTarget hwndRenderTarget;
        //compatible bitmap that is used in cached modes, in which only changes to the image are drawn to the bitmap and the bitmap is drawn to screen when needed
        private BitmapRenderTarget bitmapRenderTarget;
        //device context (DC) render target - used with the Graphics object to render to DC
        private DCRenderTarget dcRenderTarget;
        RenderTarget Graphics;

        private RenderModes renderMode;

        #endregion

        IntPtr fWindowHandle;
        SolidColorBrush fStrokeBrush;
        SolidColorBrush fFillBrush;

        public PDirect2DRenderer(IntPtr winHandle)
        {
            fWindowHandle = winHandle;
            CreateFactories();
            CreateDeviceResources();

            Graphics = hwndRenderTarget;

            fStrokeBrush = Graphics.CreateSolidColorBrush(new ColorF(0, 0, 0, 1));
            fFillBrush = Graphics.CreateSolidColorBrush(new ColorF(1, 1, 1, 1));
        }

        public RenderModes RenderMode
        {
            get { return renderMode; }

            set
            {
                SetRenderMode(value);
            }
        }

        #region CreateFactories()
        private void CreateFactories()
        {
                // Create the D2D Factory
                d2DFactory = D2DFactory.CreateFactory(D2DFactoryType.MultiThreaded);

                // Create the DWrite Factory
                dwriteFactory = DWriteFactory.CreateFactory();

                // Create the WIC Factory
                wicFactory = new ImagingFactory();
                Debug.Assert(d2DFactory.NativeInterface != IntPtr.Zero);
                Debug.Assert(dwriteFactory.NativeInterface != IntPtr.Zero);
                Debug.Assert(wicFactory.NativeInterface != IntPtr.Zero);
        }

        #endregion

        #region CreateDeviceResources()
        /// <summary>
        /// This method creates the render target and associated D2D and DWrite resources
        /// </summary>
        void CreateDeviceResources()
        {
            SetRenderMode(renderMode);
        }
        #endregion

        #region SetRenderMode()
        private void SetRenderMode(RenderModes rm)
        {
            lock (renderSyncObject)
            {
                renderMode = rm;
                //if (!IsInitialized && !isInitializing)
                //    return;

                //clean up objects that will be invalid after RenderTarget change
                if (dcRenderTarget != null)
                {
                    dcRenderTarget.Dispose();
                    dcRenderTarget = null;
                }
                if (hwndRenderTarget != null)
                {
                    hwndRenderTarget.Dispose();
                    hwndRenderTarget = null;
                }
                if (bitmapRenderTarget != null)
                {
                    bitmapRenderTarget.Dispose();
                    bitmapRenderTarget = null;
                }
                //bitmap = null; //the bitmap created in dc render target can't be used in hwnd render target

                // Create the screen render target
                RECT cRect;
                User32.GetClientRect(fWindowHandle, out cRect);
                
                var size = new SizeU((uint)cRect.Width, (uint)cRect.Height);
                PixelFormat pFormat = new PixelFormat(Format.B8G8R8A8_UNORM, AlphaMode.Ignore);
                var props = new RenderTargetProperties {PixelFormat = pFormat, Usage = RenderTargetUsage.GdiCompatible};

                //if (renderMode == RenderModes.DCRenderTarget || renderMode == RenderModes.BitmapRenderTargetOnPaint)
                //{
                //    dcRenderTarget = d2DFactory.CreateDCRenderTarget(props);
                //    if (renderMode == RenderModes.BitmapRenderTargetOnPaint)
                //    {
                //        bitmapRenderTarget =
                //            dcRenderTarget.CreateCompatibleRenderTarget(
                //            CompatibleRenderTargetOptions.GdiCompatible,
                //            new Microsoft.WindowsAPICodePack.DirectX.Direct2D1.SizeF(ClientSize.Width, ClientSize.Height));
                //    }
                //    render = null;
                //}
                //else
                {
                    HwndRenderTargetProperties hProps = new HwndRenderTargetProperties(fWindowHandle, size, PresentOptions.RetainContents);
                    hwndRenderTarget = d2DFactory.CreateHwndRenderTarget(props,  hProps);
                    
                    //if (renderMode == RenderModes.BitmapRenderTargetRealTime)
                    //{
                    //    bitmapRenderTarget =
                    //        hwndRenderTarget.CreateCompatibleRenderTarget(
                    //        CompatibleRenderTargetOptions.GdiCompatible,
                    //        new Microsoft.WindowsAPICodePack.DirectX.Direct2D1.SizeF(ClientSize.Width, ClientSize.Height));
                    //}
                    //render = RenderSceneInBackground;
                }
            }
        }
        #endregion

        public override void Resize(int w, int h)
        {
            hwndRenderTarget.Resize(new SizeU((uint)w, (uint)h));
        }

            #region Attributes
        public override void SetBrush(IBrush brush)
        {
        }

        public override void SetPen(IPen pen)
        {
            strokeWidth = pen.Width;
        
            ColorF penColor = new ColorF(pen.Color.Red, pen.Color.Green, pen.Color.Blue,1);
            fStrokeBrush = Graphics.CreateSolidColorBrush(penColor);
        }
        #endregion

        public override void ClearToColor(color c)
        {
        }

        public override void SetPixel(float x, float y, color c)
        {
            Graphics.BeginDraw();
            Graphics.DrawLine(new Point2F(x, y), new Point2F(x, y), fStrokeBrush, strokeWidth);
            Graphics.EndDraw();
        }

        public override void Line(float x1, float y1, float x2, float y2)
        {
            Graphics.BeginDraw();
            Graphics.DrawLine(new Point2F(x1, y1), new Microsoft.WindowsAPICodePack.DirectX.Direct2D1.Point2F(x2, y2), fStrokeBrush, strokeWidth);
            Graphics.EndDraw();
        }

        public override void PolyLine(Point2I[] pts)
        {
        }

        public override void Bezier(int x1, int y1, int cx1, int cy1, int cx2, int cy2, int x2, int y2)
        {
        }

        public override void Ellipse(int left, int top, int right, int bottom)
        {
        }

        public override void Arc(float x, float y, float width, float height, float startRadians, float endRadians)
        {
        }

        // Triangle
        public override void StrokeAndFillTriangle(float x1, float y1, float x2, float y2, float x3, float y3)
        {
        }

        public override void Rectangle(float x, float y, float width, float height)
        {
            Graphics.BeginDraw();
            RectF r = new RectF(x,y,x+width,y+height);
            Graphics.DrawRectangle(r, fFillBrush, strokeWidth);
            Graphics.EndDraw();
        }

        // Polygon
        public override void Polygon(Point2I[] pts)
        {
        }
    }
}
