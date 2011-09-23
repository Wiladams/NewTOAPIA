using NewTOAPIA.GL;
using AGUIL;

namespace PointSprites
{
    public class SimpleHorizon : GLRenderable
    {
        GLColor fColor;

        public SimpleHorizon()
        {
            fColor = GLColor.Yellow;
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            gi.Color(fColor);
        }

        protected override void RenderContent(NewTOAPIA.GL.GraphicsInterface gi)
        {
            GLAspectLines lineAspects = new GLAspectLines(gi);

            lineAspects.LineWidth = 3.5f;
            gi.Begin(BeginMode.LineStrip);
            gi.Vertex(0.0f, 25.0f);
            gi.Vertex(50.0f, 100.0f);
            gi.Vertex(100.0f, 25.0f);
            gi.Vertex(225.0f, 115.0f);
            gi.Vertex(300.0f, 50.0f);
            gi.Vertex(375.0f, 100.0f);
            gi.Vertex(460.0f, 25.0f);
            gi.Vertex(525.0f, 100.0f);
            gi.Vertex(600.0f, 20.0f);
            gi.Vertex(675.0f, 70.0f);
            gi.Vertex(750.0f, 25.0f);
            gi.Vertex(800.0f, 90.0f);
            gi.End();
        }
    }
}
