

namespace NewTOAPIA.Net
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;

    using TOAPI.Winsock;

    public class UdpSocket : TSocket
    {
        #region Constants
        /// <summary>
        /// Maximum size, in bytes, of an Rtp/RtcpPacket - 1452
        /// 
        /// RFC3550, Colin's book, our network staff and the netmon properties of the local netcard
        /// suggest a standard MTU (Maximum Transmission Unit) of 1500 bytes for a typical router.
        /// 
        /// They also suggest 28 bytes for the overhead of a standard IPv4 implementation of the IP
        /// (20) and UDP (8) headers.  1500 - 28 = 1472.
        /// 
        /// There is a 14 byte overhead for the Ethernet layer, but apparently that is safe to
        /// ignore in this calculation.
        /// 
        /// In order to be IPv6 ready, we are adding an additional 20 byte overhead (or 40 for the 
        /// IP layer).  1500 - 48 = 1452.
        /// 
        /// Because Rtcp requires that all packets end on a 32 bit (4 byte) boundary, we adjust 
        /// to the closest such boundary, which is still 1452.
        /// 
        /// TODO - this should be calculated at runtime by occasionally sending a packet with the
        /// DF (do not fragment) flag set.  This will cause a router to return a packet indicating
        /// the maximum packet size it can handle without fragmenting the data.  For now it is a
        /// constant. JVE
        /// </summary>
        public const int MTU = 1500;       // Maximum Transmission Unit, should be calculated
        public const int IPv6_Header_Size = 40; // Size of IPv6 Header in bytes
        public const int UDP_Header_Size = 8;   // Size of Udp Header

        public const int BEST_PACKET_SIZE = MTU - IPv6_Header_Size - UDP_Header_Size; // 1452 bytes
        #endregion

        private static Hashtable socks = Hashtable.Synchronized(new Hashtable());

        EndPoint fEndPoint;

        /// <summary>
        /// The UdpSocket represents a Udp datagram socket.  The Address family can 
        /// either be Internetwork, or InternetworkV6
        /// </summary>
        /// <param name="addressFamily"></param>
        public UdpSocket()
            : base(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
        {
            // Get the External Interface, save it for future use
            //IPAddress externalInterface = Utility.GetLocalRoutingInterface(endPoint.Address);

        }

        public UdpSocket(AddressFamily addressFamily)
            : base(addressFamily, SocketType.Dgram, ProtocolType.Udp)
        {
            // Get the External Interface, save it for future use
            //IPAddress externalInterface = Utility.GetLocalRoutingInterface(endPoint.Address);

        }

        public IPEndPoint EndPoint
        {
            set
            {
                fEndPoint = value;
            }
        }

        #region Shared Sockets Implementation
        //internal class SockInterfacePair
        //{
        //    internal UdpSocket sock;
        //    internal IPAddress extInterface;
        //    public bool Initialized;

        //    internal SockInterfacePair(UdpSocket sock, IPAddress extIntf)
        //    {
        //        this.sock = sock;
        //        this.extInterface = extIntf;
        //        Initialized = false;
        //    }
        //}


        // Apparently binding to the same ports on UDPSender and UDPListener causes problems in unicast.
        // Sharing the socket though, allows us to tunnel through firewalls as data is sent and received
        // on the same  endpoint.
        // This region of code enables sharing sockets between the two classes.

        //internal static SockInterfacePair GetSharedSocket(IPEndPoint endPoint)
        //{
        //    lock (socks)
        //    {
        //        object sockObj = socks[endPoint];
        //        if (sockObj != null)
        //        {
        //            SockInterfacePair sip = (SockInterfacePair)sockObj;
        //            ++sip.sock.refCount;

        //            return sip;
        //        }
        //        else
        //        {
        //            // Create the socket
        //            UdpSocket sock = new UdpSocket(endPoint.AddressFamily);

        //            // Get the External Interface, save it for future use
        //            IPAddress externalInterface = Utility.GetLocalRoutingInterface(endPoint.Address);

        //            if (externalInterface == null)
        //            {
        //                // Pri3: Do something more helpful here
        //                throw new Exception(Strings.UnableToFindLocalRoutingInterface);
        //            }

        //            if (Utility.IsMulticast(endPoint.Address))
        //            {
        //                // Allow multiple binds to this socket, as it will still function properly
        //                //  (this is only the case if it is a multicast socket.  Unicast sockets fail to
        //                //   receive all data on all sockets)
        //                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, -1);

        //                // We don't join the multicast group here, because we may not want to listen
        //                // to our own data (halfing our throughput).  jasonv - 10/28/2004
        //            }

        //            // Add the socket to the hashtable
        //            SockInterfacePair sip = new SockInterfacePair(sock, externalInterface);
        //            socks.Add(endPoint, sip);

        //            // Increase the socket's reference count
        //            ++sock.refCount;

        //            return sip;
        //        }
        //    }
        //}

        //internal static void ReleaseSharedSocket(IPEndPoint endPoint, UdpSocket sock)
        //{
        //    object sockObj = socks[endPoint];
        //    if (sockObj == null)
        //        throw new InvalidOperationException(Strings.SockDoesNotExistAsASharedSocket);

        //    lock (socks)
        //    {
        //        if (--sock.refCount <= 0)
        //        {
        //            // Leave the multicast group
        //            if (Utility.IsMulticast(endPoint.Address))
        //            {
        //                try
        //                {
        //                    if (endPoint.AddressFamily == AddressFamily.InterNetworkV6)
        //                    {
        //                        IPv6MulticastOption mo = new IPv6MulticastOption(endPoint.Address);
        //                        sock.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, mo);
        //                    }
        //                    else
        //                    {
        //                        MulticastOption mo = new MulticastOption(endPoint.Address);
        //                        sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, mo);
        //                    }
        //                }
        //                catch { } // The user of the socket *may* not have joined the multicast group (?)
        //            }

        //            // Remove ourselves from the shared pool
        //            socks.Remove(endPoint);

        //            // Close the socket
        //            try
        //            {
        //                sock.Close();
        //            }
        //            catch (ObjectDisposedException) { }
        //        }
        //    }
        //}

        internal static void ReleaseSocket(IPEndPoint endPoint, UdpSocket sock)
        {
            // Leave the multicast group
            if (Utility.IsMulticast(endPoint.Address))
            {
                try
                {
                    if (endPoint.AddressFamily == AddressFamily.InterNetworkV6)
                    {
                        IPv6MulticastOption mo = new IPv6MulticastOption(endPoint.Address);
                        sock.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, mo);
                    }
                    else
                    {
                        MulticastOption mo = new MulticastOption(endPoint.Address);
                        sock.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, mo);
                    }
                }
                catch { } // The user of the socket *may* not have joined the multicast group (?)
            }

            // Close the socket
            try
            {
                sock.Close();
            }
            catch (ObjectDisposedException) { }
        }
        
        #endregion


    }
}
