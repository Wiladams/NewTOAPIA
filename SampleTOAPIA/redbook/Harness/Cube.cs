
using System;

using NewTOAPIA;
using NewTOAPIA.Modeling;
using NewTOAPIA.UI;
using NewTOAPIA.GLU.Shapes;
//using NewTOAPIA.Drawing;

using TOAPI.OpenGL;
using TOAPI.Types;

using NewTOAPIA.GL;

	public class Cube : GLModel
	{
        GLUCube fCube;

        public Cube()
            :base()
		{
		}

        protected override void OnSetContext()
        {
            fCube = new GLUCube(1, CubeStyle.WireFrame);
        }

        protected override void DrawBegin()
        {
            GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
            //GLShadingModel.Flat.Realize();
        }

        protected override void DrawContent()
		{
            GI.Buffers.ColorBuffer.Clear();
            GI.Color(1.0f, 1.0f, 1.0f);
            GI.LoadIdentity();             // clear the matrix viewing transform
            glu.gluLookAt(0.0, 0.0, 5.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0);
            GI.Scale(1.0f, 2.0f, 1.0f);      /* modeling transformation */
            fCube.Render(GI);
		}

        public override void OnSetViewport(int w, int h)
        {        
            GI.Viewport(0, 0, w, h);
            
            // Change the viewing frustum
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            GI.Frustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            
            // Change to model view mode for subsequent 
            // transformations.
            GI.MatrixMode(MatrixMode.Modelview);
        }
	}