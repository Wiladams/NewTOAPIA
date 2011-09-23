using System;

using NewTOAPIA;
using NewTOAPIA.GL;

namespace PointSprites
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            GLApplication glApp = new GLApplication();

            MS3DScene scene = new MS3DScene();

            glApp.Run(scene);
        }
    }
}