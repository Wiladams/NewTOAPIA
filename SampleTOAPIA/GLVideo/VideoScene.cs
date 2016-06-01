
namespace QuadVideo
{
    using System;
    using System.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;
    using NewTOAPIA.UI;
    using NewTOAPIA.Media.Capture;
    using NewTOAPIA.Media.GL;
    using NewTOAPIA.UI.GL;

    class VideoScene : GLModel
    {
        GLTexture fVideoTexture1;
        GLTexture fVideoTexture2;
        TexturedCheckerboard fCheckerboard;

        GLSLShaderProgram fSepiaProgram;
        bool useShader;

        object splitsLock = new object();
        int fHowManySplits;

        int fViewportWidth = 320;
        int fViewportHeight = 240;
        
        float rotationIncrement = 10.0f;
        float Rotation;

        public VideoScene()
        {
            fHowManySplits = 1;
        }

        protected override void OnSetContext()
        {
            fSepiaProgram = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, null, Shaders.ShaderStrings.sepia_fs);

            int numSources = VideoCaptureDevice.GetNumberOfInputDevices();

            if (numSources > 0)
            {
                fVideoTexture1 = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
            }
            else
                fVideoTexture1 = TextureHelper.CreateCheckerboardTexture(GI, 320, 240, 8);
            fVideoTexture1.Unbind();

            if (numSources > 1)
            {
                fVideoTexture2 = VideoTexture.CreateFromDeviceIndex(GI, 1, true);
            }
            else
                fVideoTexture2 = TextureHelper.CreateCheckerboardTexture(GI, 320, 240, 16);
            fVideoTexture2.Unbind();

            //if (numSources > 2)
            //{
            //    fVideoTexture2 = VideoTexture.CreateFromDeviceIndex(GI, 2, true);
            //}

            fCheckerboard = new TexturedCheckerboard(new Size(fViewportWidth, fViewportHeight),
                new Size(fHowManySplits, fHowManySplits), fVideoTexture1, fVideoTexture2);

            // Turn off features that we don't need
            // they just slow down video processing
            GI.Features.AlphaTest.Disable();
            GI.Features.Blend.Disable();
            GI.Features.DepthTest.Disable();
            GI.Features.Dither.Disable();
            GI.Features.Fog.Disable();
            GI.Features.Lighting.Disable();
        }

        #region Drawing
        /// <summary>
        /// Do this at the beginning of each frame.
        /// Typically setup views, clear buffers, and the like.
        /// </summary>
        protected override void DrawBegin()
        {
            fCheckerboard.PixelSize = new System.Drawing.Size(fViewportWidth, fViewportHeight);

            // Set the viewport to be the same as the window size
            GI.Viewport(0, 0, fViewportWidth, fViewportHeight);

            // Setup an orthographic projection such that we have 1:1 
            // correspondence between coordinates and pixels on the window
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            //GI.Ortho(0, fViewportWidth, 0, fViewportHeight, 1, -1);
            GI.Ortho(0, fViewportWidth, 0, fViewportHeight, fViewportWidth / 2, -(fViewportWidth / 2));

            // Clear our buffers
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();
        }

        protected override void  DrawContent()
        {
            // Strictly speaking, we don't need to set the model view matrix
            // because we're not doing any model transforms.  So, we set it to 
            // the identity matrix to ensure our assumption.
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();

            if (useShader)
            {
                // Make sure the shader program is in the rendering pipeline
                fSepiaProgram.Bind();
            }

            fCheckerboard.SetSplits(fHowManySplits);
            fCheckerboard.Rotation = new Vector3f(0, Rotation, 0);
            //fCheckerboard.BlockSize = new System.Drawing.Size(fHowManySplits, fHowManySplits);
            fCheckerboard.Render(GI);

            fSepiaProgram.Unbind();
        }

        protected override void DrawEnd()
        {
            GI.Flush();
        }
        #endregion

        public override void OnSetViewport(int w, int h)
        {
            fViewportHeight = (h == 0) ? 1 : h;
            fViewportWidth = (w == 0) ? 1 : w;

        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.Space:
                        useShader = !useShader;
                        break;

                    case VirtualKeyCodes.Left:
                        Rotation -= rotationIncrement;
                        break;

                    case VirtualKeyCodes.Right:
                        Rotation += rotationIncrement;
                        break;

                    case VirtualKeyCodes.Up:
                            fHowManySplits += 1;
                        break;

                    case VirtualKeyCodes.Down:
                            fHowManySplits -= 1;
                            if (fHowManySplits < 1)
                                fHowManySplits = 1;
                        break;
                }
            }

            switch (kbde.VirtualKeyCode)
            {
                case VirtualKeyCodes.PageUp:
                    fHowManySplits += 1;
                    break;

                case VirtualKeyCodes.PageDown:
                    fHowManySplits -= 1;
                    if (fHowManySplits < 1)
                        fHowManySplits = 1;

                    break;
            }

            return IntPtr.Zero;
        }

        protected override void OnReleaseContext()
        {
            fVideoTexture1.Dispose();
            fVideoTexture2.Dispose();

            base.OnReleaseContext();
        }
    }
}