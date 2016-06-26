using System;
using System.Runtime.InteropServices;

namespace TOAPI.Setup
{
    // Two declarations for the DEV_BROADCAST_DEVICEINTERFACE structure.

    // Use this one in the call to RegisterDeviceNotification() and
    // in checking dbch_devicetype in a DEV_BROADCAST_HDR structure:

    [StructLayout(LayoutKind.Sequential)]
    public class DEV_BROADCAST_DEVICEINTERFACE
    {
        internal Int32 dbcc_size;
        internal Int32 dbcc_devicetype;
        internal Int32 dbcc_reserved;
        internal Guid dbcc_classguid;
        internal Int16 dbcc_name;
    }

    // Use this to read the dbcc_name String and classguid:

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class DEV_BROADCAST_DEVICEINTERFACE_1
    {
        public Int32 dbcc_size;
        public Int32 dbcc_devicetype;
        public Int32 dbcc_reserved;
        public Guid dbcc_classguid;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string dbcc_name;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DEV_BROADCAST_HDR
    {
        internal Int32 dbch_size;
        internal Int32 dbch_devicetype;
        internal Int32 dbch_reserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SP_DEVICE_INTERFACE_DATA
    {
        public Int32 cbSize;
        public Guid InterfaceClassGuid;
        public Int32 Flags;
        public IntPtr Reserved;
    }

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
    public struct SP_DEVICE_INTERFACE_DETAIL_DATA
    {
        public Int32 cbSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public String DevicePath;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SP_DEVINFO_DATA
    {
        internal Int32 cbSize;
        internal System.Guid ClassGuid;
        internal Int32 DevInst;
        internal Int32 Reserved;
    }

}
