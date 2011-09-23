using System;

using NewTOAPIA.UI;

namespace SnapNProjector
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
            Projector win = new Projector();

            app.Run(win);
        }
    }
}
