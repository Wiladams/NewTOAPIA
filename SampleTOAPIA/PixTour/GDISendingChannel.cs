using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

using TOAPI.GDI32;

namespace PixTour
{
    class GDISendingChannel
    {
        UdpClient fGDISpeaker;
        Socket fBroadcastSocket;
        GDIReceivingChannel fReceiver;
        GDIDeviceContext fDeviceContext;
        GCDelegate fGraphPort;
        BinaryFormatter fFormatter;
        MemoryStream fMemoryStream;

        public GDISendingChannel(GDIDeviceContext dc)
        {
            fFormatter = new BinaryFormatter();
            fMemoryStream = new MemoryStream(2048);

            fDeviceContext = dc;

            // Create the client
            fGDISpeaker = new UdpClient();

            // Create the broadcast socket
            IPAddress ip = IPAddress.Parse(GDIPortal.Multicast_Pixtour_Group);
            fBroadcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            fBroadcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
            fBroadcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 255);

            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(GDIPortal.Multicast_Pixtour_Group), GDIPortal.Multicast_Pixtour_Port);
            fBroadcastSocket.Connect(ipep);


            // Setup the speaker
            fGraphPort = new GCDelegate(fDeviceContext.Resolution);
            fGraphPort.CommandPacked += new GCDelegate.DrawCommandProc(CommandPacked);


            // Setup the antennae to listen for commands
            fReceiver = new GDIReceivingChannel(GDIPortal.Multicast_Pixtour_Group, GDIPortal.Multicast_Pixtour_Port, fDeviceContext);
        }

        public IRenderGDI GraphPort
        {
            get { return fGraphPort; }
        }

        int CommandPacked(IntPtr hdc, IntPtr lpht, EMR command, int hHandles, IntPtr data)
        {
            // Pack up command
            fMemoryStream.Seek(0, SeekOrigin.Begin);

            fFormatter.Serialize(fMemoryStream, command);

            byte[] sendBytes = fMemoryStream.GetBuffer();

            Send(sendBytes);

            return 1;
        }

        void Send(byte[] packet)
        {
            fBroadcastSocket.Send(packet, packet.Length, SocketFlags.None);
        }
    }
}
