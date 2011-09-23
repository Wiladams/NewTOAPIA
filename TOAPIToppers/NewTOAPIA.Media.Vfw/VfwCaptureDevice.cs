using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

using TOAPI.User32;
using TOAPI.AviCap32;
using TOAPI.Types;

namespace NewTOAPIA.Media
{

    /// <summary>
    /// This class encapsulates a video capture device as represented
    /// by the AviCap interface of old.  There are only two functions
    /// that are connected to actual API calls.  All the other methods
    /// are actually messages that are sent to a window.
    /// </summary>
    public class VfwCaptureDevice
    {
        public delegate void VfwVideoFrameDelegate(IntPtr hwnd, ref VIDEOHDR hdr);
        public delegate int VfwcapControlCallback(IntPtr hWnd, int nState);
        public delegate int VfwcapStatusCallback(IntPtr hWnd, int nID, string lpsz);
        public delegate int VfwcapErrorCallback(IntPtr hWnd,  int nID, string lpsz);

        int fDriverIndex;
        protected IntPtr fWindowHandle;

        bool fIsConnected;
        VfwDeviceDriver fDeviceDriver;
        CAPDRIVERCAPS fDriverCapabilities;
        CAPSTATUS fCaptureStatus;

        int fFps;
        int fInterval;
        int fWidth;
        int fHeight;

        #region Constructor
        protected VfwCaptureDevice(int index, int width, int height, int fps, int windowStyle, IntPtr parentWindow)
        {
            fDriverIndex = index;
            
            fFps = fps;
            fInterval = (int)((1.0 / fFps * 1000000.0) + 0.5);  // We specify intervals in microseconds

            fWidth = width;
            fHeight = height;

            CreateWindow(0, 0, width, height, windowStyle, parentWindow);
            Connect();
        }
        #endregion

        public void CreateWindow(int x, int y, int width, int height, int style, IntPtr parentID)
        {
            fWindowHandle = AviCap32.capCreateCaptureWindow("VfwCaptureWindow: " + DriverIndex.ToString(), 
                style, x, y, width, height, parentID, DriverIndex);
        }

        #region Connection
        public bool Connect()
        {
            // If we're already connected, then simply return
            if (fIsConnected)
                return fIsConnected;

            fIsConnected = capDriverConnect();
            if (!fIsConnected)
                throw new Exception("VfwCaptureDevice - Connection to device failed");

            // Get the driver capabilities
            fDriverCapabilities = new CAPDRIVERCAPS();
            bool gotDriverCaps = capDriverGetCaps(ref fDriverCapabilities);

            // Finally allow a subclasser to do something special
            // upon connection.
            OnConnected();

            capGetStatus(ref fCaptureStatus);
            StringBuilder sb = new StringBuilder(100);
            StringBuilder sbVersion = new StringBuilder(100);

            capDriverGetName(sb, 100);
            capDriverGetVersion(sbVersion, 100);
            fDeviceDriver = new VfwDeviceDriver(sb.ToString(), sbVersion.ToString());

            return fIsConnected;
        }

        public virtual void OnConnected()
        {
        }

        public void Disconnect()
        {
            OnDisconnecting();

            if (fIsConnected)
                capDriverDisconnect();

            fIsConnected = false;

        }

        public virtual void OnDisconnecting()
        {
        }

        public virtual void ConfigureCapture()
        {
        }
        #endregion

        #region Properties
        public void SetFramesPerSecond(int fps)
        {
            // We're not capturing in a steady stream, but 
            // we do need to make sure the capture configuration
            // is reset
            FramePerSecond = fps;
            ConfigureCapture();
        }

        public int CaptureInterval
        {
            get { return fInterval; }
            protected set { fInterval = value; }
        }

        public int FramePerSecond
        {
            get { return fFps; }
            protected set
            {
                fFps = value;
                fInterval = (int)((1.0 / fFps * 1000000.0) + 0.5);  // We specify intervals in microseconds
            }
        }

        public int Width
        {
            get { return fWidth; }
            protected set { fWidth = value; } 
        }

        public int Height
        {
            get { return fHeight; }
            protected set { fHeight = value; }
        }

        public int DriverIndex
        {
            get { return fDriverIndex; }
        }

        public bool IsConnected
        {
            get { return fIsConnected; }
        }
        #endregion

        #region Setting Callback Delegates
        public bool SetCallbackOnError(VfwcapErrorCallback fpProc)
        {
            bool success = 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_ERROR, 0, fpProc);
            return success;
        }

        public bool SetCallbackOnStatus(VfwcapStatusCallback fpProc)
        {
            return 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_STATUS, 0, fpProc);
        }

        //public bool capSetCallbackOnYield(Vfw.CallBackDelegate fpProc)
        //{
        //    return 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_YIELD, 0, fpProc);
        //}

        public bool SetCallbackOnFrame(VfwVideoFrameDelegate fpProc)
        {
            return 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_FRAME, 0, fpProc);
        }

        public bool SetCallbackOnVideoStream(VfwVideoFrameDelegate fpProc)
        {
            bool success = 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, fpProc);
            return success;
        }

        //public bool capSetCallbackOnWaveStream(Vfw.CallBackDelegate fpProc)
        //{
        //    return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_WAVESTREAM, 0, fpProc);
        //}

        public bool SetCallbackOnCapControl(VfwcapControlCallback fpProc)
        {
            return 1 == SendCallbackMessage(fWindowHandle, Vfw.WM_CAP_SET_CALLBACK_CAPCONTROL, 0, fpProc);
        }
#endregion

        #region User Data
        public bool capSetUserData(int lUser)
        {
            return 1 ==  SendMessage(Vfw.WM_CAP_SET_USER_DATA, 0, lUser);
        }

        public bool capGetUserData()
        {
            return 1 ==  SendSimpleMessage(Vfw.WM_CAP_GET_USER_DATA);
        }
        #endregion

        #region Driver Connection
        public bool capDriverConnect()
        {
            return 1 ==  SendMessage(Vfw.WM_CAP_DRIVER_CONNECT, (uint)DriverIndex, 0);
        }

        public bool capDriverDisconnect()
        {
            return 1 ==  SendSimpleMessage(Vfw.WM_CAP_DRIVER_DISCONNECT);
        }
        #endregion

        #region Driver Properties
        public bool capDriverGetName(StringBuilder szName, int wSize)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_DRIVER_GET_NAME, (uint)(wSize), szName);
        }

        public bool capDriverGetVersion(StringBuilder szVer, int wSize)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_DRIVER_GET_VERSION, (uint)(wSize), szVer);
        }

        public bool capDriverGetCaps(ref CAPDRIVERCAPS s)
        {
            int wSize = Marshal.SizeOf(s);
            bool success = (1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_DRIVER_GET_CAPS, (uint)wSize, ref s));
            return success;
        }
        #endregion

        #region Capture File
        public bool capFileSetCaptureFile(string szName)
        {
            bool success = (1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_FILE_SET_CAPTURE_FILE, 0, szName));
            return success;
        }

        public bool capFileGetCaptureFile(StringBuilder szName, int wSize)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_FILE_GET_CAPTURE_FILE, (uint)(wSize), szName);
        }

        public bool capFileAlloc(Int32 dwSize)
        {
            return 1 == SendMessage(Vfw.WM_CAP_FILE_ALLOCATE, 0, dwSize);
        }

        public bool capFileSaveAs(string szName)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_FILE_SAVEAS, 0, szName);
        }

        public bool capFileSetInfoChunk(ref CAPINFOCHUNK infoChunk)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_FILE_SET_INFOCHUNK, 0, ref infoChunk);
        }

        public bool capFileSaveDIB(string szName)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_FILE_SAVEDIB, 0, szName);
        }

        public bool capEditCopy()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_EDIT_COPY);
        }
#endregion

        #region Audio Format
        public bool capSetAudioFormat(ref WAVEFORMATEX s, int wSize)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_AUDIOFORMAT, (uint)wSize, ref s);
        }

        public int capGetAudioFormat(ref WAVEFORMATEX s, int wSize)
        {
            int retValue = SendMessageW(fWindowHandle, Vfw.WM_CAP_GET_AUDIOFORMAT, (uint)wSize, ref s);
            return retValue;
        }

        public int capGetAudioFormatSize()
        {
            int retValue = SendSimpleMessage(Vfw.WM_CAP_GET_AUDIOFORMAT);
            return retValue;
        }
        #endregion

        #region Video Format
        /// <summary>
        /// Retrieves a copy of the video format information.
        /// </summary>
        /// <param name="s"></param>
        /// <returns>Returns 0 if not connected to driver.</returns>
        protected int capGetVideoFormat(ref BITMAPINFO s)
        {
            int wSize = Marshal.SizeOf(s);
            int  bytesCopied = SendMessageW(fWindowHandle, Vfw.WM_CAP_GET_VIDEOFORMAT, (uint)wSize, ref s);

            return bytesCopied;
        }

        /// <summary>
        /// Retrieve the size needed to hold the data structure representing the video format
        /// information.
        /// </summary>
        /// <returns></returns>
        protected int capGetVideoFormatSize()
        {
            int retValue = SendSimpleMessage(Vfw.WM_CAP_GET_VIDEOFORMAT);

            return retValue;
        }

        protected bool capSetVideoFormat(ref BITMAPINFO s)
        {
            int wSize = Marshal.SizeOf(s);
            bool success = 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_VIDEOFORMAT, (uint)wSize, ref s);
            return success;
        }
        #endregion

        #region Configuration Dialog Windows
        public bool capDlgVideoFormat()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_DLG_VIDEOFORMAT);
        }

        public bool capDlgVideoSource()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_DLG_VIDEOSOURCE);
        }

        public bool capDlgVideoDisplay()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_DLG_VIDEODISPLAY);
        }

        protected bool capDlgVideoCompression()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_DLG_VIDEOCOMPRESSION);
        }
        #endregion

        #region Preview and Overlay
        protected bool capPreview(bool f)
        {
            int BOOL = f == true ? 1 : 0;
            return 1 ==  SendMessage(Vfw.WM_CAP_SET_PREVIEW, (uint)BOOL, 0);
        }

        protected bool capPreviewRate(int wMS)
        {
            return 1 == SendMessage(Vfw.WM_CAP_SET_PREVIEWRATE, (uint)(wMS), 0);
        }

        protected bool capPreviewScale(bool f)
        {
            int BOOL = f == true ? 1 : 0;
            return 1 == SendMessage(Vfw.WM_CAP_SET_SCALE, (uint)(BOOL), 0);
        }

        protected bool capOverlay(bool f)
        {
            int BOOL = f == true ? 1 : 0;
            return 1 == SendMessage(Vfw.WM_CAP_SET_OVERLAY, (uint)(BOOL), 0);
        }
        #endregion

        #region Frame Grab
        /// <summary>
        /// Grab a single frame from the device.  Overlay and preview are stopped if they
        /// are currently active.
        /// </summary>
        /// <returns></returns>
        protected bool capGrabFrame()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_GRAB_FRAME);
        }

        /// <summary>
        /// Grab a single from from the device.  Overlay and Preview are not stopped.
        /// </summary>
        /// <returns></returns>
        protected bool capGrabFrameNoStop()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_GRAB_FRAME_NOSTOP);
        }
        #endregion

        #region Stream Capture
        protected bool capGetStatus(ref CAPSTATUS s)
        {
            int wSize = Marshal.SizeOf(s);
            bool success = 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_GET_STATUS, (uint)wSize, ref s);
            return success;
        }

        protected bool capSetScrollPos(ref POINT lpP)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_SCROLL, 0, ref lpP);
        }

        public bool capCaptureSequence()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_SEQUENCE);
        }

        public bool capCaptureSequenceNoFile()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_SEQUENCE_NOFILE);
        }

        protected bool capCaptureStop()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_STOP);
        }

        protected bool capCaptureAbort()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_ABORT);
        }

        protected bool capCaptureSingleFrameOpen()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_SINGLE_FRAME_OPEN);
        }

        protected bool capCaptureSingleFrameClose()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_SINGLE_FRAME_CLOSE);
        }

        protected bool capCaptureSingleFrame()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_SINGLE_FRAME);
        }

        protected bool capCaptureGetSetup(ref CAPTUREPARMS s)
        {
            uint wSize = (uint)Marshal.SizeOf(s);
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_GET_SEQUENCE_SETUP, wSize, ref s);
        }

        protected bool capCaptureSetSetup(ref CAPTUREPARMS s)
        {
            uint wSize = (uint)Marshal.SizeOf(s);
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_SEQUENCE_SETUP, wSize, ref s);
        }
        #endregion

        #region MCI Interface
        protected bool capSetMCIDeviceName(string szName)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_SET_MCI_DEVICE, 0, szName);
        }

        protected bool capGetMCIDeviceName(StringBuilder szName, int wSize)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_GET_MCI_DEVICE, (uint)(wSize), szName);
        }
        #endregion

        #region Palette Control
        protected bool capPaletteOpen(string szName)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_PAL_OPEN, 0, szName);
        }

        protected bool capPaletteSave(string szName)
        {
            return 1 == SendMessageW(fWindowHandle, Vfw.WM_CAP_PAL_SAVE, 0, szName);
        }

        protected bool capPalettePaste()
        {
            return 1 == SendSimpleMessage(Vfw.WM_CAP_PAL_PASTE);
        }

        protected bool capPaletteAuto(int iFrames, ref int iColors)
        {
            return 1 ==  SendMessage(Vfw.WM_CAP_PAL_AUTOCREATE, (uint)iFrames, (iColors));
        }

        protected bool capPaletteManual(int fGrab, int iColors)
        {
            return 1 ==  SendMessage(Vfw.WM_CAP_PAL_MANUALCREATE, (uint)(fGrab), (iColors));
        }
        #endregion

        #region Helper functions
        public int SendSimpleMessage(int msg)
        {
            int retValue = User32.SendMessage(fWindowHandle, msg, IntPtr.Zero, IntPtr.Zero);
            return retValue;
        }

        public int SendMessage(int msg, uint wParam, int lParam)
        {

            int retValue = User32.SendMessageW(fWindowHandle, msg, new IntPtr(wParam), new IntPtr(lParam));

            return retValue;
        }

        #endregion

        #region Interop Signatures
        [DllImport("user32", EntryPoint = "SendMessage")]
        static extern int SendCallbackMessage([In] IntPtr hWnd, int wMsg, uint wParam, VfwVideoFrameDelegate lParam);

        [DllImport("user32", EntryPoint = "SendMessage")]
        static extern int SendCallbackMessage([In] IntPtr hWnd, int wMsg, uint wParam, VfwcapControlCallback lParam);

        [DllImport("user32", EntryPoint = "SendMessage")]
        static extern int SendCallbackMessage([In] IntPtr hWnd, int wMsg, uint wParam, VfwcapStatusCallback lParam);

        [DllImport("user32", EntryPoint = "SendMessage")]
        static extern int SendCallbackMessage([In] IntPtr hWnd, int wMsg, uint wParam, VfwcapErrorCallback lParam);

        
        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, [Out] [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, [In] [MarshalAs(UnmanagedType.LPWStr)] string lpString);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref BITMAPINFO caps);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref CAPDRIVERCAPS caps);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref CAPINFOCHUNK infoChunk);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref CAPSTATUS waveFormat);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref CAPTUREPARMS waveFormat);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref POINT waveFormat);

        [DllImportAttribute("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, uint wParam, ref WAVEFORMATEX waveFormat);
        #endregion

    }
}
