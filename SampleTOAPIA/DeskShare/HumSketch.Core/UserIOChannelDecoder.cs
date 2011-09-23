using System;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Net.Rtp;

namespace DistributedDesktop
{
    public class UserIOChannelDecoder : UserIODelegate
    {
        PayloadChannel fCommandChannel;

        public UserIOChannelDecoder(PayloadChannel commandChannel)
        {
            fCommandChannel = commandChannel;
            fCommandChannel.FrameReceivedEvent += FrameReceived;
        }

        private void FrameReceived(object sender, RtpStream.FrameReceivedEventArgs ea)
        {
            ReceiveChunk(ea.Frame);
        }

        public virtual void ReceiveChunk(BufferChunk aRecord)
        {
            // First read out the record type
            int recordType = aRecord.NextInt32();

            // Then deserialize the rest from there
            switch ((UserIOCommand)recordType)
            {
                case UserIOCommand.HideCursor:
                    HideCursor();
                    break;

                case UserIOCommand.Showcursor:
                    ShowCursor();
                    break;

                case UserIOCommand.MoveCursor:
                    {
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();

                        MoveCursor(x, y);
                    }
                    break;

                case UserIOCommand.KeyboardActivity:
                    {
                        KeyActivityType kEvent = (KeyActivityType)aRecord.NextInt32();
                        VirtualKeyCodes vk = (VirtualKeyCodes)aRecord.NextInt32();

                        KeyboardActivityArgs kbda = new KeyboardActivityArgs(kEvent, vk);
                        KeyboardActivity(this, kbda); 
                    }
                    break;

                case UserIOCommand.MouseActivity:
                    {
                        MouseActivityType maType = MouseActivityType.None;
                        MouseButtonActivity mbActivity = (MouseButtonActivity)aRecord.NextInt32();
                        int x = aRecord.NextInt32();
                        int y = aRecord.NextInt32();
                        int clicks = aRecord.NextInt32();
                        short delta = aRecord.NextInt16();
                        int keyflags = 0;

                        MouseActivityArgs ma = new MouseActivityArgs(null, maType, mbActivity, 
                            MouseCoordinateSpace.Desktop, MouseMovementType.Absolute, IntPtr.Zero, 
                            x, y, delta, clicks, keyflags);

                        MouseActivity(this, ma);
                    }
                    break;

                default:
                    break;
            }
        }



    }
}
