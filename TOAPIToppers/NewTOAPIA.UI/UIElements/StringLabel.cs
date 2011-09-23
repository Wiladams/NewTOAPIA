using System;

using NewTOAPIA.Drawing;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

    public class StringLabel : Graphic
    {
		private string fFontName;
		private int fPointSize;
		private FontStyle fStyle;

        private Point3D fBasePoint;
        private Point3D fStartPoint;

        private String fString;

		private Colorref fTextColor;
        private Colorref fBackColor;
		private IBrush fBrush;

		private bool fNeedsCalculation;

		private IFont fFont;

        public StringLabel(string aString, int x, int y)
			: this(aString, "Helvetica", 12, x, y, FontStyle.Regular)
		{
		}

        public StringLabel(string aString, string aFontName, int pointSize, int x, int y)
			:this(aString, aFontName, pointSize, x, y, FontStyle.Regular)
		{
		}

        public StringLabel(string aString, string aFontName, int pointSize, int x, int y, FontStyle style)
            : base("StringLabel",x,y,0,0)
        {
			fNeedsCalculation = true;
			fStyle = style;
			fFont = null;
            fString = aString;
            fFontName = aFontName;
			fPointSize = pointSize;
			fStartPoint = new Point3D(x,y);
            fBasePoint = new Point3D(x, y);
 
			fTextColor = (Colorref)Colorrefs.Black;
            fBackColor = (Colorref)Colorref.TRANSPARENT;
            fBrush = new GDISolidBrush(fTextColor);

            fFont = new GDIFont(fFontName, fPointSize);

			Recalculate();
        }


        // Graphic Overrides
        protected override void OnUpdateGeometryState()
        {
            fNeedsCalculation = true;
        }

		public override void Invalidate()
		{
			fNeedsCalculation = true;
            base.Invalidate();
		}

		// Recalculate the size of the string and other things
		void Recalculate()
		{
			if (!fNeedsCalculation)
			{
				return;
			}

            Size2D aSize = fFont.MeasureString(fString);
            ResizeTo((int)aSize.width,(int)aSize.height);
		    fNeedsCalculation = false;
		}

        public override void DrawSelf(DrawEvent devent)
        {
            IGraphPort graphPort = devent.GraphPort;

			graphPort.SetTextColor(fTextColor);
            if (Colorref.TRANSPARENT == fBackColor) 
                graphPort.SetBkMode(Colorref.TRANSPARENT);
            else
                graphPort.SetBkColor(fBackColor);

            graphPort.SetFont(fFont);

            graphPort.DrawString((int)fStartPoint.x, (int)fStartPoint.y, fString);
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

		public Colorref BackgroundColor
		{
			get { return fBackColor; }
			set { fBackColor = value; }
		}

		public Colorref TextColor
		{
			get { return fTextColor; }
			set {
				fTextColor = value;
                fBrush = new GDISolidBrush(fTextColor);
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
}
