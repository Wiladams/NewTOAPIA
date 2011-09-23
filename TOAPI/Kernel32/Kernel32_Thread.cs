using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles; 

namespace TOAPI.Kernel32
{
    public delegate uint THREAD_START_ROUTINE(IntPtr lpThreadParameter);

    public partial class Kernel32
    {
        // AttachThreadInput
        // CreateRemoteThread
        
        // CreateThread
        [DllImport("kernel32.dll", EntryPoint = "CreateThread")]
        public static extern IntPtr CreateThread(IntPtr lpThreadAttributes, 
            uint dwStackSize, THREAD_START_ROUTINE lpStartAddress, 
            IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // ExitThread
        [DllImport("kernel32.dll", EntryPoint = "ExitThread")]
        public static extern void ExitThread(uint dwExitCode);

        // GetCurrentThread
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetCurrentThread();

        // GetCurrentThreadId
        [DllImport("kernel32.dll")]
        public static extern uint GetCurrentThreadId();

        // GetExitCodeThread
        // GetThreadId
        [DllImport("kernel32.dll", EntryPoint = "GetThreadId")]
        public static extern uint GetThreadId(IntPtr Thread);

        // GetThreadIOPendingFlag
        // GetThreadPriority
        // GetThreadPriorityBoost
        
        // GetThreadTimes

        // OpenThread
        [DllImport("kernel32.dll", EntryPoint = "OpenThread")]
        public static extern IntPtr OpenThread(uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, 
            uint dwThreadId);

        // ResumeThread
        [DllImport("kernel32.dll")]
        public static extern int ResumeThread(IntPtr hThread);

        // SetThreadAffinityMask
        // SetThreadIdealProcessor
        // SetThreadPriority
        // SetThreadPriorityBoost
        // SetThreadStackGuarantee

        // Sleep
        [DllImport("kernel32.dll", EntryPoint = "Sleep")]
        public static extern void Sleep(uint dwMilliseconds);

        // SleepEx
        [DllImport("kernel32.dll", EntryPoint = "SleepEx")]
        public static extern uint SleepEx(uint dwMilliseconds, [MarshalAs(UnmanagedType.Bool)] bool bAlertable);

        // SuspendThread
        [DllImport("kernel32.dll")]
        public static extern int SuspendThread(IntPtr hThread);

        // SwithToThread
        // TerminateThread
        // ThreadProc
        // TlsAlloc
        // TlsFree
        // TlsGetValue
        // TlsSetValue
        // WaitForInputIdle

        // Thread Pooling
        // BindIoCompletionCallback
        // QueueUserWorkItem


        [DllImport("kernel32.dll")]
        public static extern uint GetThreadLocale();

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public extern static bool SetThreadLocale(uint Locale);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 WaitForSingleObject(SafeWaitHandle hHandle, Int32 dwMilliseconds);        

    }
}
