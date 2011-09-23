

using System;
using NewTOAPIA.UI;

namespace MenuTest 
{
	class Test 
	{

		public static void Main(string[] args) 
		{
            
            MenuWindow win = new MenuWindow(50,50);
            win.Show();

            User32Application app = new User32Application();
            app.Run(win);
        }
    }
}










