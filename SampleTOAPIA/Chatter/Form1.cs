using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Net;

using NewTOAPIA;
using NewTOAPIA.Net.Rtp;

namespace Chatter
{
    public partial class Form1 : Form
    {
        MultiSession fSession;
        PayloadChannel fChatChannel;
        string newText;
        string newName;

        public Form1()
        {
            InitializeComponent();

            
            //fSession = new MultiSession(Guid.NewGuid().ToString(), new IPEndPoint(IPAddress.Parse("234.5.7.25"), 5004));
            IPAddress address = IPAddress.Parse("239.5.7.25");
            IPEndPoint ep = new IPEndPoint(address, 5004);
            fSession = new ChatterSession(ep, Guid.NewGuid().ToString(), Environment.UserName, true, true, null);
            
            fChatChannel = fSession.CreateChannel(PayloadType.G721);

            fChatChannel.FrameReceivedEvent += FrameReceivedEvent;
        }

        void ReceiveText(string name, string text)
        {
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText("["+name+"]\n");
            richTextBox1.AppendText(text + "\n\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Send the text to the network
            // Get the text from rick text box 2
            string currentText = textBox1.Text;

            SendText(currentText);

            // Clear Rich Text Box 2
            textBox1.Clear();

        }


        void SendText(string text)
        {
            //ReceiveText(text);

            if (null == fChatChannel || (text.Length < 1))
                return;

            BufferChunk chunk = new BufferChunk(text.Length + 10);
            chunk += text.Length;
            chunk += text;

            fChatChannel.Send(chunk);
        }

        #region Collaboration
        protected override void OnPaint(PaintEventArgs e)
        {
            if (newText != null)
                ReceiveText(newName, newText);
            newText = null;

            base.OnPaint(e);
        }

        void FrameReceivedEvent(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            // Read the string out of the buffer chunk
            int dataLength = ea.Frame.NextInt32();
            string message = (string)ea.Frame;
            newName = ea.RtpStream.Properties.Name;
            newText = message;

            //ReceiveText(message);
            this.Invalidate();
        }
        #endregion

        #region Unused
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
