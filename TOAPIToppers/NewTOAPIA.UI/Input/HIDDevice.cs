using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI
{

    /// <summary>
    /// The HIDDevice class represents any device, physical or virtual,
    /// that is 'connected' to the system, and generally has joystick behaviors.
    /// </summary>
    public class HIDDevice : RawInputDevice
    {
        public event MouseActivityEventHandler MouseActivityEvent;

        User32MessageSource fMessageLooper;

        uint dwProductId;
        uint dwVendorId;
        uint dwVersionNumber;
        ushort usUsage;
        ushort usUsagePage;


        RAWINPUTDEVICE fRIDRegister;
        IntPtr fAttachedWindow;

        #region Constructor
        /// <summary>
        /// Constructor taking a device handle.  From this device handle
        /// very specific device information can be found.
        /// </summary>
        public HIDDevice(IntPtr hDevice)
            : base(hDevice)
        {
            // Now get more detailed information
            RID_DEVICE_INFO deviceInfo = RawInputDevice.GetRawInputDeviceInfo(fDeviceHandle);

            dwProductId = deviceInfo.Union1.hid.dwProductId;
            dwVendorId = deviceInfo.Union1.hid.dwVendorId;
            dwVersionNumber = deviceInfo.Union1.hid.dwVersionNumber;
            usUsage = deviceInfo.Union1.hid.usUsage;
            usUsagePage = deviceInfo.Union1.hid.usUsagePage;


            fRIDRegister = new RAWINPUTDEVICE();


            IsPhysical = ClassName.ToUpper() == "HIDCLASS";

        }
        #endregion

        #region Properties
        public uint VendorID
        {
            get { return dwVendorId; }
        }

        public uint ProductID
        {
            get { return dwProductId; }
        }

        public uint VersionNumber
        {
            get { return dwVersionNumber; }
        }

        public ushort Usage
        {
            get { return usUsage; }
        }

        public ushort UsagePage
        {
            get { return usUsagePage; }
        }
        #endregion

        #region Window Attachment
        public bool AttachToWindow(IntPtr windowHandle)
        {
            if (IntPtr.Zero != fAttachedWindow)
                DetachFromWindow();

            // Register as a raw input device that wants 
            // to receive WM_INPUT messages
            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x02; // MouseDevice
            fRIDRegister.dwFlags = User32.RIDEV_INPUTSINK;
            fRIDRegister.hwndTarget = windowHandle;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);

            // If we successfully attached to the window, set it as
            // our current window handle.
            if (isRegistered)
                fAttachedWindow = windowHandle;

            return isRegistered;
        }

        public bool DetachFromWindow()
        {
            // if we're not currently attached, just return immediately
            if (IntPtr.Zero == fAttachedWindow)
                return false;

            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x02; // MouseDevice
            fRIDRegister.dwFlags = User32.RIDEV_REMOVE;
            fRIDRegister.hwndTarget = fAttachedWindow;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);

            fAttachedWindow = IntPtr.Zero;

            return isRegistered;
        }
        #endregion

        #region Running a Message Loop
        public bool Start()
        {
            fMessageLooper = new User32MessageSource();

            // Hookup to receive any messages from the message looper
            fMessageLooper.Subscribe(this);

            fMessageLooper.Start();


            // Attach to the message Looper for input
            bool isRegistered = AttachToWindow(fMessageLooper.Handle);

            return isRegistered;
        }

        public bool Stop()
        {
            bool isRegistered = DetachFromWindow();

            fMessageLooper.Quit();

            return isRegistered;
        }

        public override void OnNext(MSG aMSG)
        {
            IntPtr retValue = IntPtr.Zero;
            int msg = aMSG.message;

            //Console.WriteLine("MouseDevice.Callback: ID = {0} Msg = {1} - {2}", this.fMouseID, msg, MessageDecoder.MsgToString(msg));
            switch (msg)
            {
                case (int)WinMsg.WM_INPUT:
                    {
                        uint dwSize = 0;

                        // First find out how much space is needed to store the data that will
                        // be retrieved.
                        User32.GetRawInputData(aMSG.lParam, User32.RID_INPUT, IntPtr.Zero, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

                        // Now allocate the specified number of bytes
                        IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);

                        // Now get the data for real, and check the size to see
                        // if we got what we thought we should get.
                        if (User32.GetRawInputData(aMSG.lParam, User32.RID_INPUT, buffer, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                        {
                            RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                            // At this point, we should have raw mouse data
                            if (null != MouseActivityEvent)
                            {
                                //MouseActivityArgs mactivity = MouseActivityArgs.CreateFromRawInput(this, raw.data.mouse);

                                //// Send the message to all those who are interested in receiving it.
                                //MouseActivityEvent(this, mactivity);
                            }

                            Marshal.FreeHGlobal(buffer);
                        }
                    }
                    break;

                default:
                    retValue = User32.DefWindowProc(aMSG.hWnd, msg, aMSG.wParam, aMSG.lParam);
                    //retValue = User32.DefRawInputProc(RAWHID, nInput, sizeof);
                    break;

            }

            //return retValue;
        }
        #endregion

        public override string ToString()
        {
            string aString = string.Format("<HID id='{0}'/>",
                dwVendorId);

            return aString;
        }
    }
}
