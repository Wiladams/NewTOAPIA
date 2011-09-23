using System;
using NewTOAPIA.UI;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.GL;
using QuadVideo;
using NewTOAPIA.UI.GL;

static class Program
{

    [STAThread]
    static void Main()
    {
        VideoScene model = new VideoScene();

        GLGameApplication app = new GLGameApplication("Quad Video", new System.Drawing.Rectangle(10, 10, 640, 480), true);
        app.Run(model);
    }

}