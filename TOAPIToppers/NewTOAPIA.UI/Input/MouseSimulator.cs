using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.User32;
using TOAPI.Types;

using NewTOAPIA.UI;

namespace NewTOAPIA.UI
{
    public class MouseSimulator : InputSimulator
    {
        public static int MouseEvents;

        #region Main Exported Routines

        public static uint SimulateMouseActivity(int x, int y, int delta, MouseButtonActivity activity)
        {
            MouseEvents++;
            uint rv = 0;
            INPUT mouse_input = new INPUT();
            mouse_input.type = User32.INPUT_MOUSE;
            mouse_input.mi.mouseData = delta;
            mouse_input.mi.dx = x;
            mouse_input.mi.dy = y;


            switch (activity)
            {
                case MouseButtonActivity.None:      // No button activity is assumed to be a Move event
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_ABSOLUTE | User32.MOUSEEVENTF_MOVE);
                    break;
                case MouseButtonActivity.LeftButtonDown:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_LEFTDOWN);
                    break;
                case MouseButtonActivity.LeftButtonUp:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_LEFTUP);
                    break;
                case MouseButtonActivity.RightButtonDown:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_RIGHTDOWN);
                    break;
                case MouseButtonActivity.RightButtonUp:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_RIGHTUP);
                    break;
                case MouseButtonActivity.MiddleButtonDown:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_MIDDLEDOWN);
                    break;
                case MouseButtonActivity.MiddleButtonUp:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_MIDDLEUP);
                    break;
                case MouseButtonActivity.MouseWheel:
                    mouse_input.mi.dwFlags |= (int)(User32.MOUSEEVENTF_WHEEL);
                    break;
                default:
                    break;
            }

            rv = SimulateInput(mouse_input);

            return rv;
        }
        #endregion

        #region Helpers for simulating Higher order functions
        public static int MoveMouse(int x, int y)
        {
            int retValue = User32.SetCursorPos(x, y);

            return retValue;
        }

        internal static void MouseClick()
        {
            INPUT input_down = new INPUT();
            input_down.type = User32.INPUT_MOUSE;
            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;

            input_down.mi.dwFlags = User32.MOUSEEVENTF_LEFTDOWN;
            SimulateInput(input_down);

            INPUT input_up = input_down;
            input_up.mi.dwFlags = User32.MOUSEEVENTF_LEFTUP;
            SimulateInput(input_up);
        }

        internal static void MouseUp()
        {
            INPUT input = new INPUT();
            input.type = User32.INPUT_MOUSE;
            input.mi.dx = 0;
            input.mi.dy = 0;
            input.mi.mouseData = 0;

            input.mi.dwFlags = User32.MOUSEEVENTF_LEFTUP;
            SimulateInput(input);
        }
        #endregion
    }
}
