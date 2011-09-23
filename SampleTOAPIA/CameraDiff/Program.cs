using System;

using NewTOAPIA.UI;
using NewTOAPIA.GL;

using QuadVideo;

static class Program
{
    [STAThread]
    static void Main()
    {
        VideoScene model = new VideoScene();

        GLGameApplication app = new GLGameApplication("Camera Diff", new System.Drawing.Rectangle(10, 10, 352 * 2 + 40, 288 * 2 + 40), true);
        app.Run(model);
    }

}