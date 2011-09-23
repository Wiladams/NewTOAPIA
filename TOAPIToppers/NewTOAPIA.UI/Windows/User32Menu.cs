using System;
using System.Runtime.InteropServices;

using NewTOAPIA.UI;
using TOAPI.User32;

namespace NewTOAPIA.UI
{

	public class User32Menu : IHandle
	{
		IntPtr fHandle;
		string fText;

		public static User32Menu Empty = new User32Menu("Empty");
		
		public static User32Menu SystemMenu(Window win)
		{
			User32Menu aMenu = new User32Menu(GetSystemMenu(win.Handle, false));
			return aMenu;
		}

		public User32Menu(IntPtr hMenu)
		{
			fHandle = hMenu;
		}

		public User32Menu(string name)
		{
			fText = name;
			fHandle = Create();
		}

		public IntPtr Handle 
		{
			get {return fHandle;}
		}

		public string Text
		{
			get {return fText;}
			// BUGBUG - do some more stuff to redisplay menu
			set {fText = value;}
		}

		protected virtual IntPtr Create()
		{
			return CreatePopupMenu();
		}

		public void Add(User32MenuItem item)
		{
			AppendMenu(Handle, MF_STRING, item.Identifier, item.Text);
		}

		public void Add(User32Menu menu)
		{
			AppendMenu(Handle, MF_POPUP, (uint)menu.Handle, menu.Text);
		}

		public void Add(string name, uint newID)
		{
			AppendMenu(Handle, MF_STRING, newID, name);
		}

		public void AppendSeparator()
		{
			AppendMenu(Handle, MF_SEPARATOR, 0, string.Empty);
		}

		// User32Menu interop calls
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool AppendMenu(IntPtr hMenu,uint uFlags, uint uIDNewItem, string lpNewItem);
		
		
		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		static extern IntPtr CreatePopupMenu();

		[DllImport("user32.dll", ExactSpelling=true, CharSet=CharSet.Auto)]
		static extern IntPtr GetSystemMenu(IntPtr hWnd,bool bRevert);

		internal const uint 
			MF_INSERT = 0x00000000,
			MF_CHANGE = 0x00000080,
			MF_APPEND = 0x00000100,
			MF_DELETE = 0x00000200,
			MF_REMOVE = 0x00001000,
			MF_BYCOMMAND = 0x00000000,
			MF_BYPOSITION = 0x00000400,
			MF_SEPARATOR = 0x00000800,
			MF_ENABLED = 0x00000000,
			MF_GRAYED = 0x00000001,
			MF_DISABLED = 0x00000002,
			MF_UNCHECKED = 0x00000000,
			MF_CHECKED = 0x00000008,
			MF_USECHECKBITMAPS = 0x00000200,
			MF_STRING = 0x00000000,
			MF_BITMAP = 0x00000004,
			MF_OWNERDRAW = 0x00000100,
			MF_POPUP = 0x00000010,
			MF_MENUBARBREAK = 0x00000020,
			MF_MENUBREAK = 0x00000040,
			MF_UNHILITE = 0x00000000,
			MF_HILITE = 0x00000080,
			MF_DEFAULT = 0x00001000,
			MF_SYSMENU = 0x00002000,
			MF_HELP = 0x00004000,
			MF_RIGHTJUSTIFY = 0x00004000,
			MF_MOUSESELECT = 0x00008000,
			MF_END = 0x00000080;

/*		struct MENUITEMINFO 
		{
			uint    cbSize; 
			uint    fMask; 
			uint    fType; 
			uint    fState; 
			uint    wID; 
			int   hSubMenu; 
			IntPtr hbmpChecked; 
			IntPtr hbmpUnchecked; 
			IntPtr dwItemData; 
			string  dwTypeData; 
			uint    cch; 
			IntPtr hbmpItem;
		} 	
*/
	}
}