using System;

namespace NewTOAPIA.Modeling
{
    using NewTOAPIA.GL;

    public enum TorusDrawStyle
    {
        WireFrame,
        Solid
    }

    public class GLUTorus : GLRenderable
    {
        float fInnerRadius;
        float fOuterRadius;
        int fSides;
        int fRings;
        TorusDrawStyle fDrawingStyle;

        public GLUTorus(float innerRadius, float outerRadius, int sides, int rings, TorusDrawStyle drawStyle)
        {
            fInnerRadius = innerRadius;
            fOuterRadius = outerRadius;
            fSides = sides;
            fRings = rings;
            fDrawingStyle = drawStyle;
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            switch(fDrawingStyle)
            {
                case TorusDrawStyle.WireFrame:
                    gi.PushAttrib(AttribMask.PolygonBit);
                    gi.PolygonMode(GLFace.FrontAndBack, PolygonMode.Line);
                    break;
            }
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            int i, j;
            float theta, phi, theta1;
            float cosTheta, sinTheta;
            float cosTheta1, sinTheta1;
            float ringDelta, sideDelta;

            ringDelta = (float)(2.0f * Math.PI / fRings);
            sideDelta = (float)(2.0f * Math.PI / fSides);

            theta = 0.0f;
            cosTheta = 1.0f;
            sinTheta = 0.0f;

            for (i = fRings - 1; i >= 0; i--)
            {
                theta1 = theta + ringDelta;
                cosTheta1 = (float)Math.Cos(theta1);
                sinTheta1 = (float)Math.Sin(theta1);
                gi.Drawing.QuadStrip.Begin();

                phi = 0.0f;

                for (j = fSides; j >= 0; j--)
                {
                    float cosPhi, sinPhi, dist;

                    phi += sideDelta;
                    cosPhi = (float)Math.Cos(phi);
                    sinPhi = (float)Math.Sin(phi);
                    dist = fOuterRadius + fInnerRadius * cosPhi;

                    gi.Normal(cosTheta1 * cosPhi, -sinTheta1 * cosPhi, sinPhi);
                    gi.Vertex(cosTheta1 * dist, -sinTheta1 * dist, fInnerRadius * sinPhi);
                    gi.Normal(cosTheta * cosPhi, -sinTheta * cosPhi, sinPhi);
                    gi.Vertex(cosTheta * dist, -sinTheta * dist, fInnerRadius * sinPhi);
                }
                gi.Drawing.QuadStrip.End();
                theta = theta1;
                cosTheta = cosTheta1;
                sinTheta = sinTheta1;
            }
        }

        protected override void EndRender(GraphicsInterface gi)
        {
            switch(fDrawingStyle)
                {
                    case TorusDrawStyle.WireFrame:
                        gi.PopAttrib();
                        break;
                }
        }
    }
}
