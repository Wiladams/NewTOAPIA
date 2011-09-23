using System;
using System.Runtime.InteropServices;

namespace TOAPI.User32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct USEROBJECTFLAGS
    {
        [MarshalAs(UnmanagedType.Bool)]
        public bool fInherit;

        [MarshalAsAttribute(UnmanagedType.Bool)]
        public bool fReserved;

        public uint dwFlags;
    }
}
