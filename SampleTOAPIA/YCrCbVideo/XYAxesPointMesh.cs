//using System;


using NewTOAPIA.GL;
using NewTOAPIA.Graphics;
using NewTOAPIA.Modeling;


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
    public class XYAxesPointMesh : Mesh3D
    {
        #region Private Fields
        Vector3D fSize;
        Resolution fResolution;

        // These are some variables to help with calculations.
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
        public XYAxesPointMesh(Vector3D boundary, Resolution res)
            : base(BeginMode.Points)
        {
            fSize = boundary;
            fResolution = res;

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

        #endregion

        #region IRenderable
        /// <summary>
        /// This override is called by whomever is controlling the rendering process.
        /// We call the base Render method as it will automatically use the 
        /// vertices, indices, and texture coordinates to draw the quads. 
        /// </summary>
        /// <param name="GI"></param>
        public override void Render(GraphicsInterface GI)
        {
            base.Render(GI);
        }
        #endregion

        #region Construction Helpers
        /// <summary>
        /// Do the work of creating the mesh vertices and indices. 
        /// 
        /// The general routine is to create a mesh in the x-y plane, using
        /// the appropriate width and height.
        /// </summary>
        void CreateMesh()
        {

            // Construct the point vertices based on the resolution.  
            // Start from the bottom (y == 0) and go up
            // Move from left (x == 0) to right
            xIncr = Size.X / Resolution.Columns;
            yIncr = Size.Y / Resolution.Rows;

            // Now call the routines that will setup all our
            // desired attributes for vertices, indices, and texture coordinates
            CreateVertices();
            CreateIndices();
        }

        public void CreateVertices()
        {
            Vector3f[] vertices = new Vector3f[(Resolution.Columns) * (Resolution.Rows) * 1];
            int vIndex = 0;

            for (int row = 0; row < Resolution.Rows; row++)
            {
                for (int column = 0; column < Resolution.Columns; column++)
                {
                    Vector3f vertex1 = new Vector3f(column * xIncr, row * yIncr, 0);  // lower left

                    vertices[vIndex + 0] = vertex1;

                    vIndex += 1;
                }
            }

            // Assign the vertices
            Vertices = vertices;
        }

        void CreateIndices()
        {
            int[] indices = new int[Resolution.Columns * Resolution.Rows * 1];

            int vIndex = 0;

            for (int row = 0; row < Resolution.Rows; row++)
            {
                for (int column = 0; column < Resolution.Columns; column++)
                {
                    // Set the indices for the quad
                    indices[vIndex + 0] = vIndex;       // Lower Left

                    vIndex += 1;
                }
            }

            // Assign the indices
            Indices = indices;
        }

        #endregion
    }
}