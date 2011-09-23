using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Kernel32
{
    public partial class Kernel32
    {
        // Managing Processes
        // Create Process
        //[DllImport("kernel32.dll")]
        //static extern bool CreateProcess(string lpApplicationName,
        //   string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes,
        //   ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles,
        //   uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory,
        //   [In] ref STARTUPINFO lpStartupInfo,
        //   out PROCESS_INFORMATION lpProcessInformation);
        [DllImport("kernel32.dll", CharSet=CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreateProcess([In] string lpApplicationName, 
            IntPtr lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, 
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandles,
            ProcessCreationFlags dwCreationFlags, IntPtr lpEnvironment, 
            string lpCurrentDirectory, 
            ref STARTUPINFO lpStartupInfo, 
            [Out] out PROCESS_INFORMATION lpProcessInformation);

        // CreateProcessAsUser
        // CreateProcessWithLogonW
        // CreateProcessWithTokenW

        // OpenProcess
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        // ExitProcess
        [DllImport("kernel32.dll")]
        public static extern void ExitProcess(int uExitCode);


        // GetCurrentProcess
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentProcess();

        // GetCurrentProcessId
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentProcessId();

        // GetCurrentProcessorNumber

        // GetCommnandLine
        /// <summary>
        /// From MSDN's Buffers Sample
        /// The original GetCommandLine function returns a pointer to a buffer allocated 
        /// and owned by the operating system. When marshaling strings as return types, 
        /// the interop marshaler assumes it must free the memory that the original LPTSTR type 
        /// pointed to by the function. To prevent the marshaler from automatically reclaiming 
        /// this memory, the managed GetCommandLine prototype returns an IntPtr type instead of 
        /// a string. The Marshal.PtrToStringAuto method copies the unmanaged LPSTR type to a 
        /// managed string object, widening the character format, if required.
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint="GetCommandLine", CharSet = CharSet.Auto)]
        private static extern IntPtr IntGetCommandLine();

        public static string GetCommandLine()
        {
            IntPtr ptr = IntGetCommandLine();
            string commandLine = Marshal.PtrToStringAuto(ptr);
            return commandLine;
        }

        // GetEnvironementStrings
        [DllImport("kernel32.dll", SetLastError=true)]
        static extern IntPtr GetEnvironmentStrings();

        // FreeEnvironmentStrings
        [DllImport("kernel32.dll", SetLastError= true)]
        static extern bool FreeEnvironmentStrings(string lpszEnvironmentBlock);

        // GetEnvironmentVariable
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern int GetEnvironmentVariable(string lpName, [Out] StringBuilder lpBuffer, int nSize);

        // ExpandEnvironmentStrings
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int ExpandEnvironmentStrings([MarshalAs(UnmanagedType.LPTStr)] String source, [Out] StringBuilder destination, int size);

        // GetExitCodeProcess
        // GetGuiResources
        // GetLogicalProcessorInformation
        [DllImport("kernel32.dll", EntryPoint = "GetLogicalProcessorInformation")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetLogicalProcessorInformation(System.IntPtr Buffer, ref uint ReturnedLength);

    
        //[System.Diagnostics.DebuggerStepThroughAttribute]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("InteropSignatureToolkit", "0.9 Beta1")]
        //public static bool GetLogicalProcessorInformation(out PInvokePointer Buffer) 
        //{
        //    PInvokePointer varBuffer;
        //    bool retVar_ = false;
        //    uint sizeVar = 2056;
        //    uint oldSizeVar_;
        //PerformCall:
        //    oldSizeVar_ = sizeVar;
        //    varBuffer = new PInvokePointer(((int)(sizeVar)));
        //    GetLogicalProcessorInformation(varBuffer.IntPtr, ref sizeVar);
        //    if ((sizeVar <= oldSizeVar_)) {
        //        varBuffer.Free();
        //        sizeVar = (sizeVar * (2));
        //        goto PerformCall;
        //    }
        //    Buffer = varBuffer;
        //    return retVar_;
        //}




        // GetPriorityClass
        // GetProcessAffinityMask

        // GetProcessHandleCount
        [DllImport("kernel32.dll", EntryPoint = "GetProcessHandleCount")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetProcessHandleCount(IntPtr hProcess, [Out] out int pdwHandleCount);

        // GetProcessId
        [DllImport("kernel32.dll", EntryPoint = "GetProcessId")]
        public static extern uint GetProcessId(IntPtr Process);

        // GetProcessIdOfThread
        // GetProcessIoCounters
        // GetProcessPriorityBoost
        // GetProcessShutdownParameters
        // GetProcessTimes
        // GetProcessVersion

        // GetProcessWorkingSetSize
        [DllImport("kernel32.dll", EntryPoint = "GetProcessWorkingSetSize")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetProcessWorkingSetSize(IntPtr hProcess, ref uint lpMinimumWorkingSetSize, ref uint lpMaximumWorkingSetSize);

        // GetProcessWorkkingSetSizeEx
        // GetStartupInfo
        // NeedCurrentDirectoryForExePath

        // SetEnvironmentVariable
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetEnvironmentVariable(string lpName, string lpValue);

        // SetPriorityClass
        // SetProcessAffinityMask
        // SetProcessPriorityBoost
        // SetProcessShutdownParameters
        // SetProcessWorkingSetSize
        // SetProcessWorkingSetSizeEx

        // TerminateProcess
        [DllImport("kernel32.dll", EntryPoint = "TerminateProcess")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool TerminateProcess(IntPtr hProcess, uint uExitCode);

        // Enumerate Processes
        // CreateToolhelp32Snapshot
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, int th32ProcessID);

        // Process32First
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        // Process32Next
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("kernel32.dll", EntryPoint = "ProcessIdToSessionId")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ProcessIdToSessionId(uint dwProcessId, [Out] out uint pSessionId);

        // WTSEnumerateProcesses    
    }
}