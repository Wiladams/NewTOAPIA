using System;
using System.Runtime.InteropServices;

namespace TOAPI.WtsApi32
{
    public partial class Wts
    {
        public delegate ChannelReturnCodes VirtualChannelInitDelegate(ref IntPtr ppInitHandle,
            ref ChannelDef pChannel, int channelCount, int versionRequested,
            [MarshalAs(UnmanagedType.FunctionPtr)] 
            ChannelInitEventDelegate ChannelInitEventProc);

        public delegate ChannelReturnCodes VirtualChannelOpenDelegate(IntPtr pInitHandle,
            ref int pOpenHandle,
            [MarshalAs(UnmanagedType.LPStr)] string ChannelName,
            [MarshalAs(UnmanagedType.FunctionPtr)] 
            ChannelOpenEventDelegate ChannelOpenEventProc);

        public delegate ChannelReturnCodes VirtualChannelCloseDelegate(int OpenHandle);

        public delegate ChannelReturnCodes VirtualChannelWriteDelegate(int OpenHandle,
            IntPtr pData, uint dataLength, IntPtr pUserData);

        public delegate void ChannelInitEventDelegate(IntPtr pInitHandle,
               ChannelEvents Event, IntPtr pData, int dataLength);

        public delegate void ChannelOpenEventDelegate(int openHandle,
               ChannelEvents Event, IntPtr pData, int dataLength,
               uint totalLength, ChannelFlags dataFlags);


         //  1    0 00001F28 WTSCloseServer
        [DllImport("wtsapi32.dll")]
        public static extern void WTSCloseServer(IntPtr hServer);

         // 2    1 000025B3 WTSDisconnectSession
        [DllImport("wtsapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

         // 3    2 0000326C WTSEnumerateProcessesA
         // 4    3 00002F79 WTSEnumerateProcessesW
        [DllImport("wtsapi32.dll", SetLastError=true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSEnumerateProcesses(
            IntPtr serverHandle, // Handle to a terminal server. 
            int  reserved,     // must be 0
            int  version,      // must be 1
            ref IntPtr ppProcessInfo, // pointer to array of WTS_PROCESS_INFO
            ref int  pCount);       // pointer to number of processes

        
        // 5    4 000021FD WTSEnumerateServersA
         // 6    5 000020E7 WTSEnumerateServersW
        // TODO - This one is suspect
        [DllImport("wtsapi32.dll", CharSet=CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSEnumerateServers(string pDomainName, int Reserved, int Version, ref WTS_SERVER_INFO[] ppServerInfo, ref int pCount);

         // 7    6 000028C2 WTSEnumerateSessionsA
         // 8    7 0000239F WTSEnumerateSessionsW
        [DllImport("wtsapi32.dll")]
        public static extern Int32 WTSEnumerateSessions(
            IntPtr hServer,
            int Reserved,
            int Version,
            ref IntPtr ppSessionInfo,
            ref int pCount);


         // 9    8 00001D2D WTSFreeMemory
        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = false)]
        public static extern void WTSFreeMemory(IntPtr memory);

         //10    9 000025D2 WTSLogoffSession
        [DllImport("wtsapi32.dll", ExactSpelling = true, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSLogoffSession(IntPtr hServer, int SessionId, bool bWait);

         //11    A 00001F18 WTSOpenServerA
         //12    B 00001F08 WTSOpenServerW
        [DllImport("wtsapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr WTSOpenServer(string pServerName);

         //13    C 00002E67 WTSQuerySessionInformationA
         //14    D 00002A66 WTSQuerySessionInformationW
        [DllImport("wtsapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSQuerySessionInformation(IntPtr hServer, 
            int sessionId, WTSInfoClass wtsInfoClass, 
            out IntPtr ppBuffer, out uint pBytesReturned);

         //15    E 00003BB6 WTSQueryUserConfigA
         //16    F 000038C5 WTSQueryUserConfigW
        [DllImport("wtsapi32.dll", CharSet=CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSQueryUserConfig(string pServerName, string pUserName, WTS_CONFIG_CLASS configClass, Char[] ppBuffer, ref int pBytesReturned);

        //17   10 00001E99 WTSQueryUserToken
        [DllImport("wtsapi32.dll", EntryPoint = "WTSQueryUserToken")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSQueryUserToken(uint SessionId, ref IntPtr phToken);

         //18   11 00002710 WTSRegisterSessionNotification
        [DllImport("wtsapi32.dll", EntryPoint = "WTSRegisterSessionNotification")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSRegisterSessionNotification(IntPtr hWnd, uint dwFlags);

         //19   12 000027C1 WTSRegisterSessionNotificationEx
        [DllImport("wtsapi32.dll", EntryPoint = "WTSRegisterSessionNotificationEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSRegisterSessionNotificationEx(IntPtr hServer, IntPtr hWnd, uint dwFlags);

         //20   13 0000255E WTSSendMessageA
         //21   14 00002509 WTSSendMessageW
        [DllImport("wtsapi32.dll", CharSet=CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSSendMessage(IntPtr hServer,
                    int SessionId,
                    String pTitle, int TitleLength,
                    String pMessage, int MessageLength,
                    int Style,
                    int Timeout,
                    out int pResponse,
                    bool bWait);

        // These two don't seem to be documented in the Platform SDK.
        //22   15 000024FE WTSSetSessionInformationA
        //23   16 000024FE WTSSetSessionInformationW

        //24   17 00004019 WTSSetUserConfigA
        //25   18 00003CC8 WTSSetUserConfigW
        [DllImport("wtsapi32.dll", CharSet=CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSSetUserConfig(string pServerName, string pUserName, WTS_CONFIG_CLASS WTSConfigClass, Char[] pBuffer, int DataLength);

        //26   19 00001E32 WTSShutdownSystem
        [DllImport("wtsapi32.dll", SetLastError = true)]
        public static extern int WTSShutdownSystem(IntPtr ServerHandle, long ShutdownFlags);

        //27   1A 0000324D WTSTerminateProcess
        [DllImport("wtsapi32.dll", EntryPoint = "WTSTerminateProcess")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSTerminateProcess(IntPtr hServer, uint ProcessId, uint ExitCode);

        //28   1B 00002770 WTSUnRegisterSessionNotification
        [DllImport("wtsapi32.dll", EntryPoint = "WTSUnRegisterSessionNotification")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);

        //29   1C 00002822 WTSUnRegisterSessionNotificationEx
        [DllImport("wtsapi32.dll", EntryPoint = "WTSUnRegisterSessionNotificationEx")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSUnRegisterSessionNotificationEx(IntPtr hServer, IntPtr hWnd);

        //30   1D 000034A7 WTSVirtualChannelClose
        [DllImport("wtsapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSVirtualChannelClose(IntPtr hChannelHandle);
        
        //31   1E 0000343D WTSVirtualChannelOpen
        [DllImport("wtsapi32.dll")]
        public static extern IntPtr WTSVirtualChannelOpen(IntPtr hServer,
            int SessionId, [MarshalAs(UnmanagedType.LPStr)] string VirtualName);
        
        //32   1F 000036B5 WTSVirtualChannelPurgeInput
        [DllImport("wtsapi32.dll", EntryPoint = "WTSVirtualChannelPurgeInput")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSVirtualChannelPurgeInput(IntPtr hChannelHandle);

        //33   20 000036D7 WTSVirtualChannelPurgeOutput
        [DllImport("wtsapi32.dll", EntryPoint="WTSVirtualChannelPurgeOutput")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern  bool WTSVirtualChannelPurgeOutput(IntPtr hChannelHandle) ;

        
        //34   21 000036F9 WTSVirtualChannelQuery
        [DllImport("wtsapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSVirtualChannelQuery(IntPtr hChannelHandle, WTS_VIRTUAL_CLASS virtualClass, out IntPtr ppBuffer, out int pBytesReturned);

        //35   22 00003573 WTSVirtualChannelRead
        [DllImport("wtsapi32.dll", EntryPoint = "WTSVirtualChannelRead")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSVirtualChannelRead(IntPtr hChannelHandle, uint TimeOut, IntPtr Buffer, uint BufferSize, [Out()] out uint pBytesRead);

         //36   23 000034FC WTSVirtualChannelWrite
        [DllImport("wtsapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WTSVirtualChannelWrite(IntPtr hChannelHandle,
               byte[] Buffer, int Length, ref int pBytesWritten);
        
        //37   24 00001C45 WTSWaitSystemEvent
        [DllImport("wtsapi32.dll", EntryPoint="WTSWaitSystemEvent")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern  bool WTSWaitSystemEvent(IntPtr hServer, uint EventMask, ref uint pEventFlags) ;

     }
}
