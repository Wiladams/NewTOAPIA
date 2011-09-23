using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Kernel32
{
    partial class Kernel32
    {
        // IsWow64Message - User32

        // IsWow64Process
        [DllImport("kernel32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process(IntPtr hProcess, 
            [MarshalAs(UnmanagedType.Bool)]ref bool Wow64Process);
        
        // GetNativeSystemInfo
        [DllImport("kernel32.dll", EntryPoint = "GetNativeSystemInfo")]
        public static extern void GetNativeSystemInfo([Out] out SYSTEM_INFO lpSystemInfo);

        // GetSystemWow64Directory
        [DllImport("kernel32.dll", EntryPoint = "GetSystemWow64DirectoryW")]
        public static extern uint GetSystemWow64DirectoryW([MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpBuffer, uint uSize);

        //[System.Diagnostics.DebuggerStepThroughAttribute()]
        //[System.CodeDom.Compiler.GeneratedCodeAttribute("InteropSignatureToolkit", "0.9 Beta1")]
        //public static uint GetSystemWow64DirectoryW(out string lpBuffer)
        //{
        //    System.Text.StringBuilder varlpBuffer;
        //    uint retVar_;
        //    uint sizeVar = 2056;
        //PerformCall:
        //    varlpBuffer = new System.Text.StringBuilder(((int)(sizeVar)));
        //retVar_ = NativeMethods.GetSystemWow64DirectoryW(varlpBuffer, sizeVar);
        //if ((retVar_ >= sizeVar))
        //{
        //    sizeVar = (retVar_ + ((uint)(1)));
        //    goto PerformCall;
        //}
        //lpBuffer = varlpBuffer.ToString();
        //return retVar_;
        //}

        // Wow64DisableWow64FsRedirection
        /// <summary>
        /// Disables file system redirection for the calling thread.
        /// </summary>
        /// <param name="OldValue"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "Wow64DisableWow64FsRedirection")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr OldValue);

        // Wow64EnableWow64FsRedirection
        /// <summary>
        /// Enables or disables file system redirection for the calling thread.
        /// </summary>
        /// <param name="Wow64FsEnableRedirection"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "Wow64EnableWow64FsRedirection")]
        public static extern byte Wow64EnableWow64FsRedirection(byte Wow64FsEnableRedirection);

        // Wow64RevertWow64FsRedirection
        /// <summary>
        /// Restores file system redirection for the calling thread.
        /// </summary>
        /// <param name="OlValue"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", EntryPoint = "Wow64RevertWow64FsRedirection")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr OlValue);

    }
}
