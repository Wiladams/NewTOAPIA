using System;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace NewTOAPIA.Media
{
    public class AudioEvent : EventArgs
    {
        public byte[] DataBuffer;
        public int DataLength;
        public double SampleTime;
        public WAVEFORMATEX WaveFormat;

        private AudioEvent(IntPtr data, int dataLength, double timeStamp, WAVEFORMATEX wfx)
        {
            DataBuffer = new byte[dataLength];
            DataLength = dataLength;
            SampleTime = timeStamp;
            WaveFormat = wfx;

            //copy the data to the byte buffer
            Marshal.Copy(data, DataBuffer, 0, dataLength);
        }

        public static AudioEvent CreateInstance(IntPtr data, int dataLength, double timeStamp, WAVEFORMATEX wfx)
        {
            if (data == IntPtr.Zero || dataLength < 1)
                throw new ArgumentException("data is null or empty");

            AudioEvent anEvent = new AudioEvent(data, dataLength, timeStamp, wfx);

            return anEvent;
        }
    }
}
