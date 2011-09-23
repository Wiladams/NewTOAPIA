using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;

namespace Reflection
{
    public class Cube : Mesh3D
    {
        public bool ShowTexture { get; set; }

        Vector3f[] fMyVertices;
        int[] fMyIndices;
        TextureCoordinates[] fMyTexCoords;

        Vector3D fSize;
        GLTexture fTexture;
        int numFaces = 5;

        // These are some variables to help with calculations.
        float minX;
        float maxX;

        float minY;
        float maxY;

        float minZ;
        float maxZ;

        
        public Cube(GraphicsInterface gi, Vector3D size, GLTexture texture)
            :base(gi, BeginMode.Quads)
        {
            fSize = size;
            Texture = texture;

            CreateMesh();
        }

        public Vector3D Size
        {
            get { return fSize; }
        }

        public GLTexture Texture
        {
            get { return fTexture; }
            set
            {
                fTexture = value;
                if (null != fTexture)
                    fUseTexture = true;
                else
                    fUseTexture = false;
            }
        }

        #region Construction Helpers
        /// <summary>
        /// Do the work of creating the mesh vertices, indices, and 
        /// texture coordinates.
        /// </summary>
        void CreateMesh()
        {
            // The general routine is to create a mesh in the x-y plane, using
            // the appropriate width and height.

            // First calculate the min/max values in the x-y plane
            minX = -Size.X / 2;
            maxX = Size.X/2;

            minY = -Size.Y/2;
            maxY = Size.Y;

            minZ = -Size.Z / 2;
            maxZ = Size.Z / 2;


            // Now call the routines that will setup all our
            // desired attributes for vertices, indices, and texture coordinates
            CreateVertices();
            CreateIndices();
            CreateTextureCoords();
        }

        public void CreateVertices()
        {
            Vector3f[] vertices = new Vector3f[numFaces * 4];
            int face = 0;

            // front
            vertices[face*4 + 0] = new Vector3f(minX, minY, maxZ);    // lower left
            vertices[face*4 + 1] = new Vector3f(maxX, minY, maxZ);    // lower right
            vertices[face*4 + 2] = new Vector3f(maxX, maxY, maxZ);    // upper right
            vertices[face*4 + 3] = new Vector3f(minX, maxY, maxZ);    // upper left

            // back
            face = 1;
            vertices[face*4 + 0] = new Vector3f(maxX, minY, minZ);    // lower left
            vertices[face*4 + 1] = new Vector3f(minX, minY, minZ);    // lower right
            vertices[face*4 + 2] = new Vector3f(minX, maxY, minZ);    // upper right
            vertices[face*4 + 3] = new Vector3f(maxX, maxY, minZ);    // upper left

            // right
            face = 2;
            vertices[face * 4 + 0] = new Vector3f(maxX, minY, maxZ);    // lower left
            vertices[face * 4 + 1] = new Vector3f(maxX, minY, minZ);    // lower right
            vertices[face * 4 + 2] = new Vector3f(maxX, maxY, minZ);    // upper right
            vertices[face * 4 + 3] = new Vector3f(maxX, maxY, maxZ);    // upper left

            // left
            face = 3;
            vertices[face * 4 + 0] = new Vector3f(minX, minY, minZ);    // lower left
            vertices[face * 4 + 1] = new Vector3f(minX, minY, maxZ);    // lower right
            vertices[face * 4 + 2] = new Vector3f(minX, maxY, maxZ);    // upper right
            vertices[face * 4 + 3] = new Vector3f(minX, maxY, minZ);    // upper left

            // bottom
            face = 4;
            vertices[face * 4 + 0] = new Vector3f(minX, minY, minZ);    // lower left
            vertices[face * 4 + 1] = new Vector3f(maxX, minY, minZ);    // lower right
            vertices[face * 4 + 2] = new Vector3f(maxX, minY, maxZ);    // upper right
            vertices[face * 4 + 3] = new Vector3f(minX, minY, maxZ);    // upper left

            // top 

            // Assign the vertices
            fMyVertices = vertices;
        }

        void CreateIndices()
        {
            int[] indices = new int[numFaces * 4];

            for (int face = 0; face < numFaces; face++)
            {
                // Front
                indices[face*4 + 0] = face*4 + 0;
                indices[face * 4 + 1] = face * 4 + 1;
                indices[face * 4 + 2] = face * 4 + 2;
                indices[face * 4 + 3] = face * 4 + 3;
            }

            // Assign the indices
            fMyIndices = indices;
        }

        void CreateTextureCoords()
        {
            TextureCoordinates[] texCoords = new TextureCoordinates[numFaces * 4];

            for (int face = 0; face < numFaces; face++)
            {
                texCoords[face*4 + 0] = new TextureCoordinates(0, 0);
                texCoords[face*4 + 1] = new TextureCoordinates(1, 0);
                texCoords[face*4 + 2] = new TextureCoordinates(1, 1);
                texCoords[face*4 + 3] = new TextureCoordinates(0, 1);
            }

            // assign the texture coordinates
            fMyTexCoords = texCoords;
        }
        #endregion


        #region IRenderable
        void SetupTextureOptions(GraphicsInterface GI)
        {
            if (fTexture != null && ShowTexture)
            {
                GI.Features.Texturing2D.Enable();

                fTexture.Bind();

                GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
                GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
                GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
                GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
                GI.TexEnv(TextureEnvModeParam.Modulate);
                //GI.FrontFace(FrontFaceDirection.Ccw);
            }
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            if (this.fVertexBufferObject == null)
               SetVertices(fMyVertices);

            if (this.fIndexBufferObject == null)
                SetIndices(fMyIndices);

            if (this.fTextureBufferObject == null)
                SetTextureCoordinates(fMyTexCoords);

            base.BeginRender(gi);

            SetupTextureOptions(gi);
        }

        protected override void EndRender(GraphicsInterface gi)
        {
            base.EndRender(gi);

            if (fTexture != null)
                fTexture.Unbind();
        }
        #endregion

    }
    
}
