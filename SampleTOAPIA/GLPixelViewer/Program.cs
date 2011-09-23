using System;
using System.Drawing;

using NewTOAPIA.GL;
using NewTOAPIA.UI;

using TOAPI.User32;
using TOAPI.Types;


namespace PixelViewer
{
    public class Program
    {

        [STAThread]
        static void Main()
        {
            // instantiate model and view components, so "controller" component can reference them
            PixelDisplayModel model = new PixelDisplayModel();
            GLView view = new GLView("GL Pixel Viewer", true);

            // create "controller" component by specifying what are "model" and "view"
            GLController glCtrl = new GLController(model, view);

            // create window with given controller
            GLWindow glWin = new GLWindow("GL Pixel Viewer", new Rectangle(10, 10, 640, 480), glCtrl);
            //glWin.WindowClass.ClassStyle = User32.CS_HREDRAW | User32.CS_VREDRAW | User32.CS_OWNDC;
            //glWin.CreateWindow();
            glWin.Show();

            // main message loop
            int exitCode;
            exitCode = mainMessageLoop(IntPtr.Zero);
        }

        public static int mainMessageLoop(IntPtr hAccelTable)
        {
            MSG msg = new MSG();

            // loop until WM_QUIT(0) received
            while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)
            {
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