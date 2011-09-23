

namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    using TOAPI.Types;
    using TOAPI.WinMM;

    //public delegate void AudioDeviceNotificationProc(WinMMAudioDevice aDevice);
    //public delegate void AudioDeviceBufferReceivedProc(WinMMAudioDevice aDevice, IntPtr bufferPtr, IntPtr param2);

    public class WaveInputPort : Observable<AudioEvent>
    {
        #region Fields
        protected WaveCallback fCallbackMethod;
        public int DeviceID { get; set; }
        public WaveInSafeHandle DeviceHandle {get;set;}
        public WAVEFORMATEX WaveFormat { get; set; }
        public PCMAudioFormat PCMFormat { get; set; }
        #endregion

        #region Fields
        bool fIsOpen;
        protected double Latency { get; set; }
        protected int SampleSize { get; set; }
        protected int SampleCount { get; set; }
        #endregion

        #region Constructors
        public WaveInputPort(int deviceID, int channels, int sampleRate, int bitsPerSample, double latency)
            : this(deviceID, WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample), latency)
        {
        }

        public WaveInputPort(int deviceID, WAVEFORMATEX wf, double latency)
        {
            fCallbackMethod = new WaveCallback(this.DefaultAudioCallback);

            WaveFormat = wf;

            PCMFormat = PCMAudioFormat.CreateFromWaveFormat(wf);

            Latency = latency;
            DeviceHandle = null;
            DeviceID = deviceID;


            // Calculate the sample size based on the latency
            SampleSize = (int)(Latency * PCMFormat.BytesPerSecond);
            SampleCount = (int)(1.0 / Latency);

        }

        #endregion

        #region Properties
        public bool IsRecording { get; private set; }
        
        public AudioCapabilities DeviceCapabilities
        {
            get
            {
                WAVEINCAPSW newCaps = new WAVEINCAPSW();
                winmm.waveInGetDevCapsW(DeviceID, newCaps, Marshal.SizeOf(typeof(WAVEINCAPSW)));

                AudioCapabilities aud = AudioCapabilities.CreateForInput(newCaps);

                return aud;
            }
        }

        public MMTIME Position
        {
            get
            {
                MMTIME mmt = new MMTIME();
                int result = winmm.waveInGetPosition(DeviceHandle, ref mmt, (int)Marshal.SizeOf(typeof(MMTIME)));

                return mmt;
            }
        }


        #endregion

        #region Basic Control
        protected void EnsureOpen()
        {
            // If the port is already open, return immediately
            if (fIsOpen)
                return;

            // If not already open, then open the port
            Open();
        }

        public virtual void Open()
        {
            WAVEOPENFLAGS flags = WAVEOPENFLAGS.CALLBACK_FUNCTION;

            IntPtr tmpHandle = new IntPtr(0);
            MmException.Try(winmm.waveInOpen(ref tmpHandle, DeviceID, WaveFormat, fCallbackMethod, IntPtr.Zero, flags), "waveInOpen");

            DeviceHandle = new WaveInSafeHandle(tmpHandle);

            fIsOpen = true;
        }

        public virtual void Close()
        {
            MmException.Try(winmm.waveInReset(DeviceHandle), "waveInReset");
            MmException.Try(winmm.waveInClose(DeviceHandle), "waveInClose");
            fIsOpen = false;
            DeviceHandle.Dispose();
        }

        public virtual void Reset()
        {
            EnsureOpen();

            MmException.Try(winmm.waveInReset(DeviceHandle), "waveInReset");
        }

        public void WaitForClosed()
        {
            //fHasClosed.WaitOne();
        }


        public void Start()
        {
            EnsureOpen();

            MmException.Try(winmm.waveInStart(DeviceHandle), "waveInOpen");
        }

        public void Stop()
        {
            MmException.Try(winmm.waveInStop(DeviceHandle), "waveInStop");
        }
        #endregion

        #region Writing Data
        //public void AddBuffer(IntPtr pwh)
        //{
        //    EnsureOpen();

        //    PrepareHeader(pwh);
        //    MmException.Try(winmm.waveInAddBuffer(DeviceHandle, pwh, (int)Marshal.SizeOf(typeof(WAVEHDR))), "waveInAddBuffer");
        //}

        //public int AddBuffer(ref WAVEHDR pwh)
        //{

        //    int result = AddBuffer(ref pwh, (int)Marshal.SizeOf(typeof(WAVEHDR)));

        //    return result;
        //}

        //protected int AddBuffer(ref WAVEHDR pwh, int cbwh)
        //{
        //    EnsureOpen();
        //    PrepareHeader(pwh);
        //    int result = winmm.waveInAddBuffer(DeviceHandle, ref pwh, cbwh);

        //    return result;
        //}

        //public void PrepareHeader(IntPtr pwh)
        //{
        //    EnsureOpen();

        //    MmException.Try(winmm.waveInPrepareHeader(DeviceHandle, pwh, (int)Marshal.SizeOf(typeof(WAVEHDR))), "waveInPrepareHeader");
        //}

        //public int PrepareHeader(WAVEHDR pwh)
        //{
        //    int result = PrepareHeader(pwh, (int)Marshal.SizeOf(typeof(WAVEHDR)));

        //    return result;
        //}

        //protected int PrepareHeader(WAVEHDR pwh, int cbwh)
        //{
        //    EnsureOpen();

        //    int result = winmm.waveInPrepareHeader(DeviceHandle, ref pwh, cbwh);

        //    return result;
        //}

        //public void UnprepareHeader(IntPtr pwh)
        //{
        //    EnsureOpen();

        //    MmException.Try(winmm.waveInUnprepareHeader(DeviceHandle, pwh, (int)Marshal.SizeOf(typeof(WAVEHDR))), "waveInUnprepareHeader");
        //}

        public int UnprepareHeader(WAVEHDR pwh)
        {
            EnsureOpen();

            int result = winmm.waveInUnprepareHeader(DeviceHandle, pwh, (int)Marshal.SizeOf(pwh));

            return result;
        }
        #endregion


        public int Message(uint msg, IntPtr dw1, IntPtr dw2)
        {
            EnsureOpen();

            int result = winmm.waveInMessage(DeviceHandle, (int)msg, dw1, dw2);

            return result;
        }

        #region Static Functions
        /// <summary>
        /// Create a WaveOutPort that represents the default output device for the
        /// user's system.
        /// </summary>
        /// <returns>The output port, or null if it could not be created.</returns>
        public static WaveInputPort CreateDefaultPort(double latency)
        {
            return CreateInputPort(winmm.WAVE_MAPPER, 1, 11025, 8, latency);
        }

        /// <summary>
        /// Help the user create an output device by specifying a few key parameters.
        /// </summary>
        /// <param name="deviceID"></param>
        /// <param name="channels"></param>
        /// <param name="sampleRate"></param>
        /// <param name="bitsPerSample"></param>
        /// <returns></returns>
        public static WaveInputPort CreateInputPort(int deviceID, int channels, int sampleRate, int bitsPerSample, double latency)
        {
            WAVEFORMATEX wfx = WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample);
            
            // If an error occured, return null
            // We could throw an exception here
            if (!CanSupportFormat(deviceID, channels, sampleRate, bitsPerSample))
                return null;

            WaveInputPort aPort = new WaveInputPort(deviceID, wfx, latency);

            return aPort;
        }

        public static bool CanSupportFormat(int deviceID, int channels, int sampleRate, int bitsPerSample)
        {
            int bytesPerSample = bitsPerSample / 8;

            WAVEFORMATEX wfx = WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample);

            // Try to open the port with a query to see if the specified format
            // can be supported.
            IntPtr dHandle = new IntPtr(0);
            MMSYSERROR result = winmm.waveInOpen(ref dHandle, deviceID, wfx, null, IntPtr.Zero, WAVEOPENFLAGS.WAVE_FORMAT_QUERY);

            // If an error occured, return false
            if (winmm.MMSYSERR_NOERROR != result)
                return false;

            return true;
        }

        public static int GetErrorText(int mmrError, StringBuilder errorText, int textSize)
        {
            int result = winmm.waveInGetErrorTextW(mmrError, errorText, textSize);

            return (int)result;
        }

        public static int GetNumberOfWaveInDevices()
        {
            int retValue = winmm.waveInGetNumDevs();

            return retValue;
        }

        public virtual void Dispose()
        {
            Stop();
            //FreeeWaveBuffers();
        }

        public virtual void DeviceOpened(IntPtr devHandle)
        {
        }

        public virtual void DeviceClosed(IntPtr devHandle)
        {
        }

        public virtual void DataReceived(IntPtr wavhdr, IntPtr param2)
        {

        }

        protected virtual void DefaultAudioCallback(IntPtr deviceHandle, int uMsg, IntPtr userData, IntPtr wavhdr, IntPtr reserved)
        {
            switch ((WaveCallbackMsg)uMsg)
            {
                case WaveCallbackMsg.InputOpened:
                    DeviceOpened(deviceHandle);
                    break;

                case WaveCallbackMsg.InputClosed:   // Port has been opened
                    DeviceClosed(deviceHandle);
                    break;

                case WaveCallbackMsg.InputData:
                    DataReceived(wavhdr, reserved);
                    break;
            }
        }
        #endregion

    }
}
