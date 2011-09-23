using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.WinMM
{

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MIXERCAPS
    {
        public ushort wMid;
        public ushort wPid;
        public uint vDriverVersion;     /// MMVERSION->UINT->unsigned int
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]    /// WCHAR[32]
        public string szPname;
        public uint fdwSupport;         
        public uint cDestinations;      

        public void Init()
        {
            szPname = new string(' ', 32);
        }
    }
}
