using System;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.User32;
using TOAPI.Types;

using NewTOAPIA.UI;

namespace SnapNShare
{
    public class InputSimulator
    {
        private static bool WinDown, CtrlDown, AltDown;
        public static int KeyEvents;
        public static int MouseEvents;

        static bool Is64BitOS()
        {
            return  8 == IntPtr.Size;
        }

        private static uint SimulateInput(INPUT input)
        {
            uint rv = 0;

            if (Is64BitOS())
            {
                INPUT64 input64 = new INPUT64();
                input64.type = input.type;
                if (input.type == 1) // Keyboard
                {
                    input64.ki.wVk = input.ki.wVk;
                    input64.ki.wScan = input.ki.wScan;
                    input64.ki.dwFlags = input.ki.dwFlags;
                    input64.ki.time = input.ki.time;
                    input64.ki.dwExtraInfo = input.ki.dwExtraInfo;
                }
                else // Mouse
                {
                    input64.mi.dx = input.mi.dx;
                    input64.mi.dy = input.mi.dy;
                    input64.mi.dwFlags = input.mi.dwFlags;
                    input64.mi.mouseData = input.mi.mouseData;
                    input64.mi.time = input.mi.time;
                    input64.mi.dwExtraInfo = input.mi.dwExtraInfo;
                }
                INPUT64[] inputs = { input64 };
                rv = User32.SendInput64(1, inputs, Marshal.SizeOf(input64));
            }
            else
            {
                INPUT[] inputs = { input };
                rv = User32.SendInput(1, inputs, Marshal.SizeOf(input));
            }

            return rv;
        }

        #region Main Exported Routines
        public static void SimulateKeyboardActivity(VirtualKeyCodes keyCode, KeyActivityType keyEvent)
        {
            KeyEvents++;

            INPUT structInput;
            structInput = new INPUT();
            structInput.type = User32.INPUT_KEYBOARD;
            structInput.ki.wScan = 0;
            structInput.ki.time = 0;
            structInput.ki.wVk = (short)keyCode;

            switch (keyEvent)
            {
                case KeyActivityType.SysKeyDown:
                case KeyActivityType.KeyDown:
                    structInput.ki.dwFlags = 0; // Keydown
                    if ((VirtualKeyCodes.LShiftKey == keyCode)  || (VirtualKeyCodes.RShiftKey == keyCode))
                    {
                        //|| ((Keys)kd.keyCode).ToString().CompareTo("LMenu") == 0
                        structInput.ki.dwFlags |= User32.KEYEVENTF_EXTENDEDKEY;
                    }
                    break;

                case KeyActivityType.SysKeyUp:
                case KeyActivityType.KeyUp:
                    structInput.ki.dwFlags = User32.KEYEVENTF_KEYUP;
                    break;

                default:
                    //structInput.ki.keyEvent = (int)User32.KEYEVENTF_KEYUP;
                    break;
            }

            //InputHook.RealData = false;
            SimulateInput(structInput);

            // Something interesting about the sticky Shift Key:(
            if ((keyEvent == KeyActivityType.KeyUp) &&
                ((VirtualKeyCodes.LShiftKey == keyCode) || (VirtualKeyCodes.RShiftKey == keyCode)))
            {
                User32.keybd_event((byte)User32.VK_SHIFT, 0, (uint)(User32.KEYEVENTF_EXTENDEDKEY | User32.KEYEVENTF_KEYUP), 0);
                //structInput.ki.keyCode = (short)VK.RSHIFT; // Does not work in XP => try keybd_event
                //SimulateInput(structInput);
            }
            //InputHook.RealData = true;

            InputProcessKeyEx(keyCode, keyEvent);

        }

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
            //InputHook.RealData = false;
            rv = SimulateInput(mouse_input);
            //InputHook.RealData = true;
            
            return rv;
        }
        #endregion

        #region Helpers for simulating Higher order functions

        internal static void KeyPress(int vk)
        {
            INPUT input;
            input = new INPUT();
            input.type = User32.INPUT_KEYBOARD;
            input.ki.wScan = 0;
            input.ki.time = 0;
            input.ki.wVk = (short)vk;

            //Common.Log(((Keys)input.ki.keyCode).ToString());
            //InputHook.RealData = false;
            input.ki.dwFlags = 0;   // User32.KEYEVENTF_KEYDOWN;
            SimulateInput(input);
            input.ki.dwFlags = (int)User32.KEYEVENTF_KEYUP;
            SimulateInput(input);
            //InputHook.RealData = true;
        }

        // md.x, md.y is from 0 to 655535

        // x, y is in pixel
        public static uint MoveMouse(int x, int y)
        {
            //InputHook.RealData = false;
            uint rv = (uint)User32.SetCursorPos(x, y);
            //InputHook.RealData = true;
            
            return rv;
        }

        internal static void MouseClick()
        {
            INPUT input_down = new INPUT();
            input_down.type = User32.INPUT_MOUSE;
            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;

            //InputHook.RealData = false;
            input_down.mi.dwFlags = User32.MOUSEEVENTF_LEFTDOWN;
            SimulateInput(input_down);

            INPUT input_up = input_down;
            input_up.mi.dwFlags = User32.MOUSEEVENTF_LEFTUP;
            SimulateInput(input_up);
            //InputHook.RealData = true;
        }

        internal static void MouseUp()
        {
            INPUT input = new INPUT();
            input.type = User32.INPUT_MOUSE;
            input.mi.dx = 0;
            input.mi.dy = 0;
            input.mi.mouseData = 0;

            //InputHook.RealData = false;
            input.mi.dwFlags = User32.MOUSEEVENTF_LEFTUP;
            SimulateInput(input);
            //InputHook.RealData = true;
        }
        #endregion

        private static void InputProcessKeyEx(VirtualKeyCodes vkCode, KeyActivityType eventType)
        {
            if (eventType == KeyActivityType.KeyUp)
            {
                switch (vkCode)
                {
                    case VirtualKeyCodes.LWin:
                    case VirtualKeyCodes.RWin:
                        WinDown = false;
                        break;

                    case VirtualKeyCodes.LControlKey:
                    case VirtualKeyCodes.RControlKey:
                        CtrlDown = false;
                        break;

                    case VirtualKeyCodes.LMenu:
                    case VirtualKeyCodes.RMenu:
                        AltDown = false;
                        break;

                    default:
                        break;
                }
            }
            else
            {
                switch (vkCode)
                {
                    case VirtualKeyCodes.LWin:
                    case VirtualKeyCodes.RWin:
                        WinDown = true;
                        break;

                    case VirtualKeyCodes.LControlKey:
                    case VirtualKeyCodes.RControlKey:
                        CtrlDown = true;
                        break;

                    case VirtualKeyCodes.LMenu:
                    case VirtualKeyCodes.RMenu:
                        AltDown = true;
                        break;

                    case VirtualKeyCodes.Delete:
                        if (CtrlDown && AltDown)
                        {
                            ReleaseAllKeys();
                        }
                        break;

                    case VirtualKeyCodes.L:
                        if (WinDown)
                        {
                            ReleaseAllKeys();
                            User32.LockWorkStation();
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        internal static void ReleaseAllKeys()
        {
            //kd.keyCode = (int)VK.CONTROL; kd.keyEvent = WM_KEYUP; InputSimu.SimulateKeyboardActivity(kd);

            SimulateKeyboardActivity(VirtualKeyCodes.LShiftKey, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.LControlKey, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.LMenu, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.LWin, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.RShiftKey, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.RControlKey, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.RMenu, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.RWin, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.ShiftKey, KeyActivityType.KeyUp);
            SimulateKeyboardActivity(VirtualKeyCodes.Menu, KeyActivityType.KeyUp);
        }

    }
}
