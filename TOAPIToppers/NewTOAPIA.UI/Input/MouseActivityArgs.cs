﻿using System;

using TOAPI.User32;
using TOAPI.Types;

namespace NewTOAPIA.UI
{
    public delegate void MouseActivityEventHandler(Object sender, MouseActivityArgs me);

    [Flags]
    public enum RawMouseEventType
    {
        AttributesChanged = User32.MOUSE_ATTRIBUTES_CHANGED,
        MoveRelative = User32.MOUSE_MOVE_RELATIVE,
        MoveAbsolute = User32.MOUSE_MOVE_ABSOLUTE,
        VirtualDesktop = User32.MOUSE_VIRTUAL_DESKTOP,
    }

    public enum MouseCoordinateSpace
    {
        Window,
        Desktop,
        Screen
    }

    public enum MouseMovementType
    {
        Relative,
        Absolute,
    }

    public enum MouseActivityType
    {
        MouseDown,
        MouseUp,
        MouseMove,

        MouseEnter,
        MouseLeave,
        MouseHover,

        MouseWheel,

        None,       // No activity
    }

    [Flags]
    public enum MouseButtonActivity
    {
        None                =  0,
        LeftButtonDown      = User32.RI_MOUSE_LEFT_BUTTON_DOWN,      // Left Button changed to down.
        LeftButtonUp        = User32.RI_MOUSE_LEFT_BUTTON_UP,          // Left Button changed to up.
        RightButtonDown     = User32.RI_MOUSE_RIGHT_BUTTON_DOWN,    // Right Button changed to down.
        RightButtonUp       = User32.RI_MOUSE_RIGHT_BUTTON_UP,        // Right Button changed to up.
        MiddleButtonDown    = User32.RI_MOUSE_MIDDLE_BUTTON_DOWN,  // Middle Button changed to down.
        MiddleButtonUp      = User32.RI_MOUSE_MIDDLE_BUTTON_UP,      // Middle Button changed to up.

        Button4Down         = User32.RI_MOUSE_BUTTON_4_DOWN,
        Button4Up           = User32.RI_MOUSE_BUTTON_4_UP,
        Button5Down         = User32.RI_MOUSE_BUTTON_5_DOWN,
        Button5Up           = User32.RI_MOUSE_BUTTON_5_UP,

        MouseWheel          = User32.RI_MOUSE_WHEEL,
    }

    public class MouseActivityArgs : EventArgs
    {
        public IntPtr WindowHandle { get; private set; }
        public MouseDevice Device { get; private set; }
        public MouseActivityType ActivityType { get; private set; }
        public MouseButtonActivity ButtonActivity { get; private set; }
        public MouseCoordinateSpace CoordinateSpace { get; private set; }
        public MouseMovementType MovementType { get; private set; }

        public int Clicks { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public short Delta { get; private set; }
        public int KeyFlags { get; private set; }

        #region Constructor
        public MouseActivityArgs(MouseDevice device, MouseActivityType activityType, MouseButtonActivity buttonActivity, 
            MouseCoordinateSpace space, MouseMovementType mmType, 
            IntPtr windowHandle,
            int x, int y, short delta, int clicks, int keyflags)
        {
            Device = device;
            ActivityType = activityType;
            ButtonActivity = buttonActivity;
            CoordinateSpace = space;
            MovementType = mmType;
            WindowHandle = windowHandle;

            Clicks = clicks;
            X = x;
            Y = y;
            Delta = delta;
            KeyFlags = keyflags;
        }
        #endregion

        #region Keystate Attributes
        public bool IsControlPressed
        {
            get {return User32.MK_CONTROL == (KeyFlags & User32.MK_CONTROL);}
        }

        public bool IsShiftPressed
        {
            get { return User32.MK_SHIFT == (KeyFlags & User32.MK_SHIFT); }
        }

        public bool IsLeftButtonPressed
        {
            get { return User32.MK_LBUTTON == (KeyFlags & User32.MK_LBUTTON); }
        }

        public bool IsMiddleButtonPressed
        {
            get { return User32.MK_MBUTTON == (KeyFlags & User32.MK_MBUTTON); }
        }
        
        public bool IsRightButtonPressed
        {
            get { return User32.MK_RBUTTON == (KeyFlags & User32.MK_RBUTTON); }
        }

        public bool IsXButton1Pressed
        {
            get { return User32.MK_XBUTTON1 == (KeyFlags & User32.MK_XBUTTON1); }
        }

        public bool IsXButton2Pressed
        {
            get { return User32.MK_XBUTTON2 == (KeyFlags & User32.MK_XBUTTON2); }
        }
        #endregion

        #region Construction Helpers
        public static MouseActivityType ConvertButtonActivityToMouseActivity(MouseButtonActivity mbActivity)
        {
            MouseActivityType maType = MouseActivityType.None;

            switch (mbActivity)
            {
                case MouseButtonActivity.LeftButtonDown:
                case MouseButtonActivity.RightButtonDown:
                case MouseButtonActivity.MiddleButtonDown:
                case MouseButtonActivity.Button4Down:
                case MouseButtonActivity.Button5Down:
                    maType = MouseActivityType.MouseDown;
                    break;

                case MouseButtonActivity.LeftButtonUp:
                case MouseButtonActivity.RightButtonUp:
                case MouseButtonActivity.MiddleButtonUp:

                case MouseButtonActivity.Button4Up:
                case MouseButtonActivity.Button5Up:
                    maType = MouseActivityType.MouseUp;
                    break;

                case MouseButtonActivity.MouseWheel:
                    maType = MouseActivityType.MouseWheel;
                    break;

                default:
                    maType = MouseActivityType.MouseMove;
                    break;
            }

            return maType;
        }

        public static MouseActivityType ConvertMessageToActivityType(int msg)
        {
            MouseActivityType maType = MouseActivityType.None;

            switch (msg)
            {
                case (int)WinMsg.WM_MOUSEHOVER:
                    maType = MouseActivityType.MouseHover;
                    break;

                case (int)WinMsg.WM_MOUSELEAVE:
                    maType = MouseActivityType.MouseLeave;
                    break;

                case (int)WinMsg.WM_MOUSEMOVE:
                    maType = MouseActivityType.MouseMove;
                    break;

                case (int)WinMsg.WM_MOUSEWHEEL:
                    maType = MouseActivityType.MouseWheel;
                    break;

                case (int)WinMsg.WM_LBUTTONUP:
                case (int)WinMsg.WM_MBUTTONUP:
                case (int)WinMsg.WM_RBUTTONUP:
                    maType = MouseActivityType.MouseUp;
                    break;

                // Right button processing
                case (int)WinMsg.WM_LBUTTONDBLCLK:
                case (int)WinMsg.WM_LBUTTONDOWN:
                case (int)WinMsg.WM_MBUTTONDBLCLK:
                case (int)WinMsg.WM_MBUTTONDOWN:
                case (int)WinMsg.WM_RBUTTONDBLCLK:
                case (int)WinMsg.WM_RBUTTONDOWN:
                    maType = MouseActivityType.MouseDown;
                    break;
            }

            return maType;
        }

        public static MouseButtonActivity ConvertMessageToButtonActivityType(int msg, out int clicks)
        {
            MouseButtonActivity mbActivity = MouseButtonActivity.None;
            clicks = 0;

            switch (msg)
            {
                case (int)WinMsg.WM_MOUSEMOVE:
                    mbActivity = MouseButtonActivity.None;
                    break;

                case (int)WinMsg.WM_MOUSEWHEEL:
                    mbActivity = MouseButtonActivity.MouseWheel;
                    break;

                // Left button processing
                case (int)WinMsg.WM_LBUTTONDBLCLK:
                    mbActivity = MouseButtonActivity.LeftButtonDown;
                    clicks = 2;
                    break;

                case (int)WinMsg.WM_LBUTTONDOWN:
                    mbActivity = MouseButtonActivity.LeftButtonDown;
                    clicks = 1;
                    break;

                case (int)WinMsg.WM_LBUTTONUP:
                    mbActivity = MouseButtonActivity.LeftButtonUp;
                    clicks = 1;
                    break;

                // Middle button processing
                case (int)WinMsg.WM_MBUTTONDBLCLK:
                    mbActivity = MouseButtonActivity.MiddleButtonDown;
                    clicks = 2;
                    break;

                case (int)WinMsg.WM_MBUTTONDOWN:
                    mbActivity = MouseButtonActivity.MiddleButtonDown;
                    clicks = 1;
                    break;

                case (int)WinMsg.WM_MBUTTONUP:
                    mbActivity = MouseButtonActivity.MiddleButtonUp;
                    clicks = 1;
                    break;

                // Right button processing
                case (int)WinMsg.WM_RBUTTONDBLCLK:
                    mbActivity = MouseButtonActivity.RightButtonDown;
                    clicks = 2;
                    break;

                case (int)WinMsg.WM_RBUTTONDOWN:
                    mbActivity = MouseButtonActivity.RightButtonDown;
                    clicks = 1;
                    break;

                case (int)WinMsg.WM_RBUTTONUP:
                    mbActivity = MouseButtonActivity.RightButtonUp;
                    clicks = 1;
                    break;
            }

            return mbActivity;
        }

        static int CreateKeyFlagsFromButtonStates(int buttonStates)
        {
            //Console.WriteLine("buttonsStates: {0}", buttonStates);
            int keyFlags = buttonStates;

            return keyFlags;
        }

        public static MouseActivityArgs CreateFromWindowsMessage(IntPtr windowHandle, int msg, IntPtr wParam, IntPtr lParam)
        {
            int clicks = 0;
            short delta = 0;
            short keyMasks = (short)BitUtils.Loword((int)wParam);
            short x = (short)BitUtils.Loword((int)lParam);
            short y = (short)BitUtils.Hiword((int)lParam);

            MouseActivityType maType = ConvertMessageToActivityType(msg);
            MouseButtonActivity mbActivity = ConvertMessageToButtonActivityType(msg, out clicks);

            switch (msg)
            {
                case (int)WinMsg.WM_MOUSEWHEEL:
                    delta = BitUtils.Hiword((int)wParam);       // expressed in multiples of WHEEL_DELTA (120)
                    break;
            }

            MouseActivityArgs args = new MouseActivityArgs(null, maType, mbActivity,
                MouseCoordinateSpace.Window, MouseMovementType.Absolute, windowHandle, x, y, delta, clicks, keyMasks);

            return args;
        }

        // Create a reasonable MouseActivityArgs object that
        // can be dealt with by the rest of the system.
        // Here we turn the raw data into things like button activity
        // as well as mapping the generic information into something
        // applications can deal with.
        public static MouseActivityArgs CreateFromRawInput(MouseDevice device, RAWMOUSE rawMouse)
        {
            int clicks = 0;
            short delta = 0;
            int x = rawMouse.lLastX;
            int y = rawMouse.lLastY;
            IntPtr windowHandle = IntPtr.Zero;
            int keyMasks = 0;

            MouseButtonActivity mbActivity = (MouseButtonActivity)rawMouse.Union1.Struct1.usButtonFlags;
            MouseActivityType maType = ConvertButtonActivityToMouseActivity(mbActivity);
            MouseMovementType mmType = MouseMovementType.Relative;

            if (MouseActivityType.MouseDown == maType)
                clicks = 1;

            // Figure out what type of movement is being reported
            if (User32.MOUSE_MOVE_RELATIVE == (rawMouse.usFlags & User32.MOUSE_MOVE_RELATIVE))
                mmType = MouseMovementType.Relative;
            
            if (User32.MOUSE_MOVE_ABSOLUTE == (rawMouse.usFlags & User32.MOUSE_MOVE_ABSOLUTE))
                mmType = MouseMovementType.Absolute;

            // If there's mouse wheel activity, get the number of detents that are being reported
            if (MouseButtonActivity.MouseWheel == mbActivity)
            {
                delta = (short)rawMouse.Union1.Struct1.usButtonData;
            }

            // Create a keymask to represent the state of the existing pressed keys
            keyMasks = CreateKeyFlagsFromButtonStates((int)rawMouse.ulRawButtons);

            MouseActivityArgs args = new MouseActivityArgs(device, maType, mbActivity, 
                MouseCoordinateSpace.Desktop, mmType, windowHandle, x, y, delta, clicks, keyMasks);

            return args;
        }

        public static MouseActivityArgs CreateFromLowLevelHookProc(int msg, MSLLHOOKSTRUCT eventData)
        {
            int x = eventData.X;
            int y = eventData.Y;
            int clicks = 0;
            short delta = 0;
            IntPtr windowHandle = IntPtr.Zero;
            int keyMasks = eventData.flags;

            MouseActivityType maType = ConvertMessageToActivityType(msg);
            MouseMovementType mmType = MouseMovementType.Absolute;
            MouseButtonActivity mbActivity = ConvertMessageToButtonActivityType(msg, out clicks);

            switch (msg)
            {
                case (int)WinMsg.WM_MOUSEWHEEL:
                    delta = (short)eventData.Delta;
                    break;
            }

            MouseActivityArgs args = new MouseActivityArgs(null, maType, mbActivity,
                MouseCoordinateSpace.Window, mmType, windowHandle, x, y, delta, clicks, keyMasks);

            return args;
        }

        #endregion

        public override string ToString()
        {
            return "<MouseActivity X='" + X.ToString() +
                "' Y='" + Y.ToString() +
                "' Delta = '" + Delta.ToString() +
                "' Clicks = '" + Clicks.ToString() +
                "' Button = '" + ButtonActivity.ToString() +
                "'" +
                " />";

        }
    }
}
