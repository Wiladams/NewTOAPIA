using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Drawing;

namespace HamSketch
{
    public class SpaceCommandEncoder : SpaceControlDelegate
    {
        public delegate void PackCommandEventHandler(BufferChunk chunk);

        public event PackCommandEventHandler ChunkPackedEvent;

        void PackCommand(BufferChunk aCommand)
        {
            if (ChunkPackedEvent != null)
                ChunkPackedEvent(aCommand);
        }


        #region Drawing Bitmaps
        public override void BitBlt(int x, int y, PixelBuffer pixBuff)
        {
            // Create a buffer
            // It has to be big enough for the bitmap data, as well as the x,y, and command
            int dataSize = pixBuff.Pixels.Stride * pixBuff.Pixels.Height;
            BufferChunk chunk = new BufferChunk(dataSize + 128);

            // now put the basic command and simple components in
            chunk += SpaceControlChannel.SC_BitBlt;
            CodecUtils.Pack(chunk, x, y);
            CodecUtils.Pack(chunk, pixBuff.Pixels.Width, pixBuff.Pixels.Height);
            chunk += dataSize;

            // Finally, copy in the data
            chunk.CopyFrom(pixBuff.Pixels.Data, dataSize);

            PackCommand(chunk);
        }

        public override void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer pixBuff, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            // Create a buffer
            // It has to be big enough for the bitmap data, as well as the x,y, and command
            int dataSize = pixBuff.Pixels.Stride * pixBuff.Pixels.Height;
            BufferChunk chunk = new BufferChunk(dataSize + 128);

            // now put the basic command and simple components in
            chunk += SpaceControlChannel.SC_AlphaBlend;
            CodecUtils.Pack(chunk, x, y, width, height);
            CodecUtils.Pack(chunk, srcX, srcY, srcWidth, srcHeight);
            chunk += alpha;

            CodecUtils.Pack(chunk, pixBuff.Pixels.Width, pixBuff.Pixels.Height);
            chunk += dataSize;

            // Finally, copy in the data
            chunk.CopyFrom(pixBuff.Pixels.Data, dataSize);

            PackCommand(chunk);

        }

        public override void ScaleBitmap(PixelBuffer aBitmap, RECT aFrame)
        {
            AlphaBlend(aFrame.Left, aFrame.Top, aFrame.Width, aFrame.Height,
                aBitmap, 0, 0, aBitmap.Width, aBitmap.Height, aBitmap.Alpha);
        }
        #endregion

        #region Mouse Events
        public override void MouseActivity(object sender, NewTOAPIA.UI.MouseEventArgs mevent)
        {
            BufferChunk chunk = new BufferChunk(1024);

            chunk += SpaceControlChannel.SC_MouseEvent;
            CodecUtils.Pack(chunk, mevent.Source);
            chunk += mevent.MouseID;
            chunk += (int)mevent.EventType;
            chunk += (int)mevent.Button;
            chunk += mevent.X;
            chunk += mevent.Y;
            chunk += mevent.Clicks;
            chunk += mevent.Delta;
            
            PackCommand(chunk);
        } 
        #endregion
    }
}
