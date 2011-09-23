using NewTOAPIA.GL;

namespace NewTOAPIA.GL
{
    public abstract class Mesh3D : GLRenderable
    {
        BeginMode fMode;
        private Vector3f[] vertices;
        private Vector3f[] normals;
        private TextureCoordinates[] texcoords;
        private int[] indices;
        private GLColor[] colors;

        public bool fUseColors;
        public bool fUseEdges;
        public bool fUseIndices;
        public bool fUseNormals;
        public bool fUseTexture;
        public bool fUseVertices;
        public bool fUseShader;

        public Mesh3D(BeginMode mode)
        {
            fMode = mode;
            fUseVertices = true;
            fUseIndices = true;
        }

        public Vector3f[] Vertices
        {
            get { return vertices; }
            protected set
            {
                vertices = value;
            }
        }

        public Vector3f[] Normals
        {
            get { return normals; }
            protected set
            {
                normals = value;
            }
        }

        public TextureCoordinates[] TexCoords
        {
            get { return texcoords; }
            protected set
            {
                texcoords = value;
            }
        }

        public int[] Indices
        {
            get { return indices; }
            protected set
            {
                indices = value;
            }
        }

        public GLColor[] Colors
        {
            get { return colors; }
            protected set
            {
                colors = value;
            }
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            if (fUseColors && (null != Colors))
            {
                gi.ClientFeatures.ColorArray.Enable();
                gi.ColorPointer(Colors);
            }

            //if (fUseEdges && (null != Edges))
            //    gi.EnableClientState(ClientArrayType.EdgeFlagArray);

            if (fUseIndices && (null != Indices))
            {
                gi.ClientFeatures.IndexArray.Enable();
                gi.IndexPointer(IndexPointerType.Int, 0, Indices);
            }

            if (fUseNormals && (null != Normals))
            {
                gi.ClientFeatures.NormalArray.Enable();
                gi.NormalPointer(Normals);
            }

            if (fUseTexture && (null != TexCoords))
            {
                gi.ClientFeatures.TextureCoordArray.Enable();
                gi.TexCoordPointer(TexCoords);
            }

            if (fUseVertices && (null != Vertices))
            {
                gi.ClientFeatures.VertexArray.Enable();
                gi.VertexPointer(Vertices);
            }
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            gi.DrawElements(fMode, indices.Length, DrawElementType.UnsignedInt, Indices);
        }

        protected override void EndRender(GraphicsInterface gi)
        {
            if (fUseColors && (null != Colors))
                gi.ClientFeatures.ColorArray.Disable();

            //if (fUseEdges && (null != Edges))
            //    gi.DisableClientState(ClientArrayType.EdgeFlagArray);

            if (fUseIndices && (null != Indices))
                gi.ClientFeatures.IndexArray.Disable();

            if (fUseNormals && (null != Normals))
                gi.ClientFeatures.NormalArray.Disable();

            if (fUseTexture && (null != TexCoords))
                gi.ClientFeatures.TextureCoordArray.Disable();

            if (fUseVertices && (null != Vertices))
                gi.ClientFeatures.VertexArray.Disable();
        }
    }
}
