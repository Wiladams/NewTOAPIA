using System;

namespace NewTOAPIA.GLU.Shapes
{
    using NewTOAPIA.GL;
    using NewTOAPIA.Modeling;

    public class GLUSphere : GLUQuadric
    {
        double fRadius;
        int fSlices;
        int fStacks;

        public GLUSphere(GraphicsInterface gi, double radius, int slices, int stacks)
            :base(gi, QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fRadius = radius;
            fSlices = slices;
            fStacks = stacks;
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
           gi.Glu.Sphere(Handle, fRadius, fSlices, fStacks);
        }

        public override string ToString()
        {
            return string.Format("<GLUSphere radius='{0}', slices='{1}', stacks='{2}'/>",
                fRadius, fSlices, fStacks);
        }
    }
}
