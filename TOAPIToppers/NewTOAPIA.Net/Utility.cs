


namespace NewTOAPIA.Net
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Runtime.InteropServices;
    
    using TOAPI.Winsock;

    /// <summary>
    /// Helper class to contain functions of use when working with mulitcast interfaces.
    /// </summary>
    [ComVisible(false)]
    public class Utility
    {
        #region members
        //private static IPAddress externalInterface = null;
        public static IPAddress multicastIP;
        #endregion


        #region Public Static Methods
        IPAddress GetFirstMulticastInterface()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
                return null;

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface nic in nics)
            {
                if (nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                }
            }
            return null;
        }

        /// <summary>
        /// Find the interface we should be binding to to receive a multicast stream by using Socket.IOControl to call
        /// SIO_ROUTING_INTEFACE_QUERY (see WSAIoctl in Winsock2 documentation) passing in a known multicast address.
        /// </summary>
        /// <returns>IPAddress containing the local multicast interface</returns>
        /// <example>
        /// IPAddress ifAddress = MulticastInterface.GetLocalMulticastInterface();
        /// </example>
        //public static IPAddress GetLocalMulticastInterface()
        //{
        //    if (externalInterface != null)
        //    {
        //        return externalInterface;
        //    }
        //    else if (multicastIP != null)
        //    {
        //        return GetLocalRoutingInterface(multicastIP);
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        
        /// <summary>
        /// Checks if the IP is a multicast IP
        /// </summary>
        /// <remarks>
        /// Works for both IPv4 and IPv6
        /// </remarks>
        public static bool IsMulticast(IPAddress ipAddress)
        {
            bool isMulticast = false;

            if (AddressFamily.InterNetwork == ipAddress.AddressFamily) // IPv4
            {
                // In IPv4 Multicast addresses first byte is between 224 and 239
                byte[] addressBytes = ipAddress.GetAddressBytes();
                isMulticast = (addressBytes[0] >= 224) && (addressBytes[0] <= 239);
            } else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                // In IPv6 Multicast addresses first byte is 0xFF
                byte[] ipv6Bytes = ipAddress.GetAddressBytes();
                isMulticast = (ipv6Bytes[0] == 0xff);
            }

            return isMulticast;
        }

        /// <summary>
        /// Checks if the IP is a multicast IP
        /// </summary>
        /// <remarks>
        /// Works for both IPv4 and IPv6.  Note that this overload is slightly less efficient than the
        /// alternative, as only the IPAddress is needed to determine if an IP is multicast.
        /// </remarks>
        public static bool IsMulticast(IPEndPoint ipEndPoint)
        {
            return IsMulticast(ipEndPoint.Address);
        }

        /// <summary>
        /// Create a random class 'D' multicast IP address.
        /// </summary>
        /// <returns>A IPEnPoint object that has the random multicast address.</returns>
        public static IPAddress GetRandomMulticastAddress()
        {
            Random rnd = new Random();
            int randomaddr = 234;   // Start with 234 as that's a good address to start from
            randomaddr = randomaddr * 256 + rnd.Next(1, 2 ^ 8 - 1);
            randomaddr = randomaddr * 256 + rnd.Next(1, 2 ^ 8 - 1);
            randomaddr = randomaddr * 256 + rnd.Next(1, 2 ^ 8 - 1);

            // Construct the actual address object
            IPAddress address = new IPAddress(IPAddress.HostToNetworkOrder(randomaddr));

            return address;
        }

        private const int SIO_ROUTING_INTERFACE_QUERY = unchecked((int)(0x40000000 | 0x80000000 | 0x08000000 | 20));

        public static IPAddress GetLocalRoutingInterface(IPAddress ipAddress, int port)
        {
            Socket sock = null;
            IntPtr ptrInAddr = IntPtr.Zero;
            IntPtr ptrOutAddr = IntPtr.Zero;
            IPAddress externalInterface;

            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                sockaddr_in inAddr = new sockaddr_in();
                sockaddr_in outAddr = new sockaddr_in();

                try
                {
                    ipAddress.GetAddressBytes().CopyTo(inAddr.sin_addr, 0);

                    // create a sockaddr_in function for our destination IP address
                    inAddr.sin_port = (ushort)IPAddress.HostToNetworkOrder((short)port);

                    // create an block of unmanaged memory for use by Marshal.StructureToPtr.  We seem to need to do this, even though
                    // StructureToPtr will go ahead and release/reallocate the memory
                    ptrInAddr = Marshal.AllocCoTaskMem(Marshal.SizeOf(inAddr));

                    // Copy inAddr from managed to unmanaged
                    Marshal.StructureToPtr(inAddr, ptrInAddr, false);

                    // Create a managed byte array to hold the structure, but in byte array form
                    byte[] byteInAddr = new byte[Marshal.SizeOf(inAddr)];

                    // Copy the structure from unmanaged ptr into managed byte array
                    Marshal.Copy(ptrInAddr, byteInAddr, 0, byteInAddr.Length);

                    // Create a second managed byte array to hold the output sockaddr_in structure
                    byte[] byteOutAddr = new byte[Marshal.SizeOf(inAddr)];

                    // Make the call to IOControl, asking for the Interface we should use if we want to route a packet to inAddr
                    sock.IOControl(
                        SIO_ROUTING_INTERFACE_QUERY,
                        byteInAddr,
                        byteOutAddr
                        );

                    // create the memory placeholder for our local interface

                    // Copy the results from the byteOutAddr into an unmanaged pointer
                    ptrOutAddr = Marshal.AllocCoTaskMem(Marshal.SizeOf(outAddr));
                    Marshal.Copy(byteOutAddr, 0, ptrOutAddr, byteOutAddr.Length);

                    // Copy the data from the unmanaged pointer to the ourAddr structure
                    Marshal.PtrToStructure(ptrOutAddr, outAddr);
                }
                catch (SocketException se)
                {
                    // Perhaps there were no interfaces present, AKA No Network Adapters enabled/installed and connected to media (wired or wireless)
                    EventLog.WriteEntry("RtpListener", se.ToString(), EventLogEntryType.Warning, 99);
                }
                finally
                {
                    // Release the socket
                    sock = null;

                    Marshal.FreeCoTaskMem(ptrInAddr);
                    Marshal.FreeCoTaskMem(ptrOutAddr);
                }

                // Return an IPAddress structure that is initialized with the value of the IP address contained in the outAddr structure
                if (outAddr != null)
                {
                    externalInterface = new IPAddress(outAddr.sin_addr);
                    return (new IPAddress(outAddr.sin_addr));
                }
                else
                {
                    return null;
                }

            }

            if (Socket.OSSupportsIPv6 && ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                sock = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
                sockaddr_in6 inAddr = new sockaddr_in6();
                sockaddr_in6 outAddr = new sockaddr_in6();

                try
                {
                    ipAddress.GetAddressBytes().CopyTo(inAddr.sin_addr, 0);

                    // create a sockaddr_in function for our destination IP address
                    inAddr.sin_port = (ushort)IPAddress.HostToNetworkOrder((short)port);

                    // create an block of unmanaged memory for use by Marshal.StructureToPtr.  We seem to need to do this, even though
                    // StructureToPtr will go ahead and release/reallocate the memory
                    ptrInAddr = Marshal.AllocCoTaskMem(Marshal.SizeOf(inAddr));

                    // Copy inAddr from managed to unmanaged
                    Marshal.StructureToPtr(inAddr, ptrInAddr, false);

                    // Create a managed byte array to hold the structure, but in byte array form
                    byte[] byteInAddr = new byte[Marshal.SizeOf(inAddr)];

                    // Copy the structure from unmanaged ptr into managed byte array
                    Marshal.Copy(ptrInAddr, byteInAddr, 0, byteInAddr.Length);

                    // Create a second managed byte array to hold the output sockaddr_in structure
                    byte[] byteOutAddr = new byte[Marshal.SizeOf(inAddr)];

                    // Make the call to IOControl, asking for the Interface we should use if we want to route a packet to inAddr

                    sock.IOControl(
                        SIO_ROUTING_INTERFACE_QUERY,
                        byteInAddr,
                        byteOutAddr
                        );

                    // create the memory placeholder for our local interface
                    // Copy the results from the byteOutAddr into an unmanaged pointer
                    ptrOutAddr = Marshal.AllocCoTaskMem(Marshal.SizeOf(outAddr));
                    Marshal.Copy(byteOutAddr, 0, ptrOutAddr, byteOutAddr.Length);

                    // Copy the data from the unmanaged pointer to the ourAddr structure
                    Marshal.PtrToStructure(ptrOutAddr, outAddr);
                }
                catch (SocketException se)
                {
                    // Perhaps there were no interfaces present, AKA No Network Adapters enabled/installed and connected to media (wired or wireless)
                    EventLog.WriteEntry("RtpListener", se.ToString(), EventLogEntryType.Warning, 99);
                }
                finally
                {
                    // Release the socket
                    sock = null;

                    Marshal.FreeCoTaskMem(ptrInAddr);
                    Marshal.FreeCoTaskMem(ptrOutAddr);
                }

                // Return an IPAddress structure that is initialized with the value of the IP address contained in the outAddr structure
                if (outAddr != null)
                {
                    externalInterface = new IPAddress(outAddr.sin_addr, outAddr.sin6_scope_id);
                    return (new IPAddress(outAddr.sin_addr, outAddr.sin6_scope_id));
                }
                else
                {
                    return null;
                }

            }
            return null;

        }

                //#region Detect whether there is already traffic in the venue, if so change the IP address
        //bool trafficInVenue = true;
        //int venueInUseTimeout = 2500;
        //BufferChunk bc = new BufferChunk(NewTOAPIA.Net.Rtp.Rtp.MAX_PACKET_SIZE);
                //UdpReceiver udpListener = new UdpReceiver(endPoint, venueInUseTimeout);
                //try
                //{
                //    udpListener.Receive(bc);
                //}
                //catch (System.Net.Sockets.SocketException e)
                //{
                //    if (e.ErrorCode == 10060)
                //    {
                //        trafficInVenue = false;
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                //finally
                //{
                //    udpListener.Dispose();
                //}
                //#endregion

        #endregion
    }
}
