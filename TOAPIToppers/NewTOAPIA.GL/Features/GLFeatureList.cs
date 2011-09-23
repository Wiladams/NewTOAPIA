
namespace NewTOAPIA.GL
{
    /// <summary>
    /// The list of features that are in the Graphics Interface.
    /// Features can be enabled and disabled.  They may contain hints, and other related
    /// aspects.
    /// </summary>
    public class GLFeatureList
    {
        GraphicsInterface fGI;

        GLFeatureAlphaTest fAlphaTest;
        GLFeatureAutoNormal fAutoNormal;
        GLFeatureBlend fBlend;
        GLFeatureColorMaterial fColorMaterial;
        GLFeatureColorSum fColorSum;
        GLFeatureCullFace fCullFace;
        GLFeatureDepthTest fDepthTest;
        GLFeatureDither fDither;
        GLFeatureFog fFog;
        GLFeatureLighting fLighting;
        GLFeatureLineSmooth fLineSmooth;
        GLFeatureMap1Vertex3 fMap1Vertex3;
        GLFeatureMap2Vertex3 fMap2Vertex3;
        GLFeaturePointSmooth fPointSmooth;
        GLFeaturePointSprite fPointSprite;
        GLFeaturePolygonSmooth fPolygonSmooth;
        Texturing2D fTexturing2D;

        public GLFeatureList(GraphicsInterface gi)
        {
            fGI = gi;

            fAlphaTest = new GLFeatureAlphaTest(gi);
            fAutoNormal = new GLFeatureAutoNormal(gi);
            fBlend = new GLFeatureBlend(gi);
            fColorMaterial = new GLFeatureColorMaterial(gi);
            fColorSum = new GLFeatureColorSum(gi);
            fCullFace = new GLFeatureCullFace(gi);
            fDepthTest = new GLFeatureDepthTest(gi);
            fDither = new GLFeatureDither(gi);
            fFog = new GLFeatureFog(gi);
            fLighting = new GLFeatureLighting(gi);
            fLineSmooth = new GLFeatureLineSmooth(gi);
            fMap1Vertex3 = new GLFeatureMap1Vertex3(gi);
            fMap2Vertex3 = new GLFeatureMap2Vertex3(gi);
            fPointSmooth = new GLFeaturePointSmooth(gi);
            fPolygonSmooth = new GLFeaturePolygonSmooth(gi);
            fPointSprite = new GLFeaturePointSprite(gi);
            fTexturing2D = new Texturing2D(gi);
        }

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }
        #endregion

        public GLFeatureAlphaTest AlphaTest
        {
            get { return fAlphaTest; }
        }

        public GLFeatureAutoNormal AutoNormal
        {
            get { return fAutoNormal; }
            set
            {
                fAutoNormal = value;
            }
        }

        public GLFeatureBlend Blend
        {
            get { return fBlend; }
            set
            {
                fBlend = value;
            }
        }

        public GLFeatureColorMaterial ColorMaterial
        {
            get { return fColorMaterial; }
        }

        public GLFeatureColorSum ColorSum
        {
            get { return fColorSum; }
            set
            {
                fColorSum = value;
            }
        }

        public GLFeatureCullFace CullFace
        {
            get { return fCullFace; }
            set
            {
                fCullFace = value;
            }
        }

        public GLFeatureDepthTest DepthTest
        {
            get { return fDepthTest; }
            set
            {
                fDepthTest = value;
            }
        }

        public GLFeatureDither Dither
        {
            get { return fDither; }
            set
            {
                fDither = value;
            }
        }

        public GLFeatureFog Fog
        {
            get { return fFog; }
            set
            {
                fFog = value;
            }
        }

        public GLFeatureLighting Lighting
        {
            get { return fLighting; }
            set
            {
                fLighting = value;
            }
        }

        public GLFeatureLineSmooth LineSmooth
        {
            get { return fLineSmooth; }
            set
            {
                fLineSmooth = value;
            }
        }

        public GLFeatureMap1Vertex3 Map1Vertex3
        {
            get { return fMap1Vertex3; }
            set
            {
                fMap1Vertex3 = value;
            }
        }

        public GLFeatureMap2Vertex3 Map2Vertex3
        {
            get { return fMap2Vertex3; }
            set
            {
                fMap2Vertex3 = value;
            }
        }

        public GLFeaturePointSmooth PointSmooth
        {
            get { return fPointSmooth; }
            set
            {
                fPointSmooth = value;
            }
        }

        public GLFeaturePointSprite PointSprite
        {
            get { return fPointSprite; }
            set
            {
                fPointSprite = value;
            }
        }

        public GLFeaturePolygonSmooth PolygonSmooth
        {
            get { return fPolygonSmooth; }
            set
            {
                fPolygonSmooth = value;
            }
        }

        public Texturing2D Texturing2D
        {
            get { return fTexturing2D; }
            set
            {
                fTexturing2D = value;
            }
        }
    }


}
