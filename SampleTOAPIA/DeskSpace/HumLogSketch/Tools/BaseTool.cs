using System;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;
using HumLog;

namespace HamSketch.Tools
{
    public class BaseTool
    {
        protected UIWindow fUIWindow;
        IGraphPort fGraphPort;

        public virtual void AttachToWindow(UIWindow aWindow)
        {
            fUIWindow = aWindow;

            AttachForKeyboard(fUIWindow);
            AttachForMouse(fUIWindow);
        }

        public IGraphPort GraphPort
        {
            get { return fGraphPort; }
            set { 
                fGraphPort = value;
                OnGraphPortSet();
            }
        }

        public UIWindow UIWindow
        {
            get { return fUIWindow; }
        }

        protected virtual void OnGraphPortSet()
        {
        }

        protected virtual void AttachForKeyboard(UIWindow aWindow)
        {

        }

        protected virtual void AttachForMouse(UIWindow aWindow)
        {
            // Basic mouse events
            aWindow.MouseDownEvent += new MouseDownDelegate(OnMouseDown);
            aWindow.MouseMoveEvent += new MouseMoveDelegate(OnMouseMove);
            aWindow.MouseUpEvent += new MouseUpDelegate(OnMouseUp);

            // Mouse wheel related
            aWindow.MouseWheelEvent += new MouseWheelDelegate(OnMouseWheel);

            // Mouse tracking events
            aWindow.MouseEnterEvent += new MouseEnterDelegate(OnMouseEnter);
            aWindow.MouseLeaveEvent += new MouseLeaveDelegate(OnMouseLeave);
        }



        // Reacting to the mouse
        public virtual void OnMouseDown(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseMove(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseUp(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseEnter(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseHover(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseLeave(MouseActivityArgs e)
        {
        }

        public virtual void OnMouseWheel(MouseActivityArgs e)
        {
        }
    }
}
