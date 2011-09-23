using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NewTOAPIA.UI.GL
{
    using TOAPI.User32;
    using TOAPI.Types;

    public class GLGameController : GLController
    {
        // For threading
        Thread fThread;
        bool fContinueLooping;

        public GLGameController()
        {
        }

        public GLGameController(GLModel model, GLView view)
            : base(model, view)
        {
        }

        protected override void SetupRunning()
        {
            // create a thread for OpenGL rendering
            fContinueLooping = true;
            fThread = new Thread(GameLoopFunction);
            fThread.Start();
        }

        public override bool OnCloseRequested()
        {
            // Setting this flag will cause the rendering thread to drop out of 
            // it's processing loop eventually.
            fContinueLooping = false;

            // wait for rendering thread is terminated
            while (fThread.IsAlive)
                Thread.Sleep(10);

            // close OpenGL Rendering context
            View.CloseContext(WindowHandle);

            User32.DestroyWindow(WindowHandle);

            return true;
        }


        private void GameLoopFunction()
        {
            View.GLContext.MakeCurrentContext();

            // Tell the model it now has an active GLContext
            // From this point on, the model is free to do any 
            // GL calls it needs to do.
            Model.SetContext(View.GLContext);

            // Initially, the viewport covers the entirety of the client area
            System.Drawing.Rectangle rect = GetClientRectangle();
            Model.OnSetViewport(rect.Width, rect.Height);

            // Setup the keyboard input
            SetupInput();

            // rendering loop
            while (fContinueLooping)
            {
                Thread.Sleep(10);                   // yield to other processes or threads

                RenderFrame();
            }

            Model.ReleaseContext();
            ShutdownInput();

            // terminate rendering thread
            View.GLContext.Disconnect();   // unset RC
        }
    }
}
