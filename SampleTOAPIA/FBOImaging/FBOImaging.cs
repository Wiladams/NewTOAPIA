

namespace FBOImaging
{
    using System;

    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;
    using NewTOAPIA.Drawing;

    using TOAPI.OpenGL;


    public class SampleDraw : GLModel
    {
        // These variables control the current mode
        public static int CurrentMode = 0;
        public const int NumModes = 5;

        // These variables set the dimensions of the rectanglar region we wish to view.
        int fWindowWidth;
        int fWindowHeight;

        int fOffscreenWidth=640;
        int fOffscreenHeight=480;

        // Things that are changed by keyboard actions
        bool animate = true;
        float interval = 10;
        float roty = 0;

        // The offscreen buffer used for rendering
        GLRenderTarget fRenderTarget;

        // Objects used for rendering the scene
        GLTexture aTexture;
        TexturedCube aCube;
        GLSLShaderProgram fFixedPipeline;
        GLSLShaderProgram fImageProcProgram;
        
        int fDistance = 1;
        int curker=0;   // Which kernel to use for the convolution filter
        float[][] kernels = {
		new float[]{ 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, /* Identity */
		new float[]{ 0.0f,-1.0f, 0.0f,-1.0f, 5.0f,-1.0f, 0.0f,-1.0f, 0.0f}, /* Sharpen */
		new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f}, /* Blur */
		new float[]{ 1.0f, 2.0f, 1.0f, 2.0f, 4.0f, 2.0f, 1.0f, 2.0f, 1.0f}, /* Gaussian blur */
		new float[]{ 0.0f, 0.0f, 0.0f,-1.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f}, /* Edge enhance */
		new float[]{ 1.0f, 1.0f, 1.0f, 1.0f, 8.0f, 1.0f, 1.0f, 1.0f, 1.0f}, /* Edge detect */
		new float[]{ 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f,-1.0f}  /* Emboss */
	    };

        public SampleDraw()
        {
            aCube = new TexturedCube();
        }


        protected override void  OnSetContext()
        {
            // When debugging, it's nice to turn on the following check.
            // If you do, whenever there is an error while calling one of the 
            // underlying OpenGL functions, a GLException will be thrown.
            // If this flag is not set, then errors will be silently passed by and
            // rendering will continue.
            //GraphicsInterface.gCheckErrors = true;



            // Enable a couple of features for nice rendering.
            // For this program, Texturing, and DepthTest are essential.
            // The hint for perspective correction is optional
            GI.Features.Texturing2D.Enable();
            GI.Features.DepthTest.Enable();
            GI.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            // Set an overall color buffer background color of Invisible,
            // so when we clear the color buffer, it starts out blank.
            GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;

            // Create the offscreen render target
            fRenderTarget = new GLRenderTarget(GI, fOffscreenWidth, fOffscreenHeight);

            // Create the texture object that we will be using to do normal rendering.
            // 
            aTexture = TextureHelper.CreateTextureFromFile(GI, "tex.png", false);

            // Create the shader programs using the easy static method that
            // takes vertex and fragment shader strings.
            fFixedPipeline = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, ShaderStrings.FixedVert, ShaderStrings.FixedFrag);
            fImageProcProgram = GLSLShaderProgram.CreateUsingVertexAndFragmentStrings(GI, ShaderStrings.FixedVert, ShaderStrings.ConvolutionFrag);

            // This is an alternate method of creating the shader programs.
            // You get absolute control of the individual pieces as they 
            // are created, at the cost of brevity
            //GLSLVertexShader fixedVertexShader = new GLSLVertexShader(GI, ShaderStrings.FixedVert);
            //GLSLFragmentShader fixedFragmentShader = new GLSLFragmentShader(GI, ShaderStrings.FixedFrag);
            //GLSLFragmentShader convolutionFragmentShader = new GLSLFragmentShader(GI, ShaderStrings.ConvolutionFrag);

            //// Create the pipeline shader program
            //fFixedPipeline = new GLSLShaderProgram(GI);
            //fFixedPipeline.AttachShader(fixedVertexShader);
            //fFixedPipeline.AttachShader(fixedFragmentShader);
            //fFixedPipeline.Link();

            //// Create the convolution shader program
            //fImageProcProgram = new GLSLShaderProgram(GI);
            //fImageProcProgram.AttachShader(fixedVertexShader);
            //fImageProcProgram.AttachShader(convolutionFragmentShader);
            //fImageProcProgram.Link();

        }

        protected override void DrawBegin()
        {
            // We don't do anything in here
            // instead, everything happens in the 
            // DrawContent() method
        }

        protected override void  DrawContent()
        {
            // If we're animating, then we want to rotate the
            // cubes about their y-axis.  Each time through the loop,
            // we rotate by the number of degrees indicated by the interval
            // for faster rotations, increase the interval.
            if (animate)
                roty += 0.1f * interval;
            
            #region First pass, render into the frame buffer
            // Start by binding the RenderTarget object.  This will switch
            // all subsequent drawing to occur in the offscreen buffer of the 
            // render target.
            fRenderTarget.Bind();

            // Clear the color and depth buffers
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();

            // Bind the fixed pipeline shader program.
            // In this program, all the really interesting processing happens as
            // a post processing image operation.  That's where we use the Convolution 
            // shader.  Here, we just want to put something interesting into the buffer
            // so later rendering will have something to play with.  So, we use
            // this simple shader which does nothing more than pass along the Vertex
            // an color information untouched.
            fFixedPipeline.Bind();

            // Setup a perspective viewing position.
            // First, load the indentity matrix into the 
            // projection matrix.
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            // Then, setup a perspective view.  This is as opposed to a Orthographic view, which 
            // is used for 2D drawing.  The field of view is 45 degrees, and the aspect ratio
            // is that provided by the width and height of the offscreen buffer.
            // Near and far are some reasonable values that we know will not be exceeded by 
            // our model's coordinates.
            GI.Glu.Perspective(45.0f, (float)fOffscreenWidth / (float)fOffscreenWidth, 0.1f, 100.0f);
            GI.MatrixMode(MatrixMode.Modelview);
            

            // Move the camera to a good viewing position
            GI.LoadIdentity();
            GI.Translate(2.0f, 0.0f, -15);

            // Bind the texture that we want to be displayed on all the 
            // surfaces of the cube.
            aTexture.Bind();

            // Draw some cubes
            int i;
            for (i = -2; i <= 3; i++)
            {
                GI.PushMatrix();
                    GI.Translate(i * 2.0f, 0.0f, (float)(i * (-4.0)));
                    GI.Rotate(roty + i, 0.0f, 1.0f, 0.0f);
                    aCube.Render(GI);
                GI.PopMatrix();
            }

            // Unbind the render target to indicate we no longer want
            // drawing to go to the offscreen buffer.
            fRenderTarget.Unbind();

            #endregion

            #region Second pass, render to the screen
            // At this point, we have an interesting image sitting in the 
            // offscreen frame buffer.  We can get at the image as a texture
            // using the 'ColorBuffer' property of the RenderTarget.
            // 
            // We will use that ColorBuffer to draw to a QUAD on the screen
            // applying the Convolution shader program along the way.  This is
            // where we actually do the 'image processing' of the program.

            // Now we're clearing the buffer for the screen
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();

            // Get a handle on the ColorBuffer.
            // There is no real need to get this as a reference.
            // This code is here just to emphasize the fact that you're
            // dealing with an ordinary texture object at this point.
            GLTexture imageTexture = fRenderTarget.ColorBuffer;
            imageTexture.Bind();
            
            // Bind the image processing program.  This will inject
            // it into the rendering pipeline.  Also, we need to 
            // bind it before we can set variables within the program.
            fImageProcProgram.Bind();

            // Set the necessary variables in the program.  We let the program
            // know the width and height of the buffer so the kernel knows how
            // far it can go to get the neighboring pixels.
            // The "Kernel" variable is the 3x3 matrix that represents the 
            // actual convolution filter.
            fImageProcProgram["Width"].Set(fOffscreenWidth);
            fImageProcProgram["Height"].Set(fOffscreenHeight);
            fImageProcProgram["Dist"].Set(fDistance);
            int loc = fImageProcProgram.GetUniformLocation("Kernel");
            GI.UniformMatrix3(loc, 1, false, kernels[curker]);
            
            // Setup a Ortho projection matrix
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            GI.Ortho(0, fWindowWidth, 0, fWindowHeight, -1, 1);

            // We start our model view with the identity matrix
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();

            GI.Drawing.Quads.Begin();
                // Left bottom
                GI.TexCoord(0.0f, 0.0f);
                GI.Vertex(0, 0, 0.0f);

                // Right bottom
                GI.TexCoord(1.0f, 0.0f);
                GI.Vertex(fWindowWidth, 0);

                // Right top
                GI.TexCoord(1.0f, 1.0f);
                GI.Vertex(fWindowWidth, fWindowHeight);

                // Left top
                GI.TexCoord(0.0f, 1.0f);
                GI.Vertex(0, fWindowHeight);
            GI.Drawing.Quads.End();

            // Unbind the texture because this is what
            // we usually do when we're done using a texture.
            imageTexture.Unbind();
            #endregion
        }

        /// <summary>
        /// This is called when the window resizes.  We make note of the 
        /// new size of the window, but we're not using it to do anything
        /// interesting.
        /// 
        /// We could use this information in the second pass of rendering to 
        /// center the Quad on the screen, and scale it to match the size of the window
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        public override void OnSetViewport(int w, int h)
        {
            fWindowWidth = w;
            fWindowHeight = h;
        }

        public override IntPtr  OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {

            if (kbde.AcitivityType == KeyActivityType.KeyDown)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.Left:
                        fDistance -= 1;
                        if (fDistance < 1)
                            fDistance = 1;
                        break;
                    case VirtualKeyCodes.Right:
                        fDistance += 1;
                        break;
                    case VirtualKeyCodes.Down:
                        fDistance = 1;
                        break;

                }
            }

            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.Space:
                        animate = !animate;
                        break;

                    case VirtualKeyCodes.D0:
                    case VirtualKeyCodes.NumPad0:
                        curker = 0;
                        //Console.WriteLine("No postprocessing\n");
                        break;

                    case VirtualKeyCodes.D1:
                    case VirtualKeyCodes.NumPad1:
                        curker = 1;
                        //Console.WriteLine("Sharpen filter\n");
                        break;

                    case VirtualKeyCodes.D2:
                    case VirtualKeyCodes.NumPad2:
                        curker = 2;
                        //Console.WriteLine("Blur filter\n");
                        break;

                    case VirtualKeyCodes.D3:
                    case VirtualKeyCodes.NumPad3:
                        curker = 3;
                        //Console.WriteLine("Gaussian blur filter\n");
                        break;

                    case VirtualKeyCodes.D4:
                    case VirtualKeyCodes.NumPad4:
                        curker = 4;
                        //Console.WriteLine("Edge enhance filter\n");
                        break;

                    case VirtualKeyCodes.D5:
                    case VirtualKeyCodes.NumPad5:
                        curker = 5;
                        //Console.WriteLine("Edge detect filter\n");
                        break;

                    case VirtualKeyCodes.D6:
                    case VirtualKeyCodes.NumPad6:
                        curker = 6;
                        //Console.WriteLine("Emboss filter\n");
                        break;

                }
            }

            return base.OnKeyboardActivity(sender, kbde);

        }

    }
}