using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{
    //[StructLayout(LayoutKind.Sequential)]
    //public struct TOKEN_PRIVILEGES
    //{
    //    public int PrivilegeCount;
    //    //LUID_AND_ATRIBUTES
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
    //    public int[] Privileges;
    //}

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct TOKEN_PRIVILEGES
    {
        public int PrivilegeCount;

        /// LUID_AND_ATTRIBUTES[1]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1, ArraySubType = UnmanagedType.Struct)]
        public LUID_AND_ATTRIBUTES[] Privileges;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct LUID_AND_ATTRIBUTES
    {
        public LUID Luid;
        public uint Attributes;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct LUID
    {
        public uint LowPart;
        public int HighPart;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SID_AND_ATTRIBUTES
    {
        public IntPtr Sid;
        public int Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TOKEN_MANDATORY_LABEL
    {
        public SID_AND_ATTRIBUTES Label;
    };

}
