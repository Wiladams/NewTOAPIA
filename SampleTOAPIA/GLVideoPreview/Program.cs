using System;
using NewTOAPIA.UI;
using TOAPI.Types;
using TOAPI.User32;

using NewTOAPIA.GL;
using QuadVideo;

static class Program
{
    [STAThread]
    static void Main()
    {
        VideoScene model = new VideoScene();

        GLApplication app = new GLApplication("GL Video Preview", new System.Drawing.Rectangle(10, 10, 640, 480), true);
        app.Run(model);
    }

}