using System;
using System.Collections.Generic;
using System.Text;

namespace DemoGraphics
{
	class SpiroGraphic
	{
		public void CircleDemo()
		{
			int x = (Bounds.Width / 2);
			int y = Bounds.Height / 2;
			int Radius = (Bounds.Height / 2) - 5;
			int N = 36;
			double theta = 3.14159 * 2 / N;

			for (int p = 0; p < N; p++)
			{
				byte num = (byte)(((double)p / (double)N) * 254.0);
				byte r = (byte)rnd.Next(0, num);
				byte g = (byte)rnd.Next(0, num);
				byte b = (byte)rnd.Next(0, num);
				uint color = RGBColor.RGB(r, g, b);
				// uint color = RGBColor.RGB(num, num, num);
				GraphDevice.PenColor = color;

				for (int q = 0; q < p; q++)
				{
					GraphDevice.DrawLine((int)(x + Radius * Math.Sin(p * theta)),
										(int)(y + Radius * Math.Cos(p * theta)),
										(int)(x + Radius * Math.Sin(q * theta)),
										(int)(y + Radius * Math.Cos(q * theta)));
				}
			}
		}
	}
}
