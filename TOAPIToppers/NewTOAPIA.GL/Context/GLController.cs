using System;
using System.Drawing;
using System.Threading;

using NewTOAPIA.UI;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.GL
{
    public class GLController
    {
        GLModel fModel;
        GLView fView;
        IntPtr fWindowHandle;

        bool fLoopFlag;
        bool fResizeFlag;

        int fClientWidth;
        int fClientHeight;

        // Keyboard and mouse input fields
        KeyboardDevice fKeyboardDevice;
        MouseDevice fMouseDevice;

        // For threading
        Thread fThread;

        public GLController(GLModel model, GLView view)
        {
            fModel = model;
            fView = view;
        }

        #region Properties
        public IntPtr WindowHandle
        {
            get { return fWindowHandle; }
            set { fWindowHandle = value; }
        }

        #endregion
        // Windows message handling routines
        // WM_CLOSE
        public virtual bool OnCloseRequested()
        {
            // Setting this flag will cause the rendering thread to drop out of 
            // it's processing loop eventually.
            fLoopFlag = false;

            // wait for rendering thread is terminated
            while (fThread.IsAlive)
                Thread.Sleep(10);

            // close OpenGL Rendering context
            fView.CloseContext(WindowHandle);

            User32.DestroyWindow(WindowHandle);

            return true;
        }

        // WM_COMMMAND
        public virtual void OnControlCommand(IntPtr controlParam)
        {
        }

        public virtual void OnMenuItemSelected(int commandParam)
        {
        }


        public virtual int OnContextMenu(IntPtr handle, int x, int y)
        {
            return 0;
        }

        // WM_CREATE
        public virtual int OnCreate()
        {
            // create a OpenGL rendering context
            if(!fView.CreateContext(WindowHandle, 32, 24, 8))
            {
                //Win::log(L"[ERROR] Failed to create OpenGL rendering context from ControllerGL::create().");
                return -1;
            }

            // create a thread for OpenGL rendering
            fThread = new Thread(ThreadFunction);
            fThread.Start();
            //if(fThread != null)
            //{
                fLoopFlag = true;
            //}
            //else
            //{
            //}

            return 0;
        }

        private void ThreadFunction()
        {
            fView.GLContext.MakeCurrentContext();

            // Tell the model it now has an active GLContext
            // From this point on, the model is free to do any 
            // GL calls it needs to do.
            fModel.SetContext(fView.GLContext);

            // Initially, the viewport covers the entirety of the client area
            RECT rect;
            User32.GetClientRect(WindowHandle, out rect);
            fModel.OnSetViewport(rect.Size.Width, rect.Size.Height);

            // Setup the keyboard input
            SetupInput();

            // rendering loop
            while (fLoopFlag)
            {
                Thread.Sleep(10);                   // yield to other processes or threads

                if (fResizeFlag)
                {
                    fModel.OnSetViewport(fClientWidth, fClientHeight);
                    fResizeFlag = false;
                }

                // Let the model do its drawing
                fModel.Draw();

                // Tell the view there is something to display
                fView.SwapBuffers();
            }

            fModel.ReleaseContext();
            ShutdownInput();

            // terminate rendering thread
            fView.GLContext.Disconnect();   // unset RC

        }

        public virtual void SetupInput()
        {
            //SetupKeyboardInput();
            //SetupMouseInput();
        }

        public virtual void ShutdownInput()
        {
            //ShutdownKeyboardInput();
            //ShutdownMouseInput();
        }

        public virtual void SetupKeyboardInput()
        {
            fKeyboardDevice = KeyboardDevice.GetFirstPhysicalKeyboard();
            if (null != fKeyboardDevice)
            {
                fKeyboardDevice.KeyboardActivityEvent += new KeyboardActivityEventHandler(this.OnKeyboardActivity);
                fKeyboardDevice.Run();

            }
        }

        public virtual void SetupMouseInput()
        {
            fMouseDevice = MouseDevice.GetFirstPhysicalMouse();
            if (null != fMouseDevice)
            {
                fMouseDevice.MouseActivityEvent += new MouseActivityEventHandler(this.OnMouseActivity);
                fMouseDevice.Start();
            }
        }

        public virtual void ShutdownKeyboardInput()
        {
            // Turn off keyboard processing
            if (null != fKeyboardDevice)
                fKeyboardDevice.Quit();
        }

        public virtual void ShutdownMouseInput()
        {
            if (null != fMouseDevice)
                fMouseDevice.Stop();
        }

        // WM_DESTROY
        public virtual int OnDestroy()
        {
            User32.PostQuitMessage(0);
            return 0;
        }

        // WM_ENABLE
        public virtual int OnEnable(bool flag)
        {
            return 0;
        }

        public virtual int OnEraseBkgnd(IntPtr hdc)
        {
            return 0;
        }

        public virtual int hScroll(IntPtr wParam, IntPtr lParam) { return 0; }
        public virtual int vScroll(IntPtr wParam, IntPtr lParam) { return 0; }

        public virtual IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            return fModel.OnKeyboardActivity(sender, kbde);
        }

        public virtual void OnMouseActivity(object sender, MouseActivityArgs me)
        {
            fModel.OnMouseActivity(sender, me);
        }

        public virtual int notify(int id, IntPtr lParam) { return 0; }

        // WM_PAINT
        public virtual int OnPaint() 
        { 
            return 0; 
        }

        public virtual int OnResizedTo(int width, int height) 
        {
            fResizeFlag = true;
            fClientWidth = width;
            fClientHeight = height;
            
            return 0; 
        }

        public virtual int OnTimer(IntPtr id, IntPtr lParam) 
        { 
            return 0; 
        }

    }
}
