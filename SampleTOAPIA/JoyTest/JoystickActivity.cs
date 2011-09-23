
namespace NewTOAPIA.UI
{
//    using System;

    using TOAPI.WinMM;

    public enum JoyHatDirection
    {
        Neutral = 360,
        Forward = 0,
        ForwardRight = 45,
        Right = 90,
        RightBack = 135,
        Back = 180,
        BackLeft = 225,
        Left = 270,
        LeftForward = 315
    }

    public class JoystickActivityArgs
    {
        #region Private Fields
        joyinfoex_tag fStickInfo;
        Joystick fStick;
        float fThreshold;
        float fOffset;
        #endregion

        #region Constructors
        JoystickActivityArgs(Joystick aStick, joyinfoex_tag aJoyInfo, float threshold, float offset)
        {
            fStick = aStick;
            fStickInfo = aJoyInfo;
            fThreshold = threshold;
            fOffset = offset;
        }


        public static JoystickActivityArgs Create(Joystick aStick, float threshold, float offset)
        {
            joyinfoex_tag fStickInfo;
            fStickInfo = new joyinfoex_tag();
            fStickInfo.dwSize = System.Runtime.InteropServices.Marshal.SizeOf(fStickInfo);
            fStickInfo.dwFlags = winmm.JOY_RETURNALL;

            winmm.joyGetPosEx(aStick.ID, ref fStickInfo);

            JoystickActivityArgs newState = new JoystickActivityArgs(aStick, fStickInfo, threshold, offset);

            return newState;
        }

        #endregion

        public float Offset
        {
            get { return fOffset; }
            set { value = fOffset; }
        }

        public float Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }


        public bool AButtonIsPressed
        {
            get { return (fStickInfo.dwButtonNumber != 0); }
        }

        public bool IsButtonPressed(int buttonNumber)
        {
            bool retValue = false;

            int buttonMask = 1 << buttonNumber - 1;
            retValue = (fStickInfo.dwButtons & buttonMask) != 0;

            return retValue;
        }

        public float X
        {
            get
            {
                float relative = (float)XAbsolute / (fStick.MaxX - fStick.MinX);
                
                return relative + Offset;
            }
        }

        public float Y
        {
            get
            {
                float relative = (float)YAbsolute / (fStick.MaxY - fStick.MinY);
                return -(relative + Offset);
            }
        }

        public float Z
        {
            get
            {
                float relative = (float)ZAbsolute / (fStick.MaxZ - fStick.MinZ);
                return 1.0f - relative;
            }
        }

        public double R
        {
            get
            {
                double relative = (float)RAbsolute / (fStick.MaxR - fStick.MinR);
                //if (Math.Abs(relative) < Math.Abs(Threshold))
                //    relative = 0;

                return relative + Offset;
            }
        }

        public JoyHatDirection POV
        {
            get
            {
                return (JoyHatDirection)POVDegrees;
            }
        }

        public int POVDegrees
        {
            get
            {
                // If the hat is in neutral, then we report 360 degrees
                // This works well with the "Forward" position, which
                // is reported as zero degrees.
                if (65535 == POVAbsolute)
                    return 360;

                int degrees = POVAbsolute / 100;
                return degrees;
            }
        }

        #region Absolute Data
        public int XAbsolute
        {
            get { return fStickInfo.dwXpos; }
        }

        public int YAbsolute
        {
            get { return fStickInfo.dwYpos; }
        }

        public int ZAbsolute
        {
            get { return fStickInfo.dwZpos; }
        }

        public int RAbsolute
        {
            get { return fStickInfo.dwRpos; }
        }

        public int POVAbsolute
        {
            get { return fStickInfo.dwPOV; }
        }
        #endregion
    }
}
