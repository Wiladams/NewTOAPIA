namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using UDTSOCKET = System.Int32;

    public class CRendezvousQueue
    {
        private class CRL
        {
            internal UDTSOCKET m_iID;
            internal int m_iIPversion;
            internal IPAddress m_pPeerAddr;
        };

        private List<CRL> m_vRendezvousID = new List<CRL>();         // The sockets currently in rendezvous mode

        private Mutex m_RIDVectorLock = new Mutex();


        public void insert(UDTSOCKET id, AddressFamily ipv, IPAddress addr)
        {
            CGuard vg = new CGuard(m_RIDVectorLock);

            CRL r = new CRL();
            r.m_iID = id;
            r.m_iIPversion = ipv;
            r.m_pPeerAddr = addr;
            //r.m_pPeerAddr = (AF_INET == ipv) ? (sockaddr*)new sockaddr_in : (sockaddr*)new sockaddr_in6;
            //memcpy(r.m_pPeerAddr, addr, (AF_INET == ipv) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6));

            m_vRendezvousID.Add(r);
        }

        public void remove(UDTSOCKET id)
        {
            lock (m_RIDVectorLock)
            {
                for (int i = 0; i < m_vRendezvousID.Count; i++)
                {
                    CRL tmpCRL = m_vRendezvousID[i];
                    if (tmpCRL.m_iID == id)
                    {
                        //tmpCRL.m_pPeerAddr.Dispose();

                        m_vRendezvousID.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        public bool retrieve(IPAddress addr, UDTSOCKET id)
        {
            CGuard vg = new CGuard(m_RIDVectorLock);

            foreach (CRL i in m_vRendezvousID)
            {
                if (CIPAddress.ipcmp(addr, i.m_pPeerAddr, i.m_iIPversion) && ((0 == id) || (id == i.m_iID)))
                {
                    id = i.m_iID;
                    return true;
                }
            }

            return false;
        }
    }
}