using System;
using System.Collections.Generic;
using System.Text;
using TOAPI.Types;

namespace NewTOAPIA.Media
{
    public class PCMAudioFormat
    {
        int fAvgBytesPerSec;
        int fBitsPerSample;
        int fBlockAlign;
        int fChannels;
        int fSamplesPerSec;

        public PCMAudioFormat(int channels, int bitsPerSample, int samplesPerSecond)
        {
            int bytesPerSample = bitsPerSample / 8;

            fChannels = channels;
            fBitsPerSample = bitsPerSample;      // typically 8 or 16
            fSamplesPerSec = samplesPerSecond;

            fAvgBytesPerSec = samplesPerSecond * bytesPerSample;
            fBlockAlign = channels * bytesPerSample;
        }

        public int BitsPerSample
        {
            get { return fBitsPerSample; }
        }

        public int BlockAlign
        {
            get { return fBlockAlign; }
        }

        public int BytesPerSecond
        {
            get { return fAvgBytesPerSec; }
        }

        public int Channels
        {
            get { return fChannels; }
        }

        public int SamplesPerSecond
        {
            get { return fSamplesPerSec; }
        }


        public static PCMAudioFormat CreateFromWaveFormat(WAVEFORMATEX wfx)
        {
            PCMAudioFormat af = new PCMAudioFormat(wfx.nChannels, wfx.wBitsPerSample, (int)wfx.nSamplesPerSec);

            return af;
        }

    }
}
