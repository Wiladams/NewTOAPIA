
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLColorBuffer : GLBuffer, ISelectable
    {
        DrawBufferMode fWhichBuffer;

        public GLColorBuffer(GraphicsInterface gi)
            : this(gi, DrawBufferMode.FrontLeft)
        {
        }

        public GLColorBuffer(GraphicsInterface gi, DrawBufferMode whichBuffer)
            : base(gi, ClearBufferMask.ColorBufferBit)
        {
            fWhichBuffer = whichBuffer;
        }

        #region Properties
        public ColorRGBA Color
        {
            get 
            { 
                return GI.State.ColorClearValue; 
            }
            
            set
            {
                GI.ClearColor(value);
            }
        }

        public DrawBufferMode WhichBuffers
        {
            get { return fWhichBuffer; }
        }

        #endregion

        /// <summary>
        /// Select a buffer to be drawn into.
        /// </summary>
        public virtual void Select()
        {
            GI.DrawBuffer(WhichBuffers);
        }

        public virtual void Unselect()
        {
            GI.DrawBuffer(DrawBufferMode.None);
        }
    }
}
