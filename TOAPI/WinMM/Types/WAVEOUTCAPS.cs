using System;
using System.Runtime.InteropServices;
using System.Security;

namespace TOAPI.WinMM
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WAVEOUTCAPS
    {
        public ushort wMid;
        public ushort wPid;
        public uint vDriverVersion;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]           /// CHAR[32]
        public string szPname;
        public WAVEFORMATS dwFormats;
        public ushort wChannels;
        public ushort wReserved1;
        public WAVECAPS dwSupport;
    }

    //[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    //public class WAVEOUTCAPSW
    //{
    //    public ushort wMid;             // Manufacturer ID
    //    public ushort wPid;             // Product ID
    //    public uint vDriverVersion;     // Driver version
    //    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]   /// WCHAR[32]
    //    public string szPname;
    //    public uint dwFormats;
    //    public ushort wChannels;
    //    public ushort wReserved1;
    //    public uint dwSupport;
    //}

    //[StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    //public struct WAVEOUTCAPSA
    //{
    //    public ushort wMid;
    //    public ushort wPid;
    //    public uint vDriverVersion;

    //    /// CHAR[32]
    //    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string szPname;
    //    public uint dwFormats;
    //    public ushort wChannels;
    //    public ushort wReserved1;
    //    public uint dwSupport;
    //}

}
