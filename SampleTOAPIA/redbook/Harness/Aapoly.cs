
using System;

//using TOAPI.OpenGL;
using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.Modeling;
using NewTOAPIA.UI;
using NewTOAPIA.GL;

public class Aapoly : GLModel
{
    private const int FACES = 6;

    private static bool polySmooth = true;

    //private static float[/*8*/, /*3*/] v = new float [8, 3];
    private static float[/*8*/, /*4*/] c = {
        {0.0f, 0.0f, 0.0f, 1.0f},
        {1.0f, 0.0f, 0.0f, 1.0f},
        {0.0f, 1.0f, 0.0f, 1.0f},
        {1.0f, 1.0f, 0.0f, 1.0f},
        {0.0f, 0.0f, 1.0f, 1.0f},
        {1.0f, 0.0f, 1.0f, 1.0f},
        {0.0f, 1.0f, 1.0f, 1.0f},
        {1.0f, 1.0f, 1.0f, 1.0f}
    };

    // indices of front, top, left, bottom, right, back faces
    //private static byte[/*6*/, /*4*/] indices = {
    //    {4, 5, 6, 7},
    //    {2, 3, 7, 6},
    //    {0, 4, 7, 3},
    //    {0, 1, 5, 4},
    //    {1, 5, 6, 2},
    //    {0, 3, 2, 1}
    //};

    GLUCube fCube;


    public Aapoly()
        : base()
    {
    }

    protected override void OnSetContext()
    {
        //fCube = new GLUCube(-0.5f, 0.5f, -0.5f, 0.5f, -0.5f, 0.5f);
        fCube = new GLUCube(1, CubeStyle.Solid);
    }

    private void DrawCube()
    {
        // Draw in color
        GI.ClientFeatures.ColorArray.Enable();
        GI.ColorPointer(4, ColorPointerType.Float, 0, c);
        
        //GLPolygonFillMode fillMode = new GLPolygonFillMode(PolygonMode.Line, PolygonFace.FrontAndBack);
        //fillMode.Realize();
        fCube.Render(GI);

        GI.ClientFeatures.ColorArray.Disable();
    }

    protected override void DrawBegin()
    {
        GI.CullFace(GLFace.Back);
        GI.Features.CullFace.Enable();
        GI.BlendFunc(BlendingFactorSrc.SrcAlphaSaturate, BlendingFactorDest.One);
        GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
    }

    protected override void DrawContent()
    {
            if(polySmooth) {
                GI.Buffers.ColorBuffer.Clear();
                GI.Features.Blend.Enable();
                GI.Features.PolygonSmooth.Enable();
                GI.Features.DepthTest.Disable();
            }
            else {
                GI.Buffers.ColorBuffer.Clear();
                GI.Buffers.DepthBuffer.Clear();

                GI.Features.Blend.Disable();
                GI.Features.PolygonSmooth.Disable();
                GI.Features.DepthTest.Enable();
            }

        GI.PushMatrix();
            GI.Translate(0.0f, 0.0f, -8.0f);    
            GI.Rotate(30.0f, 1.0f, 0.0f, 0.0f);
            GI.Rotate(60.0f, 0.0f, 1.0f, 0.0f); 
            DrawCube();
        GI.PopMatrix();

    }


    public override void OnSetViewport( int w, int h)
    {
        GI.Viewport(0, 0, w, h);
        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();

        GI.Glu.Perspective(30.0, (float) w / (float) h, 1.0, 20.0);
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();
    }

    public override IntPtr  OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
    {
 	    switch(kbde.VirtualKeyCode)
        {
            case VirtualKeyCodes.T:
                polySmooth = !polySmooth;
                break;
        }

        return IntPtr.Zero;
    }
}