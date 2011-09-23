
using System;

using NewTOAPIA.GL;
using NewTOAPIA.Graphics;
using NewTOAPIA.Media.GL;
using NewTOAPIA.UI;
using NewTOAPIA.UI.GL;



namespace Cloth
{
    public class FlagScene : GLModel
    {
        public long nTicks = 0;
        public long nLoops = 0;

        private const float timeStep = 0.01f;

        private const int nWidth = 40;
        private const int nHeight = 40;
        Resolution fFlagResolution;
        private const float size = 1.0f / 8.0f;

        private float angRotY = 30.0f;

        // Texture Objects
        GLTexture flagTexture;      // Which texture is currently on the flag
        GLTexture fPictureTexture;  // A texture of a static image
        VideoTexture fCamera1;
        VideoTexture fCamera2;


        FlagSystem fBanner;
        FlagPole flagpole;
        double windStrength = 0.3;

        private bool bUseTexture;
        PolygonMode polyFillMode;

        // Some light values
        Point3D positionLight = new Point3D(-6.0f, 5.0f, 0.0f);
        ColorRGBA ambientLight = new ColorRGBA(0.4f, 0.4f, 0.4f, 1.0f);
        ColorRGBA diffuseLight = new ColorRGBA(0.7f, 0.7f, 0.7f, 1.0f);

        #region Constructor
        public FlagScene()
        {
            fFlagResolution = new Resolution(40, 40);
            fBanner = new FlagSystem(fFlagResolution, timeStep);

            fBanner.size = size;
            fBanner.uTile = 1;
            fBanner.vTile = 1;
            fBanner.WindStrength = windStrength;

            fBanner.ini(timeStep);

            polyFillMode = PolygonMode.Fill;
            bUseTexture = true;
        }
        #endregion

        public void SetupTexture()
        {
            //fCamera1 = VideoTexture.CreateFromDeviceIndex(GI, 0, 320, 240);
            //fCamera2 = VideoTexture.CreateFromDeviceIndex(GI, 1, 320, 240);
            fCamera1 = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
            fCamera2 = VideoTexture.CreateFromDeviceIndex(GI, 1, true); 
            fPictureTexture = TextureHelper.CreateTextureFromFile(GI, "EELogo.jpg", false);

            flagTexture = fPictureTexture;
        }

        protected override void OnSetContext()
        {
            GI.Features.DepthTest.Enable();

            GI.ShadeModel(ShadingModel.Smooth);
            GI.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
            GI.Buffers.ColorBuffer.Color = new ColorRGBA(0, 0, 0, 1);
            GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Line);

            GI.Features.Lighting.Enable();
            GI.Features.Lighting.Light0.SetAmbientDiffuse(ambientLight, diffuseLight);
            GI.Features.Lighting.Light0.Location = positionLight;
            GI.Features.Lighting.Light0.Enable();

            GI.Features.ColorMaterial.Enable();
            GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);

            flagpole = new FlagPole(fBanner.x0, fBanner.y0, fBanner.z0, fBanner.nHeight, size);

            SetupTexture();
        }


        protected override void DrawBegin()
        {
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();

            GI.Viewport(0, 0, ViewportWidth, ViewportHeight);

            // View matrix
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            GI.Glu.Perspective(45.0f, (float)ViewportWidth / (float)ViewportHeight, 0.1f, 500.0f);

            // Model Matrix
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();

            GI.Translate(0, 0, -12);
            GI.Rotate(30, 1, 0, 0);
            GI.Rotate(angRotY, 0, 1, 0);

            fBanner.WindStrength = windStrength;
            
            base.DrawBegin();
        }

        protected override void DrawContent()
        {
            // Disable texturing to draw the flagpole
            GI.Features.Texturing2D.Disable();
            flagpole.Render(GI);

            // Update the cloth
            fBanner.MainLoop();

            // Bind the texture if we're using it
            if (bUseTexture)
            {
                GI.Features.Texturing2D.Enable();
                flagTexture.Bind();
            }

            GI.PolygonMode(GLFace.FrontAndBack, polyFillMode);
            fBanner.Render(GI);

        }

        public override void OnSetViewport(int width, int height)
        {

            ViewportHeight = height;
            ViewportWidth = width;
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == KeyActivityType.KeyDown)
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

                    case VirtualKeyCodes.Up:
                        windStrength += 0.1;
                        break;

                    case VirtualKeyCodes.Down:
                        windStrength -= 0.1;
                        break;
                }
            }

            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.F1:
                        flagTexture = fPictureTexture;
                        break;

                    case VirtualKeyCodes.F2:
                        flagTexture = fCamera1;
                        break;

                    case VirtualKeyCodes.F3:
                        flagTexture = fCamera2;
                        break;

                    case VirtualKeyCodes.F:
                        polyFillMode = PolygonMode.Fill;
                        break;
                    case VirtualKeyCodes.L:
                        polyFillMode = PolygonMode.Line;
                        break;
                    case VirtualKeyCodes.W:
                        fBanner.ApplyWind = !fBanner.ApplyWind;
                        break;
                    case VirtualKeyCodes.T:
                        bUseTexture = !bUseTexture;
                        break;
                }
            }

            return IntPtr.Zero;
        }

        protected override void OnReleaseContext()
        {
            if (fCamera1 != null)
                fCamera1.Dispose();

            base.OnReleaseContext();
        }
    }
}