using System;

using NewTOAPIA.DirectShow;
using NewTOAPIA.Graphics;
using NewTOAPIA.Kernel;
using NewTOAPIA.Media;
using NewTOAPIA.Media.Capture;
using NewTOAPIA.UI;

using TOAPI.WinMM;

namespace GDIVideo
{
    using NewTOAPIA.Drawing.GDI;

    public partial class Form1 : Window
    {
        Joystick fStick;
        TimedDispatcher dispatcher;

        VideoCaptureDevice m_CaptureDevice;
        CameraControl m_CamControl;
        bool fUseScaling;

        public Form1()
            :base("GDI Video", 10, 10, 640, 480)
        {
            // Show a dialog box on the screen so the user
            // can select which camera to use if they have
            // multiple attached.
            CaptureDeviceDescription capDescription = new CaptureDeviceDescription();
            CameraSelection camForm = new CameraSelection();
            camForm.ShowDialog();

            //// Get the chosen configuration from the dialog 
            //// and use it to create a capture device.
            object config = camForm.SetupPage.GetConfiguration();
            //m_CaptureDevice = (VideoCaptureDevice)capDescription.CreateVideoSource(config);

            // Another way to get a hold of a capture device
            m_CaptureDevice = VideoCaptureDevice.CreateCaptureDeviceFromIndex(0, 320, 240);
            m_CamControl = new CameraControl(m_CaptureDevice);

            //Console.WriteLine("Capabilities: {0}", m_CaptureDevice.Capabilities.Count);
            // Let the capture device know what function to call
            // whenever a frame is received.
            m_CaptureDevice.NewFrame += OnFrameReceived;

            // Start the capture device on its own thread
            m_CaptureDevice.Start();

            fStick = new Joystick(winmm.JOYSTICKID1);
            dispatcher = new TimedDispatcher(1.0 / 2, OnJoystickDispatch, null);
            dispatcher.Start();

        }

        public void OnJoystickDispatch(TimedDispatcher dispatcher, double time, object[] dispatchParams)
        {
            JoystickActivityArgs currentState = fStick.GetCurrentState();
            
            m_CamControl.PanToAbsolute((float)currentState.R);
        }

        public override void OnMouseWheel(MouseActivityArgs e)
        {
            int min = int.MinValue;
            int max = int.MaxValue;
            int step = 0;
            int defaultValue = 0;
            int currentValue = 0;
            CameraControlFlags camFlags = CameraControlFlags.None;
            IAMCameraControl camControl = m_CaptureDevice.GetCameraControl();

            // Delta > 0 ==> Zoom In (rolling towards the screen)
            // Delta < 0 ==> Zoom Out (rolling away from the screen)
            if (e.Delta > 0)
            {
                camControl.GetRange(CameraControlProperty.Zoom, out min, out max, out step, out defaultValue, out camFlags);
                camControl.Get(CameraControlProperty.Zoom, out currentValue, out camFlags);
                if (currentValue < max)
                    currentValue += step;
                camControl.Set(CameraControlProperty.Zoom, currentValue, camFlags);

                Console.WriteLine("Zoom: {0}", currentValue);
            }
            else
            {
                camControl.GetRange(CameraControlProperty.Zoom, out min, out max, out step, out defaultValue, out camFlags);
                camControl.Get(CameraControlProperty.Zoom, out currentValue, out camFlags);
                if (currentValue > min)
                    currentValue -= step;
                camControl.Set(CameraControlProperty.Zoom, currentValue, camFlags);

                Console.WriteLine("Zoom: {0}", currentValue);
            }
        }

        public override void OnKeyDown(KeyboardActivityArgs ke)
        {
            int min = int.MinValue;
            int max = int.MaxValue;
            int step = 0;
            int defaultValue = 0;
            int currentValue = 0;
            CameraControlFlags camFlags = CameraControlFlags.None;

            IAMCameraControl camControl = m_CaptureDevice.GetCameraControl();

            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.Up:
                    {

                        if (ke.Shift)
                        {
                            camControl.GetRange(CameraControlProperty.Zoom, out min, out max, out step, out defaultValue, out camFlags);
                            camControl.Get(CameraControlProperty.Zoom, out currentValue, out camFlags);
                            if (currentValue < max)
                                currentValue += step;
                            camControl.Set(CameraControlProperty.Zoom, currentValue, camFlags);

                            Console.WriteLine("Zoom: {0}", currentValue);
                        }
                        else
                        {
                            camControl.GetRange(CameraControlProperty.Tilt, out min, out max, out step, out defaultValue, out camFlags);
                            camControl.Get(CameraControlProperty.Tilt, out currentValue, out camFlags);
                            if (currentValue > min)
                                currentValue -= step;
                            camControl.Set(CameraControlProperty.Tilt, currentValue, camFlags);

                            Console.WriteLine("Up: {0}", currentValue);
                        }
                    }
                    break;

                case VirtualKeyCodes.Down:
                    if (ke.Shift)
                    {
                        camControl.GetRange(CameraControlProperty.Zoom, out min, out max, out step, out defaultValue, out camFlags);
                        camControl.Get(CameraControlProperty.Zoom, out currentValue, out camFlags);
                        if (currentValue > min)
                            currentValue -= step;
                        camControl.Set(CameraControlProperty.Zoom, currentValue, camFlags);

                        Console.WriteLine("Zoom: {0}", currentValue);
                    }
                    else
                    {
                        camControl.GetRange(CameraControlProperty.Tilt, out min, out max, out step, out defaultValue, out camFlags);
                        camControl.Get(CameraControlProperty.Tilt, out currentValue, out camFlags);
                        if (currentValue < max)
                            currentValue += step;
                        camControl.Set(CameraControlProperty.Tilt, currentValue, camFlags);
                        Console.WriteLine("Down: {0}", currentValue);
                    }
                    break;

                case VirtualKeyCodes.Right:
                    camControl.GetRange(CameraControlProperty.Pan, out min, out max, out step, out defaultValue, out camFlags);
                    camControl.Get(CameraControlProperty.Pan, out currentValue, out camFlags);
                    if (currentValue < max)
                        currentValue += step;
                    camControl.Set(CameraControlProperty.Pan, currentValue, camFlags);
                    break;

                case VirtualKeyCodes.Left:
                    camControl.GetRange(CameraControlProperty.Pan, out min, out max, out step, out defaultValue, out camFlags);
                    camControl.Get(CameraControlProperty.Pan, out currentValue, out camFlags);
                    if (currentValue > min)
                        currentValue -= step;
                    camControl.Set(CameraControlProperty.Pan, currentValue, camFlags);
                    break;

                case VirtualKeyCodes.PageUp:
                    camControl.GetRange(CameraControlProperty.Focus, out min, out max, out step, out defaultValue, out camFlags);
                    camControl.Get(CameraControlProperty.Focus, out currentValue, out camFlags);
                    if (currentValue > min)
                        currentValue -= step;
                    camControl.Set(CameraControlProperty.Focus, currentValue, CameraControlFlags.Manual);
                    break;

                case VirtualKeyCodes.PageDown:
                    camControl.GetRange(CameraControlProperty.Focus, out min, out max, out step, out defaultValue, out camFlags);
                    camControl.Get(CameraControlProperty.Focus, out currentValue, out camFlags);
                    if (currentValue < min)
                        currentValue += step;
                    camControl.Set(CameraControlProperty.Focus, currentValue, CameraControlFlags.Manual);
                    break;

            }

            //return IntPtr.Zero;
        }

        public override void OnKeyUp(KeyboardActivityArgs ke)
        {
            //int min = int.MinValue;
            //int max = int.MaxValue;
            //int step = 0;
            //int defaultValue = 0;
            //int currentValue = 0;
            //CameraControlFlags camFlags = CameraControlFlags.None;
            IAMCameraControl camControl = m_CaptureDevice.GetCameraControl();

            switch (ke.VirtualKeyCode)
            {
                case VirtualKeyCodes.S:
                    fUseScaling = !fUseScaling;
                    break;

                case VirtualKeyCodes.R:
                    ResetCamera();
                    break;

                case VirtualKeyCodes.Home:
                    m_CamControl.PanToAbsolute(0.0f);
                    break;

                case VirtualKeyCodes.End:
                    m_CamControl.PanToAbsolute(1.0f);
                    break;
            }

            //return IntPtr.Zero;
        }

        void ResetCamera()
        {
            int min = int.MinValue;
            int max = int.MaxValue;
            int step = 0;
            int defaultValue = 0;
            CameraControlFlags camFlags = CameraControlFlags.None;

            IAMCameraControl camControl = m_CaptureDevice.GetCameraControl();

            camControl.GetRange(CameraControlProperty.Pan, out min, out max, out step, out defaultValue, out camFlags);
            camControl.Set(CameraControlProperty.Pan, defaultValue, CameraControlFlags.Manual);

            camControl.GetRange(CameraControlProperty.Roll, out min, out max, out step, out defaultValue, out camFlags);
            camControl.Set(CameraControlProperty.Roll, defaultValue, CameraControlFlags.Manual);

            camControl.GetRange(CameraControlProperty.Tilt, out min, out max, out step, out defaultValue, out camFlags);
            camControl.Set(CameraControlProperty.Tilt, defaultValue, CameraControlFlags.Manual);

            camControl.GetRange(CameraControlProperty.Zoom, out min, out max, out step, out defaultValue, out camFlags);
            camControl.Set(CameraControlProperty.Zoom, defaultValue, CameraControlFlags.Manual);
        }

        /// <summary>
        /// Whenever a frame is received from the camera, this routine is called.
        /// </summary>
        /// <param name="sender">the object that hosts the callback routine for the received frame.</param>
        /// <param name="camEvent">Event containing video frame information</param>
        void OnFrameReceived(object sender, CameraEventArgs camEvent)
        {
            RectangleI srcRect = new RectangleI(0, 0, camEvent.Width, camEvent.Height);
            RectangleI dstRect = srcRect;


            if (fUseScaling)
            {
                dstRect = this.ClientRectangle;
            }

            DeviceContextClientArea.PixelBlt(srcRect, dstRect, camEvent.fData, BitCount.Bits24);
        }

        /// <summary>
        /// When the window is destroyed, we want to make sure the stop the camera.
        /// </summary>
        public override void OnDestroyed()
        {
            m_CaptureDevice.SignalToStop();

            // This must be called or the application won't terminate
            // after the window is closed.
            base.OnDestroyed();
        }
    }
}
