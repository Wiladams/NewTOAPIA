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

namespace DistributedDesktop
{
    public class KeyboardSimulator : InputSimulator
    {
        private static bool WinDown, CtrlDown, AltDown;
        public static int KeyEvents;


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
                    if ((VirtualKeyCodes.LShiftKey == keyCode) || (VirtualKeyCodes.RShiftKey == keyCode))
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
