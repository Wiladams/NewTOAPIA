using System;
using System.Collections.Generic;
using System.Text;

namespace TOAPI.HID
{
    public partial class HID_Constants
    {
        //  API declarations for HID communications.

        //  from hidpi.h
        //  Typedef enum defines a set of integer constants for HidP_Report_Type

        public const Int16 HidP_Input = 0;
        public const Int16 HidP_Output = 1;
        public const Int16 HidP_Feature = 2;

    }
}
