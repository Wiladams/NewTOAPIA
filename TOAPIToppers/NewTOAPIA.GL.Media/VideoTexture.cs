using System;
using NewTOAPIA.GL;
using NewTOAPIA.Graphics;
using NewTOAPIA.Media.Capture;

namespace NewTOAPIA.Media.GL
{
    public class VideoTexture : GLTexture2D
    {
        PixelBufferObjectUnpacked fBufferObject;
        VideoCaptureDevice fVideoSource;

        IntPtr fPixelPtr;
        int fPixelLength;
        double fSampleTime;
        double fSampleUsed;

        #region Constructor
        public VideoTexture(GraphicsInterface gi, VideoCaptureDevice vidSource, bool autoStart)
            :base(gi, TextureBindTarget.Texture2d)
        {
            fVideoSource = vidSource;
            SetupCaptureDevice(gi);

            if (autoStart)
                Start();
        }

        void SetupCaptureDevice(GraphicsInterface gi)
        {
            // Create the buffer object based on the video source sizes
            fBufferObject = new PixelBufferObjectUnpacked(gi, BufferUsage.StreamDraw, IntPtr.Zero, fVideoSource.Height * fVideoSource.Width * 3);

            // Set our typical parameters
            GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
            
            SetupFiltering();
            SetupWrapping();

            // Register the function to receive notification when data is received
            fVideoSource.NewFrame += ReceivedNewFrame;
        }

        #endregion

        #region Properties
        public double FrameRate
        {
            get {
                double rate = 0.0;
                //fVideoSource.Stop();
                //rate = fVideoSource.FrameRate;
                //fVideoSource.Start();
                return rate;
            }
            set 
            { 
                //fVideoSource.FrameRate = value; 
            }
        }

        public override int Width
        {
            get
            {
                return fVideoSource.Width;
            }
        }

        public override int Height
        {
            get
            {
                return fVideoSource.Height;
            }
        }

        #endregion

        public void Start()
        {
            fVideoSource.Start();
        }

        public void Stop()
        {
            fVideoSource.Stop();
        }

        //public override void Bind()
        public void Bind()
        {

            fBufferObject.Bind();

            if (fPixelLength > 0 )
            {
                //&& (fSampleTime > fSampleUsed)
                // If the sample time of the latest buffer is different
                // than what we had when we last bound, then use
                // the new data.
                GI.PixelStore(PixelStore.UnpackAlignment, 1);
                fBufferObject.Write(fPixelPtr, fPixelLength);
            }


            // Now load the data into the texture
            // Since we're using a Buffer Object, this should be nothing more
            // than a DMA transfer on the GPU
            GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
            GI.PixelStore(PixelStore.UnpackAlignment, 1);
            GI.TexImage2D(0, TextureInternalFormat.Rgb8, Width, Height, 0, TexturePixelFormat.Bgr, PixelComponentType.UnsignedByte, IntPtr.Zero);
            //GI.TexImage2D(0, TextureInternalFormat.Rgb8, Width, Height, 0, TexturePixelFormat.Bgr, PixelComponentType.UnsignedByte, fPixelPtr);

            fSampleUsed = fSampleTime;

        }

        //public override void Unbind()
        public void Unbind()
        {
            base.Unbind();

            fBufferObject.Unbind();
        }

        void ReceivedNewFrame(object sender, CameraEventArgs e)
        {
            fPixelPtr = e.fData;
            fPixelLength = e.fSize;
            fSampleTime = e.fTimeStamp;
        }

        #region IDisposable
        public override void Dispose()
        {
            Stop();

            base.Dispose();
        }
        #endregion

        #region Static Helpers
        public static VideoTexture CreateFromDeviceIndex(GraphicsInterface gi, int deviceIndex, bool autoStart)
        {
            int numDevices = VideoCaptureDevice.GetNumberOfInputDevices();

            if ((deviceIndex >= 0) && (deviceIndex <= numDevices - 1))
            {
                VideoCaptureDevice vidCap = VideoCaptureDevice.CreateCaptureDeviceFromIndex(deviceIndex,0,0);
                VideoTexture tex = new VideoTexture(gi, vidCap, autoStart);
                return tex;
            }

            // Return a checkerboard pattern
            return null;
        }

        public static VideoTexture CreateFromDevicePath(GraphicsInterface gi, string devicePath, int width, int height, bool autoStart)
        {
            VideoCaptureDevice vidCap = VideoCaptureDevice.CreateCaptureDeviceFromName(devicePath, width, height);
            if (null == vidCap)
                return null;

            VideoTexture tex = new VideoTexture(gi, vidCap, autoStart);

            return tex;
        }
        #endregion
    }
}
