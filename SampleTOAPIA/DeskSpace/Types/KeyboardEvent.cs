using System;

    public enum KeyEventType
    {
        /// <summary>
        /// A virtual key has been pressed.
        /// </summary>
        KeyDown = 0,

        /// <summary>
        /// A virtual key has been released.
        /// </summary>
        KeyUp = 1,

        /// <summary>
        /// A virtual key has been turned into an actual character.
        /// </summary>
        KeyChar = 2
    }

    /// <summary>
    /// Keyboard events represent keyboard activity.  The event can be a KeyDown, KeyUp,
    /// or KeyChar.  The down and up events report the raw virtual key code that is
    /// being pressed and released.
    /// The KeyChar event type represents an actual key once it has been translated
    /// by the system.
    /// </summary>
    public class KeyboardEvent : EventArgs
    {
        KeyEventType fEventType;
        private readonly Keyboard.VirtualKeyCodes fKeyData;
        private readonly int fKeyFlags;
        private readonly char fKeyChar;

        public KeyboardEvent(KeyEventType eventType, 
            Keyboard.VirtualKeyCodes keyData, 
            int aKeyFlags, char aChar)
        {
            fEventType = eventType;
            fKeyData = keyData;
            fKeyFlags = aKeyFlags;
            fKeyChar = aChar;
        }

        public Keyboard.VirtualKeyCodes VirtualKeyCode
        {
            get
            {
                return fKeyData;
            }
        }

        public int KeyFlags
        {
            get { return fKeyFlags; }
        }

        public char Character
        {
            get { return fKeyChar; }
        }

        public Keyboard.KeyMasks Modifiers
        {
            get
            {
                Keyboard.KeyMasks mods = (Keyboard.KeyMasks)((int)fKeyData & (int)Keyboard.KeyMasks.Modifiers);
                return mods;
            }
        }

        public bool Alt
        {
            get
            {
                return ((int)fKeyData & (int)Keyboard.KeyMasks.Alt) == (int)Keyboard.KeyMasks.Alt;
            }
        }

        public bool Control
        {
            get
            {
                return ((int)fKeyData & (int)Keyboard.KeyMasks.Control) == (int)Keyboard.KeyMasks.Control;
            }
        }

        public bool Shift
        {
            get
            {
                return ((int)fKeyData & (int)Keyboard.KeyMasks.Shift) == (int)Keyboard.KeyMasks.Shift;
            }
        }
    }





	public class KeyEventArgs : EventArgs
	{
		private readonly Keyboard.VirtualKeyCodes fKeyData;
        private readonly int fKeyFlags;
		private bool fHandled;

		public KeyEventArgs(Keyboard.VirtualKeyCodes keyData, int aKeyFlags)
		{
			fKeyData = keyData;
            fKeyFlags = aKeyFlags;
        }


		public bool Handled 
		{
			get 
			{
				return fHandled;
			}
			set 
			{
				fHandled = value;
			}
		}

		public Keyboard.VirtualKeyCodes VirtualKeyCode 
		{
			get 
			{
				return fKeyData;
			}
		}

        public int KeyFlags
        {
            get { return fKeyFlags; }
        }

		public Keyboard.KeyMasks Modifiers 
		{
			get 
			{
                Keyboard.KeyMasks mods = (Keyboard.KeyMasks)((int)fKeyData & (int)Keyboard.KeyMasks.Modifiers);
				return mods;
			}
		}

        public bool Alt
        {
            get
            {
                return ((int)fKeyData & (int)Keyboard.KeyMasks.Alt) == (int)Keyboard.KeyMasks.Alt;
            }
        }

        public bool Control
        {
            get
            {
                return ((int)fKeyData & (int)Keyboard.KeyMasks.Control) == (int)Keyboard.KeyMasks.Control;
            }
        }

		public bool Shift 
		{
			get 
			{
				return ((int)fKeyData & (int)Keyboard.KeyMasks.Shift) == (int)Keyboard.KeyMasks.Shift;
			}
		}
	}


    /// <summary>
    ///    Summary description for KeyPressEventArgs.
    /// </summary>
    public class KeyCharEventArgs : EventArgs
    {
        private readonly char fKeyChar;
        private bool fHandled;

        public KeyCharEventArgs(char keyChar)
        {
            fKeyChar = keyChar;
        }

        public char KeyChar
        {
            get
            {
                return fKeyChar;
            }
        }

        public bool Handled
        {
            get
            {
                return fHandled;
            }

            set
            {
                fHandled = value;
            }
        }
    }

