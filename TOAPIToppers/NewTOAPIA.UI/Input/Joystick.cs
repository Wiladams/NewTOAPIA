using System.Runtime.InteropServices;

using TOAPI.WinMM;
using TOAPI.Types;

namespace NewTOAPIA.UI
{
    public class Joystick
    {
        int fStickID;
        JoystickCapabilities fJoyCaps;

        float fThreshold;
        float fOffset;

        public Joystick(int joyID)
        {
            fStickID = joyID;
            fJoyCaps = new JoystickCapabilities();

            winmm.joyGetDevCapsW(ID, ref fJoyCaps, Marshal.SizeOf(fJoyCaps));

            Offset = 0;
            Threshold = 0.01f;
        }

        #region Properties
        public int ID
        {
            get { return fStickID; }
        }


        public int ButtonsAvailable
        {
            get { return fJoyCaps.wMaxButtons; }
        }

        public int ButtonsInUse
        {
            get { return fJoyCaps.wNumButtons; }
        }

        public string ProductName
        {
            get { return fJoyCaps.szPname; }
        }

        public float Threshold
        {
            get { return fThreshold; }
            set { fThreshold = value; }
        }

        public float Offset
        {
            get { return fOffset; }
            set { fOffset = value; }
        }

        #region Axes Min/Max Values
        public int AxesAvailable
        {
            get { return fJoyCaps.wMaxAxes; }
        }

        public int AxesInUse
        {
            get { return fJoyCaps.wNumAxes; }
        }

        /// <summary>
        /// Minimum rudder value.
        /// </summary>
        /// <remarks>This is the 4th axes</remarks>
        public int MinR
        {
            get { return fJoyCaps.wRmin; }
        }

        public int MaxR
        {
            get { return fJoyCaps.wRmax; }
        }

        public int MinX
        {
            get { return fJoyCaps.wXmin; }
        }

        public int MaxX
        {
            get { return fJoyCaps.wXmax; }
        }

        public int MinY
        {
            get { return fJoyCaps.wYmin; }
        }

        public int MaxY
        {
            get { return fJoyCaps.wYmax; }
        }

        public int MinZ
        {
            get { return fJoyCaps.wZmin; }
        }

        public int MaxZ
        {
            get { return fJoyCaps.wZmax; }
        }
        #endregion

        #region Axes Capabilities
        public bool HasZAxisInformation
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_HASZ));
            }
        }

        public bool HasRAxisInformation
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_HASR));
            }
        }

        public bool HasUAxisInformation
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_HASU));
            }
        }

        public bool HasVAxisInformation
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_HASV));
            }
        }

        public bool HasPOVInformation
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_HASPOV));
            }
        }

        public bool HasDiscretePOV
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_POV4DIR));
            }
        }

        public bool HasContinuousPOV
        {
            get
            {
                return (0 != (fJoyCaps.wCaps & winmm.JOYCAPS_POVCTS));
            }
        }

        #endregion
        #endregion

        public JoystickActivityArgs GetCurrentState()
        {
            JoystickActivityArgs currentState = JoystickActivityArgs.Create(this, Threshold, Offset);

            return currentState;
        }

        public override string ToString()
        {
            string infostr = "<Joystick>";
            infostr += "<ID>" + ID + "</ID>";
            infostr += "<Axes Number='" + AxesInUse + "'>";
            infostr += "<Axis Name='X' Min='" + MinX + "', Max='" + MaxX + "'/>";
            infostr += "<Axis Name='Y' Min='" + MinY + "', Max='" + MaxY + "'/>";
            infostr += "<Axis Name='Z' Min='" + MinZ + "', Max='" + MaxZ + "'/>";
            infostr += "<Axis Name='R' Min='" + MinR + "', Max='" + MaxR + "'/>";
            infostr += "</Axes>";
            infostr += "</Joystick>";

            return infostr;
        }

        public static Joystick GetJoystick(int stickID)
        {
            // Valid joysticks are numbered from 0 to n-1
            // where 'n' == number of joysticks in the system

            // First, find out how many joysticks in the system
            int numSticks = winmm.joyGetNumDevs();
            if (stickID > numSticks - 1)
            {
                // Throw an exception indicating the id is greater than the number
                // of sticks available.
                return null;
            }

            // Now that we have a valid stick id, create the joystick
            // object using that ID
            Joystick aStick = new Joystick(stickID);

            // Return the one we created
            return aStick;
        }
    }
}
