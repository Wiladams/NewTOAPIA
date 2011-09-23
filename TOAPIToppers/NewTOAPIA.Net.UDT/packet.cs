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
   Yunhong Gu, last updated 07/13/2009
*****************************************************************************/


//////////////////////////////////////////////////////////////////////////////
//    0                   1                   2                   3
//    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                        Packet Header                          |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                                                               |
//   ~              Data / Control Information Field                 ~
//   |                                                               |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//
//    0                   1                   2                   3
//    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |0|                        Sequence Number                      |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |ff |o|                     Message Number                      |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                          Time Stamp                           |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                     Destination Socket ID                     |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//
//   bit 0:
//      0: Data Packet
//      1: Control Packet
//   bit ff:
//      11: solo message packet
//      10: first packet of a message
//      01: last packet of a message
//   bit o:
//      0: in order delivery not required
//      1: in order delivery required
//
//    0                   1                   2                   3
//    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |1|            Type             |             Reserved          |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                       Additional Info                         |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                          Time Stamp                           |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |                     Destination Socket ID                     |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//
//   bit 1-15:
//      0: Protocol Connection Handshake
//              Add. Info:    Undefined
//              Control Info: Handshake information (see CHandShake)
//      1: Keep-alive
//              Add. Info:    Undefined
//              Control Info: None
//      2: Acknowledgement (ACK)
//              Add. Info:    The ACK sequence number
//              Control Info: The sequence number to which (but not include) all the previous packets have beed received
//              Optional:     RTT
//                            RTT Variance
//                            advertised flow window size (number of packets)
//                            estimated bandwidth (number of packets per second)
//      3: Negative Acknowledgement (NAK)
//              Add. Info:    Undefined
//              Control Info: Loss list (see loss list coding below)
//      4: Congestion Warning
//              Add. Info:    Undefined
//              Control Info: None
//      5: Shutdown
//              Add. Info:    Undefined
//              Control Info: None
//      6: Acknowledgement of Acknowledement (ACK-square)
//              Add. Info:    The ACK sequence number
//              Control Info: None
//      7: Message Drop Request
//              Add. Info:    Message ID
//              Control Info: first sequence number of the message
//                            last seqeunce number of the message
//      0x7FFF: Explained by bits 16 - 31
//              
//   bit 16 - 31:
//      This space is used for future expansion or user defined control packets. 
//
//    0                   1                   2                   3
//    0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |1|                 Sequence Number a (first)                   |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |0|                 Sequence Number b (last)                    |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//   |0|                 Sequence Number (single)                    |
//   +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
//
//   Loss List Field Coding:
//      For any consectutive lost seqeunce numbers that the differnece between
//      the last and first is more than 1, only record the first (a) and the
//      the last (b) sequence numbers in the loss list field, and modify the
//      the first bit of a to 1.
//      For any single loss or consectutive loss less than 2 packets, use
//      the original sequence numbers in the field.

namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;

    public class CPacket
    {
        public struct iovec
        {
            internal int iov_len;
            internal IntPtr iov_base;
        };

        public Int32 m_iSeqNo;                   // alias: sequence number
        public Int32 m_iMsgNo;                   // alias: message number
        public Int32 m_iTimeStamp;               // alias: timestamp
        public Socket m_iID;			// alias: socket ID
        public byte[] m_pcData;                     // alias: data/control information

        public UInt32[] m_nHeader = new UInt32[4];               // The 128-bit header field
        public iovec[] m_PacketVector = new iovec[2];             // The 2-demension vector of UDT packet [header, data]

        Int32 __pad;

        public const int m_iPktHdrSize = 16;	// packet header size



        // Set up the aliases in the constructure
        public CPacket()
        {
            //m_iSeqNo = ((Int32&)(m_nHeader[0]));
            //m_iMsgNo = ((Int32&)(m_nHeader[1]));
            //m_iTimeStamp = ((Int32&)(m_nHeader[2]));
            //m_iID = ((Int32&)(m_nHeader[3]));
            m_pcData = ((m_PacketVector[1].iov_base));
            //__pad()

            //m_PacketVector[0].iov_base = (IntPtr)m_nHeader;
            m_PacketVector[0].iov_len = m_iPktHdrSize;
        }

        //CPacket::~CPacket()
        //{
        //}

        // Functionality:
        //    Get the payload or the control information field length.
        // Parameters:
        //    None.
        // Returned value:
        //    the payload or the control information field length.

        public int getLength()
        {
            return m_PacketVector[1].iov_len;
        }

        // Functionality:
        //    Set the payload or the control information field length.
        // Parameters:
        //    0) [in] len: the payload or the control information field length.
        // Returned value:
        //    None.

        internal void setLength(int len)
        {
            m_PacketVector[1].iov_len = len;
        }

        // Functionality:
        //    Pack a Control packet.
        // Parameters:
        //    0) [in] pkttype: packet type field.
        //    1) [in] lparam: pointer to the first data structure, explained by the packet type.
        //    2) [in] rparam: pointer to the second data structure, explained by the packet type.
        //    3) [in] size: size of rparam, in number of bytes;
        // Returned value:
        //    None.

        void pack(int pkttype, Object lparam, IntPtr rparam, int size)
        {
           // Set (bit-0 = 1) and (bit-1~15 = type)
           m_nHeader[0] = (0x80000000 | ((pkttype << 16)));

           // Set additional information and control information field
           switch (pkttype)
           {
           case 2: //0010 - Acknowledgement (ACK)
              // ACK packet seq. no.
              if (null != lparam)
                 m_nHeader[1] = (int)lparam;

              // data ACK seq. no. 
              // optional: RTT (microsends), RTT variance (microseconds) advertised flow window size (packets), and estimated link capacity (packets per second)
              m_PacketVector[1].iov_base = (char *)rparam;
              m_PacketVector[1].iov_len = size;

              break;

           case 6: //0110 - Acknowledgement of Acknowledgement (ACK-2)
              // ACK packet seq. no.
              m_nHeader[1] = (int)lparam;

              // control info field should be none
              // but "writev" does not allow this
              m_PacketVector[1].iov_base = (char *)&__pad; //null;
              m_PacketVector[1].iov_len = 4; //0;

              break;

           case 3: //0011 - Loss Report (NAK)
              // loss list
              m_PacketVector[1].iov_base = (char *)rparam;
              m_PacketVector[1].iov_len = size;

              break;

           case 4: //0100 - Congestion Warning
              // control info field should be none
              // but "writev" does not allow this
              m_PacketVector[1].iov_base = (char *)&__pad; //null;
              m_PacketVector[1].iov_len = 4; //0;

              break;

           case 1: //0001 - Keep-alive
              // control info field should be none
              // but "writev" does not allow this
              m_PacketVector[1].iov_base = (char *)&__pad; //null;
              m_PacketVector[1].iov_len = 4; //0;

              break;

           case 0: //0000 - Handshake
              // control info filed is handshake info
              m_PacketVector[1].iov_base = (char *)rparam;
              m_PacketVector[1].iov_len = size; //sizeof(CHandShake);

              break;

           case 5: //0101 - Shutdown
              // control info field should be none
              // but "writev" does not allow this
              m_PacketVector[1].iov_base = (char *)&__pad; //null;
              m_PacketVector[1].iov_len = 4; //0;

              break;

           case 7: //0111 - Message Drop Request
              // msg id 
              m_nHeader[1] = *(Int32 *)lparam;

              //first seq no, last seq no
              m_PacketVector[1].iov_base = (char *)rparam;
              m_PacketVector[1].iov_len = size;

              break;

           case 32767: //0x7FFF - Reserved for user defined control packets
              // for extended control packet
              // "lparam" contains the extended type information for bit 16 - 31
              // "rparam" is the control information
              m_nHeader[0] |= *(Int32 *)lparam;

              if (null != rparam)
              {
                 m_PacketVector[1].iov_base = (char *)rparam;
                 m_PacketVector[1].iov_len = size;
              }
              else
              {
                 m_PacketVector[1].iov_base = (char *)&__pad;
                 m_PacketVector[1].iov_len = 4;
              }

              break;

           default:
              break;
           }
        }

        // Functionality:
        //    Read the packet vector.
        // Parameters:
        //    None.
        // Returned value:
        //    Pointer to the packet vector.
        iovec[] getPacketVector()
        {
            return m_PacketVector;
        }

        // Functionality:
        //    Read the packet flag.
        // Parameters:
        //    None.
        // Returned value:
        //    packet flag (0 or 1).
        public int getFlag()
        {
            // read bit 0
            return (int)(m_nHeader[0] >> 31);
        }

        // Functionality:
        //    Read the packet type.
        // Parameters:
        //    None.
        // Returned value:
        //    packet type filed (000 ~ 111).
        int getType()
        {
            // read bit 1~15
            return (int)((m_nHeader[0] >> 16) & 0x00007FFF);
        }

        // Functionality:
        //    Read the extended packet type.
        // Parameters:
        //    None.
        // Returned value:
        //    extended packet type filed (0x000 ~ 0xFFF).
        int getExtendedType()
        {
            // read bit 16~31
            return (int)(m_nHeader[0] & 0x0000FFFF);
        }

        // Functionality:
        //    Read the ACK-2 seq. no.
        // Parameters:
        //    None.
        // Returned value:
        //    packet header field (bit 16~31).
        int getAckSeqNo()
        {
            // read additional information field
            return (Int32)m_nHeader[1];
        }

        // Functionality:
        //    Read the message boundary flag bit.
        // Parameters:
        //    None.
        // Returned value:
        //    packet header field [1] (bit 0~1).

        int getMsgBoundary()
        {
            // read [1] bit 0~1
            return (int)(m_nHeader[1] >> 30);
        }

        // Functionality:
        //    Read the message inorder delivery flag bit.
        // Parameters:
        //    None.
        // Returned value:
        //    packet header field [1] (bit 2).

        bool getMsgOrderFlag()
        {
            // read [1] bit 2
            return (1 == ((m_nHeader[1] >> 29) & 1));
        }

        // Functionality:
        //    Read the message sequence number.
        // Parameters:
        //    None.
        // Returned value:
        //    packet header field [1] (bit 3~31).

        int getMsgSeq()
        {
            // read [1] bit 3~31
            return (int)(m_nHeader[1] & 0x1FFFFFFF);
        }

        // Functionality:
        //    Clone this packet.
        // Parameters:
        //    None.
        // Returned value:
        //    Pointer to the new packet.
        CPacket clone()
        {
            CPacket pkt = new CPacket();
            m_nHeader.CopyTo(pkt.m_nHeader, m_iPktHdrSize);
            //pkt.m_pcData = new byte[m_PacketVector[1].iov_len];
            //memcpy(pkt.m_pcData, m_pcData, m_PacketVector[1].iov_len);
            pkt.m_PacketVector[1].iov_len = m_PacketVector[1].iov_len;

            return pkt;
        }
    }
}