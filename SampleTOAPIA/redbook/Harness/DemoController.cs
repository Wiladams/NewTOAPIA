using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harness
{
    using NewTOAPIA.GL;
    using NewTOAPIA.UI;

    public class DemoController : GLController
    {
        List<GLModel> Models = new List<GLModel>();
        int fDemoCounter = 0;

        public DemoController()
        {
            // instantiate model and view components, so "controller" component can reference them
           // Models.Add(new Aapoly());
            //Models.Add(new Alpha());
            Models.Add(new BezCurve());
            Models.Add(new BezMesh());
            Models.Add(new Checker());
            //Models.Add(new ColorMat());
            Models.Add(new Cube());
            Models.Add(new Material());
            //Models.Add(new Varray());
            Models.Add(new SimpleInputModel());
        }

       
        public override IntPtr OnKeyboardActivity(object sender, NewTOAPIA.UI.KeyboardActivityArgs kbde)
        {
            if (kbde.AcitivityType == NewTOAPIA.UI.KeyActivityType.KeyUp)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case NewTOAPIA.UI.VirtualKeyCodes.Space:
                        if (fDemoCounter >= Models.Count)
                            fDemoCounter = 0;
                        Model = Models[fDemoCounter];
                        Model.SetContext(View.GLContext);

                        System.Drawing.Rectangle rect = GetClientRectangle();
                        Model.OnSetViewport(rect.Width, rect.Height);
                        fDemoCounter++;
                        break;
                }
                RenderFrame();
            }

            return IntPtr.Zero;
        }
    }
}
