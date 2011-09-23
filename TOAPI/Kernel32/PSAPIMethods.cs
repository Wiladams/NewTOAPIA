using System;
using System.Text;
using System.Runtime.InteropServices;

namespace TOAPI.PSAPI
{

    public class PSAPI
    {
        //EmptyWorkingSet 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EmptyWorkingSet(IntPtr hProcess);


        //EnumDeviceDrivers 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumDeviceDrivers([Out] IntPtr lpImageBase, [In] int cb, [Out] out int lpcbNeeded);


        //EnumPageFiles 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumPageFiles(EnumPageFilesProc pCallbackRoutine, IntPtr lpContext);


        //EnumProcesses 
        [DllImport("psapi.dll", EntryPoint = "EnumProcesses")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumProcesses(int[] pProcessIds, int cb, [Out] out int pBytesReturned);

        [DllImport("psapi.dll", EntryPoint = "EnumProcesses")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumProcesses(IntPtr procIDs, int cb, ref int pBytesReturned);



        //EnumProcessModules 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumProcessModules(IntPtr hProcess, IntPtr[] lphModule, int cb, ref int lpcbNeeded);

     
        //EnumProcessModulesEx 
        [DllImport("psapi.dll", EntryPoint = "EnumProcessModulesEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumProcessModulesEx([In] IntPtr hProcess, ref IntPtr lphModule, uint cb, [Out] out uint lpcbNeeded, uint dwFilterFlag);


        //GetDeviceDriverBaseName 
        [DllImport("psapi.dll", EntryPoint = "GetDeviceDriverBaseName")]
        public static extern uint GetDeviceDriverBaseName([In] IntPtr ImageBase, [Out] [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder lpBaseName, uint nSize);


        //GetDeviceDriverFileName 
        [DllImport("psapi.dll", EntryPoint = "GetDeviceDriverFileName")]
        public static extern uint GetDeviceDriverFileName([In] System.IntPtr ImageBase, [Out] [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder lpFilename, uint nSize);


        //GetMappedFileName 
        [DllImport("psapi.dll", EntryPoint = "GetMappedFileName")]
        public static extern uint GetMappedFileName([In] IntPtr hProcess, [In] IntPtr lpv, 
            [Out] [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder lpFilename, uint nSize);


        //GetModuleBaseName 
        [DllImport("psapi.dll", EntryPoint = "GetModuleBaseName")]
        public static extern uint GetModuleBaseName([In] IntPtr hProcess, [In] IntPtr hModule, 
            [Out] [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder lpBaseName, uint nSize);

   
        //GetModuleFileNameEx 
        [DllImport("psapi.dll", EntryPoint = "GetModuleFileNameEx")]
        public static extern uint GetModuleFileNameEx([In] IntPtr hProcess, [In] IntPtr hModule, 
            [Out] [MarshalAs(UnmanagedType.LPTStr)] System.Text.StringBuilder lpFilename, uint nSize);

 
        //GetModuleInformation 
        [DllImport("psapi.dll", EntryPoint = "GetModuleInformation")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetModuleInformation([In] IntPtr hProcess, [In] IntPtr hModule, [Out] out MODULEINFO lpmodinfo, uint cb);

     
        //GetPerformanceInfo 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPerformanceInfo(PERFORMANCE_INFORMATION pPerformanceInformation, int cb);

        //GetProcessImageFileName 
        [DllImport("psapi.dll")]
        public static extern int  GetProcessImageFileName(IntPtr hProcess, StringBuilder lpImageFileName, [In] int nSize);
  
        //GetProcessMemoryInfo 
        [DllImport("psapi.dll", EntryPoint = "GetProcessMemoryInfo")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetProcessMemoryInfo([In] IntPtr Process, [Out] out PROCESS_MEMORY_COUNTERS ppsmemCounters, uint cb);

        //GetWsChanges 
        [DllImport("psapi.dll", EntryPoint = "GetWsChanges")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWsChanges([In] IntPtr hProcess, 
            [Out] out PSAPI_WS_WATCH_INFORMATION lpWatchInfo, uint cb);


        //GetWsChangesEx 
        [DllImport("psapi.dll", EntryPoint = "GetWsChangesEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWsChangesEx([In] IntPtr hProcess, [Out] out PSAPI_WS_WATCH_INFORMATION_EX lpWatchInfoEx, uint cb);

        //InitializeProcessForWsWatch 
        [DllImport("psapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InitializeProcessForWsWatch(IntPtr hProcess);

        //QueryWorkingSet 
        [DllImport("psapi.dll", EntryPoint = "QueryWorkingSet")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryWorkingSet([In] IntPtr hProcess, IntPtr pv, uint cb);

        //QueryWorkingSetEx 
        [DllImport("psapi.dll", EntryPoint = "QueryWorkingSetEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool QueryWorkingSetEx([In] IntPtr hProcess, IntPtr pv, uint cb);

    }
}
