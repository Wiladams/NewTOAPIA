using System;

using NewTOAPIA;

namespace KeyboardTest
{
    static class Program
    {
        private const int
            QUIT = 0x0012,
            SIZE = 0x5;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            KeyboardTestWindow win = new KeyboardTestWindow();
            win.Show();

            win.PlatformWindow.Run();
        }


    }

}
