using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
    public class WAVEINCAPSW
    {
        public short wMid;             /// WORD->unsigned short
        public short wPid;             /// WORD->unsigned short
        public int vDriverVersion;     /// MMVERSION->UINT->unsigned int

        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;          /// product name - WCHAR[32]
        public int dwFormats;          /// DWORD->unsigned int
        public short wChannels;        /// WORD->unsigned short
        public short wReserved1;       /// WORD->unsigned short
    }

    
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    //public struct WAVEINCAPSA
    //{
    //    public ushort wMid;         /// WORD->unsigned short
    //    public ushort wPid;         /// WORD->unsigned short
    //    public uint vDriverVersion; /// MMVERSION->UINT->unsigned int
    //    [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
    //    public string szPname;      /// CHAR[32]
    //    public uint dwFormats;      /// DWORD->unsigned int
    //    public ushort wChannels;    /// WORD->unsigned short
    //    public ushort wReserved1;   /// WORD->unsigned short
    //}
}
