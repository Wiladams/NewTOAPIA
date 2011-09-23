using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public class SpaceCommandDecoder : IReceiveSpaceControl
    {
        IReceiveSpaceControl fReceiver;

        public SpaceCommandDecoder(IReceiveSpaceControl commandReceiver)
        {
            fReceiver = commandReceiver;
        }

        public virtual void ReceiveFrame(RtpStream.FrameReceivedEventArgs ea)
        {
            ReceiveData(ea.Frame);
        }

        public virtual void ReceiveData(BufferChunk aRecord)
        {
            // First read out the record type
            int recordType = aRecord.NextInt32();

            Console.WriteLine("SpaceCommandDecoder.ReceiveData: {0}", recordType);

            switch (recordType)
            {
                case SpaceControlChannel.SC_CreateSurface:
                    {
                        Guid uniqueID = CodecUtils.UnpackGuid(aRecord);
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int width = aRecord.NextInt32();
                        int height = aRecord.NextInt32();

                        OnCreateSurface("title",new RECT(x,y,width,height), uniqueID);
                    }
                    break;

                case SpaceControlChannel.SC_InvalidateSurfaceRect:
                    {
                        Guid uniqueID = CodecUtils.UnpackGuid(aRecord);
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int width = aRecord.NextInt32();
                        int height = aRecord.NextInt32();

                        OnInvalidateSurfaceRect(uniqueID, new RECT(x, y, width, height));
                    }
                    break;

                case SpaceControlChannel.SC_MouseEvent:
                    {
                        Guid sourceID = CodecUtils.UnpackGuid(aRecord);
                        uint mouseID = aRecord.NextUInt32();
                        MouseEventType eventType = (MouseEventType)aRecord.NextInt32();
                        MouseButtons buttons = (MouseButtons)aRecord.NextInt32();
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int clicks = aRecord.NextInt32();
                        int delta = aRecord.NextInt32();

                        MouseEventArgs args = new MouseEventArgs(mouseID, eventType, buttons, clicks, x, y, delta, sourceID);
                        
                        OnMouseActivity(this, args);
                    }
                    break;

                case SpaceControlChannel.SC_KeyboardEvent:
                    break;

                //case SpaceControlChannel.SC_BitBlt:
                //  {
                //    // Get the X, Y
                //    int x = aRecord.NextInt32();
                //    int y = aRecord.NextInt32();
                //    int width = aRecord.NextInt32();
                //    int height = aRecord.NextInt32();

                //    // Now create a pixbuff on the specified size
                //    PixelBuffer pixBuff = new PixelBuffer(width, height);
                //    int dataSize = aRecord.NextInt32();

                //    // Copy the received data into it right pixel data pointer
                //    aRecord.CopyTo(pixBuff.Pixels.Data, dataSize);

                //    // And finally, call the BitBlt function
                //    OnBitBlt(x,y,pixBuff);
                //}
                //  break;

                case SpaceControlChannel.SC_CopyPixels:
                  {
                      // Get the X, Y
                      int x = aRecord.NextInt32();
                      int y = aRecord.NextInt32();
                      int width = aRecord.NextInt32();
                      int height = aRecord.NextInt32();

                      // Now create a pixbuff on the specified size
                      PixelBuffer pixBuff = new PixelBuffer(width, height);
                      int dataSize = aRecord.NextInt32();

                      // Copy the received data into it right pixel data pointer
                      aRecord.CopyTo(pixBuff.Pixels.Data, dataSize);

                      // And finally, call the BitBlt function
                      OnCopyPixels(x, y, width, height, pixBuff);
                  }
                  break;

                case SpaceControlChannel.SC_AlphaBlend:
                {
                    // Get the X, Y
                    int x = aRecord.NextInt32();
                    int y = aRecord.NextInt32();
                    int width = aRecord.NextInt32();
                    int height = aRecord.NextInt32();

                    int srcX = aRecord.NextInt32();
                    int srcY = aRecord.NextInt32();
                    int srcWidth = aRecord.NextInt32();
                    int srcHeight = aRecord.NextInt32();

                    byte alpha = aRecord.NextByte();

                    // Now create a pixbuff on the specified size
                    int buffWidth = aRecord.NextInt32();
                    int buffHeight = aRecord.NextInt32();
                    PixelBuffer pixBuff = new PixelBuffer(buffWidth, buffHeight);
                    int dataSize = aRecord.NextInt32();

                    // Copy the received data into it right pixel data pointer
                    aRecord.CopyTo(pixBuff.Pixels.Data, dataSize);

                    // And finally, call the BitBlt function
                    OnAlphaBlend(x,y,width,height, pixBuff,srcX,srcY, srcWidth,srcHeight,alpha);
                }
                    break;

                case SpaceControlChannel.SC_ScaleBitmap:
                    break;

                case SpaceControlChannel.SC_None:
                default:
                    break;
            }
        }

        #region Surface Management
        public virtual void OnCreateSurface(string title, RECT frame, Guid uniqueID)
        {
            if (null != fReceiver)
                fReceiver.OnCreateSurface(title, frame, uniqueID);
        }

        public virtual void OnSurfaceCreated(Guid uniqueID)
        {
            if (null != fReceiver)
                fReceiver.OnSurfaceCreated(uniqueID);
        }

        public virtual void OnInvalidateSurfaceRect(Guid surfaceID, RECT rect)
        {
            if (null != fReceiver)
                fReceiver.OnInvalidateSurfaceRect(surfaceID, rect);
        }


        public virtual void OnValidateSurface(Guid surfaceID)
        {
            if (null != fReceiver)
                fReceiver.OnValidateSurface(surfaceID);
        }

        #endregion

        #region Mouse Activity
        public virtual void OnMouseActivity(Object sender, MouseActivityArgs mevent)
        {
            if (null != fReceiver)
                fReceiver.OnMouseActivity(sender, mevent);
        }
        #endregion

        #region Bitmap Management
        //public virtual void OnBitBlt(int x, int y, PixelBuffer pixBuff)
        //{
        //    if (null != fReceiver)
        //        fReceiver.OnBitBltEvent(x, y, pixBuff);
        //}

        public virtual void OnCopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            if (null != fReceiver)
                fReceiver.OnCopyPixels(x, y, width, height, pixBuff);
        }

        public virtual void OnAlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            if (null != fReceiver)
                fReceiver.OnAlphaBlend(x, y, width, height,
                    bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }


        #endregion
    }
}
