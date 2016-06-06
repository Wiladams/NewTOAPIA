

namespace NewTOAPIA.UI
{
    using System;
    using System.Runtime.InteropServices;
    
    using NewTOAPIA.Graphics;

	public class Win32Caret
	{
		int fWidth;
		int fHeight;
		uint fBlinkTime;
		int fX;
		int fY;
		int fShowCount;
		IWindow fWindow;
		public static Win32Caret Empty = new Win32Caret(0,0);
		static Win32Caret gCurrentCaret = Empty;

		public static Win32Caret Current
		{
			get {return Win32Caret.gCurrentCaret;}
			set {Win32Caret.gCurrentCaret = value;}
		}

		public Win32Caret(int width, int height)
		{
			fShowCount = 0;
			fWidth = width;
			fHeight = height;
			fBlinkTime = 500;
			fX = 0;
			fY = 0;
			fWindow = null;
		}
		
		/// <summary>
		/// FocusWindow
		/// 
		/// This method is called from places like a Win32Window's Message Dispatch
		/// loop.  When the window gains focus, it sets itself as the FocusWindow.  
		/// When it loses focus, it sets null as the FocusWindow.
		/// All we do here is create the caret and destroy it as appropriate.  This
		/// does not in and of itself make the caret show itself.  A control that is
		/// doing input would deal with that.
		/// </summary>
		public IWindow FocusWindow 
		{
			get {return fWindow;}
			set 
			{
				fWindow = value;
				if (fWindow != null)
				{
					Win32Caret.CreateCaret(fWindow.Handle, 0, fWidth, fHeight);
					Win32Caret.SetCaretBlinkTime(fBlinkTime);
					Win32Caret.Current = this;
				} 
				else
				{
					Win32Caret.DestroyCaret();
					Win32Caret.Current = Win32Caret.Empty;
				}
			}	   
		}
		
		/// <summary>
		/// The amount of time, in milliseconds, that it takes the caret to blink
		/// on and off.
		/// </summary>
		public uint BlinkTime
		{
			get {return fBlinkTime;}
			set 
			{
				if (Win32Caret.SetCaretBlinkTime(fBlinkTime))
					fBlinkTime = value;
			}
		}

		/// <summary>
		/// Simple Shape setting.  This method does not allow you to set the caret
		/// to anything other than a solid block of the specified width and height.
		/// </summary>
		public bool SetShape(int width, int height)
		{
			fWidth = width;
			fHeight = height;
            bool result = false ;

			if (fWindow != null)
			{
				// Destroy current Caret
				// Create new caret
				// put the new caret in the same state
				// as the old caret
                result = Win32Caret.CreateCaret(fWindow.Handle, 0, fWidth, fHeight);
            }

			return result;
		}

		/// <summary>
		/// MoveTo
		/// 
		/// Moves the Caret to the specified location, in the current focuswindow's
		/// client coordinates.  If the cursor is hidden, it will still move, but
		/// it won't get automatically displayed.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>Returns true if it successfully moved the cursor.</returns>
		public virtual bool MoveTo(int x, int y)
		{
			fX = x;
			fY = y;
			bool result = Win32Caret.SetCaretPos(x, y);
			
			return result;
		}

        public bool MoveTo(Point2I aPoint)
        {
            return MoveTo(aPoint.x, aPoint.y);
        }

		//
		// MakeVisible will call the Show() method until the caret 
		// is actually visible.  You would use this method if you are
		// not keeping track of how many times Hide() gets called, but
		// you just want the Caret to be visible.
		public virtual void MakeVisible()
		{
			while ((fWindow != null) && (fShowCount < 1))
			{
				if (!Show())
					break;
			} 
		}

		public virtual bool Show()
		{
			if (fWindow == null)
				return false;
				
			bool result = Win32Caret.ShowCaret(fWindow.Handle);
			if (result)
				fShowCount++;

			return result;
		}

		public virtual bool Hide()
		{
			if (fWindow == null)
				return false;

			bool result = Win32Caret.HideCaret(fWindow.Handle);
			if (result)
				fShowCount--;

			return result;
		}

		/// <summary>
		/// The following functions are all the ones defined in the user32.dll library
		/// related to Caret manipulations.  They are in here because we don't want them
		/// to be generally available, we only want Caret access to occur through this
		/// particular class.
		/// </summary>
		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool CreateCaret(IntPtr hWnd, int bitmap, int width, int height);
		
		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool DestroyCaret();

		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern uint GetCaretBlinkTime();
		
		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool GetCaretPos(ref Point2I aPoint);

		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool HideCaret(IntPtr hWnd);

		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool SetCaretBlinkTime(uint uMSeconds);

		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool SetCaretPos(int x, int y);
		
		[DllImport("user32.dll",  CharSet=CharSet.Auto)]
		static extern bool ShowCaret(IntPtr hWnd);
		
	}
}