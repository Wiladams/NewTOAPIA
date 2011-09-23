


namespace NewTOAPIA.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;
    
    public delegate void ReturnBufferHandler(BufferChunk buffer);

    /// <summary>
    /// This delegate defines the interface that will receive buffer chunks from 
    /// some networking calls.  Typical usage will be in async callbacks, such
    /// as found in IReceiveBufferChunk.AsyncReceiveFrom().
    /// </summary>
    /// <param name="bufferChunk">The BufferChunk object that was received</param>
    /// <param name="endPont">The endpoint that sent the packet</param>
    public delegate void ReceivedFromCallback (BufferChunk bufferChunk, EndPoint endPont);

    public delegate void OnReceive(EndPoint endPoint, byte[] data, int offset, int length);

    public interface IReceiveBufferChunk : IDisposable
    {
        void Receive(BufferChunk packetBuffer);
        void ReceiveFrom(BufferChunk packetBuffer, out EndPoint sender);
        void AsyncReceiveFrom(Queue<BufferChunk> queue, ReceivedFromCallback callback);
        //IPAddress ExternalInterface { get; }
    }

    public interface ISendBufferChunk : IDisposable
    {
        void Send(BufferChunk packetBuffer);
        //IPAddress ExternalInterface { get; }
        short DelayBetweenPackets { get; set; }
    }
}
