

namespace TOAPI.Winsock
{
    using System;
    using System.Runtime.InteropServices;
    
    public class WinInet
    {
        [DllImport("WININET.DLL", EntryPoint = "InternetOpen", SetLastError = true)]
        public static extern IntPtr InternetOpen(string sAgent
            , uint lAccessType
            , string sProxyName
            , string sProxyBypass
            , uint lFlags);

        [DllImport("WININET.DLL", EntryPoint = "InternetOpenUrl", SetLastError = true)]
        public static extern IntPtr InternetOpenUrl(IntPtr hIneternetSession
            , string sUrl
            , string sHeaders
            , uint lHeadrsLength
            , uint lFlags
            , uint lContext);

        [DllImport("WININET.DLL", EntryPoint = "InternetReadFile", SetLastError = true)]
        public static extern bool InternetReadFile(IntPtr hFile
            , byte[] pBuffer
            , int nBytesToRead
            , ref uint nBytesRead);

        [DllImport("WININET.DLL", EntryPoint = "InternetCloseHandle", SetLastError = true)]
        public static extern int InternetCloseHandle(IntPtr hHandle);

    }
}
