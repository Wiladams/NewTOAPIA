using System;
using System.Drawing;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI.GL
{
    public class GLApplication<T>
        where T : GLController, new()
    {
        protected GLView fView;
        protected T Controller;
        protected GLModel Model;
        protected GLWindow Window;
        protected Rectangle fWindowFrame;
        
        public GLApplication(string title)
            :this(title, new Rectangle(10,10,800,600))
        {
        }

        public GLApplication(string title, Rectangle windowFrame)
            :this(title, windowFrame, true)
        {
        }

        public GLApplication(string title, Rectangle windowFrame, bool doubleBuffer)
        {
            fWindowFrame = windowFrame;
            fView = new GLView(title, doubleBuffer);
        }

        public int Run(GLModel aModel)
        {
            Model = aModel;
            // create "controller" component by specifying what are "model" and "view"
            Controller = new T();
            Controller.SetModelAndView(aModel, fView);

            // create window with given controller
            Window = new GLWindow(fView.Title, fWindowFrame, Controller);
            Window.Show();

            return RunLoop();
        }

        protected int RunLoop()
        {
            MSG msg = new MSG();

            // loop until WM_QUIT(0) received
            int retValue;
            while ((retValue = User32.GetMessage(out msg, IntPtr.Zero, 0, 0)) != 0)
            {
                if (retValue == -1)
                    throw new Exception("GetMessage returned -1");

                // now, handle window messages
                //if(!User32.TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
                {
                    User32.TranslateMessage(ref msg);
                    User32.DispatchMessage(ref msg);
                }
            }

            return (int)msg.wParam;                 // return nExitCode of PostQuitMessage()
        }
    }
}