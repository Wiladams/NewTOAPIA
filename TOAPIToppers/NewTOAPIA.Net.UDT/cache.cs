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
   Yunhong Gu, last updated 05/06/2009
*****************************************************************************/

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    //public delegate bool CIPComp(CInfoBlock ib1, CInfoBlock ib2);

    public delegate bool CTSComp(CInfoBlock ib1, CInfoBlock ib2);

    public class CTSComparer : IEqualityComparer<CInfoBlock>
    {
        public bool Equals(CInfoBlock ib1, CInfoBlock ib2)
        {
            return (ib1.m_ullTimeStamp > ib2.m_ullTimeStamp);
        }

        public int GetHashCode(CInfoBlock ib)
        {
            return ib.m_ullTimeStamp.GetHashCode();
        }
    }

    public class CIPComparer : IEqualityComparer<CInfoBlock>
    {
        public bool Equals(CInfoBlock ib1, CInfoBlock ib2)
        {
            if (ib1.m_iIPversion != ib2.m_iIPversion)
                return (ib1.m_iIPversion < ib2.m_iIPversion);
            else if (ib1.m_iIPversion == AddressFamily.InterNetwork)
                return (ib1.m_piIP[0] > ib2.m_piIP[0]);
            else
            {
                for (int i = 0; i < ib1.m_piIP.Length; ++i)
                {
                    if (ib1.m_piIP[i] != ib2.m_piIP[i])
                        return (ib1.m_piIP[i] > ib2.m_piIP[i]);
                }
                return false;
            }
        }

        public int GetHashCode(CInfoBlock ib)
        {
            return (int)ib.m_piIP.GetHashCode();
        }
    }

    public class CCache
    {
        static CIPComparer gCIPComp = new CIPComparer();
        static CTSComparer gCTSComp = new CTSComparer();

        private uint m_uiSize;
        private Dictionary<CInfoBlock,CInfoBlock> m_sIPIndex = new Dictionary<CInfoBlock,CInfoBlock>(gCIPComp);
        private Dictionary<CInfoBlock,CInfoBlock> m_sTSIndex = new Dictionary<CInfoBlock,CInfoBlock>(gCTSComp);
        private Mutex m_Lock;

        public CCache()
            : this(1024)
        {
        }

        public CCache(uint size)
        {
            m_uiSize = size;
            m_Lock = new Mutex(true);
        }

        ~CCache()
        {
            //for (set<CInfoBlock*, CTSComp>::iterator i = m_sTSIndex.begin(); i != m_sTSIndex.end(); ++ i)
            //   delete *i;

            m_Lock.Close();
        }

        public int lookup(IPAddress addr, AddressFamily ver, CInfoBlock ib)
        {
            CGuard cacheguard = new CGuard(m_Lock);

            ib.m_piIP = addr.GetAddressBytes();
            ib.m_iIPversion = ver;

            //set<CInfoBlock*, CIPComp>::iterator i = m_sIPIndex.find(ib);

            if (!m_sIPIndex.ContainsKey(ib))
                return -1;

            CInfoBlock i = m_sIPIndex[ib];
            ib.m_ullTimeStamp = (i).m_ullTimeStamp;
            ib.m_iRTT = (i).m_iRTT;
            ib.m_iBandwidth = (i).m_iBandwidth;

            return 1;
        }

        public void update(IPAddress addr, AddressFamily ver, CInfoBlock ib)
        {
            CGuard cacheguard = new CGuard(m_Lock);

            CInfoBlock newib = new CInfoBlock();
            newib.m_piIP = addr.GetAddressBytes();
            newib.m_iIPversion = ver;

            //set<CInfoBlock*, CIPComp>::iterator i = m_sIPIndex.find(newib);

            if (m_sIPIndex.ContainsKey(newib))
            {
                m_sTSIndex.Remove(newib);
                m_sIPIndex.Remove(newib);
            }

            newib.m_iRTT = ib.m_iRTT;
            newib.m_iBandwidth = ib.m_iBandwidth;
            newib.m_ullTimeStamp = CClock.getTime();

            m_sIPIndex.Add(newib, newib);
            m_sTSIndex.Add(newib, newib);

            if (m_sTSIndex.Count() > m_uiSize)
            {
                // find the first in the list and delete it
                CInfoBlock tmp = m_sIPIndex.Values.FirstOrDefault();
                m_sIPIndex.Remove(tmp);
                m_sTSIndex.Remove(tmp);
            }
        }
    }
}
