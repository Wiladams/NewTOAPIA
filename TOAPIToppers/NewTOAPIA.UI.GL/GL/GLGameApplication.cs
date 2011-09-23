using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NewTOAPIA.UI.GL
{
    public class GLGameApplication : GLApplication<GLGameController>
    {
        public GLGameApplication(string title, Rectangle windowFrame, bool doubleBuffer)
            : base(title, windowFrame, doubleBuffer)
        {
        }

        //public override int Run(GLModel model)
        //{
        //    Model = model;
            
        //    // create "controller" component by specifying what are "model" and "view"
        //    Controller = new GLGameController();
        //    Controller.SetModelAndView(Model, fView);

        //    // create window with given controller
        //    Window = new GLWindow(fView.Title, fWindowFrame, Controller);
        //    Window.Show();

        //    return RunLoop();
        //}
    }
}
