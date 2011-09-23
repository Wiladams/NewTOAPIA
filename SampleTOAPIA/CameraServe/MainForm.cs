

namespace CameraServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Net;
    using System.Text;
    using System.Windows.Forms;

    using NewTOAPIA;
    using NewTOAPIA.DirectShow;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Media;
    using NewTOAPIA.Media.Capture;
    using NewTOAPIA.UI;
    using NewTOAPIA.Net.Rtp;
    
    public partial class MainForm : Form
    {
        VideoSender fCameraSender;

        MultiSession fSession;              // Session used to communicate
        PayloadChannel fChannel;
        CaptureDeviceSetupPage fSetupControl;
        //Bitmap fPreviewBitmap;
        //GDIRenderer fRenderer;
        //GDIContext fRendererContext;

        public MainForm()
        {
            InitializeComponent();

            fSetupControl = new CaptureDeviceSetupPage();
            //this.Controls.Add(fSetupControl);
            splitContainer1.Panel1.Controls.Add(fSetupControl);

            string groupIP = "234.5.7.15";
            int groupPort = 5004;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);
            fChannel = fSession.CreateChannel(PayloadType.dynamicPresentation);

            fCameraSender = new VideoSender(0, fChannel);
            //fPreviewBitmap = new Bitmap(fCameraSender.Width, fCameraSender.Height, 
            fCameraSender.ReceivedVideoFrameEvent += new VideoSender.ReceivedVideoFrameHandler(fCameraSender_ReceivedVideoFrameEvent);
            fCameraSender.Start();
        }

        void fCameraSender_ReceivedVideoFrameEvent(PixelAccessorBGRb accessor)
        {
            //ClientAreaGraphPort.PixBlt(accessor, 10, 10);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            fCameraSender.Stop();
            fSession.Dispose();

            base.OnClosing(e);
        }
    }
}
