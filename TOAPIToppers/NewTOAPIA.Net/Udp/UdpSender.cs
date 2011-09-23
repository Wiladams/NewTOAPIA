using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace NewTOAPIA.Net
{
    /// <summary>
    /// UdpSender is a high level class that is used to simplify the process of sending multicast UDP packets.
    /// It does all the work to set the proper socket settings for correct operation.
    /// </summary>
    [ComVisible(false)]
    public class UdpSender : ISendBufferChunk, IDisposable
    {
        #region Statics

        private const long SecondsSuppressSocketExceptions = 5;

        private static int maxRetries = 4;
        private static int delayBetweenRetries = 500;

        public static int MaxRetries
        {
            get{return maxRetries;}
            set{maxRetries = value;}
        }

        public static int DelayBetweenRetries
        {
            get{return delayBetweenRetries;}
            set{delayBetweenRetries = value;}
        }

        #endregion Statics

        #region Members
        
        /// <summary>
        /// Part of the IDisposable pattern, tells you when the instance has been disposed
        /// </summary>
        private bool disposed;
        
        /// <summary>
        /// Socket we send on.  Note that we use the NewTOAPIA version to get BufferChunk support.
        /// </summary>
        private UdpSocket fSocket;
        
        /// <summary>
        /// Multicast EndPoint we're sending to.
        /// </summary>
        private IPEndPoint endPoint;
        
        /// <summary>
        /// Our loopback address for the unicast case
        /// </summary>
        private IPEndPoint echoEndPoint;
        
        /// <summary>
        /// Millisecond delay to add between packet sends, used to govern network throughput on limited networks such as 802.11b
        /// </summary>
        private short delayBetweenPackets;
        
        /// <summary>
        /// Hold the local externalInterface for diagnostic purposes
        /// </summary>
        //private IPAddress externalInterface;
        private IPEndPoint localEndPoint;

        /// <summary>
        /// How many times have we retried this packet?
        /// </summary>
        private int retries;
        
        private int lastExceptionNativeErrorCode;
        
        private DateTime lastExceptionTime;


        #endregion Members

        #region Constructors
        
        /// <summary>
        /// Constructor that binds this object instance to an IPEndPoint.  
        /// If you need to change settings dynamically, Dispose and recreate a new object.
        /// </summary>
        /// <param name="endPoint">IPEndPoint where to send the multicast packets.
        /// This can be either a unicast address, or multicast.
        /// <param name="timeToLive">ushort Time To Live of the packets -- how many routers will we cross -- set to 2 for local or testing</param>
        public UdpSender(IPEndPoint endPoint, short timeToLive)
        {
            // Initializing instance fields
            disposed = false;
            fSocket = null;
            echoEndPoint = null;
            delayBetweenPackets = 0;
            //externalInterface = null;
            retries = 0;


            this.endPoint = endPoint;

            //UdpSocket.SockInterfacePair sip = UdpSocket.GetSharedSocket(endPoint);
            //this.fSocket = sip.sock;

            //this.externalInterface = Utility.GetLocalRoutingInterface(endPoint.Address, (ushort)endPoint.Port);

            this.fSocket = new UdpSocket();

            if (Utility.IsMulticast(endPoint.Address))
            {
                SocketOptionLevel sOL = SocketOptionLevel.IP;
                if (endPoint.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    sOL = SocketOptionLevel.IPv6;
                }

                // Set the TTL
                fSocket.Ttl = (short)timeToLive;
                //fSocket.SetSocketOption(sOL, SocketOptionName.MulticastTimeToLive, timeToLive);

                // Enable Multicast Loopback
                fSocket.MulticastLoopback = true;
                //fSocket.ExclusiveAddressUse = false;
                //fSocket.SetSocketOption(sOL, SocketOptionName.MulticastLoopback, 1);
                //fSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1); 

                // Disable Send coalescing
                //fSocket.NoDelay = true;
                //fSocket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, 1); 

            }
            else
            {
                // Enable Unicast Loopback
                //echoEndPoint = new IPEndPoint(externalInterface, endPoint.Port);
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
                    //UdpSocket.ReleaseSharedSocket(endPoint, fSocket);
                    fSocket = null;
                }
            }
        }

        /// <summary>
        /// Destructor -- needed because we hold on to an expensive resource, a network socket.  Note that this just calls Dispose.
        /// </summary>
        ~UdpSender()
        {
            Dispose();
        }


        #endregion
        
        #region Public Methods

        /// <summary>
        /// Send a BufferChunk.  This method is preferred over sending a byte[] because you can allocate a large
        /// byte[] in one BufferChunk and continously send the buffer without recreating byte[]s and dealing with the memory allocation
        /// overhead that causes.
        /// </summary>
        /// <param name="packetBuffer">BufferChunk to send</param>
        public void Send(BufferChunk packetBuffer)
        {
            if (disposed) 
                throw new ObjectDisposedException(Strings.UdpSenderAlreadyDisposed);

            try
            {
                int sendResult = 0;

                // Send an echo signal back if we're sending out to a unicast address.  This mimicks the behavior of multicast with MulticastLoopback == true
                if (echoEndPoint != null)
                    sendResult = fSocket.SendTo(packetBuffer, echoEndPoint);

                // Reset the retry counter
                retries = 0;

                // Come back to here in order to try resending to network
                RetrySend:
                try
                {
                    sendResult = fSocket.SendTo(packetBuffer, endPoint);

                    // Find out which endpoint was assigned after we sent
                    localEndPoint = (IPEndPoint)fSocket.LocalEndPoint;

                    // this is a very crude way of throttling bandwidth
                    // Really, it should happen at the application layer.
                    if(0 != delayBetweenPackets)
                    {
                        Thread.Sleep(delayBetweenPackets); // To control bandwidth
                    }
                }
                catch (SocketException se)
                {
                    // Look for a WSACancelBlockingCall (Error code 10004)
                    // We might just be in the middle of changing access points
                    // This is a problem for the Intel 2200BG card
                    if (se.ErrorCode == 10004) 
                    {
                        if(retries++ < MaxRetries)
                        {
                            Thread.Sleep(DelayBetweenRetries);
                            goto RetrySend;
                        }
                    }
 
                    // Wrong error code or we have retried max times
                    throw;
                }
            }
            catch (SocketException se)
            {
                // Suppress the SocketException if the SocketException.NativeErrorCode is the same and the last exception occured within the exception suppression period
                if (lastExceptionNativeErrorCode == se.NativeErrorCode)
                {
                    if (lastExceptionTime.AddSeconds(SecondsSuppressSocketExceptions) < DateTime.Now)
                    {
                        lastExceptionTime = DateTime.Now;
                        throw;
                    }
                }
                else
                {
                    lastExceptionNativeErrorCode = se.NativeErrorCode;
                    lastExceptionTime = DateTime.Now;
                    throw;
                }
            }
        }

        #endregion

        #region Public Properties
        

        public short DelayBetweenPackets
        {
            get
            {
                return delayBetweenPackets;
            }
            set
            {
                if ( delayBetweenPackets < 0 || delayBetweenPackets > 30 )
                {
                    throw new ArgumentException(Strings.DelayBetweenPacketsRange);
                }

                delayBetweenPackets = value;
            }
        }
        
        
        #endregion

    }
}