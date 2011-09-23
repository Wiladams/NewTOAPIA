using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct MIXERCONTROLW
    {
        public uint cbStruct;
        public uint dwControlID;
        public uint dwControlType;
        public uint fdwControl;
        public uint cMultipleItems;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szShortName;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string szName;

        public Anonymous_38113314_a7bc_4ab5_b662_c24f7e121378 Bounds;

        public Anonymous_16f81539_74a7_4546_8287_90e627b65f73 Metrics;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_38113314_a7bc_4ab5_b662_c24f7e121378
    {
        [FieldOffsetAttribute(0)]
        public Anonymous_0f02d211_5c10_4cff_aa73_ce88402f42c5 Struct1;

        [FieldOffsetAttribute(0)]
        public Anonymous_341d691a_a4d4_4c15_8f07_f14415c04b0a Struct2;

        /// DWORD[6]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U4)]
        [FieldOffsetAttribute(0)]
        public uint[] dwReserved;
    }

    [StructLayoutAttribute(LayoutKind.Explicit)]
    public struct Anonymous_16f81539_74a7_4546_8287_90e627b65f73
    {
        [FieldOffset(0)]
        public uint cSteps;
        [FieldOffset(0)]
        public uint cbCustomData;
        /// DWORD[6]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.U4)]
        [FieldOffset(0)]
        public uint[] dwReserved;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Anonymous_0f02d211_5c10_4cff_aa73_ce88402f42c5
    {
        public int lMinimum;
        public int lMaximum;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct Anonymous_341d691a_a4d4_4c15_8f07_f14415c04b0a
    {
        public uint dwMinimum;
        public uint dwMaximum;
    }
}
