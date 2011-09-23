using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    public class GLUDisk : GLUQuadric
    {
        double fInnerRadius;
        double fOuterRadius;
        int fSlices;
        int fLoops;


        public GLUDisk(double innerradius, double outerradius, int slices, int loops)
            :base(QuadricDrawStyle.Fill, QuadricNormalType.Smooth, QuadricOrientation.Outside)
        {
            fInnerRadius = innerradius;
            fOuterRadius = outerradius;
            fSlices = slices;
            fLoops = loops;
        }



        protected override void RenderContent(GraphicsInterface gi)
        {
            glu.gluDisk(Handle, fInnerRadius, fOuterRadius, fSlices, fLoops);
        }
    }
}
