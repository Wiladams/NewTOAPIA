using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    public class WinMMMixer
    {
        IntPtr fDeviceHandle;
        IntPtr fDeviceID;

        #region Constructor
        public WinMMMixer(IntPtr deviceID)
        {
            fDeviceID = deviceID;
        }
        #endregion

        #region Properties
        public MIXERCAPS MixerCapabilities
        {
            get
            {
                MIXERCAPS caps = new MIXERCAPS();

                winmm.mixerGetDevCaps(fDeviceID, ref caps, Marshal.SizeOf(typeof(MIXERCAPS)));

                return caps;
            }
        }

        public IntPtr MixerID
        {
            get
            {
                int result = winmm.mixerGetID(fDeviceHandle, ref fDeviceID, 0);

                return fDeviceID;
            }
        }
        #endregion

        #region Methods
        public void Open()
        {
            int result = winmm.mixerOpen(ref fDeviceHandle, fDeviceID, IntPtr.Zero, IntPtr.Zero, 0);
        }

        public void Close()
        {
            int result = winmm.mixerClose(fDeviceHandle);
        }

        public MIXERLINE[] GetAllMixerLineInfo()
        {
            // First get the capabilities, because that will tell 
            // us how many lines there are.
            // The MIXERCAPS.cDestinations tells us how many lines there are
            MIXERCAPS caps = MixerCapabilities;

            List<MIXERLINE> allLines = new List<MIXERLINE>();

            for (int i = 0; i < caps.cDestinations; i++)
            {
                // Get destination information
                MIXERLINE aLine = new MIXERLINE();
                aLine.Init();
                aLine.dwDestination = (uint)i;

                int result = winmm.mixerGetLineInfo(fDeviceHandle, ref aLine, 0);

                allLines.Add(aLine);

                // For each of the connections on the line, get the source line
                // information
                for (int connection = 0; connection < aLine.cConnections; connection++)
                {
                    MIXERLINE srcLine = new MIXERLINE();
                    srcLine.Init();
                    aLine.dwDestination = (uint)i;

                    int srcResult = winmm.mixerGetLineInfo(fDeviceHandle, ref srcLine, winmm.MIXER_GETLINEINFOF_SOURCE);

                    allLines.Add(srcLine);
                }
            }

            return allLines.ToArray();

        }
        #endregion

        #region Static Helpers
        public static MIXERCAPS[] GetAllMixerCapabilities()
        {
            IntPtr mixerID = new IntPtr();

            int numMixers = GetNumberOfMixers();
            if (numMixers < 1)
                return null;

            MIXERCAPS[] allCaps = new MIXERCAPS[numMixers];

            for (int i = 0; i < numMixers; i++)
            {
                mixerID = new IntPtr(i);
                MIXERCAPS caps = new MIXERCAPS();

                winmm.mixerGetDevCaps(mixerID, ref caps, Marshal.SizeOf(typeof(MIXERCAPS)));

                allCaps[i] = caps;
            }

            return allCaps;
        }

        public static int GetNumberOfMixers()
        {
            int result = winmm.mixerGetNumDevs();
            return result;
        }
        #endregion
    }
}
