
namespace GUIStyleTest 
{
    using System;

    using NewTOAPIA.UI;

	class Test 
	{

		public static void Main(string[] args) 
		{

            //GUIStyleWindow win = new GUIStyleWindow();

            GUIStyleWindow win2 = new GUIStyleWindow();
            win2.IsApplicationWindow = false;
            win2.MoveTo(300, 300);
            win2.Show();

            User32Application app = new User32Application();
            app.Run(win2);
		}
    }
}










