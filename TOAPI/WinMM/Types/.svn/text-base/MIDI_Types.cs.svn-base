using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct midihdr
    {

        /// LPSTR->CHAR*
        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string lpData;

        /// DWORD->unsigned int
        public uint dwBufferLength;

        /// DWORD->unsigned int
        public uint dwBytesRecorded;

        /// DWORD_PTR->ULONG_PTR->unsigned int
        public uint dwUser;

        /// DWORD->unsigned int
        public uint dwFlags;

        /// midihdr*
        public System.IntPtr lpNext;

        /// DWORD_PTR->ULONG_PTR->unsigned int
        public uint reserved;

        /// DWORD->unsigned int
        public uint dwOffset;

        /// DWORD_PTR[8]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U4)]
        public uint[] dwReserved;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIDIINCAPSW
    {

        /// WORD->unsigned short
        public ushort wMid;

        /// WORD->unsigned short
        public ushort wPid;

        /// MMVERSION->UINT->unsigned int
        public uint vDriverVersion;

        /// WCHAR[32]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;

        /// DWORD->unsigned int
        public uint dwSupport;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIDIOUTCAPSW
    {

        /// WORD->unsigned short
        public ushort wMid;

        /// WORD->unsigned short
        public ushort wPid;

        /// MMVERSION->UINT->unsigned int
        public uint vDriverVersion;

        /// WCHAR[32]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string szPname;

        /// WORD->unsigned short
        public ushort wTechnology;

        /// WORD->unsigned short
        public ushort wVoices;

        /// WORD->unsigned short
        public ushort wNotes;

        /// WORD->unsigned short
        public ushort wChannelMask;

        /// DWORD->unsigned int
        public uint dwSupport;
    }

    #region mmtime
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct mmtime_tag
    {

        /// UINT->unsigned int
        public uint wType;

        /// Anonymous_191604fe_770e_4688_aef5_8c113498eb25
        public Anonymous_191604fe_770e_4688_aef5_8c113498eb25 u;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_191604fe_770e_4688_aef5_8c113498eb25
    {

        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint ms;

        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint sample;

        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint cb;

        /// DWORD->unsigned int
        [FieldOffsetAttribute(0)]
        public uint ticks;

        /// Anonymous_9bd9399e_c332_40ac_ad3c_8155b5ea7388
        [FieldOffsetAttribute(0)]
        public Anonymous_9bd9399e_c332_40ac_ad3c_8155b5ea7388 smpte;

        /// Anonymous_cb1eb549_6c81_48bd_b5bf_228b52412176
        [FieldOffsetAttribute(0)]
        public Anonymous_cb1eb549_6c81_48bd_b5bf_228b52412176 midi;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Anonymous_9bd9399e_c332_40ac_ad3c_8155b5ea7388
    {

        /// BYTE->unsigned char
        public byte hour;

        /// BYTE->unsigned char
        public byte AnonymousMember1;

        /// BYTE->unsigned char
        public byte sec;

        /// BYTE->unsigned char
        public byte frame;

        /// BYTE->unsigned char
        public byte fps;

        /// BYTE->unsigned char
        public byte dummy;

        /// BYTE[2]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.I1)]
        public byte[] pad;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Anonymous_cb1eb549_6c81_48bd_b5bf_228b52412176
    {

        /// DWORD->unsigned int
        public uint songptrpos;
    }
#endregion
}
