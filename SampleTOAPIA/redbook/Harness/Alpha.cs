
using System;

using TOAPI.OpenGL;
using TOAPI.Types;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;
using NewTOAPIA.Modeling;

//using NewTOAPIA.Drawing;

	public class Alpha : GLModel
	{
        bool leftFirst;
        GLTriangle leftTriangle;
        GLTriangle rightTriangle;

        public Alpha()
            :base()
		{
            leftFirst = true;

            leftTriangle = new GLTriangle(
                new float3(0.1f, 0.9f, 0.0f),
                new float3(0.1f, 0.1f, 0.0f),
                new float3(0.7f, 0.5f, 0.0f),
                new ColorRGBA(1.0f, 1.0f, 0.0f, 0.75f));

            rightTriangle = new GLTriangle(
                new float3(0.9f, 0.9f, 0.0f),
                new float3(0.3f, 0.5f, 0.0f),
                new float3(0.9f, 0.1f, 0.0f),
                new ColorRGBA(0.0f, 1.0f, 1.0f, 0.75f));

        }

        protected override void OnSetContext()
        {
        }

        protected override void DrawBegin()
        {
            GI.Features.Blend.Enable();
            GI.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            GI.ShadeModel(ShadingModel.Flat);
            GI.Buffers.ColorBuffer.Color = ColorRGBA.Invisible;
            GI.Buffers.ColorBuffer.Clear();
        }

        protected override void DrawContent()
		{
            if (leftFirst)
            {
                leftTriangle.Render(GI);
                rightTriangle.Render(GI);
            }
            else
            {
                rightTriangle.Render(GI);
                leftTriangle.Render(GI);
            }

        }

        public override IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.T:
                        leftFirst = !leftFirst;
                        break;
                }
            }

            return IntPtr.Zero;
        }

        public override void OnSetViewport(int w, int h)
        {
            GI.Viewport(0, 0, w, h);
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            if (w <= h)
                glu.gluOrtho2D(0.0, 1.0, 0.0, 1.0 * (float)h / (float)w);
            else
                glu.gluOrtho2D(0.0, 1.0 * (float)w / (float)h, 0.0, 1.0);
        }
	}