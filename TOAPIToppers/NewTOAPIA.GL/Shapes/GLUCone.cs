
namespace NewTOAPIA.GL
{

    public class GLUCone : GLUQuadric
    {
        double fBase;
        double fHeight;
        int fSlices;
        int fStacks;

        public GLUCone(double baseSize, double height, int slices, int stacks)
            : base(QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fBase = baseSize;
            fHeight = height;
            fSlices = slices;
            fStacks = stacks;
        }



        protected override void RenderContent(GraphicsInterface gi)
        {
            // Do the actual drawing
            gi.Glu.Cylinder(Handle, fBase, 0.0, fHeight, fSlices, fStacks);
        }
    }
}
