using System;

namespace NewTOAPIA.UI
{
    public delegate void ResizeByEventHandler(Object sender, int dw, int dh);
    public delegate void ResizeToEventHandler(Object sender, int width, int height);

    public interface IResizeable
    {
        // Frame resizing command
        void ResizeBy(int dw, int dh);
        void ResizeTo(int width, int height);

    }

    public interface IResizeReaction
    {
        void OnResizing(int dw, int dy);
        void OnResizedBy(int delta_width, int delta_height);
        void OnResizedTo(int width, int height);
    }
}