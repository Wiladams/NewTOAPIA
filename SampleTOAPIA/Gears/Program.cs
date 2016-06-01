using System;
using System.Drawing;


using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.UI;
using NewTOAPIA.UI.GL;
using NewTOAPIA.GL;

	static class Program
	{
        //[STAThread]
        static void Main()
        {
            GearModel model = new GearModel();
            GLGameApplication app = new GLGameApplication("Gears", new Rectangle(10, 10, 400, 300), true);
            app.Run(model);
        }

        //static void Main()
        //{
        //    // instantiate model and view components, so "controller" component can reference them
        //    GearModel model = new GearModel();
        //    GLView view = new GLView("Gears");

        //    // create "controller" component by specifying what are "model" and "view"
        //    GLController glCtrl = new GLGameController(model, view);

        //    // create window with given controller
        //    GLWindow glWin = new GLWindow("Gear Head", new Rectangle(10, 10, 400, 300), glCtrl);

        //    glWin.Show();

        //    // main message loop
        //    int exitCode;
        //    exitCode = mainMessageLoop(IntPtr.Zero);
        //}
	
        public static int mainMessageLoop(IntPtr hAccelTable)
        {
            MSG msg=new MSG();

            // loop until WM_QUIT(0) received
            while(User32.GetMessage(out msg, IntPtr.Zero, 0, 0) != 0)  
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