using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;

using SlimDX.DXGI;

namespace DXGITest
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory dxgifactory = new Factory();

            int adaptorCount = dxgifactory.GetAdapterCount();

            Console.WriteLine("Adaptor Count: {0}", adaptorCount);

            for (int i = 0; i < adaptorCount; i++)
            {
                Adapter adapt = dxgifactory.GetAdapter(i);

                PrintAdaptor(adapt);
            }
            Console.ReadLine();
        }

        static void PrintAdaptor(Adapter adapt)
        {
            Console.WriteLine("== Adaptor ==");
            Console.WriteLine("              Description: {0}", adapt.Description.Description);
            Console.WriteLine("                Vendor ID: {0}", adapt.Description.VendorId);
            Console.WriteLine("                 Revision: {0}", adapt.Description.Revision);
            Console.WriteLine("                     LUID: {0}", adapt.Description.Luid);
            Console.WriteLine("                Device ID: {0}", adapt.Description.DeviceId);
            Console.WriteLine("             Subsystem ID: {0}", adapt.Description.SubsystemId);
            Console.WriteLine("  Dedicated System Memory: {0}", adapt.Description.DedicatedSystemMemory);
            Console.WriteLine("     Shared System Memory: {0}", adapt.Description.SharedSystemMemory);
            Console.WriteLine("             Video Memory: {0}", adapt.Description.DedicatedVideoMemory);


            int numOutput = adapt.GetOutputCount();

            for (int i = 0; i < numOutput; i++)
            {
                Output adaptOut = adapt.GetOutput(i);

                PrintOutput(adaptOut);
            }
        }

        static void PrintOutput(Output adaptOut)
        {
            Console.WriteLine("== Output ==");
            Console.WriteLine("               Name: {0}", adaptOut.Description.Name);
            Console.WriteLine("   Attached to Desk: {0}", adaptOut.Description.IsAttachedToDesktop);
            Console.WriteLine("       Desktop Rect: {0}", adaptOut.Description.DesktopBounds);
            Console.WriteLine("           Rotation: {0}", adaptOut.Description.Rotation);


            ReadOnlyCollection<ModeDescription> modes = adaptOut.GetDisplayModeList(Format.B8G8R8A8_UNorm, DisplayModeEnumerationFlags.Scaling);
            //PrintDisplayModes(modes);
        }

        static void PrintDisplayModes(System.Collections.ObjectModel.ReadOnlyCollection<ModeDescription> modes)
        {
            if (null == modes)
                return;

            Console.WriteLine("== Modes ==");

            foreach (ModeDescription mode in modes)
            {
                Console.WriteLine("{0} Scaling: {1}", mode.ToString(), mode.Scaling);
            }
        }
    }
}
