using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch
{
    public class SpaceCommandDecoder : SpaceControlDelegate
    {

        public virtual void ReceiveData(RtpStream.FrameReceivedEventArgs ea)
        {
            BufferChunk aRecord = ea.Frame;

            // First read out the record type
            int recordType = aRecord.NextInt32();

            switch (recordType)
            {
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

                case SpaceControlChannel.SC_BitBlt:
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
                    BitBlt(x,y,pixBuff);
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
                    AlphaBlend(x,y,width,height, pixBuff,srcX,srcY, srcWidth,srcHeight,alpha);
                }
                    break;

                case SpaceControlChannel.SC_ScaleBitmap:
                    break;

                case SpaceControlChannel.SC_None:
                default:
                    break;
            }
        }
    }
}
