using System;

using TOAPI.Types;
using TOAPI.OpenGL;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;


public class BezCurve : GLModel
{
    // Originally a 4x3 matrix
    float []ctrlpoints = {-4.0f, -4.0f, 0.0f, 
                          -2.0f, 4.0f, 0.0f, 
                           2.0f, -4.0f, 0.0f, 
                           4.0f, 4.0f, 0.0f};


    public BezCurve()
    {
    }

    protected override void OnSetContext()
    {
        GI.Features.Lighting.Disable();
    }

    protected override void DrawBegin()
    {
        int w = ViewportWidth;
        int h = ViewportHeight;

        GI.Viewport(0, 0, w, h);

        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();

        if (w <= h)
        {
            gl.glOrtho(-5.0f, 5.0f, -5.0f * (float)h / (float)w,
                5.0f * (float)h / (float)w, -5.0f, 5.0f);
        } else
        {
            gl.glOrtho(-5.0f*(float)w/(float)h, 
                5.0f * (float)w/(float)h, -5.0f, 5.0f,-5.0f, 5.0f);
        }

        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();



        GI.ShadeModel(ShadingModel.Flat);
        GI.Map1(MapTarget.Map1Vertex3, 0.0f, 1.0f, 3, 4, ctrlpoints);
        GI.Features.Map1Vertex3.Enable();

        // Clear buffers
        GI.Buffers.ColorBuffer.Color = ColorRGBA.Black;
        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();
    }

    protected override void DrawContent()
    {
        int i;

        GI.Color(ColorRGBA.White);

        GI.Drawing.LineStrip.Begin();
            for (i = 0; i <= 30; i++) 
                GI.EvalCoord1((float) i/30.0f);
        GI.Drawing.LineStrip.End();
   
        /* The following code displays the control points as dots. */
        GI.Drawing.Points.PointSize = 5.0f;
        GI.Drawing.Points.Smoothing.Enable();
        GI.Color(new ColorRGBA(1.0f, 1.0f, 0.0f,1.0f));
        GI.Drawing.Points.Begin();
        for (i = 0; i < 4; i++)
            GI.Vertex(ctrlpoints[3*i], ctrlpoints[3*i + 1], ctrlpoints[3*i + 2]);
        GI.Drawing.Points.End();
    }

    protected override void DrawEnd()
    {
        GI.Flush();
    }
}