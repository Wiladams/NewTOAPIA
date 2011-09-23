using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.Kernel32
{

    public partial class AdvApi
    {

        [DllImport("advapi32", SetLastError = true)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle, SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, ref IntPtr DuplicateTokenHandle);


        //[DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx")]
        //[return: MarshalAsAttribute(UnmanagedType.Bool)]
        //public static extern bool DuplicateTokenEx([In] IntPtr hExistingToken, 
        //    int dwDesiredAccess, 
        //    [In] IntPtr lpTokenAttributes, 
        //    SECURITY_IMPERSONATION_LEVEL ImpersonationLevel, 
        //    TOKEN_TYPE TokenType, 
        //    out IntPtr phNewToken);


        [DllImport("advapi32.dll", EntryPoint = "DuplicateTokenEx")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public extern static bool DuplicateTokenEx([In] IntPtr TokenHandle, 
            int dwDesiredAccess,
            [In] ref SECURITY_ATTRIBUTES lpTokenAttributes, 
            SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
            TOKEN_TYPE TokenType,
            out IntPtr phNewToken);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool LookupPrivilegeValue(IntPtr lpSystemName, string lpname, [MarshalAs(UnmanagedType.Struct)] ref LUID lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool ConvertStringSidToSid(string StringSid, out IntPtr ptrSid);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, ref TOKEN_MANDATORY_LABEL TokenInformation, uint TokenInformationLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, uint TokenInformation, uint TokenInformationLength);

        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool CreateProcessAsUser(IntPtr TokenHandle, 
            String lpApplicationName, 
            String lpCommandLine, 
            ref SECURITY_ATTRIBUTES lpProcessAttributes,
            ref SECURITY_ATTRIBUTES lpThreadAttributes, 
            bool bInheritHandle, 
            int dwCreationFlags, 
            IntPtr lpEnvironment,
            String lpCurrentDirectory, 
            ref STARTUPINFO lpStartupInfo, 
            out PROCESS_INFORMATION lpProcessInformation);



        // Dealing with Services
        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenSCManager(string machineName, string databaseName, [MarshalAs(UnmanagedType.U4)]SCM_ACCESS dwAccess);


        [DllImport("advapi32.dll", EntryPoint = "OpenServiceW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr OpenService(SafeHandle hSCManager,
                                    [MarshalAs(UnmanagedType.LPWStr)] string lpServiceName,
                                    [MarshalAs(UnmanagedType.U4)]SERVICE_ACCESS dwDesiredAccess);


        [DllImport("advapi32.dll", EntryPoint = "QueryServiceStatusEx", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool QueryServiceStatusEx(SafeHandle hService,
                                                      SC_STATUS_TYPE InfoLevel,
                                                      ref SERVICE_STATUS_PROCESS dwServiceStatus,
                                                      int cbBufSize,
                                                      ref int pcbBytesNeeded);


        [DllImport("advapi32.dll", EntryPoint = "CloseServiceHandle", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool CloseServiceHandle(IntPtr hService);

    }
}
