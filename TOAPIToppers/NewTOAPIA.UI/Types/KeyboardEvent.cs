using System;


namespace NewTOAPIA.UI
{
    /// <summary>
    /// KeyboardDevice events represent keyboard activity.  The event can be a KeyDown, KeyUp,
    /// or KeyChar.  
    /// The down and up events report the raw virtual key code that is
    /// being pressed and released.
    /// The KeyChar event type represents an actual key once it has been translated
    /// by the system.
    /// </summary>
    public class KeyboardEvent : EventArgs
    {
        KeyboardDevice fKeyboard;
        KeyEventType fEventType;
        private readonly VirtualKeyCodes fKeyData;
        private readonly int fKeyFlags;
        private readonly char fKeyChar;

        public KeyboardEvent(KeyboardDevice keyboard, KeyEventType eventType,
            VirtualKeyCodes keyData,
            int aKeyFlags, char aChar)
        {
            fKeyboard = keyboard;
            fEventType = eventType;
            fKeyData = keyData;
            fKeyFlags = aKeyFlags;
            fKeyChar = aChar;
        }

        public KeyboardDevice Keyboard
        {
            get { return fKeyboard; }
        }

        public VirtualKeyCodes VirtualKeyCode
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

        public KeyEventType EventType
        {
            get { return fEventType; }
        }

        public KeyMasks Modifiers
        {
            get
            {
                KeyMasks mods = (KeyMasks)((int)fKeyData & (int)KeyMasks.Modifiers);
                return mods;
            }
        }

        public bool Alt
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Alt) == (int)KeyMasks.Alt;
            }
        }

        public bool Control
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Control) == (int)KeyMasks.Control;
            }
        }

        public bool Shift
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Shift) == (int)KeyMasks.Shift;
            }
        }
    }





    public class KeyEventArgs : EventArgs
    {
        private readonly VirtualKeyCodes fKeyData;
        private readonly int fKeyFlags;
        private bool fHandled;

        public KeyEventArgs(VirtualKeyCodes keyData, int aKeyFlags)
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

        public VirtualKeyCodes VirtualKeyCode
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

        public KeyMasks Modifiers
        {
            get
            {
                KeyMasks mods = (KeyMasks)((int)fKeyData & (int)KeyMasks.Modifiers);
                return mods;
            }
        }

        public bool Alt
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Alt) == (int)KeyMasks.Alt;
            }
        }

        public bool Control
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Control) == (int)KeyMasks.Control;
            }
        }

        public bool Shift
        {
            get
            {
                return ((int)fKeyData & (int)KeyMasks.Shift) == (int)KeyMasks.Shift;
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
}