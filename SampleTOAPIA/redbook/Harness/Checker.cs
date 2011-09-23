
using System;

using TOAPI.OpenGL;
using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;

public class Checker : GLModel
{
    private const int CHECKIMAGEWIDTH = 64;
    private const int CHECKIMAGEHEIGHT = 64;
    private const int CHECKIMAGEBLOCKSIZE = 8;
    GLTexture fTexture;


    public Checker()
        : base()
    {
    }

    protected override void OnSetContext()
    {
        //fTexture = new GLTexture(CHECKIMAGEWIDTH, CHECKIMAGEHEIGHT);
        if (fTexture == null)
            fTexture = TextureHelper.CreateCheckerboardTexture(GI, CHECKIMAGEWIDTH, CHECKIMAGEHEIGHT, CHECKIMAGEBLOCKSIZE);
    }

    protected override void DrawBegin()
    {
        GI.Viewport(0, 0, ViewportWidth, ViewportHeight);

        GI.MatrixMode(MatrixMode.Projection);
        GI.LoadIdentity();
        
        GI.Glu.Perspective(60.0, (float)ViewportWidth / (float)ViewportHeight, 1.0, 30.0);
        GI.MatrixMode(MatrixMode.Modelview);
        GI.LoadIdentity();
        
        GI.Translate(0.0f, 0.0f, -3.6f);

        GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
        GI.Features.DepthTest.Enable();
        GI.Features.Texturing2D.Enable();
        //GLShadingModel.Flat.Realize();

        fTexture.Bind();
        //GLTextureParameters texParams = new GLTextureParameters(TextureMagFilter.Nearest, TextureMinFilter.Nearest, TextureWrapMode.Repeat);
        //GI.SetTexture2DParameters(texParams);
        //GI.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
        //GI.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
        //GI.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Nearest);
        //GI.TexParameter(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMagFilter.Nearest);

        GI.TexEnv(TextureEnvModeParam.Decal);

        GI.Buffers.ColorBuffer.Clear();
        GI.Buffers.DepthBuffer.Clear();
    }

    protected override void DrawContent()
    {

        GI.Drawing.Quads.Begin();
            GI.TexCoord(0.0f, 0.0f); GI.Vertex(-2.0f, -1.0f, 0.0f);
            GI.TexCoord(0.0f, 1.0f); GI.Vertex(-2.0f, 1.0f, 0.0f);
            GI.TexCoord(1.0f, 1.0f); GI.Vertex(0.0f, 1.0f, 0.0f);
            GI.TexCoord(1.0f, 0.0f); GI.Vertex(0.0f, -1.0f, 0.0f);

            GI.TexCoord(0.0f, 0.0f); GI.Vertex(1.0f, -1.0f, 0.0f);
            GI.TexCoord(0.0f, 1.0f); GI.Vertex(1.0f, 1.0f, 0.0f);
            GI.TexCoord(1.0f, 1.0f); GI.Vertex(2.41421f, 1.0f, -1.41421f);
            GI.TexCoord(1.0f, 0.0f); GI.Vertex(2.41421f, -1.0f, -1.41421f);
        GI.Drawing.Quads.End();
    }

    protected override void DrawEnd()
    {
        fTexture.Unbind();
        GI.Flush();
    }
}