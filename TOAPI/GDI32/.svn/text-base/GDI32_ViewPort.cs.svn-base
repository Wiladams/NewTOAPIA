using System;
using System.Runtime.InteropServices;
//using System.Drawing;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    public partial class GDI32
    {
        // Coordinate Space and Transformation 
        //        147   92 00008EAC DPtoLP
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DPtoLP([In] SafeHandle hdc, [In, Out] POINT[] lpPoints, int nCount);


        [DllImport("gdi32.dll")]
        public static extern int SetMapMode([In] SafeHandle hdc, int fnMapMode);

        // Viewport Extent
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetViewportExtEx([In] SafeHandle hdc, int nXExtent, int nYExtent,
           IntPtr lpSize);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetViewportExtEx([In] SafeHandle hdc, int nXExtent, int nYExtent,
           ref SIZE lpSize);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetViewportExtEx([In] SafeHandle hdc, out SIZE lpSize);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ScaleViewportExtEx(
            [In] SafeHandle hdc,        // handle to device context
            int Xnum,       // horizontal multiplicand
            int Xdenom,     // horizontal divisor
            int Ynum,       // vertical multiplicand
            int Ydenom,     // vertical divisor
            [Out] out SIZE lpSize);   // previous viewport extents

        // Viewport origin
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetViewportOrgEx([In] SafeHandle hdc, int X, int Y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetViewportOrgEx([In] SafeHandle hdc, int X, int Y, ref POINT oldOrg);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetViewportOrgEx([In] SafeHandle hdc, out POINT lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OffsetViewportOrgEx([In] SafeHandle hdc, int nXOffset, int nYOffset,
           IntPtr lpPoint);

        // Window Origin and extent
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowOrgEx([In] SafeHandle hdc, int X, int Y, IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowOrgEx([In] SafeHandle hdc, int X, int Y, ref POINT lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowOrgEx([In] SafeHandle hdc, out POINT lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OffsetWindowOrgEx([In] SafeHandle hdc, int nXOffset, int nYOffset,
           IntPtr lpPoint);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OffsetWindowOrgEx([In] SafeHandle hdc, int nXOffset, int nYOffset,
           ref POINT lpPoint);

        // Setting Window Extent
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowExtEx([In] SafeHandle hdc, int nXExtent, int nYExtent,
           IntPtr lpSize);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowExtEx([In] SafeHandle hdc, int nXExtent, int nYExtent,
           ref SIZE lpSize);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ScaleWindowExtEx(
            [In] SafeHandle hdc,        // handle to device context
            int Xnum,       // horizontal multiplicand
            int Xdenom,     // horizontal divisor
            int Ynum,       // vertical multiplicand
            int Ydenom,     // vertical divisor
            [Out] out SIZE lpSize);   // previous window extents


        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowExtEx([In] SafeHandle hdc, out SIZE lpSize);

        // World Transforms
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWorldTransform([In] SafeHandle hdc, [Out] out TOAPI.Types.XFORM lpXform);

        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWorldTransform([In] SafeHandle hdc, ref TOAPI.Types.XFORM lpXform);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ModifyWorldTransform([In] SafeHandle hdc, [In] TOAPI.Types.XFORM lpXform,
           uint iMode);

    }
}
