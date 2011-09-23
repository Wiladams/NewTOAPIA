using System;
using System.Drawing;

using NewTOAPIA.Drawing;

/*
* <dt> base
* <dd> The base color is the color that is used as the "dominant" color 
*      for graphical objects. For example, a button's text is drawn on top 
*      of this color when the button is "up".  
* <dt> highlight
* <dd> A lighter rendition of the base color used to create a highlight in 
*      pseudo 3D effects.
* <dt> shadow
* <dd> A darker rendition of the base color used to create a shadow in pseudo
*      3D effects.
* <dt> background
* <dd> The color used for background (or inset) <I>items</I> in a drawing 
*      scheme (rather than as a typical background area per se).  For example,
*      the background of a slider (the "groove" that the thumb slides in)
*      is drawn in this color.  [This name probably needs to be changed.]
* <dt>
* <dd> Note: the colors: base, highlight, shadow, and background are designed 
*      to be related, typically appearing to be the same material but with 
*      different lighting.
* <dt> foreground
* <dd> The color normally drawn over the base color for foreground items such
*      as textual labels.  This color needs to contrast with, but not clash 
*      with the base color.
* <dt> text_background
* <dd> The color that serves as the background for text editing areas.
* <dt> splash
* <dd> A color which is designed to contrast with, and be significantly
*      different from, the base, highlight, shadow, and background color 
*      scheme.  This is used for indicators such as found inside a selected
*      radio button or check box.
* </dl>
*/
namespace NewTOAPIA.UI 
{
    using NewTOAPIA.Drawing.GDI;
    using NewTOAPIA.Graphics;

	public class GUIStyle
	{
		static GUIStyle gDefaultStyle = new GUIStyle();

		public enum FrameStyle
		{
			Sunken = 0x01,
			Raised = 0x02
		}

		Colorref fBaseColor;
		Colorref fHighlightColor;
		Colorref fShadowColor;
		Colorref fBackground;
		Colorref fForeground;
		Colorref fTextBackground;
        Colorref fBottomShadow;
        Colorref fBottomShadowBottomLiner;
        Colorref fBottomShadowTopLiner;
        Colorref fTopShadow;

        IPen fBasePen;
        IPen fHighlightPen;
        IPen fShadowPen;
        IPen fBottomShadowBottomLinerPen;

        IBrush fBaseColorBrush;

		//uint fBottomShadowTopLiner;
		int fBorderWidth;

		public GUIStyle()
		{
			fBorderWidth = 2;

			fBaseColor = (Colorref)Colorrefs.LtGray;
            fHighlightColor = GUIStyle.brighter(fBaseColor);
            fShadowColor = GUIStyle.darker(fBaseColor);
            fBackground = GUIStyle.brighter(fHighlightColor);
            fForeground = (Colorref)Colorrefs.LtGray;
            fTextBackground = fBaseColor;
            fBottomShadow = GUIStyle.darker(fForeground); // 0x00616161;
            fBottomShadowTopLiner = GUIStyle.brighter(fBottomShadow); //fForeground;
            fTopShadow = GUIStyle.brighter(fForeground);  // 0x00cbcbcb;

            fBackground = GUIStyle.brighter((Colorref)Colorrefs.DarkGray); //0x009e9e9e;


            fBasePen = new GDIPen(fBaseColor);
            fHighlightPen = new GDIPen(fHighlightColor);
            fShadowPen = new GDIPen(fShadowColor);
            fBottomShadowBottomLinerPen = new GDIPen(fBottomShadowBottomLiner);

            fBaseColorBrush = new GDIBrush(fBaseColor);

			GUIStyle.Default = this;
		}

		public static Colorref darker(Colorref color)
		{
			byte red = (byte)(color.Red *0.60);
			byte green = (byte)(color.Green * 0.60);
			byte blue = (byte)(color.Blue * 0.60);
		
			return new Colorref(red, green, blue);
		}

		public static Colorref brighter(Colorref color)
		{
			byte red = (byte)(Math.Min(color.Red *(1/0.80), 255));
			byte green = (byte)(Math.Min(color.Green * (1.0/0.85), 255));
			byte blue = (byte)(Math.Min(color.Blue * (1.0/0.80), 255));

			return new Colorref(red, green, blue);
		}

		public static GUIStyle Default
		{
			get 
			{
				return GUIStyle.gDefaultStyle;
			}
			set 
			{
				GUIStyle.gDefaultStyle = value;
			}
		}
		public uint  SunkenColor
		{
			get 
			{
				return fForeground;
			}
		}

		public uint RaisedColor
		{
			get 
			{
				return fForeground;
			}
		}

		public uint Background
		{
			get 
			{
				return fBackground;
			}
		}

		public Colorref BaseColor
		{
			get 
			{
				return fBaseColor;
			}
			set
			{
				fBaseColor = value;
				fHighlightColor = GUIStyle.brighter(fBaseColor);
				fShadowColor = GUIStyle.darker(fBaseColor);
				fBackground = GUIStyle.brighter(fHighlightColor); 			
				fTextBackground = fBaseColor;
			}
		}

		public Colorref Foreground
		{
			get 
			{
				return fForeground;
			}
			set
			{
				fForeground = value;
			}
		}

		public int BorderWidth
		{
			get 
			{
				return fBorderWidth;
			}
			set
			{
				fBorderWidth = value;
			}
		}

		public int Padding
		{
			get 
			{
				return 2;
			}
		}

		public virtual void  DrawFrame(IGraphPort aPort, int x, int y, int w, int h, FrameStyle style)
		{
			int n;

			switch (style)
			{
				case FrameStyle.Sunken:
					for (n=0; n<BorderWidth; n++)
					{
						aPort.DrawLine(fHighlightPen, new Point2I(x+n, y+h-n), new Point2I(x+w-n, y+h-n));    // bottom shadow
                        aPort.DrawLine(fHighlightPen, new Point2I(x + w - n, y + n), new Point2I(x + w - n, y + h));	    // right shadow
					}

					for (n=0; n<BorderWidth; n++)
					{
						aPort.DrawLine(fShadowPen, new Point2I(x+n, y+n), new Point2I(x+w-n, y+n));	    // top edge
						aPort.DrawLine(fShadowPen, new Point2I(x+n, y+n), new Point2I(x+n, y+h-n));	    // left edge
					}				
				break;

				case FrameStyle.Raised:	
					for (n=0; n<BorderWidth; n++)
					{
						aPort.DrawLine(fShadowPen, new Point2I(x+n, y+h-n), new Point2I(x+w-n, y+h-n));      // bottom shadow
						aPort.DrawLine(fShadowPen, new Point2I(x+w-n, y+n), new Point2I(x+w-n, y+h));	    // right shadow
					}
	
					if (BorderWidth > 0)
					{
						n = BorderWidth - 1;
/*
						aPort.DrawingColor = fBottomShadowTopLiner;
						aPort.DrawLine(x+n, y+h-n, x+w-n, y+h-n);		// bottom shadow
						aPort.DrawLine(x+w-n, y+n, x+w-n, y+h);			// right shadow
*/						
                        aPort.DrawLine(fBottomShadowBottomLinerPen, new Point2I(x, y + h), new Point2I(x + w, y + h));				// bottom shadow
                        aPort.DrawLine(fBottomShadowBottomLinerPen, new Point2I(x + w, y), new Point2I(x + w, y + h));				// right shadow
					}

					for (n=0; n<BorderWidth; n++)
					{
						aPort.DrawLine(fHighlightPen, new Point2I(x+n,y+n), new Point2I(x+w-n, y+n));	    // top edge
						aPort.DrawLine(fHighlightPen, new Point2I(x+n, y+n), new Point2I(x+n, y+h-n));	    // left edge
					}
				break;
			}

		}

		public virtual void  DrawSunkenRect(IGraphPort aPort, int x, int y, int w, int h)
		{
			aPort.FillRectangle(fBaseColorBrush, new RectangleI(x, y, w, h));	

			DrawFrame(aPort, x, y, w, h, FrameStyle.Sunken);
		}

        public virtual void DrawRaisedRect(IGraphPort aPort, int x, int y, int w, int h)
		{
			aPort.FillRectangle(fBaseColorBrush, new RectangleI(x, y, w, h));

			DrawFrame(aPort, x, y, w, h, FrameStyle.Raised);
		}


        public virtual void DrawLine(IGraphPort aPort, int x1, int y1, int x2, int y2, int border_width)
		{
            // Vertical line
			if (x1 == x2)
			{
				for (int n=0; n<BorderWidth; n++) 
					aPort.DrawLine(fShadowPen, new Point2I(x1-n, y1+n), new Point2I(x1-n, y2-n));  // left part
	    
				for (int n=1; n<BorderWidth; n++) 
					aPort.DrawLine(fHighlightPen, new Point2I(x1+n, y1+n), new Point2I(x1+n, y2-n));  // right part
			} 
			else if (y1 == y2)  // Horizontal line
			{
				for (int n=0; n<BorderWidth; n++) 
					aPort.DrawLine(fShadowPen, new Point2I(x1+n, y1-n), new Point2I(x2-n, y1-n));  // top part

				for (int n=1; n<BorderWidth; n++) 
					aPort.DrawLine(fHighlightPen, new Point2I(x1+n, y1+n), new Point2I(x2-n, y1+n));  // bottom part
			}
		}
	}
}