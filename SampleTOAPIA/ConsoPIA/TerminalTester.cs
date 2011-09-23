using System;
using System.Text;

using TOAPI.Kernel32;

namespace ConsoPIA
{
    public class TerminalTester
    {
        Terminal fTerminal;

        public TerminalTester()
        {
            fTerminal = null;
        }

        public void Run(Terminal aTerm)
        {
            fTerminal = aTerm;

            // Set console buffer size
            fTerminal.BufferHeight = 25;

            // Do some preliminary decoration
            Decorate();

            // Do some interesting things with the terminal
            COORD pos = new COORD(10,10);

            fTerminal.CursorPosition = pos;
            fTerminal.ForegroundColor = ConsoleTextColor.Red;
            fTerminal.WriteLine("10,10");

            // 
            fTerminal.ForegroundColor = ConsoleTextColor.Yellow;
            fTerminal.WriteLine("Press Return to clear screen...");
            Console.ReadLine();

            // Turn off the cursor
            //fTerminal.CursorVisible = false;
            // Clear the screen
            fTerminal.Clear();

            // Show a bunch of text in different attributes
            ShowTextAttributes();

            Console.ReadLine();
        }

        void Decorate()
        {
            // Change text color to yellow
            fTerminal.ForegroundColor = ConsoleTextColor.Yellow;

            // Get terminal size first
            COORD tSize = fTerminal.Size;

            for (short x = 0; x < tSize.X; x++)
            {
                if (x % 5 == 0)
                {
                    fTerminal.CursorLeft = x;
                    fTerminal.Write("|");
                }
            }

            // Reset cursor x to be left side
            fTerminal.ForegroundColor = ConsoleTextColor.White;

            for (short y = 0; y < tSize.Y; y++)
            {
                fTerminal.CursorLeft = 0;
                if (y % 5 == 0)
                {
                    fTerminal.CursorTop = y;
                    fTerminal.Write("-");
                }
            }

        }

        void ShowTextAttributes()
        {
            ConsoleTextColor[] colors = new ConsoleTextColor[16];
            colors[0] = ConsoleTextColor.Black;
            colors[1] = ConsoleTextColor.DarkGray;
            colors[2] = ConsoleTextColor.DarkRed;
            colors[3] = ConsoleTextColor.DarkGreen;
            colors[4] = ConsoleTextColor.DarkBlue;
            colors[5] = ConsoleTextColor.DarkCyan;
            colors[6] = ConsoleTextColor.DarkMagenta;
            colors[7] = ConsoleTextColor.DarkYellow;
            colors[8] = ConsoleTextColor.Red;
            colors[9] = ConsoleTextColor.Green;
            colors[10] = ConsoleTextColor.Blue;
            colors[11] = ConsoleTextColor.Cyan;
            colors[12] = ConsoleTextColor.Magenta;
            colors[13] = ConsoleTextColor.Yellow;
            colors[14] = ConsoleTextColor.Gray;
            colors[15] = ConsoleTextColor.White;

            for (short y = 0; y < 16; y++)
            {
                for (short x = 0; x < 16; x++)
                {
                    fTerminal.CursorPosition = new COORD((short)(x * 5), y);

                    fTerminal.ForegroundColor = colors[x];
                    fTerminal.BackgroundColor = colors[y];
                    fTerminal.Write(" Text");
                }
            }
        }

    }
}
