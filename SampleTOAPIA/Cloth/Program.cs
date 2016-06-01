using System;

using NewTOAPIA.GL;

namespace Cloth
{
    using NewTOAPIA.UI;
    using NewTOAPIA.UI.GL;

    static class Program
    {
        //[STAThread]
        static void Main()
        {
            GLGameApplication glApp = new GLGameApplication("Cloth", new System.Drawing.Rectangle(10,10,640,480), true);

            FlagScene scene = new FlagScene();

            glApp.Run(scene);
        }
    }
}