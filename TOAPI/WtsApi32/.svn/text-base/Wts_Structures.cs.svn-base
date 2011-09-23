using System;
using System.Runtime.InteropServices;

namespace TOAPI.WtsApi32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WTS_PROCESS_INFO
    {
        public int SessionID;
        public int ProcessID;
        //This is a pointer to string...
        public IntPtr ProcessName;
        public int UserSid;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct WTSSESSION_NOTIFICATION
    {
        public uint cbSize;
        public uint dwSessionId;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct ChannelEntryPoints
    {
        public int Size;
        public int ProtocolVersion;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Wts.VirtualChannelInitDelegate VirtualChannelInit;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Wts.VirtualChannelOpenDelegate VirtualChannelOpen;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Wts.VirtualChannelCloseDelegate VirtualChannelClose;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public Wts.VirtualChannelWriteDelegate VirtualChannelWrite;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct ChannelDef
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string name;
        public ChannelOptions options;
    }


    [StructLayout(LayoutKind.Sequential)]
    public class WTS_CLIENT_ADDRESS
    {
        public uint AddressFamily;

        /// BYTE[20]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20, ArraySubType = UnmanagedType.I1)]
        public byte[] Address;

        public WTS_CLIENT_ADDRESS()
        {
            Address = new byte[20];
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct WTS_CLIENT_DISPLAY
    {
        public uint HorizontalResolution;
        public uint VerticalResolution;
        public uint ColorDepth;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WTS_SERVER_INFO
    {
        public string pServerName;
    }
}
