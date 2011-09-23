using System;

namespace NewTOAPIA.UI
{
    public delegate void MoveToEventHandler(Object sender, int x, int y);
    public delegate void MoveByEventHandler(Object sender, int dx, int dy);

    public interface IMoveable
    {
        void MoveTo(int x, int y);
        void MoveBy(int dx, int dy);
    }

    public interface IMoveableReaction
    {
        void OnMoving(int x, int y);
        void OnMovedBy(int dx, int dy);
        void OnMovedTo(int x, int y);
    }
}
