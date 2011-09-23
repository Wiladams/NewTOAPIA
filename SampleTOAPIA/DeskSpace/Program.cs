using System;
using TOAPI.Types;

namespace HamSketch
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InstructorSpace aWindow = new InstructorSpace(new RECT(10, 10, 1024, 768));

            aWindow.Show();
            aWindow.PlatformWindow.Run();
        }
    }
}
