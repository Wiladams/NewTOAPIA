
namespace OpenMic
{
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows.Forms;
    using System.Net;

    using NewTOAPIA;
    using NewTOAPIA.Net.Rtp;
    using NewTOAPIA.Media.WinMM;
    using NewTOAPIA.Media;
    
    public partial class Form1 : Form
    {
        // Network support
        MultiSession fSession;
        PayloadChannel fAudioChannel;

        WaveMicrophone fMicrophone;
        WaveSpeaker fSpeakers;

        // Managing the received audio buffer thread
        bool stopProcessing;
        Thread audioProcessor;
        AutoResetEvent processingStopped;


        bool fShareSound;

        public Form1()
        {
            InitializeComponent();

            IPAddress address = IPAddress.Parse("239.5.7.15");
            IPEndPoint ep = new IPEndPoint(address, 5010);
            fSession = new MultiSession(ep, Guid.NewGuid().ToString(), Environment.UserName, true, true, null);
            fAudioChannel = fSession.CreateChannel(PayloadType.dynamicAudio);

            fMicrophone = new WaveMicrophone(1, 11025, 8, 1.0 / 10.0);
            fSpeakers = new WaveSpeaker(1, 11025, 8, 1.0/10.0) ;

            // Startup a thread to process the audio as it comes in
            stopProcessing = false;
            processingStopped = new AutoResetEvent(false);

            audioProcessor = new Thread(ProcessAudio);
            audioProcessor.IsBackground = true;
            audioProcessor.Start();
        }

        void SendAudioEvent(AudioEvent anEvent)
        {
            BufferChunk chunk = new BufferChunk(anEvent.DataLength + 100);

            //public ushort cbSize;
            //public ushort wBitsPerSample;
            //public int nAvgBytesPerSec;
            //public short nBlockAlign;
            //public short nChannels;
            //public int nSamplesPerSec;
            //public short wFormatTag;

            chunk += (int)anEvent.WaveFormat.nChannels;
            chunk += (int)anEvent.WaveFormat.nSamplesPerSec;
            chunk += (int)anEvent.WaveFormat.wBitsPerSample;
            chunk += (int)anEvent.DataLength;
            chunk += anEvent.DataBuffer;

            fAudioChannel.Send(chunk);
        }

        void ProcessAudio()
        {
            while (!stopProcessing)
            {
                // Wait for something to be on the queue
                AudioEvent anEvent = fMicrophone.AudioEvents.Dequeue();

                try
                {
                    if (anEvent != null)
                    {
                        if (fShareSound)
                            SendAudioEvent(anEvent);
                        else
                        {
                            fSpeakers.Write(anEvent);
                        }
                    }
                    else
                    {
                        stopProcessing = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Process Audio Exception: {0}", e.Message);
                }
            }

            processingStopped.Set();

            //Console.WriteLine("Processing Audio Finished");
        }

        void WaitForProcessingFinished()
        {
            processingStopped.WaitOne();
        }

        protected override void OnClosed(EventArgs e)
        {
            stopProcessing = true;
            fMicrophone.Stop();
            fMicrophone.Close();

            Thread.Sleep(100);

            //WaitForProcessingFinished();
            
            //fMicrophone.Dispose();

            base.OnClosed(e);
        }

        // Start
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = true;

            
            fMicrophone.Start();
        }

        // Stop
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;

            fMicrophone.Stop();
        }

        // Sharing checkmark checked
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckState sharingState = ((CheckBox)sender).CheckState;

            fShareSound = (CheckState.Checked == sharingState);
        }
    }
}
