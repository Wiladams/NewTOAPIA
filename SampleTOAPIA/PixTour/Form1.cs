using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;

using TOAPI.GDI32;
using TOAPI.Types;

using Autometaii;

namespace PixTour
{
    public partial class Form1 : Form
    {

        //UdpClient fGDISpeaker;
        //Socket fBroadcastSocket;
        //GDIReceivingChannel fReceiver;
        //GDIDeviceContext fDeviceContext;
        //GCDelegate fGraphPort;
        GDISendingChannel fSendingChannel;
        int fDemoCounter;

        public Form1()
        {

            //// Create the client
            //fGDISpeaker = new UdpClient();
            
            //// Create the broadcast socket
            //IPAddress ip = IPAddress.Parse(GDIPortal.Multicast_Pixtour_Group);
            //fBroadcastSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //fBroadcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ip));
            //fBroadcastSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 255);

            //IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(GDIPortal.Multicast_Pixtour_Group), GDIPortal.Multicast_Pixtour_Port);
            //fBroadcastSocket.Connect(ipep);


            InitializeComponent();

            fSendingChannel = new GDISendingChannel(GDIDeviceContext.CreateForWindow(this.Handle));

            //// Setup listening station as well   
            //// Get the right device context
            //fDeviceContext = GDIDeviceContext.CreateForWindow(this.Handle);
            ////fDeviceContext = GDIDeviceContext.CreateForAllAttachedMonitors();

            //// Setup the speaker
            //fGraphPort = new GCDelegate(fDeviceContext.Resolution);
            //fGraphPort.CommandPacked += new GCDelegate.DrawCommandProc(aPort_CommandPacked);

            
            //// Setup the antennae to listen for commands
            //fReceiver = new GDIReceivingChannel(GDIPortal.Multicast_Pixtour_Group, GDIPortal.Multicast_Pixtour_Port, fDeviceContext);

            fDemoCounter = 0;
            this.Text = "PixTour";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fSendingChannel.GraphPort.UseDefaultBrush();
            fSendingChannel.GraphPort.UseDefaultPen();
            fSendingChannel.GraphPort.Flush();

            switch (fDemoCounter)
            {
                case 0:
                    LineDemo1 aLineDemo = new LineDemo1(new Size(ClientSize.Width, ClientSize.Height));
                    aLineDemo.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;
                case 1:
                    RectangleTest aRectTest = new RectangleTest(new Size(ClientSize.Width, ClientSize.Height));
                    aRectTest.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 2:
                    TextTest aTextTest = new TextTest(new Size(ClientSize.Width, ClientSize.Height));
                    aTextTest.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 3:
                    SineWave siner = new SineWave(new Size(ClientSize.Width, ClientSize.Height), 100);
                    siner.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 4:
                    PolygonTest polygoner = new PolygonTest(new Size(ClientSize.Width, ClientSize.Height));
                    polygoner.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 5:
                    BezierTest bezierer = new BezierTest(new Size(ClientSize.Width, ClientSize.Height));
                    bezierer.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 6:
                    GraphTest grapher = new GraphTest(new Size(ClientSize.Width, ClientSize.Height));
                    grapher.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;

                case 7:
                    RandomRect randrect = new RandomRect(new Size(ClientSize.Width, ClientSize.Height));
                    randrect.ReceiveCommand(new Command_Render(fSendingChannel.GraphPort));
                    break;
            }

            ((Button)sender).Text = fDemoCounter.ToString();

            fDemoCounter++;
            if (fDemoCounter == 8)
                fDemoCounter = 0;
        }

        //PixelBuffer CreatePixelBuffer()
        //{
        //    // Create a bitmap
        //    PixelBuffer buff = new PixelBuffer(300, 300);

        //    // Write a pixel
        //    for (int i = 0; i < 300; i++)
        //        buff.SetPixel(i, i, RGBColor.Yellow);

        //    // Draw a rectangle
        //    for (int x = 10; x < 100; x++)
        //        for (int y = 10; y < 100; y++)
        //            buff.SetPixel(x, y, RGBColor.White);

        //    return buff;
        //}

        //private void BitmapTest()
        //{
        //    PixelBuffer aBuff = CreatePixelBuffer();

        //    fGraphPort.BitBlt(10, 10, aBuff);
        //}

        //private void PixelTest()
        //{
        //    // Write a pixel
        //    for (int i = 0; i < 300; i++)
        //        fGraphPort.SetPixel(i, i, RGBColor.DarkOrange);

        //    // Draw a rectangle
        //    for (int x = 10; x < 100; x++)
        //        for (int y = 10; y < 100; y++)
        //            fGraphPort.SetPixel(x, y, RGBColor.DarkBlue);
        //}





        private void button2_Click(object sender, EventArgs e)
        {
            // Just clear the screen
            this.Invalidate();
        }
    }
}
