using System;

using NewTOAPIA;
using NewTOAPIA.GL;

using AGUIL;
using AGUIL.Media;

namespace PointSprites
{
    public class StarField2D : GLRenderable
    {
        GraphicsInterface fGI;
        int fNumStars;
        float fPointSize;
        Vector2i fFieldSize;
        float2[] fStars;
        GLTexture fTexture;

        public StarField2D(GraphicsInterface gi, int numStars, float pointSize, Vector2i fieldSize)
        {
            Random rand = new Random();

            fGI = gi;
            fNumStars = numStars;
            fPointSize = pointSize;
            fFieldSize = fieldSize;

            fStars = new float2[numStars];
            for (int i = 0; i < fNumStars; i++)
            {
                fStars[i].x = (float)(rand.Next((int)fieldSize.x));
                fStars[i].y = (float)(rand.Next((int)fieldSize.y)) + 100.0f;
            }

            LoadTexture();
        }

        void LoadTexture()
        {
            // Load the star image and create texture
            GLPixelData pBytes;
            pBytes = TargaHandler.CreatePixelDataFromFile("star.tga");
            fTexture = new GLTexture(fGI, pBytes, true);
        }

        protected override void BeginRender(GraphicsInterface gi)
        {
            gi.Features.Blend.Enable();
            gi.BlendFunc(BlendingFactorSrc.SrcColor, BlendingFactorDest.OneMinusSrcColor);

            gi.Features.PointSprite.Enable();
            gi.Features.Blend.Enable();
            gi.Features.Texturing2D.Enable();
            fTexture.MakeCurrent();

            gi.TexEnv(TextureEnvironmentTarget.PointSprite, TextureEnvironmentParameter.CoordReplace, 1);
            gi.TexEnv(TextureEnvModeParam.Decal); 
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            GLAspectPoints pointsAspect = new GLAspectPoints(gi);

            gi.Color(GLColor.White);

            pointsAspect.PointSize = fPointSize;
            gi.Begin(BeginMode.Points);
            for (int i = 0; i < fNumStars; i++)
                gi.Vertex(fStars[i]);
            gi.End();

        }
    }
}
