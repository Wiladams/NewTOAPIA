using System;

using TOAPI.OpenGL;
using NewTOAPIA.Drawing;
using NewTOAPIA.Graphics;
using NewTOAPIA.GL;
using NewTOAPIA;
using NewTOAPIA.Modeling;

/* 
 * 2008: Converted to C# by William Adams (wiladams@microsoft.com)
 * 1996: "written" by Danny Sung <dannys@ucla.edu>
*       Based on 3-D gear wheels by Brian Paul which is in the public domain.
*/

public class WireframeGear : GLRenderable
{
    float fInnerRadius;
    float fOuterRadius;
    float fWidth;
    int fTeeth;
    float fToothDepth;

    float r0, r1, r2;
    float angle, da;

    ColorRGBA fColor;

    GraphicsInterface fGI;
    GLDisplayList fDisplayList;

    /*-
     * Draw a gear wheel.  You'll probably want to call this function when
     * building a display list since we do a lot of trig here.
     *
     * Input:  inner_radius - radius of hole at center
     *         outer_radius - radius at center of teeth
     *         width - width of gear
     *         teeth - number of teeth
     *         tooth_depth - depth of tooth
     *         wire - true for wireframe mode
     */
    public WireframeGear(GraphicsInterface gi, float innerRadius, float outerRadius, float width, int teeth, float toothdepth)
    {
        fGI = gi;
        fDisplayList = new GLDisplayList(gi);
        fInnerRadius = innerRadius;
        fOuterRadius = outerRadius;
        fWidth = width;
        fTeeth = teeth;
        fToothDepth = toothdepth;

        r0 = fInnerRadius;
        r1 = fOuterRadius - fToothDepth / 2.0f;
        r2 = fOuterRadius + fToothDepth / 2.0f;

        da = 2.0f * (float)Math.PI / fTeeth / 4.0f;

        fDisplayList.BeginCompile();
        RenderContent(fGI);
        fDisplayList.End();
    }

    public ColorRGBA Color
    {
        get { return fColor; }
        set
        {
            fColor = value;
        }
    }

    void RenderFrontFacesOfTeeth(GraphicsInterface gi)
    {
        // draw front face
        for (int i = 0; i <= fTeeth; i++)
        {
            gi.Drawing.Lines.Begin();
            angle = i * 2.0f * (float)Math.PI / fTeeth;

            gi.Vertex(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), fWidth * 0.5f);
            gi.Vertex(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), fWidth * 0.5f);

            gi.Vertex(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), fWidth * 0.5f);
            gi.Vertex(r1 * (float)Math.Cos(angle + 4 * da), r1 * (float)Math.Sin(angle + 4 * da), fWidth * 0.5f);

            gi.Drawing.Lines.End();
        }
    }

    void RenderBackFacesOfTeeth(GraphicsInterface gi)
    {
        //// draw back face
        for (int i = 0; i <= fTeeth; i++)
        {
            angle = i * 2.0f * (float)Math.PI / fTeeth;
            gl.glBegin(gl.GL_LINES);
            gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -fWidth * 0.5f);
            gl.glVertex3f(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -fWidth * 0.5f);

            gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -fWidth * 0.5f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle + 4 * da), r1 * (float)Math.Sin(angle + 4 * da), -fWidth * 0.5f);
            gl.glEnd();
        }

    }

    void RenderFrontSidesOfTeeth(GraphicsInterface gi)
    {
        //// draw front sides of teeth
        for (int i = 0; i < fTeeth; i++)
        {
            angle = i * 2.0f * (float)Math.PI / fTeeth;

            gl.glBegin(gl.GL_LINE_LOOP);
            gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), fWidth * 0.5f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), fWidth * 0.5f);
            gl.glEnd();
        }

    }

    void RenderBackSidesOfTeeth(GraphicsInterface gi)
    {
        //// draw back sides of teeth
        for (int i = 0; i < fTeeth; i++)
        {
            angle = i * 2.0f * (float)Math.PI / fTeeth;

            gl.glBegin(gl.GL_LINE_LOOP);
            gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), -fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), -fWidth * 0.5f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -fWidth * 0.5f);
            gl.glEnd();
        }
    }

    void RenderOutwardFacesOfTeeth(GraphicsInterface gi)
    {
        float u, v, len;

        //// draw outward faces of teeth
        for (int i = 0; i < fTeeth; i++)
        {
            angle = i * 2.0f * (float)Math.PI / fTeeth;

            gl.glBegin(gl.GL_LINES);
            gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), fWidth * 0.5f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle), r1 * (float)Math.Sin(angle), -fWidth * 0.5f);
            u = r2 * (float)Math.Cos(angle + da) - r1 * (float)Math.Cos(angle);
            v = r2 * (float)Math.Sin(angle + da) - r1 * (float)Math.Sin(angle);
            len = (float)Math.Sqrt(u * u + v * v);
            u /= len;
            v /= len;
            gl.glNormal3f(v, -u, 0.0f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + da), r2 * (float)Math.Sin(angle + da), -fWidth * 0.5f);
            gl.glNormal3f((float)Math.Cos(angle), (float)Math.Sin(angle), 0.0f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), fWidth * 0.5f);
            gl.glVertex3f(r2 * (float)Math.Cos(angle + 2 * da), r2 * (float)Math.Sin(angle + 2 * da), -fWidth * 0.5f);
            u = r1 * (float)Math.Cos(angle + 3 * da) - r2 * (float)Math.Cos(angle + 2 * da);
            v = r1 * (float)Math.Sin(angle + 3 * da) - r2 * (float)Math.Sin(angle + 2 * da);
            gl.glNormal3f(v, -u, 0.0f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), fWidth * 0.5f);
            gl.glVertex3f(r1 * (float)Math.Cos(angle + 3 * da), r1 * (float)Math.Sin(angle + 3 * da), -fWidth * 0.5f);
            gl.glNormal3f((float)Math.Cos(angle), (float)Math.Sin(angle), 0.0f);
            gl.glEnd();
        }
    }

    void RenderInsideRadiusCylinder(GraphicsInterface gi)
    {
        float angle;

        // draw inside radius cylinder
        for (int i = 0; i <= fTeeth; i++)
        {
            angle = i * 2.0f * (float)Math.PI / fTeeth;
            gi.Drawing.Lines.Begin();

            gi.Normal(-(float)Math.Cos(angle), -(float)Math.Sin(angle), 0.0f);
            gi.Vertex(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -fWidth * 0.5f);
            gi.Vertex(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), fWidth * 0.5f);
            gi.Vertex(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), -fWidth * 0.5f);
            gi.Vertex(r0 * (float)Math.Cos(angle + 4 * da), r0 * (float)Math.Sin(angle + 4 * da), -fWidth * 0.5f);
            gi.Vertex(r0 * (float)Math.Cos(angle), r0 * (float)Math.Sin(angle), fWidth * 0.5f);
            gi.Vertex(r0 * (float)Math.Cos(angle + 4 * da), r0 * (float)Math.Sin(angle + 4 * da), fWidth * 0.5f);
            gi.Drawing.Lines.End();
        }

    }

    protected override void  RenderContent(GraphicsInterface gi)
    {
        RenderFrontFacesOfTeeth(gi);
        RenderFrontSidesOfTeeth(gi);

        RenderBackFacesOfTeeth(gi);

        RenderBackSidesOfTeeth(gi);
        RenderOutwardFacesOfTeeth(gi);
        RenderInsideRadiusCylinder(gi);
    }

    public override void Render(GraphicsInterface gi)
    {
        fDisplayList.Render(gi);
    }
}
