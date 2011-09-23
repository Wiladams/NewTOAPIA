using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetGraph
{
    using System.Net;
    using System.Threading;

    using NewTOAPIA;
    using NewTOAPIA.Net;

    /// <summary>
    /// The UdpConnection class serves two purposes
    /// 1) Provide an easy mechanism for sending data out on a Udp Socket
    /// 2) Listen on a Port waiting for UDP Packets.  Turn them into buffer chunks
    ///    and dispatch them as an Observable message.
    /// </summary>
    public class UdpConnection : Observable<BufferChunk>
    {
        Queue<BufferChunk> fChunks;

        IPEndPoint fEndpoint;
        UdpSender fSender;
        UdpReceiver fReceiver;

        #region Constructors
        public UdpConnection(IPAddress address, int port)
            :this(new IPEndPoint(address, port))
        {
        }

        public UdpConnection(IPEndPoint endPoint)
        {
            fEndpoint = endPoint;

            // Setup the receive buffer before we do anything
            SetupBufferChunks();

            // Create the sender
            fSender = new UdpSender(fEndpoint, 12);

            // Create the receiver
            fReceiver = new UdpReceiver(fEndpoint, 0);

            // Start an async receive immediately
            fReceiver.AsyncReceiveFrom(fChunks, this.OnChunkReceived);
        }
        #endregion

        public UdpSender Sender
        {
            get { return fSender; }
        }

        void SetupBufferChunks()
        {
            fChunks = new Queue<BufferChunk>();
            for (int i = 0; i < 10; i++)
            {
                BufferChunk newChunk = new BufferChunk(UDP.BEST_PACKET_SIZE);
                fChunks.Enqueue(newChunk);
            }
        }

        // Callback from AsyncReceiveFrom
        void OnChunkReceived(BufferChunk aChunk, EndPoint endPoint)
        {
            // Tell the observers that a chunk has come in
            DispatchData(aChunk);

            // Reset the chunk and add it back to the pool
            // If we don't add back to the queue, then we'll starve
            // the queue and receiving will fail.
            // It might be that we need to incur a copy here so that
            // we can be assured the application isn't still using
            // the buffer.
            aChunk.Reset();
            fChunks.Enqueue(aChunk);
        }
    }
}
