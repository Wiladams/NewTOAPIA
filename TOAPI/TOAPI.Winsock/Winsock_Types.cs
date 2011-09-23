using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.Winsock
{
    #region IPv4 Socket address information
    #region in_addr
    [StructLayout(LayoutKind.Sequential)]
    public struct in_addr
    {
        public Anonymous_cf7219a7_561f_4650_8ae4_fbd5695fe221 S_un;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_cf7219a7_561f_4650_8ae4_fbd5695fe221
    {
        [FieldOffsetAttribute(0)]
        public Anonymous_8ee52dbc_a992_4853_a328_103fc9181176 S_un_b;

        [FieldOffsetAttribute(0)]
        public Anonymous_63fe3feb_0017_41da_8c7f_24da3f99f4a8 S_un_w;

        [FieldOffsetAttribute(0)]
        public uint S_addr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_8ee52dbc_a992_4853_a328_103fc9181176
    {
        public byte s_b1;
        public byte s_b2;
        public byte s_b3;
        public byte s_b4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Anonymous_63fe3feb_0017_41da_8c7f_24da3f99f4a8
    {
        public ushort s_w1;
        public ushort s_w2;
    }

    /// <summary>
    /// The sockaddr_in data structure holds IPv4 address information for a socket.
    /// </summary>
    //[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    //public struct sockaddr_in
    //{
    //    public short sin_family;    /// short
    //    public ushort sin_port;     /// u_short->unsigned short
    //    public in_addr sin_addr;    /// in_addr
    //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.I1)]
    //    public string sin_zero;     /// char[8]
    //}
    #endregion

    #region sockaddr_in
    /// <summary>
    /// DotNet definition of sockaddr_in, the Winsock2 structure that roughly corresponds to an EndPoint structure from System.Net.  Used to interop with Winsock2,
    /// in this case to call IOControl for SIO_ROUTING_INTERFACE_QUERY.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class sockaddr_in
    {
        public short sin_family = Winsock.AF_INET;
        public ushort sin_port = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] sin_addr = new Byte[4];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public byte[] sin_zero = new Byte[8];
    }


    #endregion

    #region sockaddr
    /// <summary>
    /// The sockaddr structure is meant to be a protocol agnostic data structure
    /// used to represent socket addresses.  It will only work with IPv4 addresses
    /// though and not IPv6.
    /// For IPv6, there is the sockaddr_in6 data structure used for IPv6 addresses
    /// and it is a different size than the sockaddr structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct sockaddr
    {
        public ushort sa_family;    /// u_short->unsigned short

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14, ArraySubType = UnmanagedType.I1)]
        public string sa_data;      /// char[14]
    }
    #endregion
    #endregion

    #region IPv6 Socket address information
    #region in6_addr
    [StructLayout(LayoutKind.Explicit)]
    public struct Anonymous_bc4eb047_b0c4_45f0_9010_61fe024ea94c
    {
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I1)]
        [FieldOffset(0)]
        public byte[] Byte;

        /// USHORT[8]
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = UnmanagedType.U2)]
        [FieldOffset(0)]
        public ushort[] Word;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct in6_addr
    {
        public Anonymous_bc4eb047_b0c4_45f0_9010_61fe024ea94c u;
    }
    #endregion

    #region sockaddr_in6
    //[StructLayout(LayoutKind.Sequential)]
    //public struct sockaddr_in6
    //{
    //    public ushort sin6_family;
    //    public ushort sin6_port;
    //    public uint sin6_flowinfo;
    //    public in6_addr sin6_addr;
    //    public uint sin6_scope_id;
    //}
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class sockaddr_in6
    {
        public ushort sin_family = Winsock.AF_INET6;
        public ushort sin_port = 0;
        public uint sin6_flowinfo = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public byte[] sin_addr = new Byte[16];
        public uint sin6_scope_id = 0;
    }
    #endregion
    #endregion

    [StructLayout(LayoutKind.Sequential)]
    public struct SOCKET_ADDRESS
    {
        public IntPtr lpSockaddr;    /// LPSOCKADDR->sockaddr*
        public int iSockaddrLength;  /// INT->int
    }

    #region addrinfo
    [StructLayout(LayoutKind.Sequential)]
    public struct addrinfo
    {
        public int ai_flags;
        public int ai_family;
        public int ai_socktype;
        public int ai_protocol;
        public uint ai_addrlen;

        [MarshalAs(UnmanagedType.LPStr)]
        public string ai_canonname; /// char*
        public IntPtr ai_addr;  /// sockaddr*
        public IntPtr ai_next;  /// addrinfo*
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct addrinfoW
    {
        public int ai_flags;
        public int ai_family;
        public int ai_socktype;
        public int ai_protocol;
        public uint ai_addrlen;

        [MarshalAsAttribute(UnmanagedType.LPWStr)]
        public string ai_canonname;
        public System.IntPtr ai_addr;   /// sockaddr*
        public System.IntPtr ai_next;   /// addrinfoW*
    }

    #endregion

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct sockproto
    {
        public ushort sp_family;
        public ushort sp_protocol;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct fd_set
    {
        public uint fd_count;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64, ArraySubType = UnmanagedType.U4)]
        public uint[] fd_array;     /// SOCKET[64]
    }

    /*
* Argument structure for IP_ADD_MEMBERSHIP and IP_DROP_MEMBERSHIP.
*/
    public struct ip_mreq
    {
        public in_addr imr_multiaddr;  /* IP multicast address of group */
        public in_addr imr_interface;  /* local IP address of interface */
    }

    /*
 * Structure used for manipulating linger option.
 */
    public struct linger
    {
        public ushort l_onoff;                /* option on/off */
        public ushort l_linger;               /* linger time */
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct protoent
    {
        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string p_name;       /// char*
        public IntPtr p_aliases;    /// char**
        public short p_proto;       /// short
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct servent
    {
        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string s_name;       /// char*
        public IntPtr s_aliases;    /// char**
        public short s_port;        /// short
        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string s_proto;      /// char*
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct timeval
    {
        public int tv_sec;
        public int tv_usec;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WSABUF
    {
        public uint len;
        public IntPtr buf;  /// CHAR*
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct WSAData
    {
        public ushort wVersion;     /// WORD->unsigned short
        public ushort wHighVersion; /// WORD->unsigned short
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 257)]
        public string szDescription;    /// char[257]
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 129)]
        public string szSystemStatus;   /// char[129]
        public ushort iMaxSockets;  /// unsigned short
        public ushort iMaxUdpDg;    /// unsigned short
        public IntPtr lpVendorInfo; /// char*
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WSANAMESPACE_INFOA
    {
        public Guid NSProviderId;
        public uint dwNameSpace;
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fActive;
        public uint dwVersion;
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string lpszIdentifier;
    }

    [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct WSANAMESPACE_INFOW
    {
        public Guid NSProviderId;
        public uint dwNameSpace;
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public bool fActive;
        public uint dwVersion;
        [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.LPWStr)]
        public string lpszIdentifier;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WSAPROTOCOLCHAIN
    {
        public int ChainLen;
        public int[] ChainEntries;
    }
}
