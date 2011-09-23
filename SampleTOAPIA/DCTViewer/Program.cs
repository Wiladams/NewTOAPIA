

using System;
using NewTOAPIA.UI;

namespace TargaViewer 
{
	class Program 
	{

		public static void Main(string[] args) 
		{
            
            PictureWindow win = new PictureWindow(50,50);

            User32Application app = new User32Application();
            app.Run(win);
        }
    }
}










