
using System;

namespace Demo
{

	class DigitalClock
	{

		public static void Main(string[] args)
		{
			ClockWindow win = new ClockWindow();
			win.Show();
			win.Run();
		}
	}
}