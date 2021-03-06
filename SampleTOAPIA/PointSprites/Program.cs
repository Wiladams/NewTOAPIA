﻿using System;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.GL;


//namespace PointSprites
//{
//    static class Program
//    {
//        [STAThread]
//        static void Main()
//        {
//            GLApplication glApp = new GLApplication();
//            //GLApplication glApp = new GLApplication(new GLGameLoop());

//            SpriteScene scene = new SpriteScene();

//            glApp.Run(scene);
//        }
//    }
//}



namespace PointSprites
{
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
            // instantiate model and view components, so "controller" component can reference them
            SpriteScene model = new SpriteScene();

            GLView view = new GLView();

            // create "controller" component by specifying what are "model" and "view"
            GLController glCtrl = new GLController(model, view);

            // create window with given controller
            GLWindow glWin = new GLWindow("Point Sprites", new RECT(10, 10, 400, 300), glCtrl);
            //glWin.setWindowStyle(WS_OVERLAPPEDWINDOW | WS_VISIBLE | WS_CLIPSIBLINGS | WS_CLIPCHILDREN);
            glWin.ClassStyle = User32.CS_HREDRAW | User32.CS_VREDRAW | User32.CS_DBLCLKS | User32.CS_GLOBALCLASS | User32.CS_OWNDC;
            //glWin.setWidth(400);
            //glWin.setHeight(300);

            glWin.CreateWindow();
            glWin.Show();

            // main message loop
            int exitCode;
            exitCode = mainMessageLoop(IntPtr.Zero);
        }

        /// <summary>
        /// This is the Windows message processing loop.  The application will continue to run
        /// as long as this loop does not receive a a WM_QUIT message.  The WM_QUIT message is
        /// typically generated by closing the associated GLWindow, but it is really application
        /// dependent.
        /// </summary>
        /// <param name="hAccelTable"></param>
        /// <returns></returns>
        public static int mainMessageLoop(IntPtr hAccelTable)
        {
            // Create a little MSG object that's going
            // to be used time and again through the message loop
            MSG msg = new MSG();

            // loop until WM_QUIT(0) received
            while (User32.GetMessage(out msg, IntPtr.Zero, 0, 0))
            {
                // If we want to translate accelerator keys, we can use the following.
                //if(!User32.TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
                {
                    // Do a standard translate/dispatch message processing.
                    User32.TranslateMessage(ref msg);
                    User32.DispatchMessage(ref msg);
                }
            }

            return (int)msg.wParam;                 // return nExitCode of PostQuitMessage()
        }

	}
}