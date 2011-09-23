using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Imaging;
using NewTOAPIA.Media;
using NewTOAPIA.Media.Capture;
using NewTOAPIA.DirectShow;
using NewTOAPIA.Net.Rtp;

namespace CameraServer
{
    // This class encapsulates the functionality of sending video images
    // to a network interface.
    public class VideoSender
    {
        public delegate void ReceivedVideoFrameHandler(PixelAccessorBGRb accessor);

        public event ReceivedVideoFrameHandler ReceivedVideoFrameEvent;

        VideoCaptureDevice fCamera;
        PayloadChannel fChannel;
        MemoryStream fImageStream;

        public VideoSender(int deviceIndex, PayloadChannel channel)
        {           
            fChannel = channel;

            fImageStream = new MemoryStream(1024);

            fCamera = VideoCaptureDevice.CreateCaptureDeviceFromIndex(0, -1, -1);
            PrintCamera(fCamera);

            fCamera.NewFrame += new CameraEventHandler(ReceiveFrameFromCamera);
        }

        void PrintCamera(VideoCaptureDevice device)
        {
            List<VideoCapability> caps = fCamera.Capabilities;

            foreach (VideoCapability vCap in caps)
            {
                Console.WriteLine("Major Type: {0} - {1}", vCap.MajorTypeString, vCap.SubTypeString);
                Console.WriteLine("  Input Size: {0}", vCap.InputSize);
                Console.WriteLine("  Frame Size: {0} - {1}", vCap.MinFrameSize, vCap.MaxFrameSize);
                Console.WriteLine("  Frame Rate: {0} - {1}", vCap.MinFrameRate, vCap.MaxFrameRate);
                Console.WriteLine("");
            }
        }

        public int Width
        {
            get { return fCamera.Width; }
        }

        public int Height
        {
            get { return fCamera.Height; }
        }

        void ReceiveFrameFromCamera(object sender, CameraEventArgs camEvent)
        {
            // Wrap the frame up in a PixelAccessor
            PixelAccessorBGRb accessor = new PixelAccessorBGRb(camEvent.Width, camEvent.Height, PixmapOrientation.BottomToTop, camEvent.fData, camEvent.Width * 3);

            // Tell whomever is listening that we've received a video event
            if (null != ReceivedVideoFrameEvent)
                ReceivedVideoFrameEvent(accessor);

            SendVideoFrame(accessor);
        }

        public void Start()
        {
            fCamera.Start();
        }

        public void Stop()
        {
            fCamera.SignalToStop();
            fCamera.WaitForStop();
        }


        void SendVideoFrame(PixelAccessor<BGRb> aFrame)
        {
            BufferChunk chunk = EncodeBGRb(aFrame, 0, 0);

            fChannel.Send(chunk);
        }

        BufferChunk EncodeBGRb(PixelAccessor<BGRb> accessor, int x, int y)
        {
            fImageStream.SetLength(0);

            // 2. Run length encode the image to a memory stream
            NewTOAPIA.Imaging.TargaRunLengthCodec rlc = new NewTOAPIA.Imaging.TargaRunLengthCodec();
            rlc.Encode(accessor, fImageStream);

            // 3. Get the bytes from the stream
            byte[] imageBytes = fImageStream.GetBuffer();
            int dataLength = (int)imageBytes.Length;

            // 4. Allocate a buffer chunk to accomodate the bytes, plus some more
            BufferChunk chunk = new BufferChunk(dataLength + 128);

            // 5. Put the command, destination, and data size into the buffer first
            chunk += (int)UICommands.PixBltRLE;
            chunk += (int)x;
            chunk += (int)y;
            chunk += (int)accessor.Width;
            chunk += (int)accessor.Height;
            chunk += dataLength;

            // 6. Put the image bytes into the chunk
            chunk += imageBytes;


            // 7. Finally, return the packet
            return chunk;
        }
    }
}
