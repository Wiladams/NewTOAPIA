using System;
using System.Runtime.InteropServices;

namespace NewTOAPIA.UI
{

	public class User32MenuItem
	{
		uint fIdentifier;
		string fText;
		bool fChecked;
		bool fEnabled;
		User32Menu fMenu;

		User32MenuItem(string text, uint identifier)
		{
			fIdentifier = identifier;
			fText = text;
			fChecked = false;
			fEnabled = true;
			fMenu = User32Menu.Empty;
		}

		public bool Checked 
		{
			get {return fChecked;}
			set {
				fChecked = value;
				if (fChecked)
					CheckMenuItem(Menu.Handle, fIdentifier, User32Menu.MF_BYCOMMAND|User32Menu.MF_CHECKED);
				else
					CheckMenuItem(Menu.Handle, fIdentifier, User32Menu.MF_BYCOMMAND|User32Menu.MF_UNCHECKED);
			}
		}

		public bool Enabled
		{
			get {return fEnabled;}
			set 
			{
				fEnabled = value;
				if (fEnabled)
					EnableMenuItem(Menu.Handle, fIdentifier, User32Menu.MF_BYCOMMAND|User32Menu.MF_ENABLED);
				else
					EnableMenuItem(Menu.Handle, fIdentifier, User32Menu.MF_BYCOMMAND|User32Menu.MF_DISABLED);
			}
		}

		public User32Menu Menu
		{
			get {return fMenu;}
			set 
			{
				fMenu = value;
			}
		}

		public uint Identifier
		{
			get {return fIdentifier;}
			set {fIdentifier = value;}
		}

		public string Text
		{
			get {return fText;}
			// BUGBUG - if we're attached to a menu, make sure
			// it redraws.
			set {fText = value;}
		}

		// Interop stuff
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern int CheckMenuItem(IntPtr hmenu, uint uIDCheckItem, uint uCheck);
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
	}
}