using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.User32;

namespace NewTOAPIA.UI
{
    public delegate IntPtr KeyboardActivityEventHandler(Object sender, KeyboardActivityArgs ke);

    public class KeyboardDevice : RawInputDevice
    {
        public event KeyboardActivityEventHandler KeyboardActivityEvent;

        uint fKeyboardMode;
        int fNumFunctionKeys;
        uint fNumTotalKeys;
        uint fNumIndicators;
        int fSubType;
        TypeOfKeyboard fType;
        bool fIsPhysical;

        RAWINPUTDEVICE fRIDRegister;
        User32MessageSource fMessageLooper;

        // By constructing one of these objects, we can persist
        // keyboard information
        public KeyboardDevice(IntPtr hDevice)
            : base(hDevice)
        {
            // Get detailed information on keyboard
            RID_DEVICE_INFO deviceInfo = RawInputDevice.GetRawInputDeviceInfo(DeviceHandle);

            fKeyboardMode = deviceInfo.Union1.keyboard.dwKeyboardMode;
            fNumFunctionKeys = (int)deviceInfo.Union1.keyboard.dwNumberOfFunctionKeys;
            fNumIndicators = deviceInfo.Union1.keyboard.dwNumberOfIndicators;
            fNumTotalKeys = deviceInfo.Union1.keyboard.dwNumberOfKeysTotal;
            fSubType = (int)deviceInfo.Union1.keyboard.dwSubType;
            fType = (TypeOfKeyboard)deviceInfo.Union1.keyboard.dwType;

            fRIDRegister = new RAWINPUTDEVICE();

            fIsPhysical = ClassName.ToUpper() == "KEYBOARD";

        }

        public override bool IsPhysical
        {
            get { return fIsPhysical; }
        }

        public bool Run()
        {
            fMessageLooper = new User32MessageSource();

            // Attach to the message Looper for input
            // Hookup to receive any messages from the message looper
            fMessageLooper.Subscribe(this);

            fMessageLooper.Start();

            // Register as a raw input device that wants 
            // to receive WM_INPUT messages
            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x06; // KeyboardDevice
            fRIDRegister.dwFlags = User32.RIDEV_INPUTSINK;
            fRIDRegister.hwndTarget = fMessageLooper.Handle;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);

            return isRegistered;
        }

        public bool Quit()
        {
            fRIDRegister.usUsagePage = 0x01;
            fRIDRegister.usUsage = 0x06; // KeyboardDevice
            fRIDRegister.dwFlags = User32.RIDEV_REMOVE;
            fRIDRegister.hwndTarget = fMessageLooper.Handle;

            bool isRegistered = User32.RegisterRawInputDevice(fRIDRegister);
            fMessageLooper.Quit();

            return isRegistered;
        }

        public virtual IntPtr Callback(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr retValue = IntPtr.Zero;

            //Console.WriteLine("KeyboardDevice.Callback: Msg = {0} - {1}", msg, MessageDecoder.MsgToString(msg));

            switch (msg)
            {
                case (int)WinMsg.WM_INPUT:
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
                            //RAWINPUT raw = (RAWINPUT)Marshal.PtrToStructure(buffer, typeof(RAWINPUT));
                            //// We should have keyboard data now
                            ////Console.WriteLine("Keyboard Data: VKey: {0}[{1}],  Message: {2}",
                            ////    raw.data.keyboard.VKey, raw.data.keyboard.VKey.ToString(),
                            ////    MessageDecoder.MsgToString(raw.data.keyboard.Message));
                            //if (null != KeyboardActivityEvent)
                            //{
                            //    KeyboardActivityArgs kevent = new KeyboardActivityArgs(this, (KeyActivityType)raw.data.keyboard.Message, (VirtualKeyCodes)raw.data.keyboard.VKey, 0, '\0');
                            //    KeyboardActivityEvent(this, kevent);
                            //}
                        }
                    }
                    break;

                default:
                    retValue = User32.DefWindowProc(hWnd, msg, wParam, lParam);

                    break;
            }

            return retValue;
        }


        // KeyboardDevice information
        /// <summary>
        /// The type may be one of the following values
        /// 1 - IBM PC/XT or compatible (83-key) keyboard
        /// 2 - Olivetti "ICO" (102-key) keyboard
        /// 3 - IBM PC/AT (84-key) or similar keyboard
        /// 4 - IBM enhanced (101- or 102-key) keyboard
        /// 5 - Nokia 1050 and similar keyboards
        /// 6 - Nokia 9140 and similar keyboards
        /// 7 - Japanese keyboard
        /// </summary>
        public TypeOfKeyboard KeyboardType
        {
            get { return fType; }
        }

        /// <summary>
        /// This is an OEM specific value
        /// </summary>
        public int Subtype
        {
            get { return fSubType; }
        }

        /// <summary>
        /// Returns the number of function keys that are available on the keyboard.
        /// </summary>
        public int NumberOfFunctionKeys
        {
            get { return fNumFunctionKeys; }
        }

        /// <summary>
        /// CurrentKeyState
        /// Returns the current physical state of a virtual key, 
        /// independent of any message processing that is currently
        /// occuring. 
        /// </summary>
        /// <param name="vKey">one of the virtual key codes</param>
        /// <returns>short</returns>
        public static short CurrentKeyState(int vKey)
        {
            return 0;
            //return User32.GetAsyncKeyState(vKey);
        }

        public static short KeyState(int vKey)
        {
            return 0;
            //return User32.GetKeyState(vKey);
        }


        public override string ToString()
        {
            return "<keyboard type='" + fType.ToString() +
                "' subtype='" + fSubType.ToString() +
                "' numfunckeys = '" + NumberOfFunctionKeys.ToString() +
                "' />";
        }

        public static KeyboardDevice GetFirstPhysicalKeyboard()
        {
            RawInputDevice[] physicalDevices = RawInputDevice.GetPhysicalDevices();
            foreach (RawInputDevice rid in physicalDevices)
            {
                if (rid.ClassName.ToUpper() == "KEYBOARD")
                    return (KeyboardDevice)rid;
            }

            return null;
        }

        public static KeyboardDevice[] GetPhysicalKeyboards()
        {
            List<KeyboardDevice> keyboards = new List<KeyboardDevice>();

            RawInputDevice[] physicalDevices = RawInputDevice.GetPhysicalDevices();
            foreach (RawInputDevice rid in physicalDevices)
            {
                if (rid.ClassName.ToUpper() == "KEYBOARD")
                    keyboards.Add((KeyboardDevice)rid);
            }

            return keyboards.ToArray();
        }
    }
}
