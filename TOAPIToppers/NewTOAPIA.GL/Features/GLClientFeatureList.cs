
namespace NewTOAPIA.GL
{
    public class GLClientFeatureList
    {
        GraphicsInterface fGI;

        GLFeatureColorArray fColorArrayFeature;
        GLFeatureEdgeArray fEdgeArrayFeature;
        GLFeatureIndexArray fIndexArrayFeature;
        GLFeatureNormalArray fNormalArrayFeature;
        GLFeatureTextureCoordArray fTextureCoordArrayFeature;
        GLFeatureVertexArray fVertexArrayFeature;

        public GLClientFeatureList(GraphicsInterface gi)
        {
            fGI = gi;

            fColorArrayFeature = new GLFeatureColorArray(gi);
            fEdgeArrayFeature = new GLFeatureEdgeArray(gi);
            fIndexArrayFeature = new GLFeatureIndexArray(gi);
            fNormalArrayFeature = new GLFeatureNormalArray(gi);
            fTextureCoordArrayFeature = new GLFeatureTextureCoordArray(gi);
            fVertexArrayFeature = new GLFeatureVertexArray(gi);
        }

        public GLFeatureColorArray ColorArray
        {
            get { return fColorArrayFeature; }
        }

        public GLFeatureEdgeArray EdgeArray
        {
            get { return fEdgeArrayFeature; }
        }

        public GLFeatureIndexArray IndexArray
        {
            get { return fIndexArrayFeature; }
        }

        public GLFeatureNormalArray NormalArray
        {
            get { return fNormalArrayFeature; }
        }

        public GLFeatureTextureCoordArray TextureCoordArray
        {
            get { return fTextureCoordArrayFeature; }
        }

        public GLFeatureVertexArray VertexArray
        {
            get { return fVertexArrayFeature; }
        }
    }
}
