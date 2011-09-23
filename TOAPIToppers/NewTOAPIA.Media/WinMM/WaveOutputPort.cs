using System;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.WinMM;
using TOAPI.Kernel32;

namespace NewTOAPIA.Media.WinMM
{
    public class WaveOutputPort : WinMMAudioDevice
    {

        #region Fields
        bool fIsOpen;
        #endregion

        #region Constructors

        public WaveOutputPort(int deviceID, WAVEFORMATEX wf)
            :base(deviceID, wf)
        {
            fCallbackMethod = new WaveCallback(this.DefaultAudioCallback);
            
            Open();
        }
        #endregion

        #region Properties
        public virtual AudioCapabilities DeviceCapabilities
        {
            get
            {
                WAVEOUTCAPSW newCaps = new WAVEOUTCAPSW();

                int result = winmm.waveOutGetDevCapsW(new IntPtr(fDeviceID), ref newCaps, Marshal.SizeOf(typeof(WAVEOUTCAPSW)));

                AudioCapabilities aud = AudioCapabilities.CreateForOutput(newCaps);

                return aud;
            }
        }


        public int Pitch
        {
            get
            {
                int pitch = 0;
                winmm.waveOutGetPitch(DeviceHandle, out pitch);
                return pitch;
            }

            set
            {
                winmm.waveOutSetPitch(DeviceHandle, value);
            }
        }

        public int PlaybackRate
        {
            get
            {
                int rate = 0;
                int result = winmm.waveOutGetPlaybackRate(DeviceHandle, out rate);
                
                return rate;
            }

            set
            {
                int result = winmm.waveOutSetPlaybackRate(DeviceHandle, value);
            }
        }

        public MMTIME Position
        {
            get
            {
                MMTIME mmt = new MMTIME();
                int result = winmm.waveOutGetPosition(DeviceHandle, ref mmt, (int)Marshal.SizeOf(typeof(MMTIME)));

                return mmt;
            }
        }

        public int Volume
        {
            get
            {
                int volume = 0;
                int result = winmm.waveOutGetVolume(DeviceHandle, out volume);
                
                return volume;
            }

            set
            {
                int result = winmm.waveOutSetVolume(DeviceHandle, value);
            }
        }


        #endregion

        #region Basic Control
        void EnsureOpen()
        {
            if (fIsOpen)
                return;

            //Open(); 
        }

        public override void Open()
        {
            int lFlags = (int)MM_CALLBACK.Function;   // Use a callback function to communicate
            int result;
            result = (int)winmm.waveOutOpen(out fDeviceHandle, fDeviceID, fWaveFormat, fCallbackMethod, IntPtr.Zero, lFlags);

            fIsOpen = true;
        }

        public override void Close()
        {
            fIsOpen = false;
            int result = (int)winmm.waveOutClose(DeviceHandle);
        }

        public override void Reset()
        {
            EnsureOpen();

            int result = winmm.waveOutReset(DeviceHandle);
        }

        public int Restart()
        {
            EnsureOpen();

            int result = winmm.waveOutRestart(DeviceHandle);

            return result;
        }

        public int Pause()
        {
            EnsureOpen();
            
            int result = winmm.waveOutPause(DeviceHandle);

            return result;
        }

        public int BreakLoop()
        {
            EnsureOpen();
            
            int result = (int)winmm.waveOutBreakLoop(DeviceHandle);

            return result;
        }
        #endregion

        #region Writing Data
        public void Write(WAVEHDR pwh)
        {
            MmException.Try(winmm.waveOutWrite(DeviceHandle, pwh, (int)Marshal.SizeOf(typeof(WAVEHDR))), "waveOutWrite");
        }

        public void PrepareHeader(WAVEHDR pwh)
        {
            MmException.Try(winmm.waveOutPrepareHeader(DeviceHandle, pwh, (int)Marshal.SizeOf(pwh)), "waveOutPrepareHeader");
        }

        public void UnprepareHeader(WAVEHDR pwh)
        {
            EnsureOpen();

            MmException.Try(winmm.waveOutUnprepareHeader(DeviceHandle, pwh, Marshal.SizeOf(pwh)), "waveOutUnprepareHeader");
        }
        #endregion


        public int Message(uint msg, uint dw1, uint dw2)
        {
            EnsureOpen();
            
            int result = winmm.waveOutMessage(DeviceHandle, msg, dw1, dw2);
            
            return result;
        }

        #region Static Functions
        /// <summary>
        /// Create a WaveOutPort that represents the default output device for the
        /// user's system.
        /// </summary>
        /// <returns>The output port, or null if it could not be created.</returns>
        public static WaveOutputPort CreateDefaultPort()
        {
            return CreateOutputPort(winmm.WAVE_MAPPER, 1, 11025, 8);
        }

        /// <summary>
        /// Help the user create an output device by specifying a few key parameters.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="channels"></param>
        /// <param name="sampleRate"></param>
        /// <param name="bitsPerSample"></param>
        /// <returns></returns>
        public static WaveOutputPort CreateOutputPort(int deviceID, int channels, int sampleRate, int bitsPerSample)
        {
            int bytesPerSample = bitsPerSample / 8;

            WAVEFORMATEX wfx = WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample);

            // Query to see if the specified format is supported
            IntPtr deviceHandle = new IntPtr();
            int result = winmm.waveOutOpen(out deviceHandle, deviceID, wfx, null, IntPtr.Zero, winmm.WAVE_FORMAT_QUERY);

            // If there was any error, return null
            if (winmm.MMSYSERR_NOERROR != result)
                return null;

            WaveOutputPort outPort = new WaveOutputPort(deviceID, wfx);
            
            return outPort;
        }

        public static int GetErrorText(int mmrError, StringBuilder errorText, int textSize)
        {
            uint result = winmm.waveOutGetErrorText(mmrError, errorText, textSize);

            return (int)result;
        }

        public static int GetNumberOfWaveOutDevices()
        {
            int retValue = winmm.waveOutGetNumDevs();
            
            return retValue;
        }

        public static WAVEOUTCAPSW[] GetWaveOutCapabilities()
        {
            int numDevices = GetNumberOfWaveOutDevices();
            WAVEOUTCAPSW[] caps = new WAVEOUTCAPSW[numDevices];

            for (int i = 0; i < numDevices; i++)
            {
                WAVEOUTCAPSW newCaps = new WAVEOUTCAPSW();
                IntPtr devID = new IntPtr(i);

                winmm.waveOutGetDevCapsW(devID, ref newCaps, Marshal.SizeOf(newCaps));

                caps[i] = newCaps;
            }

            return caps;
        }
        #endregion

    }
}
