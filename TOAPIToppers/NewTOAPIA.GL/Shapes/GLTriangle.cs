

namespace NewTOAPIA.GL
{
    public class GLTriangle : GLRenderable
    {
        GLVertex fVertex1;
        GLVertex fVertex2;
        GLVertex fVertex3;

        public GLTriangle(float3 v1, float3 v2, float3 v3, ColorRGBA aColor)
        {
            fVertex1 = new GLVertex(v1, aColor);
            fVertex2 = new GLVertex(v1, aColor);
            fVertex3 = new GLVertex(v1, aColor);
        }

        public GLTriangle(GLVertex v1, GLVertex v2, GLVertex v3)
        {
            fVertex1 = v1;
            fVertex2 = v2;
            fVertex3 = v3;
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            gi.Drawing.Triangles.Begin();
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            gi.Color(fVertex1.fColor);
            gi.Vertex(fVertex1.fPosition);
            gi.Color(fVertex2.fColor);
            gi.Vertex(fVertex2.fPosition);
            gi.Color(fVertex3.fColor);
            gi.Vertex(fVertex3.fPosition);
        }

        protected override void EndRender(GraphicsInterface gi)
        {
            gi.Drawing.Triangles.End();
        }
    }
}
