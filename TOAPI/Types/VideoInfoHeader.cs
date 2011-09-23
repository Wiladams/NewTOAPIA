using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VIDEOHDR
    {
        public IntPtr lpData;
        public uint dwBufferLength;
        public uint dwBytesUsed;
        public uint dwTimeCaptured;
        public uint dwUser;
        public uint dwFlags;

        /// DWORD_PTR[4]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] dwReserved;
    }

    //[StructLayout(LayoutKind.Sequential)]
    //public struct VIDEOHEADER
    //{
    //    public IntPtr lpData;
    //    public uint dwBufferLength;
    //    public uint dwBytesUsed;
    //    public uint dwTimeCaptured;
    //    public uint dwUser;
    //    public uint dwFlags;
    //    [MarshalAs(System.Runtime.InteropServices.UnmanagedType.SafeArray)]
    //    byte[] dwReserved;
    //}

    [ComVisible(false), 
    StructLayout(LayoutKind.Sequential)]
    public class VIDEOINFOHEADER		// VIDEOINFOHEADER
    {
        public RECT SrcRect;
        public RECT TargetRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public BITMAPINFOHEADER BmiHeader;
    }

    [StructLayout(LayoutKind.Sequential), ComVisible(false)]
    public class VIDEOINFOHEADER2		// VIDEOINFOHEADER2
    {
        public RECT SrcRect;
        public RECT TargetRect;
        public int BitRate;
        public int BitErrorRate;
        public long AvgTimePerFrame;
        public int InterlaceFlags;
        public int CopyProtectFlags;
        public int PictAspectRatioX;
        public int PictAspectRatioY;
        public int ControlFlags;
        public int Reserved2;
        public BITMAPINFOHEADER BmiHeader;
    };
}
