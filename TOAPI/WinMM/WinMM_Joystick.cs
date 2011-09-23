using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    public partial class winmm
    {
        [DllImport("winmm.dll", EntryPoint = "joyGetNumDevs")]
        public static extern int joyGetNumDevs();

        [DllImport("winmm.dll", EntryPoint = "joyGetPos")]
        public static extern int joyGetPos(int uJoyID, ref JoystickInfo pji);

        [DllImport("winmm.dll", EntryPoint = "joyGetPosEx")]
        public static extern int joyGetPosEx(int uJoyID, ref JoystickInfoExtra pji);

        [DllImport("winmm.dll", EntryPoint = "joySetCapture")]
        public static extern int joySetCapture(IntPtr hwnd, int uJoyID, int uPeriod, [MarshalAsAttribute(UnmanagedType.Bool)] bool fChanged);

        [DllImport("winmm.dll", EntryPoint = "joyReleaseCapture")]
        public static extern int joyReleaseCapture(int uJoyID);

        [DllImport("winmm.dll", CharSet=CharSet.Unicode, EntryPoint = "joyGetDevCapsW")]
        public static extern int joyGetDevCapsW( int uJoyID, ref JoystickCapabilities pjc, int cbjc);

        [DllImport("winmm.dll", EntryPoint = "joyGetThreshold")]
        public static extern int joyGetThreshold(int uJoyID, ref int puThreshold);

        [DllImport("winmm.dll", EntryPoint = "joySetThreshold")]
        public static extern int joySetThreshold(int uJoyID, int uThreshold);

    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct JoystickInfo
    {
        /// int->unsigned int
        public int wXpos;

        /// int->unsigned int
        public int wYpos;

        /// int->unsigned int
        public int wZpos;

        /// int->unsigned int
        public int wButtons;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct JoystickInfoExtra
    {
        /// DWORD->unsigned int
        public int dwSize;

        /// DWORD->unsigned int
        public int dwFlags;

        /// DWORD->unsigned int
        public int dwXpos;

        /// DWORD->unsigned int
        public int dwYpos;

        /// DWORD->unsigned int
        public int dwZpos;

        /// DWORD->unsigned int
        public int dwRpos;

        /// DWORD->unsigned int
        public int dwUpos;

        /// DWORD->unsigned int
        public int dwVpos;

        /// DWORD->unsigned int
        public int dwButtons;

        /// DWORD->unsigned int
        public int dwButtonNumber;

        /// DWORD->unsigned int
        public int dwPOV;

        /// DWORD->unsigned int
        public int dwReserved1;

        /// DWORD->unsigned int
        public int dwReserved2;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct JoystickCapabilities
    {
        /// WORD->unsigned short
        public ushort wMid;

        /// WORD->unsigned short
        public ushort wPid;

        /// WCHAR[32]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;

        /// int->unsigned int
        public int wXmin;

        /// int->unsigned int
        public int wXmax;

        /// int->unsigned int
        public int wYmin;

        /// int->unsigned int
        public int wYmax;

        /// int->unsigned int
        public int wZmin;

        /// int->unsigned int
        public int wZmax;

        /// int->unsigned int
        public int wNumButtons;

        /// int->unsigned int
        public int wPeriodMin;

        /// int->unsigned int
        public int wPeriodMax;

        /// int->unsigned int
        public int wRmin;

        /// int->unsigned int
        public int wRmax;

        /// int->unsigned int
        public int wUmin;

        /// int->unsigned int
        public int wUmax;

        /// int->unsigned int
        public int wVmin;

        /// int->unsigned int
        public int wVmax;

        /// int->unsigned int
        public int wCaps;

        /// int->unsigned int
        public int wMaxAxes;

        /// int->unsigned int
        public int wNumAxes;

        /// int->unsigned int
        public int wMaxButtons;

        /// WCHAR[32]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szRegKey;

        /// WCHAR[260]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szOEMVxD;
    }

}
