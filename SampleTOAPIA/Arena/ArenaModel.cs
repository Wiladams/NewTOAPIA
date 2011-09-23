
using System;
using System.Drawing;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.DirectShow;
using NewTOAPIA.GL;
using NewTOAPIA.GL.Media;
using NewTOAPIA.UI;
using NewTOAPIA.Drawing;
using NewTOAPIA.Net;
using NewTOAPIA.Net.Rtp;
using NewTOAPIA.Media;


namespace Arena
{
    public class ArenaModel : GLModel
    {
        SketchViewer fViewer;

        // Camera Position
        Joystick fJoystick;

        Vector3f fCameraLocation;
        Vector3f fCameraRotation;
        float fExpansionFactor;
        float fExpansionRatio;
        float fSpeakerSeparation;

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

        // The cylindrical sections used for display
        CylinderSection fSection0;
        CylinderSection fSection72;
        CylinderSection fSection144;
        CylinderSection fSection216;
        CylinderSection fSection288;

        CylinderSection fSpeakerSection0;
        CylinderSection fSpeakerSection90;
        CylinderSection fSpeakerSection180;
        CylinderSection fSpeakerSection270;


        VideoTexture fWallVideo;
        VideoTexture fSpeakerVideo;

        GLTexture fWallTexture;
        GLTexture fDesktopTexture;
        GLTexture fSpeakerTexture;

        bool fUseFilter;

        public ArenaModel()
        {
            fCameraLocation = new Vector3f(0.0f, -6.0f, -28.0f);
            fCameraRotation = new Vector3f(0.0f, 0.0f, 0.0f);
            fExpansionFactor = 1.0f;
            fExpansionRatio = 1.005f;

            fSpeakerSeparation = 0.30f;

            fJoystick = new Joystick(0);
        }

        protected override void  OnSetContext()
        {
            GraphicsInterface.gCheckErrors = true;

            //fViewer = new SketchViewer(GI, 620, 440);

            
            // Sections of the wall
            fSection0 = new CylinderSection(GI, 14.0f, 12.0f, 72, 10, 10, new RectangleF(4.0f/5,1, 1.0f/5, 1));

            fSection72 = new CylinderSection(GI, 14.0f, 12.0f, 72, 10, 10, new RectangleF(3.0f/5, 1, 1.0f / 5, 1));

            fSection144 = new CylinderSection(GI, 14.0f, 12.0f, 72, 10, 10, new RectangleF(2.0f / 5, 1, 1.0f / 5, 1));

            fSection216 = new CylinderSection(GI, 14.0f, 12.0f, 72, 10, 10, new RectangleF(1.0f / 5, 1, 1.0f / 5, 1));

            fSection288 = new CylinderSection(GI, 14.0f, 12.0f, 72, 10, 10, new RectangleF(0.0f / 5, 1, 1.0f / 5, 1));


            // Sections displaying current speaker
            fSpeakerSection0 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection90 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection180 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);
            fSpeakerSection270 = new CylinderSection(GI, 4.0f, 4.0f, 45, 10, 10);



            GI.Features.DepthTest.Enable();

            // Medium Cyan background
            GI.Buffers.ColorBuffer.Color = ColorRGBA.MediumCyan;

            // Cull backs of polygons
            //GI.CullFace(GLFace.Back);
            GI.FrontFace(FrontFaceDirection.Ccw);
            //GI.Enable(GLOption.CullFace);

            GI.Features.DepthTest.Enable();

            // Setup light parameters
            //GI.LightModel(LightModelParameter.LightModelAmbient, fNoLight);
            GI.LightModel(LightModelParameter.LightModelAmbient, fBrightLight);
            GI.Features.Lighting.Light0.Ambient = new ColorRGBA(fLowLight);
            GI.Features.Lighting.Light0.Diffuse = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Light0.Specular = new ColorRGBA(fBrightLight);
            GI.Features.Lighting.Enable();
            GI.Features.Lighting.Light0.Enable();

            // Mostly use material tracking
            GI.Features.ColorMaterial.Enable();
            GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            GI.Material(GLFace.FrontAndBack, MaterialParameter.Shininess, 128);


            string PanoramicName = "Microsoft RoundTable Panoramic Video";
            string SpeakerName = "Microsoft RoundTable Active Speaker Video";

            //fWallTexture = TextureHelper.CreateCheckerboardTexture(GI, 512, 512);
            //fWallTexture = VideoTexture.CreateVideoDeviceTexture(GI, PanoramicName);
            fWallVideo = VideoTexture.CreateFromDeviceIndex(GI, 0);
            fWallTexture = fWallVideo;


            //fSpeakerTexture = TextureHelper.CreateCheckerboardTexture(GI, 512, 512);
            //fSpeakerTexture = VideoTexture.CreateVideoDeviceTexture(GI, SpeakerName);
            //fSpeakerTexture = VideoTexture.CreateFromDeviceIndex(GI, 1);
            fSpeakerVideo = VideoTexture.CreateFromDeviceIndex(GI, 1);
            fSpeakerTexture = fSpeakerVideo;

            fDesktopTexture = TextureHelper.CreateCheckerboardTexture(GI, 512, 512);
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
                GI.Begin(BeginMode.TriangleStrip);
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
                GI.End();
            }
            GI.ShadeModel(ShadingModel.Smooth);
        }

        void DrawVideoWall()
        {
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            GI.TexEnv(TextureEnvModeParam.Modulate);
            GI.Features.Texturing2D.Enable();


            // Set drawing color to white
            GI.Drawing.Color = ColorRGBA.White;
            GI.FrontFace(FrontFaceDirection.Cw);

            // Draw Section 0
            //GI.ActiveTexture(GLTextureUnit.Unit0);
            fWallTexture.Bind();


            GI.PushMatrix();
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
            //GI.Rotate(-90, 1, 0, 0);
            GI.Translate(0, 0, -fSection0.Radius*fExpansionFactor);
            fSection0.Render(GI);
            GI.PopMatrix();
            fWallTexture.Unbind();

            // Draw Section 72
            fWallTexture.Bind();
            GI.PushMatrix();
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
            GI.Rotate(72, 0, 1, 0);
            GI.Translate(0, 0, -fSection72.Radius * fExpansionFactor);
            fSection72.Render(GI);
            GI.PopMatrix();
            fWallTexture.Unbind();

            // Draw Section 144
            fWallTexture.Bind();
            GI.PushMatrix();
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
            GI.Rotate(144, 0, 1, 0);
            GI.Translate(0, 0, -fSection144.Radius * fExpansionFactor);
            fSection144.Render(GI);
            GI.PopMatrix();
            fWallTexture.Unbind();

            // Draw Section 216
            fWallTexture.Bind();
            GI.PushMatrix();
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
            GI.Rotate(216, 0, 1, 0);
            GI.Translate(0, 0, -fSection216.Radius * fExpansionFactor);
            fSection216.Render(GI);
            GI.PopMatrix();
            fWallTexture.Unbind();

            // Draw Section 288
            fWallTexture.Bind();
            GI.PushMatrix();
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
            GI.Rotate(288, 0, 1, 0);
            GI.Translate(0, 0, -fSection288.Radius * fExpansionFactor);
            fSection288.Render(GI);
            GI.PopMatrix();
            fWallTexture.Unbind();

            GI.Features.Texturing2D.Disable();

        }

        void DrawSpeakerCube()
        {
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            GI.TexEnv(TextureEnvModeParam.Modulate);
            GI.Features.Texturing2D.Enable();

            // Set drawing color to white
            //GI.Drawing.Color = ColorRGBA.White;
            //GI.FrontFace(FrontFaceDirection.Cw);

            if (null != fViewer && fViewer.TexTure != null)
                fDesktopTexture = fViewer.TexTure;


            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);

            // Draw Section 0
            fDesktopTexture.Bind();
            GI.PushMatrix();
                GI.Rotate(0, 0, 1, 0);
                GI.Translate(0, 4, fSpeakerSection0.Radius * fSpeakerSeparation);
                fSpeakerSection0.Render(GI);
            GI.PopMatrix();
            fDesktopTexture.Unbind();

            // Draw Section 90
                fSpeakerTexture.Bind();
            GI.PushMatrix();
                GI.Rotate(90, 0, 1, 0);
                GI.Translate(0, 4, fSpeakerSection90.Radius * fSpeakerSeparation);
                fSpeakerSection90.Render(GI);
            GI.PopMatrix();
                fSpeakerTexture.Unbind();

            // Draw Section 180
            fDesktopTexture.Bind();
            GI.PushMatrix();
                GI.Rotate(180, 0, 1, 0);
                GI.Translate(0, 4, fSpeakerSection180.Radius * fSpeakerSeparation);
                fSpeakerSection180.Render(GI);
            GI.PopMatrix();
            fDesktopTexture.Unbind();

            // Draw Section 270
                fSpeakerTexture.Bind();
            GI.PushMatrix();
                GI.Rotate(270, 0, 1, 0);
                GI.Translate(0, 4, fSpeakerSection270.Radius * fSpeakerSeparation);
                fSpeakerSection270.Render(GI);
            GI.PopMatrix();
                fSpeakerTexture.Unbind();
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

            DrawSpeakerCube();

            DrawVideoWall();
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

        void MoveCloser()
        {
            fCameraLocation.z += 0.5f;
            fExpansionFactor *= fExpansionRatio;
            if (fExpansionFactor > 2.0)
                fExpansionFactor = 2.0f;
        }

        void MoveAway()
        {
            fCameraLocation.z -= 0.5f;
            fExpansionFactor *= 1 / fExpansionRatio;
            if (fExpansionFactor < 1.0f)
                fExpansionFactor = 1.0f;
        }

        public override void OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            base.OnKeyboardActivity(sender, kbde);

            if (kbde.AcitivityType == KeyActivityType.KeyDown)
            {
                switch (kbde.VirtualKeyCode)
                {
                    // Move closer to current direction
                    case VirtualKeyCodes.Up:
                        MoveCloser();

                       
                        break;

                    // Move further from center
                    case VirtualKeyCodes.Down:
                        MoveAway();
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

                    case VirtualKeyCodes.Space:
                        fUseFilter = !fUseFilter;
                        break;

                    default:
                        break;
                }

            }
        }

        protected override void OnReleaseContext()
        {
            if (null != fWallTexture)
                fWallVideo.Stop();

            if (null != fSpeakerVideo)
                fSpeakerVideo.Stop();

            base.OnReleaseContext();
        }
    }
}