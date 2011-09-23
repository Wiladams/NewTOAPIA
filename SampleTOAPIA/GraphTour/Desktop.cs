using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.User32;
using TOAPI.Types;

namespace GraphTour
{
    public class Desktop
    {
        IntPtr fStationHandle;   // Handle to the window station this desktop belongs to
        IntPtr fDesktopHandle;   // Handle to the desktop object

        public Desktop(IntPtr stationHandle, IntPtr desktopHandle)
        {
            fStationHandle = stationHandle;
            fDesktopHandle = desktopHandle;
        }

        public static void GetDesktops(IntPtr aStation, EnumDesktopsDelegate callFunc)
        {
            int result = User32.EnumDesktops(aStation, callFunc, IntPtr.Zero);
        }

        public bool EnumerateDesktopsProc(string desktopname, IntPtr lParam)
        {
            return true;
        }
    }
}
