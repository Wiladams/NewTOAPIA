

namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    using TOAPI.Types;
    using TOAPI.WinMM;
    using NewTOAPIA.BCL;
    
    public class WaveSpeaker : WaveOutputPort
    {
        Dictionary<IntPtr, MediaSample> fSamples;
        BlockingBoundedQueue<WAVEHDR> fAvailableBuffers;

        double fLatency;
        int BufferSize { get; set; }
        int BufferCount { get; set; }

        #region Constructors
        public WaveSpeaker()
            : this(winmm.WAVE_MAPPER, 1, 11025, 8, 1.0/10.0)
        {
        }

        public WaveSpeaker(int channels, int sampleRate, int bitsPerSample, double latency)
            :this(winmm.WAVE_MAPPER, channels, sampleRate, bitsPerSample, latency)
        {
        }

        public WaveSpeaker(int deviceID, int channels, int sampleRate, int bitsPerSample, double latency)
            : base(deviceID, WAVEFORMATEX.CreatePCMFormat(channels, sampleRate, bitsPerSample))
        {
            // Setup speakers
            int numOutputDevices = WaveOutputPort.GetNumberOfWaveOutDevices();

            fLatency = latency;
            BufferSize = (int)(fLatency * PCMFormat.BytesPerSecond);
            BufferCount = (int)(1.0 / fLatency);


            AllocateBuffers(BufferSize, BufferCount);
        }
        #endregion

        void AllocateBuffers(int bufferSize, int bufferCount)
        {
            fSamples = new Dictionary<IntPtr, MediaSample>();
            fAvailableBuffers = new BlockingBoundedQueue<WAVEHDR>(bufferCount);

            for (int i = 0; i < bufferCount; i++)
            {
                MediaSample aSample = new MediaSample(bufferSize);

                fSamples.Add(aSample.GetHeaderPointer(), aSample);
                fAvailableBuffers.Enqueue(aSample.GetHeader());
            }
        }

        public void Write(AudioEvent anEvent)
        {
            if (null == anEvent ||
                anEvent.DataBuffer == null ||
                anEvent.DataLength < 1)
                throw new ArgumentNullException("WaveSpeaker::Write(anEvent)");

            try
            {
                // Get an available buffer
                WAVEHDR aHeader = fAvailableBuffers.Dequeue();

                // Prepare the header
                UnprepareHeader(aHeader);
                PrepareHeader(aHeader);

                // copy the data to the buffer
                Marshal.Copy(anEvent.DataBuffer, 0, aHeader.lpData, anEvent.DataBuffer.Length);


                // Write it out to the device
                Write(aHeader);
            }
            catch (Exception e)
            {
                Console.WriteLine("WaveSpeaker::Write - {0}", e.Message);
            }
        }

        //public void Write(byte[] data, int dataLength)
        //{
        //    if (null == data)
        //        return;

        //    if (0 == dataLength)
        //        return;

        //    // Get an available buffer
        //    if (0 == fAvailableBuffers.Count)
        //        return;

        //    WAVEHDR aSample = fAvailableBuffers.Dequeue();

        //    if (aSample == null)
        //    {
        //        throw new ArgumentNullException("aSample");
        //    }

        //    // copy the data to the buffer
        //    Marshal.Copy(data, 0, aSample.lpData, dataLength);


        //    // Prepare the header
        //    //fSpeakers.UnprepareHeader(ref fSpeakerSample);
        //    PrepareHeader(aSample);

        //    // Write it out to the device
        //    Write(aSample);
        //}

        #region Basic Controls
        public void Start()
        {
            Restart();
        }

        public void Stop()
        {
            Reset();
        }
        #endregion


        protected override void DefaultAudioCallback(IntPtr deviceHandle, int uMsg, IntPtr dwUser, IntPtr wavHdr, IntPtr reserved)
        {
            switch ((WaveCallbackMsg)uMsg)
            {
                case WaveCallbackMsg.OutputClosed:
                    Console.WriteLine("WaveOutCallback - Closed");
                    break;

                case WaveCallbackMsg.OutputDone:
                    // When the sample comes back, stick it back in the queue
                    // after unpreparing it.
                    // 
                    MediaSample aSample = null;
                    if (fSamples.TryGetValue(wavHdr, out aSample))
                    {
                        UnprepareHeader(aSample.GetHeader());
                        fAvailableBuffers.Enqueue(aSample.GetHeader());
                    }
                    break;

                case WaveCallbackMsg.OutputOpened:
                    Console.WriteLine("WaveSpeaker.WaveOutCallback - Opened");
                    break;
            }
        }

    }
}
