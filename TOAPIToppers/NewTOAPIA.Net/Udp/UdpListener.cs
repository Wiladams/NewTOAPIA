using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;


namespace NewTOAPIA.Net
{
    /// <summary>
    /// UdpReceiver is a high level class that is used to simplify the process of listening for multicast UDP packets.
    /// It does the required work to join a multicast group and set the proper socket settings for correct operation.
    /// 
    /// </summary>
    /// <example>
    /// ...
    /// UdpReceiver mcListener = new UdpReceiver(endPoint);
    /// mcListener.Receive(packetBuffer);
    /// mcListener.Displose();
    /// mcListener = null;
    /// ...
    /// </example>
    [ComVisible(false)]
    public class UdpReceiver : IReceiveBufferChunk, IDisposable
    {

        #region Private Properties
        /// <summary>
        /// bool to tell if the Dispose method has been called, per the IDisposable pattern
        /// </summary>
        private bool disposed;
        /// <summary>
        /// Socket upon which the Multicast Listener works.  Note that we use the NewTOAPIA.Net version of Socket in order to get BufferChunk support.
        /// </summary>
        private UdpSocket fSocket;

        /// <summary>
        /// Hold on to the multicastInterface so we can query it at a later time for diagnostic purposes
        /// </summary>
        private IPAddress externalInterface;

        private Random rnd =  new Random();
        
        private IPEndPoint MulticastEP;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor that binds this object instance to an IPEndPoint.  If you need to change 
        /// IPEndPoint dynamically, Dispose and recreate a new object.
        /// </summary>
        /// <param name="endPoint">IPEndPoint where we should be listening for IP Multicast packets.</param>
        /// <param name="TimeoutMilliseconds">Milliseconds before lack of a packet == a Network Timeout</param>
        /// <example>
        /// ...
        /// UdpReceiver mcListener = new UdpReceiver(endPoint1);
        /// mcListener.Receive(packetBuffer);
        /// mcListener.Dispose();
        ///
        /// UdpReceiver mcListener = new UdpReceiver(endPoint2);
        /// mcListener.Receive(packetBuffer);
        /// mcListener.Dispose();
        ///
        /// mcListener = null;
        /// ...
        /// </example>
        public UdpReceiver(IPEndPoint endPoint, int timeoutMilliseconds)
        {
            this.MulticastEP = endPoint;
            this.disposed = false;
            this.fSocket = null;
            externalInterface = Utility.GetLocalRoutingInterface(endPoint.Address, endPoint.Port);

            //IPAddress ipa = IPAddress.Parse("192.0.0.11");
            //int ad = (int)ipa.Address;
            //s.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastInterface, ad);


            // Create the socket
            this.fSocket = new UdpSocket(endPoint.AddressFamily);


            if (Utility.IsMulticast(endPoint.Address))
            {
                // Allow multiple binds to this socket, as it will still function properly
                //  (this is only the case if it is a multicast socket.  Unicast sockets fail to
                //   receive all data on all sockets)
                //fSocket.ExclusiveAddressUse = false;
                fSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
            }

            // Bind to the socket before joining a multicast group
            IPEndPoint iep = new IPEndPoint(IPAddress.Any, endPoint.Port);
            EndPoint localEndpoint = (EndPoint)iep;
            fSocket.Bind(localEndpoint);

            try
            {
                // Join the multicast group
                // This allows the kernel to inform routers that packets meant
                // for this group should come here.
                if (Utility.IsMulticast(endPoint.Address))
                {
                    if (endPoint.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        // Join the IPv6 Multicast group
                        fSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership,
                            new IPv6MulticastOption(endPoint.Address));
                    }
                    else
                    {
                        // Join the IPv4 Multicast group
                        MulticastOption mcOption = new MulticastOption(this.MulticastEP.Address);
                        fSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, mcOption);
                        
                        // The following is hinted at by MSDN, but is wrong
                        // The SocketOptionLevel needs to be 'IP', not 'Udp'
                        //fSocket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.AddMembership, mcOption);
                    }
                }

                // Set the timeout on the socket
                if (timeoutMilliseconds > 0)
                    fSocket.ReceiveTimeout = timeoutMilliseconds;
                //fSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, timeoutMilliseconds);

                // Make room for 80 packets plus some overhead
                //fSocket.ReceiveBufferSize = UDP.MTU * 80;
                //fSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveBuffer, UDP.MTU * 80);
            }
            catch (SocketException sockex)
            {
                Console.WriteLine(sockex.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                this.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Dispose per the IDisposable pattern
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            
            if(!disposed) 
            {
                disposed = true;
                if (fSocket != null)
                {
                    UdpSocket.ReleaseSocket(MulticastEP, fSocket);
                    //fSocket.Close();
                    fSocket = null;
                }
            }
        }
        /// <summary>
        /// Destructor -- needed because we hold on to an expensive resource, a network socket.  Note that this just calls Dispose.
        /// </summary>
        ~UdpReceiver()
        {
            Dispose();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Receive a packet into a BufferChunk.  This method is preferred over receiving 
        /// into a byte[] because you can allocate a large byte[] in one BufferChunk and 
        /// continously receive into the buffer without recreating byte[]s and dealing with 
        /// the memory allocation overhead that causes.
        /// 
        /// The number of bytes received is stored in BufferChunk.Length.
        /// </summary>
        /// <param name="packetBuffer">BufferChunk</param>
        /// <example>
        /// ...
        /// UdpReceiver mcListener = new UdpReceiver(endPoint);
        ///
        /// // Allocate a 2K buffer to hold the incoming packet
        /// BufferChunk packetBuffer = new BufferChunk(2000);
        ///
        /// mcListener.Receive(packetBuffer);
        ///
        /// mcListener.Displose();
        /// mcListener = null;
        /// ...
        /// </example>
        public void Receive(BufferChunk packetBuffer)
        {
            if (disposed) 
                throw new ObjectDisposedException(Strings.MulticastUdpListenerAlreadyDisposed);

            fSocket.Receive(packetBuffer);
        }

        /// <summary>
        /// Same as Receive, but you also get an EndPoint containing the sender of the packet.
        /// </summary>
        /// <param name="packetBuffer">BufferChunk</param>
        /// <param name="endPoint">EndPoint</param>
        /// <example>
        /// ...
        /// UdpReceiver mcListener = new UdpReceiver(endPoint);
        ///
        /// // Allocate a 2K buffer to hold the incoming packet
        /// BufferChunk packetBuffer = new BufferChunk(2000);
        ///
        /// // Allocate a structure to hold the incoming endPoint
        /// EndPoint endPoint;
        ///
        /// mcListener.ReceiveFrom(packetBuffer, endPoint);
        ///
        /// mcListener.Displose();
        /// mcListener = null;
        /// ...
        /// </example>
        public void ReceiveFrom(BufferChunk packetBuffer, out EndPoint endPoint)
        {
            if (disposed) 
                throw new ObjectDisposedException(Strings.MulticastUdpListenerAlreadyDisposed);

            endPoint = new IPEndPoint(IPAddress.Any,0);
            fSocket.ReceiveFrom(packetBuffer, ref endPoint);
        }


        #region Asynchronous ReceiveFrom functionality
        private class asyncReceiveState
        {
            internal asyncReceiveState(TSocket sock, BufferChunk bufferChunk, Queue<BufferChunk> queue, ReceivedFromCallback receivedFromCallback)
            {
                this.sock = sock;
                this.bufferChunk = bufferChunk;
                this.queue = queue;
                this.receivedFromCallback = receivedFromCallback;
            }

            internal TSocket sock = null;
            internal BufferChunk bufferChunk = null;
            internal Queue<BufferChunk> queue = null;
            internal ReceivedFromCallback receivedFromCallback = null;
        }

        public void AsyncReceiveFrom(Queue<BufferChunk> queueOfBufferChunks, ReceivedFromCallback callback)
        {
            if (disposed) // This continues in the background, so shut down if disposed
            {
                return;
            }

            // Set up the state
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderEP = (EndPoint)sender;
            BufferChunk bc = queueOfBufferChunks.Dequeue();
            asyncReceiveState ars = new asyncReceiveState(this.fSocket, bc, queueOfBufferChunks, callback);

            // Start the ReceiveFrom
            fSocket.BeginReceiveFrom(bc, ref senderEP, new AsyncCallback(asyncReceiveCallback), ars);
        }

        private void asyncReceiveCallback(IAsyncResult ar)
        {
            if (disposed) // This continues in the background, so shut down if disposed
            {
                return;
            }
            // Set up the state
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint senderEP = (EndPoint)sender;
            asyncReceiveState ars = (asyncReceiveState)ar.AsyncState;

            // Receive the packet
            try
            {
                ars.bufferChunk.Length = ars.sock.EndReceiveFrom(ar, ref senderEP);

                // We get back a size of 0 when the socket read fails due to timeout
                if (ars.bufferChunk.Length > 0)
                {
                    // Send the data back to the app that called AsyncReceiveFrom
                    ars.receivedFromCallback(ars.bufferChunk, senderEP);
                }
            }
            catch (Exception e)
            {
                //Pri2: This used to throw a network timeout event into the EventQueue when it was a synchronous receive
                System.Diagnostics.Debug.WriteLine(e.ToString());
                System.Diagnostics.Debugger.Break();
            }

            // Start another pending network read
            AsyncReceiveFrom(ars.queue, ars.receivedFromCallback);
        }
        #endregion

        #endregion

        #region Public Properties
        /// <summary>
        /// Get the IP address of the Local Multicast Interface -- used for diagnostic purposes
        /// </summary>
        //public IPAddress ExternalInterface
        //{
        //    get
        //    {
        //        return externalInterface;
        //    }
        //}
        #endregion

    }
}
