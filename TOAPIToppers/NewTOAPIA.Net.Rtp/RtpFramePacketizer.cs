using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// The purpose of this class is to take a BufferChunk object and break it down into 
    /// chunks that match the size specified.  This allows the creation of a series of
    /// BufferChunk objects which can then be sent out one at a time on the wire
    /// across the network.
    /// </summary>
    public class RtpFramePacketizer : FramePacketizer<RtpPacket>
    {
        #region Fields

        // These probably never change per instance of the object
        private PayloadType fPayloadType;   // What's the RTP payload type
        private uint fStreamSsrc;           // The identifier for the stream

        private uint fTimeStamp;     // Likely changes every time we packetize
        #endregion

        #region Constructor
        public RtpFramePacketizer(int packetSize, PacketPool<RtpPacket> pool, uint ts)
            : base(packetSize, pool)
        {
            fTimeStamp = ts;
        }
        #endregion

        #region Properties     
        public PayloadType PayloadType
        {
            get { return fPayloadType; }
            set { fPayloadType = value; }
        }

        public uint StreamSsrc
        {
            get { return fStreamSsrc; }
            set { fStreamSsrc = value; }
        }

        public uint TimeStamp
        {
            get { return fTimeStamp; }
            set { fTimeStamp = value; }
        }
        #endregion

        public void Packetize(BufferChunk chunk, uint timeStamp, PayloadType pType, uint streamSsrc, PacketPool<RtpPacket> pool)
        {
            PayloadType = pType;
            StreamSsrc = streamSsrc;
            TimeStamp = timeStamp;

            base.Packetize(chunk, pool);
        }

        #region Protected Overrides
        /// <summary>
        /// Allow subclassers to modify packet.
        /// </summary>
        /// <param name="aPacket"></param>
        /// <returns></returns>
        protected override RtpPacket OnPreparePacket(RtpPacket packet, int frameIndex)
        {
            packet.TimeStamp = TimeStamp;

            // We should probably set these each time as the
            // packets may have been used by someone else since
            // they were created.
            packet.PayloadType = PayloadType;
            packet.FrameIndex = (ushort)(frameIndex);
            packet.SSRC = fStreamSsrc;

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
        protected override void OnFramesFinished()
        {
            if (PacketCount != LastPacketsInFrame)
            {
                // Assign the number of packets in a frame
                for (int i = 0; i < PacketCount; i++)
                {
                    PacketPool[i].PacketsInFrame = (ushort)PacketCount;
                }

                LastPacketsInFrame = PacketCount;
            }
        }
        #endregion
    }
}
