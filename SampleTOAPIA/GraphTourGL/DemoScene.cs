using System;

namespace GraphTour
{
    using NewTOAPIA;
    using NewTOAPIA.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;
    using NewTOAPIA.UI.GL;

    using Autometaii;


    public class DemoScene : GLModel
    {
        GLGraphport fGraphPort;
        public Autometus CurrentAutometus { get; set; }

        protected override void OnSetContext()
        {
            fGraphPort = new GLGraphport(GI);
        }

        protected override void DrawBegin()
        {
            // Set the viewport to be the same as the window size
            GI.Viewport(0, 0, ViewportWidth, ViewportHeight);

            // Setup an orthographic projection such that we have 1:1 
            // correspondence between coordinates and pixels on the window
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();

            GI.Ortho(0, ViewportWidth, 0, ViewportHeight, ViewportWidth / 2, -(ViewportWidth / 2));

            // Clear our buffers
            GI.Buffers.ColorBuffer.Color = ColorRGBA.White;
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();
        }

        protected override void DrawContent()
        {
            if (CurrentAutometus != null)
                CurrentAutometus.ReceiveCommand(new Command_Render(fGraphPort));
        }

        protected override void DrawEnd()
        {
            base.DrawEnd();
        }
    }
}
