using System;
using System.Runtime.InteropServices;
using System.Text;
//using System.Drawing;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HANDLETABLE
    {

        /// HGDIOBJ[1]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.SysUInt)]
        public IntPtr[] objectHandle;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct METAFILEPICT
    {
        public int mm;
        public int xExt;
        public int yExt;
        public IntPtr hMF;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ENHMETARECORD
    {
        public uint iType;
        public uint nSize;

        /// DWORD[1]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.U4)]
        public uint[] dParm;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ENHMETAHEADER
    {
        public uint iType;
        public int nSize;
        public RECT rclBounds;
        public RECT rclFrame;
        public uint dSignature;
        public uint nVersion;
        public uint nBytes;
        public uint nRecords;
        public ushort nHandles;
        public ushort sReserved;
        public uint nDescription;
        public uint offDescription;
        public uint nPalEntries;
        public SIZE szlDevice;
        public SIZE szlMillimeters;
        public uint cbPixelFormat;
        public uint offPixelFormat;
        public uint bOpenGL;
        public SIZE szlMicrometers;
    }
}
