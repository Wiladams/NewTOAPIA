using System;


namespace Arena
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ArenaModel model = new ArenaModel();

            NewTOAPIA.GL.GLApplication app = new NewTOAPIA.GL.GLApplication("Arena");
            app.Run(model);
        }
    }
}