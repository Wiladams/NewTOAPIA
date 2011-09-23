using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

using NewTOAPIA.Net.Rtp;    // Classes used - PayloadChannel, RtpSender, MultiSession, RtpStream
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

using Sketcher.Core;

namespace SketchViewer
{
    public partial class Form1 : Form
    {
        // Conferencing stuff
        MultiSession fSession;

        /// <summary>
        /// This is the pixelbuffer that is receiving the updates to the image.
        /// </summary>
        SketchView fViewer;

        IGraphPort fClientGraphPort;

        public Form1()
        {
            InitializeComponent();

            // Create the session
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("234.5.7.26"), 9060);

            fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);

            fViewer = new SketchView(fSession, 1600, 1200);
            fViewer.DataChangedEvent += new SketchView.DataChangedEventHandler(DataChanged);
        }

        /// <summary>
        /// This method is called through an Event dispatch.  It is registered with 
        /// the DataChangedEvent of the viewer class.  It should be raised whenever
        /// a new set of drawing commands hits to viewer from the network.
        /// 
        /// It is a bit inefficient as it does not distinguish drawing commands from 
        /// state setting commands, so it could be doing a bit more work than necessary.
        /// Also, it does not observe the updated region, and just invalidates the 
        /// entire form.  This will result in a lot of flashing in a highly collaborative
        /// environment.
        /// </summary>
        void DataChanged()
        {
            Invalidate();
        }

        public IGraphPort GraphPort
        {
            get
            {
                if (fClientGraphPort == null)
                {
                    GDIContext dc = GDIContext.CreateForWindowClientArea(this.Handle);
                    fClientGraphPort = new GDIRenderer(dc);
                }

                return fClientGraphPort;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (fViewer != null)
            {
                GraphPort.PixBlt(fViewer.Pixels.ToPixelArray(), 0, 0);
            }
        }
    }
}
