using System;

using NewTOAPIA.GL;
using NewTOAPIA.UI;

using TOAPI.OpenGL;

namespace SnapNGLView
{
    class GLViewerModel : GLModel
    {
        int B_WIDTH;
        int B_HEIGHT;

        GLSketchViewer fViewer;

        public GLViewerModel()
        {
        }

        protected override void OnSetContext()
        {
            fViewer = new GLSketchViewer(GI, 620, 440);

        }

        protected override void DrawBegin()
        {
            gl.glMatrixMode(gl.GL_PROJECTION);
            gl.glLoadIdentity();
            gl.glOrtho(0, B_WIDTH, 0, B_HEIGHT, -1, 1);
            gl.glViewport(0, 0, B_WIDTH, B_HEIGHT);
            gl.glClearColor(0, 0, 0, 0);
            gl.glColor3f(1.0f, 0.84f, 0.0f);
            gl.glHint(gl.GL_POLYGON_SMOOTH_HINT, gl.GL_NICEST);

            GI.Features.Texturing2D.Enable();
        }


        protected override void DrawContent()
        {
            fViewer.TexTure.Bind();
            //gl.glTexParameteri(gl.GL_TEXTURE_RECTANGLE_NV, gl.GL_TEXTURE_MAG_FILTER, gl.GL_LINEAR);
            //gl.glTexParameteri(gl.GL_TEXTURE_RECTANGLE_NV, gl.GL_TEXTURE_MIN_FILTER, gl.GL_LINEAR);
            //gl.glTexEnvf(gl.GL_TEXTURE_ENV, gl.GL_TEXTURE_ENV_MODE, gl.GL_DECAL);
            
            gl.glBegin(gl.GL_QUADS);

            gl.glTexCoord2f(0, 0);
            //gl.glTexCoord2i(0, 0);
            gl.glVertex2i(0, 0);

            gl.glTexCoord2f(1, 0);
            //gl.glTexCoord2i(620, 0);
            gl.glVertex2i(B_WIDTH, 0);

            gl.glTexCoord2f(1, 1);
            //gl.glTexCoord2i(620, 440);
            gl.glVertex2i(B_WIDTH, B_HEIGHT);

            gl.glTexCoord2i(0, 1);
            //gl.glTexCoord2i(0, 440);
            gl.glVertex2i(0, B_HEIGHT);
            
            gl.glEnd();
            fViewer.TexTure.Unbind();
        }

        public override void OnSetViewport(int w, int h)
        {
            B_WIDTH = w;
            B_HEIGHT = h;
        }

        public override void OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            base.OnKeyboardActivity(sender, kbde);

            if (kbde.EventType == KeyEventType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                }
            }

            switch (kbde.VirtualKeyCode)
            {
            }

        }
    }
}