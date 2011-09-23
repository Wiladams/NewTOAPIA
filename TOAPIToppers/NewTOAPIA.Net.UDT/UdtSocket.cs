namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using UDTSOCKET = System.Int32;

    public enum UDTSockType 
{
    UDT_STREAM = 1, 
    UDT_DGRAM
}

   public enum UDTSTATUS 
   {
       INIT = 1, 
       OPENED, 
       LISTENING, 
       CONNECTED, 
       BROKEN, 
       CLOSED
   }

    public class UdtSocket
{
        public UDTSTATUS m_Status;                       // current socket state

   internal Int64  m_TimeStamp;                     // time when the socket is closed

   internal AddressFamily m_iIPversion;                         // IP version
   internal IPEndPoint m_pSelfAddr;                    // pointer to the local address of the socket
   internal IPEndPoint m_pPeerAddr;                    // pointer to the peer address of the socket

   UDTSOCKET m_SocketID;                     // socket ID
   public Socket m_ListenSocket;                 // ID of the listener socket; 0 means this is an independent socket

   UDTSOCKET m_PeerID;                       // peer socket ID
   Int32 m_iISN;                           // initial sequence number, used to tell different connection from same IP:port

   public CUDT m_pUDT;                             // pointer to the UDT entity

  public List<UdtSocket> m_pQueuedSockets;    // set of connections waiting for accept()
   public List<UdtSocket> m_pAcceptSockets;    // set of accept()ed connections

   AutoResetEvent m_AcceptCond;              // used to block "accept" call
   Mutex m_AcceptLock;             // mutex associated to m_AcceptCond

   uint m_uiBackLog;                 // maximum number of connections in queue

public UdtSocket()
{
//m_TimeStamp(0),
//m_iIPversion(0),
//m_pSelfAddr(null),
//m_pPeerAddr(null),
//m_SocketID(0),
//m_ListenSocket(0),
//m_PeerID(0),
//m_iISN(0),
//m_pUDT(null),
//m_pQueuedSockets(null),
//m_pAcceptSockets(null),
//m_AcceptCond(),
//m_AcceptLock(),
//m_uiBackLog(0)

m_Status = UDTSTATUS.INIT;


      m_AcceptLock = new Mutex();
      m_AcceptCond = new AutoResetEvent(false);
}
    
~UdtSocket()
{
   //if (AF_INET == m_iIPversion)
   //{
   //   delete (sockaddr_in*)m_pSelfAddr;
   //   delete (sockaddr_in*)m_pPeerAddr;
   //}
   //else
   //{
   //   delete (sockaddr_in6*)m_pSelfAddr;
   //   delete (sockaddr_in6*)m_pPeerAddr;
   //}

   //delete m_pUDT;

   //delete m_pQueuedSockets;
   //delete m_pAcceptSockets;


      m_AcceptLock.Close();
      m_AcceptCond.Close();
}
}
}