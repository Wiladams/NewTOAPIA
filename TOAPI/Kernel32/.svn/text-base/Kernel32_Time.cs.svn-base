using System.Runtime.InteropServices;
using System.Security;

namespace TOAPI.Kernel32
{
    partial class Kernel32
    {
        // Time Functions
        // Using SYSTEMTIME
        // GetSystemTime
        // GetSystemTimeAdjustment
        // GetTimeFormat
        // NtQuerySystemTime
        // RtlLocalTimeToSystemTime
        // RtlTimeToSecondsSince1970
        // SetSystemTime
        // SetSystemTimeAdjustment
        // SystemTimeToFileTime
        // SystemTimeToTzSpecificLocalTime
        // TzSpecificLocalTimeToSystemTime

        // Dealing with  local time
        // FileTimeToLocalFileTime
        // GetLocalTime
        [DllImport("kernel32.dll")]
        public static extern void GetLocalTime(out SYSTEMTIME lpSystemTime);

        // GetTimeZoneInformation
        // RtlLocalTimeToSystemTime
        // SetLocalTime
        // SetTimeZoneInformation
        // SystemTimeToTzSpecificLocalTime
        // TzSpecificLocalTimeToSystemTime

        // Dealing with file time
        // CompareFileTime
        // FileTimeToLocalFileTime
        // FileTimeToSystemTime
        // GetFileTime
        // GetSystemTimeAsFileTime
        // LocalFileTimeToFileTime
        // SetFileTime
        // SystemTimeToFileTime

        // MS-DOS Date and time
        // DosDateTimeToFileTime
        // FileTimeToDosDateTime
        
        
        // GetSystemTimes - CHECK
        [DllImport("kernel32.dll", EntryPoint = "GetSystemTimes")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetSystemTimes(
            [Out] out System.Runtime.InteropServices.ComTypes.FILETIME lpIdleTime,
           [Out] out System.Runtime.InteropServices.ComTypes.FILETIME lpKernelTime,
           [Out] out System.Runtime.InteropServices.ComTypes.FILETIME lpUserTime);

        // GetTickCount
        [DllImport("kernel32.dll")]
        public static extern uint GetTickCount();

        // QueryPerformanceFrequency
        /// <summary>
        /// Find out the frequency of the high performance timer, if the system is capable
        /// of using the high performance timer.
        /// 
        /// 
        /// </summary>
        /// <comments>
        /// The return type could be 'bool', but since the native call returns 'BOOL', 
        /// which is a 32-bit entity, the marshaler would have to convert to the 
        /// managed 'bool', which incurs a slight performance hit.
        /// Additionally, the 'out' parameter could be passed as a 'ref', but again,
        /// a slight performance hit as it is truly only an 'out' parameter, so we
        /// save one copy by explicitly naming it as such.
        /// </comments>
        /// <param name="lpFrequency"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int QueryPerformanceFrequency(out long lpFrequency);

        // QueryPerformanceCounter
        [DllImport("kernel32.dll"), SuppressUnmanagedCodeSecurity]
        public static extern int QueryPerformanceCounter(out long lpPerformanceCount);

    }
}
