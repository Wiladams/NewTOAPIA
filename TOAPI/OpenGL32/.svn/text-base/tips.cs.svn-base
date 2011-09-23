
using TOAPI.OpenGL;

namespace TOAPI.OpenGL.Utilities
{
    // These routines are here as an example of how to setup
    // the OpenGL environment for 2D drawing on top of a scene.
    public class Tips
    {
        public static void glEnable2D()
        {
	        int [] vPort = new int[4];

            gl.glGetIntegerv(GetTarget.Viewport, vPort);

            gl.glMatrixMode(MatrixMode.Projection);
            gl.glPushMatrix();
            gl.glLoadIdentity();

            gl.glOrtho(0, vPort[2], 0, vPort[3], -1, 1);
            gl.glMatrixMode(MatrixMode.Modelview);
            gl.glPushMatrix();
            gl.glLoadIdentity();
        }

        public static void glDisable2D()
        {
            gl.glMatrixMode(MatrixMode.Projection);
            gl.glPopMatrix();
            gl.glMatrixMode(MatrixMode.Modelview);
            gl.glPopMatrix();
        }

        public static void RenderScene()
        {
            gl.glClear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            gl.glLoadIdentity();

            glEnable2D();
                gl.glBegin(BeginMode.Triangles);
                    gl.glColor3ub(255, 0, 0);
                    gl.glVertex2d(0, 0);
                    gl.glColor3ub(0, 255, 0);
                    gl.glVertex2d(100, 0);
                    gl.glColor3ub(0, 0, 255);
                    gl.glVertex2d(50, 50);
                gl.glEnd();
            glDisable2D();
        }
    }
}
