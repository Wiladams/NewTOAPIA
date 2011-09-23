using System;

using NewTOAPIA;
using NewTOAPIA.Drawing;
using NewTOAPIA.GL;
using NewTOAPIA.Graphics;
using NewTOAPIA.UI;
using NewTOAPIA.UI.GL;

public class GearModel : GLModel
{

    float3 view_rot;

    float angle;

    float angle_step = 1.0f;
    float rotx_step = 0.8f;
    float roty_step = 1.0f;
    float rotz_step = 0.3f;

    private bool fRotateX = false;
    private bool fRotateY = false;
    private bool fRotateZ = false;

    int fViewportWidth;
    int fViewportHeight;

    WireframeGear gear1;
    WireframeGear gear2;
    WireframeGear gear3;

    public GearModel()
    {
    }

    protected override void OnSetContext()
    {
        GraphicsInterface.gCheckErrors = true;

        gear1 = new WireframeGear(GI, 1.0f, 4.05f, 1.0f, 20, 0.7f);
        gear2 = new WireframeGear(GI, 0.5f, 2.0f, 2.0f, 10, 0.7f);
        gear3 = new WireframeGear(GI, 1.4f, 2.0f, 0.5f, 10, 0.7f);

        
        GI.Features.DepthTest.Enable();
        GI.Color(ColorRGBA.Cyan);
    }

    protected override void DrawBegin()
    {
        float h = (float)fViewportHeight / fViewportWidth;
        GI.Viewport(0, 0, fViewportWidth, fViewportHeight);

        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        GI.Frustum(-1.0f, 1.0f, -h, h, 5.0f, 60.0f);

        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();
        GI.Translate(0.0f, 0.0f, -40.0f);

        // Clear the rendering window
        GI.Buffers.ColorBuffer.Color = ColorRGBA.Black;
        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();
    }

    protected override void DrawContent()
    {
        GI.PushMatrix();

        GI.Rotate(view_rot.x, 1.0f, 0.0f, 0.0f);
        GI.Rotate(view_rot.y, 0.0f, 1.0f, 0.0f);
        GI.Rotate(view_rot.z, 0.0f, 0.0f, 1.0f);

        // Draw red gear
        GI.Color(ColorRGBA.Red);
        GI.PushMatrix();
            GI.Translate(-3.0f, -2.0f, 0.0f);
            GI.Rotate(angle, 0.0f, 0.0f, 1.0f);
            gear1.Render(GI);
        GI.PopMatrix();

        // Draw green gear
        GI.Color(ColorRGBA.Green);
        GI.PushMatrix();
            GI.Translate(3.1f, -2.0f, 0.0f);
            GI.Rotate(-2.0f * angle - 9.0f, 0.0f, 0.0f, 1.0f);
            gear2.Render(GI);
        GI.PopMatrix();

        // Draw blue gear
        GI.Color(ColorRGBA.Blue);
        GI.PushMatrix();
            GI.Translate(-3.1f, 4.2f, 0.0f);
            GI.Rotate(-2.0f * angle - 25.0f, 0.0f, 0.0f, 1.0f);
            gear3.Render(GI);
        GI.PopMatrix();

        GI.PopMatrix();

        // do rotations around axis
        angle += angle_step;
        if (fRotateX)
            view_rot.x += rotx_step;
        if (fRotateY)
            view_rot.y += roty_step;
        if (fRotateZ)
            view_rot.z += rotz_step;

    }

    public override void OnSetViewport(int width, int height)
    {

        fViewportHeight = height;
        fViewportWidth = width;

    }

    public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
    {
        if (kbde.AcitivityType == KeyActivityType.KeyUp)
        {
            switch (kbde.VirtualKeyCode)
            {
                case VirtualKeyCodes.X:
                    fRotateX = !fRotateX;
                    break;
                case VirtualKeyCodes.Y:
                    fRotateY = !fRotateY;
                    break;
                case VirtualKeyCodes.Z:
                    fRotateZ = !fRotateZ;
                    break;
            }
        }

        return IntPtr.Zero;
    }

}