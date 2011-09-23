using System;
using System.Runtime.InteropServices;

namespace TOAPI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_ATTRIBUTES
    {
        public int nLength;                 // DWORD
        public IntPtr lpSecurityDescriptor;
        public int bInheritHandle;          // BOOL
    }
}