using System;
using System.Windows.Forms;

using NewTOAPIA.UI;

namespace CameraServer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            MainForm form = new MainForm();
            Application.EnableVisualStyles();
            Application.Run(form);
        }
    }
}
