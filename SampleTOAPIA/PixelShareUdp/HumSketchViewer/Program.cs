using System;

using NewTOAPIA.UI;

namespace SnapNViewer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            User32Application app = new User32Application();
            Form1 win = new Form1();

            app.Run(win);
        }
    }
}
