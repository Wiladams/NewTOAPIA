using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

using TOAPI.GDI32;
using TOAPI.Types;

using NewTOAPIA;            // BufferChunk
using NewTOAPIA.Net.Rtp;    // Classes used - MultiSession
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;

using Sketcher.Core;

namespace Sketcher
{
    public class SketchWindow : Form
    {
        // Conferencing stuff
        MultiSession fSession;
        string fUniqueSessionName;
        GraphicsChannel fGraphicsChannel;
        PayloadChannel fSketchChannel;

        IGraphPort fClientGraphPort;
        GraphPortDelegate fRetainedGraphPort;


        GDIDIBSection backingBuffer;
        GDIRenderer fBackingGraphPort;
        GDIPen fBlackPen;

        List<Point> fPointList;

        bool fLeftButtonDown;
        bool fRightButtonDown;
        int xMouse;
        int yMouse;

        public SketchWindow(int width, int height)
            :base()
        {
            InitializeComponent();

            fBlackPen = new GDIPen(RGBColor.Black);

            this.Size = new System.Drawing.Size(width, height);
            this.Text = "Sketcher";
            this.AllowDrop = true;

            fPointList = new List<Point>();

            // Create the backing buffer to retain the image
            // Make it bigger than any likely displays
            backingBuffer = GDIDIBSection.Create(1600, 1200);
            GDIContext bufferContext = GDIContext.CreateForBitmap(backingBuffer);

            bufferContext.ClearToWhite();
            fBackingGraphPort = new GDIRenderer(bufferContext);

            fRetainedGraphPort = new GraphPortDelegate();
            fRetainedGraphPort.AddGraphPort(fBackingGraphPort);
            fRetainedGraphPort.AddGraphPort(GraphPort);

            CreateSession();

        }


        void CreateSession()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("234.5.7.26"), 9060);
            fSession = new MultiSession(UniqueSessionName, ipep);

            AddChannels();
        }
        
        protected virtual void AddChannels()
        {
            // Add the channel for graphics commands
            fSketchChannel = fSession.CreateChannel(PayloadType.Whiteboard);
            fGraphicsChannel = new GraphicsChannel(fRetainedGraphPort, fSketchChannel);
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

        public string UniqueSessionName
        {
            get
            {
                if (fUniqueSessionName == null)
                    fUniqueSessionName = Guid.NewGuid().ToString();

                return fUniqueSessionName;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (backingBuffer != null)
            {
                GraphPort.PixBlt(backingBuffer.ToPixelArray(), 0, 0);
            }
        }

        public void ClearScreen()
        {
            // Clear the backing buffer
            //backingBuffer.ClearToWhite();
            Invalidate();
        }

        // Reacting to the mouse
        protected override void OnMouseDown(MouseEventArgs e)
        {
            fPointList.Clear(); // Clear out the point list to start over

            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!fRightButtonDown)
                        this.Capture = true;

                    fLeftButtonDown = true;
                    fPointList.Add(new Point(e.X, e.Y));

                    
                    xMouse = e.X;
                    yMouse = e.Y;
                    break;

                case MouseButtons.Right:
                    // On a double click, clear the screen
                    if (e.Clicks == 2)
                        ClearScreen();
                    break;
            }

            base.OnMouseDown(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!fLeftButtonDown && !fRightButtonDown)
                return;

            int lastX = xMouse;
            int lastY = yMouse;

            // Move to the last mouse location that we had
            //GraphPort.MoveTo(xMouse, yMouse);

            // Make sure we stay within the bounds of our graph port
            if (e.X < 0)
                xMouse = 0;
            else
                xMouse = e.X;

            if (e.Y < 0)
                yMouse = 0;
            else
                yMouse = e.Y;
            
            // Add the current point to the list of points
            fPointList.Add(new Point(xMouse, yMouse));

            GraphPort.DrawLine(fBlackPen, new Point(lastX, lastY), new Point(xMouse, yMouse));
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {            
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (fLeftButtonDown)
                        this.Capture = false;

                    fLeftButtonDown = false;

                    //fGraphicsChannel.SelectStockObject(GDI32.BLACK_PEN);
                    fGraphicsChannel.DrawLines(fBlackPen, fPointList.ToArray());

                    break;

                case MouseButtons.Right:
                    break;
            }

            base.OnMouseUp(e);
        }

        #region Drag Drop Support
        void SketchWindow_DragDrop(object sender, DragEventArgs e)
        {
            IDataObject theDroppedObject = e.Data;
            string[] formats = theDroppedObject.GetFormats();

            throw new NotImplementedException();
        }

        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SketchWindow
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Name = "SketchWindow";
            this.ResumeLayout(false);

        }
    }
}
