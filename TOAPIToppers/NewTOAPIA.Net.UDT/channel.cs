/*****************************************************************************
Copyright (c) 2001 - 2008, The Board of Trustees of the University of Illinois.
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
   Yunhong Gu, last updated 05/23/2008
*****************************************************************************/

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    using SOCKET = System.Int32;
    using UDPSOCKET = System.Int32;
    using DWORD = System.UInt32;

    public class CChannel
    {
        private AddressFamily m_iIPversion;                    // IP version

        private Socket m_iSocket = null;

        private int m_iSndBufSize = 65536;                   // UDP sending buffer size
        private int m_iRcvBufSize = 65536;                   // UDP receiving buffer size

        public CChannel()
            : this(AddressFamily.InterNetwork) //AF_INET
        {
        }

        public CChannel(AddressFamily version)
        {
            m_iIPversion = (version);
        }

        ~CChannel()
        {
        }

        // Functionality:
        //    Opne a UDP channel.
        // Parameters:
        //    0) [in] addr: The local address that UDP will use.
        // Returned value:
        //    None.

        public void open(IPEndPoint addr)
        {
            // construct an socket
            m_iSocket = new Socket(m_iIPversion, SocketType.Dgram, ProtocolType.IP);

            if (null != addr)
            {
                m_iSocket.Bind(addr);
                //socklen_t namelen = (AddressFamily.InterNetwork == m_iIPversion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6);

                //if (0 != bind(m_iSocket, addr, namelen))
                //   throw new CUDTException(1, 3, NET_ERROR);
            }
            else
            {
                //sendto or WSASendTo will also automatically bind the socket
                //   addrinfo hints;
                //   addrinfo* res;

                //   memset(&hints, 0, sizeof(struct addrinfo));

                //   hints.ai_flags = AI_PASSIVE;
                //   hints.ai_family = m_iIPversion;
                //   hints.ai_socktype = Winsock.SOCK_DGRAM;

                //   if (0 != getaddrinfo(null, "0", &hints, &res))
                //      throw new CUDTException(1, 3, NET_ERROR);

                //   if (0 != bind(m_iSocket, res.ai_addr, res.ai_addrlen))
                //      throw new CUDTException(1, 3, NET_ERROR);

                //   freeaddrinfo(res);
            }

            setUDPSockOpt();
        }

        // Functionality:
        //    Opne a UDP channel based on an existing UDP socket.
        // Parameters:
        //    0) [in] udpsock: UDP socket descriptor.
        // Returned value:
        //    None.

        public void open(Socket udpsock)
        {
            m_iSocket = udpsock;
            setUDPSockOpt();
        }

        // Functionality:
        //    Disconnect and close the UDP entity.
        // Parameters:
        //    None.
        // Returned value:
        //    None.

        public void close()
        {
            m_iSocket.Close();
        }

        // Functionality:
        //    Get the UDP sending buffer size.
        // Parameters:
        //    None.
        // Returned value:
        //    Current UDP sending buffer size.

        public int getSndBufSize()
        {
            m_iSndBufSize = m_iSocket.SendBufferSize;

            //socklen_t size = sizeof(socklen_t);

            //getsockopt(m_iSocket, SOL_SOCKET, SO_SNDBUF, (char *)&m_iSndBufSize, &size);

            return m_iSndBufSize;
        }

        // Functionality:
        //    Get the UDP receiving buffer size.
        // Parameters:
        //    None.
        // Returned value:
        //    Current UDP receiving buffer size.

        public int getRcvBufSize()
        {
            m_iRcvBufSize = m_iSocket.ReceiveBufferSize;
            //socklen_t size = sizeof(socklen_t);

            //getsockopt(m_iSocket, SOL_SOCKET, SO_RCVBUF, (char *)&m_iRcvBufSize, &size);

            return m_iRcvBufSize;
        }

        // Functionality:
        //    Set the UDP sending buffer size.
        // Parameters:
        //    0) [in] size: expected UDP sending buffer size.
        // Returned value:
        //    None.

        public void setSndBufSize(int size)
        {
            m_iSocket.SendBufferSize = size;
            m_iSndBufSize = size;
        }

        // Functionality:
        //    Set the UDP receiving buffer size.
        // Parameters:
        //    0) [in] size: expected UDP receiving buffer size.
        // Returned value:
        //    None.

        public void setRcvBufSize(int size)
        {
            m_iSocket.ReceiveBufferSize = size;
            m_iRcvBufSize = size;
        }

        // Functionality:
        //    Query the socket address that the channel is using.
        // Parameters:
        //    0) [out] addr: pointer to store the returned socket address.
        // Returned value:
        //    None.

        public void getSockAddr(out IPEndPoint addr)
        {
            addr = (IPEndPoint)m_iSocket.LocalEndPoint;
        }

        // Functionality:
        //    Query the peer side socket address that the channel is connect to.
        // Parameters:
        //    0) [out] addr: pointer to store the returned socket address.
        // Returned value:
        //    None.

        public void getPeerAddr(out IPAddress addr)
        {
            addr = m_iSocket.RemoteEndPoint;
        }

        // Functionality:
        //    Send a packet to the given address.
        // Parameters:
        //    0) [in] addr: pointer to the destination address.
        //    1) [in] packet: reference to a CPacket entity.
        // Returned value:
        //    Actual size of data sent.

        public int sendto(IPEndPoint addr, CPacket packet)
{
   // convert control information into network order
   if (packet.getFlag() > 0)
      for (int i = 0, n = packet.getLength() / 4; i < n; ++ i)
         *((uint *)packet.m_pcData + i) = htonl(*((uint *)packet.m_pcData + i));

   // convert packet header into network order
   //for (int j = 0; j < 4; ++ j)
   //   packet.m_nHeader[j] = htonl(packet.m_nHeader[j]);
   uint* p = packet.m_nHeader;
   for (int j = 0; j < 4; ++ j)
   {
      *p = htonl(*p);
      ++ p;
   }

      DWORD size = CPacket.m_iPktHdrSize + packet.getLength();
      //int addrsize = (AF_INET == m_iIPversion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6);
      //int res = WSASendTo(m_iSocket, (LPWSABUF)packet.m_PacketVector, 2, &size, 0, addr, addrsize, null, null);
      //int res = m_iSocket.SendTo(Buffer, addr);
      int res = m_iSocket.SendTo(Buffer, 0, size, SocketFlags.None, addr);

      res = (0 == res) ? size : -1;

   // convert back into local host order
   //for (int k = 0; k < 4; ++ k)
   //   packet.m_nHeader[k] = ntohl(packet.m_nHeader[k]);
   p = packet.m_nHeader;
   for (int k = 0; k < 4; ++ k)
   {
      *p = ntohl(*p);
       ++ p;
   }

   if (packet.getFlag() > 0)
      for (int l = 0, n = packet.getLength() / 4; l < n; ++ l)
         packet.m_pcData[l] = ntohl(packet.m_pcData[l]);

   return res;
}
        // Functionality:
        //    Receive a packet from the channel and record the source address.
        // Parameters:
        //    0) [in] addr: pointer to the source address.
        //    1) [in] packet: reference to a CPacket entity.
        // Returned value:
        //    Actual size of data received.

        public int recvfrom(IPAddress addr, CPacket packet)
{
      DWORD size = CPacket.m_iPktHdrSize + packet.getLength();
      DWORD flag = 0;
      int addrsize = (AF_INET == m_iIPversion) ? sizeof(sockaddr_in) : sizeof(sockaddr_in6);

      int res = WSARecvFrom(m_iSocket, (LPWSABUF)packet.m_PacketVector, 2, &size, &flag, addr, &addrsize, null, null);
      res = (0 == res) ? size : -1;

   if (res <= 0)
   {
      packet.setLength(-1);
      return -1;
   }

   packet.setLength(res - CPacket.m_iPktHdrSize);

   // convert back into local host order
   //for (int i = 0; i < 4; ++ i)
   //   packet.m_nHeader[i] = ntohl(packet.m_nHeader[i]);
   uint* p = packet.m_nHeader;
   for (int i = 0; i < 4; ++ i)
   {
      *p = ntohl(*p);
      ++ p;
   }

   if (packet.getFlag())
      for (int j = 0, n = packet.getLength() / 4; j < n; ++ j)
         *((uint *)packet.m_pcData + j) = ntohl(*((uint *)packet.m_pcData + j));

   return packet.getLength();
        }

        private void setUDPSockOpt()
        {
            // for other systems, if requested is greated than maximum, the maximum value will be automactally used
            if ((0 != setsockopt(m_iSocket, SOL_SOCKET, SO_RCVBUF, (char*)&m_iRcvBufSize, sizeof(int))) ||
                (0 != setsockopt(m_iSocket, SOL_SOCKET, SO_SNDBUF, (char*)&m_iSndBufSize, sizeof(int))))
                throw new CUDTException(1, 3, NET_ERROR);

            timeval tv;
            tv.tv_sec = 0;
            tv.tv_usec = 100;

            DWORD ot = 1; //milliseconds
            if (0 != setsockopt(m_iSocket, SOL_SOCKET, SO_RCVTIMEO, (char*)&ot, sizeof(DWORD)))
                throw new CUDTException(1, 3, NET_ERROR);
            // Set receiving time-out value
            if (0 != setsockopt(m_iSocket, SOL_SOCKET, SO_RCVTIMEO, (char*)&tv, sizeof(timeval)))
                throw new CUDTException(1, 3, NET_ERROR);
        }
    }
}
