using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.DirectShow;

namespace NewTOAPIA.Media
{
    public class VideoTexture : GLTexture
    {
        VideoCaptureDevice fVideoSource;

        IntPtr fPixelPtr;
        int fPixelLength;
        double fSampleTime;
        double fSampleUsed;
        PixelBufferObjectUnpacked fBufferObject;

        #region Constructor
        public VideoTexture(GraphicsInterface gi, VideoCaptureDevice vidSource)
            :base(gi)
        {
            fVideoSource = vidSource;
            SetupCaptureDevice(gi);
        }

        void SetupCaptureDevice(GraphicsInterface gi)
        {
            // Create the buffer object based on the video source sizes
            fBufferObject = new PixelBufferObjectUnpacked(gi, BufferUsage.StreamDraw, fVideoSource.Height * fVideoSource.Width * 3);

            fBufferObject.Bind();
            int buffSize = fBufferObject.Size;
            BufferUsage usage = fBufferObject.Usage;
            fBufferObject.Unbind();


            // Set our typical parameters
            GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);

            // Register the function to receive notification when data is received
            fVideoSource.NewFrame += new CameraEventHandler(ReceivedNewFrame);

            Start();
        }

        #endregion

        #region Properties
        public IntPtr PixelData
        {
            get { return fPixelPtr; }
        }

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
            fVideoSource.SignalToStop();
            fVideoSource.WaitForStop();
        }

        public override void Bind()
        {
            fBufferObject.Bind();

            if (fPixelLength > 0)
            {
                // If the sample time of the latest buffer is different
                // than what we had when we last bound, then use
                // the new data.
                if (fSampleTime != fSampleUsed)
                {
                    GI.PixelStore(PixelStore.UnpackAlignment, 1);
                    fBufferObject.Write(fPixelLength, fPixelPtr, BufferUsage.StreamDraw);
                    fSampleUsed = fSampleTime;
                }

                // Now load the data into the texture
                // Since we're using a Buffer Object, this should be nothing more
                // than a DMA transfer on the GPU
                GI.PixelStore(PixelStore.UnpackAlignment, 1);
                GI.BindTexture(TextureBindTarget.Texture2d, TextureID);
                GI.TexImage2D(0, TextureInternalFormat.Rgb8, Width, Height, 0, TexturePixelFormat.Bgr, PixelType.UnsignedByte, IntPtr.Zero);
            }
        }

        public override void Unbind()
        {
            GI.BindTexture(TextureBindTarget.Texture2d, 0);
            fBufferObject.Unbind();
        }

        void ReceivedNewFrame(object sender, CameraEventArgs e)
        {
            fPixelPtr = e.fData;
            fPixelLength = e.fSize;
            fSampleTime = e.fTimeStamp;
        }


        public static VideoTexture CreateFromDeviceIndex(GraphicsInterface gi, int deviceIndex, int width, int height)
        {
            int numDevices = VideoCaptureDevice.GetNumberOfInputDevices();

            if ((deviceIndex >= 0) && (deviceIndex <= numDevices - 1))
            {
                VideoCaptureDevice vidCap = VideoCaptureDevice.CreateCaptureDeviceFromIndex(deviceIndex, width, height);
                VideoTexture tex = new VideoTexture(gi, vidCap);
                return tex;
            }
            else
            {
                //fCubeTexture = TextureHelper.CreateCheckerboardTexture(GI, 320, 240);
                return null;
            }

            // Return a checkerboard pattern
            return null;
        }

        public static VideoTexture CreateFromDevicePath(GraphicsInterface gi, string devicePath)
        {
            VideoCaptureDevice vidCap = VideoCaptureDevice.CreateCaptureDeviceFromName(devicePath, 320,240);
            if (null == vidCap)
                return null;

            VideoTexture tex = new VideoTexture(gi, vidCap);

            return tex;
        }
    }
}
