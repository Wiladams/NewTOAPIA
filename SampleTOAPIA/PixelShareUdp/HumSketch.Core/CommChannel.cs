
namespace PixelShare.Core
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using NewTOAPIA;
    using NewTOAPIA.Net;

    public delegate void ReceivedChunk(BufferChunk aChunk);

    public class CommChannel
    {
        const int NetworkTimeout = -1;
        const int NumberOfPackets = 500;

        // Events
        public event ReceivedChunk ReceivedChunkEvent;
        public event ReceivedChunk ReceivedFrameEvent;


        UdpSender fSender;
        UdpReceiver fReceiver;
        Queue<BufferChunk> fChunks;

        PacketPool<CommPacket> fSenderPacketPool;
        CommFramePacketizer fPacketizer;

        // Frame Assembly
        FrameAssembler<CommPacket> fFrameAssembler;


        public CommChannel(IPEndPoint ep, bool willSend, bool willReceive)
        {
            SetupPacketizer();
            SetupBufferChunks();

            if (willSend)
                fSender = new UdpSender(ep, 2);

            if (willReceive)
            {
                fReceiver = new UdpReceiver(ep, NetworkTimeout);
                fReceiver.AsyncReceiveFrom(fChunks, this.ReceiveChunk);
            }
        }

        void SetupPacketizer()
        {
            fSenderPacketPool = new PacketPool<CommPacket>(10);
            fPacketizer = new CommFramePacketizer(fSenderPacketPool);
        }

        void SetupBufferChunks()
        {
            fChunks = new Queue<BufferChunk>();
            for (int i = 0; i < NumberOfPackets; i++)
            {
                BufferChunk newChunk = new BufferChunk(UDP.MTU);
                fChunks.Enqueue(newChunk);
            }
        }

        public virtual void Send(BufferChunk aChunk)
        {
            if (fSender == null)
                return;

            // Packetize the chunk
            fPacketizer.Packetize(aChunk, fSenderPacketPool);

            // Send out the smaller packets until done
            for (int i = 0; i < fSenderPacketPool.PacketsInFramePool; i++)
            {
                PacketBase packetWrapper = fSenderPacketPool.Packets[i];
                fSender.Send(packetWrapper.Buffer);
            }
        }

        public void ReceiveBufferReturn(BufferChunk aChunk)
        {
            aChunk.Reset();
            fChunks.Enqueue(aChunk);
        }

        public void ReceiveChunk(BufferChunk aChunk, EndPoint endPoint)
        {
            CommPacket aPacket = new CommPacket(aChunk);
            if (aPacket.PacketSequenceNumber == 0)
            {
                if (fFrameAssembler != null)
                {
                    fFrameAssembler.Dispose();
                    fFrameAssembler = null;
                }

                fFrameAssembler = new FrameAssembler<CommPacket>((uint)aPacket.PacketsInFrame);
                fFrameAssembler = new FrameAssembler<CommPacket>((uint)aPacket.PacketsInFrame, 0, ReceiveBufferReturn);
            }

            fFrameAssembler.SetPacket(aPacket.PacketSequenceNumber, aPacket);

            if (fFrameAssembler.IsComplete)
            {
                BufferChunk payloadChunk = fFrameAssembler.GetAssembledFrame();
                fFrameAssembler.Dispose();
                fFrameAssembler = null;

                FrameReceived(payloadChunk);
            }

        }

        protected virtual void FrameReceived(BufferChunk aChunk)
        {
            if (ReceivedFrameEvent != null)
                ReceivedFrameEvent(aChunk);
        }
    }
}
