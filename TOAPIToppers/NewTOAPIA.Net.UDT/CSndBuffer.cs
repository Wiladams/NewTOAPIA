namespace NewTOAPIA.Net.Udt
{
    using System;
    using System.IO;
    using System.Threading;

    public class CSndBuffer
    {

        internal class Buffer
        {
            internal byte[] m_pcData;			// buffer
            internal int m_iSize;			// size
            internal Buffer m_pNext;			// next buffer
        }

        internal class Block
        {
            internal byte[] m_pcData;                   // pointer to the data block
            internal int m_iLength;                    // length of the block

            internal Int32 m_iMsgNo;                 // message number
            internal UInt64 m_OriginTime;            // original request time
            internal int m_iTTL;                       // time to live (milliseconds)

            internal Block m_pNext;                   // next block
        }

        Object m_BufLock = new object();           // used to synchronize buffer operation

        Block m_pBlock;         // m_pBlock:         The head pointer
        Block m_pFirstBlock;    // m_pFirstBlock:    The first block
        Block m_pCurrBlock;     // m_pCurrBlock:	The current block
        Block m_pLastBlock;     // m_pLastBlock:     The last block (if first == last, buffer is empty)

        Buffer m_pBuffer;			// physical buffer

        Int32 m_iNextMsgNo;                // next message number

        int m_iSize;				// buffer size (number of packets)
        int m_iMSS;                          // maximum seqment/packet size

        int m_iCount;			// number of used blocks

        public CSndBuffer(int size, int mss)
        {
            m_BufLock = null;
            m_pBlock = null;
            m_pFirstBlock = null;
            m_pCurrBlock = null;
            m_pLastBlock = null;
            m_pBuffer = null;
            m_iNextMsgNo = 0;
            m_iSize = size;
            m_iMSS = mss;
            m_iCount = 0;

            // initial physical buffer of "size"
            m_pBuffer = new Buffer();
            m_pBuffer.m_pcData = new byte[m_iSize * m_iMSS];
            m_pBuffer.m_iSize = m_iSize;
            m_pBuffer.m_pNext = null;

            // circular linked list for out bound packets
            m_pBlock = new Block();
            Block pb = m_pBlock;
            for (int i = 1; i < m_iSize; ++i)
            {
                pb.m_pNext = new Block();
                pb = pb.m_pNext;
            }
            pb.m_pNext = m_pBlock;

            pb = m_pBlock;
            byte[] pc = m_pBuffer.m_pcData;
            for (int i = 0; i < m_iSize; ++i)
            {
                pb.m_pcData = pc;
                pb = pb.m_pNext;
                //pc += m_iMSS;
            }

            m_pFirstBlock = m_pCurrBlock = m_pLastBlock = m_pBlock;

            m_BufLock = new Mutex();
        }

        ~CSndBuffer()
        {
            Block pb = m_pBlock.m_pNext;
            while (pb != m_pBlock)
            {
                Block temp = pb;
                pb = pb.m_pNext;
                //temp.Dispose();
            }
            //m_pBlock.Dispose();

            while (m_pBuffer != null)
            {
                Buffer temp = m_pBuffer;
                m_pBuffer = m_pBuffer.m_pNext;
                //temp.m_pcData.Dispose();
                // temp.Dispose();
            }

            m_BufLock.Close();
        }

        // Functionality:
        //    Insert a user buffer into the sending list.
        // Parameters:
        //    0) [in] data: pointer to the user data block.
        //    1) [in] len: size of the block.
        //    2) [in] ttl: time to live in milliseconds
        //    3) [in] order: if the block should be delivered in order, for DGRAM only
        // Returned value:
        //    None.

        public void addBuffer(byte[] data, int len, int ttl, bool order)
        {
            int size = len / m_iMSS;
            if ((len % m_iMSS) != 0)
                size++;

            // dynamically increase sender buffer
            while (size + m_iCount >= m_iSize)
                increase();

            Int64 time = CClock.getTime();
            Int32 inorder = order ? 1 : 0;
            inorder <<= 29;

            Block s = m_pLastBlock;
            for (int i = 0; i < size; ++i)
            {
                int pktlen = len - i * m_iMSS;
                if (pktlen > m_iMSS)
                    pktlen = m_iMSS;

                memcpy(s.m_pcData, data + i * m_iMSS, pktlen);
                s.m_iLength = pktlen;

                s.m_iMsgNo = m_iNextMsgNo | inorder;
                if (i == 0)
                    s.m_iMsgNo |= 0x80000000;
                if (i == size - 1)
                    s.m_iMsgNo |= 0x40000000;

                s.m_OriginTime = time;
                s.m_iTTL = ttl;

                s = s.m_pNext;
            }
            m_pLastBlock = s;

            lock (m_BufLock)
            {
                m_iCount += size;
            }

            m_iNextMsgNo++;
        }

        // Functionality:
        //    Read a block of data from file and insert it into the sending list.
        // Parameters:
        //    0) [in] ifs: input file stream.
        //    1) [in] len: size of the block.
        // Returned value:
        //    actual size of data added from the file.

        public int addBufferFromFile(Stream ifs, int len)
        {
            int size = len / m_iMSS;
            if ((len % m_iMSS) != 0)
                size++;

            // dynamically increase sender buffer
            while (size + m_iCount >= m_iSize)
                increase();

            Block s = m_pLastBlock;
            int total = 0;
            for (int i = 0; i < size; ++i)
            {
                if (ifs.bad() || ifs.fail() || ifs.eof())
                    break;

                int pktlen = len - i * m_iMSS;
                if (pktlen > m_iMSS)
                    pktlen = m_iMSS;

                ifs.Read(s.m_pcData, 0, pktlen);
                if ((pktlen = ifs.gcount()) <= 0)
                    break;

                s.m_iLength = pktlen;
                s.m_iTTL = -1;
                s = s.m_pNext;

                total += pktlen;
            }
            m_pLastBlock = s;

            lock (m_BufLock)
            {
                m_iCount += size;
            }

            return total;
        }

        // Functionality:
        //    Find data position to pack a DATA packet from the furthest reading point.
        // Parameters:
        //    0) [out] data: the pointer to the data position.
        //    1) [out] msgno: message number of the packet.
        // Returned value:
        //    Actual length of data read.

        public int readData(out byte[] data, Int32 msgno)
        {
            data = null;

            // No data to read
            if (m_pCurrBlock == m_pLastBlock)
                return 0;

            data = m_pCurrBlock.m_pcData;
            int readlen = m_pCurrBlock.m_iLength;
            msgno = m_pCurrBlock.m_iMsgNo;

            m_pCurrBlock = m_pCurrBlock.m_pNext;

            return readlen;
        }

        // Functionality:
        //    Find data position to pack a DATA packet for a retransmission.
        // Parameters:
        //    0) [out] data: the pointer to the data position.
        //    1) [in] offset: offset from the last ACK point.
        //    2) [out] msgno: message number of the packet.
        //    3) [out] msglen: length of the message
        // Returned value:
        //    Actual length of data read.

        public int readData(out byte[] data, int offset, Int32 msgno, int msglen)
        {
            data = null;

            int readlen = 0;

            lock (m_BufLock)
            {

                Block p = m_pFirstBlock;

                for (int i = 0; i < offset; ++i)
                    p = p.m_pNext;

                if ((p.m_iTTL > 0) && ((CClock.getTime() - p.m_OriginTime) / 1000 > (UInt64)p.m_iTTL))
                {
                    msgno = p.m_iMsgNo & 0x1FFFFFFF;

                    msglen = 1;
                    p = p.m_pNext;
                    while (msgno == (p.m_iMsgNo & 0x1FFFFFFF))
                    {
                        p = p.m_pNext;
                        msglen++;
                    }

                    return -1;
                }

                data = p.m_pcData;
                readlen = p.m_iLength;
                msgno = p.m_iMsgNo;
            }

            return readlen;
        }

        // Functionality:
        //    Update the ACK point and may release/unmap/return the user data according to the flag.
        // Parameters:
        //    0) [in] offset: number of packets acknowledged.
        // Returned value:
        //    None.

        public void ackData(int offset)
        {
            lock (m_BufLock)
            {
                for (int i = 0; i < offset; ++i)
                    m_pFirstBlock = m_pFirstBlock.m_pNext;

                m_iCount -= offset;
            }

            CClock.triggerEvent();
        }

        // Functionality:
        //    Read size of data still in the sending list.
        // Parameters:
        //    None.
        // Returned value:
        //    Current size of the data in the sending list.

        public int getCurrBufSize()
        {
            return m_iCount;
        }

        public void increase()
        {
            int unitsize = m_pBuffer.m_iSize;

            // new physical buffer
            Buffer nbuf = null;
            try
            {
                nbuf = new Buffer();
                nbuf.m_pcData = new byte[unitsize * m_iMSS];
            }
            catch (Exception e)
            {
                //nbuf.Dispose();
                throw new CUDTException(3, 2, 0);
            }
            nbuf.m_iSize = unitsize;
            nbuf.m_pNext = null;

            // insert the buffer at the end of the buffer list
            Buffer p = m_pBuffer;
            while (null != p.m_pNext)
                p = p.m_pNext;
            p.m_pNext = nbuf;

            // new packet blocks
            Block nblk = null;
            try
            {
                nblk = new Block();
            }
            catch (Exception e)
            {
                //delete nblk;
                throw new CUDTException(3, 2, 0);
            }
            Block pb = nblk;
            for (int i = 1; i < unitsize; ++i)
            {
                pb.m_pNext = new Block();
                pb = pb.m_pNext;
            }

            // insert the new blocks onto the existing one
            pb.m_pNext = m_pLastBlock.m_pNext;
            m_pLastBlock.m_pNext = nblk;

            pb = nblk;
            byte[] pc = nbuf.m_pcData;
            for (int i = 0; i < unitsize; ++i)
            {
                pb.m_pcData = pc;
                pb = pb.m_pNext;
                //pc += m_iMSS;
            }

            m_iSize += unitsize;
        }
    }
}