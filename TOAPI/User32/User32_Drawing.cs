using System;
using System.Text;  // For StringBuilder
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.User32
{
    // DrawStateProc
    // Used with - DrawState
    public delegate int DrawStateProc(SafeHandle hdc, IntPtr lData, IntPtr wData, int cx, int cy);

    // OutputProc
    // Used with DrawGrayString
    public delegate int OutputProc(SafeHandle hDC, IntPtr lpData, int cchData);

    partial class User32
    {
        // Dealing with drawing
        // GetSysColorBrush
        [DllImport("user32.dll")]
        public static extern IntPtr GetSysColorBrush(int nIndex);

        // GetSysColor
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetSysColor(int nIndex);


        // BeginPaint
        [DllImport("user32.dll", EntryPoint = "BeginPaint")]
        public static extern IntPtr BeginPaint(IntPtr hWnd, out PAINTSTRUCT lpPaint);

        // EndPaint
        [DllImport("user32.dll", EntryPoint = "EndPaint")]
        public static extern int EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);


        // DrawState
        [DllImport("user32.dll")]
        public static extern int DrawState(SafeHandle hdc, 
            IntPtr hBrush, 
            DrawStateProc qfnCallBack, 
            IntPtr lData, 
            IntPtr wData, 
            int x, int y, int width, int height, 
            uint uFlags);


        // ExcludeUpdateRgn
        [DllImport("user32.dll")]
        public static extern int ExcludeUpdateRgn(IntPtr hDC, IntPtr hWnd);



        // Device Context
        // GetDCEx
        [DllImport("user32.dll", EntryPoint = "GetDCEx", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        // GetDC
        [DllImport("user32.dll", EntryPoint = "GetDC", CharSet = CharSet.Auto)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        //        384  17F 00010083 GetWindowDC
        [DllImport("user32.dll", EntryPoint = "GetWindowDC", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        // ReleaseDC
        [DllImport("user32.dll", EntryPoint = "ReleaseDC", CharSet = CharSet.Auto)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        

        // GetUpdateRect
        [DllImport("user32.dll", EntryPoint = "GetUpdateRect")]
        public static extern int GetUpdateRect(IntPtr hWnd, IntPtr lpRect, [MarshalAs(UnmanagedType.Bool)] bool bErase);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetUpdateRect(IntPtr hWnd, ref RECT lpRect, [MarshalAs(UnmanagedType.Bool)]bool bErase);


        // GetUpdateRgn
        [DllImport("user32.dll", EntryPoint = "GetUpdateRgn")]
        public static extern int GetUpdateRgn([In] IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bErase);

        // InvalidateRect
        [DllImport("user32.dll")]
        public static extern int InvalidateRect(IntPtr hWnd, ref RECT rect, bool erase);

        // InvalidateRect
        [DllImport("user32.dll")]
        public static extern int InvalidateRect(IntPtr hWnd, IntPtr rect, bool erase);

        // ValidateRect
        [DllImport("user32.dll")]
        public static extern int ValidateRect(IntPtr hWnd, ref RECT rect);

        // InvalidateRgn
        [DllImport("user32.dll", EntryPoint = "InvalidateRgn")]
        public static extern int InvalidateRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bErase);

        // ValidateRgn
        [DllImport("user32.dll", EntryPoint = "ValidateRgn")]
        public static extern int ValidateRgn([In] IntPtr hWnd, [In] IntPtr hRgn);


        // Dealing with rectangles
        // DrawAnimatedRects
        [DllImport("user32.dll", EntryPoint = "DrawAnimatedRects")]
        public static extern int DrawAnimatedRects(IntPtr hwnd, int idAni, ref RECT lprcFrom, ref RECT lprcTo);


        // DrawFocusRect
        [DllImport("user32.dll")]
        public static extern int DrawFocusRect(SafeHandle hDC, [In] ref RECT lprc);
        
        // FillRect
        [DllImport("user32.dll")]
        public static extern int FillRect(SafeHandle hDC, [In] ref RECT rect, IntPtr hbr);

        // FrameRect
        [DllImport("user32.dll")]
        public static extern int FrameRect(SafeHandle hDC, [In] ref RECT rect, IntPtr hbr);

        // InvertRect
        [DllImport("user32.dll", EntryPoint = "InvertRect")]
        public static extern int InvertRect(SafeHandle hDC, [In] ref RECT lprc);

        // DrawEdge
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawEdge(SafeHandle hDC, ref RECT rect, int edge, int flags);

        // DrawFrameControl
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawFrameControl(SafeHandle hDC, ref RECT rect, int type, int state);

        // DrawIconEx
        [DllImport("user32.dll")]
        public static extern int DrawIconEx(SafeHandle hDC, int x, int y, int hIcon, int width, int height, int iStepIfAniCursor, int hBrushFlickerFree, int diFlags);

        // DrawCaption
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawCaption(IntPtr hwnd, SafeHandle hdc, [In] ref RECT rect, uint uFlags);




        // Drawing Text
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int DrawText(SafeHandle hDC, StringBuilder lpszString, int nCount, ref RECT lpRect, uint nFormat);

        // DrawTextEx
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int DrawTextEx(SafeHandle hdc, StringBuilder lpchText, int cchText, ref RECT lprc, uint dwDTFormat, ref DRAWTEXTPARAMS lpDTParams);

        // GrayString
    }
}
