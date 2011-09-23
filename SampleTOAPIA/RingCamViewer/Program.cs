using System;


namespace RingCamView
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            VideoDromeScene model = new VideoDromeScene();

            NewTOAPIA.GL.GLApplication app = new NewTOAPIA.GL.GLApplication("RingCamViewer");
            app.Run(model);
        }
    }
}