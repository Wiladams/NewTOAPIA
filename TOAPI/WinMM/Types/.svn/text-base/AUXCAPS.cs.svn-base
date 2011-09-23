using System;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct AUXCAPSW
    {
        public ushort wMid;
        public ushort wPid;
        public uint vDriverVersion;

        /// WCHAR[32]
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;
        public ushort wTechnology;
        public ushort wReserved1;
        public uint dwSupport;
    }
}
