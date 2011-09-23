using System;

namespace NewTOAPIA.UI
{

    public class MouseActivityArgs : EventArgs
    {
        MouseDevice fMouseDevice;
        MouseEventType fEventType;

        private readonly MouseButtons fButton;

        private readonly int fClicks;
        private readonly int fX;
        private readonly int fY;
        private readonly int fDelta;

        //public MouseActivityArgs(MouseButtons button, int clicks, short x, short y, int delta)
        //    : this(button, clicks, x, y, delta, Guid.Empty)
        //{
        //    this.fButton = button;
        //    this.fClicks = clicks;
        //    this.fX = x;
        //    this.fY = y;
        //    this.fDelta = delta;
        //}

        public MouseActivityArgs(MouseDevice device, short x, short y)
            :this(device,MouseEventType.MouseMove,MouseButtons.None,
            0,x,y,0)
        {
        }

        public MouseActivityArgs(MouseDevice device, MouseEventType eventType, MouseButtons button, 
            int clicks, int x, int y, int delta)
        {
            fMouseDevice = device;
            fEventType = eventType;

            fButton = button;
            fClicks = clicks;
            fX = x;
            fY = y;
            fDelta = delta;
        }



        public MouseButtons Button
        {
            get
            {
                return fButton;
            }
        }

        public int Clicks
        {
            get
            {
                return fClicks;
            }
        }

        public MouseEventType EventType
        {
            get { return fEventType; }
        }

        public MouseDevice Mouse
        {
            get { return fMouseDevice; }
        }

        public int X
        {
            get
            {
                return fX;
            }
        }

        public int Y
        {
            get
            {
                return fY;
            }
        }

        /// <summary>
        ///    <para>
        ///       Gets
        ///       a signed count of the number of detents the mouse wheel has rotated.
        ///    </para>
        /// </summary>
        public int Delta
        {
            get
            {
                return fDelta;
            }
        }

        public override string ToString()
        {
            return "<MouseEvent X='" + X.ToString() +
                "' Y='" + Y.ToString() +
                "' Delta = '" + Delta.ToString() +
                "' Clicks = '" + Clicks.ToString() +
                "' Button = '" + Button.ToString() +
                "'" +
                " />";

        }
    }
}