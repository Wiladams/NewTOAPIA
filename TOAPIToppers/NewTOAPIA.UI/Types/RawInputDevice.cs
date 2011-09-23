using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;


namespace NewTOAPIA.UI
{
    using TOAPI.Types;
    using TOAPI.User32;

    using NewTOAPIA;

    public class RawInputDevice : Observer<MSG>
    {
        protected IntPtr fDeviceHandle;
        protected string fDeviceName;
        protected DeviceRegistryInfo fRegistryInfo;
        bool fIsPhysical;

        public RawInputDevice(IntPtr hDevice)
        {
            fDeviceHandle = hDevice;
            fDeviceName = RawInputDevice.GetRawInputDeviceName(fDeviceHandle);
            fRegistryInfo = new DeviceRegistryInfo(fDeviceName);
        }

        #region Properties
        public string ClassName
        {
            get { return fRegistryInfo.ClassName; }
        }

        public string Description
        {
            get { return fRegistryInfo.Description; }
        }

        public IntPtr DeviceHandle
        {
            get
            {
                return fDeviceHandle;
            }
        }

        public string DeviceName
        {
            get { return fDeviceName; }
        }

        public virtual bool IsPhysical
        {
            get { return fIsPhysical; }
            protected set { fIsPhysical = value; }
        }

        public DeviceRegistryInfo RegistryInfo
        {
            get { return fRegistryInfo; }
        }

        #endregion

        #region Static Helpers
        public static RAWINPUTDEVICELIST[] GetAllRawInputDevices()
        {
            uint deviceCount = 0;
            int dwSize = Marshal.SizeOf(typeof(RAWINPUTDEVICELIST));

            // First call the system routine with a null pointer
            // for the array to get the size needed for the list
            int retValue = (int)User32.GetRawInputDeviceList(IntPtr.Zero, ref deviceCount, (uint)dwSize);

            // If anything but zero is returned, the call failed, so return a null list
            if (0 != retValue)
                return null;


            // Now allocate an array of the specified number of entries
            RAWINPUTDEVICELIST[] deviceList = new RAWINPUTDEVICELIST[deviceCount];
            IntPtr pRawInputDeviceList = Marshal.AllocHGlobal((int)(dwSize * deviceCount));

            // Now make the call again, using the array
            User32.GetRawInputDeviceList(pRawInputDeviceList, ref deviceCount, (uint)dwSize);

            UnmanagedPointer rawPointer = new UnmanagedPointer(pRawInputDeviceList);

            for (int i = 0; i < deviceCount; i++)
            {

                deviceList[i] = (RAWINPUTDEVICELIST)Marshal.PtrToStructure((rawPointer + (dwSize * i)), typeof(RAWINPUTDEVICELIST));
            }

            // Free up the allocated memory
            Marshal.FreeHGlobal(pRawInputDeviceList);

            return deviceList;
        }

        public static RawInputDevice[] GetAllDevices()
        {
            RAWINPUTDEVICELIST[] deviceList = GetAllRawInputDevices();

            // Now that we have the basic raw info,
            // create instances of the right raw devices
            // based on the info
            RawInputDevice[] RawDeviceList = new RawInputDevice[deviceList.Length];

            for (int i = 0; i < deviceList.Length; i++)
            {
                switch (deviceList[i].dwType)
                {
                    case User32.RIM_TYPEMOUSE:     // MouseDevice
                        {
                            MouseDevice aMouse = new MouseDevice(deviceList[i].hDevice);
                            RawDeviceList[i] = aMouse;
                        }
                        break;

                    case User32.RIM_TYPEKEYBOARD:     // KeyboardDevice
                        {
                            KeyboardDevice aKeyboard = new KeyboardDevice(deviceList[i].hDevice);
                            RawDeviceList[i] = aKeyboard;
                        }
                        break;

                    case User32.RIM_TYPEHID:     // HID Device
                        {
                            HIDDevice aHIDDevice = new HIDDevice(deviceList[i].hDevice);
                            RawDeviceList[i] = aHIDDevice;
                        }
                        break;
                }
            }

            // Finally, return the filled in list
            return RawDeviceList;
        }

        public static RawInputDevice[] GetPhysicalDevices()
        {
            RAWINPUTDEVICELIST[] deviceList = GetAllRawInputDevices();
            if (null == deviceList)
                return null;

            // Now that we have the basic raw info,
            // create instances of the right raw devices
            // based on the info
            // Use a stack to store the ones that are actually physical
            Stack<RawInputDevice> deviceStack = new Stack<RawInputDevice>();

            for (int i = 0; i < deviceList.Length; i++)
            {
                switch (deviceList[i].dwType)
                {
                    case User32.RIM_TYPEMOUSE:     // MouseDevice
                        {
                            MouseDevice aMouse = new MouseDevice(deviceList[i].hDevice);
                            if (aMouse.IsPhysical)
                                deviceStack.Push(aMouse);
                        }
                        break;

                    case User32.RIM_TYPEKEYBOARD:     // KeyboardDevice
                        {
                            KeyboardDevice aKeyboard = new KeyboardDevice(deviceList[i].hDevice);
                            if (aKeyboard.IsPhysical)
                                deviceStack.Push(aKeyboard);
                        }
                        break;

                    case User32.RIM_TYPEHID:     // HID Device
                        {
                            HIDDevice aHIDDevice = new HIDDevice(deviceList[i].hDevice);
                            if (aHIDDevice.IsPhysical)
                                deviceStack.Push(aHIDDevice);
                        }
                        break;
                }
            }

            // Finally, return the filled in list
            return deviceStack.ToArray();
        }

        public static RawInputDevice[] GetRawDevices(int deviceType, bool physicalOnly)
        {
            RAWINPUTDEVICELIST[] deviceList = GetAllRawInputDevices();
            if (null == deviceList)
                return null;

            // Now that we have the basic raw info,
            // create instances of the right raw devices
            // based on the info
            // Use a collection to hold the ones that actually
            // meet the specified criteria.
            List<RawInputDevice> deviceStack = new List<RawInputDevice>();

            for (int i = 0; i < deviceList.Length; i++)
            {
                if (deviceList[i].dwType != deviceType)
                    continue;

                RawInputDevice aDevice = null;

                switch (deviceList[i].dwType)
                {
                    case User32.RIM_TYPEMOUSE:     // MouseDevice
                        {
                            aDevice = new MouseDevice(deviceList[i].hDevice);
                        }
                        break;

                    case User32.RIM_TYPEKEYBOARD:     // KeyboardDevice
                        {
                            aDevice = new KeyboardDevice(deviceList[i].hDevice);
                        }
                        break;

                    case User32.RIM_TYPEHID:     // HID Device
                        {
                            aDevice = new HIDDevice(deviceList[i].hDevice);
                        }
                        break;
                }

                if (!physicalOnly)
                {
                    deviceStack.Add(aDevice);
                }
                else if (aDevice.IsPhysical)
                    deviceStack.Add(aDevice);
            }

            // Finally, return the filled in list
            return deviceStack.ToArray();
        }

        public static string GetRawInputDeviceName(IntPtr hDevice)
        {
            uint pbSize = 0;
            User32.GetRawInputDeviceInfo(hDevice, User32.RIDI_DEVICENAME, IntPtr.Zero, ref pbSize);

            // Allocate some memory to hold the string data
            IntPtr pData = Marshal.AllocHGlobal((int)pbSize);

            // Make the call again to actually get the data
            User32.GetRawInputDeviceInfo(hDevice, User32.RIDI_DEVICENAME, pData, ref pbSize);

            // Turn it into a string to be returned
            string deviceName;
            deviceName = (string)Marshal.PtrToStringAnsi(pData);

            // Free up the allocated memory
            Marshal.FreeHGlobal(pData);

            return deviceName;
        }

        public static RID_DEVICE_INFO GetRawInputDeviceInfo(IntPtr hDevice)
        {
            uint pbSize = 0;

            // Figure out how much space needs to be allocated first
            User32.GetRawInputDeviceInfo(hDevice, User32.RIDI_DEVICEINFO, IntPtr.Zero, ref pbSize);

            // Allocate some memory to hold the data
            IntPtr pData = Marshal.AllocHGlobal((int)pbSize);

            // Make the call again to actually get the data
            User32.GetRawInputDeviceInfo(hDevice, User32.RIDI_DEVICEINFO, pData, ref pbSize);

            // Turn it into the data to be returned
            RID_DEVICE_INFO deviceInfo = new RID_DEVICE_INFO();
            deviceInfo.Init();
            Marshal.PtrToStructure(pData, deviceInfo);

            // Free up the allocated memory
            Marshal.FreeHGlobal(pData);

            return deviceInfo;
        }
#endregion
    }
}
