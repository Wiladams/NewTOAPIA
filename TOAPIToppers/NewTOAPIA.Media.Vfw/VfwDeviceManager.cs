using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.AviCap32;

namespace NewTOAPIA.Media
{
    public class VfwDeviceDriver
    {
        public string Name;
        public string Version;

        public VfwDeviceDriver(string name, string version)
        {
            this.Name = name; this.Version = version;
        }

        public override string ToString()
        {
            return Name + "  " + Version;
        }

    }

    public class VfwDeviceManager
    {
        static int gmaxDevices = 10;    // Values range from 0 - 9 according to the Vfw documentation

        List<VfwDeviceDriver> devices;

        public VfwDeviceManager()
        {
            devices = new List<VfwDeviceDriver>(gmaxDevices);
            Refresh();
        }

        public void Refresh()
        {
            string name = "".PadRight(100), version = "".PadRight(100);

            for (short i = 0; i < gmaxDevices; i++)
            {
                if (AviCap32.capGetDriverDescription(i, ref name, 100, ref version, 100))
                {
                    VfwDeviceDriver d = new VfwDeviceDriver(name, version);
                    d.Name = name.Trim();
                    d.Version = version.Trim();
                    devices.Add(d);
                }
            }
        }

        public VfwDeviceDriver Device(int index)
        {
            return devices[index];
        }

        public VfwDeviceDriver[] Devices
        {
            get { return devices.ToArray(); }
        }
    }
}
