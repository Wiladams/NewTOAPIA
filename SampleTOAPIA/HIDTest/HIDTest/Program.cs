using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HIDTest
{
    using TOAPI.HID;
    using TOAPI.Setup;
    using TOAPI.Types;

    using NewTOAPIA;

    class Program
    {
        static void Main(string[] args)
        {
            Guid hidGuid = HidDevice.HIDGuid;

            //Console.WriteLine("HID GUID: {0}", hidGuid);

            foreach (HidDevice device in HidDevice.GetDevices())
            {
                Console.WriteLine("{0}", device);
            }

            Console.ReadLine();
        }
    }
}
