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
    public class InputSimulator
    {
        static bool Is64BitOS()
        {
            return  8 == IntPtr.Size;
        }

        public static uint SimulateInput(INPUT input)
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

    }
}
