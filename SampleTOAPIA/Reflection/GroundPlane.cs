

namespace Reflection
{
    using NewTOAPIA.GL;
    using NewTOAPIA.Modeling;

    class GroundPlane : GLRenderable
    {
        float fExtent = 20.0f;
        float fStep = 0.5f;
        float y = 0.0f;
        float fColor;
        float iStrip, iRun;
        int iBounce = 0;

        public GroundPlane(float extent, float step, float color)
        {
            fExtent = extent;
            fStep = step;
            fColor = color;
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            gi.ShadeModel(ShadingModel.Flat);
            iBounce = 0;
        }

        protected override void RenderContent(GraphicsInterface GI)
        {
            for (iStrip = -fExtent; iStrip <= fExtent; iStrip += fStep)
            {
                GI.Drawing.TriangleStrip.Begin();
                for (iRun = fExtent; iRun >= -fExtent; iRun -= fStep)
                {
                    if ((iBounce % 2) == 0)
                        fColor = 1.0f;
                    else
                        fColor = 0.0f;

                    GI.Color(fColor, fColor, fColor, 0.75f);
                    GI.Vertex(iStrip, y, iRun);
                    GI.Vertex(iStrip + fStep, y, iRun);

                    iBounce++;
                }
                GI.Drawing.TriangleStrip.End();
            }
            GI.ShadeModel(ShadingModel.Smooth);
        }
    }
}
