using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;
using TOAPI.Kernel32;
using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    /// <summary>
    /// Allocate and manage a number of buffers that are to be used
    /// for input and output of wave data.
    /// </summary>
    public class MediaSamplePool
    {
        Queue<WAVEHDR> fAvailableSamples;
        Queue<WAVEHDR> fInUseSamples;

        public MediaSamplePool(int sampleSize, int numberOfSamples)
        {
            fInUseSamples = new Queue<WAVEHDR>(numberOfSamples);
            fAvailableSamples = new Queue<WAVEHDR>(numberOfSamples);

            for (int counter = 0; counter < numberOfSamples; counter++)
            {
                // Allocate a header object
                WAVEHDR Header = WAVEHDR.CreateWithLength(sampleSize);
                Header.dwBytesRecorded = 0;
                Header.dwFlags = 0;

                fAvailableSamples.Enqueue(Header);
            }
        }

        public WAVEHDR GetAvailableSample()
        {
            // Check the available list 
            // if it's empty, then return null
            if (fAvailableSamples.Count < 1)
                return null;

            WAVEHDR aSample = fAvailableSamples.Dequeue();
            fInUseSamples.Enqueue(aSample);

            return aSample;
        }

        public void ReturnSample(WAVEHDR aSample)
        {
            fAvailableSamples.Enqueue(aSample);
        }
    }
}
