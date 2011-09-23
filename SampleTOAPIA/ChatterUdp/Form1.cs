

namespace Chatter
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    using System.Net;

    using NewTOAPIA;

    using NetGraph;

    public partial class Form1 : Form, IObserver<BufferChunk>
    {
        UdpConnection fConnection; 

        string newText;
        string newName;

        /// <summary>
        /// For doing conversions with strings
        /// </summary>
        private System.Text.UTF8Encoding utf8;

        public Form1()
        {
            InitializeComponent();

            utf8 = new System.Text.UTF8Encoding();
            
            IPAddress MCastAddr = IPAddress.Parse("239.5.7.25");
            IPEndPoint endPoint = new IPEndPoint(MCastAddr, 5006);

            fConnection = new UdpConnection(endPoint);
            fConnection.Subscribe(this);
        }

        void ReceiveText(string name, string text)
        {
            richTextBox1.SelectionColor = Color.Red;
            richTextBox1.AppendText("["+name+"]\n");
            richTextBox1.AppendText(text + "\n\n");
            richTextBox1.ScrollToCaret();
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

            // Turn the text into a byte array
            byte[] textbytes = null;
            lock (utf8)
            {
                textbytes = utf8.GetBytes(text);
            }

            if ((textbytes == null) || (textbytes.Length == 0))
                return;

            BufferChunk chunk = new BufferChunk(textbytes.Length + 10);
            chunk += textbytes.Length;
            chunk += textbytes;

            fConnection.Sender.Send(chunk);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (newText != null)
                ReceiveText(newName, newText);
            newText = null;

            base.OnPaint(e);
        }


        #region IObserver<BufferChunk>
        public void OnCompleted()
        {
        }

        public void OnError(Exception excep)
        {
        }

        public void OnNext(BufferChunk chunk)
        {
            // Read the string out of the buffer chunk
            int dataLength = chunk.NextInt32();
            newText = chunk.NextUtf8String(dataLength);
            newName = "Name";

            this.Invalidate();
        }
        #endregion

    }
}
