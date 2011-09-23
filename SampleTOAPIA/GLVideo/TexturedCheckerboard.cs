using System;
using System.Collections.Generic;
using System.Text;

namespace QuadVideo
{
    using System.Drawing;

    using NewTOAPIA;
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;
    using NewTOAPIA.Modeling;

    public class TexturedCheckerboard : GLRenderable
    {
        GLTexture fTexture1;
        GLTexture fTexture2;

        public Vector3f Translation { get; set; }
        public Vector3f Rotation { get; set; }

        public Size PixelSize;
        public Size BlockSize;
        int fWidth;
        int fHeight;
        float xdiff;
        float ydiff;

        public TexturedCheckerboard(Size pixelSize, Size blockSize, GLTexture tex1, GLTexture tex2)
        {
            Translation = new Vector3f();
            Rotation = new Vector3f();

            PixelSize = pixelSize;
            BlockSize = blockSize;

            fTexture1 = tex1;
            fTexture2 = tex2;
        }


        public void SetSplits(int splits)
        {
            fWidth = splits;
            fHeight = splits;

            xdiff = PixelSize.Width / (float)fWidth;
            ydiff = PixelSize.Height / (float)fHeight;
        }

        #region Drawing Helpers
        void DrawQuad(GraphicsInterface GI, float left, float top, float right, float bottom)
        {
            GI.PushMatrix();
            GI.Rotate(Rotation);

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

            GI.PopMatrix();
        }
        #endregion

        #region GLRenderable
        protected override void BeginRender(GraphicsInterface GI)
        {
            GI.Features.Texturing2D.Enable();
            GI.TexEnv(TextureEnvModeParam.Decal);

            GI.FrontFace(FrontFaceDirection.Cw);
            GI.PolygonMode(GLFace.Front, PolygonMode.Fill);
        }

        protected override void RenderContent(GraphicsInterface gi)
        {
            float left = 0;
            float bottom = 0;

            float top = 0;
            float right = 0;


            // Repeatedly swapping the Bind() call between multiple textures
            // can be slow on some gl implementations.  So, we bind one texture
            // and do all the drawing we need to do with that texture, then swap
            // to the next texture.
            // First Draw everything with the first texture
            fTexture1.Bind();
            for (int row = 0; row < fHeight; row++)
            {
                for (int column = 0; column < fWidth; column++)
                {
                    bottom = Translation.y + (ydiff * row);
                    left = Translation.x + (xdiff * column);
                    top = bottom + (ydiff);
                    right = left + (xdiff);

                    // Create a checker pattern by varying the texture
                    // depending on row and column.
                    if (row % 2 == 0 && column % 2 == 0)
                    {
                        DrawQuad(gi, left, top, right, bottom);
                    }
                    else
                    {
                        if ((column % 2) != 0)
                        {
                            DrawQuad(gi, left, top, right, bottom);
                        }
                    }
                }
            }
            fTexture1.Unbind();


            // Now draw the squares that use the second texture
            fTexture2.Bind();
            for (int row = 0; row < fHeight; row++)
            {
                for (int column = 0; column < fWidth; column++)
                {
                    bottom = Translation.y + (ydiff * row);
                    left = Translation.x + (xdiff * column);
                    top = bottom + (ydiff);
                    right = left + (xdiff);

                    // Create a checker pattern by varying the texture
                    // depending on row and column.
                    if (row % 2 == 0)
                    {
                        if ((column % 2) == 0)
                        {
                        }
                        else
                        {
                            DrawQuad(gi, left, top, right, bottom);
                        }
                    }
                    else
                    {
                        if ((column % 2) == 0)
                        {
                            DrawQuad(gi, left, top, right, bottom);
                        }
                    }
                }
            }
        }

        protected override void EndRender(GraphicsInterface GI)
        {
            GI.Features.Texturing2D.Disable();
        }
        #endregion
    }
}
