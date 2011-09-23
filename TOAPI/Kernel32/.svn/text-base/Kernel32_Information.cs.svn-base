using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Kernel32
{
    partial class Kernel32
    {
        // DnsHostnameToComputerName

        // EnumSystemFirmawareTables

        // GetComputerName
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetComputerName(StringBuilder lpBuffer, ref int size);

        // GetComputerNameEx
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetComputerNameEx(int NameType, StringBuilder lpBuffer, ref int lpnSize);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetComputerNameEx(int NameType, IntPtr lpBuffer, ref int lpnSize);

        // GetSystemPowerStatus
        [DllImport("kernel32.dll", EntryPoint = "GetSystemPowerStatus")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetSystemPowerStatus([Out] out SYSTEM_POWER_STATUS lpSystemPowerStatus);

        // GetComputerObjectName
        // GetCurrentHwProfile
        // GetFirmwareEnvironmentVariable

        // GetSystemDirectory
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetSystemDirectory(StringBuilder lpBuffer, int uSize);

        // GetSystemFirmwareTable
        // GetSystemFileCacheSize
        // GetSystemInfo
        // GetSystemRegistryQuota

        // GetSystemWindowsDirectory
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetWindowsDirectory([Out] StringBuilder builder, uint uSize);


        // GetUserNameEx
        // GetVersion
        // GetVersionEx
        // GetWindowsDirectory

        // IsProcessorFeaturePresent
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsProcessorFeaturePresent(int aProcessorFeature);


        // NtQuerySystemInformation
        // SetComputerName

        // SetComputerNameEx
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetComputerNameEx(int NameType, string lpBuffer);

        // SetFirmwareEnvironmentVariable
        // SetSystemFileCacheSize
        // SystemParametersInfo
        // TranslateName
        // VerifyVersionInfo
        // VerSetConditionMask
    }
}
