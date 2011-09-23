using System;

    public class MouseEventArgs : EventArgs 
    {

        /// <summary>
        ///     Which button generated this event [if applicable]
        /// </summary>
        private readonly MouseButtons button;

        /// <summary>
        ///     If the user has clicked the mouse more than once, this contains the
        ///     count of clicks so far.
        /// </summary>
        private readonly int clicks;
        private readonly short x;
        private readonly short y;
        private readonly int delta;

        public MouseEventArgs(MouseButtons button, int clicks, short x, short y, int delta) 
		{
            //Debug.Assert((button & (MouseButtons.Left | MouseButtons.None | MouseButtons.Right | MouseButtons.Middle)) ==
            //             button, "Invalid information passed into MouseEventArgs constructor!");

            this.button = button;
            this.clicks = clicks;
            this.x = x;
            this.y = y;
            this.delta = delta;
        }

        public MouseButtons Button 
        {
            get {
                return button;
            }
        }

        public int Clicks {
            get {
                return clicks;
            }
        }

        public short X {
            get {
                return x;
            }
        }

        public short Y {
            get {
                return y;
            }
        }

        /// <summary>
        ///    <para>
        ///       Gets
        ///       a signed count of the number of detents the mouse wheel has rotated.
        ///    </para>
        /// </summary>
        public int Delta {
            get {
                return delta;
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
