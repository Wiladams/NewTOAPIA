using System;
using System.Collections.Generic;
using System.Text;

namespace CameraServer
{
    public enum UICommands
    {
        PixBltRaw = 10000,      // Frame Buffer Management
        PixBltRLE,
        PixBltJPG,
        PixBltLuminance,
        Flush = 10500,
    }
}
