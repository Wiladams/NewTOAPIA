using System;

[Flags]
public enum MouseButtons
{

    None = 0x00000000,

    Left = 0x00100000,
    Right = 0x00200000,
    Middle = 0x00400000,

    XButton1 = 0x0080000,
    XButton2 = 0x0100000,
}

        public class Mouse
        {
            bool fIsPresent;
            bool fButtonsSwapped;
            bool fWheelPresent;
            int fNumButtons;

            /// <summary>
            /// Default constructor.  Creates a Mouse Object for the default mouse on the system.
            /// The various properties of the mouse are already set.
            /// </summary>
            public Mouse()
            {
                fIsPresent = false;
                fButtonsSwapped = false;
                fWheelPresent = false;
                fNumButtons = 0;
            }

            public bool IsPresent
            {
                get { return fIsPresent; }
            }

            public bool ButtonsAreSwapped
            {
                get { return fButtonsSwapped; }
            }

            public bool HasWheel
            {
                get { return fWheelPresent; }
            }

            public int NumberOfButtons
            {
                get { return fNumButtons; }
            }


        }

