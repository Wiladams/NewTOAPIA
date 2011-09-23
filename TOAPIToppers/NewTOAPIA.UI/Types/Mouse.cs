using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI
{
    public delegate void MouseActivityEventHandler(Object sender, MouseEventArgs me);

    /// <summary>
    /// The MouseDevice class represents any device, physical or virtual,
    /// that is 'connected' to the system, and generally has mouse behaviors.
    /// </summary>
    public class MouseDevice : RawInputDevice
    {
        public event MouseActivityEventHandler MouseActivityEvent;

        MSGLooper fMessageLooper;

        uint fMouseID;
        uint fNumButtons;
        uint fSampleRate;
        bool fIsPhysical;
        RAWINPUTDEVICE fRIDRegister;

        /// <summary>
        /// Constructor taking a device handle.  From this device handle
        /// very specific device information can be found.
        /// </summary>
        public MouseDevice(IntPtr hDevice)
            : base(hDevice)
        {            
            // Now get more detailed information
            RID_DEVICE_INFO deviceInfo = RawInputDevice.GetRawInputDeviceInfo(fDeviceHandle);
            fMouseID = deviceInfo.Union1.mouse.dwId;
            fNumButtons = deviceInfo.Union1.mouse.dwNumberOfButtons;
            fSampleRate = deviceInfo.Union1.mouse.dwSampleRate;


            
            fRIDRegister = new RAWINPUTDEVICE();


            fIsPhysical = ClassName.ToUpper() == "MOUSE";
        }

        public override bool IsPhysical
        {
            get { return fIsPhysical; }
        }

        public bool Run()
        {
            fMessageLooper = new MSGLooper();
            fMessageLooper.Start();

            // Attach to the message Looper for input
            // Hookup to receive any messages from the message looper
            fMessageLooper.MessageReceived += new MessageProc(this.Callback);

            // Register as a raw input device that wants 
            // to receive WM_INPUT messages
            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x02; // MouseDevice
            fRIDRegister.dwFlags = User32.RIDEV_INPUTSINK;
            fRIDRegister.hwndTarget = IntPtr.Zero;
            fRIDRegister.hwndTarget = fMessageLooper.Handle;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);

            return isRegistered;
        }

        public bool Quit()
        {
            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x02; // MouseDevice
            fRIDRegister.dwFlags = User32.RIDEV_REMOVE;
            fRIDRegister.hwndTarget = fMessageLooper.Handle;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);
            fMessageLooper.Quit();

            return isRegistered;
        }

        public virtual int Callback(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            int retValue = 0;

            Console.WriteLine("Mouse.Callback: ID = {0} Msg = {1} - {2}", this.fMouseID, msg, MessageDecoder.MsgToString(msg));
            switch (msg)
            {
                case MSG.WM_INPUT:
                    {
                        uint dwSize = 0;

                        // First find out how much space is needed to store the data that will
                        // be retrieved.
                        User32.GetRawInputData(lParam, User32.RID_INPUT, IntPtr.Zero, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER)));

                        // Now allocate the specified number of bytes
                        IntPtr buffer = Marshal.AllocHGlobal((int)dwSize);

                        // Now get the data for real, and check the size to see
                        // if we got what we thought we should get.
                        if (User32.GetRawInputData(lParam, User32.RID_INPUT, buffer, ref dwSize,
                            (uint)Marshal.SizeOf(typeof(RAWINPUTHEADER))) == dwSize)
                        {
                            RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));

                            // We should have mouse data now
                            if (null != MouseActivityEvent)
                            {
                                MouseEventArgs mevent = new MouseEventArgs(this, (short)MouseEventType.MouseMove, (short)raw.data.mouse.ulRawButtons);
                                MouseActivityEvent(this, mevent);
                            }
                        }
                    }
                    break;
                default:
                    retValue = User32.DefWindowProc(hWnd, msg, wParam, lParam);

                    break;

            }

            return retValue;
        }

        public uint NumberOfButtons
        {
            get { return fNumButtons; }
        }

        public uint SampleRate
        {
            get { return fSampleRate; }
        }

        public override string ToString()
        {
            string aString = string.Format("<MOUSE id='{0}', buttons='{1}', samplerate='{2}'/>",
                fMouseID, fNumButtons, fSampleRate);
            
            return aString;
        }

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

    }

}