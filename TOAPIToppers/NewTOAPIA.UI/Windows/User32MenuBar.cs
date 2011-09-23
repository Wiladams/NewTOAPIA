using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.UI
{

	public class User32MenuBar : User32Menu
	{
		Window fWindow;

		public User32MenuBar()
			: base("MenuBar")
		{
		}

		// Create our specific menu resources
		protected override IntPtr Create()
		{
			return CreateMenu();
		}

		public Window Window 
		{
			get {return fWindow;}
			set 
			{
				bool retValue;

				if ((value == null) && (fWindow != null))
				{
					retValue = SetMenu(fWindow.Handle, IntPtr.Zero);
				}

				fWindow = value;
				if (fWindow != null)
					retValue = SetMenu(fWindow.Handle, Handle);
			}
		}

		// Interop calls for menus
		
		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern IntPtr CreateMenu();

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool DestroyMenu(int hMenu);

		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool DrawMenuBar(int hWnd);
		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool EnableMenuItem(int hMenu,uint uIDEnableItem, uint uEnable);

/*		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern int GetMenuString(
			HMENU hMenu,      // handle to the menu
			UINT uIDItem,     // menu item identifier
			LPTSTR lpString,  // buffer for the string
			int nMaxCount,    // maximum length of string
			UINT uFlag        // options
			);
*/		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool InsertMenu(IntPtr hMenu,uint uPosition,uint uFlags,
			ref uint uIDNewItem, string lpNewItem);
		
/*		[DllImport("user32", ExactSpelling=true, CharSet=System.Runtime.InteropServices.CharSet.Auto)]
		static extern bool InsertMenuItem(
			HMENU hMenu,           // handle to menu
			uint uItem,            // identifier or position
			bool fByPosition,      // meaning of uItem
			LPCMENUITEMINFO lpmii  // menu item information
			);
*/		
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool SetMenu(IntPtr hWnd,IntPtr hMenu);
	}
}
