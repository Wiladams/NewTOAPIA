using System;

using TOAPI.OpenGL;
using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.GL
{
    public class GLContext
    {
        static GLContext gContext;

        int ColorBits;
        int DepthBits;

        private readonly IntPtr fHWnd;
        private readonly IntPtr fHDC;
        private IntPtr fHGLC;

        
        static GLContext()
        {
            gContext = null;
        }


        /// <summary>
        /// Initialize a GLContext using nothing more than the window handle.
        /// </summary>
        /// <param name="hwnd">The handle to the window</param>
        /// <param name="flags">flags to pass to context creation</param>
        public GLContext(IntPtr hwnd, int colorBits, int depthBits, PFDFlags flags)
        {
            ColorBits = colorBits;
            DepthBits = depthBits;

            fHWnd = hwnd;
            fHDC = User32.GetDC(hwnd);
            InitializeContext(flags);
            gContext = this;
        }

        public static GLContext GetThreadContext()
        {
            return gContext;
        }

        public IntPtr WindowHandle
        {
            get { return fHWnd; }
        }

        public IntPtr GDIDeviceContextHandle
        {
            get { return fHDC; }
        }

        public IntPtr GLHandle
        {
            get { return fHGLC; }
        }

        public void InitializeContext(PFDFlags flags)
        {
            // NOTE: You will get the following error:
            //             126 : ERROR_MOD_NOT_FOUND
            // if you attempt to create a render context too soon after creating a
            // window and getting its Device Context (DC).
            // Calling wglSwapBuffers( hdc ) before attempting to create the RC
            // evidently solves this problem.
            // CAUTION: Not doing the following wgl.wglSwapBuffers() on the DC will
            // result in a failure to subsequently create the RC.
            wgl.wglSwapLayerBuffers(fHWnd, 0);

            bool result;

            // This makes sure no GL context is associated with 
            // the current thread.
            result = wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);

            // Now create a pixel format descriptor that is appropriate
            // for GL.  Mainly it's in the flags passed in.
            PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR();
            pfd.Init();

            pfd.dwFlags = (uint)(flags | PFDFlags.SupportOpenGL | PFDFlags.DrawToWindow);   // Put in 'SupportOpenGL' so at least there is OpenGL support
            pfd.iPixelType = (byte)PFDPixelType.RGBA;
            pfd.cColorBits = (byte)ColorBits;                        // How many bits used for color
            pfd.cRedBits = 0;
            pfd.cRedShift = 0;
            pfd.cGreenBits = 0;
            pfd.cGreenShift = 0;
            pfd.cBlueBits = 0;
            pfd.cBlueShift = 0;
            pfd.cAlphaBits = 0;
            pfd.cAlphaShift = 0;
            pfd.cAccumBits = 0;
            pfd.cAccumRedBits = 0;
            pfd.cAccumGreenBits = 0;
            pfd.cAccumBlueBits = 0;
            pfd.cAccumAlphaBits = 0;
            pfd.cDepthBits = (byte)DepthBits;                        // How many bits used for depth buffer
            pfd.cStencilBits = 0;
            pfd.cAuxBuffers = 0;            
            pfd.iLayerType = (byte)PFDLayerPlanes.Main;
            pfd.bReserved = 0;
            pfd.dwLayerMask = 0;
            pfd.dwVisibleMask = 0;
            pfd.dwDamageMask = 0;

            // Choose Pixel Format
            int pixelFormat = gl.ChoosePixelFormat(fHDC, ref pfd); // Get format that matches closest
            if (0 == pixelFormat)
            {
                // throw an appropriate exception
                throw new ArgumentException("ChoosePixelFormat returned '0'", "pixelFormat");
            }

            result = gl.SetPixelFormat(fHDC, pixelFormat, ref pfd);    // Set this as the actual format
            if (!result)
            {
                // throw an appropriate exception
                throw new ArgumentException("SetPixelFormat returned false", "result");
            }

            // Create Rendering Context (RC)

            fHGLC = IntPtr.Zero;
            fHGLC = wgl.wglCreateContext(fHDC);                   // Create a GLContext
        }

        public void MakeCurrentContext()
        {
            bool result = wgl.wglMakeCurrent(fHDC, fHGLC);             // Make this the current GL context for the calling thread
        }

        public void Disconnect()
        {
            // Disconnect the GL context
            wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            wgl.wglDeleteContext(fHGLC);
        }
    }
}
