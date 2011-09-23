using System;

using NewTOAPIA.GL;

static class Program
{
	[STAThread]
	static void Main()
	{
        GLApplication glApp = new GLApplication();

        FlagScene scene = new FlagScene();

        glApp.Run(scene);
	}		
}