using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLUQuadric : GLRenderable
    {
        protected IntPtr fQuadric;

        QuadricNormalType fNormalType;
        QuadricDrawStyle fDrawingStyle;
        QuadricOrientation fOrientation;
        int fUsesTexture;

        public GLUQuadric(QuadricDrawStyle style, QuadricNormalType normalType, QuadricOrientation orientation)
        {
            fQuadric = glu.gluNewQuadric();

            fDrawingStyle = style;
            fNormalType = normalType;
            fOrientation = orientation;

            fUsesTexture = gl.GL_TRUE;
        }

        public IntPtr Handle
        {
            get { return fQuadric; }
        }

        public virtual QuadricNormalType NormalType
        {
            get { return fNormalType; }
            set
            {
                fNormalType = value;
            }
        }

        public virtual QuadricDrawStyle DrawingStyle
        {
            get { return fDrawingStyle; }
            set
            {
                fDrawingStyle = value;
            }
        }

        public virtual QuadricOrientation Orientation
        {
            get { return fOrientation; }
            set
            {
                fOrientation = value;
            }
        }

        public virtual int UsesTexture
        {
            get { return fUsesTexture; }
            set
            {
                fUsesTexture = value;
            }
        }

        #region Rendering
        protected override void BeginRender(GraphicsInterface gi)
        {
            glu.gluQuadricNormals(Handle, (int)fNormalType);
            glu.gluQuadricDrawStyle(Handle, (int)fDrawingStyle);
            glu.gluQuadricOrientation(Handle, (int)fOrientation);
            glu.gluQuadricTexture(Handle, fUsesTexture);
        }

        #endregion

        ~GLUQuadric()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            // Take yourself off the Finalization queue 
            // to prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///    <para>
        ///       Releases the handle associated with this bitmap.
        ///    </para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            lock (this)
            {
                glu.gluDeleteQuadric(fQuadric);
            }
        }

    }

}
