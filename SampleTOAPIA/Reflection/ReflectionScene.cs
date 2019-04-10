using System;

using NewTOAPIA.GL;

using NewTOAPIA.GLU.Shapes;
using NewTOAPIA.UI.GL;
using NewTOAPIA.Graphics;
using NewTOAPIA.Media.GL;


namespace Reflection
{
    class ReflectionScene : GLModel
    {
        // Light and material Data
        Point3D fLightPos = new Point3D(-100.0f, 100.0f, 50.0f);  // Point source
        Point3D fLightPosMirror = new Point3D(-100.0f, -100.0f, 50.0f);
        ColorRGBA fNoLight = ColorRGBA.Invisible;
        ColorRGBA fLowLight = new ColorRGBA(0.25f, 0.25f, 0.25f, 1.0f);
        ColorRGBA fBrightLight = ColorRGBA.White;

        static float yRot = 0.0f;         // Rotation angle for animation        

        Cube aCube;
        GLUCylinder aCylinder;
        GroundPlane fGround;
        VideoTexture fCubeTexture;
        VideoTexture fCylindarTexture;

        bool doRotation = false;
        bool showCylindarTexture = false;
        bool showCubeTexture = true;

        public ReflectionScene()
        {
        }

        protected override void OnSetContext()
        {
            GraphicsInterface.gCheckErrors = false;

            fCubeTexture = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
            if (fCubeTexture != null) {
                fCubeTexture.Start();
            }

            fCylindarTexture = VideoTexture.CreateFromDeviceIndex(GI, 1, true);
            if (fCylindarTexture != null)
            {
                fCylindarTexture.Start();
            }

            // Set our typical parameters
            //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
            //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            //GI.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);


            aCube = new Cube(GI, new Vector3D(0.5f, 0.5f, 0.5f), fCubeTexture);

            aCylinder = new GLUCylinder(GI, 0.5, 1, 100, 20);
            aCylinder.NormalType = QuadricNormalType.Smooth;
            aCylinder.UsesTexture = 1;

            fGround = new GroundPlane(20.0f, 0.5f, 1.0f);

            // Grayish background
            GI.Buffers.ColorBuffer.Color = fLowLight;

            // Cull backs of polygons
            GI.CullFace(GLFace.Back);
            GI.FrontFace(FrontFaceDirection.Ccw);
            GI.Features.CullFace.Enable();
            GI.Features.DepthTest.Enable();

            // Setup light parameters
            GI.LightModel(LightModelParameter.LightModelAmbient, (float[])fNoLight);
            GI.Features.Lighting.Light0.Ambient = fLowLight;
            GI.Features.Lighting.Light0.Diffuse = fBrightLight;
            GI.Features.Lighting.Light0.Specular = fBrightLight;

            GI.Features.Lighting.Enable();
            GI.Features.Lighting.Light0.Enable();

            // Mostly use material tracking
            GI.Features.ColorMaterial.Enable();
            GI.ColorMaterial(GLFace.Front, ColorMaterialParameter.AmbientAndDiffuse);
            GI.Material(GLFace.Front, MaterialParameter.Shininess, 128);
        }



        protected override void DrawBegin()
        {
            aCube.ShowTexture = showCubeTexture;

            float fAspect;


            GI.Viewport(0, 0, ViewportWidth, ViewportHeight);

            fAspect = (float)ViewportWidth / (float)ViewportHeight;

            // Reset the coordinate system before modifying
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            // Set the clipping volume
            GI.Glu.Perspective(35.0f, fAspect, 1.0f, 50.0f);

            // Set the viewpoint
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Translate(0.0f, -0.4f, 0.0f);

            GI.Features.Texturing2D.Enable();
            GI.TexEnv(TextureEnvModeParam.Decal);

            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();
        }

        protected override void DrawContent()
        {
            // Preserve the view matrix
            GI.PushMatrix();

            ////// Move light under floor to light the "reflected" world
            GI.Features.Lighting.Light0.Location = fLightPosMirror;
            GI.PushMatrix();
            GI.FrontFace(FrontFaceDirection.Cw);             // geometry is mirrored, swap orientation
            GI.Scale(1.0f, -1.0f, 1.0f);

            DrawWorld();

            GI.FrontFace(FrontFaceDirection.Ccw);
            GI.PopMatrix();

            //// Draw the ground transparently over the reflection
            GI.Features.Lighting.Disable();
            GI.Features.Blend.Enable();

            GI.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            fGround.Render(GI);

            GI.Features.Blend.Disable();
            GI.Features.Lighting.Enable();

            //// Restore correct lighting and draw the world correctly
            GI.Features.Lighting.Light0.Location = fLightPos;

            DrawWorld();

            GI.PopMatrix();

            if (doRotation)
                yRot += 0.5f;   // Update Cube Rotation
        }

        void DrawWorld()
        {
            float[] zPlane = { 0, 1, 0, 0 };

            GI.PushMatrix();

            GI.Translate(0.0f, 0.5f, -3.5f);

            GI.PushMatrix();

            GI.ShadeModel(ShadingModel.Smooth);
            GI.Features.Texturing2D.Enable();
            GI.Rotate(-yRot * 2.0f, 0.0f, 1.0f, 0.0f);
            GI.Translate(1.0f, 0.0f, 0.0f);

            //if (showCubeTexture)
            //    aCube.Texture = fCubeTexture;
            //else
            //    aCube.Texture = null;

            aCube.Render(GI);

            GI.PopMatrix();

            // Draw a cylinder video display surface
            GI.PushMatrix();
            GI.Features.Texturing2D.Enable();

            if ((fCylindarTexture != null) && showCylindarTexture)
                fCylindarTexture.Bind();

            // Rotate 90 degrees around x-axis to get it upright
            GI.Rotate(-90f, 1.0f, 0.0f, 0.0f);

            // Translate by half the height to get it 
            // above the ground
            GI.Translate(0, 0, (float)(-aCylinder.Height / 2) + 0.02f);

            aCylinder.Render(GI);

            if (fCylindarTexture != null)
                fCylindarTexture.Unbind();

            GI.PopMatrix();

            GI.PopMatrix();
        }

        public override void OnSetViewport(int w, int h)
        {
            // Prevent a divide by zero, when window is too short
            // (you cant make a window of zero width).
            if (h == 0)
                h = 1;

            ViewportWidth = w;
            ViewportHeight = h;
        }

        public override IntPtr OnKeyboardActivity(object sender, NewTOAPIA.UI.KeyboardActivityArgs kbde)
        {
            IntPtr retValue = new IntPtr(1);

            if (kbde.AcitivityType == NewTOAPIA.UI.KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case NewTOAPIA.UI.VirtualKeyCodes.R:
                        doRotation = !doRotation;
                        return IntPtr.Zero;

                    case NewTOAPIA.UI.VirtualKeyCodes.S:
                        showCylindarTexture = !showCylindarTexture;
                        return IntPtr.Zero;

                    case NewTOAPIA.UI.VirtualKeyCodes.C:
                        showCubeTexture = !showCubeTexture;
                        return IntPtr.Zero;
                }
            }

            if (retValue != IntPtr.Zero)
                return base.OnKeyboardActivity(sender, kbde);

            return IntPtr.Zero;
        }

        protected override void OnReleaseContext()
        {
            if (null != fCylindarTexture)
                fCylindarTexture.Dispose();

            if (null != fCubeTexture)
                fCubeTexture.Dispose();

        }
    }
}

