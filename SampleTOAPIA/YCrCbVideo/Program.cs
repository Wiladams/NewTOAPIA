using System;

using NewTOAPIA.GL;

using QuadVideo;

static class Program
{
    [STAThread]
    static void Main()
    {
        VideoScene model = new VideoScene();

        GLApplication app = new GLApplication("YCrCb Video", new System.Drawing.Rectangle(10, 10, 352*2+40, 288*2+40));
        app.Run(model);
    }

}