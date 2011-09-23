

namespace QuadVideo
{
    using System;
    using System.Drawing;

    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.GL.Media;
    using NewTOAPIA.Media.Capture;
    using NewTOAPIA.UI;

    class VideoScene : GLModel
    {
        #region Private Fields
        YCrCbProcessor fYCrCbProcessor;     // This processor does color separation

        // Colorization shader programs
        // These are used to give some color to the images
        // output from the color separation stages
        //GLSLShaderProgram fRedColorizer;
        //GLSLShaderProgram fGreenColorizer;
        //GLSLShaderProgram fBlueColorizer;
        GLSLShaderProgram fIntensityColorizer;

        // Separation textures
        VideoTexture fVideoTexture;
        GLTexture fYTexture;
        GLTexture fCrTexture;
        GLTexture fCbTexture;

        // A mesh to demonstrate rendering a texture
        // using multiple quads
        XYAxesMesh fFaceMesh;

        Resolution fResolution;
        int fBlockSize;
        Vector3D fFaceSize;
        bool fResolutionChanged;

        // How we actually display some stuff
        PolygonMode polyFillMode; 
        int fViewWidth;
        int fViewHeight;
        int fHowManySplits;
        #endregion

        #region Constructor
        public VideoScene()
        {
            polyFillMode = PolygonMode.Fill;
            fHowManySplits = 2;     // How many sections separate the display screen
            fBlockSize = 8;
            fResolutionChanged = true;
        }
        #endregion

        /// <summary>
        /// Once we have a context, we are able to peform various setup routines
        /// that require us to connect to OpenGL.
        /// </summary>
        protected override void OnSetContext()
        {
            // find out how many video cameras there are.
            int numSources = VideoCaptureDevice.GetNumberOfInputDevices();

            // If there's more than one, then connect to the one that is
            // at position '0' in the index.  To change which camera to use,
            // simply change the index number.
            if (numSources > 4)
            {
                fVideoTexture = VideoTexture.CreateFromDeviceIndex(GI, 4, true);
            }
            else if (numSources > 0)
            {
                fVideoTexture = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
            }

            // Enable texture support as we'll be using that.
            GI.Features.Texturing2D.Enable();
            GI.TexEnv(TextureEnvModeParam.Replace);
        
            // Turn off features that we don't need, and they 
            // just slow down video processing
            GI.Features.AlphaTest.Disable();
            GI.Features.Blend.Disable();
            GI.Features.DepthTest.Disable();
            GI.Features.Dither.Disable();
            GI.Features.Fog.Disable();
            GI.Features.Lighting.Disable();

            // Create the FaceMesh that will receive the gray texture so we 
            // can break it up into 8x8 squares
            fFaceSize = new Vector3D(fVideoTexture.Width, fVideoTexture.Height, 0);

            fYCrCbProcessor = new YCrCbProcessor(GI, fVideoTexture.Width, fVideoTexture.Height);

            // Create the shader programs that will perform the colorization so we can see our results.
            //fRedColorizer = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, Shaders.ShaderStrings.FixedVert, Shaders.ShaderStrings.redcolorizer_fs);
            //fGreenColorizer = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, Shaders.ShaderStrings.FixedVert, Shaders.ShaderStrings.greencolorizer_fs);
            //fBlueColorizer = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, Shaders.ShaderStrings.FixedVert, Shaders.ShaderStrings.bluecolorizer_fs);
            fIntensityColorizer = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, Shaders.ShaderStrings.FixedVert, Shaders.ShaderStrings.graycolorizer_fs);
        }


        /// <summary>
        /// This routine uses the YCrCbProcessor object to do the actual work of color
        /// model conversion.  After the processing occurs, we are left with three
        /// different texture objects as properties on the processor.
        /// 
        /// We assign those properties to variables that are used to display the results.
        /// </summary>
        void SeparateChannels()
        {
            fYCrCbProcessor.SeparateChannels(fVideoTexture);

            // Assign resultant textures to variables
            fYTexture = fYCrCbProcessor.YChannel;
            fCrTexture = fYCrCbProcessor.CrChannel;
            fCbTexture = fYCrCbProcessor.CbChannel;

            // assign the texture on the mesh object
            fFaceMesh.Texture = fYTexture;
        }

        #region Drawing Content
        /// <summary>
        /// This routine is meant to draw a quad with the specified texture
        /// on the screen.  It achieves this by first changing the viewport to 
        /// match the coordinates specified in the destination rectangle.
        /// 
        /// The coordinate system starts with 0,0 being the lower left hand
        /// corner of the window, and increasing x to the right and y as you
        /// go up the screen.  The coordinates are in pixels.
        /// 
        /// This can draw a "screen aligned quad" if the x,y == 0,0, and the
        /// width,height == the size of the frame buffer being drawn into.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x">location of x coordinate</param>
        /// <param name="y">location of y coordinate</param>
        /// <param name="width">width in pixels of the viewport</param>
        /// <param name="height">height in pixels of the viewport</param>
        void DisplayQuad(GLTexture texture, RectangleF srcRect, Rectangle dstRect)
        {
            int left = dstRect.X;
            int bottom = dstRect.Y;
            int right = left + dstRect.Width;
            int top = bottom + dstRect.Height;

            GI.Viewport(left, bottom, dstRect.Width, dstRect.Height);
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            GI.Ortho(0, dstRect.Width, 0, dstRect.Height, -1.0, 1.0);



            texture.Bind();
            GI.FrontFace(FrontFaceDirection.Ccw);

            GI.Drawing.Quads.Begin();
            {
                // Left bottom
                GI.TexCoord(srcRect.X, srcRect.Y);
                GI.Vertex(0, 0);

                // Right bottom
                GI.TexCoord(srcRect.X + srcRect.Width, srcRect.Y);
                GI.Vertex(dstRect.Width, 0);

                // Right top
                GI.TexCoord(srcRect.X + srcRect.Width, srcRect.Y + srcRect.Height);
                GI.Vertex(dstRect.Width, dstRect.Height);

                // Left top
                GI.TexCoord(srcRect.X, srcRect.Y + srcRect.Height);
                GI.Vertex(0, dstRect.Height);
            }
            GI.Drawing.Quads.End();

            texture.Unbind();
        }


        /// <summary>
        /// This routine will perform a channel separation, as well as displaying the 
        /// subsequent resulting quads on the display.
        /// </summary>
        void DrawQuads()
        {
            int xpos = fViewWidth/fHowManySplits ;
            int ypos = fViewHeight/ fHowManySplits;

            int xdiff = fVideoTexture.Width / fHowManySplits;
            int ydiff = fVideoTexture.Height / fHowManySplits;

            RectangleF srcRect = new RectangleF(0, 1.0f / fHowManySplits, 1.0f / fHowManySplits, 1.0f / fHowManySplits);
            Rectangle dstRect = new Rectangle(0, fViewHeight-ydiff, xdiff, ydiff);

            // Draw the video input without alteration
            // we'll show only the upper left quadrant
            DisplayQuad(fVideoTexture, srcRect, dstRect);

            // Now draw the intensities for each of the YUV planes.
            // The values in the texture object are only intensities
            // in order to have them actually show up, we use:
            // IntensityColorizer - to show the values as gray
            //
            // Showing the intensities is only an effect at the Cr and Cb
            // values are actually a combination of RGB to varying extents,
            // but they are dominant in these values.

            // Draw Y component in the upper right quadrant
            fIntensityColorizer.Bind();
            dstRect.X = xdiff;
            srcRect.X = 1.0f / fHowManySplits;
            DisplayQuad(fYTexture, srcRect, dstRect);
            fIntensityColorizer.Unbind();

            // If we want to display the quadmesh we can do the following
            //GI.PolygonMode(GLFace.FrontAndBack, polyFillMode);
            //GI.Viewport(xdiff, ydiff, xdiff, ydiff);
            //fFaceMesh.Render(GI);
            //GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Fill);

            // Draw Cr in lower left quadrant
            fIntensityColorizer.Bind();
            dstRect.X = 0;
            dstRect.Y = fViewHeight-(ydiff*2);
            srcRect = new RectangleF(0, 0, 1.0f / fHowManySplits, 1.0f / fHowManySplits);
            DisplayQuad(fCrTexture, srcRect, dstRect);
            fIntensityColorizer.Unbind();

            // Draw Cb in lower right quadrant
            fIntensityColorizer.Bind();
            dstRect.X = xdiff;
            srcRect.X = 1.0f / fHowManySplits;
            srcRect.Y = 0;
            DisplayQuad(fCbTexture, srcRect, dstRect);
            fIntensityColorizer.Unbind();
        }

        protected override void DrawBegin()
        {
            // Start with viewport covering whole window
            GI.Viewport(0, 0, fViewWidth, fViewHeight);

            // Create an orthographic view so we can draw
            // using 2D coordinates and not really worry about the
            // camera position
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            GI.Ortho(0, fViewWidth, 0, fViewHeight, -1.0, 1.0);

            // Clear the color buffer to an unlikely color so
            // that we can see where the drawing is NOT occuring
            GI.Buffers.ColorBuffer.Color = new ColorRGBA(1, 0, 0, 1); ;
            GI.Buffers.ColorBuffer.Clear();

            // If the resolution of the mesh changed since the last time we were
            // drawing, recreate the mesh using the newest dimensions.
            if (fResolutionChanged)
            {
                Console.WriteLine("Block Size: {0}", fBlockSize);
                fResolutionChanged = false;
                fResolution = new Resolution(fVideoTexture.Width / fBlockSize, fVideoTexture.Height / fBlockSize);
                fFaceMesh = new XYAxesMesh(fFaceSize, fResolution, fYTexture);
            }
        }

        /// <summary>
        /// This is called by the rendering system automatically.
        /// 
        /// We want to do the channel separation, and then the drawing
        /// of the quads.
        /// </summary>
        protected override void  DrawContent()
        {
            SeparateChannels();

            DrawQuads();
        }
        #endregion

        #region Reacting to events
        protected override void OnReleaseContext()
        {
            fVideoTexture.Stop();

            base.OnReleaseContext();
        }

        /// <summary>
        /// This is called when the window resizes, and therefore the size
        /// of the client area changes.
        /// </summary>
        /// <param name="w">The new width of the client area</param>
        /// <param name="h">The new height of the client area</param>
        public override void OnSetViewport(int w, int h)
        {
            fViewWidth = w;
            fViewHeight = h;
        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {

            switch (kbde.VirtualKeyCode)
            {
                case VirtualKeyCodes.PageUp:
                    if (fBlockSize > 1)
                    {
                        fBlockSize -= 1;
                        fResolutionChanged = true;
                    }
                    break;

                case VirtualKeyCodes.PageDown:
                    if (fBlockSize <= fVideoTexture.Width / 2)
                    {
                        fBlockSize += 1;
                        fResolutionChanged = true;
                    }
                    break;
            }

            // The following actions are only triggered on KeyUp.
            // This gives us finer control of when they are fired.
            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.F:
                        polyFillMode = PolygonMode.Fill;
                        break;
                    case VirtualKeyCodes.L:
                        polyFillMode = PolygonMode.Line;
                        break;
                    
                    case VirtualKeyCodes.Up:
                        if (fBlockSize > 1)
                        {
                            fBlockSize -= 1;
                            fResolutionChanged = true;
                        }
                        break;

                    case VirtualKeyCodes.Down:
                        if (fBlockSize <= fVideoTexture.Width / 2)
                        {
                            fBlockSize += 1;
                            fResolutionChanged = true;
                        }
                        break;
                }
            }

            return base.OnKeyboardActivity(sender, kbde);
        }
        #endregion
    }
}