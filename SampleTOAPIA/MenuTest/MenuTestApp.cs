using System;

using NewTOAPIA.UI;

namespace MenuTest
{
	class MenuTestApp
	{
		private void TestGUIStyle(int x, int y)
		{
            MenuWindow win = new MenuWindow(x, y);
			win.Show();
		}

/*
		private void TestDesktopMenu()
		{
			DesktopWindow desktop = new DesktopWindow();

			// put a funny menu in it
			// Create a User32MenuBar
			User32MenuBar myBar = new User32MenuBar();

			User32Menu fileMenu = new User32Menu("&File");
			fileMenu.Add("New",1);
			fileMenu.Add("Open",2);
			fileMenu.Add("Close",3);
			fileMenu.AppendSeparator();
			fileMenu.Add("Save",4);
			fileMenu.Add("Save As",5);
			fileMenu.Add("Save All",6);
			fileMenu.AppendSeparator();
			fileMenu.Add("Page Setup",7);
			fileMenu.Add("Print",8);
			fileMenu.AppendSeparator();
			fileMenu.Add("Exit",9);

			User32Menu editMenu = new User32Menu("&Edit");
			editMenu.Add("Undo", 10);
			editMenu.Add("Redo", 11);
			editMenu.AppendSeparator();
			editMenu.Add("Cut", 12);
			editMenu.Add("Copy", 13);
			editMenu.Add("Paste", 14);
			editMenu.Add("Clear", 15);
			editMenu.AppendSeparator();
			editMenu.Add("Select All", 15);
			editMenu.AppendSeparator();
		
			User32Menu findReplace = new User32Menu("Find and Replace");
			findReplace.Add("Find", 16);
			findReplace.Add("Replace", 17);
			findReplace.Add("Find in files", 18);
			findReplace.Add("Replace in files", 19);

			editMenu.Add(findReplace);


			myBar.Add(fileMenu);
			myBar.Add(editMenu);

			myBar.Window = desktop;

			// Draw in the window just to make sure it's there
			desktop.GraphDevice.FillRectangle(0,0,desktop.Frame.Width,desktop.Frame.Height);
			desktop.GraphDevice.Flush();

		}
*/

	}
}
