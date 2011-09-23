using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

using TOAPI.Types;

public class HeadingButton : TextBox
{
	public event System.EventHandler MouseDownEvent;
	public event System.EventHandler MouseUpEvent;
	public event System.EventHandler MouseMoveEvent;

    public HeadingButton(string aString, string aFontName, int pointSize, GDIFont.FontStyle style, int x, int y, int width, int height, uint txtColor)
			:this(aString,aFontName,pointSize,style,x,y,width,height,StringAlignment.Center,StringAlignment.Center,txtColor,null)
		{
		}

    public HeadingButton(string aString, string aFontName, int pointSize, GDIFont.FontStyle style, int x, int y, int width, int height, StringAlignment align, StringAlignment lineAlign, uint txtColor, Graphic background)
		:base(aString,aFontName,pointSize,style, x,y,width,height,align,lineAlign,txtColor,background)
	{
        //GFont aFont = new GFont(aFontName, pointSize, Guid.NewGuid());
        //this.Font = aFont;
        Enabled = true;
	}

	// Reacting to the mouse
	public override void OnMouseDown(MouseActivityArgs e)
	{

		float x = e.X;
		float y = e.Y;

		//ScreenToLocal(ref x, ref y);

		//Console.WriteLine("ActiveArea.OnMouseDown: {0} {1} - BEGIN \n", e.X, e.Y);


		// Draw myself Immediately
		//DrawInto(LastGraphDevice);

		if (MouseDownEvent != null)
			MouseDownEvent(this, e);

	}

	/// <summary>
	/// OnMouseEnter
	/// 
	/// This gets called whenever the pointing device enters our frame.
	/// We want to do interesting things here like change the cursor shape
	/// to be whatever we require.
	/// </summary>
	/// <param name="e"></param>
	public override void OnMouseEnter(MouseActivityArgs e)
	{
		//Console.WriteLine("ActiveArea.OnMouseEnter: {0}", e.ToString());

		// Possibly Draw Myself Immediately
		//Invalidate();
	}

    public override void OnMouseLeave(MouseActivityArgs e)
	{
		//Console.WriteLine("ActiveArea.OnMouseLeave: {0}", e.ToString());
		//fTracking = false;
		//fIsDepressed = false;

		// Draw Myself Immediately
		// WAA - Maybe not because it may not be necessary
		//Invalidate();
	}

    public override void OnMouseMove(MouseActivityArgs e)
	{
		//Console.WriteLine("ActiveArea.OnMouseMove: {0}", e.ToString());
		if (MouseMoveEvent != null)
			MouseMoveEvent(this, e);

		// Possibly Draw Myself Immediately
		//Invalidate();
	}

    public override void OnMouseUp(MouseActivityArgs e)
	{
		if (MouseUpEvent != null)
			MouseUpEvent(this, e);

	}
}
