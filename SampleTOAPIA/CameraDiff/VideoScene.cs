using System;
using System.Drawing;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.GL.Imaging;
using NewTOAPIA.GL.Media;

using NewTOAPIA.UI;
using NewTOAPIA.Media;  // For camera support

//using TOAPI.OpenGL;

namespace QuadVideo
{
    class VideoScene : GLModel
    {
        #region Private Fields
        GLTexture2D fCurrentFrame;

        GLTexture2D fBackgroundFrame;
        RGBToGray fBackgroundCreator;
        UnaryTextureProcessor fBackgroundCopier;
        Pixellate fBackgroundPixellator;

        bool fHaveBackground;
        int fBackgroundCount;

        UnaryTextureProcessor fImageOperator;
        PowerLawTransform fGammaCorrection;
        RGBToGray fGrayConverter;
        LuminanceThreshold fLuminanceThreshold;
        LuminanceBinarizer fLuminanceBinarizer;
        Pixellate fPixellate;
        BlockProcessor fBlocker;
        AverageProcessor fAverager;
        Morph fMorpher;

        // Convolution filters
        ConvolutionProcessor fSobell;
        ConvolutionProcessor fEdgeEnhance;
        ConvolutionProcessor fEmboss;
        ConvolutionProcessor fLaplacian;
        ConvolutionProcessor fSoften;

        // Binary Image filters
        DifferenceProcessor fDifference;

        VideoTexture fCamera1;

        Vector3D fFaceSize;

        XYAxesMesh fGrayMesh;
        XYAxesPointMesh fPointMesh;

        // How we actually display some stuff
        int fViewWidth;
        int fViewHeight;
        float fAlpha;
        float fAlphaStep;
        #endregion

        #region Constructor
        public VideoScene()
        {
            fAlpha = 1.0f;
            fAlphaStep = 0.005f;
        }
        #endregion

        /// <summary>
        /// Once we have a context, we are able to peform various setup routines
        /// that require us to connect to OpenGL.
        /// </summary>
        protected override void OnSetContext()
        {
            //fCamera1 = VideoTexture.CreateFromDeviceIndex(GI, 0, 640, 480);
            fCamera1 = VideoTexture.CreateFromDeviceIndex(GI, 0, true);
        
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
            fFaceSize = new Vector3D(fCamera1.Width, fCamera1.Height, 0);

            fBackgroundCreator = new RGBToGray(GI, fCamera1.Width, fCamera1.Height);
            fBackgroundCopier = new UnaryTextureProcessor(GI, fCamera1.Width, fCamera1.Height);
            fBackgroundPixellator = new Pixellate(GI, fCamera1.Width, fCamera1.Height, 8);
            
            fImageOperator = new UnaryTextureProcessor(GI, fCamera1.Width, fCamera1.Height);
            fGrayConverter = new RGBToGray(GI, fCamera1.Width, fCamera1.Height);
            fGammaCorrection = new PowerLawTransform(GI, fCamera1.Width, fCamera1.Height, 1.0f);
            fLuminanceThreshold = new LuminanceThreshold(GI, fCamera1.Width, fCamera1.Height, 0f);
            fLuminanceBinarizer = new LuminanceBinarizer(GI, fCamera1.Width, fCamera1.Height, 0.3f);
            fLuminanceBinarizer.OverColor = ColorRGBA.Black;
            fLuminanceBinarizer.UnderColor = ColorRGBA.White;
            fPixellate = new Pixellate(GI, fCamera1.Width, fCamera1.Height,8);
            fBlocker = new BlockProcessor(GI, fCamera1.Width, fCamera1.Height, 2);
            fAverager = new AverageProcessor(GI, fCamera1.Width, fCamera1.Height);
            fMorpher = new Morph(GI, fCamera1.Width, fCamera1.Height);

            fEdgeEnhance = new ConvolutionProcessor(GI, fCamera1.Width, fCamera1.Height, ConvolutionKernel.EdgeEnhance);
            fEdgeEnhance.Distance = 4;
            fEmboss = new ConvolutionProcessor(GI, fCamera1.Width, fCamera1.Height, ConvolutionKernel.Emboss);
            fSoften = new ConvolutionProcessor(GI, fCamera1.Width, fCamera1.Height, ConvolutionKernel.GaussianBlur);
            fSoften.Distance = 4;
            fSobell = new ConvolutionProcessor(GI, fCamera1.Width, fCamera1.Height, ConvolutionKernel.Sobell);
            fLaplacian = new ConvolutionProcessor(GI, fCamera1.Width, fCamera1.Height, ConvolutionKernel.Laplacian);

            fDifference = new DifferenceProcessor(GI, fCamera1.Width, fCamera1.Height);



            // We want replace mode because we don't care about
            // the values that are currently in place, we just want
            // to replace them.
            GI.TexEnv(TextureEnvModeParam.Replace);

            fGrayMesh = new XYAxesMesh(GI, new Vector3D(fCamera1.Width, fCamera1.Height, 0), new Resolution(2, 2), null);
            fPointMesh = new XYAxesPointMesh(GI, new Vector3D(fCamera1.Width, fCamera1.Height, 0), new Resolution(fCamera1.Width/4, fCamera1.Height/4), null);
        }

        #region Drawing Content

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
            GI.Features.DepthTest.Disable();

        }

        protected void DoMorph()
        {
            // Morph the base with the current
            fMorpher.Alpha = fAlpha;
            //GLTexture intermediate = fLuminanceThreshold.ProcessTexture(fDifference.ProcessTwoTextures(fBackgroundFrame, fPixellate.ProcessTexture(fCamera1)));
            GLTexture2D finalImage = fMorpher.ProcessTwoTextures(fBackgroundFrame, fCamera1);

            fAlpha += fAlphaStep;
            if (fAlpha >= 1.0f)
            {
                fAlpha = 1.0f;
            }

            if (fAlpha <= 0)
            {
                fAlpha = 0;
            }

            fGrayMesh.Texture = finalImage;
            //fGrayMesh.Texture = intermediate;
            fGrayMesh.Render(GI);

        }

        /// <summary>
        /// This is called by the rendering system automatically.
        /// 
        /// We want to do the channel separation, and then the drawing
        /// of the quads.
        /// </summary>
        protected override void  DrawContent()
        {
            GLTexture finalImage = null;

            //GLTexture finalImage = fCamera1;
            finalImage = fGrayConverter.ProcessTexture(fCamera1);

           // GLTexture grayImage = fGrayConverter.ProcessTexture(fCamera1);
            //GLTexture finalImage = fPixellate.ProcessTexture(fCamera1);

            //GLTexture finalImage = fGammaCorrection.ProcessTexture(processedImge);

            //GLTexture finalImage = fLuminanceThreshold.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1));
            //GLTexture finalImage = fLuminanceBinarizer.ProcessTexture(fCamera1);
            //GLTexture finalImage = fLuminanceThreshold.ProcessTexture(fCamera1);
            //GLTexture finalImage =  fEdgeEnhance.ProcessTexture(fCamera1);
            //GLTexture finalImage = fGrayConverter.ProcessTexture(fEdgeEnhance.ProcessTexture(fCamera1));
            //GLTexture finalImage = fSobell.ProcessTexture(fSoften.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1)));
            finalImage = fSobell.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1)); // Scary
            //GLTexture finalImage = fSobell.ProcessTexture(fCamera1); // Scary
            //GLTexture finalImage = fLuminanceThreshold.ProcessTexture(fPixellate.ProcessTexture(fCamera1));
            //GLTexture finalImage = fPixellate.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1));
            //GLTexture finalImage = fSoften.ProcessTexture(fCamera1);
            //GLTexture finalImage = fLaplacian.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1));
            //GLTexture finalImage = fBlocker.ProcessTexture(fCamera1);

            // Get 30 snapshots before proceeding so we don't start from
            // a blank image.
            if (null == fBackgroundFrame)
            {
                fBackgroundCount++;
                    if (fBackgroundCount < 30)
                        return;

                    fBackgroundFrame = fBackgroundCopier.ProcessTexture(fCamera1);
            }

            //DoMorph();

            //// Get the current image from the camera
            //finalImage = fSobell.ProcessTexture(fPixellate.ProcessTexture(fGrayConverter.ProcessTexture(fCamera1)));

            //finalImage = fDifference.ProcessTwoTextures(fBackgroundFrame, fCamera1);


            fGrayMesh.Texture = finalImage;
            fGrayMesh.Render(GI);

            //fPointMesh.Texture = fCamera1;
            //fPointMesh.Render(GI);
        }
        #endregion

        #region Reacting to events
        protected override void OnReleaseContext()
        {
            fCamera1.Stop();
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
            base.OnKeyboardActivity(sender, kbde);

            float gamma = fGammaCorrection.Gamma;
            float threshold = fLuminanceThreshold.Threshold;

            switch (kbde.VirtualKeyCode)
            {
                case VirtualKeyCodes.PageUp:    // Gamma increase
                    {
                        gamma -= 0.1f;
                    }
                    break;

                case VirtualKeyCodes.PageDown:  // Gamma decrease
                    {
                        gamma = fGammaCorrection.Gamma;
                        gamma += 0.1f;
                    }
                    break;

                case VirtualKeyCodes.Up:
                    {
                        threshold += 0.01f;
                        if (threshold > 1)
                            threshold = 1;
                    }
                    break;

                case VirtualKeyCodes.Down:
                    {
                        threshold -= 0.01f;
                        if (threshold < 0)
                            threshold = 0;
                    }
                    break;
            }

            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.D:
                        fAlphaStep = -fAlphaStep;
                        break;
                }
            }

            fGammaCorrection.Gamma = gamma;
            fLuminanceThreshold.Threshold = threshold;
            fLuminanceBinarizer.Threshold = threshold;

            Console.WriteLine("Gamma: {0}  Threshold: {1}", gamma, threshold);

            return IntPtr.Zero;
        }
        #endregion
    }
}