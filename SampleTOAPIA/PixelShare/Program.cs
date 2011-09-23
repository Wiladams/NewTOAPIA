using System;

using NewTOAPIA.UI;     // Needed for User32Application object

namespace PixelShare.Core
{
    /// <summary>
    /// The SnapNShare 'server' is an application that simply shares out a portion of 
    /// a device context to 'viewers' who want to look at it.
    /// The STAThread attribute is set so that the clipboard can be used to copy captures.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>You only need to use the [STAThread] attribute
        /// if the application needs to be single threaded from a Windows Forms, or other frameworks.
        /// This is the case if you intend to capture screen shots to the Clipboard and Windows.Forms.Clipboard
        /// will only work in a single threaded environment.
        /// </remarks>
        //[STAThread]
        static void Main()
        {
            // Create an instance of the window
            SnapperWindow win = new SnapperWindow();

            // Use the NewTOAPIA application object just for fun
            User32Application app = new User32Application();

            // This will autmatically show the window, and when the 
            // window is closed, the application will discontinue.
            app.Run(win);
        }
    }
}
