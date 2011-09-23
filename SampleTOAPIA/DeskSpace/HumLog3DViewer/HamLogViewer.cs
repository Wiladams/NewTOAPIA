using System;

using HumLog;
using TOAPI.Types;

namespace HumLogViewer
{
    static class HamLogViewer
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PassiveSpace aWindow = new PassiveSpace("HumLog Viewer",new RECT(10, 10, 1024, 768));

            aWindow.Show();
            aWindow.Run();
        }
    }
}
