using System;
using System.Text;  // For StringBuilder
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.User32
{
    partial class User32
    {
        // Capture and release
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ReleaseCapture();

        // Mouse and keyboard
        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        public static extern uint SendInput(uint cInputs,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] INPUT[] pInputs,
            int cbSize);

        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        public static extern uint SendInput64(uint nInputs,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] INPUT64[] pInputs,
            int cbSize);

        //[DllImport("user32.dll", EntryPoint = "SendInput")]
        //public static extern uint SendInput(uint cInputs,
        //    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] MOUSEINPUT[] pInputs,
        //    int cbSize);


        //[DllImport("user32.dll", EntryPoint = "SendInput")]
        //public static extern uint SendInput(uint cInputs,
        //    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] KEYBDINPUT[] pInputs,
        //    int cbSize);

        //[DllImport("user32.dll", EntryPoint = "SendInput")]
        //public static extern uint SendInput(uint cInputs,
        //    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 0)] HARDWAREINPUT[] pInputs,
        //    int cbSize);

        // Dealing with keyboard
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int vKey);

        /// The nTypeFlag must be one of the following
        ///  0 - Return keyboard type
        ///	1 - Return keyboard subtype
        ///	2 - Return number of function keys
        ///
        [DllImport("user32.dll")]
        public static extern int GetKeyboardType(int nTypeFlag);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", EntryPoint = "VkKeyScanExW")]
        public static extern short VkKeyScanExW(char ch, IntPtr dwhkl);



        // Dealing with mouse
        /// <summary>
        /// The following functions are all the ones defined in the user32.dll library
        /// related to Cursor manipulations.  
        /// </summary>
        public const int CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        public static extern IntPtr CreateCursor(IntPtr hInst, int xHotSpot, int yHotSpot, int nWidth, int nHeight, ref byte[] pvANDPlane, ref byte[] pvXORPlane);

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);

        [DllImport("user32.dll")]
        public static extern int SetCursor(IntPtr hCursor);

        [DllImport("user32.dll")]
        public static extern int ShowCursor(bool bShow);

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]
        public static extern int GetCursorPos([Out] out POINT lpPoint);

        [DllImport("user32.dll", EntryPoint = "GetCursorInfo", SetLastError = true)]
        public static extern int GetCursorInfo(ref CURSORINFO cursorInfo);

        [DllImport("user32.dll", EntryPoint = "SetCursorPos", SetLastError = true)]
        public static extern int SetCursorPos(int X, int Y);

        [DllImport("user32.dll", EntryPoint = "SetCapture")]
        public static extern IntPtr SetCapture(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "TrackMouseEvent", SetLastError = true)]
        public static extern int TrackMouseEvent(ref TRACKMOUSEEVENT lpEventTrack);

        [DllImport("user32.dll", EntryPoint = "DragDetect")]
        public static extern int DragDetect(IntPtr hwnd, POINT pt);





        [DllImport("user32.dll", EntryPoint = "DefRawInputProc")]
        public static extern int DefRawInputProc([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 1)] RAWINPUT[] paRawInput, int nInput, int cbSizeHeader);

        //        579  242 00004DC3 RegisterRawInputDevices
        [DllImport("user32.dll", EntryPoint = "RegisterRawInputDevices")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, 
            uint uiNumDevices, int cbSize);

        /// <summary>
        /// Register a raw input device based on a structure being passed in.
        /// </summary>
        /// <param name="device">Device information.</param>
        /// <returns>TRUE if successful, FALSE if not.</returns>
        public static bool RegisterRawInputDevice(RAWINPUTDEVICE device)
        {
            RAWINPUTDEVICE[] devices = new RAWINPUTDEVICE[1];        // Raw input devices.

            devices[0] = device;
            return RegisterRawInputDevices(devices, 1, Marshal.SizeOf(typeof(RAWINPUTDEVICE)));
        }

        // GetRawInputDeviceList
        //        355  162 000655AB GetRawInputDeviceList
        [DllImport("user32.dll", EntryPoint = "GetRawInputDeviceList")]
        public static extern uint GetRawInputDeviceList([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 1)] RAWINPUTDEVICELIST[] pRawInputDeviceList, 
            ref uint puiNumDevices, uint cbSize);

        [DllImport("user32.dll", EntryPoint = "GetRawInputDeviceList")]
        public static extern uint GetRawInputDeviceList(IntPtr pRawInputDeviceList, ref uint puiNumDevices, uint cbSize);

        [DllImport("user32.dll")]
        public static extern int GetRawInputDeviceInfo(IntPtr hDevice, uint uiCommand, IntPtr pData, ref uint pbSize);

        // Getting raw input
        [DllImport("user32.dll", EntryPoint = "GetRawInputData")]
        public static extern uint GetRawInputData(IntPtr hRawInput, uint uiCommand, IntPtr pData, ref uint pcbSize, uint cbSizeHeader);

    }
}
