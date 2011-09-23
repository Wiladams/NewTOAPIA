
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;

using TOAPI.OpenGL;
using TOAPI.Types;

public enum DrawModes
{
    CREATE_LIST,
    DRAW_LIST
}


public class FlagScene : GLModel
{
    public long nTicks = 0;
    public long nLoops = 0;

    private const float timeStep = 0.01f;

    private const int nWidth = 40;
    private const int nHeight = 40;
    private const float size = 1.0f / 8.0f;

    private float angRotY = 30.0f;

    GLTexture flagTexture;
    GLTexture fPictureTexture;
    private particles bandera = new particles();
    FlagPole flagpole;


    private bool bUseTexture;
    PolygonMode polyFillMode; 

    // Some light values
    float4 positionLight = new float4(-6.0f, 5.0f, 0.0f, 1.0f );
    ColorRGBA ambientLight = new ColorRGBA(0.4f, 0.4f, 0.4f, 1.0f);
    ColorRGBA diffuseLight = new ColorRGBA(0.7f, 0.7f, 0.7f, 1.0f);

    #region Constructor
    public FlagScene()
    {
        polyFillMode = PolygonMode.Fill;
        bUseTexture = true;
    }
    #endregion

    protected override void OnSetContext()
    {
        GI.ShadeModel(ShadingModel.Smooth);
        GI.Hint(HintTarget.PolygonSmoothHint, HintMode.DontCare);
        GI.Buffers.ColorBuffer.Color = new ColorRGBA(0, 0, 0, 1);
        GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Line);
        
        GI.Features.Lighting.Enable();
        GI.Features.Lighting.Light0.SetAmbientDiffuse(ambientLight, diffuseLight);
        GI.Features.Lighting.Light0.Location = positionLight;
        //GLC.Features.Lighting.Light0.Location = positionLight;
        GI.Features.Lighting.Light0.Enable();

        GI.Features.ColorMaterial.Enable();
        GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);

        GI.Features.DepthTest.Enable();


        bandera.nWidth = nWidth;
        bandera.nHeight = nHeight;
        bandera.size = size;
        bandera.uTile = 1;
        bandera.vTile = 1;

        bandera.ini(timeStep);

        flagpole = new FlagPole(bandera.x0, bandera.y0, bandera.z0, bandera.nHeight, size);

        SetupTexture();
    }

    public void SetupTexture()
    {
        //fPictureTexture = TextureHelper.CreateTextureFromFile(GI, "bara.gif", false);
        fPictureTexture = TextureHelper.CreateTextureFromFile(GI, "EELogo.jpg", false);
        //fPictureTexture = TextureHelper.CreateTextureFromFile(GI, "EEIndia.jpg", false);

        flagTexture = fPictureTexture;
    }

    protected override void DrawContent()
    {
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();

        GI.Translate(0, 0, -12);
        GI.Rotate(30, 1, 0, 0);
        GI.Rotate(angRotY, 0, 1, 0);

        GI.Features.Lighting.Light0.Location = positionLight;

        GI.Features.Texturing2D.Disable();
        flagpole.Render(this.GI);

        bandera.MainLoop();

        if (bUseTexture)
        {
            GI.Features.Texturing2D.Enable();
            flagTexture.Bind();
        }

        GI.PolygonMode(GLFace.FrontAndBack, polyFillMode);
        bandera.Render(GI);

    }



    public override void OnSetViewport(int width, int height)
    {
        GI.Viewport(0, 0, width, height);

        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        GI.Glu.Perspective(45.0f, (float)width / (float)height, 0.1f, 500.0f);

    }

    public override void OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
    {
        if (kbde.EventType == KeyEventType.KeyDown)
        {
            switch (kbde.VirtualKeyCode)
            {
                case VirtualKeyCodes.Left:
                case VirtualKeyCodes.A:
                    angRotY += 1.0f;
                    break;

                case VirtualKeyCodes.Right:
                case VirtualKeyCodes.S:
                    angRotY -= 1.0f;
                    break;
            }
        }

        if (kbde.EventType == KeyEventType.KeyUp)
        {
            switch (kbde.VirtualKeyCode)
            {
                //case VirtualKeyCodes.Space:
                //    if (flagTexture == fVideoTexture)
                //        flagTexture = fPictureTexture;
                //    else
                //        flagTexture = fVideoTexture;
                //    break;

                case VirtualKeyCodes.F:
                    polyFillMode = PolygonMode.Fill;
                    break;
                case VirtualKeyCodes.L:
                    polyFillMode = PolygonMode.Line;
                    break;
                case VirtualKeyCodes.W:
                    bandera.bWind = !bandera.bWind;
                    break;
                case VirtualKeyCodes.T:
                    bUseTexture = !bUseTexture;
                    break;
            }
        }
    }
}