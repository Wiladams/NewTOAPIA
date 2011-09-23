using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.OpenGL
{
    /// <summary>
    /// These are OpenGL related calls, but they are located in the gdi32.dll library.
    /// They are included here so that the TOAPI.GDI32 library does not have to be 
    /// included in an application just to use them.
    /// </summary>
    public partial class gl
    {
        // ChoosePixelFormat
        [DllImport("gdi32.dll", EntryPoint = "ChoosePixelFormat")]
        public static extern int ChoosePixelFormat(IntPtr hdc, [In] ref PIXELFORMATDESCRIPTOR pfd);

        // DescribePixelFormat
        // use this one when you want to find out the maximum index of 
        // pixel formats.  Pass the pfd as IntPtr.Zero
        [DllImport("gdi32.dll", EntryPoint = "DescribePixelFormat")]
        public static extern int DescribePixelFormat(IntPtr hdc, int iPixelFormat, uint nBytes, IntPtr pfd);

        [DllImport("gdi32.dll", EntryPoint = "DescribePixelFormat")]
        public static extern int DescribePixelFormat(IntPtr hdc, int iPixelFormat, uint nBytes, ref PIXELFORMATDESCRIPTOR pfd);
        
        // GetPixelFormat
        [DllImport("gdi32.dll", EntryPoint = "GetPixelFormat")]
        public static extern int GetPixelFormat(IntPtr hdc);

        // SetPixelFormat
        [DllImport("gdi32.dll", EntryPoint = "SetPixelFormat")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetPixelFormat(IntPtr hdc, int format, [In] ref PIXELFORMATDESCRIPTOR pfd);

        // SwapBuffers
        [DllImport("gdi32.dll", EntryPoint = "SwapBuffers")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SwapBuffers(IntPtr hDC);

    }
}
