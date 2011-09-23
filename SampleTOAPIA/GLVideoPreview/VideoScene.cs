
namespace QuadVideo
{
    using System;
    using System.Collections.Generic;

    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;

    using NewTOAPIA.Media;
    using NewTOAPIA.Media.Capture;
    using NewTOAPIA.DirectShow;

    class VideoScene : GLModel
    {
        VideoCaptureDevice fCaptureDevice;
        GLDraw fFrameDrawer;
        Queue<CameraEventArgs> fVideoFrames;
        float2 fScale;


        public VideoScene()
        {
            fScale = new float2(1, 1);
            fVideoFrames = new Queue<CameraEventArgs>();
        }

        protected override void OnSetContext()
        {

            fFrameDrawer = new GLDraw(GI);

            fCaptureDevice = VideoCaptureDevice.CreateCaptureDeviceFromIndex(0, -1, -1);
            fCaptureDevice.NewFrame += new CameraEventHandler(fCaptureDevice_NewFrame);        
        
            // Turn off features that we don't need, and they 
            // just slow down video processing
            GI.Features.AlphaTest.Disable();
            GI.Features.Blend.Disable();
            GI.Features.DepthTest.Disable();
            GI.Features.Dither.Disable();
            GI.Features.Fog.Disable();
            GI.Features.Lighting.Disable();

            fCaptureDevice.Start();
        }

        void fCaptureDevice_NewFrame(object sender, CameraEventArgs e)
        {
            fVideoFrames.Enqueue(e);
        }

        protected override void DrawBegin()
        {
            GI.DrawBuffer(DrawBufferMode.FrontAndBack);
            GI.PixelZoom(fScale.x, fScale.y);
        }

        protected override void  DrawContent()
        {
            CameraEventArgs cEvent = null;

            if (fVideoFrames.Count > 0)
                cEvent = fVideoFrames.Dequeue();

            if (null != cEvent)
            {
                // Create an accessor for the frame
                PixelAccessor<BGRb> accessor = new PixelAccessor<BGRb>(cEvent.Width, cEvent.Height, PixmapOrientation.BottomToTop, cEvent.fData, cEvent.Width);

                // Send the accessor to the frame buffer drawer
                fFrameDrawer.DrawPixels(accessor, 0, 0);
            }
        }

        protected override void DrawEnd()
        {
            // Do nothing as we don't want to swap buffers
        }

        public override void OnSetViewport(int w, int h)
        {
            fScale.x = w / fCaptureDevice.Width;
            fScale.y = h / fCaptureDevice.Height;
        }

        protected override void OnReleaseContext()
        {
            fCaptureDevice.Stop();
        }
    }
}