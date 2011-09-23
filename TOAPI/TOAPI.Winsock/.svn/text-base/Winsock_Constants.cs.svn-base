using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.Winsock
{
    public partial class Winsock
    {
        public const int INVALID_SOCKET  = ~0;
        public const int SOCKET_ERROR =  -1;

/*
 * Constants and structures defined by the internet system,
 * Per RFC 790, September 1981, taken from the BSD file netinet/in.h.
 */

/*
 * Protocols
 */
        public const int
            IPPROTO_IP = 0,               /* dummy for IP */
            IPPROTO_ICMP = 1,               /* control message protocol */
            IPPROTO_IGMP = 2,               /* group management protocol */
            IPPROTO_GGP = 3,               /* gateway^2 (deprecated) */
            IPPROTO_TCP = 6,               /* tcp */
            IPPROTO_PUP = 12,              /* pup */
            IPPROTO_UDP = 17,              /* user datagram protocol */
            IPPROTO_IDP = 22,              /* xns idp */
            IPPROTO_RDP = 27,
            IPPROTO_IPV6 = 41, // IPv6 header
            IPPROTO_ROUTING = 43, // IPv6 Routing header
            IPPROTO_FRAGMENT = 44, // IPv6 fragmentation header
            IPPROTO_ESP = 50, // encapsulating security payload
            IPPROTO_AH = 51, // authentication header
            IPPROTO_ICMPV6 = 58, // ICMPv6
            IPPROTO_NONE = 59, // IPv6 no next header
            IPPROTO_DSTOPTS = 60, // IPv6 Destination options
            IPPROTO_ND = 77,              /* UNOFFICIAL net disk proto */
            IPPROTO_ICLFXBM = 78,
            IPPROTO_PIM = 103,
            IPPROTO_PGM = 113,
            IPPROTO_RM = IPPROTO_PGM,
            IPPROTO_L2TP = 115,
            IPPROTO_SCTP = 132;

        public const int
            IPPROTO_RAW          =   255,             /* raw IP packet */
            IPPROTO_MAX          =   256;

        //
        //  These are reserved for internal use by Windows.
        //
        public const int
            IPPROTO_RESERVED_RAW = 257,
            IPPROTO_RESERVED_IPSEC = 258,
            IPPROTO_RESERVED_IPSECOFFLOAD = 259,
            IPPROTO_RESERVED_MAX = 260;

        // Address families
        public const int
            AF_UNSPEC = 0,          /* unspecified */
            AF_UNIX = 1,            /* local to host (pipes, portals) */
            AF_INET = 2,            /* internetwork: UDP, TCP, etc. */
            AF_IMPLINK = 3,         /* arpanet imp addresses */
            AF_PUP = 4,             /* pup protocols: e.g. BSP */
            AF_CHAOS = 5,           /* mit CHAOS protocols */
            AF_IPX = 6,             /* IPX and SPX */
            AF_NS = 6,              /* XEROX NS protocols */
            AF_ISO = 7,             /* ISO protocols */
            AF_OSI = AF_ISO,        /* OSI is ISO */
            AF_ECMA = 8,            /* european computer manufacturers */
            AF_DATAKIT = 9,         /* datakit protocols */
            AF_CCITT = 10,          /* CCITT protocols, X.25 etc */
            AF_SNA = 11,            /* IBM SNA */
            AF_DECnet = 12,         /* DECnet */
            AF_DLI = 13,            /* Direct data link interface */
            AF_LAT = 14,            /* LAT */
            AF_HYLINK = 15,         /* NSC Hyperchannel */
            AF_APPLETALK = 16,      /* AppleTalk */
            AF_NETBIOS = 17,        /* NetBios-style addresses */
            AF_VOICEVIEW = 18,      /* VoiceView */
            AF_FIREFOX = 19,        /* FireFox */
            AF_UNKNOWN1 = 20,       /* Somebody is using this! */
            AF_BAN = 21,            /* Banyan */
            AF_INET6  =      23,              // Internetwork Version 6
            AF_IRDA   =      26,              // IrDA

            AF_MAX = 33;

        // Socket Types
        public const int
            SOCK_STREAM     = 1,    /* stream socket */
            SOCK_DGRAM      = 2,    /* datagram socket */
            SOCK_RAW        = 3,    /* raw-protocol interface */
            SOCK_RDM        = 4,    /* reliably-delivered message */
            SOCK_SEQPACKET  = 5;    /* sequenced packet stream */

        // for get/setsockopt, the levels can be:
        // IPPROTO_XXX - IP, IPV6, RM, TCP, UDP
        // NSPROTO_IPX
        // SOL_APPLETALK
        // SOL_IRLMP
        // SOL_SOCKET
        public const int 
            SOL_SOCKET     = 0xffff;          /* options for socket level */

        /*
        * Options for use with [gs]etsockopt at the IP level.
        */
        public const int
            IP_OPTIONS         = 1,           /* set/get IP per-packet options    */
            IP_MULTICAST_IF    = 2,           /* set/get IP multicast interface   */
            IP_MULTICAST_TTL   = 3,           /* set/get IP multicast timetolive  */
            IP_MULTICAST_LOOP  = 4,           /* set/get IP multicast loopback    */
            IP_ADD_MEMBERSHIP  = 5,           /* add  an IP group membership      */
            IP_DROP_MEMBERSHIP = 6,           /* drop an IP group membership      */
            IP_TTL             = 7,           /* set/get IP Time To Live          */
            IP_TOS             = 8,           /* set/get IP Type Of Service       */
            IP_DONTFRAGMENT    = 9;           /* set/get IP Don't Fragment flag   */

        public const int
            IP_DEFAULT_MULTICAST_TTL  = 1,    /* normally limit m'casts to 1 hop  */
            IP_DEFAULT_MULTICAST_LOOP = 1,    /* normally hear sends if a member  */
            IP_MAX_MEMBERSHIPS        = 20;   /* per socket; must fit in one mbuf */

        // Option flags per-socket.
        public const int
            SO_DEBUG        = 0x0001,          /* turn on debugging info recording */
            SO_ACCEPTCONN   = 0x0002,          /* socket has had listen() */
            SO_REUSEADDR    = 0x0004,          /* allow local address reuse */
            SO_KEEPALIVE    = 0x0008,          /* keep connections alive */
            SO_DONTROUTE    = 0x0010,          /* just use interface addresses */
            SO_BROADCAST    = 0x0020,          /* permit sending of broadcast msgs */
            SO_USELOOPBACK  = 0x0040,          /* bypass hardware when possible */
            SO_LINGER       = 0x0080,          /* linger on close if data present */
            SO_OOBINLINE    = 0x0100;          /* leave received OOB data in line */

        public const uint
            SO_DONTLINGER = unchecked((uint)(~SO_LINGER));

        public const int
            SO_EXCLUSIVEADDRUSE = ((int)(~SO_REUSEADDR)); /* disallow local address reuse */

        // Additional options.
        public const int
            SO_SNDBUF     =  0x1001,          /* send buffer size */
            SO_RCVBUF     =  0x1002,          /* receive buffer size */
            SO_SNDLOWAT   =  0x1003,          /* send low-water mark */
            SO_RCVLOWAT   =  0x1004,          /* receive low-water mark */
            SO_SNDTIMEO   =  0x1005,          /* send timeout */
            SO_RCVTIMEO   =  0x1006,          /* receive timeout */
            SO_ERROR      =  0x1007,          /* get error status and clear */
            SO_TYPE       =  0x1008;          /* get socket type */

        // Options for connect and disconnect data and options.  Used only by
        // non-TCP/IP transports such as DECNet, OSI TP4, etc.
        public const int
            SO_CONNDATA     = 0x7000,
            SO_CONNOPT      = 0x7001,
            SO_DISCDATA     = 0x7002,
            SO_DISCOPT      = 0x7003,
            SO_CONNDATALEN  = 0x7004,
            SO_CONNOPTLEN   = 0x7005,
            SO_DISCDATALEN  = 0x7006,
            SO_DISCOPTLEN   = 0x7007;

        /*
         * Option for opening sockets for synchronous access.
         */
        public const int
            SO_OPENTYPE             = 0x7008,
            SO_SYNCHRONOUS_ALERT    = 0x10,
            SO_SYNCHRONOUS_NONALERT = 0x20;

        /*
        * Other NT-specific options.
        */
        public const int
            SO_MAXDG        = 0x7009,
            SO_MAXPATHDG    = 0x700A,
            SO_UPDATE_ACCEPT_CONTEXT = 0x700B,
            SO_CONNECT_TIME = 0x700C;

        /*
        * TCP options.
        */
        public const int
            TCP_NODELAY     = 0x0001,
            TCP_BSDURGENT   = 0x7000;

        /*
        * WinSock 2 extension -- new options
        */
        public const int
            SO_GROUP_ID       = 0x2001,      /* ID of a socket group */
            SO_GROUP_PRIORITY = 0x2002,      /* the relative priority within a group*/
            SO_MAX_MSG_SIZE   = 0x2003,      /* maximum message size */
            SO_PROTOCOL_INFOA = 0x2004,      /* WSAPROTOCOL_INFOA structure */
            SO_PROTOCOL_INFOW = 0x2005,      /* WSAPROTOCOL_INFOW structure */
            SO_PROTOCOL_INFO  = SO_PROTOCOL_INFOW,
            PVD_CONFIG        = 0x3001,       /* configuration info for service provider */
            SO_CONDITIONAL_ACCEPT = 0x3002;   /* enable true conditional accept: */
                                                   /*  connection is not ack-ed to the */
                                       /*  other side until conditional */
                                       /*  function returns CF_ACCEPT */

        /*
        * Maximum queue length specifiable by listen.
        */
        public const int SOMAXCONN     =  0x7fffffff;

        public const int 
            MSG_OOB         =  0x0001,      /* process out-of-band data */
            MSG_PEEK        =  0x0002,      /* peek at incoming message */
            MSG_DONTROUTE   =  0x0004,      /* send without using routing tables */
            MSG_WAITALL     =  0x0008,      /* do not complete until packet is completely filled */
            MSG_PARTIAL     =  0x8000,      /* partial send or recv for message xport */
            MSG_INTERRUPT   =  0x10,           /* send/recv in the interrupt context */
            MSG_MAXIOVLEN   =  16;


        /*
         * Define constant based on rfc883, used by gethostbyxxxx() calls.
         */
        public const int 
            MAXGETHOSTSTRUCT  = 1024;

    }
}
