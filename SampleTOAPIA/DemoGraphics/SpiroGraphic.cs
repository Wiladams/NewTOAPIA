using System;
using System.Drawing;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace DemoGraphics
{
	public class SpiroGraphic : IDrawable
	{
		Random rnd;

		public SpiroGraphic()
		{
			rnd = new Random();
		}

        public void Draw(DrawEvent de)
		{
			DrawAt(de.GraphPort, 0, 0, de.ClipRect);
		}

		public void DrawAt(IGraphPort graphPort, int x, int y, Rectangle updateRect)
		{
			int centerX = updateRect.Width/2;
			int centerY = updateRect.Height/2;
			int Radius = (updateRect.Height / 2) - 5;
			int N = 36;
			double theta = 3.14159 * 2 / N;

            GDIPen aPen = new GDIPen(PenType.Cosmetic, PenStyle.Solid, PenJoinStyle.Round, PenEndCap.Round, RGBColor.Red, 1, Guid.NewGuid());

            for (int p = 0; p < N; p++)
			{
				byte num = (byte)(((double)p / (double)N) * 254.0);
				byte r = (byte)rnd.Next(0, num);
				byte g = (byte)rnd.Next(0, num);
				byte b = (byte)rnd.Next(0, num);
				uint color = RGBColor.RGB(r, g, b);
				//graphPort.PenColor = color;

				for (int q = 0; q < p; q++)
				{
					graphPort.DrawLine(aPen, new Point((int)(centerX + Radius * Math.Sin(p * theta)),
										(int)(centerY + Radius * Math.Cos(p * theta))),
										new Point((int)(centerX + Radius * Math.Sin(q * theta)),
										(int)(centerY + Radius * Math.Cos(q * theta))));
				}
			}
		}
	}
}
