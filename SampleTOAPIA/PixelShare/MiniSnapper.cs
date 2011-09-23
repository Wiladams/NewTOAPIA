

namespace PixelShare.Core
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Threading;
    using System.Net;

    using TOAPI.User32;
    using TOAPI.Types;

    using NewTOAPIA;
    using NewTOAPIA.Kernel;
    using NewTOAPIA.Net.Rtp;
    using NewTOAPIA.UI;
    using NewTOAPIA.Drawing;
    
    public class SnapperWindow : Window
    {
        MultiSession fSession;              // Session used to communicate
        GraphPortChunkEncoder fCommandDispatcher;      // Used to do PixBlt to the net
        
        PayloadChannel fUserIOChannel;
        //UserIOChannelEncoder fUserIOEncoder;
        //UserIOChannelDecoder fUserIODecoder;

        ScreenSnapper fSnapper;             // The snapshot object
        GDIDIBSection fScreenImage;         // The last snapped image
        PixelArray<Lumb> fGrayImage;

        POINT fClientOrigin;

        bool fIsMinimized;
        bool fNeedsResize;              // Indicates when PixelBuffer needs to be resized
        bool fUseGray = false;
        //bool fAllowRemoteControl = true;

        // These are related to the capture thread
        PrecisionTimer fGlobalTimer;
        bool fSnapperRunning;
        double fFrameRate;
        Thread snapperThread;

        #region Constructor
        public SnapperWindow()
            : this(10, 10, 640, 480)
        {
        }

        public SnapperWindow(int x, int y, int width, int height)
            : base("Snap N Share", 10, 10, 640, 480)
        {
            // Show a form so we can capture the desired group IP and port number
            ServerForm groupForm = new ServerForm();
            //IPAddress randomAddress = NewTOAPIA.Net.Utility.GetRandomMulticastAddress();
            //groupForm.groupAddressField.Text = randomAddress.ToString();
            groupForm.ShowDialog();

            // Get the address and port from the form
            fUseGray = groupForm.checkBox1.Checked;
            string groupIP = groupForm.groupAddressField.Text;
            int groupPort = int.Parse(groupForm.groupPortField.Text);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(groupIP), groupPort);

            // Set our title to the address specified so the user
            // can easily identify their session.
            Title = "SnapNShare - " + ipep.ToString();
            
            fSnapper = new ScreenSnapper();

            fClientOrigin = new POINT();
            int pwidth = ClientRectangle.Width;
            int pheight = ClientRectangle.Height;
            fScreenImage = new GDIDIBSection(width, height, BitCount.Bits24);
            fGrayImage = new PixelArray<Lumb>(width, height,fScreenImage.Orientation, new Lumb());

            BackgroundColor = RGBColor.White;
            this.Opacity = 0.5;

            // Create the MultiSession object so we can send stuff out to a group
            //fSession = new MultiSession(Guid.NewGuid().ToString(), ipep);
            fSession = new MultiSession(ipep, Guid.NewGuid().ToString(), "William", true, true, null);

            // Add the channel for graphics commands
            PayloadChannel payloadChannel = fSession.CreateChannel(PayloadType.dynamicPresentation);
            fCommandDispatcher = new GraphPortChunkEncoder(payloadChannel);

            fUserIOChannel = fSession.CreateChannel(PayloadType.xApplication2);
            //fUserIOEncoder = new UserIOChannelEncoder(fUserIOChannel);
            //fUserIODecoder = new UserIOChannelDecoder(fUserIOChannel);
            //fUserIODecoder.MouseActivityEvent += new MouseActivityEventHandler(fUserIODecoder_MouseActivityEvent);
            //fUserIODecoder.KeyboardActivityEvent += new KeyboardActivityEventHandler(fUserIODecoder_KeyboardActivityEvent);

            // Start the thread that will take snapshots of the screen
            fGlobalTimer = new PrecisionTimer();
            fFrameRate = 2; // Frames per second
            fSnapperRunning = true;
            snapperThread = new Thread(RunSnaps);
            snapperThread.Start();
        }

        //IntPtr fUserIODecoder_KeyboardActivityEvent(object sender, KeyboardActivityArgs ke)
        //{
        //    if (!fAllowRemoteControl)
        //        return IntPtr.Zero;

        //    Console.WriteLine("Received Key Event: {0}  {1}  {2}", InputSimulator.KeyEvents, ke.AcitivityType, ke.VirtualKeyCode);

        //    InputSimulator.SimulateKeyboardActivity(ke.VirtualKeyCode, ke.AcitivityType);

        //    return IntPtr.Zero;
        //}

        //void fUserIODecoder_MouseActivityEvent(object sender, MouseActivityArgs me)
        //{
        //    if (fAllowRemoteControl)
        //    {
        //        // What we've received are window relative coordinates.
        //        // First we need to convert them to screen relative coordinates
        //        POINT aPoint = new POINT(me.X, me.Y);
        //        User32.ClientToScreen(Handle, ref aPoint);

        //        // Now for input simulation, we need to turn the screen 
        //        // point into a normalized range of 0 to 65535
        //        // Normalize the point
        //        Screen myScreen = Screen.FromHandle(Handle);
        //        Rectangle screenRect = myScreen.Bounds;

        //        float xFrac = (float)aPoint.X / screenRect.Width;
        //        float yFrac = (float)aPoint.Y / screenRect.Height;

        //        int normalizedX = (int)(xFrac * 65535);
        //        int normalizedY = (int)(yFrac * 65535);
                
        //        // And finally, send the input
        //        InputSimulator.SimulateMouseActivity(normalizedX, normalizedY, me.Delta, me.ButtonActivity);
        //    }
        //}
        #endregion


        void SnapClientArea()
        {
            Rectangle cRect = ClientRectangle;

            // If we have resized since last picture, then 
            // resize the capture buffer before taking
            // the next snapshot.
            if (fNeedsResize)
            {
                fScreenImage = new GDIDIBSection(cRect.Width, cRect.Height, BitCount.Bits24);
                fGrayImage = new PixelArray<Lumb>(cRect.Width, cRect.Height, fScreenImage.Orientation, new Lumb());

                fNeedsResize = false;
            }

            // To take a snapshot, we need to convert the client area's upper left corner
            // to screen space, as the device context we're using is for the whole screen.
            // So, we get the origin, and make the User32 call to convert that to screen space.
            fClientOrigin = new POINT(0, 0);
            User32.ClientToScreen(Handle, ref fClientOrigin);

            // Now we actually take the snapshot.
            // We pass in the client area, based in screen coordinates
            // and the PixelBuffer object to capture into.
            fSnapper.SnapAPicture(new Rectangle(fClientOrigin.X, fClientOrigin.Y, fScreenImage.Width, fScreenImage.Height), fScreenImage);

        }

        public override void OnMinimized()
        {
            fIsMinimized = true;
        }

        public override void OnMaximized(int width, int height)
        {
            fIsMinimized = false;

            if ((width != fScreenImage.Width) || (height != fScreenImage.Height))
            {
                fNeedsResize = true;
            }
        }

        /// <summary>
        /// After resizing has occured, this method is called.  At this point, you know the new
        /// width and height of the client area.  We set the field 'fNeedsResize' to true so 
        /// that the next time the snapper thread asks to take a snapshot, the PixelBuffer
        /// object will be resized.
        /// 
        /// the newWidth, and newHeight capture the new sizes.  This could be eliminated
        /// as the CaptureClientArea could simply take the current ClientRectangle size
        /// at the time of the call.
        /// </summary>
        public override void  OnResizedTo(int width, int height)
        {
            fIsMinimized = false;
            Rectangle clientRect = ClientRectangle;
            
            if ((clientRect.Width != fScreenImage.Width) || (clientRect.Height != fScreenImage.Height))
            {
                fNeedsResize = true;
            }
        }

        public override IntPtr OnKeyUp(KeyboardActivityArgs ke)
        {

            switch (ke.VirtualKeyCode)
            {
                    // Switch color on and off
                case VirtualKeyCodes.G:
                    fUseGray = !fUseGray;
                    break;

                //case VirtualKeyCodes.I:
                //    fAllowRemoteControl = !fAllowRemoteControl;
                //    break;
            }

            return base.OnKeyUp(ke);
        }

        /// <summary>
        /// This is our opportunity to shut things down cleanly as the window
        /// has been requested to close.
        /// 
        /// We shut down the thread that's taking the snapshots, and wait 100 milliseconds
        /// before returning 'true' which indicates that we are ok with the shutdown continuing.
        /// </summary>
        /// <returns>Return true to indicate shutdown can continue.</returns>
        public override bool OnCloseRequested()
        {
            fSnapperRunning = false;
            Thread.Sleep(100);

            return true;
        }

        void SendSnapshot()
        {
            if (fIsMinimized)
                return;

            double captureStart = fGlobalTimer.GetElapsedSeconds();
            SnapClientArea();
            double captureDuration = fGlobalTimer.GetElapsedSeconds() - captureStart;


            if (fUseGray)
            {
                double grayStart = fGlobalTimer.GetElapsedSeconds();
                PixmapTransfer.Copy(fGrayImage, fScreenImage);

                double grayDuration = fGlobalTimer.GetElapsedSeconds() - grayStart;

                fCommandDispatcher.PixBltLumb(fGrayImage, fClientOrigin.X, fClientOrigin.Y);
            }
            else
            {
                fCommandDispatcher.PixBltPixelBuffer24(fScreenImage, fClientOrigin.X, fClientOrigin.Y);
            }

            // Send current cursor location on separate channel
            //CURSORINFO cInfo = new CURSORINFO();
            //cInfo.Init();
            //if (User32.GetCursorInfo(ref cInfo))
            //{
            //    POINT cPoint = cInfo.ptScreenPos;
            //    User32.ScreenToClient(Handle, ref cPoint);
            //    fUserIOEncoder.MoveCursor(cPoint.X, cPoint.Y);
            //}
        }

        /// <summary>
        /// This thread routine takes snapshots of the screen periodically.  The frame
        /// rate is governed by the FrameRate property of the window.
        /// The frame rate is governed by taking time stamps after each frame is captured
        /// and sent to the network.  If there is more than 10 ms before the next frame
        /// is to be captured, the thread will sleep for that amount of time before
        /// performing the next capture.
        /// </summary>
        void RunSnaps()
        {
            int frameCounter = 0;
            double frameIncrement = 1.0 / fFrameRate;
            PrecisionTimer timer = new PrecisionTimer();    // Use to monitor frame rate

            timer.Reset();
            double lastTime = timer.GetElapsedSeconds();
            double nextTime = lastTime;

            double captureStart;
            double captureDuration;

            while (fSnapperRunning)
            {
                // Figure out when the next frame should be displayed
                nextTime += frameIncrement;

                // Capture and display the current frame
                captureStart = timer.GetElapsedSeconds();
                SendSnapshot();
                captureDuration = timer.GetElapsedSeconds() - captureStart;

                lastTime = timer.GetElapsedSeconds();

                // Increment the frame count just so we know
                // what frame we're on.
                frameCounter++;
                
                // We know when the next frame is to be displayed.
                // Calculate the amount to sleep by looking at the current time, and 
                // the amount of time it takes to make the snapshot.
                double diffTime = nextTime - lastTime - captureDuration;

                // Turn it into milliseconds
                int millis = (int)(diffTime * 1000);

                // We don't bother sleeping if the difference is less 
                // than 10 milliseconds as we can't really guaranteed
                // the accuracy of the Sleep() method, so we get as close
                // as we can.
                if (millis > 10)
                    Thread.Sleep(millis);
            }
        }
    }
}