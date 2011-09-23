
namespace NewTOAPIA.GL
{
    public class GLClientStateFeature : IEnable
    {
        GraphicsInterface fGI;
        ClientArrayType fArrayType;

        public GLClientStateFeature(GraphicsInterface gi, ClientArrayType arrayType)
        {
            fGI = gi;
            fArrayType = arrayType;
        }

        protected GraphicsInterface GI
        {
            get { return fGI; }
        }

        public void Enable()
        {
            fGI.EnableClientState(fArrayType);
        }

        public void Disable()
        {
            fGI.DisableClientState(fArrayType);
        }
    }

    public class GLArrayFeature : GLClientStateFeature
    {
        object fArrayPointer;

        public GLArrayFeature(GraphicsInterface gi, ClientArrayType arrayType)
            : base(gi, arrayType)
        {
            fArrayPointer = null;
        }

        public object ArrayPointer
        {
            get { return fArrayPointer; }
            set
            {
                fArrayPointer = value;
                SetArrayPointer(fArrayPointer);
            }
        }

        public virtual void SetArrayPointer(object arrayPointer)
        {
        }
    }

    public class GLFeatureColorArray : GLArrayFeature
    {
        public GLFeatureColorArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.ColorArray)
        {
        }
    }

    public class GLFeatureEdgeArray : GLArrayFeature
    {
        public GLFeatureEdgeArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.EdgeFlagArray)
        {
        }
    }


    public class GLFeatureIndexArray : GLArrayFeature
    {
        public GLFeatureIndexArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.IndexArray)
        {
        }
    }

    public class GLFeatureNormalArray : GLArrayFeature
    {
        public GLFeatureNormalArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.NormalArray)
        {
        }
    }

    public class GLFeatureVertexArray : GLArrayFeature
    {
        public GLFeatureVertexArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.VertexArray)
        {
        }
    }

    public class GLFeatureTextureCoordArray : GLArrayFeature
    {
        public GLFeatureTextureCoordArray(GraphicsInterface gi)
            : base(gi, ClientArrayType.TextureCoordArray)
        {
        }
    }

}
