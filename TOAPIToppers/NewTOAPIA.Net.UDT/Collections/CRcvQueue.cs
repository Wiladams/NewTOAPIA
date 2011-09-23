namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using DWORD = System.Int32;

    public class CRcvQueue
    {
        #region Fields
        private CUnitQueue m_UnitQueue;		// The received packet queue

        private CRcvUList m_pRcvUList;		// List of UDT instances that will read packets from the queue
        private CHash m_pHash;			// Hash table for UDT socket looking up
        private CChannel m_pChannel;		// UDP channel for receving packets
        private CClock m_pTimer;			// shared timer with the snd queue

        private int m_iPayloadSize;                  // packet payload size

        private volatile bool m_bClosing;            // closing the workder
        private AutoResetEvent m_ExitCond;

        Object m_LSLock = new object();
        CUDT m_pListener;			// pointer to the (unique, if any) listening UDT entity
        CRendezvousQueue m_pRendezvousQueue;	// The list of sockets in rendezvous mode

        List<CUDT> m_vNewEntry;              // newly added entries, to be inserted
        Mutex m_IDLock;

        Dictionary<Int32, CPacket> m_mBuffer;	// temporary buffer for rendezvous connection request
        Mutex m_PassLock;
        AutoResetEvent m_PassCond;

        Thread m_WorkerThread;
        #endregion

        public CRcvQueue()
        {
            //m_WorkerThread(),
            //m_UnitQueue(),
            //m_pRcvUList(null),
            //m_pHash(null),
            //m_pChannel(null),
            //m_pTimer(null),
            //m_iPayloadSize(),
            //m_bClosing(false),
            //m_ExitCond(),
            //m_LSLock(),
            //m_pListener(null),
            //m_pRendezvousQueue(null),
            //m_vNewEntry(),
            //m_IDLock(),
            //m_mBuffer(),
            //m_PassLock(),
            //m_PassCond()

            m_PassLock = new Mutex();
            m_PassCond = new AutoResetEvent(false);
            //m_LSLock = new Mutex();
            m_IDLock = new Mutex();
            m_ExitCond = new AutoResetEvent(false);
        }

        ~CRcvQueue()
{
   m_bClosing = true;

      if (null != m_WorkerThread)
        m_ExitCond.WaitOne(-1);


    //m_WorkerThread.Abort();
    m_PassLock.Close();
    m_PassCond.Close();
    //m_LSLock.Close();
    m_IDLock.Close();
    m_ExitCond.Close();

   //delete m_pRcvUList;
   //delete m_pHash;
   //delete m_pRendezvousQueue;

   //for (map<Int32, CPacket*>::iterator i = m_mBuffer.begin(); i != m_mBuffer.end(); ++ i)
   //{
   //   delete [] i.second.m_pcData;
   //   delete i.second;
   //}
}

        // Functionality:
        //    Initialize the receiving queue.
        // Parameters:
        //    1) [in] size: queue size
        //    2) [in] mss: maximum packet size
        //    3) [in] version: IP version
        //    4) [in] hsize: hash table size
        //    5) [in] c: UDP channel to be associated to the queue
        //    6) [in] t: timer
        // Returned value:
        //    None.

        public void init(int qsize, int payload, AddressFamily version, int hsize, CChannel[] cc, CClock[] t)
        {
            m_iPayloadSize = payload;

            m_UnitQueue.init(qsize, payload, version);

            m_pHash = new CHash();

            m_pChannel = cc;
            m_pTimer = t;

            m_pRcvUList = new CRcvUList();
            m_pRendezvousQueue = new CRendezvousQueue();

            //DWORD threadID;
            //m_WorkerThread = CreateThread(null, 0, CRcvQueue.worker, this, 0, &threadID);
            m_WorkerThread = new Thread(QRcvQueue.worker);
            m_WorkerThread.Start(this);

            if (null == m_WorkerThread)
                throw new CUDTException(3, 1);
        }

        // Functionality:
        //    Read a packet for a specific UDT socket id.
        // Parameters:
        //    1) [in] id: Socket ID
        //    2) [out] packet: received packet
        // Returned value:
        //    Data size of the packet

        public int recvfrom(int id, ref CPacket packet)
        {
            packet = null;

            CGuard bufferlock = new CGuard(m_PassLock);

            // Try to find a packet for the specified
            // socket ID
            CPacket workPacket;
            if (!m_mBuffer.TryGetValue(id, out workPacket))
            {
                // If we didn't find the packet waiting already
                // Then wait around until a packet arrives
                m_PassLock.ReleaseMutex();
                m_PassCond.WaitOne(1000);
                m_PassLock.WaitOne(-1);

                // Try to find it again
                if (!m_Buffer.TryGetValue(id, out workPacket))
                {
                    packet.setLength(-1);
                    return -1;
                }
            }

            if (packet.getLength() < i.second.getLength())
            {
                packet.setLength(-1);
                return -1;
            }

            workPacket.m_nHeader.CopyTo(packet.m_nHeader, 0);
            workPacket.m_pcData.CopyTo(packet.m_pcData, 0);
            packet.setLength(workPacket.getLength());

            m_mBuffer.Remove(i);

            return packet.getLength();
        }

        public static DWORD worker(Object param)
        {
            CRcvQueue self = (CRcvQueue)param;

            IPEndPoint addr = (AddressFamily.InterNetwork == self.m_UnitQueue.m_iIPversion) ? new IPEndPoint(IPAddress.Any, 0) : new IPEndPoint(IPAddress.IPv6Any, 0);
            CUDT u = null;
            Int32 id;

            while (!self.m_bClosing)
            {
                self.m_pTimer.Tick();

                // check waiting list, if new socket, insert it to the list
                if (self.ifNewEntry())
                {
                    CUDT ne = self.getNewEntry();
                    if (null != ne)
                    {
                        self.m_pRcvUList.insert(ne);
                        self.m_pHash.Insert(ne.m_SocketID, ne);
                    }
                }

                // find next available slot for incoming packet
                CUnit unit = self.m_UnitQueue.getNextAvailUnit();
                if (null == unit)
                {
                    // no space, skip this packet
                    CPacket temp;
                    temp.m_pcData = new byte[self.m_iPayloadSize];
                    temp.setLength(self.m_iPayloadSize);
                    self.m_pChannel.recvfrom(addr, temp);
                    //delete [] temp.m_pcData;
                    goto TIMER_CHECK;
                }

                unit.m_Packet.setLength(self.m_iPayloadSize);

                // reading next incoming packet
                if (self.m_pChannel.recvfrom(addr, unit.m_Packet) <= 0)
                    goto TIMER_CHECK;

                id = unit.m_Packet.m_iID;

                // ID 0 is for connection request, which should be passed to the listening socket or rendezvous sockets
                if (0 == id)
                {
                    if (null != self.m_pListener)
                        ((CUDT*)self.m_pListener).listen(addr, unit.m_Packet);
                    else if (self.m_pRendezvousQueue.retrieve(addr, id))
                        self.storePkt(id, unit.m_Packet.clone());
                }
                else if (id > 0)
                {
                    if (null != (u = self.m_pHash.Lookup(id)))
                    {
                        if (CIPAddress.ipcmp(addr, u.m_pPeerAddr, u.m_iIPversion))
                        {
                            if (u.m_bConnected && !u.m_bBroken && !u.m_bClosing)
                            {
                                if (0 == unit.m_Packet.getFlag())
                                    u.processData(unit);
                                else
                                    u.processCtrl(unit.m_Packet);

                                u.checkTimers();
                                self.m_pRcvUList.update(u);
                            }
                        }
                    }
                    else if (self.m_pRendezvousQueue.retrieve(addr, id))
                        self.storePkt(id, unit.m_Packet.clone());
                }

            TIMER_CHECK:
                // take care of the timing event for all UDT sockets

                CRNode ul = self.m_pRcvUList.m_pUList;
                Int64 currtime;
                CClock.rdtsc(out currtime);
                Int64 ctime = currtime - CClock.gMhzFactor * CClock.CPUFrequency;

                while ((null != ul) && (ul.m_llTimeStamp < ctime))
                {
                    CUDT u = ul.m_pUDT;

                    if (u.m_bConnected && !u.m_bBroken && !u.m_bClosing)
                    {
                        u.checkTimers();
                        self.m_pRcvUList.update(u);
                    }
                    else
                    {
                        // the socket must be removed from Hash table first, then RcvUList
                        self.m_pHash.Remove(u.m_SocketID);
                        self.m_pRcvUList.remove(u);
                    }

                    ul = self.m_pRcvUList.m_pUList;
                }
            }

            // Cleanup allocations if necessary
            addr.Dispose();

            // Signal exit condition for those who might be waiting
            self.m_ExitCond.Set();

            return 0;
        }

        public void storePkt(Int32 id, CPacket pkt)
        {
            m_PassLock.WaitOne(-1);

            // if the packet already exists for the ID
            // then replace it, otherwise, add it
            if (m_mBuffer.ContainsKey(id))
            {
                m_mBuffer.Remove(id);
            }

            m_mBuffer.Add(id, pkt);

            m_PassLock.ReleaseMutex();
            m_PassCond.Set();
        }

        public int setListener(CUDT u)
        {
            lock (m_LSLock)
            {
                if (null != m_pListener)
                    return -1;

                m_pListener = u;
            }

            return 1;
        }

        public void removeListener(CUDT u)
        {
            lock (m_LSLock)
            {
                if (u == m_pListener)
                    m_pListener = null;
            }
        }

        public void setNewEntry(CUDT u)
        {
            CGuard listguard = new CGuard(m_IDLock);
            m_vNewEntry.insert(m_vNewEntry.end(), u);
        }

        public bool ifNewEntry()
        {
            return !(m_vNewEntry.empty());
        }

        public CUDT getNewEntry()
        {
            lock (m_IDLock)
            {

                if (m_vNewEntry.empty())
                    return null;

                CUDT u = (CUDT) * (m_vNewEntry.begin());
                m_vNewEntry.erase(m_vNewEntry.begin());
            }

            return u;
        }
    }
}