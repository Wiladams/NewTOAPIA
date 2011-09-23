using System;

using NewTOAPIA.GL;
using NewTOAPIA.UI;

using TOAPI.OpenGL;

namespace PixelViewer
{
    class PixelDisplayModel : GLModel
    {
        GLPixelData fPixmap;
        GLPixelData fModifiedPixels;

        bool fFillWindow;
        
        bool fUseRed;
        bool fUseGreen;
        bool fUseBlue;
        bool fUseLuminance;

        int fRenderMode;
        const int lastmode = 3;

        public PixelDisplayModel()
        {
            fFillWindow = false;

            fUseRed = true;
            fUseGreen = true;
            fUseBlue = true;
            fUseLuminance = false;

            fRenderMode = 1;
            fModifiedPixels = null;
        }

        protected override void OnSetContext()
        {
            GI.PixelStore(PixelStore.UnpackAlignment, 1);

            //fPixmap = TargaHandler.CreatePixelDataFromFile("ground.tga");
            //fPixmap = TargaHandler.CreatePixelDataFromFile("cube.tga");
            //fPixmap = TargaHandler.CreatePixelDataFromFile("fire.tga");
            fPixmap = TargaHandler.CreatePixelDataFromFile("horse.tga");
            //fPixmap = TargaHandler.CreatePixelDataFromFile("moon.tga");
            //fPixmap = TargaHandler.CreatePixelDataFromFile("star.tga");
            //fPixmap = TargaHandler.CreatePixelDataFromFile("orb.tga");

        }

        void CreateGrayscaleImage()
        {
            GLDraw drawer = new GLDraw(GI);

            byte[] lumdata = new byte[fPixmap.Width * fPixmap.Height];
            fModifiedPixels = new GLPixelData(fPixmap.Width, fPixmap.Height, TextureInternalFormat.Luminance8, GLPixelFormat.Luminance, PixelType.UnsignedByte, lumdata);

            // Make sure we get full color first
            gl.glPixelTransferf(gl.GL_RED_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_GREEN_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_BLUE_SCALE, 1.0f);

            //    // First draw image into color buffer
            GI.DrawPixels(0, 0, fPixmap);

            //    // Scale colors according to NSTC standard
            gl.glPixelTransferf(gl.GL_RED_SCALE, 0.3f);
            gl.glPixelTransferf(gl.GL_GREEN_SCALE, 0.59f);
            gl.glPixelTransferf(gl.GL_BLUE_SCALE, 0.11f);

            //    // Read pixles into buffer (scale above will be applied)
            GI.ReadPixels(0, 0, fModifiedPixels);

            //    // Return color scaling to normal
            gl.glPixelTransferf(gl.GL_RED_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_GREEN_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_BLUE_SCALE, 1.0f);
        }

        void AdjustColor()
        {
            if (!fUseRed && !fUseBlue && !fUseGreen)
            {
                fUseLuminance = true;

                CreateGrayscaleImage();
            }
            else
            {
                fUseLuminance = false;
                // Color components
                if (fUseRed)
                    gl.glPixelTransferf(gl.GL_RED_SCALE, 1.0f);
                else
                    gl.glPixelTransferf(gl.GL_RED_SCALE, 0.0f);

                if (fUseGreen)
                    gl.glPixelTransferf(gl.GL_GREEN_SCALE, 1.0f);
                else
                    gl.glPixelTransferf(gl.GL_GREEN_SCALE, 0.0f);

                if (fUseBlue)
                    gl.glPixelTransferf(gl.GL_BLUE_SCALE, 1.0f);
                else
                    gl.glPixelTransferf(gl.GL_BLUE_SCALE, 0.0f);
            }
        }

        protected override void DrawContent()
        {
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();

            GI.RasterPos2i(0, 0);

            switch (fRenderMode)
            {
                case 2:     // Flip the pixels
                    GI.PixelZoom(-1.0f, -1.0f);
                    GI.RasterPos2i(fPixmap.Width, fPixmap.Height);
                    break;

                case 3:     // Invert colors
                    {
                        float[] invertMap = new float[256];
                        invertMap[0] = 1.0f;
                        for (int i = 1; i < 256; i++)
                            invertMap[i] = 1.0f - (1.0f / 255.0f * (float)i);

                        gl.glPixelMapfv(gl.GL_PIXEL_MAP_R_TO_R, 255, invertMap);
                        gl.glPixelMapfv(gl.GL_PIXEL_MAP_G_TO_G, 255, invertMap);
                        gl.glPixelMapfv(gl.GL_PIXEL_MAP_B_TO_B, 255, invertMap);
                        gl.glPixelTransferi(gl.GL_MAP_COLOR, gl.GL_TRUE);
                    }
                    break;

                case 1:     // Just do a plain old image copy
                default:
                    // This line intentially left blank
                    break;
            }

            // Zooming
            if (fFillWindow)
            {
                int[] iViewport = new int[8];
                GI.GetInteger(GetTarget.Viewport, iViewport);
                GI.PixelZoom((float)iViewport[2] / (float)fPixmap.Width, (float)iViewport[3] / (float)fPixmap.Height);
            }

            AdjustColor();

            if (!fUseLuminance)
            {
                GI.DrawPixels(fPixmap.Width, fPixmap.Height, fPixmap.PixelFormat, fPixmap.PixelType, fPixmap.Pixels);
            } 
            else
            {
                gl.glDrawPixels(fModifiedPixels.Width, fModifiedPixels.Height, gl.GL_LUMINANCE, gl.GL_UNSIGNED_BYTE, fModifiedPixels.Pixels);
            }

            // Reset everyting to default
            gl.glPixelTransferi(gl.GL_MAP_COLOR, gl.GL_FALSE);
            gl.glPixelTransferf(gl.GL_RED_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_GREEN_SCALE, 1.0f);
            gl.glPixelTransferf(gl.GL_BLUE_SCALE, 1.0f);
            gl.glPixelZoom(1.0f, 1.0f);                    // No Pixel Zooming
        }


        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.R:
                        fUseRed = !fUseRed;
                        break;

                    case VirtualKeyCodes.G:
                        fUseGreen = !fUseGreen;
                        break;
                    
                    case VirtualKeyCodes.B:
                        fUseBlue = !fUseBlue;
                        break;
                    
                    // Zoom into pixels to fill the window
                    case VirtualKeyCodes.Z:
                        fFillWindow = !fFillWindow;
                        break;

                        // Change what's rendered
                    case VirtualKeyCodes.Space:
                        fRenderMode += 1;
                        if (fRenderMode > lastmode)
                            fRenderMode = 1;
                        break;

                    case VirtualKeyCodes.PrintScreen:
                        // Print Screen
                        break;
                }

                
            }

            return IntPtr.Zero;
        }

        public override void OnSetViewport(int w, int h)
        {
            GI.Viewport(0, 0, w, h);
            
            h = (h == 0) ? 1 : h;
            w = (w == 0) ? 1 : w;

            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            GI.Glu.Ortho2D(0, w, 0, h);
        }
    }
}

