
using NewTOAPIA.GL;

namespace FBOImaging
{
    class TexturedCube : IRenderable
    {
        public void Render(GraphicsInterface gi)
        {
            GraphicsInterface GI = gi;

            GI.Begin(BeginMode.Quads);
            /* Front Face */
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(-1.0f, -1.0f, 1.0f);
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(1.0f, -1.0f, 1.0f);
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(1.0f, 1.0f, 1.0f);
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(-1.0f, 1.0f, 1.0f);
            /* Back Face */
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(-1.0f, -1.0f, -1.0f);
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(-1.0f, 1.0f, -1.0f);
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(1.0f, 1.0f, -1.0f);
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(1.0f, -1.0f, -1.0f);
            /* Top Face */
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(-1.0f, 1.0f, -1.0f);
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(-1.0f, 1.0f, 1.0f);
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(1.0f, 1.0f, 1.0f);
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(1.0f, 1.0f, -1.0f);
            /* Bottom Face */
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(-1.0f, -1.0f, -1.0f);
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(1.0f, -1.0f, -1.0f);
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(1.0f, -1.0f, 1.0f);
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(-1.0f, -1.0f, 1.0f);
            // Right face
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(1.0f, -1.0f, -1.0f);
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(1.0f, 1.0f, -1.0f);
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(1.0f, 1.0f, 1.0f);
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(1.0f, -1.0f, 1.0f);
            // Left Face
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(-1.0f, -1.0f, -1.0f);
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(-1.0f, -1.0f, 1.0f);
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(-1.0f, 1.0f, 1.0f);
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(-1.0f, 1.0f, -1.0f);
            GI.End();
        }
    }
}
