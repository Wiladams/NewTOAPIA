
namespace NewTOAPIA.GL
{

    public class GLUCylinder : GLUQuadric
    {
        double fBase;
        double fHeight;
        int fSlices;
        int fStacks;

        public GLUCylinder(double baseSize, double height, int slices, int stacks)
            : base(QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fBase = baseSize;
            fHeight = height;
            fSlices = slices;
            fStacks = stacks;
        }


        public double Height
        {
            get { return fHeight; }
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            // Do the actual drawing
            gi.Glu.Cylinder(Handle, fBase, fBase, fHeight, fSlices, fStacks);
        }
    }
}