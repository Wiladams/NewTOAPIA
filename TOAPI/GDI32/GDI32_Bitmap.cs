using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.GDI32
{
    public partial class GDI32
    {
        // CreateDIBSection
        [DllImport("gdi32.dll", EntryPoint = "CreateDIBSection")]
        public static extern IntPtr CreateDIBSection([In] SafeHandle hdc, 
            [In] ref BITMAPINFO lpbmi,
            uint usage,
            ref IntPtr ppvBits,
            IntPtr hSection,
            int offset);

        // CreateDIBitmap
        [DllImport("gdi32.dll", EntryPoint = "CreateDIBitmap")]
        public static extern IntPtr CreateDIBitmap([In] SafeHandle hdc, 
            IntPtr pbmih, 
            uint flInit, 
            IntPtr pjBits, 
            IntPtr pbmi, 
            int iUsage);


        // Creating GDI Device Dependent Bitmaps (DDBs)
        // CreateBitmap
        [DllImport("gdi32.dll", EntryPoint = "CreateBitmap")]
        public static extern IntPtr CreateBitmap(int nWidth, int nHeight, uint nPlanes, uint nBitCount, IntPtr lpBits);

        // CreateBitmapIndirect
        [DllImport("gdi32.dll", EntryPoint = "CreateBitmapIndirect")]
        public static extern IntPtr CreateBitmapIndirect([In] ref BITMAP pbm);

        // CreateCompatibleBitmap
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap([In] SafeHandle hDC, int width, int height);


        // BLT routines
        // PatBlt
        //        558  22D 00008117 PatBlt
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PatBlt([In] SafeHandle hDC, 
            int x, int y, int nWidth, int nHeight, 
            int dwRop);

        // BitBlt
        //         19   12 00006AB7 BitBlt
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt([In] SafeHandle hDC, 
            int x, int y, int nWidth, int nHeight,
            [In] SafeHandle hSrcDC, 
            int xSrc, int ySrc, 
            int dwRop);

        // MaskBlt
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool MaskBlt([In] SafeHandle hdcDest, 
            int nXDest, int nYDest, int nWidth, int nHeight,
            [In] SafeHandle hdcSrc, 
            int nXSrc, int nYSrc, 
            IntPtr hbmMask, int xMask,
            int yMask, uint dwRop);

        // PlgBlt
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PlgBlt([In] SafeHandle hdcDest, 
            POINT[] lpPoint,
            SafeHandle hdcSrc, 
            int nXSrc, int nYSrc, int nWidth, int nHeight,
            IntPtr hbmMask, int xMask, int yMask);

        // StretchBlt
        [DllImport("gdi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StretchBlt([In] SafeHandle hdcDest, 
            int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            [In] SafeHandle hdcSrc, 
            int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            int dwRop);

        // TransparentBlt
        //        397  18C 0000B85A GdiTransparentBlt
        [DllImport("gdi32.dll", EntryPoint = "GdiTransparentBlt")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        public static extern bool TransparentBlt([In] SafeHandle hdcDest, 
            int xoriginDest, int yoriginDest, int wDest, int hDest,
            [In] SafeHandle hdcSrc, 
            int xoriginSrc, int yoriginSrc, int wSrc, int hSrc, 
            UInt32 crTransparent);

        // AlphaBlend
        [DllImport("gdi32.dll", SetLastError = true, EntryPoint = "GdiAlphaBlend")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AlphaBlend([In] SafeHandle hdcDest, 
            int nXOriginDest, int nYOriginDest,
            int nWidthDest, int nHeightDest,
            [In] SafeHandle hdcSrc, 
            int nXOriginSrc, int nYOriginSrc, int nWidthSrc, int nHeightSrc,
            BLENDFUNCTION blendFunction);


        // Drawing DIB Sections
        // SetDIBitsToDevice
        [DllImport("gdi32.dll", EntryPoint = "SetDIBitsToDevice")]
        public static extern int SetDIBitsToDevice([In] SafeHandle hdc, 
            int xDest, int yDest, uint w, uint h, 
            int xSrc, int ySrc, 
            uint StartScan, uint cLines, 
            IntPtr lpvBits, 
            [In] ref BITMAPINFO lpbmi, 
            uint ColorUse);

        // StretchDIBits
        [DllImport("gdi32.dll", EntryPoint = "StretchDIBits")]
        public static extern int StretchDIBits([In] SafeHandle hdc, 
            int xDest, int yDest, int DestWidth, int DestHeight, 
            int xSrc, int ySrc, int SrcWidth, int SrcHeight, 
            IntPtr lpBits, 
            [In] ref BITMAPINFO lpbmi, 
            uint iUsage, uint rop);

    }
}
