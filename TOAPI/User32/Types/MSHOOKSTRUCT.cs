using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.User32
{
        [StructLayout(LayoutKind.Sequential)]
        public class MOUSEHOOKSTRUCT
        {
            public POINT pt;
            public IntPtr hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        //http://msdn.microsoft.com/en-us/library/ms644970(VS.85).aspx
        [StructLayout(LayoutKind.Sequential)]
        public class MSLLHOOKSTRUCT
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;

            #region Properties
            public int Delta
            {
                get { return ((mouseData >> 16) & 0xffff); }
            }

            public int X
            {
                get { return pt.X; }
            }

            public int Y
            {
                get { return pt.Y; }
            }

            public bool IsInjected
            {
                get { return 1 == flags; }  // LLMHF_INJECTED
            }
            #endregion
        }

}
