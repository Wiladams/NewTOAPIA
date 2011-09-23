
namespace FBOImaging
{
    using System;

    using NewTOAPIA.GL;

    static class Program
    {
        [STAThread]
        static void Main()
        {
            GLApplication glApp = new GLApplication("Frame Buffer Imaging");

            SampleDraw scene = new SampleDraw();

            glApp.Run(scene);
        }
    }
}
