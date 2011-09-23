using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using AGUIL;
using AGUIL.Media;
using TOAPI.OpenGL;

namespace PointSprites
{
    class SpriteScene : GLModel
    {
        public const int SCREEN_X = 800;
        public const int SCREEN_Y = 600;

        public const int SMALL_STARS = 500;
        public const int MEDIUM_STARS = 40;
        public const int LARGE_STARS = 15;

        StarField2D fSmallStars;
        StarField2D fMediumStars;
        StarField2D fLargeStars;


        //int drawMode;       // Normal points

        GLTexture fMoonTexture;
        SimpleHorizon fHorizon;

        public SpriteScene()
        {
            fHorizon = new SimpleHorizon();

            //drawMode = 3;
        }

        protected override void OnSetContext()
        {
            fSmallStars = new StarField2D(GI, SMALL_STARS, 7.0f, new Vector2i(SCREEN_X, SCREEN_Y));
            fMediumStars = new StarField2D(GI, MEDIUM_STARS, 12.0f, new Vector2i(SCREEN_X * 10, SCREEN_Y));
            fLargeStars = new StarField2D(GI, LARGE_STARS, 20.0f, new Vector2i(SCREEN_X * 10, SCREEN_Y));

            // Load moon texture
            GLPixelData pBytes;
            pBytes = TargaHandler.CreatePixelDataFromFile("moon.tga");
            fMoonTexture = new GLTexture(GI, pBytes, true);
        }

        //void ProcessCommand(int value)
        //{
        //    drawMode = value;

        //    switch (value)
        //    {
        //        case 1:
        //            // Turn off blending and all smoothing
        //            GLC.Features.Blend.Disable();
        //            GLC.Features.LineSmooth.Disable();
        //            GLC.Features.PointSmooth.Disable();
        //            GLC.Features.Texturing2D.Disable();
        //            GLC.Features.PointSprite.Disable();
        //            break;

        //        case 2:
        //            // Turn on antialiasing, and give hint to do the best
        //            // job possible.
        //            gl.glBlendFunc(gl.GL_SRC_ALPHA, gl.GL_ONE_MINUS_SRC_ALPHA);
        //            GI.Enable(GLEnable.Blend);
        //            GLC.Features.PointSmooth.Enable();
        //            GLC.Features.PointSmooth.Hint = HintMode.Nicest;
        //            GLC.Features.LineSmooth.Enable();
        //            GLC.Features.LineSmooth.Hint = HintMode.Nicest;
        //            GLC.Features.Texturing2D.Disable();
        //            GLC.Features.PointSprite.Disable();
        //            break;

        //        default:
        //            break;
        //    }
        //}

        protected override void DrawBegin()
        {
            // Sky background color
            GI.Buffers.ColorBuffer.Color = GLColor.Black;


            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();
        }

        protected override void DrawContent()
        {
            float2 moonLocation = new float2(700, 500);

            // Draw star fields
            fSmallStars.Render(GI);
            fMediumStars.Render(GI);
            fLargeStars.Render(GI);


            // Draw the moon
            GI.Drawing.Points.PointSize = 120.0f;
            GI.Features.Blend.Disable();
            fMoonTexture.MakeCurrent();

            // Set texture environment
            gl.glTexEnvi(gl.GL_POINT_SPRITE, gl.GL_COORD_REPLACE, gl.GL_TRUE);
            gl.glTexEnvi(gl.GL_TEXTURE_ENV, gl.GL_TEXTURE_ENV_MODE, gl.GL_DECAL);

            GI.Drawing.Points.Begin();
            GI.Drawing.AddVertex(moonLocation);
            GI.Drawing.Points.End();

            // Turn off point sprite 
            // and texturing
            GI.Features.PointSprite.Disable();
            GI.Features.Texturing2D.Disable();

            fHorizon.Render(GI);

        }

        protected override void DrawEnd()
        {
            GI.Flush();
        }

        public override void OnKeyboardActivity(object sender, NewTOAPIA.UI.KeyboardActivityArgs kbde)
        {
            //if (kbde.EventType == KeyEventType.KeyUp)
            //{
            //    GI.Redisplay();
            //}
        }

        public override void OnSetViewport(int w, int h)
        {
        
            // Prevent a divide by zero
            h = (h == 0) ? 1 : h;
            w = (w == 0) ? 1 : w;

            GI.Viewport(0, 0, w, h);

            // Reset projection matrix stack
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            // Establish clipping volume (left, right, bottom, top, near, far)
            GI.Glu.Ortho2D(0, SCREEN_X, 0, SCREEN_Y);
        }
    }
}

