
using System;

using TOAPI.OpenGL;
using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.UI;
//using NewTOAPIA.Drawing;
using NewTOAPIA.GL;

public class Varray : GLModel
{
    public const int POINTER = 1;
    public const int INTERLEAVED = 2;

    public const int DRAWARRAY = 1;
    public const int ARRAYELEMENT = 2;
    public const int DRAWELEMENTS = 3;

    int setupMethod = POINTER;
    int derefMethod = DRAWARRAY;

    int[,] vertices = {{25, 25},
                               {100, 325},
                               {175, 25},
                                {175, 325},
                               {250, 25},
                               {325, 325}};
    float[,] colors = {{1.0f, 0.2f, 0.2f},
                               {0.2f, 0.2f, 1.0f},
                               {0.8f, 1.0f, 0.2f},
                               {0.75f, 0.75f, 0.75f},
                               {0.35f, 0.35f, 0.35f},
                               {0.5f, 0.5f, 0.5f}};

    float[] intertwined = new float[]
              {1.0f, 0.2f, 1.0f, 100.0f, 100.0f, 0.0f,
               1.0f, 0.2f, 0.2f, 0.0f, 200.0f, 0.0f,
               1.0f, 1.0f, 0.2f, 100.0f, 300.0f, 0.0f,
               0.2f, 1.0f, 0.2f, 200.0f, 300.0f, 0.0f,
               0.2f, 1.0f, 1.0f, 300.0f, 200.0f, 0.0f,
               0.2f, 0.2f, 1.0f, 200.0f, 100.0f, 0.0f};


    public Varray()
        : base()
    {
    }

    void setupInterleave()
    {
        GI.InterleavedArrays(InterleavedArrays.C3fV3f, 0, intertwined);
    }

    void setupPointers()
    {
        GI.ClientFeatures.VertexArray.Enable();
        GI.ClientFeatures.ColorArray.Enable();

        GI.VertexPointer(2, VertexPointerType.Int, 0, vertices);
        GI.ColorPointer(3, ColorPointerType.Float, 0, colors);
    }

    protected override void DrawBegin()
    {
        GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
        GI.ShadeModel(ShadingModel.Smooth);
        setupPointers();

        GI.Buffers.ColorBuffer.Clear();
    }

    protected override void DrawContent()
    {
        if (derefMethod == DRAWARRAY)
            GI.DrawArrays(BeginMode.Triangles, 0, 6);
        else if (derefMethod == ARRAYELEMENT)
        {
            GI.Drawing.Triangles.Begin();
            GI.ArrayElement(2);
            GI.ArrayElement(3);
            GI.ArrayElement(5);
            GI.Drawing.Triangles.End();
        }
        else if (derefMethod == DRAWELEMENTS)
        {
            uint[] indices = new uint[] { 0, 1, 3, 4 };

            GI.DrawElements(BeginMode.Polygon, 4, DrawElementType.UnsignedInt, indices);
        }
    }

    protected override void DrawEnd()
    {
        GI.Flush();
    }

    public override void OnSetViewport( int w, int h)
    {
        GI.Viewport(0, 0, w, h);

        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        glu.gluOrtho2D(0.0, (double)w, 0.0, (double)h);
    }

    public override void OnMouseActivity(Object sender, MouseActivityArgs mea)
    {
        switch (mea.ButtonActivity)
        {
            case MouseButtonActivity.LeftButtonDown:
                if (setupMethod == POINTER)
                {
                    setupMethod = INTERLEAVED;
                    setupInterleave();
                }
                else if (setupMethod == INTERLEAVED)
                {
                    setupMethod = POINTER;
                    setupPointers();
                }
                break;

            case MouseButtonActivity.MiddleButtonDown:
            case MouseButtonActivity.RightButtonDown:
                if (derefMethod == DRAWARRAY)
                    derefMethod = ARRAYELEMENT;
                else if (derefMethod == ARRAYELEMENT)
                    derefMethod = DRAWELEMENTS;
                else if (derefMethod == DRAWELEMENTS)
                    derefMethod = DRAWARRAY;
                break;

            default:
                break;
        }
    }
}

	