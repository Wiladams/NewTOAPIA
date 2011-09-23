using System;
using System.Runtime.InteropServices;


namespace TOAPI.WinMM
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct MMCKINFO
    {
        public uint ckid;           /// FOURCC->DWORD->unsigned int
        public uint cksize;         /// DWORD->unsigned int
        public uint fccType;        /// FOURCC->DWORD->unsigned int
        public uint dwDataOffset;   /// DWORD->unsigned int
        public uint dwFlags;        /// DWORD->unsigned int
    }
}
