

namespace Harness
{
    using System;
    using System.Drawing;

    using TOAPI.Types;

    using NewTOAPIA.GL;
    using NewTOAPIA.UI;

    /// <summary>
    /// The general mechanism for running an application using Open GL is to follow a Model/View/Controller 
    /// mechanism.  The "model" is the GL scene, so that's where all the interesting GL code resides.
    /// The "View" is represented by the GLView object.  The GLView knows about the window, and has
    /// the responsibility of creating the GLContext object.
    /// The "controller" is the GLController object.  It is responsible for communicating the mouse 
    /// and keyboard activity to the model, and telling the view to swap buffers and whatnot.
    /// The separation allows the GLController to run on a separate thread from the general 
    /// windows message processing thread.  That means that all GL calls are on the same thread, and not
    /// dependent on Message processing from Windows at all.
    /// </summary>
    static class Program
	{
        [STAThread]
        static void Main()
        {

            SimpleInputModel model = new SimpleInputModel();

            GLApplication<DemoController> app = new GLApplication<DemoController>("Red Book", 
                new Rectangle(10,10,640,480), true);
            app.Run(model);
        }
	}
}