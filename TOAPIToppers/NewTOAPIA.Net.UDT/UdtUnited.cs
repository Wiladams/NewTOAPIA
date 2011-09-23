/*****************************************************************************
Copyright (c) 2001 - 2009, The Board of Trustees of the University of Illinois.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

* Redistributions of source code must retain the above
  copyright notice, this list of conditions and the
  following disclaimer.

* Redistributions in binary form must reproduce the
  above copyright notice, this list of conditions
  and the following disclaimer in the documentation
  and/or other materials provided with the distribution.

* Neither the name of the University of Illinois
  nor the names of its contributors may be used to
  endorse or promote products derived from this
  software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*****************************************************************************/

/*****************************************************************************
written by
   Yunhong Gu, last updated 05/05/2009
*****************************************************************************/

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using UDPSOCKET = System.Int32;
    using UDTSOCKET = System.Int32;
    using DWORD = System.Int32;

    public class CUDTUnited
    {
        bool m_bClosing;
        Mutex m_GCStopLock;
        AutoResetEvent m_GCStopCond;

        static Mutex m_InitLock;
        static bool m_bGCStatus;					            // if the GC thread is working (true)

        Thread m_GCThread;
        CCache m_pCache;					            // UDT network information cache


        Dictionary<UDTSOCKET, UdtSocket> m_ClosedSockets;    // temporarily store closed sockets
        List<CMultiplexer> m_vMultiplexer;		            // UDP multiplexer
        Mutex m_MultiplexerLock;

        Dictionary<UDTSOCKET, UdtSocket> m_Sockets = new Dictionary<int, UdtSocket>();       // stores all the socket structures

        Mutex m_ControlLock;                    // used to synchronize UDT API
        Mutex m_IDLock;                         // used to synchronize ID generation
        UDTSOCKET m_SocketID;                             // seed to generate a new unique socket ID

        int m_TLSError;                         // thread local error record (last error)
        Dictionary<DWORD, CUDTException> m_mTLSRecord = new Dictionary<int, CUDTException>();



        Mutex m_TLSLock;

        CUDTUnited()
        {
            //m_Sockets(),
            //m_ControlLock(),
            //m_IDLock(),
            //m_SocketID(0),
            //m_TLSError(),
            //m_vMultiplexer(),
            //m_MultiplexerLock(),
            //m_pCache(null),
            //m_bClosing(false),
            //m_GCStopLock(),
            //m_GCStopCond(),
            //m_InitLock(),
            //m_bGCStatus(false),
            //m_GCThread(),
            //m_ClosedSockets()

            srand((int)CClock.getTime());
            m_SocketID = 1 + (int)((1 << 30) * ((double)(rand()) / RAND_MAX));

            m_ControlLock = new Mutex();
            m_IDLock = new Mutex();
            m_InitLock = new Mutex();

            m_TLSError = TlsAlloc();
            m_TLSLock = new Mutex();



            m_pCache = new CCache();
        }

        ~CUDTUnited()
        {
            //   CloseHandle(m_ControlLock);
            //   CloseHandle(m_IDLock);
            //   CloseHandle(m_InitLock);

            //   TlsFree(m_TLSError);
            //   CloseHandle(m_TLSLock);

            //delete m_pCache;

        }



        // Functionality:
        //    initialize the UDT library.
        // Parameters:
        //    None.
        // Returned value:
        //    0 if success, otherwise -1 is returned.
        static CUDTUnited()
        {
            m_InitLock = new Mutex();
            CGuard gcinit = new CGuard(m_InitLock);

            //init CTimer.EventLock

            if (m_bGCStatus)
                return true;

            m_bClosing = false;
            m_GCStopLock = new Mutex();
            m_GCStopCond = new AutoResetEvent(false);
            DWORD ThreadID;
            m_GCThread = CreateThread(null, 0, garbageCollect, this, null, &ThreadID);

            m_bGCStatus = true;

            return 0;
        }

        // Functionality:
        //    release the UDT library.
        // Parameters:
        //    None.
        // Returned value:
        //    0 if success, otherwise -1 is returned.

        int cleanup()
        {
            CGuard gcinit = new CGuard(m_InitLock);

            //destroy CTimer.EventLock

            if (!m_bGCStatus)
                return 0;

            m_bClosing = true;
            m_GCStopCond.Set();
            m_GCThread.Join();

            m_GCThread.Join();
            m_GCStopLock.Close();
            m_GCStopCond.Close();

            m_bGCStatus = false;

            return 0;
        }

        // Functionality:
        //    Create a new UDT socket.
        // Parameters:
        //    0) [in] af: IP version, IPv4 (AF_INET) or IPv6 (AF_INET6).
        //    1) [in] type: socket type, SOCK_STREAM or SOCK_DGRAM
        // Returned value:
        //    The new UDT socket ID, or INVALID_SOCK.

        UDTSOCKET newSocket(AddressFamily af, UDTSockType type)
        {
            //if ((type != UDTSockType.SOCK_STREAM) && (type != UDTSockType.SOCK_DGRAM))
            //   throw new CUDTException(5, 3, 0);

            UdtSocket ns = null;

            try
            {
                ns = new UdtSocket();
                ns.m_pUDT = new CUDT();
                ns.m_pSelfAddr = new IPAddress();

                if (AddressFamily.InterNetwork == af)
                {
                    ns.m_pSelfAddr = new IPEndPoint(IPAddress.Any, 0);
                }
                else
                {
                    ns.m_pSelfAddr = new IPEndPoint(IPAddress.IPv6Any, 0);
                }
            }
            catch (Exception e)
            {
                //delete ns;
                throw new CUDTException(3, 2, 0);
            }

            lock (m_IDLock)
            {
                ns.m_SocketID = --m_SocketID;
            }

            ns.m_Status = UDTSTATUS.INIT;
            ns.m_ListenSocket = 0;
            ns.m_pUDT.m_SocketID = ns.m_SocketID;
            ns.m_pUDT.m_iSockType = (UDTSockType.SOCK_STREAM == type) ? UDTSockType.UDT_STREAM : UDTSockType.UDT_DGRAM;
            ns.m_pUDT.m_iIPversion = ns.m_iIPversion = af;
            ns.m_pUDT.m_pCache = m_pCache;

            // protect the m_Sockets structure.
            lock (m_ControlLock)
            {
                try
                {
                    m_Sockets[ns.m_SocketID] = ns;
                }
                catch (Exception e)
                {
                    //failure and rollback
                    //delete ns;
                    ns = null;
                }
            }

            if (null == ns)
                throw new CUDTException(3, 2, 0);

            return ns.m_SocketID;
        }

        // Functionality:
        //    Create a new UDT connection.
        // Parameters:
        //    0) [in] listen: the listening UDT socket;
        //    1) [in] peer: peer address.
        //    2) [in/out] hs: handshake information from peer side (in), negotiated value (out);
        // Returned value:
        //    If the new connection is successfully created: 1 success, 0 already exist, -1 error.

        public int newConnection(UDTSOCKET listen, IPEndPoint peer, CHandShake hs)
        {
            UdtSocket ns = null;
            UdtSocket ls = locate(listen);

            if (null == ls)
                return -1;

            // if this connection has already been processed
            if (null != (ns = locate(listen, peer, hs.m_iID, hs.m_iISN)))
            {
                if (ns.m_pUDT.m_bBroken)
                {
                    // last connection from the "peer" address has been broken
                    ns.m_Status = UDTSTATUS.CLOSED;
                    ns.m_TimeStamp = CClock.getTime();

                    lock (ls.m_AcceptLock)
                    {
                        ls.m_pQueuedSockets.Remove(ns.m_SocketID);
                        ls.m_pAcceptSockets.Remove(ns.m_SocketID);
                    }
                }
                else
                {
                    // connection already exist, this is a repeated connection request
                    // respond with existing HS information

                    hs.m_iISN = ns.m_pUDT.m_iISN;
                    hs.m_iMSS = ns.m_pUDT.m_iMSS;
                    hs.m_iFlightFlagSize = ns.m_pUDT.m_iFlightFlagSize;
                    hs.m_iReqType = -1;
                    hs.m_iID = ns.m_SocketID;

                    return 0;

                    //except for this situation a new connection should be started
                }
            }

            // exceeding backlog, refuse the connection request
            if (ls.m_pQueuedSockets.size() >= ls.m_uiBackLog)
                return -1;

            try
            {
                ns = new UdtSocket();
                ns.m_pUDT = new CUDT((ls.m_pUDT));
                if (AddressFamily.InterNetwork == ls.m_iIPversion)
                {
                    ns.m_pSelfAddr = new IPEndPoint(IPAddress.Any, 0);
                    ns.m_pPeerAddr = peer;
                }
                else
                {
                    ns.m_pSelfAddr = new IPEndPoint(IPAddress.IPv6Any, 0);
                    ns.m_pPeerAddr = peer;
                }
            }
            catch (Exception e)
            {
                ns = null;
                return -1;
            }

            lock (m_IDLock)
            {
                ns.m_SocketID = --m_SocketID;
            }

            ns.m_ListenSocket = listen;
            ns.m_iIPversion = ls.m_iIPversion;
            ns.m_pUDT.m_SocketID = ns.m_SocketID;
            ns.m_PeerID = hs.m_iID;
            ns.m_iISN = hs.m_iISN;

            int error = 0;

            try
            {
                // bind to the same addr of listening socket
                ns.m_pUDT.open();
                updateMux(ns.m_pUDT, ls);
                ns.m_pUDT.connect(peer, hs);
            }
            catch (Exception e)
            {
                error = 1;
                goto ERR_ROLLBACK;
            }

            ns.m_Status = UDTSTATUS.CONNECTED;

            // copy address information of local node
            ns.m_pUDT.m_pSndQueue.m_pChannel.getSockAddr(ns.m_pSelfAddr);
            CIPAddress.pton(ns.m_pSelfAddr, ns.m_pUDT.m_piSelfIP, ns.m_iIPversion);

            // protect the m_Sockets structure.
            lock (m_ControlLock)
            {
                try
                {
                    m_Sockets[ns.m_SocketID] = ns;
                }
                catch (Exception e)
                {
                    error = 2;
                }
            }

            lock (ls.m_AcceptLock)
            {
                try
                {
                    ls.m_pQueuedSockets.insert(ns.m_SocketID);
                }
                catch (Exception e)
                {
                    error = 3;
                }
            }

            CClock.triggerEvent();

        ERR_ROLLBACK:
            if (error > 0)
            {
                ns.m_pUDT.close();
                ns.m_Status = UDTSTATUS.CLOSED;
                ns.m_TimeStamp = CClock.getTime();

                return -1;
            }

            // wake up a waiting accept() call
            m_AcceptCond.Set();

            return 1;
        }

        // Functionality:
        //    look up the UDT entity according to its ID.
        // Parameters:
        //    0) [in] u: the UDT socket ID.
        // Returned value:
        //    Pointer to the UDT entity.

        public CUDT lookup(UDTSOCKET u)
        {
            UdtSocket aValue = null;

            // protects the m_Sockets structure
            lock (m_ControlLock)
            {
                bool getResult = m_Sockets.TryGetValue(u, out aValue);

                if (!getResult || (aValue.m_Status == UDTSTATUS.CLOSED))
                {
                    throw new CUDTException(5, 4, 0);
                }
            }

            return aValue.m_pUDT;
        }

        // Functionality:
        //    Check the status of the UDT socket.
        // Parameters:
        //    0) [in] u: the UDT socket ID.
        // Returned value:
        //    UDT socket status, or INIT if not found.
        public UDTSTATUS getStatus(UDTSOCKET u)
        {
            // protects the m_Sockets structure
            UdtSocket aValue = null;

            lock (m_ControlLock)
            {
                if (!m_Sockets.TryGetValue(u, out aValue))
                {
                    return UDTSTATUS.INIT;
                }

                if (aValue.m_pUDT.m_bBroken)
                    return UDTSTATUS.BROKEN;
            }

            return aValue.m_Status;
        }


        // socket APIs

        public int bind(UDTSOCKET u, IPAddress name, int namelen)
        {
            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            // cannot bind a socket more than once
            if (UDTSTATUS.INIT != s.m_Status)
                throw new CUDTException(5, 0, 0);

            // check the size of SOCKADDR structure
            if (AF_INET == s.m_iIPversion)
            {
                if (namelen != sizeof(sockaddr_in))
                    throw new CUDTException(5, 3, 0);
            }
            else
            {
                if (namelen != sizeof(sockaddr_in6))
                    throw new CUDTException(5, 3, 0);
            }

            s.m_pUDT.open();
            updateMux(s.m_pUDT, name);
            s.m_Status = UdtSocket.OPENED;

            // copy address information of local node
            s.m_pUDT.m_pSndQueue.m_pChannel.getSockAddr(s.m_pSelfAddr);

            return 0;
        }

        public int bind(UDTSOCKET u, UDPSOCKET udpsock)
{
   UdtSocket s = locate(u);

   if (null == s)
      throw new CUDTException(5, 4, 0);

   // cannot bind a socket more than once
   if (UDTSTATUS.INIT != s.m_Status)
      throw new CUDTException(5, 0, 0);

   sockaddr_in name4;
   sockaddr_in6 name6;
   sockaddr* name;
   socklen_t namelen;

   if (AF_INET == s.m_iIPversion)
   {
      namelen = sizeof(sockaddr_in);
      name = (sockaddr*)&name4;
   }
   else
   {
      namelen = sizeof(sockaddr_in6);
      name = (sockaddr*)&name6;
   }

   if (-1 == ::getsockname(udpsock, name, &namelen))
      throw new CUDTException(5, 3);

   s.m_pUDT.open();
   updateMux(s.m_pUDT, name, &udpsock);
   s.m_Status = UDTSTATUS.OPENED;

   // copy address information of local node
   s.m_pUDT.m_pSndQueue.m_pChannel.getSockAddr(s.m_pSelfAddr);

   return 0;
}

        public int listen(UDTSOCKET u, int backlog)
        {
            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            // do nothing if the socket is already listening
            if (UDTSTATUS.LISTENING == s.m_Status)
                return 0;

            // a socket can listen only if is in OPENED status
            if (UDTSTATUS.OPENED != s.m_Status)
                throw new CUDTException(5, 5, 0);

            // listen is not supported in rendezvous connection setup
            if (s.m_pUDT.m_bRendezvous)
                throw new CUDTException(5, 7, 0);

            if (backlog <= 0)
                throw new CUDTException(5, 3, 0);

            s.m_uiBackLog = backlog;

            try
            {
                s.m_pQueuedSockets = new List<UDTSOCKET>();
                s.m_pAcceptSockets = new List<UDTSOCKET>();
            }
            catch (Exception e)
            {
                //delete s.m_pQueuedSockets;
                throw new CUDTException(3, 2, 0);
            }

            s.m_pUDT.listen();

            s.m_Status = UDTSTATUS.LISTENING;

            return 0;
        }


        public UDTSOCKET accept(UDTSOCKET listen, IPAddress addr, out int addrlen)
        {
            if ((null != addr) && (null == addrlen))
                throw new CUDTException(5, 3, 0);

            UdtSocket* ls = locate(listen);

            if (ls == null)
                throw new CUDTException(5, 4, 0);

            // the "listen" socket must be in LISTENING status
            if (UDTSTATUS.LISTENING != ls.m_Status)
                throw new CUDTException(5, 6, 0);

            // no "accept" in rendezvous connection setup
            if (ls.m_pUDT.m_bRendezvous)
                throw new CUDTException(5, 7, 0);

            UDTSOCKET u = CUDT.INVALID_SOCK;
            bool accepted = false;

            // !!only one conection can be set up each time!!

            while (!accepted)
            {
                WaitForSingleObject(ls.m_AcceptLock, INFINITE);

                if (ls.m_pQueuedSockets.size() > 0)
                {
                    u = *(ls.m_pQueuedSockets.begin());
                    ls.m_pAcceptSockets.insert(ls.m_pAcceptSockets.end(), u);
                    ls.m_pQueuedSockets.erase(ls.m_pQueuedSockets.begin());

                    accepted = true;
                }
                else if (!ls.m_pUDT.m_bSynRecving)
                    accepted = true;

                ReleaseMutex(ls.m_AcceptLock);

                if (!accepted & (UDTSTATUS.LISTENING == ls.m_Status))
                    WaitForSingleObject(ls.m_AcceptCond, INFINITE);

                if (UDTSTATUS.LISTENING != ls.m_Status)
                {
                    SetEvent(ls.m_AcceptCond);
                    accepted = true;
                }
            }

            if (u == CUDT.INVALID_SOCK)
            {
                // non-blocking receiving, no connection available
                if (!ls.m_pUDT.m_bSynRecving)
                    throw new CUDTException(6, 2, 0);

                // listening socket is closed
                throw new CUDTException(5, 6, 0);
            }

            if (AF_INET == locate(u).m_iIPversion)
                *addrlen = sizeof(sockaddr_in);
            else
                *addrlen = sizeof(sockaddr_in6);

            // copy address information of peer node
            memcpy(addr, locate(u).m_pPeerAddr, *addrlen);

            return u;
        }

        int connect(UDTSOCKET u, IPEndPoint name, int namelen)
        {
            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            // check the size of SOCKADDR structure
            if (AddressFamily.InterNetwork == s.m_iIPversion)
            {
                if (namelen != sizeof(sockaddr_in))
                    throw new CUDTException(5, 3, 0);
            }
            else
            {
                if (namelen != sizeof(sockaddr_in6))
                    throw new CUDTException(5, 3, 0);
            }

            // a socket can "connect" only if it is in INIT or OPENED status
            if (UDTSTATUS.INIT == s.m_Status)
            {
                if (!s.m_pUDT.m_bRendezvous)
                {
                    s.m_pUDT.open();
                    updateMux(s.m_pUDT);
                    s.m_Status = UDTSTATUS.OPENED;
                }
                else
                    throw new CUDTException(5, 8, 0);
            }
            else if (UDTSTATUS.OPENED != s.m_Status)
                throw new CUDTException(5, 2, 0);

            s.m_pUDT.connect(name);
            s.m_Status = UDTSTATUS.CONNECTED;

            // copy address information of local node
            s.m_pUDT.m_pSndQueue.m_pChannel.getSockAddr(s.m_pSelfAddr);
            CIPAddress.pton(s.m_pSelfAddr, s.m_pUDT.m_piSelfIP, s.m_iIPversion);

            // record peer address
            s.m_pPeerAddr = name;
            //if (AF_INET == s.m_iIPversion)
            //{
            //   s.m_pPeerAddr = (sockaddr*)(new sockaddr_in);
            //   memcpy(s.m_pPeerAddr, name, sizeof(sockaddr_in));
            //}
            //else
            //{
            //   s.m_pPeerAddr = (sockaddr*)(new sockaddr_in6);
            //   memcpy(s.m_pPeerAddr, name, sizeof(sockaddr_in6));
            //}

            return 0;
        }

        public int close(UDTSOCKET u)
        {
            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            s.m_pUDT.close();

            // a socket will not be immediated removed when it is closed
            // in order to prevent other methods from accessing invalid address
            // a timer is started and the socket will be removed after approximately 1 second
            s.m_TimeStamp = CClock.getTime();

            UDTSTATUS os = s.m_Status;

            // synchronize with garbage collection.
            lock (m_ControlLock)
            {
                s.m_Status = UDTSTATUS.CLOSED;

                m_Sockets.erase(s.m_SocketID);
                m_ClosedSockets[s.m_SocketID] = s;

                if (0 != s.m_ListenSocket)
                {
                    // if it is an accepted socket, remove it from the listener's queue
                    //map<UDTSOCKET, UdtSocket*>::iterator ls = m_Sockets.find(s.m_ListenSocket);
                    UdtSocket ls = null;
                    if (m_Sockets.TryGetValue(s.m_ListenSocket))
                    {
                        lock (ls.second.m_AcceptLock)
                        {
                            ls.m_pAcceptSockets.erase(s.m_SocketID);
                        }
                    }
                }
            }

            // broadcast all "accept" waiting
            if (UDTSTATUS.LISTENING == os)
            {
                SetEvent(s.m_AcceptCond);
            }

            CClock.triggerEvent();

            return 0;
        }

        int getpeername(UDTSOCKET u, out IPAddress addr, out int namelen)
        {
            namelen = 0;

            if (UDTSTATUS.CONNECTED != getStatus(u))
                throw new CUDTException(2, 2, 0);

            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            if (!s.m_pUDT.m_bConnected || s.m_pUDT.m_bBroken)
                throw new CUDTException(2, 2, 0);

            if (AddressFamily.InterNetwork == s.m_iIPversion)
                namelen = sizeof(sockaddr_in);
            else
                namelen = sizeof(sockaddr_in6);

            // copy address information of peer node
            memcpy(name, s.m_pPeerAddr, *namelen);
            addr = s.m_PeerAddr;

            return 0;
        }

        public int getsockname(UDTSOCKET u, out IPEndPoint name)
        {
            UdtSocket s = locate(u);

            if (null == s)
                throw new CUDTException(5, 4, 0);

            if (UDTSTATUS.INIT == s.m_Status)
                throw new CUDTException(2, 2, 0);

            name = s.m_pSelfAddr;
            //if (AF_INET == s.m_iIPversion)
            //    *namelen = sizeof(sockaddr_in);
            //else
            //    *namelen = sizeof(sockaddr_in6);

            // copy address information of local node
            //memcpy(name, s.m_pSelfAddr, *namelen);

            return 0;
        }

        public int select(ref UDTSOCKET[] readfds, ref UDTSOCKET[] writefds, ref UDTSOCKET[] exceptfds, timeval timeout)
        {
            Int64 entertime = CClock.getTime();

            Int64 to;
            if (null == timeout)
                to = 0xFFFFFFFFFFFFFFFF;
            else
                to = timeout.tv_sec * 1000000 + timeout.tv_usec;

            // initialize results
            int count = 0;
            HashSet<UDTSOCKET> rs = new HashSet<int>();
            HashSet<UDTSOCKET> ws = new HashSet<int>();
            HashSet<UDTSOCKET> es = new HashSet<int>();

            // retrieve related UDT sockets
            List<UdtSocket> ru = new List<UdtSocket>();
            List<UdtSocket> wu = new List<UdtSocket>();
            List<UdtSocket> eu = new List<UdtSocket>();
            UdtSocket s;

            if (null != readfds)
            {
                foreach (UDTSOCKET sockid in readfds)
                {
                    if (UDTSTATUS.BROKEN == getStatus(sockid))
                    {
                        rs.Add(sockid);
                        ++count;
                    }
                    else if (null == (s = locate(sockid)))
                        throw new CUDTException(5, 4, 0);
                    else
                        ru.Add(s);
                }
            }

            if (null != writefds)
            {
                foreach (UDTSOCKET i2 in writefds)
                {
                    if (UDTSTATUS.BROKEN == getStatus(i2))
                    {
                        ws.Add(i2);
                        ++count;
                    }
                    else if (null == (s = locate(i2)))
                        throw new CUDTException(5, 4, 0);
                    else
                        wu.Add(s);
                }
            }

            if (null != exceptfds)
            {
                //for (set<UDTSOCKET>::iterator i3 = exceptfds.begin(); i3 != exceptfds.end(); ++ i3)
                foreach (UDTSOCKET i3 in exceptfds)
                {
                    if (UDTSTATUS.BROKEN == getStatus(i3))
                    {
                        es.Add(i3);
                        ++count;
                    }
                    else if (null == (s = locate(i3)))
                        throw new CUDTException(5, 4, 0);
                    else
                        eu.Add(s);
                }
            }

            do
            {
                // query read sockets
                //for (vector<UdtSocket*>::iterator j1 = ru.begin(); j1 != ru.end(); ++ j1)
                foreach (UdtSocket j1 in ru)
                {
                    s = j1;

                    if ((s.m_pUDT.m_bConnected && (s.m_pUDT.m_pRcvBuffer.getRcvDataSize() > 0) && ((s.m_pUDT.m_iSockType == UDTSockType.UDT_STREAM) || (s.m_pUDT.m_pRcvBuffer.getRcvMsgNum() > 0)))
                       || (!s.m_pUDT.m_bListening && (s.m_pUDT.m_bBroken || !s.m_pUDT.m_bConnected))
                       || (s.m_pUDT.m_bListening && (s.m_pQueuedSockets.size() > 0))
                       || (s.m_Status == UDTSTATUS.CLOSED))
                    {
                        rs.insert(s.m_SocketID);
                        ++count;
                    }
                }

                // query write sockets
                //for (vector<UdtSocket*>::iterator j2 = wu.begin(); j2 != wu.end(); ++ j2)
                foreach (UdtSocket j2 in wu)
                {
                    s = j2;

                    if ((s.m_pUDT.m_bConnected && (s.m_pUDT.m_pSndBuffer.getCurrBufSize() < s.m_pUDT.m_iSndBufSize))
                       || s.m_pUDT.m_bBroken || !s.m_pUDT.m_bConnected || (s.m_Status == UDTSTATUS.CLOSED))
                    {
                        ws.insert(s.m_SocketID);
                        ++count;
                    }
                }

                // query exceptions on sockets
                //for (vector<UdtSocket*>::iterator j3 = eu.begin(); j3 != eu.end(); ++ j3)
                foreach (UdtSocket j3 in eu)
                {
                    // check connection request status, not supported now
                }

                if (0 < count)
                    break;

                CClock.waitForEvent();
            } while (to > CClock.getTime() - entertime);

            if (null != readfds)
                readfds = rs.ToArray();

            if (null != writefds)
                writefds = ws.ToArray();

            if (null != exceptfds)
                exceptfds = es.ToArray();

            return count;
        }

        public int selectEx(ref UDTSOCKET[] fds, ref UDTSOCKET[] readfds, ref UDTSOCKET[] writefds, ref UDTSOCKET[] exceptfds, Int64 msTimeOut)
        {
            Int64 entertime = CClock.getTime();

            Int64 to;
            if (msTimeOut >= 0)
                to = msTimeOut * 1000;
            else
                to = 0xFFFFFFFFFFFFFFFF;

            // initialize results
            int count = 0;
            if (null != readfds)
                readfds.clear();
            if (null != writefds)
                writefds.clear();
            if (null != exceptfds)
                exceptfds.clear();

            do
            {
                //for (vector<UDTSOCKET>::const_iterator i = fds.begin(); i != fds.end(); ++ i)
                foreach (UDTSOCKET i in fds)
                {
                    UdtSocket s = locate(i);

                    if ((null == s) || s.m_pUDT.m_bBroken || (s.m_Status == UDTSTATUS.CLOSED))
                    {
                        if (null != exceptfds)
                        {
                            exceptfds.push_back(i);
                            ++count;
                        }
                        continue;
                    }

                    if (null != readfds)
                    {
                        if ((s.m_pUDT.m_bConnected && (s.m_pUDT.m_pRcvBuffer.getRcvDataSize() > 0) && ((s.m_pUDT.m_iSockType == UDT_STREAM) || (s.m_pUDT.m_pRcvBuffer.getRcvMsgNum() > 0)))
                           || (s.m_pUDT.m_bListening && (s.m_pQueuedSockets.size() > 0)))
                        {
                            readfds.push_back(s.m_SocketID);
                            ++count;
                        }
                    }

                    if (null != writefds)
                    {
                        if (s.m_pUDT.m_bConnected && (s.m_pUDT.m_pSndBuffer.getCurrBufSize() < s.m_pUDT.m_iSndBufSize))
                        {
                            writefds.push_back(s.m_SocketID);
                            ++count;
                        }
                    }
                }

                if (count > 0)
                    break;

                CClock.waitForEvent();
            } while (to > CClock.getTime() - entertime);

            return count;
        }


        // Functionality:
        //    record the UDT exception.
        // Parameters:
        //    0) [in] e: pointer to a UDT exception instance.
        // Returned value:
        //    None.

        internal void setError(CUDTException e)
        {
            CGuard tg = new CGuard(m_TLSLock);
            //delete (CUDTException*)TlsGetValue(m_TLSError);
            //TlsSetValue(m_TLSError, e);
            m_mTLSRecord[GetCurrentThreadId()] = e;
        }

        // Functionality:
        //    look up the most recent UDT exception.
        // Parameters:
        //    None.
        // Returned value:
        //    pointer to a UDT exception instance.

        public CUDTException getError()
        {
            CGuard tg = new CGuard(m_TLSLock);
            if (null == TlsGetValue(m_TLSError))
            {
                CUDTException* e = new CUDTException();
                TlsSetValue(m_TLSError, e);
                m_mTLSRecord[GetCurrentThreadId()] = e;
            }
            return (CUDTException*)TlsGetValue(m_TLSError);
        }


        public UdtSocket locate(UDTSOCKET u)
        {
            lock (m_ControlLock)
            {
                UdtSocket i = null;
                bool foundValue = m_Sockets.TryGetValue(u, out i);
            }
            if ((!foundValue) || (i.m_Status == UDTSTATUS.CLOSED))
                return null;

            return i;
        }

        public UdtSocket locate(UDTSOCKET u, IPEndPoint peer, UDTSOCKET id, Int32 isn)
        {
            CGuard cg = new CGuard(m_ControlLock);

            UdtSocket i = null;

            m_Sockets.TryGetValue(u, out i);

            CGuard ag = new CGuard(i.m_AcceptLock);

            // look up the "peer" address in queued sockets set
            foreach (UDTSOCKET j1 in i.m_pQueuedSockets)
            {
                UdtSocket k1 = null;
                m_Sockets.TryGetValue(j1, out k1);
                // this socket might have been closed and moved m_ClosedSockets
                if (k1 == null)
                    continue;

                if (CIPAddress.ipcmp(peer, k1.m_pPeerAddr, i.m_iIPversion))
                {
                    if ((id == k1.m_PeerID) && (isn == k1.m_iISN))
                        return k1;
                }
            }

            // look up the "peer" address in accept sockets set
            foreach (UDTSOCKET j1 in i.m_pAcceptSockets)
            {
                UdtSocket k2 = null;
                m_Sockets.TryGetValue(j2, out k2);
                // this socket might have been closed and moved m_ClosedSockets
                if (k2 == null)
                    continue;

                if (CIPAddress.ipcmp(peer, k2.m_pPeerAddr, i.m_iIPversion))
                {
                    if ((id == k2.m_PeerID) && (isn == k2.m_iISN))
                        return k2;
                }
            }

            return null;
        }

        void updateMux(CUDT u, IPEndPoint addr, Socket udpsock)
        {
            CGuard cg = new CGuard(m_ControlLock);

            if ((u.m_bReuseAddr) && (null != addr))
            {
                //int port = (AF_INET == u.m_iIPversion) ? ntohs(((sockaddr_in*)addr).sin_port) : ntohs(((sockaddr_in6*)addr).sin6_port);
                int port = addr.Port;

                // find a reusable address
                //for (vector<CMultiplexer>::iterator i = m_vMultiplexer.begin(); i != m_vMultiplexer.end(); ++ i)
                foreach (CMultiplexer i in m_vMultiplexer)
                {
                    if ((i.m_iIPversion == u.m_iIPversion) && (i.m_iMSS == u.m_iMSS) && i.m_bReusable)
                    {
                        if (i.m_iPort == port)
                        {
                            // reuse the existing multiplexer
                            ++i.m_iRefCount;
                            u.m_pSndQueue = i.m_pSndQueue;
                            u.m_pRcvQueue = i.m_pRcvQueue;
                            return;
                        }
                    }
                }
            }

            // a new multiplexer is needed
            CMultiplexer m;
            m.m_iMSS = u.m_iMSS;
            m.m_iIPversion = u.m_iIPversion;
            m.m_iRefCount = 1;
            m.m_bReusable = u.m_bReuseAddr;

            m.m_pChannel = new CChannel(u.m_iIPversion);
            m.m_pChannel.setSndBufSize(u.m_iUDPSndBufSize);
            m.m_pChannel.setRcvBufSize(u.m_iUDPRcvBufSize);

            try
            {
                if (null != udpsock)
                    m.m_pChannel.open(udpsock);
                else
                    m.m_pChannel.open(addr);
            }
            catch (CUDTException e)
            {
                m.m_pChannel.close();
                //delete m.m_pChannel;
                throw e;
            }

            IPEndPoint sa = (AddressFamily.InterNetwork == u.m_iIPversion) ? new IPEndPoint(IPAddress.Any, 0) : new IPEndPoint(IPAddress.IPv6Any, 0);
            m.m_pChannel.getSockAddr(out sa);
            m.m_iPort = sa.Port;
            //         (AF_INET == u.m_iIPversion) ? ntohs(((sockaddr_in*)sa).sin_port) : ntohs(((sockaddr_in6*)sa).sin6_port);
            //if (AddressFamily.InterNetwork == u.m_iIPversion) 
            //    delete (sockaddr_in*)sa; 
            // else 
            //     delete (sockaddr_in6*)sa;

            m.m_pTimer = new CClock();

            m.m_pSndQueue = new CSndQueue(m.m_pChannel, m.m_pTimer);
            m.m_pRcvQueue = new CRcvQueue();
            m.m_pRcvQueue.init(32, u.m_iPayloadSize, m.m_iIPversion, 1024, m.m_pChannel, m.m_pTimer);

            m_vMultiplexer.insert(m_vMultiplexer.end(), m);

            u.m_pSndQueue = m.m_pSndQueue;
            u.m_pRcvQueue = m.m_pRcvQueue;
        }

        public void updateMux(CUDT u, UdtSocket ls)
        {
            CGuard cg = new CGuard(m_ControlLock);

            //int port = (AF_INET == ls.m_iIPversion) ? ntohs(((sockaddr_in*)ls.m_pSelfAddr).sin_port) : ntohs(((sockaddr_in6*)ls.m_pSelfAddr).sin6_port);
            int port = ls.m_pSelfAddr.Port;

            // find the listener's address
            foreach (CMultiplexer i in m_vMultiplexer)
            {
                if (i.m_iPort == port)
                {
                    // reuse the existing multiplexer
                    ++i.m_iRefCount;
                    u.m_pSndQueue = i.m_pSndQueue;
                    u.m_pRcvQueue = i.m_pRcvQueue;
                    return;
                }
            }
        }

        DWORD garbageCollect(Object p)
        {
            CUDTUnited self = (CUDTUnited)p;

            CGuard gcguard = new CGuard(self.m_GCStopLock);

            while (!self.m_bClosing)
            {
                self.checkBrokenSockets();

                self.checkTLSValue();

                //WaitForSingleObject(self.m_GCStopCond, 1000);
                self.m_GCStopCond.WaitOne(1000);
            }

            // remove all sockets and multiplexers
            foreach (KeyValuePair<UDTSOCKET, UdtSocket> kvp in m_Sockets)
            {
                kvp.Value.m_pUDT.close();
                kvp.Value.m_Status = UDTSTATUS.CLOSED;
                kvp.Value.m_TimeStamp = CClock.getTime();
                self.m_ClosedSockets[kvp.Key] = kvp.Value;
            }
            self.m_Sockets.Clear();

            while (self.m_ClosedSockets.Count > 0)
            {
                Thread.Sleep(1);

                self.checkBrokenSockets();
            }

            return 0;
        }




        public void checkBrokenSockets()
        {
            CGuard cg = new CGuard(m_ControlLock);

            // set of sockets To Be Closed and To Be Removed
            HashSet<UDTSOCKET> tbc = new HashSet<int>();
            HashSet<UDTSOCKET> tbr = new HashSet<int>();

            //for (map<UDTSOCKET, UdtSocket*>::iterator i = m_Sockets.begin(); i != m_Sockets.end(); ++ i)
            foreach (KeyValuePair<UDTSOCKET, UdtSocket> i in m_Sockets)
            {
                // check broken connection
                if (i.Value.m_pUDT.m_bBroken)
                {
                    // if there is still data in the receiver buffer, wait longer
                    if ((i.Value.m_pUDT.m_pRcvBuffer.getRcvDataSize() > 0) && (i.Value.m_pUDT.m_iBrokenCounter-- > 0))
                        continue;

                    //close broken connections and start removal timer
                    i.Value.m_Status = UDTSTATUS.CLOSED;
                    i.Value.m_TimeStamp = CClock.getTime();
                    tbc.Add(i.Key);
                    m_ClosedSockets[i.Key] = i.Value;

                    // remove from listener's queue
                    //map<UDTSOCKET, UdtSocket*>::iterator ls = m_Sockets.find(i.second.m_ListenSocket);
                    UdtSocket ls = null;
                    bool foundIt = m_Sockets.TryGetValue(i.Value.m_ListenSocket, out ls);
                    if (ls != null)
                    {
                        lock (ls.Value.m_AcceptLock)
                        {
                            ls.Value.m_pQueuedSockets.erase(i.Value.m_SocketID);
                            ls.Value.m_pAcceptSockets.erase(i.Value.m_SocketID);
                        }
                    }
                }
            }

            foreach (KeyValuePair<UDTSOCKET, UdtSocket> j in m_ClosedSockets)
            {
                // timeout 1 second to destroy a socket AND it has been removed from RcvUList
                if ((CClock.getTime() - j.Value.m_TimeStamp > 1000000) && ((null == j.Value.m_pUDT.m_pRNode) || !j.Value.m_pUDT.m_pRNode.m_bOnList))
                    tbr.Add(j.Key);

                // sockets cannot be removed here because it will invalidate the map iterator
            }

            // move closed sockets to the ClosedSockets structure
            foreach (UDTSOCKET sock in tbc)
            {
                m_Sockets.Remove(sock);
            }

            // remove those timeout sockets
            foreach (UDTSOCKET sock in tbr)
            {
                removeSocket(sock);
            }
        }

        public void removeSocket(UDTSOCKET u)
        {
            UdtSocket i = null;

            if (m_ClosedSockets.TryGetValue(u, out i))
            {
                // invalid socket ID
                return;
            }


            // decrease multiplexer reference count, and remove it if necessary
            int port = foundSocket.m_pSelfAddr.Port;
            //if (AF_INET == i.second.m_iIPversion)
            //   port = ntohs(((sockaddr_in*)(i.second.m_pSelfAddr)).sin_port);
            //else
            //   port = ntohs(((sockaddr_in6*)(i.second.m_pSelfAddr)).sin6_port);

            CMultiplexer m = null;
            foreach (CMultiplexer aMulti in m_vMultiplexer)
                if (port == aMulti.m_iPort)
                {
                    m = aMulti;
                    break;
                }

            if (null != i.m_pQueuedSockets)
            {
                CGuard.enterCS(i.m_AcceptLock);

                // if it is a listener, close all un-accepted sockets in its queue and remove them later
                HashSet<UDTSOCKET> tbc = new HashSet<int>();
                //for (set<UDTSOCKET>::iterator q = i.second.m_pQueuedSockets.begin(); q != i.second.m_pQueuedSockets.end(); ++ q)
                foreach (UDTSOCKET q in i.m_pQueuedSockets)
                {
                    m_Sockets[q].m_pUDT.close();
                    m_Sockets[q].m_TimeStamp = CClock.getTime();
                    m_Sockets[q].m_Status = UDTSTATUS.CLOSED;
                    tbc.Add(q);
                    m_ClosedSockets[q] = m_Sockets[q];
                }

                //for (set<UDTSOCKET>::iterator c = tbc.begin(); c != tbc.end(); ++ c)
                foreach (UDTSOCKET c in tbc)
                    m_Sockets.Remove(c);

                CGuard.leaveCS(i.m_AcceptLock);
            }

            // delete this one
            i.m_pUDT.close();
            //delete m_ClosedSockets[u];
            m_ClosedSockets.Remove(u);

            if (m == null)
                return;

            m.m_iRefCount--;
            if (0 == m.m_iRefCount)
            {
                m.m_pChannel.close();
                //delete m.m_pSndQueue;
                //delete m.m_pRcvQueue;
                //delete m.m_pTimer;
                //delete m.m_pChannel;
                m_vMultiplexer.Remove(m);
            }
        }

        //        void checkTLSValue()
        //{
        //   CGuard tg = new CGuard(m_TLSLock);

        //   List<DWORD> tbr = new List<int>();
        //   for (map<DWORD, CUDTException*>::iterator i = m_mTLSRecord.begin(); i != m_mTLSRecord.end(); ++ i)
        //   {
        //      HANDLE h = OpenThread(THREAD_QUERY_INFORMATION, FALSE, i.first);
        //      if (null == h)
        //      {
        //         tbr.insert(tbr.end(), i.first);
        //         break;
        //      }
        //      if (WAIT_OBJECT_0 == WaitForSingleObject(h, 0))
        //      {
        //         //delete i.second;
        //         tbr.Add(i.first);
        //      }
        //      CloseHandle(h);
        //   }
        //   for (vector<DWORD>::iterator j = tbr.begin(); j != tbr.end(); ++ j)
        //      m_mTLSRecord.erase(*j);
        //}

    }
}

