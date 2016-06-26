using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Winsock
{

    public partial class Winsock
    {
        //  1   84 0000A9C9 accept
        [DllImport("ws2_32.dll", EntryPoint = "accept", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        public static extern uint accept([MarshalAs(UnmanagedType.SysUInt)] uint s, ref sockaddr addr, ref int addrlen);

        //  2   85 00003F41 bind
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///addr: sockaddr*
        ///namelen: int
        [DllImport("ws2_32.dll", EntryPoint = "bind", CallingConvention = CallingConvention.StdCall)]
        public static extern int bind([MarshalAs(UnmanagedType.SysUInt)] uint s, ref sockaddr addr, int namelen);

        //  3   86 00003847 closesocket
        [DllImport("ws2_32.dll", EntryPoint = "closesocket", CallingConvention = CallingConvention.StdCall)]
        public static extern int closesocket([MarshalAs(UnmanagedType.SysUInt)] uint s);

        //  4   87 00004BA7 connect
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///name: sockaddr*
        ///namelen: int
        [DllImport("ws2_32.dll", EntryPoint = "connect", CallingConvention = CallingConvention.StdCall)]
        public static extern int connect([MarshalAs(UnmanagedType.SysUInt)] uint s, ref sockaddr name, int namelen);


        // 51   8A 00015D3D gethostbyaddr
        /// Return Type: hostent*
        ///addr: char*
        ///len: int
        ///type: int
        [DllImport("ws2_32.dll", EntryPoint = "gethostbyaddr", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr gethostbyaddr([In] [MarshalAs(UnmanagedType.LPStr)] string addr, int len, int type);

        // 52   8B 0000DB26 gethostbyname
        /// Return Type: hostent*
        ///name: char*
        [DllImport("ws2_32.dll", EntryPoint = "gethostbyname", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr gethostbyname([In] [MarshalAs(UnmanagedType.LPStr)] string name);

        // 57   8C 0000B3E3 gethostname
        /// Return Type: int
        ///name: char*
        ///namelen: int
        [DllImport("ws2_32.dll", EntryPoint = "gethostname", CallingConvention = CallingConvention.StdCall)]
        public static extern int gethostname(StringBuilder name, int namelen);

        //  5   8E 0001A7DB getpeername
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///name: sockaddr*
        ///namelen: int*
        [DllImport("ws2_32.dll", EntryPoint = "getpeername", CallingConvention = CallingConvention.StdCall)]
        public static extern int getpeername(IntPtr s, ref sockaddr name, ref int namelen);

        // 53   8F 00015B59 getprotobyname
        /// Return Type: protoent*
        ///name: char*
        [DllImport("ws2_32.dll", EntryPoint = "getprotobyname", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr getprotobyname([In] [MarshalAs(UnmanagedType.LPStr)] string name);

        // 54   90 00015A9C getprotobynumber
        /// Return Type: protoent*
        ///proto: int
        [DllImport("ws2_32.dll", EntryPoint = "getprotobynumber", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr getprotobynumber(int proto);

        // 55   91 0000C10A getservbyname
        /// Return Type: servent*
        ///name: char*
        ///proto: char*
        [DllImport("ws2_32.dll", EntryPoint = "getservbyname", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr getservbyname([In] [MarshalAs(UnmanagedType.LPStr)] string name, [In] [MarshalAs(UnmanagedType.LPStr)] string proto);

        // 56   92 00015E6F getservbyport
        /// Return Type: servent*
        ///port: int
        ///proto: char*
        [DllImport("ws2_32.dll", EntryPoint = "getservbyport", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr getservbyport(int port, [In] [MarshalAs(UnmanagedType.LPStr)] string proto);

        //  6   93 00003D69 getsockname
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///name: sockaddr*
        ///namelen: int*
        [DllImport("ws2_32.dll", EntryPoint = "getsockname", CallingConvention = CallingConvention.StdCall)]
        public static extern int getsockname([MarshalAs(UnmanagedType.SysUInt)] uint s, ref sockaddr name, ref int namelen);

        //  7   94 0000A380 getsockopt
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///level: int
        ///optname: int
        ///optval: char*
        ///optlen: int*
        [DllImport("ws2_32.dll", EntryPoint = "getsockopt", CallingConvention = CallingConvention.StdCall)]
        public static extern int getsockopt([MarshalAs(UnmanagedType.SysUInt)] uint s, int level, int optname, IntPtr optval, ref int optlen);

        //  8   95 00002D30 htonl
        /// Return Type: u_long->unsigned int
        ///hostlong: u_long->unsigned int
        [DllImport("ws2_32.dll", EntryPoint = "htonl", CallingConvention = CallingConvention.StdCall)]
        public static extern uint htonl(uint hostlong);

        //  9   96 00002DD7 htons
        /// Return Type: u_short->unsigned short
        ///hostshort: u_short->unsigned short
        [DllImport("ws2_32.dll", EntryPoint = "htons", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort htons(ushort hostshort);

        // 11   97 00003BF2 inet_addr
        /// Return Type: unsigned int
        ///cp: char*
        [DllImport("ws2_32.dll", EntryPoint = "inet_addr", CallingConvention = CallingConvention.StdCall)]
        public static extern uint inet_addr([In] [MarshalAs(UnmanagedType.LPStr)] string cp);

        // 12   98 000067E1 inet_ntoa
        /// Return Type: char*
        ///in: in_addr
        [DllImport("ws2_32.dll", EntryPoint = "inet_ntoa", CallingConvention = CallingConvention.StdCall)]
        public static extern System.IntPtr inet_ntoa(in_addr @in);


        // 10   9B 00003D06 ioctlsocket
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///cmd: int
        ///argp: u_long*
        [DllImport("ws2_32.dll", EntryPoint = "ioctlsocket", CallingConvention = CallingConvention.StdCall)]
        public static extern int ioctlsocket([MarshalAs(UnmanagedType.SysUInt)] uint s, int cmd, ref uint argp);

        // 13   9C 0000A85A listen
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///backlog: int
        [DllImport("ws2_32.dll", EntryPoint = "listen", CallingConvention = CallingConvention.StdCall)]
        public static extern int listen([MarshalAs(UnmanagedType.SysUInt)] uint s, int backlog);

        // 14   9D 00002D30 ntohl
        /// Return Type: u_long->unsigned int
        ///netlong: u_long->unsigned int
        [DllImport("ws2_32.dll", EntryPoint = "ntohl", CallingConvention = CallingConvention.StdCall)]
        public static extern uint ntohl(uint netlong);

        // 15   9E 00002DD7 ntohs
        /// Return Type: u_short->unsigned short
        ///netshort: u_short->unsigned short
        [DllImport("ws2_32.dll", EntryPoint = "ntohs", CallingConvention = CallingConvention.StdCall)]
        public static extern ushort ntohs(ushort netshort);

        // 16   9F 00004ABD recv
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///buf: char*
        ///len: int
        ///flags: int
        [DllImport("ws2_32.dll", EntryPoint = "recv", CallingConvention = CallingConvention.StdCall)]
        public static extern int recv([MarshalAs(UnmanagedType.SysUInt)] uint s, IntPtr buf, int len, int flags);

        // 17   A0 0000BEB2 recvfrom
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///buf: char*
        ///len: int
        ///flags: int
        ///from: sockaddr*
        ///fromlen: int*
        [DllImport("ws2_32.dll", EntryPoint = "recvfrom", CallingConvention = CallingConvention.StdCall)]
        public static extern int recvfrom([MarshalAs(UnmanagedType.SysUInt)] uint s, IntPtr buf, int len, int flags, ref sockaddr from, ref int fromlen);

        // 18   A1 0000368C select
        /// Return Type: int
        ///nfds: int
        ///readfds: fd_set*
        ///writefds: fd_set*
        ///exceptfds: fd_set*
        ///timeout: timeval*
        [DllImport("ws2_32.dll", EntryPoint = "select", CallingConvention = CallingConvention.StdCall)]
        public static extern int select(int nfds, ref fd_set readfds, ref fd_set writefds, ref fd_set exceptfds, ref timeval timeout);

        // 19   A2 00003A8A send
        [DllImport("ws2_32.dll", EntryPoint = "send", CallingConvention = CallingConvention.StdCall)]
        public static extern int send([MarshalAs(UnmanagedType.SysUInt)] uint s, [In]  IntPtr buf, int len, int flags);

        // 20   A3 00003DD4 sendto
        [DllImport("ws2_32.dll", EntryPoint = "sendto", CallingConvention = CallingConvention.StdCall)]
        public static extern int sendto([MarshalAs(UnmanagedType.SysUInt)] uint s, [In] IntPtr buf, int len, int flags, ref sockaddr to, int tolen);

        // 21   A4 00003E7E setsockopt
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///level: int
        ///optname: int
        ///optval: char*
        ///optlen: int
        [DllImport("ws2_32.dll", EntryPoint = "setsockopt", CallingConvention = CallingConvention.StdCall)]
        public static extern int setsockopt([MarshalAs(UnmanagedType.SysUInt)] uint s, int level, int optname, [In] IntPtr optval, int optlen);

        // 22   A5 00006978 shutdown
        /// Return Type: int
        ///s: SOCKET->UINT_PTR->unsigned int
        ///how: int
        [DllImport("ws2_32.dll", EntryPoint = "shutdown", CallingConvention = CallingConvention.StdCall)]
        public static extern int shutdown([MarshalAs(UnmanagedType.SysUInt)] uint s, int how);

        // 23   A6 00004358 socket
        /// Return Type: SOCKET->UINT_PTR->unsigned int
        ///af: int
        ///type: int
        ///protocol: int
        [DllImport("ws2_32.dll", EntryPoint = "socket", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.SysUInt)]
        public static extern uint socket(int af, int type, int protocol);

        // WinSigGen doesn't know these ones
        //162   88 000049D0 freeaddrinfo
        
        //163   89 00004C58 getaddrinfo
        [DllImport("ws2_32.dll", EntryPoint = "getaddrinfo", CallingConvention = CallingConvention.StdCall)]
        public static extern int getaddrinfo([In] [MarshalAs(UnmanagedType.LPStr)] string pNodeName, 
            [In] [MarshalAs(UnmanagedType.LPStr)] string pServiceName, 
            [In] IntPtr pHints, 
            out IntPtr ppResult);

        // uses the addrinfow structure
        [DllImport("ws2_32.dll", EntryPoint = "GetAddrInfoW", CallingConvention = CallingConvention.StdCall)]
        public static extern int GetAddrInfoW([In] [MarshalAs(UnmanagedType.LPWStr)] string pNodeName, 
            [In] [MarshalAs(UnmanagedType.LPWStr)] string pServiceName, 
            [In] IntPtr pHints, 
            out IntPtr ppResult);

        //164   8D 0000315D getnameinfo
        //165   99 000113A8 inet_ntop
        //166   9A 000112C1 inet_pton
    }
}
