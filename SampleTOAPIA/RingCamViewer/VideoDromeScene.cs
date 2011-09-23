

namespace RingCamView
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;

    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.GL.Media;
    using NewTOAPIA.UI;
    using NewTOAPIA.Drawing;

    public class VideoDromeScene : GLModel
    {
        // Camera Position
        Vector3f fCameraLocation;
        Vector3f fCameraRotation;
        float fExpansionFactor;
        float fExpansionRatio;

        // Light and material Data
        float[] fLightPos = { -100.0f, 100.0f, 50.0f, 1.0f };  // Point source
        float[] fLightPosMirror = { -100.0f, -100.0f, 50.0f, 1.0f };
        float[] fNoLight = { 0.0f, 0.0f, 0.0f, 0.0f };
        float[] fLowLight = { 0.25f, 0.25f, 0.25f, 1.0f };
        float[] fBrightLight = { 1.0f, 1.0f, 1.0f, 1.0f };


        // These variables set the dimensions of the rectanglar region we wish to view.
        float fAspect;
        const double Xmin = 0.0, Xmax = 3.0;
        const double Ymin = 0.0, Ymax = 3.0;


        WallCylinder fWallCylinder;
        SpeakerCube fSpeakerCube;

        GLTexture fWallTexture;
        GLTexture fCubeTexture;

        public VideoDromeScene()
        {
            // Outside the walls
            fCameraLocation = new Vector3f(0.0f, -6.0f, -28.0f);
            
            // Inside the walls
            //fCameraLocation = new Vector3f(0.0f, -6.0f, 0.0f);
            fCameraRotation = new Vector3f(0.0f, 0.0f, 0.0f);
            fExpansionFactor = 1.0f;
            fExpansionRatio = 1.005f;
        }

        protected override void OnSetContext()
        {
            GraphicsInterface.gCheckErrors = true;

            GI.Features.DepthTest.Enable();

            // Medium Cyan background
            GI.Buffers.ColorBuffer.Color = ColorRGBA.MediumCyan;

            // Cull backs of polygons
            //GI.CullFace(GLFace.Back);
            GI.FrontFace(FrontFaceDirection.Ccw);
            //GI.Enable(GLOption.CullFace);

            GI.Features.DepthTest.Enable();

            SetupLighting();

            // Mostly use material tracking
            GI.Features.ColorMaterial.Enable();
            GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            GI.Material(GLFace.FrontAndBack, MaterialParameter.Shininess, 128);


            string PanoramicName = "Microsoft RoundTable Panoramic Video";
            string SpeakerName = "Microsoft RoundTable Active Speaker Video";

            fWallTexture = VideoTexture.CreateFromDevicePath(GI, PanoramicName, -1, -1, true);
            if (null == fWallTexture)
            {
                fWallTexture = TextureHelper.CreateCheckerboardTexture(GI, 512, 512, 16);
            }
            else
            {
                ((VideoTexture)fWallTexture).Start();
            }

            fCubeTexture = VideoTexture.CreateFromDevicePath(GI, SpeakerName, -1, -1, true);
            if (fCubeTexture == null)
            {
                fCubeTexture = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
                if (fCubeTexture == null)
                {
                    fCubeTexture = TextureHelper.CreateCheckerboardTexture(GI, 512, 512, 8);
                }
            }
            else
            {
                ((VideoTexture)fCubeTexture).Start();
            }

            fWallCylinder = new WallCylinder(GI, fWallTexture, fExpansionFactor);
            fSpeakerCube = new SpeakerCube(GI, fCubeTexture);
        }

        void SetupLighting()
        {
            // Setup light parameters
            //GI.LightModel(LightModelParameter.LightModelAmbient, fNoLight);
            GI.LightModel(LightModelParameter.LightModelAmbient, fBrightLight);
            GI.Features.Lighting.Light0.Ambient = new ColorRGBA(fLowLight);
            GI.Features.Lighting.Light0.Diffuse = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Light0.Specular = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Enable();
            GI.Features.Lighting.Light0.Enable();
        }

        // Draw the ground as a series of triangle strips. The 
        // shading model and colors are set such that we end up 
        // with a black and white checkerboard pattern.
        void DrawGround()
        {
            float fExtent = 20.0f;
            float fStep = 0.5f;
            float y = 0.0f;
            float fColor;
            float iStrip, iRun;
            int iBounce = 0;

            GI.ShadeModel(ShadingModel.Flat);
            GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Fill);

            for (iStrip = -fExtent; iStrip <= fExtent; iStrip += fStep)
            {
                GI.Drawing.TriangleStrip.Begin();
                for (iRun = fExtent; iRun >= -fExtent; iRun -= fStep)
                {
                    if ((iBounce % 2) == 0)
                        fColor = 1.0f;
                    else
                        fColor = 0.0f;

                    GI.Color(fColor, fColor, fColor, 0.5f);
                    GI.Vertex(iStrip, y, iRun);
                    GI.Vertex(iStrip + fStep, y, iRun);

                    iBounce++;
                }
                GI.Drawing.TriangleStrip.End();
            }
            GI.ShadeModel(ShadingModel.Smooth);
        }

        protected override void DrawBegin()
        {
            base.DrawBegin();

            // Reset the coordinate system before modifying
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            // Set the clipping volume
            GI.Glu.Perspective(35.0f, fAspect, 1.0f, 500.0f);

            // Set the viewpoint
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Translate(fCameraLocation);
            GI.Rotate(fCameraRotation);
        }

        protected override void DrawContent()
        {
            // Draw some axis indicators
            //fAxes.Render(GI);


            DrawGround();

            fSpeakerCube.Render(GI);

            fWallCylinder.ExpansionFactor = fExpansionFactor;
            fWallCylinder.Render(GI);
        }

        public override void OnSetViewport(int width, int height)
        {
            // Prevent a divide by zero, when window is too short
            // (you cant make a window of zero width).
            if (height == 0)
                height = 1;

            GI.Viewport(0, 0, width, height);

            fAspect = (float)width / (float)height;
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            base.OnKeyboardActivity(sender, kbde);

            if (kbde.AcitivityType == KeyActivityType.KeyDown)
            {
                switch (kbde.VirtualKeyCode)
                {
                    // Move closer to current direction
                    case VirtualKeyCodes.Up:
                        fCameraLocation.z += 0.5f;
                        fExpansionFactor *= fExpansionRatio;
                        if (fExpansionFactor > 2.0)
                            fExpansionFactor = 2.0f;

                       
                        break;

                    // Move further from center
                    case VirtualKeyCodes.Down:
                        fCameraLocation.z -= 0.5f;
                        fExpansionFactor *= 1 / fExpansionRatio;
                        if (fExpansionFactor < 1.0f)
                            fExpansionFactor = 1.0f;
                        break;

                    // Look to the right 
                    case VirtualKeyCodes.Right:
                        if (!kbde.Shift)
                            fCameraRotation.y += 10;
                        else
                            fCameraLocation.x += 0.5f;
                        break;

                    // Look to the left
                    case VirtualKeyCodes.Left:
                        if (!kbde.Shift)
                            fCameraRotation.y -= 10;
                        else
                            fCameraLocation.x -= 0.5f;
                        break;

                    case VirtualKeyCodes.PageUp:
                        fCameraLocation.y -= 0.5f;
                        break;

                    case VirtualKeyCodes.PageDown:
                        fCameraLocation.y += 0.5f;
                        break;

                    default:
                        break;
                }

            }

            return IntPtr.Zero;
        }

        protected override void OnReleaseContext()
        {
            if (fWallTexture != null)
            {
                fWallTexture.Dispose();
            }

            if (fCubeTexture != null)
            {
                fCubeTexture.Dispose();
            }

            base.OnReleaseContext();
        }
    }
}