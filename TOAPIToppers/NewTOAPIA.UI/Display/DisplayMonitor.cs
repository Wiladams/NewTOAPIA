using System;
using System.Drawing;

using TOAPI.User32;

namespace NewTOAPIA.UI
{
	/// <summary>
	/// The DisplayMonitor class allows you to access some information about 
	/// the various monitors available on a system.  At present, it assumes a 
	/// single primary display monitor.
	/// </summary>
	public class DisplayMonitor
	{
		static DisplayMonitor gPrimaryMonitor = new DisplayMonitor();
		IntPtr fHandle;

		MONITORINFOEX fMonitorInfo;
		bool fPrimary;
		Rectangle fWorkArea;
		Rectangle fBounds;
		string fDeviceName;

		public static DisplayMonitor PrimaryMonitor 
		{
			get {return DisplayMonitor.gPrimaryMonitor;}
		}

		// For the default constructor, we'll just get the
		// handle for the Primary Monitor
		public DisplayMonitor()
		{
			fHandle = TOAPI.User32.User32.MonitorFromWindow(User32.GetDesktopWindow(), MONITOR_DEFAULTTOPRIMARY);
			fMonitorInfo = new MONITORINFOEX();

			User32.GetMonitorInfo(Handle, ref fMonitorInfo);
			fBounds = new Rectangle(fMonitorInfo.rcMonitor.X, fMonitorInfo.rcMonitor.Y, fMonitorInfo.rcMonitor.Width,fMonitorInfo.rcMonitor.Height);
			fWorkArea = new Rectangle(fMonitorInfo.rcWork.X, fMonitorInfo.rcWork.Y, fMonitorInfo.rcWork.Width,fMonitorInfo.rcWork.Height);
			fPrimary = (fMonitorInfo.dwFlags & MONITORINFOF_PRIMARY) != 0;

			// If you don't truncate, you get padding at the end
			//int count = fMonitorInfo.szDevice.Length;
			//while(count > 0 && fMonitorInfo.szDevice[count - 2] == '\0') 
			//{
			//	count -= 2;
			//}
			//fDeviceName = System.Text.Encoding.Unicode.GetString(fMonitorInfo.szDevice, 0, count);
			fDeviceName = fMonitorInfo.szDevice;
		}
	
		public IntPtr Handle
		{
			get {return fHandle;}
		}

		public bool Primary
		{
			get {return fPrimary;}
		}

		public Rectangle Bounds
		{
			get 
			{
				return fBounds;
			}
		}

		public Rectangle WorkingArea
		{
			get 
			{
				return fWorkArea;
			}
		}

		public string Name
		{
			get {return fDeviceName;}
		}

		public override string ToString() 
		{
			return "<Monitor " + "name='" + Name + "' primary='" + fPrimary.ToString() + "'>"
				+ "<bounds>" + Bounds.ToString() + "</bounds>"
				+ "<workarea>" + WorkingArea.ToString() + "</workarea>"
				+ "</Monitor>";
		}

		public  const int 
			MONITOR_DEFAULTTONULL		=	0x00000000,
			MONITOR_DEFAULTTOPRIMARY	=	0x00000001,
			MONITOR_DEFAULTTONEAREST	=	0x00000002,
			MONITORINFOF_PRIMARY		=	0x00000001;


	}
}
