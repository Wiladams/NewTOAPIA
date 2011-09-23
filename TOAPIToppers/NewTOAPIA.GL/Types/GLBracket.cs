namespace NewTOAPIA.GL
{
    public class GLBracketDrawing : IBracket
    {
        BeginMode fMode;
        GraphicsInterface fGI;

        //public GLBracketDrawing(BeginMode aMode, GraphicsInterface gi)
        //{
        //    fGI = gi;
        //    fMode = aMode;
        //}

        public GLBracketDrawing(GraphicsInterface gi, BeginMode aMode)
        {
            fGI = gi;
            fMode = aMode;
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public virtual void Begin()
        {
            GI.Begin(fMode);
        }

        public virtual void End()
        {
            GI.End();
        }
    }


    
    public class BracketTrianglesDrawing : GLBracketDrawing
    {
        public BracketTrianglesDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.Triangles)
        {
        }
    }
    
    public class BracketTriangleStripDrawing : GLBracketDrawing
    {
        public BracketTriangleStripDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.TriangleStrip)
        {
        }
    }
    
    public class BracketTriangleFanDrawing : GLBracketDrawing
    {
        public BracketTriangleFanDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.TriangleFan)
        {
        }
    }
    
    public class BracketQuadsDrawing : GLBracketDrawing
    {
        public BracketQuadsDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.Quads)
        {
        }
    }
    
    public class BracketQuadStripDrawing : GLBracketDrawing
    {
        public BracketQuadStripDrawing(GraphicsInterface gi)
            : base(gi, BeginMode.QuadStrip)
        {
        }
    }
    

}
