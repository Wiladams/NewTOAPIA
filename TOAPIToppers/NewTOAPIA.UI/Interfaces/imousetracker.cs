using System;

namespace NewTOAPIA.UI
{
    public delegate void MouseEventHandler(Object sender, MouseActivityArgs args);

    // Delegates for various mouse actions
    public delegate void MouseActivityDelegate(MouseActivityArgs e);

    //public delegate void MouseDownDelegate(MouseActivityArgs e);
    //public delegate void MouseUpDelegate(MouseActivityArgs e);
    //public delegate void MouseMoveDelegate(MouseActivityArgs e);
    //public delegate void MouseEnterDelegate(MouseActivityArgs e);
    //public delegate void MouseLeaveDelegate(MouseActivityArgs e);
    //public delegate void MouseWheelDelegate(MouseActivityArgs e);



    public interface IReactToMouseActivity : IObserver<MouseActivityArgs>
    {
    }

    public interface IHandleMouseActivity
    {
        // Reacting to the mouse
        void OnMouseDown(MouseActivityArgs e);
        void OnMouseUp(MouseActivityArgs e);
        void OnMouseMove(MouseActivityArgs e);
        void OnMouseWheel(MouseActivityArgs e);
    }

    public interface IReactToMouseLocation
    {
        void OnMouseEnter(MouseActivityArgs e);
        void OnMouseHover(MouseActivityArgs e);
        void OnMouseLeave(MouseActivityArgs e);
    }
}