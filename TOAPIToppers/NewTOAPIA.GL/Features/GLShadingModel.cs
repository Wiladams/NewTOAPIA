
namespace NewTOAPIA.GL
{
    public class GLShadingModel : GLAspect
    {
        // A couple of handy instances
        //public static GLShadingModel Flat = new GLShadingModel(ShadingModel.Flat);
        //public static GLShadingModel Smooth = new GLShadingModel(ShadingModel.Smooth);

        ShadingModel fShadingModel;

        public GLShadingModel(GraphicsInterface gi, ShadingModel aModel)
            :base(gi)
        {
            fShadingModel = aModel;
        }

        public ShadingModel Model
        {
            get { return fShadingModel; }
            set
            {
                SetModel(value);
            }
        }

        public virtual void SetModel(ShadingModel aModel)
        {
            fShadingModel = aModel;
            Realize();
        }

        public override void Realize()
        {
            GL.ShadeModel(fShadingModel);
        }
    }

    //public class FlatShadingModel : GLShadingModel
    //{
    //    public FlatShadingModel()
    //        : base(ShadingModel.Flat)
    //    {
    //    }
    //}

    //public class SmoothShadingModel : GLShadingModel
    //{
    //    public SmoothShadingModel()
    //        : base(ShadingModel.Smooth)
    //    {
    //    }
    //}
}
