
namespace QuadVideo
{
    using System;
    using System.Drawing;
    
    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;

    using NewTOAPIA.Media;
    using NewTOAPIA.Media.Capture;
    using NewTOAPIA.GL.Media;
    using NewTOAPIA.Modeling;
    using NewTOAPIA.UI.GL;

    class VideoScene : GLModel
    {
        GLTexture fVideoTexture1;
        GLTexture fVideoTexture2;

        
        public VideoScene()
        {
        }

        protected override void OnSetContext()
        {
            int numSources = VideoCaptureDevice.GetNumberOfInputDevices();

            if (numSources < 2)
                throw new ArgumentOutOfRangeException("numSources", numSources, "Number of sources needs to be at least two or greater.");

            fVideoTexture1 = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
            fVideoTexture1.Unbind();

            fVideoTexture2 = VideoTexture.CreateFromDeviceIndex(GI, 1, true);
            fVideoTexture2.Unbind();


            // Turn off features that we don't need
            // they just slow down video processing
            GI.Features.AlphaTest.Disable();
            GI.Features.Blend.Disable();
            GI.Features.DepthTest.Disable();
            GI.Features.Dither.Disable();
            GI.Features.Fog.Disable();
            GI.Features.Lighting.Disable();

            GI.Features.Texturing2D.Enable();
            GI.TexEnv(TextureEnvModeParam.Decal);

            GI.FrontFace(FrontFaceDirection.Cw);
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);

        }

        #region Drawing
        #region Drawing Helpers
        void DrawQuad(GLTexture tex, float left, float top, float right, float bottom)
        {
            tex.Bind();

            GI.Drawing.Quads.Begin();
            // Left bottom
            GI.TexCoord(0.0f, 0.0f);
            GI.Vertex(left, bottom, 0.0f);

            // Left top
            GI.TexCoord(0.0f, 1.0f);
            GI.Vertex(left, top, 0.0f);

            // Right top
            GI.TexCoord(1.0f, 1.0f);
            GI.Vertex(right, top, 0.0f);

            // Right bottom
            GI.TexCoord(1.0f, 0.0f);
            GI.Vertex(right, bottom, 0.0f);
            GI.Drawing.Quads.End();

            tex.Unbind();
        }
        #endregion

        /// <summary>
        /// Do this at the beginning of each frame.
        /// Typically setup views, clear buffers, and the like.
        /// </summary>
        protected override void DrawBegin()
        {
            // Setup an orthographic projection such that we have 1:1 
            // correspondence between coordinates and pixels on the window
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();



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

            // Draw the left eye
            GI.Ortho(0, ViewportWidth, 0, ViewportHeight, ViewportWidth / 2, -(ViewportWidth / 2));
            GI.Viewport(0, 0, ViewportWidth / 2, ViewportHeight);
            //GI.Viewport(0, 0, ViewportWidth, ViewportHeight);
            DrawQuad(fVideoTexture1, 0, ViewportHeight, ViewportWidth, 0);

            // Draw the right eye
            //
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Ortho(0, ViewportWidth, 0, ViewportHeight, ViewportWidth / 2, -(ViewportWidth / 2));
            GI.Viewport(ViewportWidth / 2, 0, ViewportWidth / 2, ViewportHeight);
            DrawQuad(fVideoTexture2, 0, ViewportHeight, ViewportWidth, 0);

        }

        protected override void DrawEnd()
        {
            GI.Flush();
        }
        #endregion

        public override void OnSetViewport(int w, int h)
        {
            ViewportHeight = (h < 1) ? 1 : h;
            ViewportWidth = (w < 1) ? 1 : w;
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {

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