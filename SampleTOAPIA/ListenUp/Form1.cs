using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Text;
using System.Windows.Forms;

using System.Net;

using NewTOAPIA;
using NewTOAPIA.Media;
using NewTOAPIA.Media.WinMM;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;

namespace Chatter
{
    public partial class Form1 : Form
    {
        MultiSession fSession;
        PayloadChannel fAudioChannel;

        // Audio handlers
        WaveSpeaker fSpeakers;

        public Form1()
        {
            InitializeComponent();


            // Setup the audio processing stuff
            fSpeakers = new WaveSpeaker();

            
            IPAddress address = IPAddress.Parse("239.5.7.15");
            IPEndPoint ep = new IPEndPoint(address, 5004);
            fSession = new MultiSession(ep, Guid.NewGuid().ToString(), Environment.UserName, true, true, null);
            
            fAudioChannel = fSession.CreateChannel(PayloadType.dynamicAudio);
            fAudioChannel.FrameReceivedEvent += new RtpStream.FrameReceivedEventHandler(ReceivedAudioEvent);
        }

        void ReceivedAudioEvent(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            int channels = ea.Frame.NextInt32();
            int samplesPerSec = ea.Frame.NextInt32();
            int bitsPerSample = ea.Frame.NextInt32();
            int dataLength = ea.Frame.NextInt32();

            byte[] data = (byte[])ea.Frame;


            fSpeakers.Write(data, dataLength);
        }

    }
}
