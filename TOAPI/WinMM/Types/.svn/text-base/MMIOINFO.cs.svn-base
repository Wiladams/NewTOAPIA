using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.WinMM
{
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MMIOINFO
    {
        public uint dwFlags;        /// DWORD->unsigned int
        public uint fccIOProc;      /// FOURCC->DWORD->unsigned int
        public MMIOPROC pIOProc;    /// LPMMIOPROC->MMIOPROC*
        public uint wErrorRet;      /// UINT->unsigned int
        public IntPtr htask;        /// HTASK->HTASK__*
        public int cchBuffer;       /// LONG->int
        public string pchBuffer;    /// HPSTR->char*
        public string pchNext;      /// HPSTR->char*
        public string pchEndRead;   /// HPSTR->char*
        public string pchEndWrite;  /// HPSTR->char*
        public int lBufOffset;      /// LONG->int
        public int lDiskOffset;     /// LONG->int
        /// DWORD[3]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
        public uint[] adwInfo;
        public uint dwReserved1;    /// DWORD->unsigned int
        public uint dwReserved2;    /// DWORD->unsigned int
        public IntPtr hmmio;        /// HMMIO->HMMIO__*
    }
}
