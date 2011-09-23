
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLLight : GLFeature // , IPlaceable4f
    {
        protected GLLightName fLightName;

        Point3D fPosition;

        ColorRGBA fAmbient;
        ColorRGBA fDiffuse;
        ColorRGBA fSpecular;
        
        // attenuation
        GLLightAttenuation fLightAttenuation;

        public GLLight(GraphicsInterface gi, GLLightName aName)
            :this(gi, aName, default(Point3D), ColorRGBA.White)
        {
        }

        public GLLight(GraphicsInterface gi, GLLightName aName, Point3D position, ColorRGBA aColor)
            :base(gi, (GLOption)aName)
        {
            fLightName = aName;
            fPosition = position;

            fAmbient = aColor;
            fDiffuse = aColor;
            fSpecular = aColor;

            fLightAttenuation = GLLightAttenuation.GetDefault();
        }

        public virtual void SetAmbientDiffuse(ColorRGBA ambient, ColorRGBA diffuse)
        {
            Ambient = ambient;
            Diffuse = diffuse;
        }

        public virtual void SetColors(ColorRGBA ambient, ColorRGBA diffuse, ColorRGBA specular)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
        }

        public ColorRGBA Ambient
        {
            get { return fAmbient; }
            set
            {
                SetAmbient(value);
            }
        }

        public void SetAmbient(ColorRGBA ambient)
        {
            fAmbient = ambient;
            GI.Light(fLightName, LightParameter.Ambient, (float[])fAmbient);
        }

        public ColorRGBA Diffuse
        {
            get { return fDiffuse; }
            set
            {
                SetDiffuse(value);
            }
        }

        public void SetDiffuse(ColorRGBA diffuse)
        {
            fDiffuse = diffuse;
            GI.Light(fLightName, LightParameter.Diffuse, (float[])fDiffuse);
        }

        public ColorRGBA Specular
        {
            get { return fSpecular; }
            set
            {
                SetSpecular(value);
            }
        }

        public void SetSpecular(ColorRGBA specular)
        {
            fSpecular = specular;
            GI.Light(fLightName, LightParameter.Specular, (float[])fSpecular);
        }

        public virtual Point3D Location
        {
            get { return fPosition; }
            set
            {
                SetPosition(value);
            }
        }

        public virtual void SetPosition(Point3D aPosition)
        {
            fPosition = aPosition;
            GI.Light(fLightName, LightParameter.Position, fPosition);
        }

        public virtual GLLightAttenuation Attenuation
        {
            get
            {
                return fLightAttenuation;
            }

            set
            {
                fLightAttenuation = value;
                Realize();
            }
        }


        public override void Realize()
        {
            GI.Light(fLightName, LightParameter.Position, fPosition);

            // All lights
            GI.Light(fLightName, LightParameter.Ambient, (float[])fAmbient);
            GI.Light(fLightName, LightParameter.Specular, (float[])fSpecular);
            GI.Light(fLightName, LightParameter.Diffuse, (float[])fDiffuse);

            // Attenuation and spotlight are effects.  They could be optionally
            // set, and there may be more effects.
            // A better structure would be to have a list of effects, since they
            // all inherit from the 'IAffectLight' interface.
            // Then it is simply a matter of enumerating the list and realizing
            // each of the effects.
            // Attenuation
            //fLightAttenuation.RealizeForLight(fLightName);
        
            // Spotlight control
            //fSpotEffect.RealizeForLight(fLightName);
        }
    }
}
