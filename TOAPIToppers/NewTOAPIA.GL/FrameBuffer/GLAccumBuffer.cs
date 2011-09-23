
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLAccumBuffer : GLBuffer
    {
        ColorRGBA fColor;

        public GLAccumBuffer(GraphicsInterface gi)
            : base(gi, ClearBufferMask.AccumBufferBit)
        {
        }

        public ColorRGBA Color
        {
            get { return fColor; }
            set
            {
                SetColor(value);
            }
        }

        public void SetColor(ColorRGBA color)
        {
            fColor = color;
            GI.ClearAccum(fColor.R, fColor.G, fColor.B, fColor.A);
        }
    }
}
