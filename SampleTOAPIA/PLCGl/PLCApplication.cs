using System;
using Papyrus;
using System.Windows.Forms;

namespace PLC
{
	class PLCApplication : Application
	{
		public PLCApplication()
		{
			PLCWindow win = new PLCWindow();
			win.Show();
		}

		protected override void Initialize()
		{
			// Create a window
			//Console.WriteLine("PLCApplication.Initialize()");


			//Console.WriteLine("PLCApplication.Initialize - Ready to go!");
		}
	}
}