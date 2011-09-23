

namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TOAPI.WinMM;
    
    public class AudioCapabilities
    {
        public short ManufacturerID { get; set; }
        public short ProductID { get; set; }
        public short DriverVersion { get; set; }
        public string ProductName { get; set; }
        public int Formats { get; set; }
        public short Channels { get; set; }
        public int Support { get; set; }
        public bool IsInput { get; set; }

        internal AudioCapabilities(bool isInput, short manufacturerID, short productID, short driverVersion, string productName, 
            int formats, short channels, int support)
        {
            IsInput = isInput;
            ManufacturerID = manufacturerID;
            ProductID = productID;
            DriverVersion = driverVersion;
            ProductName = productName;
            Formats = formats;
            Channels = channels;
            Support = support;
        }

        public bool IsOutput
        {
            get { return !IsInput; }
        }

        public bool HasExtendedSupport
        {
            get {return Support != 0;}
        }

        public short DriverMajor
        {
            get { return (short)((DriverVersion & 0xff00) >> 8); }
        }

        public short DriverMinor
        {
            get { return (short)(DriverVersion & 0xff); }
        }

        #region Static Helpers
        public static AudioCapabilities CreateForInput(WAVEINCAPSW caps)
        {
            AudioCapabilities aud = new AudioCapabilities(true, (short)caps.wMid, (short)caps.wPid, (short)caps.vDriverVersion, caps.szPname, (int)caps.dwFormats, (short)caps.wChannels, 0);

            return aud;
        }

        public static AudioCapabilities CreateForOutput(WAVEOUTCAPS caps)
        {
            AudioCapabilities aud = new AudioCapabilities(true, (short)caps.wMid, (short)caps.wPid, (short)caps.vDriverVersion, caps.szPname, (int)caps.dwFormats, (short)caps.wChannels, (int)caps.dwSupport);

            return aud;
        }
        #endregion

        //public static WAVEINCAPSW[] GetWaveInCapabilities()
        //{
        //    int numDevices = GetNumberOfWaveInDevices();
        //    WAVEINCAPSW[] caps = new WAVEINCAPSW[numDevices];

        //    for (int i = 0; i < numDevices; i++)
        //    {
        //        WAVEINCAPSW newCaps = new WAVEINCAPSW();
        //        IntPtr devID = new IntPtr(i);

        //        winmm.waveInGetDevCapsW(devID, ref newCaps, Marshal.SizeOf(newCaps));

        //        caps[i] = newCaps;
        //    }

        //    return caps;
        //}

    }
}
