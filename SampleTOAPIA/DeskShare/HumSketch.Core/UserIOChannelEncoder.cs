using System;

using NewTOAPIA;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.UI;

namespace DistributedDesktop
{
    public class UserIOChannelEncoder : UserIODelegate
    {
        PayloadChannel fCommandChannel;

        public UserIOChannelEncoder(PayloadChannel commandChannel)
        {
            fCommandChannel = commandChannel;
        }

        void SendCommand(BufferChunk aCommand)
        {
            fCommandChannel.Send(aCommand);
        }

        public override void ShowCursor()
        {
            BufferChunk bc = new BufferChunk(128);
            
            bc += (int)UserIOCommand.Showcursor;

            SendCommand(bc);
        }

        public override void HideCursor()
        {
            BufferChunk bc = new BufferChunk(128);
            
            bc += (int)UserIOCommand.HideCursor;

            SendCommand(bc);
        }

        public override void MoveCursor(int x, int y)
        {
            BufferChunk bc = new BufferChunk(128);
            
            bc += (int)UserIOCommand.MoveCursor;
            
            bc += x;
            bc += y;

            SendCommand(bc);
        }

        public override void MouseActivity(Object sender, MouseActivityArgs ma)
        {
            BufferChunk bc = new BufferChunk(128);
            
            bc += (int)UserIOCommand.MouseActivity;
            
            bc += (int)ma.ButtonActivity;
            bc += (int)ma.X;
            bc += (int)ma.Y;
            bc += (int)ma.Clicks;
            bc += (int)ma.Delta;

            SendCommand(bc);
        }

        public override IntPtr KeyboardActivity(Object sender, KeyboardActivityArgs kbda)
        {
            BufferChunk bc = new BufferChunk(128);

            bc += (int)UserIOCommand.KeyboardActivity;
            
            bc += (int)kbda.AcitivityType;
            bc += (int)kbda.VirtualKeyCode;

            SendCommand(bc);

            return IntPtr.Zero;
        }
    }
}
