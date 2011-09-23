using System;
using System.Drawing;
using System.Threading;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI.GL
{
    using NewTOAPIA;

    public class GLController : IObserver<KeyboardActivityArgs>, IObserver<MouseActivityArgs>
    {
        protected GLModel Model { get; set; }
        protected GLView View { get; set; }
        IntPtr fWindowHandle;

        bool fResizeFlag;

        int fClientWidth;
        int fClientHeight;

        // Keyboard and mouse input fields
        KeyboardDevice fKeyboardDevice;
        MouseDevice fMouseDevice;

        public GLController()
        {
        }

        protected GLController(GLModel model, GLView view)
        {
            SetModelAndView(model, view);
        }

        public virtual void SetModelAndView(GLModel model, GLView view)
        {
            this.Model = model;
            this.View = view;
        }

        #region Properties
        public IntPtr WindowHandle
        {
            get { return fWindowHandle; }
            set { fWindowHandle = value; }
        }

        public System.Drawing.Rectangle GetClientRectangle()
        {
            if (WindowHandle == IntPtr.Zero)
                return System.Drawing.Rectangle.Empty;

            // Initially, the viewport covers the entirety of the client area
            RECT rect;
            User32.GetClientRect(WindowHandle, out rect);

            return new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

        }

        #endregion
        
        // Windows message handling routines
        // WM_CLOSE
        public virtual bool OnCloseRequested()
        {
            // close OpenGL Rendering context
            View.CloseContext(WindowHandle);

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
            if(!View.CreateContext(WindowHandle, 32, 24, 8))
            {
                //Win::log(L"[ERROR] Failed to create OpenGL rendering context from ControllerGL::create().");
                return -1;
            }

            SetupRunning();
            
            return 0;
        }

        protected virtual void SetupRunning()
        {
            View.GLContext.MakeCurrentContext();

            // Tell the model it now has an active GLContext
            // From this point on, the model is free to do any 
            // GL calls it needs to do.
            Model.SetContext(View.GLContext);

            // Initially, the viewport covers the entirety of the client area
            RECT rect;
            User32.GetClientRect(WindowHandle, out rect);
            Model.OnSetViewport(rect.Size.Width, rect.Size.Height);

            // Setup the keyboard input
            SetupInput();
        }

        public virtual void RenderFrame()
        {
            if (fResizeFlag)
            {
                Model.OnSetViewport(fClientWidth, fClientHeight);
                fResizeFlag = false;
            }

            // Let the model do its drawing
            Model.Draw();

            // Tell the view there is something to display
            View.SwapBuffers();
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
            //if (null != fKeyboardDevice)
            //{
            //    fKeyboardDevice.KeyboardActivityEvent += new KeyboardActivityEventHandler(this.OnKeyboardActivity);
            //    fKeyboardDevice.Run();

            //}
        }

        public virtual void SetupMouseInput()
        {
            fMouseDevice = MouseDevice.GetFirstPhysicalMouse();
            //if (null != fMouseDevice)
            //{
            //    fMouseDevice.MouseActivityEvent += new MouseActivityEventHandler(this.OnMouseActivity);
            //    fMouseDevice.Start();
            //}
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
            // Post a quit message to get the message
            // processing loop to stop
            User32.PostQuitMessage(0);
            
            Model.ReleaseContext();
            ShutdownInput();

            // terminate rendering thread
            View.GLContext.Disconnect();   // unset RC

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

        #region IObserver
        public virtual void OnCompleted()
        {
        }

        public virtual void OnError(Exception excep)
        {
        }

        public virtual void OnNext(KeyboardActivityArgs kbde)
        {
            Model.OnKeyboardActivity(this, kbde);
        }

        public virtual void OnNext(MouseActivityArgs me)
        {
            Model.OnMouseActivity(this, me);
        }
        #endregion

        public virtual int notify(int id, IntPtr lParam) 
        { 
            return 0; 
        }

        // WM_PAINT
        public virtual int OnPaint() 
        {
            //RenderFrame();
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
