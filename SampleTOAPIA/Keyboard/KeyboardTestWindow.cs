using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.Drawing.GDI;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;

namespace KeyboardTest
{
    /// <summary>
    /// The main window for the Keyboard Test Application.
    /// </summary>
	public class KeyboardTestWindow : GraphicWindow
	{
		private string TextStr0, TextStr1;
		private string TextStr2;
		private string TextStr3, TextStr4;
		private string StrON, StrOFF;
		private char [] BuffStr0;
		private char [] BuffStr1;
		private char [] BuffStr2;
		private char [] BuffStr3;
		private char [] BuffStr4;
		
        private StringLabel fCharTypedLabel;
        private StringLabel fBackspaceLabel;
		private StringLabel fShiftLabel;
		//private GraphicCell fGraphicCell;
		//private BinaryLayout fBinaryLayout;
        GDIFont fFont;

        /// <summary>
        /// Default constructor for the window.  The window is located at 0,0 with
        /// a size of 640,480
        /// </summary>
		public KeyboardTestWindow()
			: base("KeyboardTestWindow",10,10,640,480)
		{
            PrintKeyboards();
            PrintMice();

            fFont = new GDIFont("Courier", 12);

			TextStr0 = "00000000 00000000 00000000 00000000 <-- KeyData\n-------- -------- -----------------\n|||    |     |            |        \n|||    |     |            |____ repeat count\n|||    |     |_________________ OEM code\n|||    |_______________ extended key flag\n|||____________________ context code\n||_____________________ previous key state\n|______________________ transition state";
			BuffStr0 = TextStr0.ToCharArray(0,TextStr0.Length);

			TextStr1 = "-------- -------- 00000000 00000000 <-- wParam\n";
			BuffStr1 = TextStr1.ToCharArray(0,TextStr1.Length);

			TextStr2 = "CHARACTER TYPED:   ";
			BuffStr2 = TextStr2.ToCharArray(0,TextStr2.Length);

			TextStr3 = "Backspace key:   ";
			BuffStr3 = TextStr3.ToCharArray(0,TextStr3.Length);
			
			TextStr4 = "Shift state:      ";
			BuffStr4 = TextStr4.ToCharArray(0,TextStr4.Length);

			StrON = "ON ";
			StrOFF = "OFF";

			fCharTypedLabel = new StringLabel("Character Typed", "Courier", 30, 0, 0);
            AddGraphic(fCharTypedLabel);
			
            fBackspaceLabel = new StringLabel("Backspace Key", "Courier", 30, 10, 17 * 17);
            AddGraphic(fBackspaceLabel);
			
            fShiftLabel = new StringLabel("Shift State", "Courier", 30, 0, 0);
			
			// Cell Layout thing
            //Rectangle frame = new Rectangle(0, 0, 400, 60);
            //GraphicGroup cellGroup = new GraphicGroup("cellGroup", frame);
            //cellGroup.LayoutHandler = new GraphicCell(Position.Right, 4); 
            //cellGroup.AddGraphic(fCharTypedLabel, null);

			// Binary layout thing
            //GraphicGroup binaryGroup = new GraphicGroup("binaryGroup", frame);
            //binaryGroup.LayoutHandler = new BinaryLayout(Position.BottomRight, 4); 
            //binaryGroup.AddGraphic(cellGroup, null);
            //binaryGroup.AddGraphic(fShiftLabel, null);

			
            //AddGraphic(binaryGroup, null);
            
            
            //BackgroundColor = RGBColor.LtGray;
		}

        void PrintKeyboards()
        {
            // First, get the list of physical keyboards
            KeyboardDevice[] keyboards = KeyboardDevice.GetPhysicalKeyboards();

            foreach (KeyboardDevice keyboard in keyboards)
            {
                Console.WriteLine("        Class: {0}", keyboard.ClassName);
                Console.WriteLine("       Device: {0}", keyboard.DeviceName);
                Console.WriteLine("  Description: {0}", keyboard.Description);
                Console.WriteLine("         Type: {0}", keyboard.KeyboardType);
                Console.WriteLine("     Sub-Type: {0}", keyboard.Subtype);
                Console.WriteLine("Function Keys: {0}", keyboard.NumberOfFunctionKeys);
            }
        }

        void PrintMice()
        {
            MouseDevice[] mice = MouseDevice.GetPhysicalMice();

            foreach (MouseDevice mouse in mice)
            {
                Console.WriteLine("      Mouse: {0}", mouse.ClassName);
                Console.WriteLine("      Class: {0}", mouse.ClassName);
                Console.WriteLine("Description: {0}", mouse.Description);
                Console.WriteLine("    Buttons: {0}", mouse.NumberOfButtons);
                Console.WriteLine("Sample Rate: {0}", mouse.SampleRate);
            }
        }

        /// <summary>
        /// Drawing that occurs whenever it needs to.
        /// </summary>
        /// <param name="graphPort"></param>
        public override void Draw(DrawEvent de)
        {

            base.Draw(de);

            IGraphPort graphPort = de.GraphPort;
            //Console.WriteLine("**** KeyboardTestWindow - ONPAINT ****");

            // Display multi-line text string 
            int cxChar = 12;
            int cyChar = 17;

            GraphPort.SetTextColor(Colorrefs.Black);
            graphPort.SetFont(fFont);

            //GraphPort.DrawText(new string(BuffStr0), 2 * cxChar, cyChar, Frame.Width -(2 * cxChar), Frame.Height,
            //             GDI.DT_LEFT | GDI.DT_WORDBREAK);
            graphPort.DrawString(2 * cxChar, cyChar, new string(BuffStr0));

            //GraphPort.DrawText(new string(BuffStr1), 2*cxChar, 13*cyChar, Frame.Width -(2 * cxChar), Frame.Height,
            //             GDI.DT_LEFT | GDI.DT_WORDBREAK);
            graphPort.DrawString(2 * cxChar, 13 * cyChar, new string(BuffStr1));

            //// Display text string - character typed
            //GraphPort.DrawText(new string(BuffStr2), 2 * cxChar, 15*cyChar, Frame.Width -(2 * cxChar),Frame.Height,
            //             GDI.DT_LEFT | GDI.DT_WORDBREAK);
            graphPort.DrawString(2 * cxChar, 15 * cyChar, new string(BuffStr2));

            //// Display text string 
            //GraphPort.DrawText(new string(BuffStr3), 2*cxChar, 17*cyChar, Frame.Width -(2 * cxChar),Frame.Height,
            //             GDI.DT_LEFT | GDI.DT_WORDBREAK);
            graphPort.DrawString(2 * cxChar, 17 * cyChar, new string(BuffStr3));

            //// Display text string 
            //GraphPort.DrawText(new string(BuffStr4), 2*cxChar, 19*cyChar, Frame.Width -(2 * cxChar),Frame.Height,
            //             GDI.DT_LEFT | GDI.DT_WORDBREAK);	
            graphPort.DrawString(2 * cxChar, 19 * cyChar, new string(BuffStr4));

        }

        private void DisplayBinary(KeyboardActivityArgs ke)
        {
            int i, j;
            //int keycode = (int)ke.VirtualKeyCode;
            int binarycode = (int)ke.ScanCode;
            uint keymask;

            // Store bits for lParam in TextStr0[]
            i = 0;               // counter for keystroke bits
            j = 0;               // offset into string
            keymask = 0x80000000;// bitmask

            for (i = 0; i < 32; i++)
            {
                // Test for separators and skip
                if (i == 8 || i == 16 || i == 24)
                {
                    BuffStr0[j] = (char)0x20;
                    j++;
                }

                // Test for 1 and 0 bits and display digits
                if ((binarycode & keymask) > 0)
                    BuffStr0[j] = '1';
                else
                    BuffStr0[j] = '0';

                keymask = keymask >> 1;
                j++;
            }
        }

        private void DisplaySpecialsPressed(KeyboardActivityArgs ke)
        {
            int i=0;
            int j = 0;
            int keycode = (int)ke.VirtualKeyCode;

            ushort keycodeonly = (ushort)(keycode & 0xff);
            if ((keycodeonly & (int)VirtualKeyCodes.Back) == keycodeonly)
                BuffStr3[15] = 'Y';
            else
                BuffStr3[15] = 'N';

            // Test for shift key pressed
            if (ke.Shift)
            {
                i = 0;        // counter
                j = 13;       // string offset
                for (i = 0; i < 3; i++)
                {
                    BuffStr4[j] = StrON[i];
                    j++;
                }
            }
            else
            {
                i = 0;        // counter
                j = 13;       // string offset
                for (i = 0; i < 3; i++)
                {
                    BuffStr4[j] = StrOFF[i];
                    j++;
                }
            }
        }

        /// <summary>
        /// Called when a key is pressed on the keyboard.
        /// </summary>
        /// <param name="ke">Contains keyboard event information.</param>
		public override void OnKeyDown(KeyboardActivityArgs ke)
		{
			int i, j;
			uint keymask;
			int keycode = (int)ke.VirtualKeyCode;

			// Store bits for lParam in TextStr0[]
			i = 0;               // counter for keystroke bits
			j = 0;               // offset into string
			keymask = 0x80000000;// bitmask

            // Display the virtual key in binary form
            DisplayBinary(ke);
			   
			// Store bits for wParam in TextStr1[]
			// Since the virtual-key code is a 16-bit value only
			// the 16-low order bits are examined in the code
            keycode = (int)ke.VirtualKeyCode;
            i = 0;               // counter for keystroke bits
			j = 18;              // initial offset into string
			keymask = 0x8000;    // bitmask

            // 16-bit loop
			for (i = 0; i < 16; i++) 
			{
				// Test for separators and skip
				if(i == 8) 
				{
					BuffStr1[j] = (char)0x20;
					j++;
				}
				
				// Test for 1 and 0 bits and display digits
				if((keycode & keymask) >0)
					BuffStr1[j] = '1';
				else
					BuffStr1[j] = '0';

				keymask = keymask >> 1; 
				j++;
			}

			// Display Backspace, Shift, etc.
            DisplaySpecialsPressed(ke);

			// Force WM_PAINT message
			Invalidate();

            //return IntPtr.Zero;
		}

        /// <summary>
        /// Called when a key is released on the keyboard.
        /// </summary>
        /// <param name="ke"></param>
        public override void OnKeyUp(KeyboardActivityArgs ke)
		{
            // Display the virtual key in binary form
            DisplayBinary(ke);
            

            // Display Backspace, Shift, etc.
            DisplaySpecialsPressed(ke);

			// Force a redraw
			Invalidate();

            //return IntPtr.Zero;
		}

        /// <summary>
        /// Called when a keyboard character is received.  This is 
        /// after the system has translated the raw keycode into an
        /// actual character.
        /// </summary>
        /// <param name="kpe"></param>
        public override void OnKeyPress(KeyboardActivityArgs kpe)
		{
			char aChar = kpe.Character;

			//Console.WriteLine("== OnKeyPress: {0}", (int)aChar);

			// Test for control codes and replace with space
			if (aChar < 30)
				aChar = (char)0x20;



			BuffStr2[17] = aChar;
			
			Invalidate();

            //return IntPtr.Zero;
		}

	}
}
