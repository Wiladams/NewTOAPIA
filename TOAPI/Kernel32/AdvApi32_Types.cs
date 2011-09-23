using System;
using System.Runtime.InteropServices;

namespace TOAPI.Kernel32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SERVICE_STATUS_PROCESS
    {
        public uint dwServiceType;
        public uint dwCurrentState;
        public uint dwControlsAccepted;
        public uint dwWin32ExitCode;
        public uint dwServiceSpecificExitCode;
        public uint dwCheckPoint;
        public uint dwWaitHint;
        public uint dwProcessId;
        public uint dwServiceFlags;

        public static readonly int SizeOf = Marshal.SizeOf(typeof(SERVICE_STATUS_PROCESS));
    }

}
