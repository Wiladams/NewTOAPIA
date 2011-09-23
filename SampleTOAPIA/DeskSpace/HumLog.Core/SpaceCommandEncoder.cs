using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net.Rtp;

namespace HumLog
{
    public class SpaceCommandEncoder : ISendSpaceControl
    {
        //public delegate void PackCommandEventHandler(BufferChunk chunk);
        //public event PackCommandEventHandler ChunkPackedEvent;
        
        RtpSender fSender;

        // So the default constructor can not be called casually
        private SpaceCommandEncoder()
        {
        }

        public SpaceCommandEncoder(RtpSender sender)
        {
            fSender = sender;
        }

        void PackCommand(BufferChunk aCommand)
        {
            if (fSender != null)
                fSender.Send(aCommand);
        }

        public virtual void CreateSurface(string title, RECT frame, Guid uniqueID)
        {
            BufferChunk chunk = new BufferChunk(1024);

            chunk += SpaceControlChannel.SC_CreateSurface;
            CodecUtils.Pack(chunk,uniqueID);
            CodecUtils.Pack(chunk,frame.Left, frame.Top, frame.Width, frame.Height);

            PackCommand(chunk);
        }

        #region Surface Validation
        public virtual void ValidateSurface(Guid surfaceID)
        {
            //BufferChunk chunk = new BufferChunk(1024);

            //chunk += SpaceControlChannel.SC_ValidateSurface;
            //CodecUtils.Pack(chunk, surfaceID);
            //CodecUtils.Pack(chunk, frame.Left, frame.Top, frame.Width, frame.Height);

            //PackCommand(chunk);
        }

        public virtual void InvalidateSurfaceRect(Guid surfaceID, RECT frame)
        {
            BufferChunk chunk = new BufferChunk(1024);

            chunk += SpaceControlChannel.SC_InvalidateSurfaceRect;
            CodecUtils.Pack(chunk, surfaceID);
            CodecUtils.Pack(chunk, frame.Left, frame.Top, frame.Width, frame.Height);

            PackCommand(chunk);
        }

        #endregion
        #region Drawing Bitmaps
        public virtual void CopyPixels(int x, int y, int width, int height, PixelBuffer24 pixBuff)
        {
            // Create a buffer
            // It has to be big enough for the bitmap data, as well as the x,y, and command
            int dataSize = (width) * (height) * pixBuff.Pixels.BytesPerPixel;
            BufferChunk chunk = new BufferChunk(dataSize + 128);

            // now put the basic command and simple components in
            chunk += SpaceControlChannel.SC_CopyPixels;
            CodecUtils.Pack(chunk, x, y, width, height);
            chunk += dataSize;

            // Finally, copy in the data
            int numBytesPerRow = pixBuff.Pixels.BytesPerPixel * width;
            IntPtr rowPtr = pixBuff.Pixels.Data;
            int offset = 0;
            int row = 0;
            for (row = y; row < (y + height); row++)
            {
                offset = pixBuff.Pixels.Data.ToInt32() + row * pixBuff.Pixels.Stride + x * pixBuff.Pixels.BytesPerPixel;
                rowPtr = (IntPtr)offset;
                chunk.CopyFrom(rowPtr, numBytesPerRow);
            }

            PackCommand(chunk);
        }

        //public override void BitBlt(int x, int y, PixelBuffer pixBuff)
        //{
        //    // Create a buffer
        //    // It has to be big enough for the bitmap data, as well as the x,y, and command
        //    int dataSize = pixBuff.Pixels.Stride * pixBuff.Pixels.Height;
        //    BufferChunk chunk = new BufferChunk(dataSize + 128);

        //    // now put the basic command and simple components in
        //    chunk += SpaceControlChannel.SC_BitBlt;
        //    CodecUtils.Pack(chunk, x, y);
        //    CodecUtils.Pack(chunk, pixBuff.Pixels.Width, pixBuff.Pixels.Height);
        //    chunk += dataSize;

        //    // Finally, copy in the data
        //    chunk.CopyFrom(pixBuff.Pixels.Data, dataSize);

        //    PackCommand(chunk);
        //}

        public virtual void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer24 pixBuff, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            // Create a buffer
            // It has to be big enough for the bitmap data, as well as the x,y, and command
            int dataSize = pixBuff.Pixels.BytesPerPixel * srcWidth * srcHeight;
            BufferChunk chunk = new BufferChunk(dataSize + 128);

            // now put the basic command and simple components in
            chunk += SpaceControlChannel.SC_AlphaBlend;
            CodecUtils.Pack(chunk, x, y, width, height);
            CodecUtils.Pack(chunk, srcX, srcY, srcWidth, srcHeight);
            chunk += alpha;

            CodecUtils.Pack(chunk, srcWidth, srcHeight);
            chunk += dataSize;

            // Finally, copy in the data
            // One row at a time
            int numBytesPerRow = pixBuff.Pixels.BytesPerPixel * srcWidth;
            IntPtr rowPtr = pixBuff.Pixels.Data;
            int offset = rowPtr.ToInt32() + srcY * pixBuff.Pixels.Stride + srcX * pixBuff.Pixels.BytesPerPixel;
            for (int row = srcY; row < (srcY + srcHeight); row++)
            {
                rowPtr = (IntPtr)offset;
                chunk.CopyFrom(rowPtr, numBytesPerRow);
                offset = rowPtr.ToInt32() + row * pixBuff.Pixels.Stride + srcX * pixBuff.Pixels.BytesPerPixel;
            }

            PackCommand(chunk);

        }

        public virtual void ScaleBitmap(PixelBuffer24 aBitmap, RECT aFrame)
        {
            AlphaBlend(aFrame.Left, aFrame.Top, aFrame.Width, aFrame.Height,
                aBitmap, 0, 0, aBitmap.Width, aBitmap.Height, aBitmap.Alpha);
        }
        #endregion

        #region Mouse Events
        public virtual void MouseActivity(object sender, NewTOAPIA.UI.MouseActivityArgs mevent)
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
