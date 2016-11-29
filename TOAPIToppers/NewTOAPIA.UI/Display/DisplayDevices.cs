using System;
using System.Collections.Generic;

using TOAPI.User32;

namespace NewTOAPIA.UI
{

    public class DisplayDevice
    {
        DISPLAY_DEVICE fDevice;

        public DisplayDevice()
        {
            fDevice = new DISPLAY_DEVICE();
            fDevice.Init();
        }


        public bool IsAttachedToDesktop { get { return (bool)((fDevice.StateFlags & User32.DISPLAY_DEVICE_ATTACHED_TO_DESKTOP) > 0); } }
        public bool IsMultiDriver { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_MULTI_DRIVER) > 0); } }
        public bool IsPrimaryDevice { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_PRIMARY_DEVICE) > 0); } }
        public bool IsMirroringDevice { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_MIRRORING_DRIVER) > 0); } }
        public bool IsVGACompatible { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_VGA_COMPATIBLE) > 0); } }
        public bool IsRemovable { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_REMOVABLE) > 0); } }
        public bool IsModesPruned { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_MODESPRUNED) > 0); } }
        public bool IsRemote { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_REMOTE) > 0); } }
        public bool IsDisconnected { get { return ((fDevice.StateFlags & User32.DISPLAY_DEVICE_DISCONNECT) > 0); } }


        // Child device state
        //public const int
        //DISPLAY_DEVICE_ACTIVE = 0x00000001,
        //DISPLAY_DEVICE_ATTACHED = 0x00000002;
    }

	/// <summary>
	/// The DisplayDevices class encapsulates all the display devices that
	/// are in the system, giving ready access to the information.
	/// </summary>
	public class DisplayDevices
	{
		List<DISPLAY_DEVICE> fDevices;

		public DisplayDevices()
		{
			fDevices = new List<DISPLAY_DEVICE>(10);

			DISPLAY_DEVICE dd = new DISPLAY_DEVICE();

			try
			{
				for (uint id = 0; User32.EnumDisplayDevices(null, id, ref dd, 0) != 0; id++)
				{
					fDevices.Add(dd);

					// Create a new device each time around the loop
					dd = new DISPLAY_DEVICE();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(String.Format("{0}", ex.ToString()));
			}
		}

		public List<DISPLAY_DEVICE> Devices
		{
			get { return fDevices; }
		}
	}
}