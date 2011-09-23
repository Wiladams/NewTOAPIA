using System;
using System.Runtime.InteropServices;
//using System.Runtime.InteropServices.ComTypes;  // For FILETIME

namespace TOAPI.Kernel32
{
        #region Type aliases

    using HWND = System.IntPtr;
    using HINSTANCE = System.IntPtr;
    using HMENU = System.IntPtr;
    using HICON = System.IntPtr;
    using HBRUSH = System.IntPtr;
    using HCURSOR = System.IntPtr;

    using LRESULT = System.IntPtr;
    using LPVOID = System.IntPtr;
    using LPCTSTR = System.String;

    using WPARAM = System.IntPtr;
    using LPARAM = System.IntPtr;
    using HANDLE = System.IntPtr;
    using HRAWINPUT = System.IntPtr;

    using BYTE = System.Byte;
    using SHORT = System.Int16;
    using USHORT = System.UInt16;
    using LONG = System.Int32;
    using ULONG = System.UInt32;
    using WORD = System.Int16;
    using DWORD = System.Int32;
    using BOOL = System.Boolean;
    using INT = System.Int32;
    using UINT = System.UInt32;
    using LONG_PTR = System.IntPtr;
    using ATOM = System.Int32;

    using COLORREF = System.Int32;
    using WNDPROC = System.IntPtr;

    using HRESULT = System.IntPtr;

    #endregion

    public partial class Kernel32
    {
        // Generic across many sub-systems
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle(IntPtr hObject);


        // NUMA Support
        // GetNumaAvailableMemoryNode
        // GetNumaHighestNodeNumber
        // GetNumaNodeProcessorMask
        // GetNumaProcessorNode



        // Job Objects
        // AssignProcessToJobObject
        // CreateJobObject
        // IsProcessInJob
        // OpenJobObject
        // QueryInformaitonJobObject
        // SetInformationJobObject
        // TerminateJobObject
        // UserHandleGrantAccess

        [DllImport("kernel32.dll", EntryPoint = "FormatMessageW")]
        public static extern uint FormatMessageW(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, 
            [MarshalAs(UnmanagedType.LPWStr)] System.Text.StringBuilder lpBuffer, int nSize, ref IntPtr Arguments);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Int32 FormatMDWORDessage(Int32 dwFlags, ref Int64 lpSource, Int32 dwMessageId, Int32 dwLanguageZId, String lpBuffer, Int32 nSize, Int32 Arguments);        


        [DllImport("kernel32.dll")]
        public static extern  uint GetLastError();

        [DllImport("kernel32.dll")]
        public static extern void SetLastError( DWORD dwErrCode);

        [DllImport("kernel32.dll")]
        public static extern uint WTSGetActiveConsoleSessionId();


    }
}
