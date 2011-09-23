using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewTOAPIA.Net
{
    using System.Net;

    using NewTOAPIA;

    public enum UdpPingCommands
    {
        PingRequest,
        PingReply
    }

    public class UdpPingReply
    {
        public UdpPingCommands Command { get; set; }
        public int Value { get; set; }
        IPAddress Address { get; set; }
        TimeSpan RoundtripTime {get; set;}
        bool Success {get; set;}
    }

    public class UdpPingRequester : Observable<UdpPingReply>, IObserver<BufferChunk> 
    {
        UdpConduit Conduit;

        public void Ping(IPEndPoint endPoint, int value)
        {
            Conduit = new UdpConduit(endPoint);
            Conduit.Subscribe(this);

            BufferChunk aChunk = new BufferChunk(1024);
            aChunk += (int)UdpPingCommands.PingRequest;
            aChunk += value;

            Conduit.Sender.Send(aChunk);
        }

        #region IObserver
        public virtual void OnError(Exception excep)
        {
            throw excep;
        }

        public virtual void OnCompleted()
        {
        }

        // Receive the response
        public virtual void OnNext(BufferChunk chunk)
        {
            UdpPingReply reply = new UdpPingReply();

            // Read out the command
            reply.Command = (UdpPingCommands)chunk.NextInt32(); ;
            reply.Value = chunk.NextInt32();

            // Read out the IP Address

            // Notify observers of the event
            DispatchData(reply);
        }
        #endregion
    }

    public class UdpPingReplyer : IObserver<BufferChunk>
    {
        UdpConduit Conduit;

        public UdpPingReplyer(IPEndPoint endPoint)
        {
            Conduit = new UdpConduit(endPoint);
            Conduit.Subscribe(this);
        }

        #region IObserver
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception excep)
        {
            throw excep;
        }

        public virtual void OnNext(BufferChunk chunk)
        {
            UdpPingCommands command = (UdpPingCommands)chunk.NextInt32();
            int value = chunk.NextInt32();

            BufferChunk newchunk = new BufferChunk(1024);
            chunk += (int)UdpPingCommands.PingReply;
            chunk += value;

            Conduit.Sender.Send(chunk);
        }
        #endregion
    }
}
