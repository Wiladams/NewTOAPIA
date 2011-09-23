using System;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{
    public class StdHandleEnum
    {
        public const int STD_INPUT_HANDLE = -10;
        public const int STD_OUTPUT_HANDLE = -11;
        public const int STD_ERROR_HANDLE = -12;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct COORD
    {
        public short X;
        public short Y;

        public COORD(short x, short y)
        {
            X = x;
            Y = y;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct SMALL_RECT
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;

        public SMALL_RECT(short left, short top, short right, short bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct UniChar
    {
        [FieldOffset(0)]
        public ushort UnicodeChar;
        [FieldOffset(0)]
        public byte AsciiChar;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CHAR_INFO 
    {  
        public Char Character;  
        public ushort  Attributes; // CharacterAttributes
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_SCREEN_BUFFER_INFO
    {
        public COORD dwSize;
        public COORD dwCursorPosition;
        public ushort wAttributes;
        public SMALL_RECT srWindow;
        public COORD dwMaximumWindowSize;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_CURSOR_INFO
    {
        public int dwSize; 
        [MarshalAs(UnmanagedType.Bool)]
        public bool bVisible;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_FONT_INFO
    {
        public int nFont;
        public COORD dwFontSize;
    }

    // For Vista Only
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct CONSOLE_HISTORY_INFO
    {  
        public Int32 cbSize;  
        public Int32 HistoryBufferSize;  
        public Int32 NumberOfHistoryBuffers;  
        public Int32 dwFlags;

        public void Init()
        {
            cbSize = Marshal.SizeOf(this);
        }
    }



    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MODULEENTRY32
    {
        public uint dwSize;
        public uint th32ModuleID;
        public uint th32ProcessID;
        public uint GlblcntUsage;
        public uint ProccntUsage;
        public IntPtr modBaseAddr;
        public uint modBaseSize;
        public IntPtr hModule;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]    /// TCHAR[MAX_MODULE_NAME32 + 1]
        public string szModule;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]  /// TCHAR[MAX_PATH]
        public string szExePath;
    }


    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct PROCESSENTRY32
    {
        public uint dwSize;
        public uint cntUsage;
        public uint th32ProcessID;
        public IntPtr th32DefaultHeapID;      // Obsolete, always set to zero
        public uint th32ModuleID;
        public uint cntThreads;
        public uint th32ParentProcessID;
        public int pcPriClassBase;
        public uint dwFlags;

        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szExeFile;
    }




    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_INFORMATION
    {
        public IntPtr hProcess;
        public IntPtr hThread;
        public uint dwProcessId;
        public uint dwThreadId;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_POWER_STATUS
    {
        public byte ACLineStatus;
        public byte BatteryFlag;
        public byte BatteryLifePercent;
        public byte Reserved1;
        public uint BatteryLifeTime;
        public uint BatteryFullLifeTime;
    }


    // For - GetNativeSystemInfo
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_INFO
    {
        public Anonymous_dd1ca8d8_270d_4547_8677_80c8c5f76891 Union1;
        public uint dwPageSize;
        public IntPtr lpMinimumApplicationAddress;
        public IntPtr lpMaximumApplicationAddress;
        public uint dwActiveProcessorMask;
        public uint dwNumberOfProcessors;
        public uint dwProcessorType;
        public uint dwAllocationGranularity;
        public ushort wProcessorLevel;
        public ushort wProcessorRevision;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_dd1ca8d8_270d_4547_8677_80c8c5f76891
    {
        [FieldOffset(0)]
        public uint dwOemId;
        [FieldOffset(0)]
        public Anonymous_3a786109_04ed_4307_9948_7516e1258fcb Struct1;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_3a786109_04ed_4307_9948_7516e1258fcb
    {
        public ushort wProcessorArchitecture;
        public ushort wReserved;
    }




    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION
    {
        public uint ProcessorMask;
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public Anonymous_c11c7619_ea71_4c94_9a69_77694c9746c5 Union1;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_c11c7619_ea71_4c94_9a69_77694c9746c5
    {
        [FieldOffset(0)]
        public Anonymous_153c60ad_cf18_46a9_affd_127e106c378c ProcessorCore;
        [FieldOffset(0)]
        public Anonymous_11279bb0_4f59_41da_b361_5c1feb0ff4f4 NumaNode;
        [FieldOffset(0)]
        public CACHE_DESCRIPTOR Cache;

        /// ULONGLONG[2]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.R8)]
        [FieldOffset(0)]
        public double[] Reserved;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_153c60ad_cf18_46a9_affd_127e106c378c
    {
        public byte Flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_11279bb0_4f59_41da_b361_5c1feb0ff4f4
    {
        public uint NodeNumber;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CACHE_DESCRIPTOR
    {
        public byte Level;
        public byte Associativity;
        public ushort LineSize;
        public uint Size;
        public PROCESSOR_CACHE_TYPE Type;
    }

    //[StructLayout(LayoutKind.Sequential)]
    //public struct FILETIME
    //{
    //    public uint dwLowDateTime;
    //    public uint dwHighDateTime;
    //}

    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        [MarshalAs(UnmanagedType.U2)]
        public short Year;
        [MarshalAs(UnmanagedType.U2)]
        public short Month;
        [MarshalAs(UnmanagedType.U2)]
        public short DayOfWeek;
        [MarshalAs(UnmanagedType.U2)]
        public short Day;
        [MarshalAs(UnmanagedType.U2)]
        public short Hour;
        [MarshalAs(UnmanagedType.U2)]
        public short Minute;
        [MarshalAs(UnmanagedType.U2)]
        public short Second;
        [MarshalAs(UnmanagedType.U2)]
        public short Milliseconds;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct FILE_SEGMENT_ELEMENT
    {
        [FieldOffset(0)]
        public IntPtr Buffer;
        [FieldOffset(0)]
        public double Alignment;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WIN32_FIND_DATAW
    {
        public uint dwFileAttributes;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftCreationTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
        public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
        public uint nFileSizeHigh;
        public uint nFileSizeLow;
        public uint dwReserved0;
        public uint dwReserved1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string cFileName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string cAlternateFileName;
    }


    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct OFSTRUCT
    {
        public byte cBytes;
        public byte fFixedDisk;
        public ushort nErrCode;
        public ushort Reserved1;
        public ushort Reserved2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPathName;
    }

}
