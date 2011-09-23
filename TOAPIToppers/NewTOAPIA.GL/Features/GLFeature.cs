

namespace NewTOAPIA.GL
{
    public class GLFeature : GLAspect, IEnable
    {
        bool fIsEnabled;
        GLOption fFeature;

        public GLFeature(GraphicsInterface gi, GLOption aFeature)
            :base(gi)
        {
            fFeature = aFeature;
            fIsEnabled = true;
        }

        public virtual void Enable()
        {
            fIsEnabled = true;
            GI.Enable(fFeature);

            Realize();
        }

        public virtual void Disable()
        {
            fIsEnabled = false;
            GI.Disable(fFeature);
            
            Realize();
        }

        public override void Realize()
        {
        }

        public override string ToString()
        {
            return string.Format("<GLFeature name='{0}', constant='{1}', enabled='{2}'/>",
                fFeature.ToString(), (int)fFeature, fIsEnabled);
        }
    }

    public class GLFeatureAlphaTest : GLFeature
    {
        public GLFeatureAlphaTest(GraphicsInterface gi)
            : base(gi, GLOption.AlphaTest)
        {
        }
    }

    public class GLFeatureAutoNormal : GLFeature
    {
        public GLFeatureAutoNormal(GraphicsInterface gi)
            : base(gi, GLOption.AutoNormal)
        {
        }
    }


    public class GLFeatureColorMaterial : GLFeature
    {
        public GLFeatureColorMaterial(GraphicsInterface gi)
            : base(gi, GLOption.ColorMaterial)
        {
        }
    }

    public class GLFeatureColorSum : GLFeature
    {
        public GLFeatureColorSum(GraphicsInterface gi)
            : base(gi, GLOption.ColorSum)
        {
        }
    }

    public class GLFeatureCullFace : GLFeature
    {
        public GLFeatureCullFace(GraphicsInterface gi)
            : base(gi, GLOption.CullFace)
        {
        }
    }

    public class GLFeatureDepthTest : GLFeature
    {
        public GLFeatureDepthTest(GraphicsInterface gi)
            : base(gi, GLOption.DepthTest)
        {
        }
    }

    public class GLFeatureDither : GLFeature
    {
        public GLFeatureDither(GraphicsInterface gi)
            : base(gi, GLOption.Dither)
        {
        }
    }


    public class GLFeatureMap1Vertex3 : GLFeature
    {
        public GLFeatureMap1Vertex3(GraphicsInterface gi)
            : base(gi, GLOption.Map1Vertex3)
        {
        }
    }

    public class GLFeatureMap2Vertex3 : GLFeature
    {
        public GLFeatureMap2Vertex3(GraphicsInterface gi)
            : base(gi, GLOption.Map2Vertex3)
        {
        }
    }

    public class GLFeatureMap2Vertex4 : GLFeature
    {
        public GLFeatureMap2Vertex4(GraphicsInterface gi)
            : base(gi, GLOption.Map2Vertex4)
        {
        }
    }


    public class GLFeaturePointSprite : GLFeature
    {
        public GLFeaturePointSprite(GraphicsInterface gi)
            : base(gi, GLOption.PointSprite)
        {
        }
    }

    public class GLFeatureTextureGenS : GLFeature
    {
        public GLFeatureTextureGenS(GraphicsInterface gi)
            : base(gi, GLOption.TextureGenS)
        {
        }
    }

    public class GLFeatureTextureGenT : GLFeature
    {
        public GLFeatureTextureGenT(GraphicsInterface gi)
            : base(gi, GLOption.TextureGenT)
        {
        }
    }
}
