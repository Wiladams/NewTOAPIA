using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
//using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace DistributedDesktop
{
    public class UserIODelegate : IUserIOChannel
    {
        public event ShowCursorHandler ShowCursorEvent;
        public event HideCursorHandler HideCursorEvent;
        public event MoveCursorHandler MoveCursorEvent;
        public event MouseActivityEventHandler MouseActivityEvent;
        public event KeyboardActivityEventHandler KeyboardActivityEvent;

        public virtual void AddDelegate(IUserIOChannel aChannel)
        {
            ShowCursorEvent += aChannel.ShowCursor;
            HideCursorEvent += aChannel.HideCursor;
            MoveCursorEvent += aChannel.MoveCursor;

            MouseActivityEvent += aChannel.MouseActivity;
            KeyboardActivityEvent += aChannel.KeyboardActivity;
        }


        // UI Management
        public virtual void ShowCursor()
        {
            if (null != ShowCursorEvent)
                ShowCursorEvent();
        }

        public virtual void HideCursor()
        {
            if (null != HideCursorEvent)
                HideCursorEvent();
        }

        public virtual void MoveCursor(int x, int y)
        {
            if (null != MoveCursorEvent)
                MoveCursorEvent(x, y);
        }

        public virtual void MouseActivity(Object sender, MouseActivityArgs ma)
        {
            if (null != MouseActivityEvent)
                MouseActivityEvent(this, ma);
        }

        public virtual IntPtr KeyboardActivity(Object sender, KeyboardActivityArgs kbda)
        {
            if (null != KeyboardActivityEvent)
                KeyboardActivityEvent(this, kbda);

            return new IntPtr(1);
        }

    }
}
