using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.WinMM
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct MIXERLINE
    {
        public int structSize;
        public uint dwDestination;
        public uint dwSource;
        public uint dwLineID;
        public uint fdwLine;
        public uint dwUser;
        public uint dwComponentType;
        public uint cChannels;
        public uint cConnections;
        public uint cControls;

        /// WCHAR[16]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        /// WCHAR[64]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        /// Anonymous_31c351f2_1e00_4744_ab77_67de4479d1ce
        public Anonymous_31c351f2_1e00_4744_ab77_67de4479d1ce Target;

        public void Init()
        {
            structSize = Marshal.SizeOf(this);
            szShortName = new string(' ', 16);
            szName = new string(' ', 64);

            Target.Init();
        }

    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct Anonymous_31c351f2_1e00_4744_ab77_67de4479d1ce
    {
        public uint dwType;
        public uint dwDeviceID;
        public ushort wMid;
        public ushort wPid;

        /// MMVERSION->UINT->unsigned int
        public uint vDriverVersion;

        /// WCHAR[32]
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;

        public void Init()
        {
            szPname = new string(' ', 32);
        }
    }
}
