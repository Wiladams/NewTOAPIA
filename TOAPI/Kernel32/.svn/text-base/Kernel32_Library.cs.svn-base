using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.Kernel32
{
    partial class Kernel32
    {
        // Dynamic linking
        //
        // DisableThreadLibraryCalls
        // DllMain
        // FreeLibrary
        // FreeLibraryAndExitThread
        // GetDllDirectory
        // GetModuleFileName
        // GetModuleFileNameEx
        // GetModuleHandle
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle([In] string moduleName);

        // GetModuleHandleEx
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetModuleHandleEx(uint dwFlags, [In] string modName, ref IntPtr phModule);

        // GetProcAddress
        [DllImport("kernel32", EntryPoint = "GetProcAddress", ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern FARPROC GetProcAddress(IntPtr hModule,
            [In] [MarshalAs(UnmanagedType.LPStr)]string lpProcName);

        // LoadLibrary
        // LoadLibraryEx
        // SetDllDirectory
    }
}