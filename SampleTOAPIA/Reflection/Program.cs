using System;
using System.Drawing;

using NewTOAPIA.UI.GL;

namespace Reflection
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ReflectionScene scene = new ReflectionScene();
            GLGameApplication glApp = new GLGameApplication("Reflection", new Rectangle(10,10, 800,600), true);

            glApp.Run(scene);
        }
    }
}