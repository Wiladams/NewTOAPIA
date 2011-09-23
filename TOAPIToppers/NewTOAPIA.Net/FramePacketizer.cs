using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net
{
    using TOAPI.Types;

    /// <summary>
    /// The purpose of this class is to take a BufferChunk object and break it down into 
    /// chunks that match the size specified.  This allows the creation of a series of
    /// BufferChunk objects which can then be sent out one at a time on the wire
    /// across the network.
    /// </summary>
    public class FramePacketizer<T>
        where T : PacketBase, new()
    {
        #region Fields
        int fLength;            // The length of the frame to be packetized
        int fMaxPacketSize;     // The maximum size of an individual packet
        int packetsInFrame;     // How many packets are in the frame
        int lastPacketsInFrame;

        PacketPool<T> fPool;                // The packet pool used for packets
        
        #endregion

        #region Constructor
        public FramePacketizer(int packetSize, PacketPool<T> pool)
        {
            fPool = pool;
            fMaxPacketSize = packetSize;
        }
        #endregion

        #region Properties
        public int LastPacketsInFrame
        {
            get { return lastPacketsInFrame; }
            set { lastPacketsInFrame = value; }
        }

        public int Length
        {
            get { return fLength; }
        }

        /// <summary>
        /// The number of packets that are actually in use for the frame.
        /// This will be the same, or less, than the number of packets
        /// that are actually available in the pool.
        /// </summary>
        public int PacketCount
        {
            get { return packetsInFrame; }
        }

        public PacketPool<T> PacketPool
        {
            get { return fPool; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Converts chunk into packets
        /// 
        /// Note: This method does not currently take into account header extensions in the packet
        /// If we re-add support for optional headers, we would need to use the MaxPayloadSize
        /// property of each packet, and header extensions would have to be set before this method 
        /// is called. JVE 6/28/2004
        /// </summary>
        public virtual void Packetize(BufferChunk chunk, PacketPool<T> pool)
        {
            //fPayloadType = pType;
            //fStreamSsrc = streamSsrc;
            //fTimeStamp = timeStamp;
            fPool = pool;
            fLength = chunk.Length;

            AllocateApproximatePackets((uint)fLength);

            // We'll find out real number of packets as we build them
            packetsInFrame = 0;

            while (chunk.Length > 0)
            {
                T packet = GetNextPacket();

                // In the event that we re-add support for custom headers, add that data here 
                // before making the call to packet.MaxPayloadSize - 9/23/2004 JVE

                // Copy the MaxPayload amount of data
                packet.Payload = chunk.NextBufferChunkMax(packet.MaxPayloadSize);
            }

            OnFramesFinished();
        }

        public void Packetize(IntPtr[] ptrs, int[] lengths, bool prependLengths)
        {
            // This assumes length member variable has been set already
            AllocateApproximatePackets(1);

            // Initialize to enter loop
            packetsInFrame = 0; // We'll find out real number of packets
            T packet = null;
            int pktAvailable = 0;
            int ptrIndex = 0;
            int ptrAvailable = 0;
            UnmanagedPointer ptr = new UnmanagedPointer(IntPtr.Zero);
            bool writePtrLength = false;
            int ptrsLength = ptrs.Length;

            while (true)
            {
                // If there is no data left in this ptr, get a new one
                if (ptrAvailable == 0)
                {
                    if (ptrIndex + 1 > ptrs.Length)
                    {
                        break; // while(true)
                    }

                    ptrAvailable = lengths[ptrIndex];
                    ptr = new UnmanagedPointer(ptrs[ptrIndex]);
                    ptrIndex++;

                    // Write the length of the ptr to the packet
                    if (prependLengths)
                    {
                        writePtrLength = true;

                        if (pktAvailable < 4)
                        {
                            pktAvailable = 0;  // Get a new packet
                        }
                    }
                }

                // Note:
                // pktAvailable == 0 is handled after ptrAvailable == 0 to bypass the case where
                // the final ptr fit into the packet perfectly.  We want to exit the loop at that
                // point before grabbing another packet which won't have anything put in it.  The
                // case for prepending the length of ptr still works if the previous packet didn't
                // have room for the length.

                // If there is no room in the current packet, get a new one
                if (pktAvailable == 0)
                {
                    packet = GetNextPacket();

                    // In the event that we re-add support for custom headers, add that data here 
                    // before making the call to packet.MaxPayloadSize - 9/23/2004 JVE

                    // Find out how much room is left in the packet
                    packet.Reset(); // Mimic packet.Payload = ...
                    pktAvailable = packet.MaxPayloadSize - packet.PayloadSize;
                }

                // Write length of ptr to packet
                if (writePtrLength)
                {
                    // Add 4 bytes for length
                    packet.AppendPayload(ptrAvailable);
                    pktAvailable -= 4;

                    writePtrLength = false;

                    // Place holder ptr
                    if (ptrAvailable == 0)
                    {
                        continue; // Nothing to copy
                    }
                }

                // Copy as much as the packet can hold
                if (ptrAvailable >= pktAvailable)
                {
                    packet.AppendPayload(ptr, pktAvailable);
                    // buffer.CopyFrom(ptr, pktAvailable);

                    ptr = ptr + pktAvailable; // Advance pointer

                    ptrAvailable -= pktAvailable;
                    pktAvailable = 0;
                }
                else // Copy as much as the ptr can provide
                {
                    packet.AppendPayload(ptr, ptrAvailable);

                    pktAvailable -= ptrAvailable;
                    ptrAvailable = 0;
                }
            }

            OnFramesFinished();
        }
        #endregion


        /// <summary>
        /// Approximates how many packets would be in a chunk of the given length.  This is a perf
        /// improvement if sending a large chunk that would grow the pool multiple times
        /// 
        /// Note: This assumes length member variable has been set already
        /// </summary>
        /// <param name="length">length of chunk</param>
        private void AllocateApproximatePackets(uint frameLength)
        {
            // + 1 because the chances are pretty slim it will land exactly on the packet boundary
            // and it is better safe than sorry.
            uint packets = (uint)(frameLength / fMaxPacketSize) + 1;

            // Grow pool if necessary
            if (packets > fPool.Length)
            {
                fPool.GrowPool(packets);
            }
        }

        /// <summary>
        /// Retrieve the next packet in sequence.  If the packet
        /// pool needs to grow, it will grow automatically, based
        /// on the growth multiplier.
        /// </summary>
        /// <returns></returns>
        protected virtual T GetNextPacket()
        {
            packetsInFrame++;

            // Grow pool if necessary
            if (packetsInFrame > fPool.Length)
            {
                fPool.GrowPool((uint)packetsInFrame);
            }

            T packet = GetPacket((int)packetsInFrame - 1);
            packet = OnPreparePacket(packet, packetsInFrame);

            return packet;
        }


        /// <summary>
        /// Retrieve an individual RtpPacket from the packet pool.  If the
        /// packet has not yet been allocated, allocate the packet, set a few
        /// attributes on it, and return it.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private T GetPacket(int index)
        {
            T packet = fPool[index];

            if (packet == null)
            {
                // WAA - need to change packetbase so that we can adjust the size
                // after calling a default constructor so we can set the size we want
                // and not just the default size.
                //packet = new T((int)fMaxPacketSize);
                packet = new T();

                OnPreparePacket(packet, index);

                fPool[index] = packet;

                fPool.PacketsInFramePool = (uint)index + 1;
            }

            return packet;
        }

        /// <summary>
        /// Allow subclassers to modify packet.
        /// </summary>
        /// <param name="aPacket"></param>
        /// <returns></returns>
        protected virtual T OnPreparePacket(T packet, int frameIndex)
        {
            return packet;
        }

        /// <summary>
        /// Writes the number of packets in the frame into each individual packet.
        /// That way on the receiving end, the total number of packets that are 
        /// coming for a frame can be known from the first packet received.
        /// 
        /// The number is written whenever the number of packets in the frame is 
        /// different than the last time it was called.
        /// </summary>
        protected virtual void OnFramesFinished()
        {
            if (PacketCount != LastPacketsInFrame)
            {
                // Assign the number of packets in a frame
                for (int i = 0; i < packetsInFrame; i++)
                {
                    // WAA - do this in subclass
                    //PacketPool[i].PacketsInFrame = (ushort)packetsInFrame;
                }

                LastPacketsInFrame = PacketCount;
            }
        }
    }
}
