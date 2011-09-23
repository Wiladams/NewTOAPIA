using System;
using NewTOAPIA.UI;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.GL;

namespace SnapNGLView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // instantiate model and view components, so "controller" component can reference them
            GLViewerModel model = new GLViewerModel();

            GLApplication app = new GLApplication(new System.Drawing.Rectangle(10, 10, 640, 480));
            app.Run(model);
        }

    }
}