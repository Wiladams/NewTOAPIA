using System;

namespace TextTour
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Form1 form = new Form1();
            form.Show();
            form.PlatformWindow.Run();
        }
    }
}
