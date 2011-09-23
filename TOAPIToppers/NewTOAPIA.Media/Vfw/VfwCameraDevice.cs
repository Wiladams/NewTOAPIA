using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using TOAPI.Types;
using TOAPI.AviCap32;
using TOAPI.GDI32;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// The VfwCameraDevice object represents a Vfw video capture device.  As Vfw
    /// also supports audio capture devices, this object is specialized to only
    /// the video capture interfaces.
    /// </summary>
    public class VfwCameraDevice : VfwCaptureDevice
    {
        #region Fields

        VfwVideoFrameDelegate fVideoFrameDelegate;
        VfwcapControlCallback fControlDelegate;
        VfwVideoFrameDelegate fVideoStreamDelegate;
        VfwcapStatusCallback fStatusDelegate;
        VfwcapErrorCallback fErrorDelegate;

        // Configuration fields
        BITMAPINFO fBitmapInfo;
        CAPTUREPARMS fCaptureParams;
        #endregion

        #region Constructor
        public VfwCameraDevice(int width, int height)
            : this(0, width, height, 15, 0, IntPtr.Zero)
        {
        }

        public VfwCameraDevice(int index, int width, int height, int fps, int windowStyle, IntPtr parentWindow)
            : base(index, width, height, fps, windowStyle, parentWindow)
        {


            // Setup the delegate functions that will be called
            fVideoStreamDelegate = StreamCallBack;            
            fVideoFrameDelegate = FrameCallBack;
            fControlDelegate = ControlCallback;
            fErrorDelegate = ErrorCallback;

            SetCallbackOnVideoStream(fVideoStreamDelegate);
            SetCallbackOnFrame(fVideoFrameDelegate);
            SetCallbackOnCapControl(fControlDelegate);
            SetCallbackOnStatus(fStatusDelegate);
            SetCallbackOnError(fErrorDelegate);
        }
        #endregion

        #region Configuration
        void PrintCompressionType(uint compression)
        {
            Console.Write("Compression - ");
            switch (fBitmapInfo.bmiHeader.biCompression)
            {
                case GDI32.BI_BITFIELDS:
                    Console.WriteLine("Bitfields");
                    break;
                case GDI32.BI_JPEG:
                    Console.WriteLine("JPEG");
                    break;
                case GDI32.BI_PNG:
                    Console.WriteLine("PNG");
                    break;
                case GDI32.BI_RGB:
                    Console.WriteLine("RGB");
                    break;
                case GDI32.BI_RLE4:
                    Console.WriteLine("RLE4");
                    break;
                case GDI32.BI_RLE8:
                    Console.WriteLine("RLE8");
                    break;

                default:
                    Console.WriteLine("UNKNOWN");
                    break;
            }
        }

        void ConfigureVideoFormat()
        {
            fBitmapInfo = new BITMAPINFO();
            fBitmapInfo.Init();

            fBitmapInfo.bmiHeader = new BITMAPINFOHEADER();
            fBitmapInfo.bmiHeader.Init();

            // Get the current format information
            int formatSize = capGetVideoFormatSize();
            capGetVideoFormat(ref fBitmapInfo);

            PrintCompressionType(fBitmapInfo.bmiHeader.biCompression);

            // Fill in the things we want to change
            fBitmapInfo.bmiHeader.biWidth = Width;
            fBitmapInfo.bmiHeader.biHeight = Height;
            fBitmapInfo.bmiHeader.biPlanes = 1;
            fBitmapInfo.bmiHeader.biBitCount = 24; // 24 bits per frame - RGB
            fBitmapInfo.bmiHeader.biCompression = GDI32.BI_RGB;

            // Try to set the video format
            bool setSuccess = capSetVideoFormat(ref fBitmapInfo);

            // Get the settings again, and set the width
            // and height based on what was actually set
            capGetVideoFormat(ref fBitmapInfo);
            Width = fBitmapInfo.bmiHeader.biWidth;
            Height = fBitmapInfo.bmiHeader.biHeight;
        }

        /// <summary>
        /// We can configure some settings related to capturing images from
        /// the camera.  Primarily we want to control how many buffers are 
        /// used in the capture process.
        /// 
        /// If we are doing continuous capture, we can configure
        /// the interval between subsequent frames.
        /// </summary>
        public override void ConfigureCapture()
        {
            fCaptureParams = new CAPTUREPARMS();
            
            capCaptureGetSetup(ref fCaptureParams);

            // These are the core parameters that must be 
            // set when we're capturing video

            fCaptureParams.dwRequestMicroSecPerFrame = (uint)CaptureInterval;   // Start with 15 frames per second
            fCaptureParams.wPercentDropForError = 10;               // Percentage of frames that can drop before error is signaled
            fCaptureParams.fYield = true;                          // If set to true, a thread will be spawned            
            fCaptureParams.wNumVideoRequested = 2;                  // Maximum number of video buffers to allocate
            
            // Related to capturing audio
            fCaptureParams.fCaptureAudio = false;
            fCaptureParams.wNumAudioRequested = 0;
            fCaptureParams.dwAudioBufferSize = 0;

            fCaptureParams.fLimitEnabled = false;
            fCaptureParams.wTimeLimit = 0;

            fCaptureParams.fDisableWriteCache=false;
            fCaptureParams.AVStreamMaster=0;

            // Used when capturing to a file
            fCaptureParams.dwIndexSize = 0;             // Determines the limit of audio/video buffers that can be captured
            fCaptureParams.wChunkGranularity = 0;

            // Related to MCI
            fCaptureParams.fMCIControl = false;
            fCaptureParams.fStepMCIDevice = false;
            fCaptureParams.dwMCIStartTime = 0;
            fCaptureParams.dwMCIStopTime = 0;
            fCaptureParams.fStepCaptureAt2x = false;
            fCaptureParams.wStepCaptureAverageFrames = 5;

            
            // These are related to mouse and keyboard control of capturing
            // Since we want total control of the capture process
            // we don't want these used at all.
            fCaptureParams.vKeyAbort = 0;
            fCaptureParams.fAbortLeftMouse = false;
            fCaptureParams.fAbortRightMouse = false;
            fCaptureParams.fMakeUserHitOKToCapture = false;

            fCaptureParams.fUsingDOSMemory = false;

            
            // Configure the actual capture settings
            capCaptureSetSetup(ref fCaptureParams);


        }
        #endregion

        #region Connection
        public override void OnConnected()
        {

            ConfigureVideoFormat();
            ConfigureCapture();
        }

        #endregion

        #region Methods
        public bool StartStreaming()
        {
            bool success = capCaptureSequenceNoFile();
            return success;
        }

        public bool StopStreaming()
        {
            bool success = capCaptureStop();
            return success;
        }

        /// <summary>
        /// Grab a single frame from the video device.  Disables Overlay
        /// and preview.
        /// 
        /// Since we want complete control of video grabbing, this is what we
        /// want.  We don't want or need a preview window, and we don't want 
        /// to be doing overlay either.
        /// 
        /// As the frame is grabbed, our frameDelegate function is called with 
        /// the retrieved frame.
        /// </summary>
        public bool GrabSingleFrame()
        {
            bool success = capGrabFrameNoStop();
            return success;
        }
        #endregion

        #region Configuration Dialog Windows 
        public void ShowCompressionChoices()
        {
            bool success = capDlgVideoCompression();
        }

        public bool ShowPreviewWindow()
        {
            bool success = capPreview(true);
            return success;
        }

        /// <summary>
        /// If the driver supports a dialog box to select the video Source, this
        /// method will display it on the screen.
        /// </summary>
        public void ShowVideoSourceChoices()
        {
            bool success = capDlgVideoSource();
        }

        public void ShowVideoFormats()
        {
            bool success = capDlgVideoFormat();
        }
        #endregion


        #region Properties
        public BITMAPINFO GetBitmapInfo()
        {
            return fBitmapInfo;
        }

        public VfwVideoFrameDelegate FrameDelegate
        {
            get { return fVideoFrameDelegate; }
            set
            {
                fVideoFrameDelegate = value;
                SetCallbackOnFrame(fVideoFrameDelegate);
            }
        }

        public VfwVideoFrameDelegate StreamDelegate
        {
            get { return fVideoStreamDelegate; }
            set
            {
                fVideoStreamDelegate = value;
                SetCallbackOnVideoStream(fVideoStreamDelegate);
            }
        }

        public int PreviewRate
        {
            set
            {
                capPreviewRate(value);
            }
        }
        #endregion


        #region Callback Delegate Functions
        private int StatusCallback(IntPtr hWnd, int nID, string lpsz)
        {
            return 0;   // Indicating we're doing streaming or single frame capture
        }

        private int ControlCallback(IntPtr hWnd, int nState)
        {
            if (nState == Vfw.CONTROLCALLBACK_PREROLL)
                return 1;

            else
                return 1;
        }

        private int ErrorCallback(IntPtr hWnd, int nID, string lpsz)
        {

            return 1;
        }

        private void StreamCallBack(IntPtr hwnd, ref VIDEOHDR hdr)
        {
            Console.WriteLine("VfwCameraDevice.StreamCallBack");
        }

        private void FrameCallBack(IntPtr hwnd, ref VIDEOHDR hdr)
        {
            Console.WriteLine("VfwCameraDevice.Frame Received");
        }
        #endregion
    }
}
