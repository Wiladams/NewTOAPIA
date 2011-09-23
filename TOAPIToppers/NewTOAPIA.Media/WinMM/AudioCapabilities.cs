using System;
using System.Collections.Generic;
using System.Text;
using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    public class AudioCapabilities
    {
        short fManufacturerID;
        short fProductID;
        short fDriverVersion;
        string fProductName;
        int fFormats;
        short fChannels;
        int fSupport;
        bool fIsInput;

        internal AudioCapabilities(bool isInput, short manufacturerID, short productID, short driverVersion, string productName, 
            int formats, short channels, int support)
        {
            fIsInput = isInput;
            fManufacturerID = manufacturerID;
            fProductID = productID;
            fDriverVersion = driverVersion;
            fProductName = productName;
            fFormats = formats;
            fChannels = channels;
            fSupport = support;
        }

        public bool IsOutput
        {
            get { return !fIsInput; }
        }

        public bool IsInput
        {
            get { return fIsInput; }
        }

        public short Channels
        {
            get { return fChannels; }
        }

        public bool HasExtendedSupport
        {
            get {return fSupport != 0;}
        }

        public short ManufacturerID
        {
            get { return fManufacturerID; }
        }

        public short ProductID
        {
            get { return fProductID; }
        }

        public short DriverMajor
        {
            get { return (short)((fDriverVersion & 0xff00) >> 8); }
        }

        public short DriverMinor
        {
            get { return (short)(fDriverVersion & 0xff); }
        }

        public static AudioCapabilities CreateForInput(WAVEINCAPSW caps)
        {
            AudioCapabilities aud = new AudioCapabilities(true, (short)caps.wMid, (short)caps.wPid, (short)caps.vDriverVersion, caps.szPname, (int)caps.dwFormats, (short)caps.wChannels, 0);

            return aud;
        }

        public static AudioCapabilities CreateForOutput(WAVEOUTCAPSW caps)
        {
            AudioCapabilities aud = new AudioCapabilities(true, (short)caps.wMid, (short)caps.wPid, (short)caps.vDriverVersion, caps.szPname, (int)caps.dwFormats, (short)caps.wChannels, (int)caps.dwSupport);

            return aud;
        }

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
