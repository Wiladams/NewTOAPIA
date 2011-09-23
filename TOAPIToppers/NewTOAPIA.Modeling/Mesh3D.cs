

namespace NewTOAPIA.Modeling
{
    using System;
    using System.Runtime.InteropServices;
    using System.Collections.Generic;
    
    using NewTOAPIA.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;

    public abstract class Mesh3D : GLRenderable
    {
        BeginMode fDrawingPrimitive;
        protected GraphicsInterface GI { get; set; }
        protected VertexBufferObject fVertexBufferObject;
        protected VertexBufferObject fTextureBufferObject;
        protected BufferObjectIndices fIndexBufferObject;
        int fNumberOfIndices;
        private Vector3f[] normals;
        private ColorRGBA[] colors;

        public bool fUseColors;
        public bool fUseEdges;
        public bool fUseIndices;
        public bool fUseNormals;
        public bool fUseTexture;
        public bool fUseVertices;
        public bool fUseShader;

        #region Constructors
        //public Mesh3D(BeginMode mode)
        //    :this(null, mode)
        //{
        //}

        public Mesh3D(GraphicsInterface gi, BeginMode mode)
        {
            GI = gi;
            fDrawingPrimitive = mode;
            fUseVertices = true;
            fUseIndices = true;
        }
        #endregion

        public void SetVertices(Vector3f[] verts)
        {
            if (fVertexBufferObject != null)
            {
                fVertexBufferObject.Dispose();
                fVertexBufferObject = null;
            }


            // Write the vertex data to the buffer
            GCHandle dataPtr = GCHandle.Alloc(verts, GCHandleType.Pinned);
            int dataSize = Marshal.SizeOf(typeof(Vector3f)) * verts.Length;
            fVertexBufferObject = new VertexBufferObject(GI);
            fVertexBufferObject.Bind();
            fVertexBufferObject.Size = dataSize;
            
            try
            {
                fVertexBufferObject.Bind();
                fVertexBufferObject.Write(dataPtr.AddrOfPinnedObject(), 0, dataSize);
            }
            finally
            {
                fVertexBufferObject.Unbind();
                dataPtr.Free();
            }
       }

        //public VertexBufferObject Vertices
        //{
        //    get { return fVertexBufferObject; }
        //}


        public Vector3f[] Normals
        {
            get { return normals; }
            protected set
            {
                normals = value;
            }
        }

        public void SetTextureCoordinates(TextureCoordinates[] coords)
        {
            if (fTextureBufferObject != null)
            {
                fTextureBufferObject.Dispose();
                fTextureBufferObject = null;
            }


            // Write the vertex data to the buffer
            GCHandle dataPtr = GCHandle.Alloc(coords, GCHandleType.Pinned);
            int dataSize = Marshal.SizeOf(typeof(TextureCoordinates)) * coords.Length;
            fTextureBufferObject = new VertexBufferObject(GI);
            fTextureBufferObject.Bind();
            fTextureBufferObject.Size = dataSize;

            try
            {
                fTextureBufferObject.Bind();
                fTextureBufferObject.Write(dataPtr.AddrOfPinnedObject(), 0, dataSize);
            }
            finally
            {
                fTextureBufferObject.Unbind();
                dataPtr.Free();
            }
        }

        //public GLBufferObject TexCoords
        //{
        //    get { return fTextureBufferObject; }
        //}

        public void SetIndices(int[] indices)
        {
            if (fIndexBufferObject != null)
            {
                fIndexBufferObject.Dispose();
                fIndexBufferObject = null;
            }

            // Write the vertex data to the buffer
            GCHandle dataPtr = GCHandle.Alloc(indices, GCHandleType.Pinned);
            fNumberOfIndices = indices.Length;
            int dataSize = Marshal.SizeOf(typeof(int)) * fNumberOfIndices;
            fIndexBufferObject = new BufferObjectIndices(GI);
            fIndexBufferObject.Bind();
            fIndexBufferObject.Size = dataSize;

            try
            {
                fIndexBufferObject.Bind();
                fIndexBufferObject.Write(dataPtr.AddrOfPinnedObject(), 0, dataSize);
            }
            finally
            {
                fIndexBufferObject.Unbind();
                dataPtr.Free();
            }
        }

        public BufferObjectIndices Indices
        {
            get { return fIndexBufferObject; }
        }

        public ColorRGBA[] Colors
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


            if (fUseNormals && (null != Normals))
            {
                gi.ClientFeatures.NormalArray.Enable();
                gi.NormalPointer(Normals);
            }

            if (fUseTexture && (fTextureBufferObject != null))
            {
                gi.ClientFeatures.TextureCoordArray.Enable();
                fTextureBufferObject.Bind();
                int textureBufferSize = fTextureBufferObject.Size;
                gi.TexCoordPointer(2, TexCoordPointerType.Float, 0, IntPtr.Zero);
            }


            if (fUseVertices && (fVertexBufferObject != null))
            {
                gi.ClientFeatures.VertexArray.Enable();
                fVertexBufferObject.Bind();
                int vertexBufferSize = fVertexBufferObject.Size;
                gi.VertexPointer(3, VertexPointerType.Float, 0, IntPtr.Zero);
            }

            if (fUseIndices && (null != Indices))
            {
                gi.ClientFeatures.IndexArray.Enable();
                fIndexBufferObject.Bind();
                int indexBufferSize = fIndexBufferObject.Size;
                gi.IndexPointer(IndexPointerType.Int, 0, IntPtr.Zero);
            }
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            gi.DrawElements(fDrawingPrimitive, fNumberOfIndices, DrawElementType.UnsignedInt, IntPtr.Zero);
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

            if (fUseTexture && (null != fTextureBufferObject))
                gi.ClientFeatures.TextureCoordArray.Disable();

            if (fUseVertices && (null != fVertexBufferObject))
            {
                gi.ClientFeatures.VertexArray.Disable();
                if (fVertexBufferObject != null)
                    fVertexBufferObject.Unbind();
            }
        }
    }
}
