using System;

using TOAPI.User32;
using TOAPI.Types;
using NewTOAPIA.UI;

using NewTOAPIA.Media;

namespace GDIVideo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            Form1 form = new Form1();
            User32Application app = new User32Application();

            app.Run(form);

        }
    }
}
