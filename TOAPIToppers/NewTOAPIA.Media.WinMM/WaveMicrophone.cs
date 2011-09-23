using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;


using NewTOAPIA.Media.WinMM;

using TOAPI.Types;
using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    using NewTOAPIA.BCL;

    public class WaveMicrophone : WaveInputPort
    {
        BlockingBoundedQueue<AudioEvent> fAudioEvents;
        Dictionary<IntPtr, MediaSampleRecording> fSamples = new Dictionary<IntPtr, MediaSampleRecording>();

        #region Constructor
        public WaveMicrophone()
            : this(winmm.WAVE_MAPPER, 1, 11025, 8, 1.0/10.0)
        {
        }

        public WaveMicrophone(int channels, int sampleRate, int bitsPerSample, double latency)
            : this(winmm.WAVE_MAPPER, channels, sampleRate, bitsPerSample, latency)
        {
        }

        public WaveMicrophone(int deviceID, int channels, int sampleRate, int bitsPerSample, double latency)
            : base(deviceID, channels, sampleRate, bitsPerSample, latency)
        {
            fAudioEvents = new BlockingBoundedQueue<AudioEvent>(SampleCount);

            Open();

            // Preload buffers so we are ready to start recording
            AllocateBuffers(DeviceHandle);
        }
        #endregion

        #region Properties
        public BlockingBoundedQueue<AudioEvent> AudioEvents
        {
            get { return fAudioEvents; }
        }
        #endregion

        #region Utility Functions
        void AllocateBuffers(WaveInSafeHandle devHandle)
        {
            for (int i = 0; i < SampleCount; i++)
            {
                MediaSampleRecording aSample = new MediaSampleRecording(devHandle, SampleSize);
                IntPtr headerPointer = aSample.GetHeaderPointer();
                fSamples.Add(headerPointer, aSample);
            }
        }
        #endregion

        #region Callback Routines
        public override void DeviceClosed(IntPtr devHandle)
        {
            //    // Some drivers need the reset to properly release buffers
            //    WaveInterop.waveInReset(waveInHandle);
            //if (fSamples.Values != null)
            //{
            //    foreach (MediaSampleRecording sample in fSamples.Values)
            //    {
            //        sample.Dispose();
            //    }
            //    buffers = null;
            //}
            
            //    WaveInterop.waveInClose(waveInHandle);
            DeviceHandle.Dispose();
        }

        public override void DataReceived(IntPtr wavhdrPtr, IntPtr param2)
        {
            if (wavhdrPtr == null)
                throw new ArgumentNullException("WaveMicrophone::ReceiveBuffer - bufferPtr");

            MediaSampleRecording aSample = null;

            if (!fSamples.TryGetValue(wavhdrPtr, out aSample))
                return ;

            try
            {
                // Copy the data into an audio event
                AudioEvent anEvent = AudioEvent.CreateInstance(aSample.GetHeader().lpData, (int)aSample.GetHeader().dwBytesRecorded, 0, WaveFormat);

                // Enqueue the audio event
                fAudioEvents.Enqueue(anEvent);

                // Put the buffer back into the recording queue
                aSample.PrepareForRecording(DeviceHandle);
            }
            catch (Exception e)
            {
                Console.WriteLine("WaveMicrophone::ReceivedBuffer - {0}", e.Message);
            }
        }
        #endregion
    }
}
