using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.PSAPI
{
    public delegate bool EnumPageFilesProc(IntPtr pContext, ref ENUM_PAGE_FILE_INFORMATION pPageFileInfo, string lpFilename);

    [StructLayout(LayoutKind.Sequential)]
    public struct ENUM_PAGE_FILE_INFORMATION
    {
        int cb;
        int Reserved;
        IntPtr TotalSize;
        IntPtr TotalInUse;
        IntPtr PeakUsage;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct MODULEINFO
    {
        public IntPtr lpBaseOfDll;
        public uint SizeOfImage;
        public System.IntPtr EntryPoint;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PERFORMANCE_INFORMATION
    {
        int cb;
        IntPtr CommitTotal;
        IntPtr CommitLimit;
        IntPtr CommitPeak;
        IntPtr PhysicalTotal;
        IntPtr PhysicalAvailable;
        IntPtr SystemCache;
        IntPtr KernelTotal;
        IntPtr KernelPaged;
        IntPtr KernelNonpaged;
        IntPtr PageSize;
        int IntPtrCount;
        int ProcessCount;
        int ThreadCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PROCESS_MEMORY_COUNTERS
    {
        int cb;
        int PageFaultCount;
        IntPtr PeakWorkingSetSize;
        IntPtr WorkingSetSize;
        IntPtr QuotaPeakPagedPoolUsage;
        IntPtr QuotaPagedPoolUsage;
        IntPtr QuotaPeakNonPagedPoolUsage;
        IntPtr QuotaNonPagedPoolUsage;
        IntPtr PagefileUsage;
        IntPtr PeakPagefileUsage;
    }


    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct PSAPI_WS_WATCH_INFORMATION
    {
        public IntPtr FaultingPc;
        public IntPtr FaultingVa;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct PSAPI_WS_WATCH_INFORMATION_EX
    {
        public PSAPI_WS_WATCH_INFORMATION BasicInfo;
        public uint FaultingThreadId;
        public uint Flags;
    }

}
