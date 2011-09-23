

namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    using TOAPI.Types;
    using TOAPI.WinMM;
    
    public class WaveOutputPort
    {
        #region Fields
        protected WaveCallback fCallbackMethod;
        public int DeviceID { get; set; }
        public WaveOutSafeHandle DeviceHandle { get; set; }
        public WAVEFORMATEX WaveFormat { get; set; }
        public PCMAudioFormat PCMFormat { get; set; }
        #endregion


        #region Fields
        bool fIsOpen;
        #endregion

        #region Constructors
        public WaveOutputPort(int deviceID, WAVEFORMATEX wf)
        {
            fCallbackMethod = new WaveCallback(this.DefaultAudioCallback);

            DeviceHandle = null;
            DeviceID = deviceID;
            WaveFormat = wf;
            PCMFormat = PCMAudioFormat.CreateFromWaveFormat(wf);
            
            Open();
        }
        #endregion

        #region Properties
        public virtual AudioCapabilities DeviceCapabilities
        {
            get
            {
                WAVEOUTCAPS newCaps = new WAVEOUTCAPS();

                MMSYSERROR result = winmm.waveOutGetDevCaps(new IntPtr(DeviceID), ref newCaps, Marshal.SizeOf(typeof(WAVEOUTCAPS)));

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
                MMSYSERROR result = winmm.waveOutGetPlaybackRate(DeviceHandle, out rate);
                
                return rate;
            }

            set
            {
                MMSYSERROR result = winmm.waveOutSetPlaybackRate(DeviceHandle, value);
            }
        }

        public MMTIME Position
        {
            get
            {
                MMTIME mmt = new MMTIME();
                MMSYSERROR result = winmm.waveOutGetPosition(DeviceHandle, ref mmt, (int)Marshal.SizeOf(typeof(MMTIME)));

                return mmt;
            }
        }

        public int Volume
        {
            get
            {
                int volume = 0;
                MMSYSERROR result = winmm.waveOutGetVolume(DeviceHandle, out volume);
                
                return volume;
            }

            set
            {
                MMSYSERROR result = winmm.waveOutSetVolume(DeviceHandle, value);
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

        public virtual void Open()
        {
            int lFlags = (int)MM_CALLBACK.Function;   // Use a callback function to communicate
            int result;
            IntPtr tmpHandle = new IntPtr(0);
            result = (int)winmm.waveOutOpen(ref tmpHandle, DeviceID, WaveFormat, fCallbackMethod, IntPtr.Zero, lFlags);
            DeviceHandle = new WaveOutSafeHandle(tmpHandle);

            fIsOpen = true;
        }

        public virtual void Close()
        {
            fIsOpen = false;
            int result = (int)winmm.waveOutClose(DeviceHandle);
        }

        public virtual void Reset()
        {
            EnsureOpen();

            MMSYSERROR result = winmm.waveOutReset(DeviceHandle);
        }

        public MMSYSERROR Restart()
        {
            EnsureOpen();

            MMSYSERROR result = winmm.waveOutRestart(DeviceHandle);

            return result;
        }

        public MMSYSERROR Pause()
        {
            EnsureOpen();

            MMSYSERROR result = winmm.waveOutPause(DeviceHandle);

            return result;
        }

        public MMSYSERROR BreakLoop()
        {
            EnsureOpen();

            MMSYSERROR result = winmm.waveOutBreakLoop(DeviceHandle);

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


        public MMSYSERROR Message(uint msg, uint dw1, uint dw2)
        {
            EnsureOpen();

            MMSYSERROR result = winmm.waveOutMessage(DeviceHandle, msg, dw1, dw2);
            
            return result;
        }

        public virtual void DeviceOpened(IntPtr devHandle)
        {
        }

        public virtual void DeviceClosed(IntPtr devHandle)
        {
        }

        public virtual void OutputDone(IntPtr wavhdr, IntPtr param2)
        {
        }

        protected virtual void DefaultAudioCallback(IntPtr deviceHandle, int uMsg, IntPtr userData, IntPtr wavhdr, IntPtr reserved)
        {
            switch ((WaveCallbackMsg)uMsg)
            {
                case WaveCallbackMsg.OutputOpened:
                    DeviceOpened(deviceHandle);
                    break;

                case WaveCallbackMsg.OutputClosed:
                    DeviceClosed(deviceHandle);
                    break;

                case WaveCallbackMsg.OutputDone:
                    OutputDone(wavhdr, reserved);
                    break;
            }
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
            IntPtr tmpHandle = new IntPtr();
            MMSYSERROR result = winmm.waveOutOpen(ref tmpHandle, deviceID, wfx, null, IntPtr.Zero, winmm.WAVE_FORMAT_QUERY);

            // If there was any error, return null
            if (winmm.MMSYSERR_NOERROR != result)
                return null;

            WaveOutputPort outPort = new WaveOutputPort(deviceID, wfx);
            
            return outPort;
        }

        public static MMSYSERROR GetErrorText(int mmrError, StringBuilder errorText, int textSize)
        {
            MMSYSERROR result = winmm.waveOutGetErrorText(mmrError, errorText, textSize);

            return result;
        }

        public static int GetNumberOfWaveOutDevices()
        {
            int retValue = winmm.waveOutGetNumDevs();
            
            return retValue;
        }

        public static WAVEOUTCAPS[] GetWaveOutCapabilities()
        {
            int numDevices = GetNumberOfWaveOutDevices();
            WAVEOUTCAPS[] caps = new WAVEOUTCAPS[numDevices];

            for (int i = 0; i < numDevices; i++)
            {
                WAVEOUTCAPS newCaps = new WAVEOUTCAPS();
                IntPtr devID = new IntPtr(i);

                winmm.waveOutGetDevCaps(devID, ref newCaps, Marshal.SizeOf(newCaps));

                caps[i] = newCaps;
            }

            return caps;
        }
        #endregion

    }
}
