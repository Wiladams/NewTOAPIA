
namespace NewTOAPIA.GL
{
    public class Texturing2D : GLFeature
    {
        GLFeatureTextureGenS fTextureGenS;
        GLFeatureTextureGenT fTextureGenT;

        public Texturing2D(GraphicsInterface gi)
            : base(gi, GLOption.Texture2d)
        {
            fTextureGenS = new GLFeatureTextureGenS(gi);
            fTextureGenT = new GLFeatureTextureGenT(gi);
        }

        public GLFeatureTextureGenS TextureGenS
        {
            get { return fTextureGenS; }
        }

        public GLFeatureTextureGenT TextureGenT
        {
            get { return fTextureGenT; }
        }


    }
}
