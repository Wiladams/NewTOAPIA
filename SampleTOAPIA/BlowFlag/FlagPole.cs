using System;
using System.Collections.Generic;

using NewTOAPIA.GL;
using TOAPI.OpenGL;

class FlagPole : GLRenderable
{
    float fxbase;
    float fybase;
    float fzbase;
    float fHeight;
    float fSize;

    public FlagPole(float xbase, float ybase, float zbase, float height, float size)
    {
        fxbase = xbase;
        fybase = ybase;
        fzbase = zbase;
        fHeight = height;
        fSize = size;
    }

    protected override void RenderContent(GraphicsInterface gi)
    {
        gi.Normal(0, 0, 1);

        gi.Begin(BeginMode.Lines);
        gi.Vertex(fxbase, fybase - 5, fzbase);
        gi.Vertex(fxbase, fybase + fHeight * fSize + 0.5f, fzbase);
        gi.End();
    }

}

