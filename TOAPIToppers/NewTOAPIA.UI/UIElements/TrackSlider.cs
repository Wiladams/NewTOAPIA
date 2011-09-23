using System;

namespace NewTOAPIA.UI
{
    using NewTOAPIA.Graphics;

	public class TrackSlider : ActiveArea, IPositionalValue
	{
		float	fMin;				// Minimum value slider can have
		float	fMax;				// Maximum value slider can have
		float	fCurrentValue;		// This is between fMin and fMax
		float	fPosition;			// The position is between 0.0 and 1.0

		// Track Slider specifics
		IGraphic	fTrackGraphic;
		IGraphic	fThumbGraphic;

		Orientation	fOrientation;

		// Events we generate
		public event System.EventHandler PositionChangedEvent;
		//public event System.EventHandler ValueChangedEvent;

		public TrackSlider(string name, IGraphic track, IGraphic thumb,
						Orientation orient,
						float min, float max, 
						float initialValue)
			: base(name, 0,0,0,0)
		{
			fTrackGraphic = track;
			fThumbGraphic = thumb;
			fOrientation = orient;

			fMin = min;
			fMax = max;
			fCurrentValue = initialValue;
			fPosition = initialValue;

			int yoffset = 0;
			int xoffset = 0;
			int trackyoffset = 0;
			int trackxoffset = 0;
	
			if ((track==null) || (thumb==null))
				return ;
		
			AddGraphic(fTrackGraphic, null);
			AddGraphic(fThumbGraphic, null);

			if (orient == Orientation.Horizontal)
			{
				// Must align thumb vertically with track.
				int midy = fTrackGraphic.Frame.Top + (fTrackGraphic.Frame.Height+1)/2;
				int thumbHeight = fThumbGraphic.Frame.Height+1;
				yoffset = midy - (thumbHeight / 2);
				xoffset = fTrackGraphic.Frame.Left;
			} 
			else
			{
				// Must align thumb horizontally with track.
				int midx = Frame.Left+(Frame.Width+1)/2;
				int thumbWidth = fThumbGraphic.Frame.Width+1;
				int trackWidth = fTrackGraphic.Frame.Width+1;
				xoffset = midx - (thumbWidth / 2);
				yoffset = fTrackGraphic.Frame.Top;

				trackxoffset = midx - (trackWidth/2);
				trackyoffset = fTrackGraphic.Frame.Top;
		
				fTrackGraphic.MoveTo(trackxoffset, trackyoffset);
			}
	
			fThumbGraphic.MoveTo(xoffset,yoffset);
			RectangleI newFrame = fTrackGraphic.Frame;
			//newFrame.Union(fThumbGraphic.Frame);
			Frame = newFrame;

            SetValue(initialValue, false);
		}
	
		public virtual void	SetValue(float newValue, bool invoke)
		{
			if (newValue < fMin)
				fCurrentValue = fMin;
			else if (newValue > fMax)
				fCurrentValue = fMax;
			else
				fCurrentValue = newValue;

			ValueChanged(fCurrentValue, invoke);
		}

		public virtual void	SetValueBy(int mouseX, int mouseY, bool invoke)
		{
			float thumbSize=0;
			float lowestThumb=0;
			float highestThumb=0;
			float travelLength=0;
	
			float newX = mouseX;
			float newY = mouseY;

			if (Orientation.Horizontal == fOrientation)
			{
				// If the track is layed out horizontally
				thumbSize = fThumbGraphic.Frame.Width+1.0F;
				lowestThumb = fTrackGraphic.Frame.Left;
				highestThumb = fTrackGraphic.Frame.Right - thumbSize;
				travelLength = highestThumb - lowestThumb;
				if (newX > highestThumb)
					newX = highestThumb;
				else if (newX < lowestThumb)
					newX = lowestThumb;
				fPosition = (newX - lowestThumb) / travelLength;
			} 
			else
			{
				// If the track is layed out vertically
				thumbSize = fThumbGraphic.Frame.Height+1;
				lowestThumb = fTrackGraphic.Frame.Top;
				highestThumb = fTrackGraphic.Frame.Bottom - thumbSize;
				travelLength = highestThumb - lowestThumb;
				if (newY > highestThumb)
					newY = highestThumb;
				else if (newY < lowestThumb)
					newY = lowestThumb;
				fPosition = (travelLength - (newY - lowestThumb)) / travelLength;
			}
	
			// The current position has been set, now calculate
			// the current value based on this position.	
			fCurrentValue = fMin + (fMax-fMin)*fPosition;
	
			PositionChanged(fPosition, invoke);
		}

		public virtual void	SetPosition(float newValue, bool invoke)
		{
			if (newValue < 0.0F)
				fPosition = 0.0F;
			else if (newValue > 1.0F)
				fPosition = 1.0F;
			else
				fPosition = newValue;

			PositionChanged(fPosition,invoke);
		}

		public virtual void	SetPositionBy(int mouseX, int mouseY, bool invoke)
		{
			float thumbSize=0;
			float lowestThumb=0;
			float highestThumb=0;
			float travelLength=0;
	
			float newX = mouseX;
			float newY = mouseY;

			float diffX = mouseX - LastMouseX;
			float diffY = newY - LastMouseY;

			// If there's no significant movement, just return
			//if ((Math.Abs(diffX) < 1) && (Math.Abs(diffY)<1))
			//	return ;
	

			if (Orientation.Horizontal == fOrientation)
			{
				// If the track is layed out horizontally
				thumbSize = fThumbGraphic.Frame.Width+1;
				lowestThumb = fTrackGraphic.Frame.Left;
				highestThumb = fTrackGraphic.Frame.Right - thumbSize;
				travelLength = highestThumb - lowestThumb;
				if (newX > highestThumb)
					newX = highestThumb;
				else if (newX < lowestThumb)
					newX = lowestThumb;
				fPosition = (float)(newX - lowestThumb) / (float)travelLength;
			} 
			else
			{
				// If the track is layed out vertically
				thumbSize = fThumbGraphic.Frame.Height+1;
				lowestThumb = fTrackGraphic.Frame.Top;
				highestThumb = fTrackGraphic.Frame.Bottom - thumbSize;
				travelLength = highestThumb - lowestThumb;
				if (newY > highestThumb)
					newY = highestThumb;
				else if (newY < lowestThumb)
					newY = lowestThumb;
				fPosition = (float)(travelLength - (newY - lowestThumb)) / (float)travelLength;
			}
	
			// The current position has been set, now calculate
			// the current value based on this position.	
			fCurrentValue = fMin + (fMax-fMin)*fPosition;
			
			LastMouseX = mouseX;
			LastMouseY = mouseY;

			PositionChanged(fPosition, invoke);
		}

		public virtual void	SetRange(float min, float max)
		{
			fMin = min;
			fMax = max;
		}
	
		public virtual void	GetRange(ref float min, ref float max)
		{
			min = fMin;
			max = fMax;
		}

		public virtual float Value
		{
			get {return fCurrentValue;}
		}
		
		public virtual float Position
		{
			get {return fPosition;}
		}
		
	
		public virtual void	ValueChanged(float newValue, bool invoke)
		{
			// Turn the value into a position and call 
			// PositionChanged
			float newPosition = (newValue - fMin)/(fMax-fMin);
			PositionChanged(newPosition, invoke);

			//if (invoke && ValueChangedEvent != null)
			//	ValueChangedEvent(this, newValue);
		}

		// This one is all important for subclasses to implement
		public virtual void	PositionChanged(float newPosition, bool invoke)
		{
			int thumbSize=0;
            int lowestThumb = 0;
            int highestThumb = 0;
            int travelLength = 0;
	
			int newX = 0;
			int newY = 0;

			//Console.WriteLine("TrackSlider.PositionChanged - NewPos: {0}", newPosition);

			if (Orientation.Horizontal == fOrientation)
			{
				// If the track is layed out horizontally
				thumbSize = fThumbGraphic.Frame.Width+1;
				lowestThumb = fTrackGraphic.Frame.Left;
				highestThumb = fTrackGraphic.Frame.Right - thumbSize;
				travelLength = highestThumb - lowestThumb;
				newY = fThumbGraphic.Frame.Top;
				newX = lowestThumb + (int)(travelLength * newPosition);
			} 
			else
			{
				// If the track is layed out vertically
				thumbSize = fThumbGraphic.Frame.Height+1;
				lowestThumb = fTrackGraphic.Frame.Top;
				highestThumb = fTrackGraphic.Frame.Bottom - thumbSize;
				travelLength = highestThumb - lowestThumb+1;
				newX = fThumbGraphic.Frame.Left;
				newY = lowestThumb + (int)((1.0 - newPosition) * travelLength);
			}

			fThumbGraphic.MoveTo(newX, newY);

            Invalidate();
			
			if (invoke && PositionChangedEvent != null)
				PositionChangedEvent(this, EventArgs.Empty);
		}
	
		public override void OnMouseMove(MouseActivityArgs e)
		{
			if (Tracking && Depressed)
			{
				SetPositionBy(e.X, e.Y, true);

/*
			int diffX = 0;
			int diffY = 0;
				diffX = e.X - LastMouseX;
				diffY = e.Y - LastMouseY;
				if ((Math.Abs(diffX) > 0) || (Math.Abs(diffY)>0))
					SetPositionBy(e.X, e.Y, true);
	
				LastMouseX = e.X;
				LastMouseY = e.Y;
*/
			}

			//Console.WriteLine("TrackSlider.OnMouseMove: {0}", e.ToString());
			//if (MouseMoveEvent != null)
			//	MouseMoveEvent(this, e);
		}
	}
}