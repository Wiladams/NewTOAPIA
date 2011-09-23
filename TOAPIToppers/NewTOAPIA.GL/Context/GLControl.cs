using System;
using System.ComponentModel;
using System.Windows.Forms;

using TOAPI.Types;
using TOAPI.User32;
using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{

    public class GLControl : UserControl
    {
        // Multicast event for when OpenGL is first initialized
        public delegate void OpenGLStartedHandler(GLControl sender);
        public event OpenGLStartedHandler OpenGLStartedEvent = null;

        // Internal properties of this control.
        private IContainer fComponents;
        private bool mAttemptedInitialization = false;

        private GLContext fGLContext;
        private GraphicsInterface mGR = null;

        public GLControl()
        {
            InitializeStyles();
            InitializeComponent();
        }

        #region InitializeStyles()
        /// <summary>
        ///     Initializes the control's styles.
        /// </summary>
        private void InitializeStyles()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, false);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        #endregion InitializeStyles()

        #region InitializeComponent
        private void InitializeComponent()
        {
            fComponents = new System.ComponentModel.Container();
            // 
            // SimpleOpenGlControl
            // 
            BackColor = System.Drawing.Color.Black;
            Size = new System.Drawing.Size(320, 240);
        }
        #endregion


        #region Protected Property Overloads
        #region CreateParams CreateParams
        /// <summary>
        ///     Overrides the control's class style parameters.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = cp.ClassStyle | User32.CS_VREDRAW | User32.CS_HREDRAW | User32.CS_OWNDC;
                return cp;
            }
        }
        #endregion CreateParams CreateParams
        #endregion Protected Property Overloads

        public IntPtr GetHDC()
        {
            return (fGLContext.DCHandle);
        }

        public IntPtr GetHGLRC()
        {
            return (fGLContext.GLHandle);
        }

        public GraphicsInterface GetGI()
        {
            return (mGR);
        }


        protected void InvokeOpenGLStarted()
        {
            if (null != OpenGLStartedEvent)
            {
                OpenGLStartedEvent(this);
            }
        }

        // Methods handled by this control itself; called by the operating system.
        // In many cases, when one of the following methods is called by the 
        // operating system, the method will call a corrsponding method to invoke
        // event handlers set by the client of this control.

        protected override void OnPaint(PaintEventArgs e)
        {
            // If in designer mode, then just fill the area with the BackColor color.
            if ((DesignMode) || (false == mAttemptedInitialization))
            {
                e.Graphics.Clear(BackColor);

                if (DesignMode)
                {
                    return;
                }
            }

            if (false == mAttemptedInitialization)
            {
                mAttemptedInitialization = true;

                fGLContext = new GLContext(Handle, PFDFlags.DoubleBuffer);
                mGR = new GraphicsInterface(fGLContext);
                fGLContext.MakeCurrentContext();

                // Set some default drawing conditions
                mGR.ClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, 1.0f);

                InvokeOpenGLStarted();

                base.OnPaint(e); // Triggers Paint *event*; thus, our override gets called before the event

                return;
            }

            // Set some default drawing conditions
            mGR.ClearColor(BackColor.R / 255.0f, BackColor.G / 255.0f, BackColor.B / 255.0f, 1.0f);

            fGLContext.MakeCurrentContext();

            base.OnPaint(e); // Triggers Paint *event*; thus, our override gets called before the event
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // Override painting background so there's no flickering as the background
            // is repainted before the next GL Frame is painted.
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            base.OnMouseDown(e); // This will invoke any events connected to this window
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (fComponents != null)
                {
                    fComponents.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}