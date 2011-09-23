namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.IO;

public class CRcvBuffer
{
   CUnit[] m_pUnit;                     // pointer to the protocol buffer
   int m_iSize;                         // size of the protocol buffer
   CUnitQueue m_pUnitQueue;		// the shared unit queue

   int m_iStartPos;                     // the head position for I/O (inclusive)
   int m_iLastAckPos;                   // the last ACKed position (exclusive)
					// EMPTY: m_iStartPos = m_iLastAckPos   FULL: m_iStartPos = m_iLastAckPos + 1
   int m_iMaxPos;			// the furthest data position

   int m_iNotch;			// the starting read point of the first unit

    public CRcvBuffer(CUnitQueue queue)
        :this(65536, queue)
{
}

    public CRcvBuffer(int bufsize, CUnitQueue queue)
{
m_pUnit = null;
m_iSize = (bufsize);
m_pUnitQueue = (queue);
m_iStartPos = (0);
m_iLastAckPos = (0);
m_iMaxPos = (-1);
m_iNotch = (0);

    m_pUnit = new CUnit [m_iSize];
   for (int i = 0; i < m_iSize; ++ i)
      m_pUnit[i] = null;
}


//    ~CRcvBuffer()
//{
//   for (int i = 0; i < m_iSize; ++ i)
//   {
//      if (null != m_pUnit[i])
//      {
//         m_pUnit[i].m_iFlag = 0;
//         -- m_pUnitQueue.m_iCount;
//      }
//   }

//   delete [] m_pUnit;
//}

      // Functionality:
      //    Write data into the buffer.
      // Parameters:
      //    0) [in] unit: pointer to a data unit containing new packet
      //    1) [in] offset: offset from last ACK point.
      // Returned value:
      //    0 is success, -1 if data is repeated.

public int addData(CUnit unit, int offset)
{
   int pos = (m_iLastAckPos + offset) % m_iSize;
   if (offset > m_iMaxPos)
      m_iMaxPos = offset;

   if (null != m_pUnit[pos])
      return -1;
   
   m_pUnit[pos] = unit;

   unit.m_iFlag = 1;
   ++ m_pUnitQueue.m_iCount;

   return 0;
}

      // Functionality:
      //    Read data into a user buffer.
      // Parameters:
      //    0) [in] data: pointer to user buffer.
      //    1) [in] len: length of user buffer.
      // Returned value:
      //    size of data read.

public int readBuffer(byte[] data, int len)
{
   int p = m_iStartPos;
   int lastack = m_iLastAckPos;
   int rs = len;

   while ((p != lastack) && (rs > 0))
   {
      int unitsize = m_pUnit[p].m_Packet.getLength() - m_iNotch;
      if (unitsize > rs)
         unitsize = rs;

      memcpy(data, m_pUnit[p].m_Packet.m_pcData + m_iNotch, unitsize);
      data += unitsize;

      if ((rs > unitsize) || (rs == m_pUnit[p].m_Packet.getLength() - m_iNotch))
      {
         CUnit tmp = m_pUnit[p];
         m_pUnit[p] = null;
         tmp.m_iFlag = 0;
         -- m_pUnitQueue.m_iCount;

         if (++ p == m_iSize)
            p = 0;

         m_iNotch = 0;
      }
      else
         m_iNotch += rs;

      rs -= unitsize;
   }

   m_iStartPos = p;
   return len - rs;
}

      // Functionality:
      //    Read data directly into file.
      // Parameters:
      //    0) [in] file: C++ file stream.
      //    1) [in] len: expected length of data to write into the file.
      // Returned value:
      //    size of data read.

public int readBufferToFile(Stream ofs, int len)
{
   int p = m_iStartPos;
   int lastack = m_iLastAckPos;
   int rs = len;

   while ((p != lastack) && (rs > 0))
   {
      int unitsize = m_pUnit[p].m_Packet.getLength() - m_iNotch;
      if (unitsize > rs)
         unitsize = rs;

      //ofs.write(m_pUnit[p].m_Packet.m_pcData + m_iNotch, unitsize);
       ofs.Write(m_pUnit[p].m_Packet.m_pcData, m_iNotch, unitsize);

      if ((rs > unitsize) || (rs == m_pUnit[p].m_Packet.getLength() - m_iNotch))
      {
         CUnit tmp = m_pUnit[p];
         m_pUnit[p] = null;
         tmp.m_iFlag = 0;
         -- m_pUnitQueue.m_iCount;

         if (++ p == m_iSize)
            p = 0;

         m_iNotch = 0;
      }
      else
         m_iNotch += rs;

      rs -= unitsize;
   }

   m_iStartPos = p;
   return len - rs;
}

      // Functionality:
      //    Update the ACK point of the buffer.
      // Parameters:
      //    0) [in] len: size of data to be acknowledged.
      // Returned value:
      //    1 if a user buffer is fulfilled, otherwise 0.

public void ackData(int len)
{
   m_iLastAckPos = (m_iLastAckPos + len) % m_iSize;
   m_iMaxPos -= len;

   CClock.triggerEvent();
}

      // Functionality:
      //    Query how many buffer space left for data receiving.
      // Parameters:
      //    None.
      // Returned value:
      //    size of available buffer space (including user buffer) for data receiving.

public int getAvailBufSize()
{
   // One slot must be empty in order to tell the difference between "empty buffer" and "full buffer"
   return m_iSize - getRcvDataSize() - 1;
}

      // Functionality:
      //    Query how many data has been continuously received (for reading).
      // Parameters:
      //    None.
      // Returned value:
      //    size of valid (continous) data for reading.

public int getRcvDataSize()
{
   if (m_iLastAckPos >= m_iStartPos)
      return m_iLastAckPos - m_iStartPos;

   return m_iSize + m_iLastAckPos - m_iStartPos;
}

      // Functionality:
      //    mark the message to be dropped from the message list.
      // Parameters:
      //    0) [in] msgno: message nuumer.
      // Returned value:
      //    None.

public void dropMsg(Int32 msgno)
{
   for (int i = m_iStartPos, n = (m_iLastAckPos + m_iMaxPos) % m_iSize; i != n; i = (i + 1) % m_iSize)
      if ((null != m_pUnit[i]) && (msgno == m_pUnit[i].m_Packet.m_iMsgNo))
         m_pUnit[i].m_iFlag = 3;
}

      // Functionality:
      //    read a message.
      // Parameters:
      //    0) [out] data: buffer to write the message into.
      //    1) [in] len: size of the buffer.
      // Returned value:
      //    actuall size of data read.

public int readMsg(byte[] data,  int len)
{
   int p, q;
   bool passack;
   if (!scanMsg(p, q, passack))
      return 0;

   int rs = len;
   while (p != (q + 1) % m_iSize)
   {
      int unitsize = m_pUnit[p].m_Packet.getLength();
      if ((rs >= 0) && (unitsize > rs))
         unitsize = rs;

      if (unitsize > 0)
      {
         memcpy(data, m_pUnit[p].m_Packet.m_pcData, unitsize);
         data += unitsize;
         rs -= unitsize;
      }

      if (!passack)
      {
         CUnit* tmp = m_pUnit[p];
         m_pUnit[p] = null;
         tmp.m_iFlag = 0;
         -- m_pUnitQueue.m_iCount;
      }
      else
         m_pUnit[p].m_iFlag = 2;

      if (++ p == m_iSize)
         p = 0;
   }

   if (!passack)
      m_iStartPos = (q + 1) % m_iSize;

   return len - rs;
}

      // Functionality:
      //    Query how many messages are available now.
      // Parameters:
      //    None.
      // Returned value:
      //    number of messages available for recvmsg.

public int getRcvMsgNum()
{
   int p, q;
   bool passack;
   return scanMsg(p, q, passack) ? 1 : 0;
}

private bool scanMsg(int p, int q, bool passack)
{
   // empty buffer
   if ((m_iStartPos == m_iLastAckPos) && (0 == m_iMaxPos))
      return false;

   //skip all bad msgs at the beginning
   while (m_iStartPos != m_iLastAckPos)
   {
      if (null == m_pUnit[m_iStartPos])
      {
         if (++ m_iStartPos == m_iSize)
            m_iStartPos = 0;
         continue;
      }

      if ((1 == m_pUnit[m_iStartPos].m_iFlag) && (m_pUnit[m_iStartPos].m_Packet.getMsgBoundary() > 1))
         break;

      CUnit* tmp = m_pUnit[m_iStartPos];
      m_pUnit[m_iStartPos] = null;
      tmp.m_iFlag = 0;
      -- m_pUnitQueue.m_iCount;

      if (++ m_iStartPos == m_iSize)
         m_iStartPos = 0;
   }

   p = -1;                  // message head
   q = m_iStartPos;         // message tail
   passack = m_iStartPos == m_iLastAckPos;
   bool found = false;

   // looking for the first message
   for (int i = 0, n = m_iMaxPos + getRcvDataSize(); i <= n; ++ i)
   {
      if ((null != m_pUnit[q]) && (1 == m_pUnit[q].m_iFlag))
      {
         switch (m_pUnit[q].m_Packet.getMsgBoundary())
         {
         case 3: // 11
            p = q;
            found = true;
            break;

         case 2: // 10
            p = q;
            break;

         case 1: // 01
            if (p != -1)
               found = true;
         }
      }
      else
      {
         // a hole in this message, not valid, restart search
         p = -1;
      }

      if (found)
      {
         // the msg has to be ack'ed or it is allowed to read out of order, and was not read before
         if (!passack || !m_pUnit[q].m_Packet.getMsgOrderFlag())
            break;

         found = false;
      }

      if (++ q == m_iSize)
         q = 0;

      if (q == m_iLastAckPos)
         passack = true;
   }

   // no msg found
   if (!found)
   {
      // if the message is larger than the receiver buffer, return part of the message
      if ((p != -1) && ((q + 1) % m_iSize == p))
         found = true;
   }

   return found;
}


//private:
//   CRcvBuffer(const CRcvBuffer&);
//   CRcvBuffer& operator=(const CRcvBuffer&);
}
}