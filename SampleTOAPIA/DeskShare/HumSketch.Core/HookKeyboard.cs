using System;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI
{
    public class HookKeyboardInput
    {

        #region Structs and Variables
        [StructLayout(LayoutKind.Sequential)]
        private class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        //http://msdn.microsoft.com/en-us/library/ms644970(VS.85).aspx
        [StructLayout(LayoutKind.Sequential)]
        private class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        public event KeyboardActivityEventHandler KeybdEvent;

        private IntPtr hKeyboardHook = IntPtr.Zero;
        private static User32.HOOKPROC KeyboardHookProcedure;

        //private static KEYBDDATA hookCallbackKeybdData = new KEYBDDATA();

        private static bool realData = true;
        private long lastSwitchKey1;
        private long lastSwitchKey2;
        private bool lastSwitchKey = true;
        private bool winDown, ctrlDown, altDown;

        public static bool RealData
        {
            get { return HookKeyboardInput.realData; }
            set { HookKeyboardInput.realData = value; }
        }

        #endregion


        public HookKeyboardInput()
        {
            Start();
        }

        ~HookKeyboardInput()
        {
            Stop();
        }

        #region Hooks
        public void Start()
        {
            // Install Keyboard Hook
            KeyboardHookProcedure = new User32.HOOKPROC(KeyboardHookProc);
            hKeyboardHook = User32.SetWindowsHookEx(
                User32.WH_KEYBOARD_LL,
                KeyboardHookProcedure,
                Marshal.GetHINSTANCE(
                Assembly.GetExecutingAssembly().GetModules()[0]),
                0);
            if (IntPtr.Zero == hKeyboardHook)
            {
                //Common.Log("Error installing keyboard hook: " + Marshal.GetLastWin32Error().ToString());
                Stop();
            }
        }

        public void Stop()
        {

            if (IntPtr.Zero != hKeyboardHook)
            {
                bool retKeyboard = User32.UnhookWindowsHookEx(hKeyboardHook) != 0;
                hKeyboardHook = IntPtr.Zero;
                if (retKeyboard == false)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    //throw new Win32Exception(errorCode);
                    //Common.Log("Exception uninstalling keyboard hook, error code: " + errorCode.ToString());
                }
            }
        }
        #endregion

        #region Hook Callbacks

        private IntPtr KeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            //Common.InputEventCount++;
            if (!realData)
            {
                return User32.CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            }

            if ((nCode >= 0) && (KeybdEvent != null))
            {
                KeyboardHookStruct keydbHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
                KeyboardActivityArgs kbda = KeyboardActivityArgs.CreateFromKeyboardHookProc(wParam, lParam);
                KeybdEvent(this, kbda);

                //ProcessKeyEx(keydbHookStruct.vkCode, wParam.ToInt32());
            }

            //if (Common.DesMachineIP == IP.NONE || Common.DesMachineIP == IP.ALL || Common.DesMachineIP == Common.MachineIP)
            //{
                return User32.CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
            //}
            //else
            {
                return new IntPtr(1) ;
            }
        }

        //private void ProcessKeyEx(int vkCode, int flags)
        //{
        //    if (flags == MSG.WM_KEYUP)
        //    {
        //        switch (vkCode)
        //        {
        //            case User32.VK_LWIN:
        //            case User32.VK_RWIN:
        //                winDown = false;
        //                break;

        //            case User32.VK_LCONTROL:
        //                ctrlDown = false;
        //                if ((Common.GetTick() - lastSwitchKey1 < 500) && (Common.GetTick() - lastSwitchKey2 < 500) &&
        //                    (Common.GetTick() - Common.IJustGotAKey) > 1000)
        //                {
        //                    Common.SwitchToMultipleMode(Common.DesMachineIP != IP.ALL);
        //                    Common.MainForm.UpdateMultipleModeIconAndMenu();
        //                    lastSwitchKey = true;
        //                    lastSwitchKey1 = lastSwitchKey2 = 0;
        //                    break;
        //                }
        //                if (lastSwitchKey)
        //                    lastSwitchKey1 = Common.GetTick();
        //                else
        //                    lastSwitchKey2 = Common.GetTick();
        //                lastSwitchKey = !lastSwitchKey;
        //                break;

        //            case User32.VK_RCONTROL:
        //                ctrlDown = false;
        //                break;

        //            case User32.VK_LMENU:
        //            case User32.VK_RMENU:
        //                altDown = false;
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        switch (vkCode)
        //        {
        //            case User32.VK_LWIN:
        //            case User32.VK_RWIN:
        //                winDown = true;
        //                break;

        //            case User32.VK_LCONTROL:
        //            case User32.VK_RCONTROL:
        //                ctrlDown = true;
        //                break;

        //            case User32.VK_LMENU:
        //            case User32.VK_RMENU:
        //                altDown = true;
        //                break;

        //            case User32.VK_DELETE:
        //                if (ctrlDown && altDown)
        //                {
        //                    if (Common.DesMachineIP != IP.ALL) 
        //                        Common.SwitchToMachine(Common.MachineName.Trim());
        //                }
        //                break;

        //            case 'L':
        //                if (winDown)
        //                {
        //                    if (Common.DesMachineIP != IP.ALL) 
        //                        Common.SwitchToMachine(Common.MachineName.Trim());
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}

        internal bool CtrlDown
        {
            get { return ctrlDown; }
            set { ctrlDown = value; }
        }

        #endregion
    }
}
