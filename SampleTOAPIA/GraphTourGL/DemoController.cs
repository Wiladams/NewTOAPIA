using System;
using System.Collections.Generic;


namespace GraphTour
{
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;
    using NewTOAPIA.UI.GL;

    using Autometaii;

    public class DemoController : GLController
    {
        int fDemoCounter;
        List<Autometus> fDemos = new List<Autometus>();

        // The demo instances
        RectangleTest aRectTest;
        RandomRect randomRectTest;
        SineWave sineTest;
        LineDemo1 lineTest;
        GraphTest graphTest;

        public DemoController()
        {
        }

        public override void RenderFrame()
        {
            //if (fResizeFlag)
            //{
            DemoScene scene = Model as DemoScene;
            if (scene == null)
                return;

            scene.CurrentAutometus = fDemos[fDemoCounter];

            // Let the model do its drawing
            scene.Draw();

            // Tell the view there is something to display
            View.SwapBuffers();
        }

        public override void SetModelAndView(GLModel model, GLView view)
        {
            base.SetModelAndView(model, view);

            OnSetContext(view.GLContext);
        }

        public virtual void OnSetContext(GLContext GI)
        {
            aRectTest = new RectangleTest(new System.Drawing.Size(640, 480));
            randomRectTest = new RandomRect(new System.Drawing.Size(640, 480));
            sineTest = new SineWave(new System.Drawing.Size(640, 480), 100);
            lineTest = new LineDemo1(new System.Drawing.Size(640, 480));
            graphTest = new GraphTest(new System.Drawing.Size(640, 480));

            fDemos.Add(aRectTest);
            fDemos.Add(randomRectTest);
            fDemos.Add(sineTest);
            fDemos.Add(lineTest);
            fDemos.Add(graphTest);
        }

        public IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.Space:
                        fDemoCounter++;
                        if (fDemoCounter >= fDemos.Count)
                            fDemoCounter = 0;
                        RenderFrame();
                        break;
                }
            }

            return IntPtr.Zero;
        }

    }
}
