

namespace NewTOAPIA.Net
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    using NewTOAPIA;
    
    /// <summary>
    /// NewTOAPIA.Net.TSocket is a helper class that inherits from System.Net.Sockets.Socket 
    /// This class adds support for sending and receiving BufferChunks.
    /// </summary>
    [ComVisible(false)]
    public class TSocket : Socket
    {        
        #region Constructors
        /// <summary>
        /// Standard Socket constructor.  Calls the base System.Net.Sockets.Socket constructor.
        /// </summary>
        /// <param name="addressFamily">AddressFamily</param>
        /// <param name="socketType">SocketType</param>
        /// <param name="protocolType">ProtocolType</param>
        public TSocket(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType) 
            : base(addressFamily, socketType, protocolType)
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calls the base class Send.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk to send.</param>
        /// <returns>int bytes sent</returns>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.TSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.Send(bufferChunk);
        /// </example>
        public int Send(BufferChunk bufferChunk)
        {
            return Send(bufferChunk, SocketFlags.None);
        }

        /// <summary>
        /// Calls the base class Send.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk to send</param>
        /// <param name="socketFlags">SocketFlags</param>
        /// <returns>int bytes sent</returns>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.Send(bufferChunk, SocketFlags.None);
        /// </example>
        public int Send(BufferChunk bufferChunk, SocketFlags socketFlags)
        {
            return base.Send(bufferChunk.Buffer, bufferChunk.Index, bufferChunk.Length, socketFlags);
        }

        /// <summary>
        /// Calls the base class SendTo.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk</param>
        /// <param name="endPoint">EndPoint</param>
        /// <returns>int bytes sent</returns>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.SendTo(bufferChunk, endPoint);
        /// </example>
        public int SendTo(BufferChunk bufferChunk, EndPoint endPoint)
        {
            return SendTo(bufferChunk.Buffer, bufferChunk.Index, bufferChunk.Length, SocketFlags.None, endPoint);
        }

        /// <summary>
        /// Calls the base class Receive.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk</param>
        /// <param name="socketFlags">SocketFlags</param>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.TSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.Receive(bufferChunk, SocketFlags.None);
        /// </example>
        public void Receive(BufferChunk bufferChunk, SocketFlags socketFlags)
        {
            bufferChunk.Length = base.Receive(bufferChunk.Buffer, 0, bufferChunk.Buffer.Length, socketFlags);
        }

        /// <summary>
        /// Calls the base class Receive.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk</param>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.TSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.Receive(bufferChunk);
        /// </example>
        public void Receive(BufferChunk bufferChunk)
        {
            Receive(bufferChunk, SocketFlags.None);
        }

        /// <summary>
        /// Calls the base class ReceiveFrom.  Performs an efficient conversion from BufferChunk.
        /// </summary>
        /// <param name="bufferChunk">BufferChunk</param>
        /// <param name="endPoint">ref EndPoint</param>
        /// <example>
        /// Socket fSocket = new NewTOAPIA.Net.TSocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        /// fSocket.ReceiveFrom(bufferChunk, ref endPoint);
        /// </example>
        public void ReceiveFrom(BufferChunk bufferChunk, ref EndPoint endPoint)
        {
            bufferChunk.Length = base.ReceiveFrom(bufferChunk.Buffer, 0, bufferChunk.Buffer.Length, SocketFlags.None, ref endPoint);
        }

        public IAsyncResult BeginReceiveFrom(BufferChunk bufferChunk, ref EndPoint endPoint, AsyncCallback callback, object state)
        {
            return base.BeginReceiveFrom(bufferChunk.Buffer, 0, bufferChunk.Buffer.Length, SocketFlags.None, ref endPoint, callback, state);
        }
        #endregion


    }
}
