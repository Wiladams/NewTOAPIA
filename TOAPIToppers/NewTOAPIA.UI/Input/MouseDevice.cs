using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI
{
    /// <summary>
    /// The MouseDevice class represents any device, physical or virtual,
    /// that is 'connected' to the system, and generally has mouse behaviors.
    /// </summary>
    public class MouseDevice : RawInputDevice
    {
        // Properties of the device
        int fMouseID;
        int fNumButtons;
        int fSampleRate;


        RAWINPUTDEVICE fRIDRegister;

        public event MouseActivityEventHandler MouseActivityEvent;
        User32MessageSource fMessageLooper;
        IntPtr fAttachedWindow;

        #region Constructor
        /// <summary>
        /// Constructor taking a device handle.  From this device handle
        /// very specific device information can be found.
        /// </summary>
        public MouseDevice(IntPtr hDevice)
            : base(hDevice)
        {
            // Now get more detailed information
            RID_DEVICE_INFO deviceInfo = RawInputDevice.GetRawInputDeviceInfo(fDeviceHandle);
            fMouseID = (int)deviceInfo.Union1.mouse.dwId;
            fNumButtons = (int)deviceInfo.Union1.mouse.dwNumberOfButtons;
            fSampleRate = (int)deviceInfo.Union1.mouse.dwSampleRate;

            fRIDRegister = new RAWINPUTDEVICE();


            IsPhysical = ClassName.ToUpper() == "MOUSE";

        }
        #endregion

        #region Properties
        public int NumberOfButtons
        {
            get { return fNumButtons; }
        }

        public int SampleRate
        {
            get { return fSampleRate; }
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
            int msg = aMSG.message;
            IntPtr retValue = IntPtr.Zero;

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
                                MouseActivityArgs mactivity = MouseActivityArgs.CreateFromRawInput(this, raw.data.mouse);

                                // Send the message to all those who are interested in receiving it.
                                MouseActivityEvent(this, mactivity);
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
        }
        #endregion

        #region Static Helper Routines
        public static MouseDevice GetFirstPhysicalMouse()
        {
            RawInputDevice[] physicalDevices = RawInputDevice.GetPhysicalDevices();
            foreach (RawInputDevice rid in physicalDevices)
            {
                if (rid.ClassName.ToUpper() == "MOUSE")
                    return (MouseDevice)rid;
            }

            return null;
        }

        public static MouseDevice[] GetPhysicalMice()
        {
            List<MouseDevice> mice = new List<MouseDevice>();

            RawInputDevice[] physicalDevices = RawInputDevice.GetPhysicalDevices();
            foreach (RawInputDevice rid in physicalDevices)
            {
                if (rid.ClassName.ToUpper() == "MOUSE")
                    mice.Add((MouseDevice)rid);
            }

            return mice.ToArray();
        }

        #endregion

        public override string ToString()
        {
            string aString = string.Format("<MOUSE id='{0}', buttons='{1}', samplerate='{2}'/>",
                fMouseID, fNumButtons, fSampleRate);

            return aString;
        }
    }
}
