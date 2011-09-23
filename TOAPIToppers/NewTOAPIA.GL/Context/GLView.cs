using System;

using TOAPI.OpenGL;     // For wgl.xxx
using TOAPI.GDI32;

namespace NewTOAPIA.GL
{
    public class GLView
    {
        protected IntPtr fDCHandle;

        private GLContext fGLContext;
        private GraphicsInterface fGI;
        string fTitle;
        bool fIsDoubleBuffered;

        #region Constructor
        public GLView(string title)
            :this(title, true)
        {
        }

        public GLView(string title, bool doubleBuffer)
        {
            fIsDoubleBuffered = doubleBuffer;
            fTitle = title;
        }

        #endregion

        public string Title
        {
            get { return fTitle; }
        }

        public GLContext GLContext
        {
            get {return fGLContext;}
        }

        public bool CreateContext(IntPtr windowHandle, int colorBits, int depthBits, int stencilBits)
        {
            fGLContext = new GLContext(windowHandle, PFDFlags.DoubleBuffer);
            fGI = new GraphicsInterface(fGLContext);

            return (null != fGLContext);
        }

        public void CloseContext(IntPtr windowHandle)
        {
            fGLContext.Disconnect();
        }
        
        public void SwapBuffers()
        {
            if (fIsDoubleBuffered)
                gl.SwapBuffers(fGLContext.DCHandle);
            
            //wgl.wglSwapLayerBuffers(fGLContext.DCHandle, wgl.WGL_SWAP_MAIN_PLANE);
        }

        ~GLView()
        {
        }

    }
}
