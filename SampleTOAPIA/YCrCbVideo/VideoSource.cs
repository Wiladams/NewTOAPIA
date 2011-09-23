using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

using NewTOAPIA.GL;
using DirectShowLib;

namespace QuadVideo
{
    // a small enum to record the graph state
    enum PlayState
    {
        Stopped,
        Paused,
        Running,
        Init
    };

    public class VideoSource : ISampleGrabberCB, IDisposable
    {
        // Application-defined message to notify app of filtergraph events
        public const int WM_GRAPHNOTIFY = 0x8000 + 1;

        /// <summary> Dimensions of the image, calculated once in constructor. </summary>
        private int videoWidth;
        private int videoHeight;
        private int fStride;
        private byte[] fPixelArray;

        IVideoWindow videoWindow = null;
        IMediaControl mediaControl = null;
        IMediaEventEx mediaEventEx = null;
        IGraphBuilder graphBuilder = null;
        ICaptureGraphBuilder2 captureGraphBuilder = null;
        ISampleGrabber sampleGrabber = null;

        IntPtr fPixels;
        int fPixelBufferLen;

        PlayState currentState = PlayState.Stopped;

        DsROTEntry rot = null;


        public VideoSource()
        {
            fPixels = IntPtr.Zero;
            fPixelBufferLen = 0;
            Initialize();
            Run();
        }

        public byte[] PixelArray
        {
            get { return fPixelArray; }
        }

        public IntPtr Pixels
        {
            get { return fPixels; }
        }

        public int Width
        {
            get { return videoWidth; }
        }

        public int Height
        {
            get { return videoHeight; }
        }

        public void Run()
        {
            // Start previewing video data
            int hr = this.mediaControl.Run();
            DsError.ThrowExceptionForHR(hr);

            // Remember current state
            this.currentState = PlayState.Running;
        }

        public void Pause()
        {
            // Start previewing video data
            int hr = this.mediaControl.Pause();
            DsError.ThrowExceptionForHR(hr);

            // Remember current state
            this.currentState = PlayState.Paused;
        }

        public void Initialize()
        {
            try
            {
                // Get DirectShow interfaces
                SetupGraph();

                // Add our graph to the running object table, which will allow
                // the GraphEdit application to "spy" on our graph
                rot = new DsROTEntry(this.graphBuilder);
            }
            catch
            {
                Console.WriteLine("An unrecoverable error has occurred.");
            }
        }

        public IBaseFilter GetFirstVideoInputDevice()
        {
            DsDevice[] devices;
            object source;

            // Get all video input devices
            devices = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            // Take the first device
            DsDevice device = (DsDevice)devices[0];

            // Bind Moniker to a filter object
            Guid iid = typeof(IBaseFilter).GUID;
            device.Mon.BindToObject(null, null, ref iid, out source);

            // An exception is thrown if cast fail
            return (IBaseFilter)source;
        }

        public void SetupGraph()
        {
            int hr = 0;
            IBaseFilter sourceFilter = null;
            IBaseFilter baseGrabFlt = null;

            GetInterfaces();

            // Attach the filter graph to the capture graph
            hr = this.captureGraphBuilder.SetFiltergraph(this.graphBuilder);
            DsError.ThrowExceptionForHR(hr);

            // Use the system device enumerator and class enumerator to find
            // a video capture/preview device, such as a desktop USB video camera.
            sourceFilter = GetFirstVideoInputDevice();

            // Add Capture filter to our graph.
            hr = this.graphBuilder.AddFilter(sourceFilter, "Video Capture");
            DsError.ThrowExceptionForHR(hr);

            baseGrabFlt = (IBaseFilter)sampleGrabber;
            ConfigureSampleGrabber(sampleGrabber, 640,480);

            // Add the frame grabber to the graph
            hr = this.graphBuilder.AddFilter(baseGrabFlt, "Ds.NET Grabber");
            DsError.ThrowExceptionForHR(hr);

            // null renderer
            IBaseFilter pNullRenderer = (IBaseFilter)new NullRenderer();
            hr = this.graphBuilder.AddFilter(pNullRenderer, "Null Renderer");

            // Render the capture pin on the video grabber filter
            hr = this.captureGraphBuilder.RenderStream(PinCategory.Capture, MediaType.Video, sourceFilter, baseGrabFlt, pNullRenderer);
            DsError.ThrowExceptionForHR(hr);

            // Now that sizes are fixed, store the sizes
            SaveSizeInfo(sampleGrabber);
            //SetConfigParms(640, 480);

            // Now that the filter has been added to the graph and we have
            // rendered its stream, we can release this reference to the filter.
            Marshal.ReleaseComObject(sourceFilter);


        }

        /// <summary> Read and store the properties </summary>
        private void SaveSizeInfo(ISampleGrabber sampGrabber)
        {
            int hr;

            // Get the media type from the SampleGrabber
            AMMediaType media = new AMMediaType();
            hr = sampGrabber.GetConnectedMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            if ((media.formatType != FormatType.VideoInfo) || (media.formatPtr == IntPtr.Zero))
            {
                throw new NotSupportedException("Unknown Grabber Media Format");
            }

            // Grab the size info
            VideoInfoHeader videoInfoHeader = new VideoInfoHeader();
            Marshal.PtrToStructure(media.formatPtr, videoInfoHeader);

            //VideoInfoHeader videoInfoHeader = (VideoInfoHeader)Marshal.PtrToStructure(media.formatPtr, typeof(VideoInfoHeader));
            videoInfoHeader.BmiHeader.Width = 320;
            videoInfoHeader.BmiHeader.Height = 240;
            videoWidth = videoInfoHeader.BmiHeader.Width;
            videoHeight = videoInfoHeader.BmiHeader.Height;
            fStride = videoWidth * (videoInfoHeader.BmiHeader.BitCount / 8);
            fPixelArray = new byte[fStride * videoHeight];

            // Copy the media structure back
            Marshal.StructureToPtr(videoInfoHeader, media.formatPtr, false);
            hr = sampleGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;
        }

        // Set the Framerate, and video size
        private void SetConfigParms(IPin pStill, int iWidth, int iHeight, short iBPP)
        {
            int hr;
            AMMediaType media;
            VideoInfoHeader v;

            IAMStreamConfig videoStreamConfig = pStill as IAMStreamConfig;

            // Get the existing format block
            hr = videoStreamConfig.GetFormat(out media);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                // copy out the videoinfoheader
                v = new VideoInfoHeader();
                Marshal.PtrToStructure(media.formatPtr, v);

                // if overriding the width, set the width
                if (iWidth > 0)
                {
                    v.BmiHeader.Width = iWidth;
                }

                // if overriding the Height, set the Height
                if (iHeight > 0)
                {
                    v.BmiHeader.Height = iHeight;
                }

                // if overriding the bits per pixel
                if (iBPP > 0)
                {
                    v.BmiHeader.BitCount = iBPP;
                }

                // Copy the media structure back
                Marshal.StructureToPtr(v, media.formatPtr, false);

                // Set the new format
                hr = videoStreamConfig.SetFormat(media);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                DsUtils.FreeAMMediaType(media);
                media = null;
            }
        }

        /// <summary> Set the options on the sample grabber </summary>
        private void ConfigureSampleGrabber(ISampleGrabber sampGrabber, int width, int height)
        {
            int hr;
            AMMediaType media = new AMMediaType();
            //VideoInfoHeader v;

            // copy out the videoinfoheader
            //v = new VideoInfoHeader();
            //Marshal.PtrToStructure(media.formatPtr, v);

            //// Set the size
            //v.BmiHeader.Width = width;
            //v.BmiHeader.Height = height;
            
            // Copy the media structure back
            //Marshal.StructureToPtr(v, media.formatPtr, false);

            // Set the media type to Video/RBG24
            media.majorType = MediaType.Video;
            media.subType = MediaSubType.RGB24;
            media.formatType = FormatType.VideoInfo;
            
            hr = sampGrabber.SetMediaType(media);
            DsError.ThrowExceptionForHR(hr);

            DsUtils.FreeAMMediaType(media);
            media = null;

            hr = sampGrabber.SetBufferSamples(false);
            hr = sampGrabber.SetOneShot(false);


            // Configure the samplegrabber callback
            hr = sampGrabber.SetCallback(this, 1);
            DsError.ThrowExceptionForHR(hr);
        }

        #region Get and Release COM Interfaces
        public void GetInterfaces()
        {
            int hr = 0;

            // An exception is thrown if cast fail
            this.graphBuilder = (IGraphBuilder)new FilterGraph();
            this.captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            this.mediaControl = (IMediaControl)this.graphBuilder;
            this.videoWindow = (IVideoWindow)this.graphBuilder;
            this.mediaEventEx = (IMediaEventEx)this.graphBuilder;
            
            // Get the SampleGrabber interface
            sampleGrabber = (ISampleGrabber)new SampleGrabber();

            //hr = this.mediaEventEx.SetNotifyWindow(this.Handle, WM_GRAPHNOTIFY, IntPtr.Zero);
            DsError.ThrowExceptionForHR(hr);
        }

        public void ReleaseInterfaces()
        {
            // Stop previewing data
            if (this.mediaControl != null)
                this.mediaControl.StopWhenReady();

            this.currentState = PlayState.Stopped;

            // Stop receiving events
            if (this.mediaEventEx != null)
                this.mediaEventEx.SetNotifyWindow(IntPtr.Zero, WM_GRAPHNOTIFY, IntPtr.Zero);

            // Relinquish ownership (IMPORTANT!) of the video window.
            // Failing to call put_Owner can lead to assert failures within
            // the video renderer, as it still assumes that it has a valid
            // parent window.
            if (this.videoWindow != null)
            {
                this.videoWindow.put_Visible(OABool.False);
                this.videoWindow.put_Owner(IntPtr.Zero);
            }

            // Remove filter graph from the running object table
            if (rot != null)
            {
                rot.Dispose();
                rot = null;
            }

            // Release DirectShow interfaces
            Marshal.ReleaseComObject(this.mediaControl); this.mediaControl = null;
            Marshal.ReleaseComObject(this.mediaEventEx); this.mediaEventEx = null;
            Marshal.ReleaseComObject(this.videoWindow); this.videoWindow = null;
            Marshal.ReleaseComObject(this.graphBuilder); this.graphBuilder = null;
            Marshal.ReleaseComObject(this.captureGraphBuilder); this.captureGraphBuilder = null;
        }
        #endregion

        //public void ChangePreviewState(bool showVideo)
        //{
        //    int hr = 0;

        //    // If the media control interface isn't ready, don't call it
        //    if (this.mediaControl == null)
        //        return;

        //    if (showVideo)
        //    {
        //        if (this.currentState != PlayState.Running)
        //        {
        //            // Start previewing video data
        //            hr = this.mediaControl.Run();
        //            this.currentState = PlayState.Running;
        //        }
        //    }
        //    else
        //    {
        //        // Stop previewing video data
        //        hr = this.mediaControl.StopWhenReady();
        //        this.currentState = PlayState.Stopped;
        //    }
        //}


        #region ISampleGrabberCB
        /// <summary> sample callback, NOT USED. </summary>
        int ISampleGrabberCB.SampleCB(double SampleTime, IMediaSample pSample)
        {
            Marshal.ReleaseComObject(pSample);
            return 0;
        }


        /// <summary> buffer callback, COULD BE FROM FOREIGN THREAD. </summary>
        int ISampleGrabberCB.BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            // Avoid the possibility that someone is calling SetLogo() at this instant
            lock (this)
            {
                fPixels = pBuffer;
                fPixelBufferLen = BufferLen;
                //Marshal.Copy(pBuffer, fPixelArray, 0, BufferLen);
                //Console.WriteLine("Sample Time: {0}, Ptr: {1}", SampleTime, pBuffer.ToInt32());
                //if (m_bmdLogo != null)
                //{
                //    IntPtr ipSource = m_bmdLogo.Scan0;
                //    IntPtr ipDest = pBuffer;

                //    for (int x = 0; x < m_bmdLogo.Height; x++)
                //    {
                //        CopyMemory(ipDest, ipSource, (uint)m_bmdLogo.Stride);
                //        ipDest = (IntPtr)(ipDest.ToInt32() + m_stride);
                //        ipSource = (IntPtr)(ipSource.ToInt32() + m_bmdLogo.Stride);
                //    }
                //}
            }

            return 0;
        }
        #endregion

        /// <summary> release everything. </summary>
        public void Dispose()
        {
            ReleaseInterfaces();
        }
    }
}

