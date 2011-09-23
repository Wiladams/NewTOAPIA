using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    public class WinMMAux
    {

        public static AUXCAPSW[] GetAuxCapabilities()
        {
            int numCaps = GetNumberOfAuxiliaryDevices();
            if (numCaps < 1)
                return null;


            AUXCAPSW[] caps = new AUXCAPSW[numCaps];

            for (int i = 0; i < numCaps; i++)
            {
                AUXCAPSW newCaps = new AUXCAPSW();
                IntPtr devID = new IntPtr(i);

                winmm.auxGetDevCapsW(devID, ref newCaps, Marshal.SizeOf(typeof(AUXCAPSW)));

                caps[i] = newCaps;
            }

            return caps;
        }

        public static int GetNumberOfAuxiliaryDevices()
        {
            int result = winmm.auxGetNumDevs();
            return result;
        }
    }
}
