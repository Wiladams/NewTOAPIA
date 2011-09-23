using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.Kernel32
{
    // FiberProc
    //public delegate void FiberProc(IntPtr parameter);
    public delegate void PFLS_CALLBACK_FUNCTION(IntPtr lpFlsData);

    partial class Kernel32
    {
        // ConvertFiberToThread
        [DllImport("kernel32.dll", EntryPoint = "ConvertFiberToThread")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConvertFiberToThread();
        
        // ConvertThreadToFiber
        [DllImport("kernel32.dll", EntryPoint = "ConvertThreadToFiber")]
        public static extern IntPtr ConvertThreadToFiber(IntPtr lpParameter);
        
        // ConvertThreadToFiberEx
        [DllImport("kernel32.dll", EntryPoint = "ConvertThreadToFiberEx")]
        public static extern IntPtr ConvertThreadToFiberEx(IntPtr lpParameter, uint dwFlags);

        // CreateFiber
        [DllImport("kernel32.dll", EntryPoint = "CreateFiber")]
        public static extern IntPtr CreateFiber(uint dwStackSize, PFIBER_START_ROUTINE lpStartAddress, IntPtr lpParameter);

        // CreateFiberEx
        [DllImport("kernel32.dll", EntryPoint = "CreateFiberEx")]
        public static extern IntPtr CreateFiberEx(uint dwStackCommitSize, uint dwStackReserveSize, 
            uint dwFlags, PFIBER_START_ROUTINE lpStartAddress, IntPtr lpParameter);
        
        // DeleteFiber
        [DllImport("kernel32.dll", EntryPoint = "DeleteFiber")]
        public static extern void DeleteFiber(IntPtr lpFiber);

        // FlsAlloc
        [DllImport("kernel32.dll", EntryPoint = "FlsAlloc")]
        public static extern uint FlsAlloc(PFLS_CALLBACK_FUNCTION lpCallback);

        // FlsFree
        [DllImport("kernel32.dll", EntryPoint = "FlsFree")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlsFree(uint dwFlsIndex);

        // FlsGetValue
        [DllImport("kernel32.dll", EntryPoint = "FlsGetValue")]
        public static extern IntPtr FlsGetValue(uint dwFlsIndex);

        // FlsSetValue
        [DllImport("kernel32.dll", EntryPoint = "FlsSetValue")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlsSetValue(uint dwFlsIndex, System.IntPtr lpFlsData);

        // SwitchToFiber
        [DllImport("kernel32.dll", EntryPoint = "SwitchToFiber")]
        public static extern void SwitchToFiber(IntPtr lpFiber);
    }
}
