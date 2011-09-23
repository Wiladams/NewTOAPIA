
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class AmbientLight : GLAspect
    {
        ColorRGBA fColor;

        public AmbientLight(GraphicsInterface gi, ColorRGBA  color)
            :base(gi)
        {
            fColor = color;
        }

        public virtual ColorRGBA Color
        {
            get { return fColor; }
            set
            {
                SetColor(value);
            }
        }

        public virtual void SetColor(ColorRGBA color)
        {
            fColor = color;
            GL.LightModel(LightModelParameter.LightModelAmbient, (float[])fColor);
        }

        public override void Realize()
        {
        }

        public override string ToString()
        {
            return string.Format("<AmbientLight>{0}</AmbientLight>", fColor);
        }
    }
}
