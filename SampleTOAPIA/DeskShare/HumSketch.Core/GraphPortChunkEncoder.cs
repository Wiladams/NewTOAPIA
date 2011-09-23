using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

using NewTOAPIA;
//using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.Drawing;


namespace DistributedDesktop
{
    public class GraphPortChunkEncoder : UIPortDelegate
    {
        PayloadChannel fCollaborationChannel;

        #region Constructor
        public GraphPortChunkEncoder(PayloadChannel aChannel)
        {
            fCollaborationChannel = aChannel;
        }
        #endregion

        void SendCommand(BufferChunk aCommand)
        {
            fCollaborationChannel.Send(aCommand);
        }

        #region Drawing Pixel Maps

        public override void PixBltPixelArray(IPixelArray pixBuff, int x, int y)
        {
            //NewTOAPIA.Kernel.PrecisionTimer timer = new NewTOAPIA.Kernel.PrecisionTimer();

            // 1. convert the pixel array to a Bitmap object
            Bitmap bm = PixelBufferHelper.CreateBitmapFromPixelArray(pixBuff);

            // 2. Write this bitmap to a memory stream as a compressed JPEG image
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            // 3. Get the bytes from the stream
            byte[] imageBytes = ms.GetBuffer();

            // 4. Allocate a buffer chunk to accomodate the bytes, plus some more
            BufferChunk chunk = new BufferChunk(imageBytes.Length + 128);

            // 5. Put the command, destination, and data size into the buffer first
            chunk += (int)UICommands.PixBltJPG;
            ChunkUtils.Pack(chunk, x, y);
            chunk += (int)imageBytes.Length;

            // 6. Put the image bytes into the chunk
            chunk += imageBytes;
            //double duration = timer.GetElapsedSeconds();
            //Console.WriteLine("Encoder - Time to pack image: {0}", duration);

            // Finally, send the packet
            SendCommand(chunk);
        }

        public override void PixBltBGRb(PixelAccessor<BGRb> accessor, int x, int y)
        {
            MemoryStream ms = new MemoryStream();

            // 2. Run length encode the image to a memory stream
            NewTOAPIA.Imaging.TargaRunLengthCodec rlc = new NewTOAPIA.Imaging.TargaRunLengthCodec();
            rlc.Encode(accessor, ms);

            // 3. Get the bytes from the stream
            byte[] imageBytes = ms.GetBuffer();
            int dataLength = (int)imageBytes.Length;

            // 4. Allocate a buffer chunk to accomodate the bytes, plus some more
            BufferChunk chunk = new BufferChunk(dataLength + 128);

            // 5. Put the command, destination, and data size into the buffer first
            chunk += (int)UICommands.PixBltRLE;
            ChunkUtils.Pack(chunk, x, y);
            ChunkUtils.Pack(chunk, accessor.Width, accessor.Height);
            chunk += dataLength;

            // 6. Put the image bytes into the chunk
            chunk += imageBytes;


            // 6. Finally, send the packet
            SendCommand(chunk);
        }

        public override void PixBltBGRAb(PixelArray<BGRAb> pixBuff, int x, int y)
        {
            PixBltPixelArray(pixBuff, x, y);
        }

        public override void PixBltLumb(PixelArray<Lumb> pixMap, int x, int y)
        {
            MemoryStream ms = new MemoryStream();

            // 2. Run length encode the image to a memory stream
            NewTOAPIA.Imaging.TargaLuminanceRLE rlc = new NewTOAPIA.Imaging.TargaLuminanceRLE();
            rlc.Encode(pixMap, ms);

            // 3. Get the bytes from the stream
            byte[] imageBytes = ms.GetBuffer();
            int dataLength = (int)imageBytes.Length;

            // 4. Allocate a buffer chunk to accomodate the bytes, plus some more
            BufferChunk chunk = new BufferChunk(dataLength + 128);

            // 5. Put the command, destination, and data size into the buffer first
            chunk += (int)UICommands.PixBltLuminance;
            ChunkUtils.Pack(chunk, x, y);
            ChunkUtils.Pack(chunk, pixMap.Width, pixMap.Height);
            chunk += dataLength;

            // 6. Put the image bytes into the chunk
            chunk += imageBytes;


            // 6. Finally, send the packet
            SendCommand(chunk);
        }

        #endregion

    }
}