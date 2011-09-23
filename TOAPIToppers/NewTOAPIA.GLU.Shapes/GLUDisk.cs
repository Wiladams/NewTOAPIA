using System;

namespace NewTOAPIA.GLU.Shapes
{
    using NewTOAPIA.GL;

    public class GLUDisk : GLUQuadric
    {
        double fInnerRadius;
        double fOuterRadius;
        int fSlices;
        int fLoops;


        public GLUDisk(GraphicsInterface gi, double innerradius, double outerradius, int slices, int loops)
            :base(gi, QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fInnerRadius = innerradius;
            fOuterRadius = outerradius;
            fSlices = slices;
            fLoops = loops;
        }



        protected override void RenderContent(GraphicsInterface gi)
        {
            gi.Glu.Disk(Handle, fInnerRadius, fOuterRadius, fSlices, fLoops);
        }
    }
}
