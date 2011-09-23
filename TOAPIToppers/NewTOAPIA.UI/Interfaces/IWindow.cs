using System;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

    public interface IWindowBase : ITransformable, IShowable, IHandle
    {
        // Some Properties
        RectangleI Frame { get; }
        RectangleI ClientRectangle { get; }

        void Invalidate();
        void Invalidate(RectangleI rect);
        void Validate();

        // And other interesting window stuff
        bool SetWindowAlpha(byte alpha);
    }

    public interface IWindow : IWindowBase, IInteractor, IMoveableReaction, IResizeReaction
    {
        void CaptureMouse();
        void ReleaseMouse();

        // Some drawing related stuff
        IGraphPort ClientAreaGraphPort { get; }
        IGraphPort GraphPort { get; }	// For drawing into window directly

        // Event management
        IntPtr OnEraseBackground();
        void OnPaint(DrawEvent dea);
        void OnQuit();
        bool OnCloseRequested();
        void OnControlCommand(IntPtr controlParam);
        void OnMenuItemSelected(int commandParam);
        void OnSetFocus();
        void OnKillFocus();
        void OnTimer();
    }
}
