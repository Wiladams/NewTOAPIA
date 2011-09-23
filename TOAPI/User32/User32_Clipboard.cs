using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace TOAPI.User32
{
    public partial class User32
    {
        [DllImport("user32.dll", EntryPoint = "SetClipboardViewer")]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        [DllImport("user32.dll", EntryPoint = "ChangeClipboardChain")]
        public static extern int ChangeClipboardChain(IntPtr hWndRemove, [In] IntPtr hWndNewNext);

    }
}
