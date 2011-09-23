using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.User32
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int EnumWindowStationProcA([MarshalAs(UnmanagedType.LPStr)] StringBuilder stationName, IntPtr param1);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate int EnumWindowStationProcW([MarshalAs(UnmanagedType.LPWStr)] StringBuilder stationName, IntPtr param1);

    public partial class User32
    {
        // CloseWindowStation
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int CloseWindowStation(IntPtr hWinSta);
        
        // CreateWindowStation
        [DllImport("user32.dll", EntryPoint = "CreateWindowStationW")]
        public static extern IntPtr CreateWindowStationW([In] [MarshalAs(UnmanagedType.LPWStr)] string lpwinsta, 
            uint dwFlags, uint dwDesiredAccess, 
            [In] IntPtr lpsa);

        // EnumWindowStations
        [DllImport("user32.dll", EntryPoint="EnumWindowStationsA")]
        public static extern int EnumWindowStationsA(EnumWindowStationProcA lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int EnumWindowStationsW(EnumWindowStationProcW lpEnumFunc, IntPtr lParam);

        // GetProcessWindowStation
        [DllImport("user32.dll")]
        public static extern IntPtr GetProcessWindowStation();

        // LockWorkStation
        [DllImport("user32.dll", EntryPoint = "LockWorkStation", SetLastError = true)]
        public static extern uint LockWorkStation();

        // OpenWindowStation
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, int dwDesiredAccess);
        
        // SetProcessWindowStation
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetProcessWindowStation(IntPtr hWinSta);




    }
}
