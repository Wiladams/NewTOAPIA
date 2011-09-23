using System;
using System.Runtime.InteropServices;

using TOAPI.Types;


	public class Win32Cursor : ICursor, IHandle
	{
		int fIdentifier;
		IntPtr fHandle;
		int fShowCount;
		Win32Window fWindow;

		// Default system cursors
		public static Win32Cursor Arrow = new Win32Cursor(IDC_ARROW);
		public static Win32Cursor IBeam = new Win32Cursor(IDC_IBEAM);
		public static Win32Cursor Wait = new Win32Cursor(IDC_WAIT);
		public static Win32Cursor Cross = new Win32Cursor(IDC_CROSS);
		public static Win32Cursor UpArrow = new Win32Cursor(IDC_UPARROW);
		public static Win32Cursor Size = new Win32Cursor(IDC_SIZE);
		public static Win32Cursor Icon = new Win32Cursor(IDC_ICON);
		public static Win32Cursor SizeNWSE = new Win32Cursor(IDC_SIZENWSE);
		public static Win32Cursor SizeNESW = new Win32Cursor(IDC_SIZENESW);
		public static Win32Cursor SizeWE = new Win32Cursor(IDC_SIZEWE);
		public static Win32Cursor SizeNS = new Win32Cursor(IDC_SIZENS);
		public static Win32Cursor SizeAll = new Win32Cursor(IDC_SIZEALL);
		public static Win32Cursor None = new Win32Cursor(IDC_NO);
		public static Win32Cursor AppStarting = new Win32Cursor(IDC_APPSTARTING);
		public static Win32Cursor Help = new Win32Cursor(IDC_HELP);

		static Win32Cursor gCurrentCursor = Arrow;


		public static Win32Cursor Current
		{
			get {return Win32Cursor.gCurrentCursor;}
			set 
			{
				Win32Cursor.gCurrentCursor = value;
				Win32Cursor.SetCursor(value.Handle);
			}
		}


		public Win32Cursor(int ident)
		{
			fIdentifier = ident;
			fHandle = Win32Cursor.LoadCursor(IntPtr.Zero, ident);
			fShowCount = 0;
			fWindow = null;
		}
		
		public IntPtr Handle
		{
			get {return fHandle;}
		}

			/// <summary>
			/// FocusWindow
			/// 
			/// This method is called from places like a Win32Window's Message Dispatch
			/// loop.  When the window gains focus, it sets itself as the FocusWindow.  
			/// When it loses focus, it sets null as the FocusWindow.
			/// All we do here is create the cursor and destroy it as appropriate.  This
			/// does not in and of itself make the caret show itself.  A control that is
			/// doing input would deal with that.
			/// </summary>
		public Win32Window FocusWindow 
		{
			get {return fWindow;}
			set 
			{
				fWindow = value;
/*				if (fWindow != null)
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
*/
			}	   
		}
		


		/// <summary>
		/// MoveTo
		/// 
		/// Moves the Cursor to the specified location, in the current focuswindow's
		/// client coordinates.  If the cursor is hidden, it will still move, but
		/// it won't get automatically displayed.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns>Returns true if it successfully moved the cursor.</returns>
		public virtual bool MoveTo(int x, int y)
		{
			bool result = Win32Cursor.SetCursorPos(x, y);
			
			return result;
		}

        public virtual bool MoveBy(int x, int y)
        {
            return false;
        }

		//
		// MakeVisible will call the Show() method until the Cursor 
		// is actually visible.  You would use this method if you are
		// not keeping track of how many times Hide() gets called, but
		// you just want the Cursor to be visible.
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

			fShowCount = Win32Cursor.ShowCursor(true);

			return true;
		}

		public virtual bool Hide()
		{
			if (fWindow == null)
				return false;

			fShowCount = Win32Cursor.ShowCursor(false);

			return true;
		}

		const int 
			IDC_ARROW = 32512,
			IDC_IBEAM = 32513,
			IDC_WAIT = 32514,
			IDC_CROSS = 32515,
			IDC_UPARROW = 32516,
			IDC_SIZE = 32640,
			IDC_ICON = 32641,
			IDC_SIZENWSE = 32642,
			IDC_SIZENESW = 32643,
			IDC_SIZEWE = 32644,
			IDC_SIZENS = 32645,
			IDC_SIZEALL = 32646,
			IDC_NO = 32648,
			IDC_APPSTARTING = 32650,
			IDC_HELP = 32651;
		
		/// <summary>
		/// The following functions are all the ones defined in the user32.dll library
		/// related to Cursor manipulations.  They are in here because we don't want them
		/// to be generally available, we only want Cursor access to occur through this
		/// particular class.
		/// </summary>

		[DllImport("user32.dll")]
		static extern IntPtr CreateCursor(IntPtr hInst, int xHotSpot, int yHotSpot, int nWidth, int nHeight, ref byte [] pvANDPlane, ref byte [] pvXORPlane);		
		
		[DllImport("user32.dll")]
		static extern IntPtr LoadCursor(IntPtr hInstance,  int lpCursorName);				
	
		[DllImport("user32.dll")]
		static extern int SetCursor(IntPtr hCursor);

		[DllImport("user32.dll")]
		static extern int ShowCursor(bool bShow);	
	
		[DllImport("user32.dll")]
		static extern bool SetCursorPos(int X,int Y);
	
	}
