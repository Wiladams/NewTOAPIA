using System;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{

    [StructLayout(LayoutKind.Sequential)]
    public struct OVERLAPPED
    {
        public IntPtr InternalLow;
        public IntPtr InternalHigh;
        public int OffsetLow;      /// DWORD->unsigned int
        public int OffsetHigh;  /// DWORD->unsigned int
        public IntPtr EventHandle;   //public Anonymous_09c34f72_48f5_4ebf_8c58_0e5ed7358919 Union1;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_09c34f72_48f5_4ebf_8c58_0e5ed7358919
    {
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public Anonymous_17a2e8a7_9186_49d2_b3c5_2153812bf61f Struct1;
        [System.Runtime.InteropServices.FieldOffsetAttribute(0)]
        public System.IntPtr Pointer;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Anonymous_17a2e8a7_9186_49d2_b3c5_2153812bf61f
    {

        /// DWORD->unsigned int
        public int Offset;

        /// DWORD->unsigned int
        public int OffsetHigh;
    }
}
