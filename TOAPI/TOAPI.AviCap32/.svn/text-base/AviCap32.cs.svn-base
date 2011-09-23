using System;
using System.Runtime.InteropServices;

namespace TOAPI.AviCap32
{
    public class AviCap32
    {   
        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
           int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        [DllImport("avicap32.dll")]
        public static extern bool capGetDriverDescription(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
            int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);


        [DllImport("avicap32.dll", EntryPoint = "capCreateCaptureWindow")]
        public static extern IntPtr capCreateCaptureWindow([In] string lpszWindowName, 
            int dwStyle, int X, int Y, int nWidth, int nHeight, IntPtr hwndParent, 
            int nID);

        [DllImport("avicap32.dll")]
        public static extern IntPtr capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

    }
}
