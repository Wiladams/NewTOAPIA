using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

//using TOAPI.User32;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Kernel;

using PixelShare.Core;

namespace ShowIt
{
    public class DesktopCapture
    {
        #region Fields
        ConferenceAttendee fAttendee;
        bool fIsMinimized;
        bool fSnapperRunning;
        GDIDIBSection fScreenImage;
        PixelArray<Lumb> fGrayImage;
        PrecisionTimer fGlobalTimer;
        int fFrameRate;
        Thread fSnapperThread;
        Resolution fResolution;
        GDIContext fContext;
        bool fNeedsResize;
        GraphPortChunkEncoder fCommandDispatcher;      // Used to do PixBlt to the net

        #endregion

        public DesktopCapture(ConferenceAttendee attendee)
        {
            fAttendee = attendee;

            fContext = GDIContext.CreateForDefaultDisplay();
            //fCommandDispatcher = new GraphPortChunkEncoder(attendee);

            int width = 800;
            int height = 600;
            fResolution = new Resolution(width, height);
            fScreenImage = new GDIDIBSection(width, height);
            fGrayImage = new PixelArray<Lumb>(width, height, fScreenImage.Orientation, new Lumb());

            // Start the thread that will take snapshots of the screen
            fGlobalTimer = new PrecisionTimer();
            fFrameRate = 10; // Frames per second
            fSnapperRunning = true;
            fSnapperThread = new Thread(RunSnaps);
            // WAA
            //fSnapperThread.Start();
        }

        /// <summary>
        /// Take a snapshot of the current held context.  Use the rectangle as the area of the source
        /// that is to be captured.
        /// </summary>
        /// <param name="rect">The Source area to be captured.</param>
        /// <param name="pixMap">The PixelBuffer object that will receive the snapshot.</param>
        public void SnapAPicture(Rectangle rect, GDIDIBSection pixMap)
        {
            pixMap.DeviceContext.BitBlt(fContext, rect.X, rect.Y, new Rectangle(0, 0, rect.Width, rect.Height),
                (TernaryRasterOps.SRCCOPY | TernaryRasterOps.CAPTUREBLT));
        }

        void SnapClientArea()
        {
            Rectangle cRect = new Rectangle(new Point(0, 0), new Size(fResolution.Columns, fResolution.Rows));

            // If we have resized since last picture, then 
            // resize the capture buffer before taking
            // the next snapshot.
            if (fNeedsResize)
            {
                fScreenImage = new GDIDIBSection(cRect.Width, cRect.Height);
                fGrayImage = new PixelArray<Lumb>(cRect.Width, cRect.Height, fScreenImage.Orientation, new Lumb());

                fNeedsResize = false;
            }


            // Now we actually take the snapshot.
            // We pass in the client area, based in screen coordinates
            // and the PixelBuffer object to capture into.
            SnapAPicture(new Rectangle(0, 0, fScreenImage.Width, fScreenImage.Height), fScreenImage);

        }

        void SendSnapshot()
        {
            if (fIsMinimized)
                return;

            double captureStart = fGlobalTimer.GetElapsedSeconds();
            SnapClientArea();
            double captureDuration = fGlobalTimer.GetElapsedSeconds() - captureStart;

            fCommandDispatcher.PixBltPixelBuffer24(fScreenImage, 0, 0);
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
