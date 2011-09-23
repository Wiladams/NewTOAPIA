
namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    using TOAPI.WinMM;

    public class MediaSampleRecording : MediaSample
    {
        public MediaSampleRecording(WaveInSafeHandle deviceHandle, int bufferSize)
            :base(bufferSize)
        {
            PrepareForRecording(deviceHandle);
        }

        public void PrepareForRecording(WaveInSafeHandle waveDevice)
        {
            MmException.Try(winmm.waveInPrepareHeader(waveDevice, fHeader, Marshal.SizeOf(fHeader)), "waveInPrepareHeader");
            MmException.Try(winmm.waveInAddBuffer(waveDevice, GetHeaderPointer(), Marshal.SizeOf(fHeader)), "waveInAddBuffer");
        }

        /// <summary>
        /// Place this buffer back to record more audio
        /// </summary>
        public void Reuse(WaveInSafeHandle waveInDevice)
        {
            MmException.Try(winmm.waveInUnprepareHeader(waveInDevice, fHeader, Marshal.SizeOf(fHeader)), "waveUnprepareHeader");

            PrepareForRecording(waveInDevice);
        }

        public void Release(WaveInSafeHandle waveInDevice)
        {
            MmException.Try(winmm.waveInUnprepareHeader(waveInDevice, fHeader, Marshal.SizeOf(fHeader)), "waveUnprepareHeader");
        }

    }
}
