using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Net;

using TOAPI.User32;
using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Kernel;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Imaging;

namespace DistributedDesktop
{
    public class DesktopSnapper
    {
        GDIContext fContext;

        PayloadChannel fGraphicsChannel;
        GraphPortChunkEncoder fGraphicsEncoder;      // Used to do PixBlt to the net
        
        PayloadChannel fUserIOChannel;
        UserIOChannelEncoder fUserIOEncoder;
        UserIOChannelDecoder fUserIODecoder;

        GDIDIBSection fScreenImage;         // The last snapped image
        PixelArray<Lumb> fGrayImage;

        Rectangle fCaptureArea;

        bool fUseGray = true;
        bool fAllowRemoteControl = false;
        bool fSharing;

        // These are related to the capture thread
        TimedDispatcher fDispatcher;
        double fFrameRate;


        #region Constructor
        public DesktopSnapper(PayloadChannel graphicsChannel, PayloadChannel userIOChannel, bool sharing)
        {
            fGraphicsChannel = graphicsChannel;
            fUserIOChannel = userIOChannel;
            fSharing = sharing;

            fContext = GDIContext.CreateForDefaultDisplay();
            fCaptureArea = Screen.PrimaryScreen.Bounds;

            fScreenImage = new GDIDIBSection(fCaptureArea.Width, fCaptureArea.Height);
            fGrayImage = new PixelArray<Lumb>(fCaptureArea.Width, fCaptureArea.Height, fScreenImage.Orientation, new Lumb());

            
            // Add the channel for graphics commands
            fGraphicsEncoder = new GraphPortChunkEncoder(fGraphicsChannel);

            fUserIOEncoder = new UserIOChannelEncoder(fUserIOChannel);
            fUserIODecoder = new UserIOChannelDecoder(fUserIOChannel);
            fUserIODecoder.MouseActivityEvent += new MouseActivityEventHandler(fUserIODecoder_MouseActivityEvent);
            fUserIODecoder.KeyboardActivityEvent += new KeyboardActivityEventHandler(fUserIODecoder_KeyboardActivityEvent);

            fDispatcher = new TimedDispatcher(1.0 / 10.0, SendSnapshot, null);

            Start();
        }
        #endregion

        #region Properties
        public double FrameRate
        {
            get { return fDispatcher.FrameRate; }
        }

        public bool AllowRemoteControl
        {
            get { return fAllowRemoteControl; }
            set { fAllowRemoteControl = value; }
        }

        public bool SendLuminance
        {
            get { return fUseGray; }
            set { fUseGray = value; }
        }

        public bool ActivelySharing
        {
            get { return fSharing; }
            set { fSharing = value; }
        }

        #endregion

        #region Reacting to Remote IO Events
        IntPtr fUserIODecoder_KeyboardActivityEvent(object sender, KeyboardActivityArgs ke)
        {
            if (!fAllowRemoteControl)
                return new IntPtr(1);

            KeyboardSimulator.SimulateKeyboardActivity(ke.VirtualKeyCode, ke.AcitivityType);

            return IntPtr.Zero;
        }

        void fUserIODecoder_MouseActivityEvent(object sender, MouseActivityArgs me)
        {
            if (!fAllowRemoteControl)
                return;

                // What we've received are coordinates that are relative to the client's
                // view of our desktop in a window.  That is, they are relative to 0,0 being
                // the upper left of the screen.
                // We need to offset these coordinates by the x/y offset of the upper left
                // corner of our screen.  Most of the time, this will also be 0,0, but 
                // in the case where we have multiple monitors, it may be different.
                Screen myScreen = Screen.PrimaryScreen;
                //Rectangle screenRect = myScreen.Bounds;
                Rectangle screenRect = fCaptureArea;
                POINT aPoint = new POINT(me.X + screenRect.X, me.Y + screenRect.Y);

                // Now for input simulation, we need to turn the screen 
                // point into a normalized range of 0 to 65535
                float xFrac = (float)aPoint.X / screenRect.Width;
                float yFrac = (float)aPoint.Y / screenRect.Height;

                int normalizedX = (int)(xFrac * 65535);
                int normalizedY = (int)(yFrac * 65535);
                
                // And finally, simulate the mouse activity
                MouseSimulator.SimulateMouseActivity(normalizedX, normalizedY, me.Delta, me.ButtonActivity);
        }
        #endregion

        #region Thread Management
        public virtual void Start()
        {
            fDispatcher.Start();
        }

        /// <summary>
        /// This is our opportunity to shut things down cleanly as the service
        /// has been requested to stop.
        /// 
        /// We shut down the thread that's taking the snapshots, 
        /// </summary>
        /// <returns>Return true to indicate shutdown can continue.</returns>
        public virtual bool Stop()
        {
            fDispatcher.Stop();

            return true;
        }
        #endregion

        public void SnapAPicture(Rectangle rect, GDIDIBSection pixMap)
        {
            pixMap.DeviceContext.BitBlt(fContext, new Point(rect.X, rect.Y), new Rectangle(0, 0, rect.Width, rect.Height),
                (TernaryRasterOps.SRCCOPY | TernaryRasterOps.CAPTUREBLT));

        }

        void SendSnapshot(TimedDispatcher dispatcher, double time, Object[] dispatchParams)
        {
            if (!fSharing)
                return;

            SnapAPicture(fCaptureArea, fScreenImage);


            if (fUseGray)
            {
                PixmapTransfer.Copy(fGrayImage, fScreenImage);

                fGraphicsEncoder.PixBltLumb(fGrayImage, fCaptureArea.X, fCaptureArea.Y);
            }
            else
            {
                PixelAccessor<BGRb> accessor = new PixelAccessor<BGRb>(fScreenImage.Width, fScreenImage.Height, fScreenImage.Orientation, fScreenImage.Pixels, fScreenImage.BytesPerRow);
                fGraphicsEncoder.PixBltBGRb(accessor, fCaptureArea.X, fCaptureArea.Y);
            }

            // Send current cursor location on separate channel
            // Ideally, this would happen as raw input on the mouse
            // device.
            CURSORINFO cInfo = new CURSORINFO();
            cInfo.Init();
            if (User32.GetCursorInfo(ref cInfo) != 0)
            {
                POINT cPoint = cInfo.ptScreenPos;
                fUserIOEncoder.MoveCursor(cPoint.X, cPoint.Y);
            }
        }
    }
}