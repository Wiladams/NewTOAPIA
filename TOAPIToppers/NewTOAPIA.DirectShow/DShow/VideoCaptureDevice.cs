using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

using TOAPI.Types;

using NewTOAPIA.Media;
using NewTOAPIA.DirectShow.Core;
using NewTOAPIA.DirectShow.DES;
using NewTOAPIA.DirectShow.Quartz;


namespace NewTOAPIA.DirectShow
{

	/// <summary>
	/// VideoCaptureDevice - capture video from local device
	/// </summary>
	public class VideoCaptureDevice : IVideoSource
    {
        #region Fields
        private string	source;
		private object	userData = null;
		private int		framesReceived;

		private Thread	thread = null;
		private ManualResetEvent stopEvent = null;
        Grabber fGrabber;

        private DsDevice fDevice;
        IBaseFilter fsourceBaseFilter;

        // DirectShow COM interfaces
        IGraphBuilder graphBuilder;
        ICaptureGraphBuilder2 captureGraphBuilder;
        IBaseFilter grabberFilter;
        ISampleGrabber sampleGrabber;

        IBaseFilter nullRendererFilter;
        NullRenderer nullRenderer;

        IAMStreamConfig fVideoStreamConfig;			// DShow Filter: configure frame rate, size
        List<VideoCapability> fVideoCapsList;

        IMediaControl mediaControl;
        //IVideoWindow win;

        int fDesiredWidth;
        int fDesiredHeight;

		// new frame event
		public event CameraEventHandler NewFrame;
        #endregion

        #region Constructor
        public VideoCaptureDevice()
        {
            fDesiredWidth = -1;
            fDesiredHeight = -1;

            fGrabber = new Grabber();
            fGrabber.NewMediaBuffer += OnNewBuffer;
            fVideoCapsList = null;

            AcquireInterfaces();

        }
        
        public VideoCaptureDevice(DsDevice device, int width, int height)
            :this()
		{
            fDesiredWidth = width;
            fDesiredHeight = height;
            fDevice = device;

            fsourceBaseFilter = fDevice.BaseFilter;

            source = device.DevicePath;
            SetupGraph();


            // At this point, the graph has been setup and
            // we're just waiting for the Start() method to be called.
        }

        #endregion

        #region Properties
        public IBaseFilter BaseFilter
        {
            get { return fsourceBaseFilter; }
        }

        public DsDevice Device
        {
            get { return fDevice; }
        }

        public int Width
        {
            get { return fGrabber.Width; }
        }

        public int Height
        {
            get { return fGrabber.Height; }
        }

        public List<VideoCapability> Capabilities
        {
            get
            {
                if (null == fVideoCapsList)
                    fVideoCapsList = VideoCapability.GetAllVideoCapabilities(fVideoStreamConfig);

                return fVideoCapsList;
            }
        }
        #endregion

        #region IVideoSource
        public virtual string VideoSource
        {
            get { return source; }
            set 
            { 
                source = value;
                
                // Associate the device with a source filter
                Guid iid = typeof(IBaseFilter).GUID;
                object sourceObj = null;
                IMoniker moniker = DsUtils.GetMonikerFromMonikerString(source);
                moniker.BindToObject(null, null, ref iid, out sourceObj);
                fsourceBaseFilter = (IBaseFilter)sourceObj;

                SetupGraph();

            }
        }

        public string Login
        {
            get { return null; }
            set { }
        }
        
        public string Password
        {
            get { return null; }
            set { }
        }
        
        public int FramesReceived
        {
            get
            {
                int frames = framesReceived;
                framesReceived = 0;
                return frames;
            }
        }
        
        public int BytesReceived
        {
            get { return 0; }
        }
        
        public bool Running
        {
            get
            {
                if (thread != null)
                {
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, so free resources
                    Free();
                }
                return false;
            }
        }

        public object UserData
        {
            get { return userData; }
            set { userData = value; }
        }
        #endregion

        #region Media Control Methods
        public void Start()
		{
			if (thread == null)
			{
				framesReceived = 0;

				// create events
				stopEvent	= new ManualResetEvent(false);
				
				// create and start new thread
				thread = new Thread(new ThreadStart(WorkerThread));
				thread.Name = source;
				thread.Start();
			}
		}

		// Signal thread to stop work
		public void SignalToStop()
		{
			// stop thread
			if (thread != null)
			{
				// signal to stop
				stopEvent.Set();
			}
		}

		// Wait for thread stop
		public void WaitForStop()
		{
			if (thread != null)
			{
				// wait for thread stop
				thread.Join();

				Free();
			}
		}

		// Abort thread
		public void Stop()
		{
			if (this.Running)
			{
				thread.Abort();
				// WaitForStop();
			}
		}

		// Free resources
		private void Free()
		{
			thread = null;

			// release events
			stopEvent.Close();
			stopEvent = null;
        }
        #endregion

        #region COM Interface Management
        void AcquireInterfaces()
        {
            graphBuilder = (IGraphBuilder)new FilterGraph();
            captureGraphBuilder = (ICaptureGraphBuilder2)new CaptureGraphBuilder2();
            mediaControl = (IMediaControl)graphBuilder;
            //win = (IVideoWindow)graphBuilder;

            // Null Renderer
            nullRenderer = new NullRenderer();
            nullRendererFilter = (IBaseFilter)nullRenderer;

            // Sample grabber
            sampleGrabber = (ISampleGrabber)new SampleGrabber();
            grabberFilter = (IBaseFilter)sampleGrabber;

        }

        void ReleaseInterfaces()
        {
            //Marshal.ReleaseComObject(this.win); 
            //this.win = null;

            Marshal.ReleaseComObject(this.mediaControl); this.mediaControl = null;
            //Marshal.ReleaseComObject(this.mediaEventEx); this.mediaEventEx = null;
            Marshal.ReleaseComObject(this.graphBuilder); this.graphBuilder = null;
            Marshal.ReleaseComObject(this.captureGraphBuilder); this.captureGraphBuilder = null;
            Marshal.ReleaseComObject(this.sampleGrabber); this.sampleGrabber = null;
            Marshal.ReleaseComObject(this.nullRenderer); this.nullRenderer = null;

            // release filters based on other objects
            fsourceBaseFilter = null;
            grabberFilter = null;

        }
        #endregion

        #region Graph Setup

        void AddSampleGrabber()
        {
            int hr = 0;

            // Add the sample grabber to the graph
            hr = graphBuilder.AddFilter(grabberFilter, "NewTOAPIA Grabber");
            DsError.ThrowExceptionForHR(hr);

            // After the filter is added to the graph, and before
            // it is connected to anything, we need to set the format
            // and size that we expect.
            AMMediaType mt = new AMMediaType();

            // Set the media type to Video/RBG24
            mt.majorType = MediaType.Video;
            mt.subType = MediaSubType.RGB24;
            mt.formatType = FormatType.VideoInfo;    // What format do we want to use to describe the media

            //VIDEOINFOHEADER vih = new VIDEOINFOHEADER();
            //vih.BmiHeader.biBitCount = 24;
            //vih.BmiHeader.biClrImportant = 0;
            //vih.BmiHeader.biClrUsed = 0;
            //vih.BmiHeader.biCompression = 0;
            //vih.BmiHeader.biHeight = fDesiredHeight;
            //vih.BmiHeader.biPlanes = 1;
            //vih.BmiHeader.biSize = Marshal.SizeOf(vih);
            //vih.BmiHeader.biSizeImage = (uint)(fDesiredWidth * fDesiredHeight * 3); // It's very important to set this one or things won't be right
            //vih.BmiHeader.biWidth = fDesiredWidth;
            //vih.BmiHeader.biXPelsPerMeter = 0;
            //vih.BmiHeader.biYPelsPerMeter = 0;
            
            //// turn the structure back into a pointer
            //mt.formatPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(vih));
            //mt.formatSize = Marshal.SizeOf(vih);
            //mt.formatType = FormatType.VideoInfo;
            //Marshal.StructureToPtr(vih, mt.formatPtr, false);

            hr = sampleGrabber.SetMediaType(mt);
            DsError.ThrowExceptionForHR(hr);

            // Free the media type object because we're done with it
            DsUtils.FreeAMMediaType(mt);
            mt = null;

            hr = sampleGrabber.SetOneShot(false);

            hr = sampleGrabber.SetBufferSamples(false);

            hr = sampleGrabber.SetCallback(fGrabber, 1);
            DsError.ThrowExceptionForHR(hr);

        }

        public IAMCameraControl GetCameraControl()
        {
            return (IAMCameraControl)fsourceBaseFilter;
        }

        /// <summary>
        /// Configure the capture filter once it is in the graph.
        /// </summary>
        /// <param name="streamConfig"></param>
        /// <param name="iWidth">Desired width of video</param>
        /// <param name="iHeight">Desired height of video</param>
        /// <param name="bitsPerPixel">Number of bits per pixel</param>
        /// <remarks>
        /// We configure the source filter with the desired size, mediasubtype, and 
        /// bits per pixel.  This must be done before the capture pin is connected
        /// to other filters in the graph.
        /// </remarks>
        private void ConfigureSourceFilter(IAMStreamConfig streamConfig, int iWidth, int iHeight, short bitsPerPixel)
        {
            int hr;
            AMMediaType mType = new AMMediaType();
            VIDEOINFOHEADER v;

            // First, we need to get the existing format block
            // If there's an error, throw an exception.
            hr = streamConfig.GetFormat(out mType);
            DsError.ThrowExceptionForHR(hr);

            // Return early if the media type is not what we expect
            // First, the majorType must be 'Video'
            // Second, the size of the format structure must be sizeof(VIDEOINFOHEADER)
            if ((MediaType.Video != mType.majorType) || 
                (mType.formatSize != Marshal.SizeOf(typeof(VIDEOINFOHEADER))))
                return;

            try
            {
                // The formatPtr member of the AMMediaType object points
                // to a chunk of unmanaged memory that contains the data
                // for the format structure.  We need to copy this data 
                // into a structure object so we can manipulate it.
                v = new VIDEOINFOHEADER();
                Marshal.PtrToStructure(mType.formatPtr, v);

                // In order to change the size and bits per pixel, we
                // need to change the values of the VIDEOINFOHEADER object
                // that we pulled out from the AMMediaType object.
                if (iWidth > 0)
                {
                    v.BmiHeader.biWidth = iWidth;
                }

                if (iHeight > 0)
                {
                    v.BmiHeader.biHeight = iHeight;
                }

                if (bitsPerPixel > 0)
                {
                    v.BmiHeader.biBitCount = (ushort)bitsPerPixel;
                }

                // Now we copy the VIDEOINFOHEADER structure back into 
                // a piece of unmanaged memory and give the pointer to 
                // the AMMediaType structure.
                Marshal.StructureToPtr(v, mType.formatPtr, false);

                // Finally, we set the new format on the pin
                hr = streamConfig.SetFormat(mType);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                mType.Dispose();
            }
        }

        void AddSourceFilter()
        {
            int hr = 0;

            // Add the source filter to the graph
            hr = graphBuilder.AddFilter(fsourceBaseFilter, "Webcam Source");
            DsError.ThrowExceptionForHR(hr);

            // We want to configure the video capture pin
            // So, we first find the interface
            Object o = null;
            Guid iid = typeof(IAMStreamConfig).GUID;

            hr = captureGraphBuilder.FindInterface(PinCategory.Capture, MediaType.Video, fsourceBaseFilter, iid, out o);
            if (hr != 0)
                o = null;

            // If there was no interface found, then just return
            if (null == o)
                return;

            fVideoStreamConfig = o as IAMStreamConfig;

            ConfigureSourceFilter(fVideoStreamConfig, fDesiredWidth, fDesiredHeight, 24);
        }

        void AddFilters()
        {
            AddSampleGrabber();

            AddSourceFilter();

            int hr = 0;
            hr = graphBuilder.AddFilter(nullRendererFilter, "Null Renderer");
            DsError.ThrowExceptionForHR(hr);

        }

        void GetCaptureSize()
        {
            int hr = 0;

            // After the filter is added to the graph, and before
            // it is connected to anything, we need to set the format
            // and size that we expect.
            AMMediaType mt = new AMMediaType();

            hr = sampleGrabber.GetConnectedMediaType(mt);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                if ((mt.formatType != FormatType.VideoInfo) || (mt.formatPtr == IntPtr.Zero))
                {
                    throw new NotSupportedException("Unknown Grabber Media Format");
                }

                // Get the struct
                VIDEOINFOHEADER videoInfoHeader = new VIDEOINFOHEADER();
                Marshal.PtrToStructure(mt.formatPtr, videoInfoHeader);

                // Grab the size info
                fGrabber.Width = videoInfoHeader.BmiHeader.biWidth;
                fGrabber.Height = videoInfoHeader.BmiHeader.biHeight;
                fGrabber.FrameBytes = (int)videoInfoHeader.BmiHeader.biSizeImage;
            }
            finally
            {
                DsUtils.FreeAMMediaType(mt);
                mt = null;
            }

        }

        void ConnectFilters()
        {
            int hr = 0;

            hr = captureGraphBuilder.RenderStream(PinCategory.Capture, MediaType.Video, fsourceBaseFilter, grabberFilter, nullRendererFilter);
            DsError.ThrowExceptionForHR(hr);

            // Get size of connected media
            // After we connect the grabber to the source filter, 
            // we can ask it what size it actually has, and what
            // media type.
            GetCaptureSize();

            // If we were not using a null renderer, we would have to do 
            // the following in order to turn off the default display window
            //win.set_AutoShow(false);
            //win = null;

            // But, since we're using a null renderer, we don't have to do anything
        }

        void SetupGraph()
        {
            int hr = 0;

            hr = captureGraphBuilder.SetFiltergraph(graphBuilder);
            DsError.ThrowExceptionForHR(hr);

            AddFilters();

            ConnectFilters();
        }
        #endregion

        // Thread entry point
		public void WorkerThread()
		{
            try
            {
                // Now, actually run the graph
                mediaControl.Run();

                // The bulk of this thread's time is spent waiting
                // for a stopEvent to occur.  If the event is signaled
                // then the media is stopped, and we exit the loop
                while (!stopEvent.WaitOne(0, true))
                {
                    Thread.Sleep(100);
                }

                mediaControl.StopWhenReady();
            }
            // catch any exceptions
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("----: " + e.Message);
            }
            finally
            {                
            }
		}

        protected int OnNewBuffer(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if ((!stopEvent.WaitOne(0, true)) && (NewFrame != null))
            {
                CameraEventArgs camEvent = new CameraEventArgs(SampleTime, pBuffer, BufferLen, Width, Height);
                NewFrame(this, camEvent);
            }

            return 0;
        }




        #region Static Helper Methods

        internal static IBaseFilter GetIndexedVideoInputDevice(int indexed)
        {
            DsDevice[] devices;

            // Get all video input devices
            devices = DsDevice.GetDevicesOfCategory(FilterCategory.VideoInputDevice);

            // Get the specific one, if in range
            if (indexed < devices.Length)
            {
                return devices[indexed].BaseFilter;
            }

                string error = "There are only" + devices.Length + "Video Devices.";
                throw new IndexOutOfRangeException(error);
        }

        public static int GetNumberOfInputDevices()
        {
            DsDevice[] devices;
            devices = DsDevice.GetDevicesOfCategory(FilterCategory.VideoInputDevice);

            return devices.Length;
        }

        public static VideoCaptureDevice CreateCaptureDeviceFromName(string friendlyName, int width, int height)
        {
            DsDevice[] devices;
            devices = DsDevice.GetDevicesOfCategory(FilterCategory.VideoInputDevice);

            foreach(DsDevice dev in devices)
            {
                if (dev.FriendlyName.Equals(friendlyName))
                    return new VideoCaptureDevice(dev, width,height);
            }

            return null;
        }

        public static VideoCaptureDevice CreateCaptureDeviceFromIndex(int index, int width, int height)
        {
            DsDevice[] devices;
            devices = DsDevice.GetDevicesOfCategory(FilterCategory.VideoInputDevice);

            VideoCaptureDevice newDevice = new VideoCaptureDevice(devices[index], width, height);

            return newDevice;
        }

        #endregion
    }


    internal class Grabber : ISampleGrabberCB
    {
        private int width, height;
        private int totalBytes;
        public event MediaBufferCallback NewMediaBuffer;

        // Constructor
        public Grabber()
        {
        }

        #region Properties
        public int FrameBytes
        {
            get { return totalBytes; }
            set { totalBytes = value; }
        }

        // Width property
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        // Height property
        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        #endregion

        #region ISampleGrabberCB
        public int SampleCB(double SampleTime, IntPtr pSample)
        {
            return 0;
        }

        // Callback method that receives a pointer to the sample buffer
        public virtual int BufferCB(double SampleTime, IntPtr pBuffer, int BufferLen)
        {
            if (null != NewMediaBuffer)
                NewMediaBuffer(SampleTime, pBuffer, BufferLen);

            return 0;
        }
        #endregion
    }

}
