using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;

namespace ShowIt
{
    public enum AABBFace
    {
        Front,
        Back,
        Ceiling,
        Floor,
        Left,
        Right
    }

    /// <summary>
    /// This object represents a single wall of a Axis Aligned 
    /// Bounding Box
    /// </summary>
    public class AABBFaceMesh : Mesh3D
    {
        #region Private Fields
        Vector3D fSize;
        GLTexture fTexture;
        AABBFace fWhichFace;
        bool fMirrorView;
        #endregion

        #region Constructor
        AABBFaceMesh(GraphicsInterface gi, Vector3D boundary, AABBFace whichFace)
            : base(gi, BeginMode.Quads)
        {
            fSize = boundary;
            fWhichFace = whichFace;
        }
        #endregion

        #region Properties
        public bool MirrorView
        {
            get { return fMirrorView; }
            set { fMirrorView = value; }
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
        #endregion

        #region IRenderable
        public override void Render(GraphicsInterface GI)
        {
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
            GI.TexEnv(TextureEnvModeParam.Modulate);
            GI.Features.Texturing2D.Enable();

            if (fMirrorView)
                GI.FrontFace(FrontFaceDirection.Cw);
            else
                GI.FrontFace(FrontFaceDirection.Ccw);

            if (null != fTexture)
                fTexture.Bind();

            base.Render(GI);


            if (null != fTexture)
                fTexture.Unbind();
        }
        #endregion

        /// <summary>
        /// Create a mesh object that represents the wall.
        /// 
        /// </summary>
        /// <param name="roomSize">The size of the room</param>
        /// <param name="whichWall">Which wall of the room are we creating</param>
        /// <param name="res">The Resolution of the mesh, meaning, the number of rows and columns of quads to create.</param>
        /// <returns>The mesh3D object representing the wall</returns>
        public static AABBFaceMesh CreateFace(GraphicsInterface gi, Vector3D roomSize, AABBFace whichWall, Resolution res, GLTexture texture)
        {
            AABBFaceMesh wall = new AABBFaceMesh(gi, roomSize, whichWall);

            Vector3f[] vertices = new Vector3f[(res.Columns)*(res.Rows)*4];
            TextureCoordinates[] texCoords = new TextureCoordinates[(res.Columns) * (res.Rows)*4];
            int[] indices = new int[res.Columns * res.Rows * 4];

            
            // The general routine is to create a mesh in the x-y plane, using
            // the appropriate width and height.
            // Then take this mesh and rotate and translate it into the right position
            // after it is created.

            // First calculate the min/max values in the x-y plane
            float minX=0.0f;
            float maxX=0.0f;
            
            float minY=0.0f;
            float maxY=0.0f;

            switch (whichWall)
            {
                case AABBFace.Front:
                case AABBFace.Back:
                    minX = roomSize.X / -2;
                    maxX = roomSize.X / 2;

                    minY = roomSize.Y / -2;
                    maxY = roomSize.Y / 2;
                    break;

                case AABBFace.Left:
                case AABBFace.Right:
                    minX = roomSize.Z / -2;
                    maxX = roomSize.Z / 2;

                    minY = roomSize.Y / -2;
                    maxY = roomSize.Y / 2;
                    break;

                case AABBFace.Ceiling:
                case AABBFace.Floor:
                    minX = roomSize.X / -2;
                    maxX = roomSize.X / 2;

                    minY = roomSize.Z / -2;
                    maxY = roomSize.Z / 2;
                    break;
            }

            // Now that we have the min/max sizes in the x-y plane
            // Construct the quad vertices based on the resolution.  
            // Start from the bottom (negative y) and go up
            // Move from left (negative x) to right
            float xDiff = maxX - minX;
            float yDiff = maxY - minY;
            float xIncr = xDiff / res.Columns;
            float yIncr = yDiff / res.Rows;
            int vIndex = 0;

            for (int row = 0; row < res.Rows; row++)
            {
                for (int column = 0; column < res.Columns; column++)
                {
                    Vector3f vertex1 = new Vector3f(minX + column * xIncr, minY + row * yIncr, 0);
                    Vector3f vertex2 = new Vector3f(minX + (column + 1) * xIncr, minY + row * yIncr, 0);
                    Vector3f vertex3 = new Vector3f(minX + (column + 1) * xIncr, minY + (row + 1) * yIncr, 0);
                    Vector3f vertex4 = new Vector3f(minX + column * xIncr, minY + (row + 1) * yIncr, 0);

                    // Set the vertices for the quad
                    vertices[vIndex + 0] = vertex1;
                    vertices[vIndex + 1] = vertex2;
                    vertices[vIndex + 2] = vertex3;
                    vertices[vIndex + 3] = vertex4;

                    // Set the indices for the quad
                    indices[vIndex + 0] = vIndex;
                    indices[vIndex + 1] = vIndex + 1;
                    indices[vIndex + 2] = vIndex + 2;
                    indices[vIndex + 3] = vIndex + 3;

                    // Set the texture coords for the quad
                    texCoords[vIndex + 0].Set(0.0f, 0.0f);
                    texCoords[vIndex + 1].Set(1.0f, 0.0f);
                    texCoords[vIndex + 2].Set(1.0f, 1.0f);
                    texCoords[vIndex + 3].Set(0.0f, 1.0f);

                    vIndex += 4;
                }
            }

            // assign all the attributes to the mesh object
            // and return it.
            wall.SetIndices(indices);
            wall.SetVertices(vertices);
            wall.SetTextureCoordinates(texCoords);
            wall.Texture = texture;

            
            // Now transform the vertices based on which wall it is we are constructing
            minX = roomSize.X / -2;
            maxX = roomSize.X / 2;

            minY = roomSize.Y / -2;
            maxY = roomSize.Y / 2;

            float minZ = roomSize.Z / -2;
            float maxZ = roomSize.Z / 2;
            Transformation transform = new Transformation();

            switch (whichWall)
            {
                case AABBFace.Front:
                    transform.Translate(new float3(0, 0, minZ));
                    break;

                case AABBFace.Back:
                    transform.SetRotate(float3x3.Rotate((float)Math.PI, new float3(0, 1, 0)));
                    transform.Translate(new float3(0, 0, maxZ));
                    break;

                case AABBFace.Left:
                    transform.SetRotate(float3x3.Rotate((float)Math.PI/2, new float3(0, 1, 0)));
                    transform.Translate(new float3(minX, 0, 0));

                    break;

                case AABBFace.Right:
                    transform.SetRotate(float3x3.Rotate((float)-Math.PI/2, new float3(0, 1, 0)));
                    transform.Translate(new float3(maxX, 0, 0));
                    break;

                case AABBFace.Ceiling:
                    transform.SetRotate(float3x3.Rotate((float)-Math.PI/2, new float3(1, 0, 0)));
                    transform.Translate(new float3(0, maxY, 0));
                    break;

                case AABBFace.Floor:
                    transform.SetRotate(float3x3.Rotate((float)Math.PI/2, new float3(1, 0, 0)));
                    transform.Translate(new float3(0, minY, 0));
                    break;
            }

            wall.ApplyTransform(transform);


            return wall;
        }

        public void PositionWall(AABBFace whichFace)
        {
        }

        public void ApplyTransform(Transformation transform)
        {
            // Now that we have the correct transformation, use it to change the vertices
            for (int ctr = 0; ctr < Vertices.Length; ctr++)
            {
                Vector3f vec = Vertices[ctr];
                float3 floatIn = new float3(vec.x, vec.y, vec.z);
                float3 floatOut = transform.ApplyForward(floatIn);
                Vertices[ctr] = new Vector3f(floatOut.x, floatOut.y, floatOut.z);
            }
        }
    }
}