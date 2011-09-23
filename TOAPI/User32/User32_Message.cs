using System;
using System.Text;  // For StringBuilder
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.User32
{
    partial class User32
    {
        public delegate void CallbackFunction();

        // PostMessage
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hwnd, int msg, int wparam, int lparam);

        // PostQuitMessage
        [DllImport("user32.dll")]
        public static extern void PostQuitMessage(int nExitCode);

        // PostThreadMessage
        [DllImport("user32.dll")]
        public static extern int PostThreadMessage(uint idThread, uint Msg, IntPtr wParam, IntPtr lParam);

        // SendMessage
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([In] IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);


        // TranslateMessage
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int TranslateMessage(ref MSG msg);

        // DispatchMessage
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr DispatchMessage(ref MSG msg);

        // GetMessage
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetMessage(out MSG msg, IntPtr hWnd, uint uMsgFilterMin, uint uMsgFilterMax);

        // GetMessageExtraInfo
        [DllImport("user32.dll", SetLastError = true, EntryPoint = "GetMessageExtraInfo")]
        public static extern IntPtr GetMessageExtraInfo();

        // PeekMessage
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int PeekMessage(out MSG msg, IntPtr hwnd, uint msgMin, uint msgMax, uint remove);

        // WaitMessage
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int WaitMessage();



        /// <summary>
        /// User32 Calls
        /// </summary>
        // CallWindowProc
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CallWindowProc(IntPtr wndProc, IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet=CharSet.Auto)]
        public static extern IntPtr DefWindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    }
}
