using System;

//using TOAPI.Types;
using TOAPI.OpenGL;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;

public class BezMesh : GLModel
{
    // Originally a 4x4x3 matrix
    private static float[/* 4*4*3 */] controlPoints = {
            -1.5f, -1.5f,  4.0f,
            -0.5f, -1.5f,  2.0f,
             0.5f, -1.5f, -1.0f,
             1.5f, -1.5f,  2.0f,
        
            -1.5f, -0.5f,  1.0f,
            -0.5f, -0.5f,  3.0f,
             0.5f, -0.5f,  0.0f,
             1.5f, -0.5f, -1.0f,
        
            -1.5f, 0.5f, 4.0f,
            -0.5f, 0.5f, 0.0f,
             0.5f, 0.5f, 3.0f,
             1.5f, 0.5f, 4.0f,
        
            -1.5f, 1.5f, -2.0f,
            -0.5f, 1.5f, -2.0f,
             0.5f, 1.5f,  0.0f,
             1.5f, 1.5f, -1.0f
        };

    GLMaterial fMaterial;


    protected override void OnSetContext()
    {
        //GLMaterial material = new GLMaterial( 
        //    new float4(0.6f, 0.6f, 0.6f, 1.0f), 
        //    new float4(1.0f, 1.0f, 1.0f, 1.0f), 50.0f);
        fMaterial = new GLMaterial(new ColorRGBA(0.6f, 0.6f, 0.6f, 1.0f), new ColorRGBA(1.0f, 1.0f, 1.0f, 1.0f),
            new ColorRGBA(0.6f, 0.6f, 0.6f, 1.0f), ColorRGBA.Invisible, 50.0f);

        GI.Features.Lighting.AmbientLight.Color = new ColorRGBA(0.5f,0.5f,0.5f,1.0f);
        GI.Features.Lighting.Light0.Location = new float4(0.0f, 0.0f, 2.0f, 1.0f);
        GI.Features.Lighting.Light0.SetAmbientDiffuse(ColorRGBA.MediumBlue, new ColorRGBA(0.7f, 0.2f, 0.2f, 1.0f));
        GI.Features.Lighting.Light0.Enable();
        GI.Features.Lighting.Enable();

        GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
        GI.Features.DepthTest.Enable();

        GI.ShadeModel(ShadingModel.Smooth);
    }

    protected override void DrawBegin()
    {
        GI.Viewport(0, 0, ViewportWidth, ViewportHeight);
        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        if (ViewportWidth <= ViewportHeight)
        {
            GI.Ortho(-4.0, 4.0, -4.0 * ViewportHeight / ViewportWidth, 4.0 * ViewportHeight / ViewportWidth, -4.0, 4.0);
        }
        else
        {
            GI.Ortho(-4.0 * ViewportWidth / ViewportHeight, 4.0 * ViewportWidth / ViewportHeight, -4.0, 4.0, -4.0, 4.0);
        }
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();


        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();

        GI.Map2(MapTarget.Map2Vertex3, 0.0f, 1.0f, 3, 4, 0.0f, 1.0f, 12, 4, controlPoints);
        GI.Features.Map2Vertex3.Enable();
        GI.Features.AutoNormal.Enable();
        GI.MapGrid2(20, 0.0f, 1.0f, 20, 0.0f, 1.0f);
    }

    protected override void DrawContent()
    {
        GI.PushMatrix();
        GI.Rotate(85.0f, 1.0f, 1.0f, 1.0f);
        fMaterial.RealizeFace(GLFace.Front);
        GI.EvalMesh2(MeshMode2.Fill, 0, 20, 0, 20);
        GI.PopMatrix();
        
        GI.Flush();
    }
}