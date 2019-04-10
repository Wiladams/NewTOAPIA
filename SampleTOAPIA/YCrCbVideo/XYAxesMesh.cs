using System;

//using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;
using NewTOAPIA.Graphics;

namespace QuadVideo
{
    /// <summary>
    /// This object uses a set of quads to represent a mesh that is in the XY plane.
    /// A size parameter sets the model coordinate boundaries of the mesh
    /// The Resolution determines how many quads are constructed along each axis.
    /// 
    /// This class clearly separates the creation of vertices, indices, and texture coodinates
    /// for the sake of clarity.  It may not be the most efficient manner, but is 
    /// probably the easiest to understand, modiy, extend, and maintain.
    /// 
    /// This is not the most optimal representation of a mesh of quads.  First, the 
    /// individual vertices are duplicated rather than shared.  This is to allow 
    /// setting of individual normal vectors, and texture coordinates per vertex.
    /// Also, instead of using quad strips, each individual quad is drawn as a separate
    /// object.  Again, this is for full control of drawing.
    /// 
    /// One scenario might be that each individual quad is drawn and some shader
    /// parameters are set before the drawing occurs.  This might allow a shader 
    /// to convey information to the quad such as which position it is in relative
    /// to the resolution of the entire mesh.
    /// </summary>
    public class XYAxesMesh : Mesh3D
    {
        #region Private Fields
        Vector3D fSize;
        Resolution fResolution;
        GLTexture fTexture;

        // These are some variables to help with calculations.
        float minX;
        float maxX;

        float minY;
        float maxY;

        float xDiff;
        float yDiff;
        float xIncr;
        float yIncr;

        #endregion

        #region Constructor
        /// <summary>
        /// To construct a mesh, we need to know the size and resolution.  
        /// We also set the texture as a convenience.
        /// </summary>
        /// <param name="boundary">The size on the XY plane.  The value is not used.</param>
        /// <param name="res">The resolution determines how many rows and columns of quads will be generated.</param>
        /// <param name="texture">An optional texture object to be bound to the mesh.
        /// Normalized texture coordinates will be generated whether a texture object is assigned or not.</param>
        public XYAxesMesh(Vector3D boundary, Resolution res, GLTexture texture)
            : base(BeginMode.Quads)
        {
            fSize = boundary;
            fResolution = res;
            Texture = texture;

            CreateMesh();
        }
        #endregion

        #region Properties
        public Vector3D Size
        {
            get { return fSize; }
        }

        public Resolution Resolution
        {
            get { return fResolution; }
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
        void SetupTextureOptions(GraphicsInterface GI)
        {
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
            GI.TexEnv(TextureEnvModeParam.Modulate);
            GI.Features.Texturing2D.Enable();
            GI.FrontFace(FrontFaceDirection.Ccw);

            if (null != fTexture)
                fTexture.Bind();
        }

        /// <summary>
        /// This override is called by whomever is controlling the rendering process.
        /// We call the base Render method as it will automatically use the 
        /// vertices, indices, and texture coordinates to draw the quads. 
        /// </summary>
        /// <param name="GI"></param>
        public override void Render(GraphicsInterface GI)
        {
            SetupTextureOptions(GI);

            base.Render(GI);
        }
        #endregion

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
            minX=0.0f;
            maxX = Size.X;
            
            minY=0.0f;
            maxY = Size.Y;


            // Now that we have the min/max sizes in the x-y plane
            // Construct the quad vertices based on the resolution.  
            // Start from the bottom (y == 0) and go up
            // Move from left (x == 0) to right
            xDiff = maxX - minX;
            yDiff = maxY - minY;
            xIncr = xDiff / Resolution.Columns;
            yIncr = yDiff / Resolution.Rows;
            
            // Now call the routines that will setup all our
            // desired attributes for vertices, indices, and texture coordinates
            CreateVertices();
            CreateIndices();
            CreateTextureCoords();
        }

        public void CreateVertices()
        {
            Vector3f[] vertices = new Vector3f[(Resolution.Columns) * (Resolution.Rows) * 4];
            int vIndex = 0;

            for (int row = 0; row < Resolution.Rows; row++)
            {
                for (int column = 0; column < Resolution.Columns; column++)
                {
                    // Use this set to do the regular display
                    Vector3f vertex1 = new Vector3f(minX + column * xIncr, minY + row * yIncr, 0);
                    Vector3f vertex2 = new Vector3f(minX + (column + 1) * xIncr, minY + row * yIncr, 0);
                    Vector3f vertex3 = new Vector3f(minX + (column + 1) * xIncr, minY + (row + 1) * yIncr, 0);
                    Vector3f vertex4 = new Vector3f(minX + column * xIncr, minY + (row + 1) * yIncr, 0);

                    // Set the vertices for the quad
                    vertices[vIndex + 0] = vertex1;
                    vertices[vIndex + 1] = vertex2;
                    vertices[vIndex + 2] = vertex3;
                    vertices[vIndex + 3] = vertex4;

                    vIndex += 4;
                }
            }

            // Assign the vertices
            Vertices = vertices;
        }

        void CreateIndices()
        {
            int[] indices = new int[Resolution.Columns * Resolution.Rows * 4];

            int vIndex = 0;

            for (int row = 0; row < Resolution.Rows; row++)
            {
                for (int column = 0; column < Resolution.Columns; column++)
                {
                    // Set the indices for the quad
                    indices[vIndex + 0] = vIndex;       // Lower Left
                    indices[vIndex + 1] = vIndex + 1;   // Lower Right
                    indices[vIndex + 2] = vIndex + 2;   // Upper Right
                    indices[vIndex + 3] = vIndex + 3;   // Upper Left

                    vIndex += 4;
                }
            }

            // Assign the indices
            Indices = indices;
        }

        void CreateTextureCoords()
        {
            TextureCoordinates[] texCoords = new TextureCoordinates[(Resolution.Columns) * (Resolution.Rows) * 4];

            int vIndex = 0;
            float xOffset=0;
            float yOffset = 0;
            float xTxIncr = 1.0f/Resolution.Columns;
            float yTxIncr = 1.0f/Resolution.Rows;

            for (int row = 0; row < Resolution.Rows; row++)
            {
                yOffset = row * yTxIncr;
                for (int column = 0; column < Resolution.Columns; column++)
                {
                    xOffset = column * xTxIncr;
                    // Set the texture coords for the quad
                    texCoords[vIndex + 0].Set(xOffset, yOffset);  // Lower left
                    texCoords[vIndex + 1].Set(xOffset+xTxIncr, yOffset);  // Lower right
                    texCoords[vIndex + 2].Set(xOffset + xTxIncr, yOffset+yTxIncr);  // upper right
                    texCoords[vIndex + 3].Set(xOffset, yOffset + yTxIncr);  // upper left

                    vIndex += 4;
                }
            }

            // assign the texture coordinates
            TexCoords = texCoords;
        }
        #endregion
    }
}