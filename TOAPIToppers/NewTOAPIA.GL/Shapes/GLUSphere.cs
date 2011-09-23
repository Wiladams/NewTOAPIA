using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLUSphere : GLUQuadric
    {
        double fRadius;
        int fSlices;
        int fStacks;

        public GLUSphere(double radius, int slices, int stacks)
            :base(QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fRadius = radius;
            fSlices = slices;
            fStacks = stacks;
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
           glu.gluSphere(Handle, fRadius, fSlices, fStacks);
        }

        public override string ToString()
        {
            return string.Format("<GLUSphere radius='{0}', slices='{1}', stacks='{2}'/>",
                fRadius, fSlices, fStacks);
        }
    }
}
