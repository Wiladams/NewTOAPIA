namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using UDPSOCKET=System.Int32;
    using UDTSOCKET = System.Int32;

    public enum UDTOpt
    {
        UDT_MSS,             // the Maximum Transfer Unit
        UDT_SNDSYN,          // if sending is blocking
        UDT_RCVSYN,          // if receiving is blocking
        UDT_CC,              // custom congestion control algorithm
        UDT_FC,		// Flight flag size (window size)
        UDT_SNDBUF,          // maximum buffer in sending queue
        UDT_RCVBUF,          // UDT receiving buffer size
        UDT_LINGER,          // waiting for unsent data when closing
        UDP_SNDBUF,          // UDP sending buffer size
        UDP_RCVBUF,          // UDP receiving buffer size
        UDT_MAXMSG,          // maximum datagram message size
        UDT_MSGTTL,          // time-to-live of a datagram message
        UDT_RENDEZVOUS,      // rendezvous connection mode
        UDT_SNDTIMEO,        // send() timeout
        UDT_RCVTIMEO,        // recv() timeout
        UDT_REUSEADDR,	// reuse an existing port or create a new one
        UDT_MAXBW		// maximum bandwidth (bytes per second) that the connection can use
    }

    public class CUDT
{
        public const UDTSOCKET INVALID_SOCK = -1;
public const int ERROR = -1;

   static CUDTUnited s_UDTUnited;               // UDT global management base

        #region Congestion Control
   private CCCVirtualFactory m_pCCFactory;      // Factory class to create a specific CC instance
   private CCC m_pCC;                           // congestion control class
   private CCache m_pCache;				        // network information cache
#endregion

#region Constants
        public const int m_iVersion = 4;            // UDT version, for compatibility use
        public const int m_iSYNInterval = 10000;    // Periodical Rate Control Interval, 10 ms
        public const int NI_MAXHOST = 64;
        public const int NI_MAXSERV = 64;

        public const int m_iSelfClockInterval = 64;          // ACK interval for self-clocking

#endregion

        #region Identification
        public UDPSOCKET m_SocketID;                        // UDT socket number
   UDTSockType m_iSockType;                     // Type of the UDT connection (SOCK_STREAM or SOCK_DGRAM)
   public UdtSocket m_PeerID;				// peer id, for multiplexer
#endregion

        #region Options
   public int m_iMSS;                                  // Maximum Segment Size
   public bool m_bSynSending;                          // Sending syncronization mode
   public bool m_bSynRecving;                          // Receiving syncronization mode
   public int m_iFlightFlagSize;                       // Maximum number of packets in flight from the peer side
   public int m_iSndBufSize;                           // Maximum UDT sender buffer size
   public int m_iRcvBufSize;                           // Maximum UDT receiver buffer size
   public LingerOption m_Linger;                             // Linger information on close
   public int m_iUDPSndBufSize;                        // UDP sending buffer size
   public int m_iUDPRcvBufSize;                        // UDP receiving buffer size
   public AddressFamily m_iIPversion;                            // IP version
   public bool m_bRendezvous;                          // Rendezvous connection mode
   public int m_iSndTimeOut;                           // sending timeout in milliseconds
   public int m_iRcvTimeOut;                           // receiving timeout in milliseconds
   public bool m_bReuseAddr;				// reuse an exiting port or not, for UDP multiplexer
   public Int64 m_llMaxBW;				// maximum data transfer rate (threshold)
#endregion

        #region Packet size and sequence number attributes
   private int m_iPktSize;                              // Maximum/regular packet size, in bytes
   private int m_iPayloadSize;                          // Maximum/regular payload size, in bytes
#endregion

#region Receiving related data
   private CRcvBuffer m_pRcvBuffer;                    // Receiver buffer
   private CRcvLossList m_pRcvLossList;                // Receiver loss list
   private CACKWindow m_pACKWindow;                    // ACK history window
   private CPktTimeWindow m_pRcvTimeWindow;            // Packet arrival time window

   private Int32 m_iRcvLastAck;                       // Last sent ACK
   private UInt64 m_ullLastAckTime;                   // Timestamp of last ACK
   private Int32 m_iRcvLastAckAck;                    // Last sent ACK that has been acknowledged
   private Int32 m_iAckSeqNo;                         // Last ACK sequence number
   private Int32 m_iRcvCurrSeqNo;                     // Largest received sequence number

   private UInt64 m_ullLastWarningTime;               // Last time that a warning message is sent

   private Int32 m_iPeerISN;                          // Initial Sequence Number of the peer side
#endregion

#region Sending Related Data
   CSndBuffer m_pSndBuffer;                    // Sender buffer
   CSndLossList m_pSndLossList;                // Sender loss list
   CPktTimeWindow m_pSndTimeWindow;            // Packet sending time window

   UInt64 m_ullInterval;             // Inter-packet time, in CPU clock cycles
   UInt64 m_ullTimeDiff;                      // aggregate difference in inter-packet time

   int m_iFlowWindowSize;              // Flow control window size
   double m_dCongestionWindow;         // congestion window size

   Int32 m_iSndLastAck;              // Last ACK received
   Int32 m_iSndLastDataAck;          // The real last ACK that updates the sender buffer and loss list
   Int32 m_iSndCurrSeqNo;            // The largest sequence number that has been sent
   Int32 m_iLastDecSeq;                       // Sequence number sent last decrease occurs
   Int32 m_iSndLastAck2;                      // Last ACK2 sent back
   UInt64 m_ullSndLastAck2Time;               // The time when last ACK2 was sent back

   Int32 m_iISN;                              // Initial Sequence Number
#endregion

#region Synchronization
        // synchronization: mutexes and conditions
   Mutex m_ConnectionLock;            // used to synchronize connection operation

   AutoResetEvent m_SendBlockCond;              // used to block "send" call
   Mutex m_SendBlockLock;             // lock associated to m_SendBlockCond

   Mutex m_AckLock;                   // used to protected sender's loss list when processing ACK

   AutoResetEvent m_RecvDataCond;               // used to block "recv" when there is no data
   Mutex m_RecvDataLock;              // lock associated to m_RecvDataCond

   Mutex m_SendLock;                  // used to synchronize "send" call
   Mutex m_RecvLock;                  // used to synchronize "recv" call
#endregion

        #region Timers
   private UInt32  m_ullCPUFrequency;                  // CPU clock frequency, used for Timer


    UInt32  m_ullNextACKTime;			// Next ACK time, in CPU clock cycles
    UInt32  m_ullNextNAKTime;			// Next NAK time
    UInt32  m_ullNextEXPTime;			// Next timeout

   volatile  UInt32  m_ullSYNInt;		// SYN interval
   volatile  UInt32  m_ullACKInt;		// ACK interval
   volatile  UInt32  m_ullNAKInt;		// NAK interval
   volatile  UInt32  m_ullEXPInt;		// EXP interval
   volatile  UInt32  m_ullMinEXPInt;		// Minimum EXP interval

   int m_iPktCount;				// packet counter for ACK
   int m_iLightACKCount;			// light ACK counter

   UInt64 m_ullTargetTime;			// target time of next packet sending

void checkTimers()
{
   // update CC parameters
   m_ullInterval = (UInt64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
   m_dCongestionWindow = m_pCC.m_dCWndSize;
   UInt64 minint = (UInt64)(m_ullCPUFrequency * m_pSndTimeWindow.getMinPktSndInt() * 0.9);
   if (m_ullInterval < minint)
      m_ullInterval = minint;

   Int64 currtime;
   CClock.rdtsc(out currtime);
   Int32 loss = m_pRcvLossList.getFirstLostSeq();

   if ((currtime > m_ullNextACKTime) || ((m_pCC.m_iACKInterval > 0) && (m_pCC.m_iACKInterval <= m_iPktCount)))
   {
      // ACK timer expired or ACK interval reached

      sendCtrl(2);
      CClock.rdtsc(currtime);
      if (m_pCC.m_iACKPeriod > 0)
         m_ullNextACKTime = currtime + m_pCC.m_iACKPeriod * m_ullCPUFrequency;
      else
         m_ullNextACKTime = currtime + m_ullACKInt;

      m_iPktCount = 0;
      m_iLightACKCount = 1;
   }
   else if (m_iSelfClockInterval * m_iLightACKCount <= m_iPktCount)
   {
      //send a "light" ACK
      sendCtrl(2, null, null, 4);
      ++ m_iLightACKCount;
   }

   if ((loss >= 0) && (currtime > m_ullNextNAKTime))
   {
      // NAK timer expired, and there is loss to be reported.
      sendCtrl(3);

      CClock.rdtsc(currtime);
      m_ullNextNAKTime = currtime + m_ullNAKInt;
   }

   if (currtime > m_ullNextEXPTime)
   {
      // Haven't receive any information from the peer, is it dead?!
      // timeout: at least 16 expirations and must be greater than 3 seconds and be less than 30 seconds
      if (((m_iEXPCount > 16) && (m_iEXPCount * ((m_iEXPCount - 1) * (m_iRTT + 4 * m_iRTTVar) / 2 + m_iSYNInterval) > 3000000))
          || (m_iEXPCount > 30)
          || (m_iEXPCount * ((m_iEXPCount - 1) * (m_iRTT + 4 * m_iRTTVar) / 2 + m_iSYNInterval) > 30000000))
      {
         //
         // Connection is broken. 
         // UDT does not signal any information about this instead of to stop quietly.
         // Apllication will detect this when it calls any UDT methods next time.
         //
         m_bClosing = true;
         m_bBroken = true;
         m_iBrokenCounter = 30;

         // update snd U list to remove this socket
         m_pSndQueue.m_pSndUList.update(this);

         releaseSynch();

         CClock.triggerEvent();

         return;
      }

      // sender: Insert all the packets sent after last received acknowledgement into the sender loss list.
      // recver: Send out a keep-alive packet
      if (CSeqNo.incseq(m_iSndCurrSeqNo) != m_iSndLastAck)
      {
         Int32 csn = m_iSndCurrSeqNo;
         int num = m_pSndLossList.insert((Int32)(m_iSndLastAck), csn);
         m_iTraceSndLoss += num;
         m_iSndLossTotal += num;

         m_pCC.onTimeout();
         // update CC parameters
         m_ullInterval = (Int64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
         m_dCongestionWindow = m_pCC.m_dCWndSize;
      }
      else
         sendCtrl(1);

      if (m_pSndBuffer.getCurrBufSize() > 0)
      {
         // immediately restart transmission
         m_pSndQueue.m_pSndUList.update(this);
      }

      ++ m_iEXPCount;
      m_ullEXPInt = (m_iEXPCount * (m_iRTT + 4 * m_iRTTVar) + m_iSYNInterval) * m_ullCPUFrequency;
      if (m_ullEXPInt < m_iEXPCount * 100000 * m_ullCPUFrequency)
         m_ullEXPInt = m_iEXPCount * 100000 * m_ullCPUFrequency;
      CClock.rdtsc(m_ullNextEXPTime);
      m_ullNextEXPTime += m_ullEXPInt;
   }
}
        #endregion

#region Trace
           // Trace
   Int64 m_StartTime;                        // timestamp when the UDT entity is started
   Int64 m_llSentTotal;                       // total number of sent data packets, including retransmissions
   Int64 m_llRecvTotal;                       // total number of received packets
   int m_iSndLossTotal;                         // total number of lost packets (sender side)
   int m_iRcvLossTotal;                         // total number of lost packets (receiver side)
   int m_iRetransTotal;                         // total number of retransmitted packets
   int m_iSentACKTotal;                         // total number of sent ACK packets
   int m_iRecvACKTotal;                         // total number of received ACK packets
   int m_iSentNAKTotal;                         // total number of sent NAK packets
   int m_iRecvNAKTotal;                         // total number of received NAK packets
   Int64 m_llSndDurationTotal;		// total real time for sending
#endregion

#region Statistics
        // Statistics
        Int64  m_LastSampleTime;                   // last performance sample time
   Int64 m_llTraceSent;                       // number of pakctes sent in the last trace interval
   Int64 m_llTraceRecv;                       // number of pakctes received in the last trace interval
   int m_iTraceSndLoss;                         // number of lost packets in the last trace interval (sender side)
   int m_iTraceRcvLoss;                         // number of lost packets in the last trace interval (receiver side)
   int m_iTraceRetrans;                         // number of retransmitted packets in the last trace interval
   int m_iSentACK;                              // number of ACKs sent in the last trace interval
   int m_iRecvACK;                              // number of ACKs received in the last trace interval
   int m_iSentNAK;                              // number of NAKs sent in the last trace interval
   int m_iRecvNAK;                              // number of NAKs received in the last trace interval
   Int64 m_llSndDuration;			// real time for sending
   Int64 m_llSndDurationCounter;		// timers to record the sending duration
#endregion

        #region Status
   private volatile bool m_bListening;                  // If the UDT entit is listening to connection
   internal volatile bool m_bConnected;                  // Whether the connection is on or off
   public static volatile bool m_bClosing;                    // If the UDT entity is closing
   public volatile bool m_bShutdown;                   // If the peer side has shutdown the connection
   internal volatile bool m_bBroken;                     // If the connection has been broken
   public bool m_bOpened;                              // If the UDT entity has been opened
   public int m_iBrokenCounter;			// a counter (number of GC checks) to let the GC tag this socket as disconnected

   public int m_iEXPCount;                             // Expiration counter
   public int m_iBandwidth;                            // Estimated bandwidth
   public int m_iRTT;                                  // RTT
   public int m_iRTTVar;                               // RTT varianc
   public int m_iDeliveryRate;				// Packet arrival rate at the receiver side
#endregion

#region UDP multiplexer
   internal CSndQueue m_pSndQueue;			    // packet sending queue
   internal CRcvQueue m_pRcvQueue;			    // packet receiving queue
   internal IPEndPoint m_pPeerAddr;			    // peer address
   internal UInt32[] m_piSelfIP = new uint[4];	// local UDP IP address
   internal CSNode m_pSNode;				    // node information for UDT list used in snd queue
   internal CRNode m_pRNode;                    // node information for UDT list used in rcv queue
#endregion

#region constructor and desctructor
        CUDT(CUDT ancestor)
{
   m_pSndBuffer = null;
   m_pRcvBuffer = null;
   m_pSndLossList = null;
   m_pRcvLossList = null;
   m_pACKWindow = null;
   m_pSndTimeWindow = null;
   m_pRcvTimeWindow = null;

   m_pSndQueue = null;
   m_pRcvQueue = null;
   m_pPeerAddr = null;
   m_pSNode = null;
   m_pRNode = null;

   // Initilize mutex and condition variables
   initSynch();

   // Default UDT configurations
   m_iMSS = ancestor.m_iMSS;
   m_bSynSending = ancestor.m_bSynSending;
   m_bSynRecving = ancestor.m_bSynRecving;
   m_iFlightFlagSize = ancestor.m_iFlightFlagSize;
   m_iSndBufSize = ancestor.m_iSndBufSize;
   m_iRcvBufSize = ancestor.m_iRcvBufSize;
   m_Linger = ancestor.m_Linger;
   m_iUDPSndBufSize = ancestor.m_iUDPSndBufSize;
   m_iUDPRcvBufSize = ancestor.m_iUDPRcvBufSize;
   m_iSockType = ancestor.m_iSockType;
   m_iIPversion = ancestor.m_iIPversion;
   m_bRendezvous = ancestor.m_bRendezvous;
   m_iSndTimeOut = ancestor.m_iSndTimeOut;
   m_iRcvTimeOut = ancestor.m_iRcvTimeOut;
   m_bReuseAddr = true;	// this must be true, because all accepted sockets shared the same port with the listener
   m_llMaxBW = ancestor.m_llMaxBW;

   m_pCCFactory = ancestor.m_pCCFactory.clone();
   m_pCC = null;
   m_pCache = ancestor.m_pCache;

   // Initial status
   m_bOpened = false;
   m_bListening = false;
   m_bConnected = false;
   m_bClosing = false;
   m_bShutdown = false;
   m_bBroken = false;
}

   internal CUDT()
   {
          // Initilize mutex and condition variables
   initSynch();

   // Default UDT configurations
   m_iMSS = 1500;
   m_bSynSending = true;
   m_bSynRecving = true;
   m_iFlightFlagSize = 25600;
   m_iSndBufSize = 8192;
   m_iRcvBufSize = 8192; //Rcv buffer MUST NOT be bigger than Flight Flag size
   m_Linger = new LingerOption(1, 180);
   //m_Linger.l_onoff = 1;
   //m_Linger.l_linger = 180;
   m_iUDPSndBufSize = 65536;
   m_iUDPRcvBufSize = m_iRcvBufSize * m_iMSS;
   m_iIPversion = AF_INET;
   m_bRendezvous = false;
   m_iSndTimeOut = -1;
   m_iRcvTimeOut = -1;
   m_bReuseAddr = true;
   m_llMaxBW = -1;

   m_pCCFactory = new CCCFactory<CUDTCC>();
   m_pCC = null;
   m_pCache = null;

   // Initial status
   m_bOpened = false;
   m_bListening = false;
   m_bConnected = false;
   m_bClosing = false;
   m_bShutdown = false;
   m_bBroken = false;

   }

   //CUDT(CUDT ancestor);
   //const CUDT& operator=(const CUDT&) {return *this;}
   ~CUDT()
   {
          // release mutex/condtion variables
   destroySynch();

   // destroy the data structures
   //delete m_pSndBuffer;
   //delete m_pRcvBuffer;
   //delete m_pSndLossList;
   //delete m_pRcvLossList;
   //delete m_pACKWindow;
   //delete m_pSndTimeWindow;
   //delete m_pRcvTimeWindow;
   //delete m_pCCFactory;
   //delete m_pCC;
   //delete m_pPeerAddr;
   //delete m_pSNode;
   //delete m_pRNode;
   }
#endregion

        // API
public static int startup()
{
   return s_UDTUnited.startup();
}

public static int cleanup()
{
   return s_UDTUnited.cleanup();
}

       /// <summary>
       /// 
       /// </summary>
       /// <param name="af"></param>
       /// <param name="type"></param>
       /// <param name="protocol"></param>
       /// <returns></returns>
 //public static UDTSOCKET socket(int af, int type = SOCK_STREAM, int protocol = 0);
public static UDTSOCKET socket(AddressFamily af, UDTSockType type, int protocol)
{
   if (!s_UDTUnited.m_bGCStatus)
      s_UDTUnited.startup();

   try
   {
      return s_UDTUnited.newSocket(af, type);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return INVALID_SOCK;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return INVALID_SOCK;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return INVALID_SOCK;
   }
}
        
public static int bind(UDTSOCKET u, IPAddress name, int namelen)
{
   try
   {
      return s_UDTUnited.bind(u, name, namelen);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

   public static int bind(UDTSOCKET u, UDPSOCKET udpsock)
{
   try
   {
      return s_UDTUnited.bind(u, udpsock);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

public static int listen(IPAddress addr, out CPacket packet)
{
   CGuard cg = new CGuard(m_ConnectionLock);
   if (m_bClosing)
      return 1002;

   CHandShake hs = (CHandShake )packet.m_pcData;

   // SYN cookie
   byte[] clienthost = new byte[NI_MAXHOST];
   byte[] clientport = new byte[NI_MAXSERV];
   getnameinfo(addr, (AF_INET == m_iVersion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6), clienthost, sizeof(clienthost), clientport, sizeof(clientport), NI_NUMERICHOST|NI_NUMERICSERV);
   Int64 timestamp = (CClock.getTime() - m_StartTime) / 60000000; // secret changes every one minute
   //byte[] cookiestr = new byte[1024];
   //sprintf(cookiestr, "%s:%s:%lld", clienthost, clientport, (long long int)timestamp);
    
    string cookiestr = string.Format("{0}:{1}:{2}", clienthost, clientport, timestamp);
   byte[] cookie = new byte[16];
   CMD5.compute(cookiestr, cookie);

   if (1 == hs.m_iReqType)
   {
      hs.m_iCookie = *(int*)cookie;
      packet.m_iID = hs.m_iID;
      m_pSndQueue.sendto(addr, packet);

      return 0;
   }
   else
   {
      if (hs.m_iCookie != *(int*)cookie)
      {
         timestamp --;
         cookiestr = string.Format("{0}:{1}:{2}", clienthost, clientport, timestamp);
         CMD5.compute(cookiestr, out cookie);

         if (hs.m_iCookie != *(int*)cookie)
            return -1;
      }
   }

   Int32 id = hs.m_iID;

   // When a peer side connects in...
   if ((1 == packet.getFlag()) && (0 == packet.getType()))
   {
      if ((hs.m_iVersion != m_iVersion) || (hs.m_iType != m_iSockType) || (-1 == s_UDTUnited.newConnection(m_SocketID, addr, hs)))
      {
         // couldn't create a new connection, reject the request
         hs.m_iReqType = 1002;
      }

      packet.m_iID = id;

      m_pSndQueue.sendto(addr, packet);
   }

   return hs.m_iReqType;
}
   
        public static UDTSOCKET accept(UDTSOCKET u, IPAddress addr, out int addrlen)
{
   try
   {
      return s_UDTUnited.accept(u, addr, addrlen);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return INVALID_SOCK;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return INVALID_SOCK;
   }
}

        public static int connect(UDTSOCKET u, IPAddress name, int namelen)
{
   try
   {
      return s_UDTUnited.connect(u, name, namelen);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int close(UDTSOCKET u)
{
   try
   {
      return s_UDTUnited.close(u);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int getpeername(UDTSOCKET u, IPAddress name, out int namelen)
{
   try
   {
      return s_UDTUnited.getpeername(u, name, namelen);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}


        public static int getsockname(UDTSOCKET u, IPAddress name, out int namelen)
{
   try
   {
      return s_UDTUnited.getsockname(u, name, namelen);;
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int getsockopt(UDTSOCKET u, int level, UDTOpt optname, Object optval, out int optlen)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      udt.getOpt(optname, optval, outoptlen);
      return 0;
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int setsockopt(UDTSOCKET u, int level, UDTOpt optname, Object optval, int optlen)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      udt.setOpt(optname, optval, optlen);
      return 0;
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int send(UDTSOCKET u,byte[] buf, int len, int flags)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.send(buf, len);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int recv(UDTSOCKET u, byte[] buf, int len, int flags)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.recv(buf, len);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int sendmsg(UDTSOCKET u, byte[] buf, int len)
    {
        sendmsg(u, buf, len, -1, false);
    }

        public static int sendmsg(UDTSOCKET u, byte[] buf, int len, int ttl, bool inorder)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.sendmsg(buf, len, ttl, inorder);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int recvmsg(UDTSOCKET u, byte[] buf, int len)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.recvmsg(buf, len);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}


        public static Int64 sendfile(UDTSOCKET u, Stream ifs, Int64 offset, Int64 size)
        {
            sendfile(u, ifs, offset, size, 364000);
        }

        public static Int64 sendfile(UDTSOCKET u, Stream ifs, Int64 offset, Int64 size, int block)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.sendfile(ifs, offset, size, block);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

   public static Int64 recvfile(UDTSOCKET u, Stream ofs, Int64 offset, Int64 size)
    {
        recvfile(u, ofs, offset, size, 7280000);
    }

        public static Int64 recvfile(UDTSOCKET u, Stream ofs, Int64 offset, Int64 size, int block)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      return udt.recvfile(ofs, offset, size, block);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int select(int nfds, fd_set readfds, fd_set writefds, fd_set exceptfds, timeval timeout)
{
   if ((null == readfds) && (null == writefds) && (null == exceptfds))
   {
      s_UDTUnited.setError(new CUDTException(5, 3, 0));
      return ERROR;
   }

   try
   {
      return s_UDTUnited.select(readfds, writefds, exceptfds, timeout);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static int selectEx(UDTSOCKET[] fds, UDTSOCKET[] readfds, UDTSOCKET[] writefds, UDTSOCKET[] exceptfds, Int64 msTimeOut)
{
   if ((null == readfds) && (null == writefds) && (null == exceptfds))
   {
      s_UDTUnited.setError(new CUDTException(5, 3, 0));
      return ERROR;
   }

   try
   {
      return s_UDTUnited.selectEx(fds, readfds, writefds, exceptfds, msTimeOut);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

public static CUDTException getlasterror()
{
   return s_UDTUnited.getError();
}

    public static int perfmon(UDTSOCKET u, CPerfMon perf)
    {
        perfmon(u, perf, true);
    }

public static int perfmon(UDTSOCKET u, CPerfMon perf, bool clear)
{
   try
   {
      CUDT udt = s_UDTUnited.lookup(u);
      udt.sample(perf, clear);
      return 0;
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

        public static CUDT getUDTHandle(UDTSOCKET u)
{
   try
   {
      return s_UDTUnited.lookup(u);
   }
   catch (Exception e)
   {
      return null;
   }
        }

      // Functionality:
      //    initialize a UDT entity and bind to a local address.
      // Parameters:
      //    None.
      // Returned value:
      //    None.

public void open()
{
   CGuard cg = new CGuard(m_ConnectionLock);

   // Initial sequence number, loss, acknowledgement, etc.
   m_iPktSize = m_iMSS - 28;
   m_iPayloadSize = m_iPktSize - CPacket.m_iPktHdrSize;

   m_iEXPCount = 1;
   m_iBandwidth = 1;
   m_iDeliveryRate = 16;
   m_iAckSeqNo = 0;
   m_ullLastAckTime = 0;

   // trace information
   m_StartTime = CClock.getTime();
   m_llSentTotal = m_llRecvTotal = m_iSndLossTotal = m_iRcvLossTotal = m_iRetransTotal = m_iSentACKTotal = m_iRecvACKTotal = m_iSentNAKTotal = m_iRecvNAKTotal = 0;
   m_LastSampleTime = CClock.getTime();
   m_llTraceSent = m_llTraceRecv = m_iTraceSndLoss = m_iTraceRcvLoss = m_iTraceRetrans = m_iSentACK = m_iRecvACK = m_iSentNAK = m_iRecvNAK = 0;
   m_llSndDuration = m_llSndDurationTotal = 0;

   // structures for queue
   if (null == m_pSNode)
      m_pSNode = new CSNode();
   m_pSNode.m_pUDT = this;
   m_pSNode.m_llTimeStamp = 1;
   m_pSNode.m_iHeapLoc = -1;

   if (null == m_pRNode)
      m_pRNode = new CRNode();
   m_pRNode.m_pUDT = this;
   m_pRNode.m_llTimeStamp = 1;
   m_pRNode.m_pPrev = m_pRNode.m_pNext = null;
   m_pRNode.m_bOnList = false;

   m_iRTT = 10 * m_iSYNInterval;
   m_iRTTVar = m_iRTT >> 1;
   m_ullCPUFrequency = CClock.getCPUFrequency();

   // set up the timers
   m_ullSYNInt = m_iSYNInterval * m_ullCPUFrequency;
   
   m_ullACKInt = m_ullSYNInt;
   m_ullNAKInt = (m_iRTT + 4 * m_iRTTVar) * m_ullCPUFrequency;
   m_ullEXPInt = (m_iRTT + 4 * m_iRTTVar) * m_ullCPUFrequency + m_ullSYNInt;
   m_ullMinEXPInt = 100000 * m_ullCPUFrequency;

   CClock.rdtsc(m_ullNextACKTime);
   m_ullNextACKTime += m_ullSYNInt;
   CClock.rdtsc(m_ullNextNAKTime);
   m_ullNextNAKTime += m_ullNAKInt;
   CClock.rdtsc(m_ullNextEXPTime);
   m_ullNextEXPTime += m_ullEXPInt;

   m_iPktCount = 0;
   m_iLightACKCount = 1;

   m_ullTargetTime = 0;
   m_ullTimeDiff = 0;

   // Now UDT is opened.
   m_bOpened = true;
}
        
        public int listen(UDTSOCKET u, int backlog)
{
   try
   {
      return s_UDTUnited.listen(u, backlog);
   }
   catch (CUDTException e)
   {
      s_UDTUnited.setError(new CUDTException(e));
      return ERROR;
   }
   catch (bad_alloc)
   {
      s_UDTUnited.setError(new CUDTException(3, 2, 0));
      return ERROR;
   }
   catch (Exception e)
   {
      s_UDTUnited.setError(new CUDTException(-1, 0, 0));
      return ERROR;
   }
}

      // Functionality:
      //    Start listening to any connection request.
      // Parameters:
      //    None.
      // Returned value:
      //    None.

public void listen()
{
   CGuard cg = new CGuard(m_ConnectionLock);

   if (!m_bOpened)
      throw new CUDTException(5, 0, 0);

   if (m_bConnected)
      throw new CUDTException(5, 2, 0);

   // listen can be called more than once
   if (m_bListening)
      return;

   // if there is already another socket listening on the same port
   if (m_pRcvQueue.setListener(this) < 0)
      throw new CUDTException(5, 11, 0);

   m_bListening = true;
}

      // Functionality:
      //    Connect to a UDT entity listening at address "peer".
      // Parameters:
      //    0) [in] peer: The address of the listening UDT entity.
      // Returned value:
      //    None.

        public void connect(IPEndPoint serv_addr)
{
   CGuard cg = new CGuard(m_ConnectionLock);

   if (!m_bOpened)
      throw new CUDTException(5, 0, 0);

   if (m_bListening)
      throw new CUDTException(5, 2, 0);

   if (m_bConnected)
      throw new CUDTException(5, 2, 0);

   // register this socket in the rendezvous queue
   m_pRcvQueue.m_pRendezvousQueue.insert(m_SocketID, m_iIPversion, serv_addr);

   CPacket request;
   byte[] reqdata = new byte [m_iPayloadSize];
   CHandShake req = (CHandShake)reqdata;

   CPacket response;
   byte[] resdata = new byte [m_iPayloadSize];
   CHandShake res = (CHandShake)resdata;

   // This is my current configurations.
   req.m_iVersion = m_iVersion;
   req.m_iType = m_iSockType;
   req.m_iMSS = m_iMSS;
   req.m_iFlightFlagSize = (m_iRcvBufSize < m_iFlightFlagSize)? m_iRcvBufSize : m_iFlightFlagSize;
   req.m_iReqType = (!m_bRendezvous) ? 1 : 0;
   req.m_iID = m_SocketID;
   CIPAddress.ntop(serv_addr, req.m_piPeerIP, m_iIPversion);

   // Random Initial Sequence Number
   srand((int)CClock.getTime());
   m_iISN = req.m_iISN = (Int32)(CSeqNo.m_iMaxSeqNo * ((double)(rand()) / RAND_MAX));

   m_iLastDecSeq = req.m_iISN - 1;
   m_iSndLastAck = req.m_iISN;
   m_iSndLastDataAck = req.m_iISN;
   m_iSndCurrSeqNo = req.m_iISN - 1;
   m_iSndLastAck2 = req.m_iISN;
   m_ullSndLastAck2Time = CClock.getTime();

   // Inform the server my configurations.
   request.pack(0, null, reqdata, sizeof(CHandShake));
   // ID = 0, connection request
   request.m_iID = 0;

   // Wait for the negotiated configurations from the peer side.
   response.pack(0, null, resdata, sizeof(CHandShake));

   Int64 timeo = 3000000;
   if (m_bRendezvous)
      timeo *= 10;
   Int64 entertime = CClock.getTime();
   CUDTException e = new CUDTException(0, 0);

   byte[] tmp = null;

   while (!m_bClosing)
   {
      m_pSndQueue.sendto(serv_addr, request);

      response.setLength(m_iPayloadSize);
      if (m_pRcvQueue.recvfrom(m_SocketID, response) > 0)
      {
         if (m_bRendezvous && ((0 == response.getFlag()) || (1 == response.getType())) && (null != tmp))
         {
            // a data packet or a keep-alive packet comes, which means the peer side is already connected
            // in this situation, a previously recorded response (tmp) will be used
            memcpy(resdata, tmp, sizeof(CHandShake));
            memcpy(m_piSelfIP, res.m_piPeerIP, 16);
            break;
         }

         if ((1 != response.getFlag()) || (0 != response.getType()))
            response.setLength(-1);

         if (m_bRendezvous)
         {
            // regular connect should NOT communicate with rendezvous connect
            // rendezvous connect require 3-way handshake
            if (1 == res.m_iReqType)
               response.setLength(-1);
            else if ((0 == res.m_iReqType) || (0 == req.m_iReqType))
            {
               tmp = new char [m_iPayloadSize];
               memcpy(tmp, resdata, sizeof(CHandShake));

               req.m_iReqType = -1;
               request.m_iID = res.m_iID;
               response.setLength(-1);
            }
         }
         else
         {
            // set cookie
            if (1 == res.m_iReqType)
            {
               req.m_iReqType = -1;
               req.m_iCookie = res.m_iCookie;
               response.setLength(-1);
            }
         }
      }

      if (response.getLength() > 0)
      {
         memcpy(m_piSelfIP, res.m_piPeerIP, 16);
         break;
      }

      if (CClock.getTime() > entertime + timeo)
      {
         // timeout
         e = CUDTException(1, 1, 0);
         break;
      }
   }

   //delete [] tmp;
   //delete [] reqdata;

   if (e.getErrorCode() == 0)
   {
      if (m_bClosing)						// if the socket is closed before connection...
         e = new CUDTException(1);
      else if (1002 == res.m_iReqType)				// connection request rejected
         e = new CUDTException(1, 2, 0);
      else if ((!m_bRendezvous) && (m_iISN != res.m_iISN))	// secuity check
         e = new CUDTException(1, 4, 0);
   }

   if (e.getErrorCode() != 0)
   {
      // connection failure, clean up and throw exception
     // delete [] resdata;

      if (m_bRendezvous)
         m_pRcvQueue.m_pRendezvousQueue.remove(m_SocketID);

      throw e;
   }

   // Got it. Re-configure according to the negotiated values.
   m_iMSS = res.m_iMSS;
   m_iFlowWindowSize = res.m_iFlightFlagSize;
   m_iPktSize = m_iMSS - 28;
   m_iPayloadSize = m_iPktSize - CPacket.m_iPktHdrSize;
   m_iPeerISN = res.m_iISN;
   m_iRcvLastAck = res.m_iISN;
   m_iRcvLastAckAck = res.m_iISN;
   m_iRcvCurrSeqNo = res.m_iISN - 1;
   m_PeerID = res.m_iID;

   delete [] resdata;

   // Prepare all data structures
   try
   {
      m_pSndBuffer = new CSndBuffer(32, m_iPayloadSize);
      m_pRcvBuffer = new CRcvBuffer(m_iRcvBufSize, &(m_pRcvQueue.m_UnitQueue));
      // after introducing lite ACK, the sndlosslist may not be cleared in time, so it requires twice space.
      m_pSndLossList = new CSndLossList(m_iFlowWindowSize * 2);
      m_pRcvLossList = new CRcvLossList(m_iFlightFlagSize);
      m_pACKWindow = new CACKWindow(4096);
      m_pRcvTimeWindow = new CPktTimeWindow(16, 64);
      m_pSndTimeWindow = new CPktTimeWindow();
   }
   catch (Exception e)
   {
      throw new CUDTException(3, 2, 0);
   }

   m_pCC = m_pCCFactory.create();
   m_pCC.m_UDT = m_SocketID;
   m_ullInterval = (Int64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
   m_dCongestionWindow = m_pCC.m_dCWndSize;

   CInfoBlock ib;
   if (m_pCache.lookup(serv_addr, m_iIPversion, &ib) >= 0)
   {
      m_iRTT = ib.m_iRTT;
      m_iBandwidth = ib.m_iBandwidth;
   }

   m_pCC.setMSS(m_iMSS);
   m_pCC.setMaxCWndSize((int)m_iFlowWindowSize);
   m_pCC.setSndCurrSeqNo((int)m_iSndCurrSeqNo);
   m_pCC.setRcvRate(m_iDeliveryRate);
   m_pCC.setRTT(m_iRTT);
   m_pCC.setBandwidth(m_iBandwidth);
   if (m_llMaxBW > 0) m_pCC.setUserParam((char*)&(m_llMaxBW), 8);
   m_pCC.init();

   m_pPeerAddr = serv_addr;
   //         m_pPeerAddr = (AddressFamily.InterNetwork == m_iIPversion) ? new IPEndPoint(IPAddress.Any,0) : new IPEndPoint(IPAddress.IPv6Any,0);
   //memcpy(m_pPeerAddr, serv_addr, (AF_INET == m_iIPversion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6));

   // And, I am connected too.
   m_bConnected = true;

   // register this socket for receiving data packets
   m_pRcvQueue.setNewEntry(this);

   // remove from rendezvous queue
   m_pRcvQueue.m_pRendezvousQueue.remove(m_SocketID);
}

      // Functionality:
      //    Connect to a UDT entity listening at address "peer", which has sent "hs" request.
      // Parameters:
      //    0) [in] peer: The address of the listening UDT entity.
      //    1) [in/out] hs: The handshake information sent by the peer side (in), negotiated value (out).
      // Returned value:
      //    None.

public void connect(IPAddress peer, CHandShake hs)
{
   // Type 0 (handshake) control packet
   CPacket initpkt;
   CHandShake ci;
   memcpy(&ci, hs, sizeof(CHandShake));
   initpkt.pack(0, null, &ci, sizeof(CHandShake));

   // Uses the smaller MSS between the peers        
   if (ci.m_iMSS > m_iMSS)
      ci.m_iMSS = m_iMSS;
   else
      m_iMSS = ci.m_iMSS;

   // exchange info for maximum flow window size
   m_iFlowWindowSize = ci.m_iFlightFlagSize;
   ci.m_iFlightFlagSize = (m_iRcvBufSize < m_iFlightFlagSize)? m_iRcvBufSize : m_iFlightFlagSize;

   m_iPeerISN = ci.m_iISN;

   m_iRcvLastAck = ci.m_iISN;
   m_iRcvLastAckAck = ci.m_iISN;
   m_iRcvCurrSeqNo = ci.m_iISN - 1;

   m_PeerID = ci.m_iID;
   ci.m_iID = m_SocketID;

   // use peer's ISN and send it back for security check
   m_iISN = ci.m_iISN;

   m_iLastDecSeq = m_iISN - 1;
   m_iSndLastAck = m_iISN;
   m_iSndLastDataAck = m_iISN;
   m_iSndCurrSeqNo = m_iISN - 1;
   m_iSndLastAck2 = m_iISN;
   m_ullSndLastAck2Time = CClock.getTime();

   // this is a reponse handshake
   ci.m_iReqType = -1;

   // get local IP address and send the peer its IP address (because UDP cannot get local IP address)
   memcpy(m_piSelfIP, ci.m_piPeerIP, 16);
   CIPAddress.ntop(peer, ci.m_piPeerIP, m_iIPversion);

   // Save the negotiated configurations.
   memcpy(hs, &ci, sizeof(CHandShake));
  
   m_iPktSize = m_iMSS - 28;
   m_iPayloadSize = m_iPktSize - CPacket.m_iPktHdrSize;

   // Prepare all structures
   try
   {
      m_pSndBuffer = new CSndBuffer(32, m_iPayloadSize);
      m_pRcvBuffer = new CRcvBuffer(m_iRcvBufSize, &(m_pRcvQueue.m_UnitQueue));
      m_pSndLossList = new CSndLossList(m_iFlowWindowSize * 2);
      m_pRcvLossList = new CRcvLossList(m_iFlightFlagSize);
      m_pACKWindow = new CACKWindow(4096);
      m_pRcvTimeWindow = new CPktTimeWindow(16, 64);
      m_pSndTimeWindow = new CPktTimeWindow();
   }
   catch (Exception e)
   {
      throw new CUDTException(3, 2, 0);
   }

   m_pCC = m_pCCFactory.create();
   m_pCC.m_UDT = m_SocketID;
   m_ullInterval = (long)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
   m_dCongestionWindow = m_pCC.m_dCWndSize;

   CInfoBlock ib;
   if (m_pCache.lookup(peer, m_iIPversion, &ib) >= 0)
   {
      m_iRTT = ib.m_iRTT;
      m_iBandwidth = ib.m_iBandwidth;
   }

   m_pCC.setMSS(m_iMSS);
   m_pCC.setMaxCWndSize((int)m_iFlowWindowSize);
   m_pCC.setSndCurrSeqNo((int)m_iSndCurrSeqNo);
   m_pCC.setRcvRate(m_iDeliveryRate);
   m_pCC.setRTT(m_iRTT);
   m_pCC.setBandwidth(m_iBandwidth);
   if (m_llMaxBW > 0) m_pCC.setUserParam((char*)&(m_llMaxBW), 8);
   m_pCC.init();

    m_pPeerAddr = peer;
   //m_pPeerAddr = (AF_INET == m_iIPversion) ? (sockaddr*)new sockaddr_in : (sockaddr*)new sockaddr_in6;
   //memcpy(m_pPeerAddr, peer, (AF_INET == m_iIPversion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6));

   // And of course, it is connected.
   m_bConnected = true;

   // register this socket for receiving data packets
   m_pRcvQueue.setNewEntry(this);
}

      // Functionality:
      //    Close the opened UDT entity.
      // Parameters:
      //    None.
      // Returned value:
      //    None.

public void close()
{
   if (!m_bOpened)
      return;

   if (!m_bConnected)
      m_bClosing = true;

   if (0 != m_Linger.l_onoff)
   {
      Int64 entertime = CClock.getTime();

      while (!m_bBroken && m_bConnected && (m_pSndBuffer.getCurrBufSize() > 0) && (CClock.getTime() - entertime < m_Linger.l_linger * 1000000))
      {
            Thread.Sleep(1);
      }
   }

   // remove this socket from the snd queue
   if (m_bConnected)
      m_pSndQueue.m_pSndUList.remove(this);

   CGuard cg = new CGuard(m_ConnectionLock);

   // Inform the threads handler to stop.
   m_bClosing = true;

   // Signal the sender and recver if they are waiting for data.
   releaseSynch();

   if (m_bListening)
   {
      m_bListening = false;
      m_pRcvQueue.removeListener(this);
   }
   if (m_bConnected)
   {
      if (!m_bShutdown)
         sendCtrl(5);

      m_pCC.close();

      CInfoBlock ib;
      ib.m_iRTT = m_iRTT;
      ib.m_iBandwidth = m_iBandwidth;
      m_pCache.update(m_pPeerAddr, m_iIPversion, &ib);

      m_bConnected = false;
   }

   // waiting all send and recv calls to stop
   CGuard sendguard = new CGuard(m_SendLock);
   CGuard recvguard = new CGuard(m_RecvLock);

   // CLOSED.
   m_bOpened = false;
}
        
        // Functionality:
      //    Request UDT to send out a data block "data" with size of "len".
      // Parameters:
      //    0) [in] data: The address of the application data to be sent.
      //    1) [in] len: The size of the data block.
      // Returned value:
      //    Actual size of data sent.

public int send(byte[] data, int len)
{
   if (UDT_DGRAM == m_iSockType)
      throw new CUDTException(5, 10, 0);

   // throw an exception if not connected
   if (m_bBroken || m_bClosing)
      throw new CUDTException(2, 1, 0);
   else if (!m_bConnected)
      throw new CUDTException(2, 2, 0);

   if (len <= 0)
      return 0;

   CGuard sendguard = new CGuard(m_SendLock);

   if (m_iSndBufSize <= m_pSndBuffer.getCurrBufSize())
   {
      if (!m_bSynSending)
         throw new CUDTException(6, 1, 0);
      else
      {
         // wait here during a blocking sending
            if (m_iSndTimeOut < 0)
            {
               while (!m_bBroken && m_bConnected && !m_bClosing && (m_iSndBufSize <= m_pSndBuffer.getCurrBufSize()))
                  WaitForSingleObject(m_SendBlockCond, INFINITE);
            }
            else 
               WaitForSingleObject(m_SendBlockCond, DWORD(m_iSndTimeOut)); 

         // check the connection status
         if (m_bBroken || m_bClosing)
            throw new CUDTException(2, 1, 0);
         else if (!m_bConnected)
            throw new CUDTException(2, 2, 0);
      }
   }

   if (m_iSndBufSize <= m_pSndBuffer.getCurrBufSize())
      return 0; 

   int size = (m_iSndBufSize - m_pSndBuffer.getCurrBufSize()) * m_iPayloadSize;
   if (size > len)
      size = len;

   // record total time used for sending
   if (0 == m_pSndBuffer.getCurrBufSize())
      m_llSndDurationCounter = CClock.getTime();

   // insert the user buffer into the sening list
   m_pSndBuffer.addBuffer(data, size);

   // insert this socket to snd list if it is not on the list yet
   m_pSndQueue.m_pSndUList.update(this, false);

   return size;
}

      // Functionality:
      //    Request UDT to receive data to a memory block "data" with size of "len".
      // Parameters:
      //    0) [out] data: data received.
      //    1) [in] len: The desired size of data to be received.
      // Returned value:
      //    Actual size of data received.

public int recv(byte[] data, int len)
{
   if (UDT_DGRAM == m_iSockType)
      throw new CUDTException(5, 10, 0);

   // throw an exception if not connected
   if (!m_bConnected)
      throw new CUDTException(2, 2, 0);
   else if ((m_bBroken || m_bClosing) && (0 == m_pRcvBuffer.getRcvDataSize()))
      throw new CUDTException(2, 1, 0);

   if (len <= 0)
      return 0;

   CGuard recvguard = new CGuard(m_RecvLock);

   if (0 == m_pRcvBuffer.getRcvDataSize())
   {
      if (!m_bSynRecving)
         throw new CUDTException(6, 2, 0);
      else
      {

            if (m_iRcvTimeOut < 0)
            {
               while (!m_bBroken && m_bConnected && !m_bClosing && (0 == m_pRcvBuffer.getRcvDataSize()))
                  WaitForSingleObject(m_RecvDataCond, INFINITE);
            }
            else
            {
               Int64 enter_time = CClock.getTime();

               while (!m_bBroken && m_bConnected && !m_bClosing && (0 == m_pRcvBuffer.getRcvDataSize()))
               {
                  int diff = (int)(CClock.getTime() - enter_time) / 1000;
                  if (diff >= m_iRcvTimeOut)
                      break;
                  WaitForSingleObject(m_RecvDataCond, DWORD(m_iRcvTimeOut - diff ));
               }
            }
      }
   }

   // throw an exception if not connected
   if (!m_bConnected)
      throw new CUDTException(2, 2, 0);
   else if ((m_bBroken || m_bClosing) && (0 == m_pRcvBuffer.getRcvDataSize()))
      throw new CUDTException(2, 1, 0);

   return m_pRcvBuffer.readBuffer(data, len);
}

      // Functionality:
      //    send a message of a memory block "data" with size of "len".
      // Parameters:
      //    0) [out] data: data received.
      //    1) [in] len: The desired size of data to be received.
      //    2) [in] ttl: the time-to-live of the message.
      //    3) [in] inorder: if the message should be delivered in order.
      // Returned value:
      //    Actual size of data sent.

public        int sendmsg(byte[] data, int len, int msttl, bool inorder)
{
   if (UDT_STREAM == m_iSockType)
      throw new CUDTException(5, 9, 0);

   // throw an exception if not connected
   if (m_bBroken || m_bClosing)
      throw new CUDTException(2, 1, 0);
   else if (!m_bConnected)
      throw new CUDTException(2, 2, 0);

   if (len <= 0)
      return 0;

   if (len > m_iSndBufSize * m_iPayloadSize)
      throw new CUDTException(5, 12, 0);

   CGuard sendguard = new CGuard(m_SendLock);

   if ((m_iSndBufSize - m_pSndBuffer.getCurrBufSize()) * m_iPayloadSize < len)
   {
      if (!m_bSynSending)
         throw new CUDTException(6, 1, 0);
      else
      {
         // wait here during a blocking sending

            if (m_iSndTimeOut < 0)
            {
               while (!m_bBroken && m_bConnected && !m_bClosing && ((m_iSndBufSize - m_pSndBuffer.getCurrBufSize()) * m_iPayloadSize < len))
                  WaitForSingleObject(m_SendBlockCond, INFINITE);
            }
            else
               WaitForSingleObject(m_SendBlockCond, DWORD(m_iSndTimeOut));

         // check the connection status
         if (m_bBroken || m_bClosing)
            throw new CUDTException(2, 1, 0);
         else if (!m_bConnected)
            throw new CUDTException(2, 2, 0);
      }
   }

   if ((m_iSndBufSize - m_pSndBuffer.getCurrBufSize()) * m_iPayloadSize < len)
      return 0;

   // record total time used for sending
   if (0 == m_pSndBuffer.getCurrBufSize())
      m_llSndDurationCounter = CClock.getTime();

   // insert the user buffer into the sening list
   m_pSndBuffer.addBuffer(data, len, msttl, inorder);

   // insert this socket to the snd list if it is not on the list yet
   m_pSndQueue.m_pSndUList.update(this, false);

   return len;   
}

      // Functionality:
      //    Receive a message to buffer "data".
      // Parameters:
      //    0) [out] data: data received.
      //    1) [in] len: size of the buffer.
      // Returned value:
      //    Actual size of data received.

        public int recvmsg(byte[] data, int len)
{
   if (UDT_STREAM == m_iSockType)
      throw new CUDTException(5, 9, 0);

   // throw an exception if not connected
   if (!m_bConnected)
      throw new CUDTException(2, 2, 0);

   if (len <= 0)
      return 0;

   CGuard recvguard = new CGuard(m_RecvLock);

   if (m_bBroken || m_bClosing)
   {
      int res = m_pRcvBuffer.readMsg(data, len);
      if (0 == res)
         throw new CUDTException(2, 1, 0);
      else
         return res;
   }

   if (!m_bSynRecving)
   {
      int res = m_pRcvBuffer.readMsg(data, len);
      if (0 == res)
         throw new CUDTException(6, 2, 0);
      else
         return res;
   }

   int res = 0;
   bool timeout = false;

   do
   {

         if (m_iRcvTimeOut < 0)
         {
            while (!m_bBroken && m_bConnected && !m_bClosing && (0 == (res = m_pRcvBuffer.readMsg(data, len))))
               WaitForSingleObject(m_RecvDataCond, INFINITE);
         }
         else
         {
            if (WaitForSingleObject(m_RecvDataCond, DWORD(m_iRcvTimeOut)) == WAIT_TIMEOUT)
               timeout = true;

            res = m_pRcvBuffer.readMsg(data, len);
         }

      if (m_bBroken || m_bClosing)
         throw new CUDTException(2, 1, 0);
      else if (!m_bConnected)
         throw new CUDTException(2, 2, 0);
   } while ((0 == res) && !timeout);

   return res;
}

      // Functionality:
      //    Request UDT to send out a file described as "fd", starting from "offset", with size of "size".
      // Parameters:
      //    0) [in] ifs: The input file stream.
      //    1) [in] offset: From where to read and send data;
      //    2) [in] size: How many data to be sent.
      //    3) [in] block: size of block per read from disk
      // Returned value:
      //    Actual size of data sent.

        public Int64 sendfile(Stream ifs, Int64 offset, Int64 size, int block)
{
   if (UDT_DGRAM == m_iSockType)
      throw new CUDTException(5, 10, 0);

   if (m_bBroken || m_bClosing)
      throw new CUDTException(2, 1, 0);
   else if (!m_bConnected)
      throw new CUDTException(2, 2, 0);

   if (size <= 0)
      return 0;

   CGuard sendguard = new CGuard(m_SendLock);

   long tosend = size;
   int unitsize;

   // positioning...
   try
   {
      ifs.seekg((streamoff)offset);
   }
   catch (Exception e)
   {
      throw new CUDTException(4, 1);
   }

   // sending block by block
   while (tosend > 0)
   {
      if (ifs.bad() || ifs.fail() || ifs.eof())
         break;

      unitsize = (int)((tosend >= block) ? block : tosend);

         while (!m_bBroken && m_bConnected && !m_bClosing && (m_iSndBufSize <= m_pSndBuffer.getCurrBufSize()))
            WaitForSingleObject(m_SendBlockCond, INFINITE);

      if (m_bBroken || m_bClosing)
         throw new CUDTException(2, 1, 0);
      else if (!m_bConnected)
         throw new CUDTException(2, 2, 0);

      // record total time used for sending
      if (0 == m_pSndBuffer.getCurrBufSize())
         m_llSndDurationCounter = CClock.getTime();

      tosend -= m_pSndBuffer.addBufferFromFile(ifs, unitsize);

      // insert this socket to snd list if it is not on the list yet
      m_pSndQueue.m_pSndUList.update(this, false);
   }

   return size - tosend;
}

      // Functionality:
      //    Request UDT to receive data into a file described as "fd", starting from "offset", with expected size of "size".
      // Parameters:
      //    0) [out] ofs: The output file stream.
      //    1) [in] offset: From where to write data;
      //    2) [in] size: How many data to be received.
      //    3) [in] block: size of block per write to disk
      // Returned value:
      //    Actual size of data received.

public Int64 recvfile(Stream ofs, Int64 offset, Int64 size, int block)
{
   if (UDT_DGRAM == m_iSockType)
      throw new CUDTException(5, 10, 0);

   if (!m_bConnected)
      throw new CUDTException(2, 2, 0);
   else if ((m_bBroken || m_bClosing) && (0 == m_pRcvBuffer.getRcvDataSize()))
      throw new CUDTException(2, 1, 0);

   if (size <= 0)
      return 0;

   CGuard recvguard = new CGuard(m_RecvLock);

   long torecv = size;
   int unitsize = block;
   int recvsize;

   // positioning...
   try
   {
      ofs.seekp((streamoff)offset);
   }
   catch (Exception e)
   {
      throw new CUDTException(4, 3);
   }

   // receiving... "recvfile" is always blocking
   while (torecv > 0)
   {
      if (ofs.bad() || ofs.fail())
         break;

         while (!m_bBroken && m_bConnected && !m_bClosing && (0 == m_pRcvBuffer.getRcvDataSize()))
             m_RecvDataCond.WaitOne(-1);

      if (!m_bConnected)
         throw new CUDTException(2, 2, 0);
      else if ((m_bBroken || m_bClosing) && (0 == m_pRcvBuffer.getRcvDataSize()))
         throw new CUDTException(2, 1, 0);

      unitsize = (int)((torecv >= block) ? block : torecv);
      recvsize = m_pRcvBuffer.readBufferToFile(ofs, unitsize);

      torecv -= recvsize;
   }

   return size - torecv;
}

      // Functionality:
      //    Configure UDT options.
      // Parameters:
      //    0) [in] optName: The enum name of a UDT option.
      //    1) [in] optval: The value to be set.
      //    2) [in] optlen: size of "optval".
      // Returned value:
      //    None.

void setOpt(UDTOpt optName, Object optval, int optlen)
{
   CGuard cg = new CGuard(m_ConnectionLock);
   CGuard sendguard = new CGuard(m_SendLock);
   CGuard recvguard = new CGuard(m_RecvLock);

   switch (optName)
   {
   case UDTOpt.UDT_MSS:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);

      if ((int)optval < (int)(28 + sizeof(CHandShake)))
         throw new CUDTException(5, 3, 0);

      m_iMSS = (int)optval;

      // Packet size cannot be greater than UDP buffer size
      if (m_iMSS > m_iUDPSndBufSize)
         m_iMSS = m_iUDPSndBufSize;
      if (m_iMSS > m_iUDPRcvBufSize)
         m_iMSS = m_iUDPRcvBufSize;

      break;

   case UDTOpt.UDT_SNDSYN:
      m_bSynSending = (bool)optval;
      break;

   case UDTOpt.UDT_RCVSYN:
      m_bSynRecving = (bool)optval;
      break;

   case UDTOpt.UDT_CC:
      if (m_bConnected)
         throw new CUDTException(5, 1, 0);
      //if (null != m_pCCFactory)
      //   delete m_pCCFactory;
      m_pCCFactory = ((CCCVirtualFactory )optval).clone();

      break;

   case UDTOpt.UDT_FC:
      if (m_bConnected)
         throw new CUDTException(5, 2, 0);

      if (*(int*)optval < 1)
         throw new CUDTException(5, 3);

      // Mimimum recv flight flag size is 32 packets
      if (*(int*)optval > 32)
         m_iFlightFlagSize = *(int*)optval;
      else
         m_iFlightFlagSize = 32;

      break;

   case UDTOpt.UDT_SNDBUF:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);

      if (*(int*)optval <= 0)
         throw new CUDTException(5, 3, 0);

      m_iSndBufSize = *(int*)optval / (m_iMSS - 28);

      break;

   case UDTOpt.UDT_RCVBUF:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);

      if (*(int*)optval <= 0)
         throw new CUDTException(5, 3, 0);

      // Mimimum recv buffer size is 32 packets
      if (*(int*)optval > (m_iMSS - 28) * 32)
         m_iRcvBufSize = *(int*)optval / (m_iMSS - 28);
      else
         m_iRcvBufSize = 32;

      // recv buffer MUST not be greater than FC size
      if (m_iRcvBufSize > m_iFlightFlagSize)
         m_iRcvBufSize = m_iFlightFlagSize;

      break;

   case UDTOpt.UDT_LINGER:
      m_Linger = *(linger*)optval;
      break;

   case UDTOpt.UDP_SNDBUF:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);

      m_iUDPSndBufSize = *(int*)optval;

      if (m_iUDPSndBufSize < m_iMSS)
         m_iUDPSndBufSize = m_iMSS;

      break;

   case UDTOpt.UDP_RCVBUF:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);

      m_iUDPRcvBufSize = *(int*)optval;

      if (m_iUDPRcvBufSize < m_iMSS)
         m_iUDPRcvBufSize = m_iMSS;

      break;

   case UDTOpt.UDT_RENDEZVOUS:
      if (m_bConnected)
         throw new CUDTException(5, 1, 0);
      m_bRendezvous = *(bool *)optval;
      break;

   case UDTOpt.UDT_SNDTIMEO: 
      m_iSndTimeOut = *(int*)optval; 
      break; 
    
   case UDTOpt.UDT_RCVTIMEO: 
      m_iRcvTimeOut = *(int*)optval; 
      break; 

   case UDTOpt.UDT_REUSEADDR:
      if (m_bOpened)
         throw new CUDTException(5, 1, 0);
      m_bReuseAddr = *(bool*)optval;
      break;

   case UDTOpt.UDT_MAXBW:
      if (m_bConnected)
         throw new CUDTException(5, 1, 0);
      m_llMaxBW = *(Int64*)optval;
      break;
    
   default:
      throw new CUDTException(5, 0, 0);
   }
}

      // Functionality:
      //    Read UDT options.
      // Parameters:
      //    0) [in] optName: The enum name of a UDT option.
      //    1) [in] optval: The value to be returned.
      //    2) [out] optlen: size of "optval".
      // Returned value:
      //    None.

void getOpt(UDTOpt optName, Object optval, int optlen)
{
   CGuard cg = new CGuard(m_ConnectionLock);

   switch (optName)
   {
   case UDT_MSS:
      *(int*)optval = m_iMSS;
      optlen = sizeof(int);
      break;

   case UDT_SNDSYN:
      *(bool*)optval = m_bSynSending;
      optlen = sizeof(bool);
      break;

   case UDT_RCVSYN:
      *(bool*)optval = m_bSynRecving;
      optlen = sizeof(bool);
      break;

   case UDT_CC:
      if (!m_bOpened)
         throw new CUDTException(5, 5, 0);
      *(CCC**)optval = m_pCC;
      optlen = sizeof(CCC*);

      break;

   case UDT_FC:
      *(int*)optval = m_iFlightFlagSize;
      optlen = sizeof(int);
      break;

   case UDT_SNDBUF:
      *(int*)optval = m_iSndBufSize * (m_iMSS - 28);
      optlen = sizeof(int);
      break;

   case UDT_RCVBUF:
      *(int*)optval = m_iRcvBufSize * (m_iMSS - 28);
      optlen = sizeof(int);
      break;

   case UDT_LINGER:
      if (optlen < (int)(sizeof(linger)))
         throw new CUDTException(5, 3, 0);

      *(linger*)optval = m_Linger;
      optlen = sizeof(linger);
      break;

   case UDP_SNDBUF:
      *(int*)optval = m_iUDPSndBufSize;
      optlen = sizeof(int);
      break;

   case UDP_RCVBUF:
      *(int*)optval = m_iUDPRcvBufSize;
      optlen = sizeof(int);
      break;

   case UDT_RENDEZVOUS:
      *(bool *)optval = m_bRendezvous;
      optlen = sizeof(bool);
      break;

   case UDT_SNDTIMEO: 
      *(int*)optval = m_iSndTimeOut; 
      optlen = sizeof(int); 
      break; 
    
   case UDT_RCVTIMEO: 
      *(int*)optval = m_iRcvTimeOut; 
      optlen = sizeof(int); 
      break; 

   case UDT_REUSEADDR:
      *(bool *)optval = m_bReuseAddr;
      optlen = sizeof(bool);
      break;

   case UDT_MAXBW:
      *(Int64*)optval = m_llMaxBW;
      break;

   default:
      throw new CUDTException(5, 0, 0);
   }
}

      // Functionality:
      //    read the performance data since last sample() call.
      // Parameters:
      //    0) [in, out] perf: pointer to a CPerfMon structure to record the performance data.
      //    1) [in] clear: flag to decide if the local performance trace should be cleared.
      // Returned value:
      //    None.

        public void sample(CPerfMon perf)
        {
            sample(perf, true);
        }

        public void sample(CPerfMon perf, bool clear)
{
   if (!m_bConnected)
      throw new CUDTException(2, 2, 0);
   if (m_bBroken || m_bClosing)
      throw new CUDTException(2, 1, 0);

   Int64 currtime = CClock.getTime();
   perf.msTimeStamp = (currtime - m_StartTime) / 1000;

   perf.pktSent = m_llTraceSent;
   perf.pktRecv = m_llTraceRecv;
   perf.pktSndLoss = m_iTraceSndLoss;
   perf.pktRcvLoss = m_iTraceRcvLoss;
   perf.pktRetrans = m_iTraceRetrans;
   perf.pktSentACK = m_iSentACK;
   perf.pktRecvACK = m_iRecvACK;
   perf.pktSentNAK = m_iSentNAK;
   perf.pktRecvNAK = m_iRecvNAK;
   perf.usSndDuration = m_llSndDuration;

   perf.pktSentTotal = m_llSentTotal;
   perf.pktRecvTotal = m_llRecvTotal;
   perf.pktSndLossTotal = m_iSndLossTotal;
   perf.pktRcvLossTotal = m_iRcvLossTotal;
   perf.pktRetransTotal = m_iRetransTotal;
   perf.pktSentACKTotal = m_iSentACKTotal;
   perf.pktRecvACKTotal = m_iRecvACKTotal;
   perf.pktSentNAKTotal = m_iSentNAKTotal;
   perf.pktRecvNAKTotal = m_iRecvNAKTotal;
   perf.usSndDurationTotal = m_llSndDurationTotal;

   double interval = (double)(currtime - m_LastSampleTime);

   perf.mbpsSendRate = (double)(m_llTraceSent) * m_iPayloadSize * 8.0 / interval;
   perf.mbpsRecvRate = (double)(m_llTraceRecv) * m_iPayloadSize * 8.0 / interval;

   perf.usPktSndPeriod = m_ullInterval / (double)(m_ullCPUFrequency);
   perf.pktFlowWindow = m_iFlowWindowSize;
   perf.pktCongestionWindow = (int)m_dCongestionWindow;
   perf.pktFlightSize = CSeqNo.seqlen((Int32)(m_iSndLastAck), (Int32)(m_iSndCurrSeqNo));
   perf.msRTT = m_iRTT/1000.0;
   perf.mbpsBandwidth = m_iBandwidth * m_iPayloadSize * 8.0 / 1000000.0;

      if (WAIT_OBJECT_0 == WaitForSingleObject(m_ConnectionLock, 0))
   {
      perf.byteAvailSndBuf = (null == m_pSndBuffer) ? 0 : (m_iSndBufSize - m_pSndBuffer.getCurrBufSize()) * m_iMSS;
      perf.byteAvailRcvBuf = (null == m_pRcvBuffer) ? 0 : m_pRcvBuffer.getAvailBufSize() * m_iMSS;

          m_ConnectionLock.ReleaseMutex();
   }
   else
   {
      perf.byteAvailSndBuf = 0;
      perf.byteAvailRcvBuf = 0;
   }

   if (clear)
   {
      m_llTraceSent = m_llTraceRecv = m_iTraceSndLoss = m_iTraceSndLoss = m_iTraceRetrans = m_iSentACK = m_iRecvACK = m_iSentNAK = m_iRecvNAK = 0;
      m_llSndDuration = 0;
      m_LastSampleTime = currtime;
   }
}



void initSynch()
{
    m_SendBlockLock = new Mutex();
      m_SendBlockCond = new AutoResetEvent(false);
      m_RecvDataLock = new Mutex();
      m_RecvDataCond = new AutoResetEvent(false);
      m_SendLock = new Mutex();
      m_RecvLock = new Mutex();
      m_AckLock = new Mutex();
      m_ConnectionLock = new Mutex();
}
        
void destroySynch()
{
      CloseHandle(m_SendBlockLock);
      CloseHandle(m_SendBlockCond);
      CloseHandle(m_RecvDataLock);
      CloseHandle(m_RecvDataCond);
      CloseHandle(m_SendLock);
      CloseHandle(m_RecvLock);
      CloseHandle(m_AckLock);
      CloseHandle(m_ConnectionLock);
}
        
void releaseSynch()
{
      SetEvent(m_SendBlockCond);
      WaitForSingleObject(m_SendLock, INFINITE);
      ReleaseMutex(m_SendLock);
      SetEvent(m_RecvDataCond);
      WaitForSingleObject(m_RecvLock, INFINITE);
      ReleaseMutex(m_RecvLock);
}

#region Generation and processing of packets
void sendCtrl(int pkttype, IntPtr lparam, IntPtr rparam, int size)
{
   CPacket ctrlpkt;

   switch (pkttype)
   {
   case 2: //010 - Acknowledgement
      {
      Int32 ack;

      // If there is no loss, the ACK is the current largest sequence number plus 1;
      // Otherwise it is the smallest sequence number in the receiver loss list.
      if (0 == m_pRcvLossList.getLossLength())
         ack = CSeqNo.incseq(m_iRcvCurrSeqNo);
      else
         ack = m_pRcvLossList.getFirstLostSeq();

      if (ack == m_iRcvLastAckAck)
         break;

      // send out a lite ACK
      // to save time on buffer processing and bandwidth/AS measurement, a lite ACK only feeds back an ACK number
      if (4 == size)
      {
         ctrlpkt.pack(2, null, &ack, size);
         ctrlpkt.m_iID = m_PeerID;
         m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

         break;
      }

      Int64 currtime;
      CClock.rdtsc(out currtime);

      // There are new received packets to acknowledge, update related information.
      if (CSeqNo.seqcmp(ack, m_iRcvLastAck) > 0)
      {
         int acksize = CSeqNo.seqoff(m_iRcvLastAck, ack);

         m_iRcvLastAck = ack;

         m_pRcvBuffer.ackData(acksize);

         // signal a waiting "recv" call if there is any data available
            if (m_bSynRecving)
               SetEvent(m_RecvDataCond);
      }
      else if (ack == m_iRcvLastAck)
      {
         if ((currtime - m_ullLastAckTime) < ((m_iRTT + 4 * m_iRTTVar) * m_ullCPUFrequency))
            break;
      }
      else
         break;

      // Send out the ACK only if has not been received by the sender before
      if (CSeqNo.seqcmp(m_iRcvLastAck, m_iRcvLastAckAck) > 0)
      {
         Int32[] data  = new int[6];

         m_iAckSeqNo = CAckNo.incack(m_iAckSeqNo);
         data[0] = m_iRcvLastAck;
         data[1] = m_iRTT;
         data[2] = m_iRTTVar;
         data[3] = m_pRcvBuffer.getAvailBufSize();
         // a minimum flow window of 2 is used, even if buffer is full, to break potential deadlock
         if (data[3] < 2)
            data[3] = 2;

         if (currtime - m_ullLastAckTime > m_ullSYNInt)
         {
            data[4] = m_pRcvTimeWindow.getPktRcvSpeed();
            data[5] = m_pRcvTimeWindow.getBandwidth();
            ctrlpkt.pack(2, &m_iAckSeqNo, data, 24);

            CClock.rdtsc(m_ullLastAckTime);
         }
         else
         {
            ctrlpkt.pack(2, &m_iAckSeqNo, data, 16);
         }

         ctrlpkt.m_iID = m_PeerID;
         m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

         m_pACKWindow.store(m_iAckSeqNo, m_iRcvLastAck);

         ++ m_iSentACK;
         ++ m_iSentACKTotal;
      }

      break;
      }

   case 6: //110 - Acknowledgement of Acknowledgement
      ctrlpkt.pack(6, lparam);
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

      break;

   case 3: //011 - Loss Report
      if (null != rparam)
      {
         if (1 == size)
         {
            // only 1 loss packet
            ctrlpkt.pack(3, null, (Int32 *)rparam + 1, 4);
         }
         else
         {
            // more than 1 loss packets
            ctrlpkt.pack(3, null, rparam, 8);
         }

         ctrlpkt.m_iID = m_PeerID;
         m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

         ++ m_iSentNAK;
         ++ m_iSentNAKTotal;
      }
      else if (m_pRcvLossList.getLossLength() > 0)
      {
         // this is periodically NAK report

         // read loss list from the local receiver loss list
         Int32* data = new Int32[m_iPayloadSize / 4];
         int losslen;
         m_pRcvLossList.getLossArray(data, losslen, m_iPayloadSize / 4, m_iRTT + 4 * m_iRTTVar);

         if (0 < losslen)
         {
            ctrlpkt.pack(3, null, data, losslen * 4);
            ctrlpkt.m_iID = m_PeerID;
            m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

            ++ m_iSentNAK;
            ++ m_iSentNAKTotal;
         }

         delete [] data;
      }

      break;

   case 4: //100 - Congestion Warning
      ctrlpkt.pack(4);
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

      CClock.rdtsc(m_ullLastWarningTime);

      break;

   case 1: //001 - Keep-alive
      ctrlpkt.pack(1);
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);
 
      break;

   case 0: //000 - Handshake
      ctrlpkt.pack(0, null, rparam, sizeof(CHandShake));
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

      break;

   case 5: //101 - Shutdown
      ctrlpkt.pack(5);
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

      break;

   case 7: //111 - Msg drop request
      ctrlpkt.pack(7, lparam, rparam, 8);
      ctrlpkt.m_iID = m_PeerID;
      m_pSndQueue.sendto(m_pPeerAddr, ctrlpkt);

      break;

   case 32767: //0x7FFF - Resevered for future use
      break;

   default:
      break;
   }
}

void processCtrl(CPacket ctrlpkt)
{
   // Just heard from the peer, reset the expiration count.
   m_iEXPCount = 1;
   if ((CSeqNo.incseq(m_iSndCurrSeqNo) == m_iSndLastAck) || (2 == ctrlpkt.getType()) || (3 == ctrlpkt.getType()))
   {
      m_ullEXPInt = m_ullMinEXPInt;
      CClock.rdtsc(out m_ullNextEXPTime);
      m_ullNextEXPTime += m_ullEXPInt;
   }

   switch (ctrlpkt.getType())
   {
   case 2: //010 - Acknowledgement
      {
      Int32 ack;

      // process a lite ACK
      if (4 == ctrlpkt.getLength())
      {
         ack = *(Int32 *)ctrlpkt.m_pcData;
         if (CSeqNo.seqcmp(ack, (Int32)(m_iSndLastAck)) >= 0)
         {
            m_iFlowWindowSize -= CSeqNo.seqoff((Int32)(m_iSndLastAck), ack);
            m_iSndLastAck = ack;
         }

         break;
      }

       // read ACK seq. no.
      ack = ctrlpkt.getAckSeqNo();

      // send ACK acknowledgement
      // ACK2 can be much less than ACK
      Int64 currtime = CClock.getTime();
      if ((currtime - m_ullSndLastAck2Time > (Int64)m_iSYNInterval) || (ack == m_iSndLastAck2))
      {
         sendCtrl(6, &ack);
         m_iSndLastAck2 = ack;
         m_ullSndLastAck2Time = currtime;
      }

      // Got data ACK
      ack = *(Int32 *)ctrlpkt.m_pcData;

      // check the validation of the ack
      if (CSeqNo.seqcmp(ack, CSeqNo.incseq(m_iSndCurrSeqNo)) > 0)
      {
         //this should not happen: attack or bug
         m_bBroken = true;
         m_iBrokenCounter = 0;
         break;
      }

      if (CSeqNo.seqcmp(ack, (Int32)(m_iSndLastAck)) >= 0)
      {
         // Update Flow Window Size, must update before and together with m_iSndLastAck
         m_iFlowWindowSize = *((Int32 *)ctrlpkt.m_pcData + 3);
         m_iSndLastAck = ack;
      }

      // protect packet retransmission
      CGuard.enterCS(m_AckLock);

      int offset = CSeqNo.seqoff((Int32)m_iSndLastDataAck, ack);
      if (offset <= 0)
      {
         // discard it if it is a repeated ACK
         CGuard.leaveCS(m_AckLock);
         break;
      }

      // acknowledge the sending buffer
      m_pSndBuffer.ackData(offset);

      // record total time used for sending
      m_llSndDuration += currtime - m_llSndDurationCounter;
      m_llSndDurationTotal += currtime - m_llSndDurationCounter;
      m_llSndDurationCounter = currtime;

      // update sending variables
      m_iSndLastDataAck = ack;
      m_pSndLossList.remove(CSeqNo.decseq((Int32)m_iSndLastDataAck));

      CGuard.leaveCS(m_AckLock);

         if (m_bSynSending)
            SetEvent(m_SendBlockCond);

      // insert this socket to snd list if it is not on the list yet
      m_pSndQueue.m_pSndUList.update(this, false);

      // Update RTT
      //m_iRTT = *((Int32 *)ctrlpkt.m_pcData + 1);
      //m_iRTTVar = *((Int32 *)ctrlpkt.m_pcData + 2);
      int rtt = *((Int32 *)ctrlpkt.m_pcData + 1);
      m_iRTTVar = (m_iRTTVar * 3 + abs(rtt - m_iRTT)) >> 2;
      m_iRTT = (m_iRTT * 7 + rtt) >> 3;

      m_pCC.setRTT(m_iRTT);

      m_ullMinEXPInt = (m_iRTT + 4 * m_iRTTVar) * m_ullCPUFrequency + m_ullSYNInt;
      if (m_ullMinEXPInt < 100000 * m_ullCPUFrequency)
          m_ullMinEXPInt = 100000 * m_ullCPUFrequency;

      if (ctrlpkt.getLength() > 16)
      {
         // Update Estimated Bandwidth and packet delivery rate
         if (*((Int32 *)ctrlpkt.m_pcData + 4) > 0)
            m_iDeliveryRate = (m_iDeliveryRate * 7 + *((Int32 *)ctrlpkt.m_pcData + 4)) >> 3;

         if (*((Int32 *)ctrlpkt.m_pcData + 5) > 0)
            m_iBandwidth = (m_iBandwidth * 7 + *((Int32 *)ctrlpkt.m_pcData + 5)) >> 3;

         m_pCC.setRcvRate(m_iDeliveryRate);
         m_pCC.setBandwidth(m_iBandwidth);
      }

      m_pCC.onACK(ack);
      // update CC parameters
      m_ullInterval = (Int64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
      m_dCongestionWindow = m_pCC.m_dCWndSize;

      ++ m_iRecvACK;
      ++ m_iRecvACKTotal;

      break;
      }

   case 6: //110 - Acknowledgement of Acknowledgement
      {
      Int32 ack;
      int rtt = -1;

      // update RTT
      rtt = m_pACKWindow.acknowledge(ctrlpkt.getAckSeqNo(), ack);

      if (rtt <= 0)
         break;

      //if increasing delay detected...
      //   sendCtrl(4);

      // RTT EWMA
      m_iRTTVar = (m_iRTTVar * 3 + abs(rtt - m_iRTT)) >> 2;
      m_iRTT = (m_iRTT * 7 + rtt) >> 3;

      m_pCC.setRTT(m_iRTT);

      m_ullMinEXPInt = (m_iRTT + 4 * m_iRTTVar) * m_ullCPUFrequency + m_ullSYNInt;
      if (m_ullMinEXPInt < 100000 * m_ullCPUFrequency)
          m_ullMinEXPInt = 100000 * m_ullCPUFrequency;

      // update last ACK that has been received by the sender
      if (CSeqNo.seqcmp(ack, m_iRcvLastAckAck) > 0)
         m_iRcvLastAckAck = ack;

      break;
      }

   case 3: //011 - Loss Report
      {
      Int32* losslist = (Int32 *)(ctrlpkt.m_pcData);

      m_pCC.onLoss(losslist, ctrlpkt.getLength() / 4);
      // update CC parameters
      m_ullInterval = (Int64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
      m_dCongestionWindow = m_pCC.m_dCWndSize;

      bool secure = true;

      // decode loss list message and insert loss into the sender loss list
      for (int i = 0, n = (int)(ctrlpkt.getLength() / 4); i < n; ++ i)
      {
         if (0 != (losslist[i] & 0x80000000))
         {
            if ((CSeqNo.seqcmp(losslist[i] & 0x7FFFFFFF, losslist[i + 1]) > 0) || (CSeqNo.seqcmp(losslist[i + 1], (Int32)(m_iSndCurrSeqNo)) > 0))
            {
               // seq_a must not be greater than seq_b; seq_b must not be greater than the most recent sent seq
               secure = false;
               break;
            }

            int num = 0;
            if (CSeqNo.seqcmp(losslist[i] & 0x7FFFFFFF, (Int32)(m_iSndLastAck)) >= 0)
               num = m_pSndLossList.insert(losslist[i] & 0x7FFFFFFF, losslist[i + 1]);
            else if (CSeqNo.seqcmp(losslist[i + 1], (Int32)(m_iSndLastAck)) >= 0)
               num = m_pSndLossList.insert((Int32)(m_iSndLastAck), losslist[i + 1]);

            m_iTraceSndLoss += num;
            m_iSndLossTotal += num;

            ++ i;
         }
         else if (CSeqNo.seqcmp(losslist[i], (Int32)(m_iSndLastAck)) >= 0)
         {
            if (CSeqNo.seqcmp(losslist[i], (Int32)(m_iSndCurrSeqNo)) > 0)
            {
               //seq_a must not be greater than the most recent sent seq
               secure = false;
               break;
            }

            int num = m_pSndLossList.insert(losslist[i], losslist[i]);

            m_iTraceSndLoss += num;
            m_iSndLossTotal += num;
         }
      }

      if (!secure)
      {
         //this should not happen: attack or bug
         m_bBroken = true;
         m_iBrokenCounter = 0;
         break;
      }

      // the lost packet (retransmission) should be sent out immediately
      m_pSndQueue.m_pSndUList.update(this);

      ++ m_iRecvNAK;
      ++ m_iRecvNAKTotal;

      break;
      }

   case 4: //100 - Delay Warning
      // One way packet delay is increasing, so decrease the sending rate
      m_ullInterval = (Int64)ceil(m_ullInterval * 1.125);
      m_iLastDecSeq = m_iSndCurrSeqNo;

      break;

   case 1: //001 - Keep-alive
      // The only purpose of keep-alive packet is to tell that the peer is still alive
      // nothing needs to be done.

      break;

   case 0: //000 - Handshake
      if ((((CHandShake*)(ctrlpkt.m_pcData)).m_iReqType > 0) || (m_bRendezvous && (((CHandShake*)(ctrlpkt.m_pcData)).m_iReqType != -2)))
      {
         // The peer side has not received the handshake message, so it keeps querying
         // resend the handshake packet

         CHandShake initdata;
         initdata.m_iISN = m_iISN;
         initdata.m_iMSS = m_iMSS;
         initdata.m_iFlightFlagSize = m_iFlightFlagSize;
         initdata.m_iReqType = (!m_bRendezvous) ? -1 : -2;
         initdata.m_iID = m_SocketID;
         sendCtrl(0, null, (char *)&initdata, sizeof(CHandShake));
      }

      break;

   case 5: //101 - Shutdown
      m_bShutdown = true;
      m_bClosing = true;
      m_bBroken = true;
      m_iBrokenCounter = 60;

      // Signal the sender and recver if they are waiting for data.
      releaseSynch();

      CClock.triggerEvent();

      break;

   case 7: //111 - Msg drop request
      m_pRcvBuffer.dropMsg(ctrlpkt.getMsgSeq());
      m_pRcvLossList.remove(*(Int32*)ctrlpkt.m_pcData, *(Int32*)(ctrlpkt.m_pcData + 4));

      break;

   case 32767: //0x7FFF - reserved and user defined messages
      m_pCC.processCustomMsg(&ctrlpkt);
      // update CC parameters
      m_ullInterval = (Int64)(m_pCC.m_dPktSndPeriod * m_ullCPUFrequency);
      m_dCongestionWindow = m_pCC.m_dCWndSize;

      break;

   default:
      break;
   }
}

#endregion
        
private int CUDTpackData(CPacket packet, Int64 ts)
{
   int payload = 0;
   bool probe = false;

   Int64 entertime;
   CClock.rdtsc(out entertime);

   if ((0 != m_ullTargetTime) && (entertime > m_ullTargetTime))
      m_ullTimeDiff += entertime - m_ullTargetTime;

   // Loss retransmission always has higher priority.
   if ((packet.m_iSeqNo = m_pSndLossList.getLostSeq()) >= 0)
   {
      // protect m_iSndLastDataAck from updating by ACK processing
      CGuard ackguard = new CGuard(m_AckLock);

      int offset = CSeqNo.seqoff((Int32)m_iSndLastDataAck, packet.m_iSeqNo);
      if (offset < 0)
         return 0;

      int msglen;

      payload = m_pSndBuffer.readData(&(packet.m_pcData), offset, packet.m_iMsgNo, msglen);

      if (-1 == payload)
      {
         Int32[] seqpair = new int[2];
         seqpair[0] = packet.m_iSeqNo;
         seqpair[1] = CSeqNo.incseq(seqpair[0], msglen);
         sendCtrl(7, &packet.m_iMsgNo, seqpair, 8);

         // only one msg drop request is necessary
         m_pSndLossList.remove(seqpair[1]);

         return 0;
      }
      else if (0 == payload)
         return 0;

      ++ m_iTraceRetrans;
      ++ m_iRetransTotal;
   }
   else
   {
      // If no loss, pack a new packet.

      // check congestion/flow window limit
      int cwnd = (m_iFlowWindowSize < (int)m_dCongestionWindow) ? m_iFlowWindowSize : (int)m_dCongestionWindow;
      if (cwnd >= CSeqNo.seqlen((m_iSndLastAck), CSeqNo.incseq(m_iSndCurrSeqNo)))
      {
         if (0 != (payload = m_pSndBuffer.readData(&(packet.m_pcData), packet.m_iMsgNo)))
         {
            m_iSndCurrSeqNo = CSeqNo.incseq(m_iSndCurrSeqNo);
            m_pCC.setSndCurrSeqNo((Int32)m_iSndCurrSeqNo);

            packet.m_iSeqNo = m_iSndCurrSeqNo;

            // every 16 (0xF) packets, a packet pair is sent
            if (0 == (packet.m_iSeqNo & 0xF))
               probe = true;
         }
         else
         {
            m_ullTargetTime = 0;
            m_ullTimeDiff = 0;
            ts = 0;
            return 0;
         }
      }
      else
      {
         m_ullTargetTime = 0;
         m_ullTimeDiff = 0;
         ts = 0;
         return 0;
      }
   }

   packet.m_iTimeStamp = (int)(CClock.getTime() - m_StartTime);
   m_pSndTimeWindow.onPktSent(packet.m_iTimeStamp);

   packet.m_iID = m_PeerID;

   m_pCC.onPktSent(&packet);

   ++ m_llTraceSent;
   ++ m_llSentTotal;

   if (probe)
   {
      // sends out probing packet pair
      ts = entertime;
      probe = false;
   }
   else
   {
         if (m_ullTimeDiff >= m_ullInterval)
         {
            ts = entertime;
            m_ullTimeDiff -= m_ullInterval;
         }
         else
         {
            ts = entertime + m_ullInterval - m_ullTimeDiff;
            m_ullTimeDiff = 0;
         }
   }

   m_ullTargetTime = ts;

   packet.m_iID = m_PeerID;
   packet.setLength(payload);

   return payload;
}
   
private int processData(CUnit unit)
{
   CPacket packet = unit.m_Packet;

   // Just heard from the peer, reset the expiration count.
   m_iEXPCount = 1;
   m_ullEXPInt = m_ullMinEXPInt;

   if (CSeqNo.incseq(m_iSndCurrSeqNo) == m_iSndLastAck)
   {
      CClock.rdtsc(m_ullNextEXPTime);
      if (!m_pCC.m_bUserDefinedRTO)
         m_ullNextEXPTime += m_ullEXPInt;
      else
         m_ullNextEXPTime += m_pCC.m_iRTO * m_ullCPUFrequency;
   }

   m_pCC.onPktReceived(&packet);

   ++ m_iPktCount;

   // update time information
   m_pRcvTimeWindow.onPktArrival();

   // check if it is probing packet pair
   if (0 == (packet.m_iSeqNo & 0xF))
      m_pRcvTimeWindow.probe1Arrival();
   else if (1 == (packet.m_iSeqNo & 0xF))
      m_pRcvTimeWindow.probe2Arrival();

   ++ m_llTraceRecv;
   ++ m_llRecvTotal;

   Int32 offset = CSeqNo.seqoff(m_iRcvLastAck, packet.m_iSeqNo);
   if ((offset < 0) || (offset >= m_pRcvBuffer.getAvailBufSize()))
      return -1;

   if (m_pRcvBuffer.addData(unit, offset) < 0)
      return -1;

   // Loss detection.
   if (CSeqNo.seqcmp(packet.m_iSeqNo, CSeqNo.incseq(m_iRcvCurrSeqNo)) > 0)
   {
      // If loss found, insert them to the receiver loss list
      m_pRcvLossList.insert(CSeqNo.incseq(m_iRcvCurrSeqNo), CSeqNo.decseq(packet.m_iSeqNo));

      // pack loss list for NAK
      Int32[] lossdata = new int[2];
      lossdata[0] = CSeqNo.incseq(m_iRcvCurrSeqNo) | 0x80000000;
      lossdata[1] = CSeqNo.decseq(packet.m_iSeqNo);

      // Generate loss report immediately.
      sendCtrl(3, null, lossdata, (CSeqNo.incseq(m_iRcvCurrSeqNo) == CSeqNo.decseq(packet.m_iSeqNo)) ? 1 : 2);

      m_iTraceRcvLoss += CSeqNo.seqlen(m_iRcvCurrSeqNo, packet.m_iSeqNo) - 2;
   }

   // This is not a regular fixed size packet...   
   //an irregular sized packet usually indicates the end of a message, so send an ACK immediately   
   if (packet.getLength() != m_iPayloadSize)   
      CClock.rdtsc(m_ullNextACKTime); 

   // Update the current largest sequence number that has been received.
   // Or it is a retransmitted packet, remove it from receiver loss list.
   if (CSeqNo.seqcmp(packet.m_iSeqNo, m_iRcvCurrSeqNo) > 0)
      m_iRcvCurrSeqNo = packet.m_iSeqNo;
   else
      m_pRcvLossList.remove(packet.m_iSeqNo);

   return 0;
}   



}
}