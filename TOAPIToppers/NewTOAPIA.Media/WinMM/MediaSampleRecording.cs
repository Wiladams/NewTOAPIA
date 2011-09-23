
namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;

    using TOAPI.WinMM;

    public class MediaSampleRecording : MediaSample
    {
        public MediaSampleRecording(IntPtr deviceHandle, int bufferSize)
            :base(bufferSize)
        {
            PrepareForRecording(deviceHandle);
        }

        public void PrepareForRecording(IntPtr waveDevice)
        {
            MmException.Try(winmm.waveInPrepareHeader(waveDevice, fHeader, Marshal.SizeOf(fHeader)), "waveInPrepareHeader");
            MmException.Try(winmm.waveInAddBuffer(waveDevice, GetHeaderPointer(), Marshal.SizeOf(fHeader)), "waveInAddBuffer");
        }

        /// <summary>
        /// Place this buffer back to record more audio
        /// </summary>
        public void Reuse(IntPtr waveInDevice)
        {
            MmException.Try(winmm.waveInUnprepareHeader(waveInDevice, fHeader, Marshal.SizeOf(fHeader)), "waveUnprepareHeader");

            PrepareForRecording(waveInDevice);
        }

        public void Release(IntPtr waveInDevice)
        {
            MmException.Try(winmm.waveInUnprepareHeader(waveInDevice, fHeader, Marshal.SizeOf(fHeader)), "waveUnprepareHeader");
        }

    }
}
