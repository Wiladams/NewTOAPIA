using System;

namespace NewTOAPIA.GLU.Shapes
{
    using NewTOAPIA.GL;
    using NewTOAPIA.Modeling;

    public class GLUQuadric : GLRenderable
    {
        protected GraphicsInterface GI { get; set; }
        protected IntPtr fQuadric;

        QuadricNormalType fNormalType;
        QuadricDrawStyle fDrawingStyle;
        QuadricOrientation fOrientation;
        int fUsesTexture;

        public GLUQuadric(GraphicsInterface gi, QuadricDrawStyle style, QuadricNormalType normalType, QuadricOrientation orientation)
        {
            GI = gi;

            fQuadric = gi.Glu.NewQuadric();

            fDrawingStyle = style;
            fNormalType = normalType;
            fOrientation = orientation;

            fUsesTexture = 1;
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
            gi.Glu.QuadricNormals(Handle, (int)fNormalType);
            gi.Glu.QuadricDrawStyle(Handle, (int)fDrawingStyle);
            gi.Glu.QuadricOrientation(Handle, (int)fOrientation);
            gi.Glu.QuadricTexture(Handle, fUsesTexture);
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
                GI.Glu.DeleteQuadric(fQuadric);
            }
        }

    }

}
