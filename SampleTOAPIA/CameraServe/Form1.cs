using System;
using System.IO;
using System.Net;


using NewTOAPIA;
using NewTOAPIA.DirectShow;
using NewTOAPIA.UI;
using NewTOAPIA.Net.Rtp;


namespace CameraServer
{
    public partial class Form1 : Window
    {
        VideoSender fCameraSender;

        MultiSession fSession;              // Session used to communicate
        PayloadChannel fChannel;


        public Form1()
            :base("Camera Server", 10, 10, 320, 240)
        {
            string groupIP = "234.5.7.15";
            int groupPort = 5004;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);
            fChannel = fSession.CreateChannel(PayloadType.xApplication3);

            fCameraSender = new VideoSender(0, fChannel);
            fCameraSender.Start();
        }

        public override void OnDestroyed()
        {
            fCameraSender.Stop();
            fSession.Dispose();

            base.OnDestroyed();
        }

        public override void OnKeyUp(KeyboardActivityArgs ke)
        {
            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.Escape:
                    this.Destroy();
                    break;
            }
        }
    }
}
