

namespace HelloNewTOAPIA
{
    using System;

    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;
    using NewTOAPIA.UI;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            // Create the window that we'll show on the screen
            Window win = new Window("Hello NewTOAPIA", 10, 10, 320, 240);
            
            // Explicitly call show as the window is not shown by default
            win.BackgroundColor = Colorrefs.White;
            win.Show();

            // Draw into the window directly
            win.ClientAreaGraphPort.DrawString(100, 100, "Hello NewTOAPIA!");

            // Start the window/application running.  It will continue
            // running until the close box is clicked on.
            win.Run();
        }
    }
}
