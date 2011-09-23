using System;
using System.Drawing;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

    public class StringLabel : Graphic
    {
		private string fFontName;
		private int fPointSize;
		private GFont.FontStyle fStyle;

        private Point fBasePoint;
        private Point fStartPoint;

        private String fString;

		private uint fTextColor;
        private uint fBackColor;
		private GBrush fBrush;

		private bool fNeedsCalculation;

        private GDIFont fFont;

        public StringLabel(string aString, int x, int y)
			: this(aString, "Helvetica", 12, x, y, GFont.FontStyle.Regular)
		{
		}

        public StringLabel(string aString, string aFontName, int pointSize, int x, int y)
			:this(aString, aFontName, pointSize, x, y, GFont.FontStyle.Regular)
		{
		}

        public StringLabel(string aString, string aFontName, int pointSize, int x, int y, GFont.FontStyle style)
            : base("StringLabel",x,y,0,0)
        {
			// WAA
			fNeedsCalculation = true;
			fStyle = style;
            fString = aString;
            fFontName = aFontName;
			fPointSize = pointSize;
			fStartPoint = new Point(x,y);
            fBasePoint = new Point(x, y);
 
			fTextColor = RGBColor.Black;
            fBackColor = RGBColor.TRANSPARENT;
            fBrush = new GBrush(fTextColor);

            fFont = new GDIFont(fFontName, fPointSize);

			Recalculate();
        }


        // Graphic Overrides
        protected override void OnUpdateGeometryState()
        {
            // Don't need to do this since we're now zero based
            //fStartPoint.X += dw;
            //fStartPoint.Y += dh;
            //fBasePoint.X += dw;
            //fBasePoint.Y += dh;
        }

		public override void Invalidate()
		{
			fNeedsCalculation = true;
		}

		// Recalculate the size of the string and other things
		void Recalculate()
		{
			if (!fNeedsCalculation)
			{
				return;
			}


			//SizeF aSize = fGraphPort.MeasureString(fString, fFont);
            int stringWidth = 0;
            int stringHeight = 0;
            Size stringSize = fFont.MeasureString(fString);
            fNeedsCalculation = false;

            ResizeTo(stringSize.Width, stringSize.Height);
		}

        public override void DrawSelf(DrawEvent de)
        {
			IGraphPort graphPort = de.GraphPort;


			//fFont = new Win32Font(graphPort.DeviceContext, fFontName, fPointSize,fStyle);
			//graphPort.Font = fFont;

			graphPort.SetTextColor(fTextColor);
			graphPort.SetBkColor(fBackColor);
            graphPort.SetBkMode(RGBColor.TRANSPARENT);

            graphPort.DrawString(fStartPoint.X, fStartPoint.Y, fString);
        }


		public string Text 
		{
			get 
			{
				return fString;
			}

			set 
			{
				fString = value;
				Recalculate();
			}
		}

		public uint BackgroundColor
		{
			get { return fBackColor; }
			set { fBackColor = value; }
		}

		public uint TextColor
		{
			get { return fTextColor; }
			set {
				fTextColor = value;
                fBrush = new GBrush(fTextColor);
            }
		}

		public string FontName
		{
			get 
			{
				return fFontName;
			}

			set
			{
				fFontName = value;
				Invalidate();
			}
		}

		public int PointSize 
		{
			get 
			{
				return fPointSize;
			}

			set
			{
				fPointSize = value;
				Invalidate();
			}
		}
    }

