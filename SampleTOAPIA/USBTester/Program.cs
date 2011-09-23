using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace USBTester
{
    using TOAPI.HID;
    using TOAPI.Setup;

    class Program
    {
        static void Main(string[] args)
        {
            IntPtr deviceInfoSet;
            Guid myGuid = Guid.NewGuid();
            TOAPI.HID.Hid.HidD_GetHidGuid(ref myGuid);

            deviceInfoSet = TOAPI.Setup.setup.SetupDiGetClassDevs(ref myGuid, IntPtr.Zero, IntPtr.Zero, setup.DIGCF_PRESENT | setup.DIGCF_DEVICEINTERFACE);
        }
    }
}
