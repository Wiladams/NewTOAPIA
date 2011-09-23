using System;
using System.Windows.Forms;

namespace GraphTour
{
    using System.Drawing;

    using NewTOAPIA.GL;
    using NewTOAPIA.UI.GL;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DemoScene model = new DemoScene();

            GLApplication<DemoController> app = new GLApplication<DemoController>("GraphTourGL", new Rectangle(10, 10, 640, 480));
            app.Run(model);
        }
    }
}
