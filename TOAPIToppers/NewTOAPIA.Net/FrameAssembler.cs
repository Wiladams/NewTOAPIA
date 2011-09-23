using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net
{
    public class FrameAssembler<T>
        where T : PacketBase, new()
    {
        private PacketPool<T> fPool;    // packet pool used for assembly

        private int packetsInFrame;
        private uint timeStamp;     // An initial timestamp is set
        private ReturnBufferHandler returnBufferHandler;
        private bool allowDuplicatePackets;

        // Packet reception accounting
        int length;
        private ushort packetsReceived;


        #region Constructors
        public FrameAssembler(int packetsInFrame)
            : this(packetsInFrame, 0, null, false)
        {
        }

        /// <summary>
        /// Constructs a chunk for Receiving data
        /// Defaults allowDuplicatePackets to false
        /// </summary>
        public FrameAssembler(int packetsInFrame, uint timeStamp, ReturnBufferHandler returnBufferHandler)
            : this(packetsInFrame, timeStamp, returnBufferHandler, false)
        {
        }

        /// <summary>
        /// Constructs a chunk for Receiving data
        /// </summary>
        public FrameAssembler(int packetsInFrame, uint timeStamp, ReturnBufferHandler returnBufferHandler, bool allowDuplicatePackets)
        {
            this.packetsInFrame = packetsInFrame;
            this.timeStamp = timeStamp;
            this.returnBufferHandler = returnBufferHandler;
            this.allowDuplicatePackets = allowDuplicatePackets;

            fPool = new PacketPool<T>(packetsInFrame);
        }

        #endregion

        #region Properties
        public bool IsComplete
        {
            get { return packetsReceived == packetsInFrame; }
        }

        public int PacketCount
        {
            get { return (int)packetsInFrame; }
        }

        public uint TimeStamp
        {
            set { timeStamp = value; }
        }
        #endregion

        public BufferChunk GetAssembledFrame()
        {
            if (!IsComplete)
            {
                throw new FrameIncompleteException();
            }

            BufferChunk chunk = new BufferChunk((int)length);

            for (int i = 0; i < packetsInFrame; i++)
            {
                chunk += fPool[i].Payload;
            }

            return chunk;
        }

        public virtual void SetPacket(int index, T aPacket)
        {
            if (index >= packetsInFrame)
            {
                throw new IndexOutOfRangeException();
            }

            if (fPool[index] == null)
            {
                packetsReceived++;
                length += aPacket.PayloadSize;
                fPool[index] = aPacket;
            }
            else
            {
                if (!allowDuplicatePackets)
                {
                    throw new DuplicatePacketException();
                }
            }
        }

        public virtual T GetPacket(int index)
        {
            if (index >= packetsInFrame)
            {
                throw new IndexOutOfRangeException();
            }

            return fPool[index];
        }

        public void Dispose()
        {
            // Return packets back to where they came from
            for (int i = 0; i < packetsInFrame; i++)
            {
                T packet = fPool[i];

                // A packet could be null if we never received a packet for that location
                if (packet != null)
                {
                    if (null != returnBufferHandler)
                    {
                        returnBufferHandler(packet.ReleaseBuffer());
                    }

                    fPool[i] = null;
                }
            }
        }
    }
}
