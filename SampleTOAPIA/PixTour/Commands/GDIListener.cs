using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public delegate void ReceiveByteArray(byte[] data);

public class GDIListener
{
    UdpClient fUdpListener;
    string fMulticastAddressName;
    int fPort;
    AsyncCallback fReceiveDelegate;

    public event ReceiveByteArray DataReceived;

    public GDIListener(string anAddress, int aPort)
    {
        int timeToLive = 255;

        fMulticastAddressName = anAddress;
        fPort = aPort;
    
        fUdpListener = new UdpClient(fPort);
        IPAddress group = IPAddress.Parse(fMulticastAddressName);

        fUdpListener.JoinMulticastGroup(group, timeToLive);

        StartListeningTo(fUdpListener);
    }

    public GDIListener(UdpClient udpListener)
    {
        StartListeningTo(udpListener);
    }

    void StartListeningTo(UdpClient udpListener)
    {
        fPort = 0;
        fUdpListener = udpListener;
        fReceiveDelegate = new AsyncCallback(ReceiveHandler);

        fUdpListener.BeginReceive(fReceiveDelegate, null);
    }

    void ReceiveHandler(IAsyncResult asyncResult)
    {
        byte[] bytesReceived;
        IPEndPoint ep = new IPEndPoint(IPAddress.Any, fPort);

        bytesReceived = fUdpListener.EndReceive(asyncResult, ref ep);
        if (bytesReceived.Length != 0)
        {
            // Start the next receive
            fUdpListener.BeginReceive(fReceiveDelegate, null);

            // Process the data from the currently received packet
            if (DataReceived != null)
                DataReceived(bytesReceived);
        }
    }
}

