using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace TOAPI.OpenGL
{
    public partial class wgl
    {
        // Callback functions
        public delegate int PROC();


        //wglCopyContext
        [DllImport("opengl32.dll", EntryPoint = "wglCopyContext")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglCopyContext(IntPtr param0, IntPtr param1, uint param2);

        //wglCreateContext
        [DllImport("opengl32.dll", EntryPoint = "wglCreateContext")]
        public static extern IntPtr wglCreateContext(IntPtr param0);

        //wglCreateLayerContext
        [DllImport("opengl32.dll", EntryPoint = "wglCreateLayerContext")]
        public static extern IntPtr wglCreateLayerContext(IntPtr param0, int param1);

        //wglDeleteContext
        [DllImport("opengl32.dll", EntryPoint = "wglDeleteContext")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglDeleteContext(IntPtr param0);

        //wglDescribeLayerPlane
        [DllImport("opengl32.dll", EntryPoint = "wglDescribeLayerPlane")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglDescribeLayerPlane(IntPtr param0, int param1, int param2, uint param3, ref LAYERPLANEDESCRIPTOR param4);

        //wglGetCurrentContext
        [DllImport("opengl32.dll", EntryPoint = "wglGetCurrentContext")]
        public static extern IntPtr wglGetCurrentContext();

        //wglGetCurrentDC
        [DllImport("opengl32.dll", EntryPoint = "wglGetCurrentDC")]
        public static extern IntPtr wglGetCurrentDC();

        //wglGetLayerPaletteEntries
        [DllImport("opengl32.dll", EntryPoint = "wglGetLayerPaletteEntries")]
        public static extern int wglGetLayerPaletteEntries(IntPtr param0, int param1, int param2, int param3, ref uint param4);

        //wglGetProcAddress
        [DllImport("opengl32.dll", EntryPoint = "wglGetProcAddress")]
        public static extern IntPtr wglGetProcAddress([In] [MarshalAs(UnmanagedType.LPStr)] string param0);

        //wglMakeCurrent
        [DllImport("opengl32.dll", EntryPoint = "wglMakeCurrent")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglMakeCurrent(IntPtr hDC, IntPtr param1);

        //wglRealizeLayerPalette
        [DllImport("opengl32.dll", EntryPoint = "wglRealizeLayerPalette")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglRealizeLayerPalette(IntPtr param0, int param1, [MarshalAs(UnmanagedType.Bool)] bool param2);

        //wglSetLayerPaletteEntries
        [DllImport("opengl32.dll", EntryPoint = "wglSetLayerPaletteEntries")]
        public static extern int wglSetLayerPaletteEntries(IntPtr param0, int param1, int param2, int param3, ref uint param4);

        //wglShareLists
        [DllImport("opengl32.dll", EntryPoint = "wglShareLists")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglShareLists(IntPtr param0, IntPtr param1);

        //wglSwapLayerBuffers
        [DllImport("opengl32.dll", EntryPoint = "wglSwapLayerBuffers")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglSwapLayerBuffers(IntPtr param0, uint param1);

        //wglUseFontBitmaps
        [DllImport("opengl32.dll", EntryPoint = "wglUseFontBitmapsW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglUseFontBitmapsW(IntPtr param0, uint param1, uint param2, uint param3);

        //wglUseFontOutlines
        [DllImport("opengl32.dll", EntryPoint = "wglUseFontOutlinesW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool wglUseFontOutlinesW(IntPtr hdc, uint first, uint count, uint listBase, float deviation, float extrusion, int format, [Out, MarshalAs(UnmanagedType.LPArray)] GLYPHMETRICSFLOAT[] gmfarray);

    }
}
