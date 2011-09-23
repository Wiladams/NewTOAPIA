using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA.UI;

namespace DistributedDesktop
{
    public delegate void ShowCursorHandler();
    public delegate void HideCursorHandler();
    public delegate void MoveCursorHandler(int x, int y);

    public enum UserIOCommand
    {
        Showcursor = 11000, // UI Management
        HideCursor,
        MoveCursor,
        MouseActivity,
        KeyboardActivity,
    }

    public interface IUserIOChannel
    {
        void ShowCursor();
        void HideCursor();
        void MoveCursor(int x, int y);

        void MouseActivity(Object sender, MouseActivityArgs ma);
        IntPtr KeyboardActivity(Object sender, KeyboardActivityArgs kbda);
    }
}
