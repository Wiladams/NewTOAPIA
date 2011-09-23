using System;
using System.Drawing;
using System.Runtime.InteropServices;

using TOAPI.Kernel32;     // GetModuleHandle
using TOAPI.OpenGL;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;

namespace NewTOAPIA.GL
{
    public delegate void WindowResizedEventHandler(Object sender, int w, int h);
    public delegate void WindowRepaintEventHandler(Object sender, DrawEvent args);

    /// <summary>
    /// Class: GLWindow
    /// 
    /// </summary>
    public class GLWindow : Window
    {
        public event MouseActivityEventHandler MouseActivityEvent;
        public event WindowResizedEventHandler WindowResizedEvent;
        public event WindowRepaintEventHandler WindowRepaintEvent;

        protected static string gWindowClassName = "GLWindow";


        GLController fController;

        #region Constructor
        public GLWindow(string title, Rectangle frame, GLController controller)
            :this(title, frame.X, frame.Y, frame.Width, frame.Height, controller)
        {
        }

        public GLWindow(string title, int x, int y, int width, int height, GLController controller)
            : base(title, x, y, width, height)
        {
            fController = controller;
            fController.WindowHandle = Handle;
            fController.OnCreate();
        }
        #endregion

        #region Window Class Properties
        public override User32WindowClass CreateWindowClass()
        {
            CallbackDelegate = new WindowProc(this.Callback);

            User32WindowClass aClass = new User32WindowClass(gWindowClassName,
                (User32.CS_HREDRAW |
                User32.CS_VREDRAW |
                User32.CS_GLOBALCLASS |
                User32.CS_OWNDC),
                CallbackDelegate);

            return aClass;
        }


        public override WindowProfile CreateWindowProfile()
        {
            WindowProfile aProfile = new WindowProfile(WindowStyle.OverlappedWindow,
                    ExtendedWindowStyle.OverLappedWindow);

            return aProfile;
        }
        #endregion

        public virtual void OnIdle()
        {
        }

        public override bool OnCloseRequested()
        {
            bool success = fController.OnCloseRequested();

            return success;		
        }

        public override void OnControlCommand(IntPtr controlParam)
        {
            fController.OnControlCommand(controlParam);
        }


        public override void OnMenuItemSelected(int commandParam)
        {
            fController.OnMenuItemSelected(commandParam);
        }

        public override void OnDestroy()
        {
            fController.OnDestroy();
        }

        public override void OnEnable(bool enabled)
        {
            fController.OnEnable(enabled);
        }

        public override IntPtr OnEraseBackground()
        {
            // We set the result to '1' to indicate that we dealt
            // with the message.
            
            return new IntPtr(1);
        }

        public override void OnPaint(DrawEvent dea)
        {
            fController.OnPaint();
            //result = User32.DefWindowProc(msg.hWnd, msg.message, msg.wParam, msg.lParam);

            if (null != WindowRepaintEvent)
                WindowRepaintEvent(this, dea);
        }


        #region Keyboard Activity
        public override IntPtr OnKeyboardActivity(Object sender, KeyboardActivityArgs ke)
        {
            base.OnKeyboardActivity(sender, ke);
            
            return fController.OnKeyboardActivity(sender, ke);
        }
        #endregion

        #region Mouse Messages
        public override void OnMouseActivity(Object sender, MouseActivityArgs mea)
        {
            if (null != MouseActivityEvent)
                MouseActivityEvent(sender, mea);

            fController.OnMouseActivity(sender, mea);
        }

        MouseButtons GetButtons(uint wParam)
        {
            MouseButtons buttons = MouseButtons.None;
            if ((wParam & User32.MK_LBUTTON) > 0)
                buttons |= MouseButtons.Left;
            if ((wParam & User32.MK_RBUTTON) > 0)
                buttons |= MouseButtons.Right;

            return buttons;
        }
        #endregion Mouse Messages

        #region Timer
        public override void OnTimer()
        {
            //MSG msg = new MSG();
            //msg.hWnd = m.hWnd;
            //msg.lParam = m.lParam;
            //msg.message = m.message;
            //msg.wParam = m.wParam;
            fController.OnTimer(IntPtr.Zero, IntPtr.Zero);
            base.OnTimer();
        }
        #endregion
        
        #region Window Movement and Sizing
        public override void OnResizedTo(int width, int height)
        {
            fController.OnResizedTo(width, height);

            if (null != WindowResizedEvent)
                WindowResizedEvent(this, width, height);
        }
        #endregion

        #region Scene Rendering
        //public virtual void DrawScene()
        //{
        //    BeginScene();
        //    EndScene();
        //}

        //public virtual void BeginScene()
        //{
        //}

        //public virtual void EndScene()
        //{
        //}
        #endregion Scene Rendering

        ~GLWindow()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                //User32.UnregisterClass(gWindowClassName, fAppInstance);
            }
        }
    }
}