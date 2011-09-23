

namespace PixelShare.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    
    using NewTOAPIA.Net;

    public class CommFramePacketizer : FramePacketizer<CommPacket>
    {
        public CommFramePacketizer(PacketPool<CommPacket> pool)
            :base(UDP.MTU, pool)
        {
        }

        /// <summary>
        /// Writes the number of packets in the frame into each individual packet.
        /// That way on the receiving end, the total number of packets that are 
        /// coming for a frame can be known from the first packet received.
        /// 
        /// The number is written whenever the number of packets in the frame is 
        /// different than the last time it was called.
        /// </summary>
        protected override void OnFramesFinished()
        {
            // Assign the number of packets in a frame
            for (int i = 0; i < PacketCount; i++)
            {
                PacketPool[i].PacketsInFrame = PacketCount;
                PacketPool[i].PacketSequenceNumber = i;
            }

            LastPacketsInFrame = PacketCount;
        }
    }
}
