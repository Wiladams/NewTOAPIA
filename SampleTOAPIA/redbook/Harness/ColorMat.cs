using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;
using NewTOAPIA.GLU.Shapes;

public class ColorMat : GLModel
{
    float [] diffuseMaterial = { 0.5f, 0.5f, 0.5f, 1.0f };
    float[] mat_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
    float4 light_position = new float4( 1.0f, 1.0f, 1.0f, 0.0f );

    GLUSphere sphere;

    protected override void OnSetContext()
    {
        GI.ShadeModel(ShadingModel.Smooth);
        GI.Features.DepthTest.Enable();
        
        // Definition of material
        GI.Material(GLFace.Front, MaterialParameter.Diffuse, diffuseMaterial);
        GI.Material(GLFace.Front, MaterialParameter.Specular, mat_specular);
        GI.Material(GLFace.Front, MaterialParameter.Shininess, 25.0f);

        // Lighting
        GI.Features.Lighting.Light0.Location = light_position;
        GI.Features.Lighting.Enable();
        GI.Features.Lighting.Light0.Enable();

        GI.ColorMaterial(GLFace.Front, ColorMaterialParameter.Diffuse);
        GI.Features.ColorMaterial.Enable();

        GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;

        sphere = new GLUSphere(GI, 1, 20, 16);
        sphere.DrawingStyle = QuadricDrawStyle.Fill;
    }

    protected override void DrawBegin()
    {
        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();
    
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();
    }

    protected override void DrawContent()
    {
        sphere.Render(GI);
    }

    protected override void DrawEnd()
    {
        GI.Flush();
    }

    public override void OnMouseActivity(object sender, NewTOAPIA.UI.MouseActivityArgs mea)
    {
        if (mea.ActivityType != MouseActivityType.MouseDown)
            return;

        switch (mea.ButtonActivity)
        {
            case MouseButtonActivity.LeftButtonDown:
                {
                    diffuseMaterial[0] += 0.1f;
                    if (diffuseMaterial[0] > 1.0)
                        diffuseMaterial[0] = 0.0f;
                    GI.Color4(diffuseMaterial);
                }
                break;
            case MouseButtonActivity.MiddleButtonDown:
                {
                    diffuseMaterial[1] += 0.1f;
                    if (diffuseMaterial[1] > 1.0)
                        diffuseMaterial[1] = 0.0f;
                    GI.Color4(diffuseMaterial);
                }
                break;
            case MouseButtonActivity.RightButtonDown:
                {
                    diffuseMaterial[2] += 0.1f;
                    if (diffuseMaterial[2] > 1.0)
                        diffuseMaterial[2] = 0.0f;
                    GI.Color4(diffuseMaterial);
                }
                break;
            default:
                break;
        }
    }

    public override void OnSetViewport(int w, int h)
    {
        GI.Viewport(0, 0, w, h);
        
        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        if (w <= h)
            GI.Ortho(-1.5, 1.5, -1.5 * (float)h / (float)w,
               1.5 * (float)h / (float)w, -10.0, 10.0);
        else
            GI.Ortho(-1.5 * w / (float)h,
               1.5 * (float)w / (float)h, -1.5, 1.5, -10.0, 10.0);
    }
}

