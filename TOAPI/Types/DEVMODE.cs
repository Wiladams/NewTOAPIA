using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Types
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DEVMODE
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;
        public ushort dmSpecVersion;
        public ushort dmDriverVersion;
        public ushort dmSize;
        public ushort dmDriverExtra;
        public uint dmFields;
        public Anonymous_c5e120a1_51e5_4e2a_9b1b_3eb7df849c5e Union1;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;
        public ushort dmLogPixels;
        public uint dmBitsPerPel;
        public uint dmPelsWidth;
        public uint dmPelsHeight;
        public Anonymous_907bbdd4_5453_489f_ab45_8165ad4c079a Union2;
        public uint dmDisplayFrequency;
        public uint dmICMMethod;
        public uint dmICMIntent;
        public uint dmMediaType;
        public uint dmDitherType;
        public uint dmReserved1;
        public uint dmReserved2;
        public uint dmPanningWidth;
        public uint dmPanningHeight;

        public void Init()
        {
            dmSize = (ushort)Marshal.SizeOf(this);
            dmDeviceName = new string(' ', 32);
            dmFormName = new string(' ', 32);
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_c5e120a1_51e5_4e2a_9b1b_3eb7df849c5e
    {
        [FieldOffset(0)]
        public Anonymous_89c2f8db_e5d0_4038_b7f5_866aa97721de Struct1;
        [FieldOffset(0)]
        public Anonymous_6c302cb5_9def_4078_8640_35710d663447 Struct2;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_907bbdd4_5453_489f_ab45_8165ad4c079a
    {
        [FieldOffset(0)]
        public uint dmDisplayFlags;
        [FieldOffset(0)]
        public uint dmNup;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_89c2f8db_e5d0_4038_b7f5_866aa97721de
    {
        public short dmOrientation;
        public short dmPaperSize;
        public short dmPaperLength;
        public short dmPaperWidth;
        public short dmScale;
        public short dmCopies;
        public short dmDefaultSource;
        public short dmPrintQuality;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_6c302cb5_9def_4078_8640_35710d663447
    {
        public POINT dmPosition;
        public uint dmDisplayOrientation;
        public uint dmDisplayFixedOutput;
    }
}
