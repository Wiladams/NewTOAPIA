

namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    using TOAPI.Types;
    using TOAPI.WinMM;

    public class WaveInputPort : WinMMAudioDevice, IDisposable
    {
        #region Fields
        bool fIsOpen;
        protected double Latency { get; set; }
        protected int SampleSize { get; set; }
        protected int SampleCount { get; set; }
        #endregion

        #region Constructors
        public WaveInputPort(int deviceID, WAVEFORMATEX wf, double latency)
            :this(deviceID, wf.nChannels, wf.nSamplesPerSec, wf.wBitsPerSample, latency)
        {
        }

        public WaveInputPort(int deviceID, int channels, int sampleRate, int bitsPerSample, double latency)
            :base(deviceID, channels, sampleRate, bitsPerSample)
        {
            Latency = latency;

            // Calculate the sample size based on the latency
            SampleSize = (int)(Latency * PCMFormat.BytesPerSecond);
            SampleCount = (int)(1.0 / Latency);

            fCallbackMethod = new WaveCallback(this.DefaultAudioCallback);
        }
        #endregion

        #region Properties
        public bool IsRecording { get; private set; }
        
        public AudioCapabilities DeviceCapabilities
        {
            get
            {
                WAVEINCAPSW newCaps = new WAVEINCAPSW();
                winmm.waveInGetDevCapsW(fDeviceID, newCaps, Marshal.SizeOf(typeof(WAVEINCAPSW)));

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

        public override void Open()
        {
            int lFlags = (int)MM_CALLBACK.Function;   // Use a callback function to communicate

            MmException.Try(winmm.waveInOpen(out fDeviceHandle, fDeviceID, fWaveFormat, fCallbackMethod, IntPtr.Zero, lFlags), "waveInOpen");

            fIsOpen = true;
        }

        public override void Close()
        {
            MmException.Try(winmm.waveInReset(DeviceHandle), "waveInReset");
            MmException.Try(winmm.waveInClose(DeviceHandle), "waveInClose");
            fIsOpen = false;
            fDeviceHandle = IntPtr.Zero;
        }

        public override void Reset()
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
            int result;
            IntPtr dHandle = IntPtr.Zero;
            result = winmm.waveInOpen(out dHandle, deviceID, wfx, null, IntPtr.Zero, winmm.WAVE_FORMAT_QUERY);

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
        #endregion

    }
}
