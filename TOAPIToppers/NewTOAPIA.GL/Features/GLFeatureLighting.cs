
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class GLFeatureLighting : GLFeature
    {
        GLLightModel fModel;
        AmbientLight fAmbientLight;
        GLLight fLight0;
        GLLight fLight1;
        GLLight fLight2;
        GLLight fLight3;
        GLLight fLight4;
        GLLight fLight5;
        GLLight fLight6;
        GLLight fLight7;
        GLState fState;

        public GLFeatureLighting(GraphicsInterface gi)
            :base(gi, GLOption.Lighting)
        {
            fState = new GLState(gi);

            fModel = new GLLightModel(gi);

            fAmbientLight = new AmbientLight(gi, ColorRGBA.White);

            fLight0 = new GLLight(gi, GLLightName.Light0);
            fLight1 = new GLLight(gi, GLLightName.Light1);
            fLight2 = new GLLight(gi, GLLightName.Light2);
            fLight3 = new GLLight(gi, GLLightName.Light3);
            fLight4 = new GLLight(gi, GLLightName.Light4);
            fLight5 = new GLLight(gi, GLLightName.Light5);
            fLight6 = new GLLight(gi, GLLightName.Light6);
            fLight7 = new GLLight(gi, GLLightName.Light7);
        }

        public int MaxLights
        {
            get { return fState.MaxLights; }
        }

        public AmbientLight AmbientLight
        {
            get { return fAmbientLight; }
        }

        public GLLight Light0
        {
            get { return fLight0; }
        }

        public GLLight Light1
        {
            get { return fLight1; }
        }

        public GLLight Light2
        {
            get { return fLight2; }
        }

        public GLLight Light3
        {
            get { return fLight3; }
        }

        public GLLight Light4
        {
            get { return fLight4; }
        }

        public GLLight Light5
        {
            get { return fLight5; }
        }

        public GLLight Light6
        {
            get { return fLight6; }
        }

        public GLLight Light7
        {
            get { return fLight7; }
        }


    }
}
