using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.User32;
using TOAPI.Types;

namespace GraphTour
{
    public class WindowStation
    {
        static string[] gStations;
        static List<string> gWindowStationList;

        string fStationName;
        bool fInherit;
        int fAccess;
        IntPtr fStationHandle;

        string[] fDesktopNames;
        List<string> fDesktopNameList;


        public WindowStation(string stationName, bool inherit, int access)
        {
            fStationName = stationName;
            fInherit = inherit;
            fAccess = access;
        }

        public void Open()
        {
            fStationHandle = User32.OpenWindowStation(fStationName, fInherit, fAccess);
        }

        public bool Close()
        {
            bool success = (0!= User32.CloseWindowStation(fStationHandle));

            return success;
        }

        #region Static Methods

        public static string[] GetStationNames()
        {
            // Set the list of stations to null to start
            gStations = null;

            // Allocate a new array to be filled
            gWindowStationList = new List<string>();

            int result = User32.EnumWindowStationsW(new EnumWindowStationProcW(ListAStation), IntPtr.Zero);

            gStations = gWindowStationList.ToArray();

            return gStations;
        }

        static int ListAStation(StringBuilder stationName, IntPtr param1)
        {
            gWindowStationList.Add(stationName.ToString());

            return 0;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return fStationName; }
        }

        public string[] DesktopNames
        {
            get
            {
                GetDesktopNames();

                return fDesktopNames;
            }
        }

        void GetDesktopNames()
        {
            // Get the list of desktops
            fDesktopNameList = new List<string>();
            int result = User32.EnumDesktops(fStationHandle, new EnumDesktopsDelegate(this.ListADesktop), IntPtr.Zero);

            fDesktopNames = fDesktopNameList.ToArray();
        }

        int ListADesktop(string desktopName, IntPtr lParam)
        {
            fDesktopNameList.Add(desktopName);

            return 1;
        }

        #endregion
    }
}
