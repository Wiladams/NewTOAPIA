using NewTOAPIA;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLPrimitiveDrawing
    {
        private GraphicsInterface fGI;

        BracketLineLoopDrawing fLineLoopDrawing;
        BracketLinesDrawing fLinesDrawing;
        BracketLineStripDrawing fLineStripDrawing;
        BracketPointDrawing fPointsDrawing;
        BracketPolygonDrawing fPolygonDrawing;
        BracketQuadsDrawing fQuadsDrawing;
        BracketQuadStripDrawing fQuadStripDrawing;
        BracketTriangleFanDrawing fTriangleFanDrawing;
        BracketTrianglesDrawing fTrianglesDrawing;
        BracketTriangleStripDrawing fTriangleStripDrawing;

        public GLPrimitiveDrawing(GraphicsInterface gi)
        {
            fGI = gi;

            fLineLoopDrawing = new BracketLineLoopDrawing(fGI);
            fLinesDrawing = new BracketLinesDrawing(fGI);
            fLineStripDrawing = new BracketLineStripDrawing(fGI);
            fPointsDrawing = new BracketPointDrawing(fGI);
            fPolygonDrawing = new BracketPolygonDrawing(fGI);
            fQuadsDrawing = new BracketQuadsDrawing(fGI);
            fQuadStripDrawing = new BracketQuadStripDrawing(fGI);
            fTriangleFanDrawing = new BracketTriangleFanDrawing(fGI);
            fTrianglesDrawing = new BracketTrianglesDrawing(fGI);
            fTriangleStripDrawing = new BracketTriangleStripDrawing(fGI);
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public ColorRGBA Color
        {
            get { return ColorRGBA.Invisible; }
            set
            {
                GI.Color(value);
            }
        }

        #region AddVertex
        public void AddVertex(float2 vec)
        {
            GI.Vertex(vec);
        }

        public void AddVertex(float x, float y)
        {
            GI.Vertex(x, y);
        }


        public void AddVertex(float3 vec)
        {
            GI.Vertex(vec);
        }

        public void AddVertex(float x, float y, float z)
        {
            GI.Vertex(x, y, z);
        }
        #endregion

        #region Drawing Primitives
        public BracketLineLoopDrawing LineLoop
        {
            get { return fLineLoopDrawing; }
        }

        public BracketLinesDrawing Lines
        {
            get { return fLinesDrawing; }
        }

        public BracketLineStripDrawing LineStrip
        {
            get { return fLineStripDrawing; }
        }

        public BracketPointDrawing Points
        {
            get { return fPointsDrawing; }
        }

        public BracketPolygonDrawing Polygon
        {
            get { return fPolygonDrawing; }
        }

        public BracketQuadsDrawing Quads
        {
            get { return fQuadsDrawing; }
        }

        public BracketQuadStripDrawing QuadStrip
        {
            get { return fQuadStripDrawing; }
        }

        public BracketTriangleFanDrawing TriangleFan
        {
            get { return fTriangleFanDrawing;}
        }

        public BracketTrianglesDrawing Triangles
        {
            get {return fTrianglesDrawing;}
        }

        public BracketTriangleStripDrawing TriangleStrip
        {
            get {return fTriangleStripDrawing;}
        }
        #endregion
    }
}
