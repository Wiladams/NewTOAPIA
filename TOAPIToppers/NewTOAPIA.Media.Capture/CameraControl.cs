using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media.Capture
{
    using NewTOAPIA.DirectShow;
    using NewTOAPIA.DirectShow.Core;
    using NewTOAPIA.DirectShow.DES;

    struct CamControlRange
    {
        public int Min;
        public int Max;
        public int Stepping;
        public int Default;
    }

    public class CameraControl
    {
        IAMCameraControl m_CameraControlInterface;
        IBaseFilter m_BaseFilter;
        CamControlRange m_PanRange;
        CamControlRange m_TiltRange;
        CamControlRange m_RollRange;
        CamControlRange m_ZoomRange;

        public CameraControl(VideoCaptureDevice vidDevice)
        {
            m_BaseFilter = vidDevice.BaseFilter;
            m_CameraControlInterface = (IAMCameraControl)m_BaseFilter;
            
            int valMin = 0;
            int valMax = 0;
            int valDefault = 0;
            int valStepping = 0;
            CameraControlFlags valFlags = CameraControlFlags.None;

            Controller.GetRange(CameraControlProperty.Pan, out valMin, out valMax, out valStepping, out valDefault, out valFlags);
            m_PanRange = new CamControlRange{Min = valMin, Max = valMax, Stepping = valStepping, Default = valDefault};

            Controller.GetRange(CameraControlProperty.Tilt, out valMin, out valMax, out valStepping, out valDefault, out valFlags);
            m_TiltRange = new CamControlRange { Min = valMin, Max = valMax, Stepping = valStepping, Default = valDefault };

            Controller.GetRange(CameraControlProperty.Roll, out valMin, out valMax, out valStepping, out valDefault, out valFlags);
            m_RollRange = new CamControlRange { Min = valMin, Max = valMax, Stepping = valStepping, Default = valDefault };

            Controller.GetRange(CameraControlProperty.Zoom, out valMin, out valMax, out valStepping, out valDefault, out valFlags);
            m_ZoomRange = new CamControlRange { Min = valMin, Max = valMax, Stepping = valStepping, Default = valDefault };
        }

        IAMCameraControl Controller
        {
            get { return m_CameraControlInterface; }
        }

        public void PanToAbsolute(float absolute)
        {
            int min = int.MinValue;
            int max = int.MaxValue;
            int step = 0;
            int defaultValue = 0;
            int currentValue = 0;
            CameraControlFlags camFlags = CameraControlFlags.None;
            //IAMCameraControl Controller = m_CaptureDevice.GetCameraControl();

            Controller.GetRange(CameraControlProperty.Pan, out min, out max, out step, out defaultValue, out camFlags);
            Controller.Get(CameraControlProperty.Pan, out currentValue, out camFlags);
            //if (currentValue > min)
            //    currentValue -= step;
            //camControl.Set(CameraControlProperty.Pan, currentValue, camFlags);
            int abValue = m_PanRange.Min + (int)((absolute * (m_PanRange.Max - m_PanRange.Min)));

            Controller.Set(CameraControlProperty.Pan, abValue, camFlags);
        }

        public void PanRelative(float relative)
        {
        }

        public int Pan
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Pan, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Pan, currentValue, camFlags);
            }
        }

        public int Tilt
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Tilt, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Tilt, currentValue, camFlags);
            }
        }

        public int Roll
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Roll, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Roll, currentValue, camFlags);
            }
        }

        public int Zoom
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Zoom, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Zoom, currentValue, camFlags);
            }
        }

        public int Exposure
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Exposure, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Exposure, currentValue, camFlags);
            }
        }

        public int Iris
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Iris, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Iris, currentValue, camFlags);
            }
        }
     
    public int Focus
        {
            get
            {
                int currentValue = 0;
                CameraControlFlags camFlags = CameraControlFlags.None;

                Controller.Get(CameraControlProperty.Focus, out currentValue, out camFlags);

                return currentValue;
            }

            set
            {
                int currentValue = value;
                CameraControlFlags camFlags = CameraControlFlags.Manual;

                Controller.Set(CameraControlProperty.Focus, currentValue, camFlags);
            }
        }

    }
}
