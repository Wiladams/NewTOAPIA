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
    public class HookMouseInput
    {
        public delegate void HookedMouseActivityHandler(MouseActivityArgs mouseEvent);

        public event HookedMouseActivityHandler MouseEvent;

        User32.HOOKPROC MouseHookProcedure;

        IntPtr hMouseHook;


        public HookMouseInput()
        {
            Start();
        }

        ~HookMouseInput()
        {
            Stop();
        }

        #region Hooks
        public void Start()
        {
            // Install Mouse Hook
            MouseHookProcedure = new User32.HOOKPROC(MouseHookProc);
            hMouseHook = User32.SetWindowsHookEx(
                User32.WH_MOUSE_LL,
                MouseHookProcedure,
                Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                0);

            if (IntPtr.Zero == hMouseHook)
            {
                //Common.Log("Error installing mouse hook: " + Marshal.GetLastWin32Error().ToString());
                Stop();
            }
        }

        public void Stop()
        {
            if (IntPtr.Zero != hMouseHook)
            {
                bool retMouse = User32.UnhookWindowsHookEx(hMouseHook) != 0;
                hMouseHook = IntPtr.Zero;
                if (retMouse == false)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    //throw new Win32Exception(errorCode);
                    //Common.Log("Exception uninstalling mouse hook, error code: " + errorCode.ToString());
                }
            }
        }
        #endregion

        #region Hook Callbacks
        private IntPtr MouseHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            IntPtr rv = IntPtr.Zero;
            MSLLHOOKSTRUCT mouseHookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

            try
            {
                if (mouseHookStruct.IsInjected)
                {
                    rv = User32.CallNextHookEx(hMouseHook, nCode, wParam, lParam);
                }
                else
                {
                    if ((nCode >= 0) && (null != MouseEvent))
                    {
                        MouseActivityArgs ma = MouseActivityArgs.CreateFromLowLevelHookProc(wParam.ToInt32(), mouseHookStruct);

                        MouseEvent(ma);

                    }
                    rv = User32.CallNextHookEx(hMouseHook, nCode, wParam, lParam);
                }
            }
            catch (Exception e)
            {
                rv = User32.CallNextHookEx(hMouseHook, nCode, wParam, lParam);
            }

            return rv;
        }
        #endregion
    }
}
