
namespace JoyTest
{
    using System;
    
    using TOAPI.WinMM;
    using TOAPI.User32;

    using NewTOAPIA;
    using NewTOAPIA.UI;
    using NewTOAPIA.Kernel;

    public class JoyWindow : Window
    {
        Joystick fStick;
        TimedDispatcher dispatcher;

        public JoyWindow(int width, int height)
            : base("Joystick Test", 10, 10, 640, 480)
        {
            CheckRawInputDevices();

            fStick = new Joystick(winmm.JOYSTICKID1);
            //fStick.Offset = -0.5f;

            PrintJoystickReport(fStick);
            
            dispatcher = new TimedDispatcher(1.0 / 5, OnDispatch, null);
            dispatcher.Start();
        }


        void CheckRawInputDevices()
        {
            RawInputDevice[] devices = RawInputDevice.GetRawDevices(User32.RIM_TYPEHID, true) ;

            foreach (RawInputDevice rid in devices)
            {
                Console.WriteLine("Class Name: {0}", rid.ClassName);
                Console.WriteLine("Description: {0}", rid.Description);
                Console.WriteLine("GUID: {0}", rid.RegistryInfo.ClassGUID);

            }
        }

        public void OnDispatch(TimedDispatcher dispatcher, double time, object[] dispatchParams)
        {
            JoystickActivityArgs currentState = fStick.GetCurrentState();
            PrintStickState(currentState);
        }

        void PrintJoystickReport(Joystick aStick)
        {
            Console.WriteLine("---- ---- BEGIN ---- ----");
            Console.WriteLine("Product: {0}", aStick.ProductName);
            Console.WriteLine("Joystick ID: {0}", aStick.ID);
            Console.WriteLine("Axes Available: {0}", aStick.AxesAvailable);
            Console.WriteLine("Axes In Use: {0}", aStick.AxesInUse);
            Console.WriteLine("Buttons Available: {0}", aStick.ButtonsAvailable);
            Console.WriteLine("Buttons In Use: {0}", aStick.ButtonsInUse);
            Console.WriteLine("---- Axes Capabilities ----");
            Console.WriteLine("Has Z Axis: {0}", aStick.HasZAxisInformation);
            Console.WriteLine("Has R Axis: {0}", aStick.HasRAxisInformation);
            Console.WriteLine("Has U Axis: {0}", aStick.HasUAxisInformation);
            Console.WriteLine("Has V Axis: {0}", aStick.HasVAxisInformation);
            Console.WriteLine("---- POV Capabilities ----");
            Console.WriteLine("Has POV: {0}", aStick.HasPOVInformation);
            Console.WriteLine("POV Discrete: {0}", aStick.HasDiscretePOV);
            Console.WriteLine("POV Continuous: {0}", aStick.HasContinuousPOV);
            Console.WriteLine("---- ---- END ---- ----");
        }

        void PrintStickState(JoystickActivityArgs aState)
        {
            Console.WriteLine("X: {0}  Y: {1}  Z: {2}", aState.X, aState.Y, aState.Z);
            Console.WriteLine("R: {0}", aState.R);
            Console.WriteLine("POV: {0}", aState.POV);
            Console.WriteLine("A Button Is Pressed: {0}", aState.AButtonIsPressed);
            Console.WriteLine("Button 1: {0}", aState.IsButtonPressed(1));
            Console.WriteLine("Button 2: {0}", aState.IsButtonPressed(2));
            Console.WriteLine("Button 3: {0}", aState.IsButtonPressed(3));
            Console.WriteLine("Button 4: {0}", aState.IsButtonPressed(4));

        }        

    }
}
