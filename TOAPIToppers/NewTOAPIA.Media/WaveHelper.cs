using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using TOAPI.Types;

namespace NewTOAPIA.Media
{
    // Perform some simple functions on Audio Waveform data
    public class WaveHelper
    {
        public static double GetAmplitudeRange(WAVEFORMATEX wfx)
        {
            double maxRange = 0;

            if (16 == wfx.wBitsPerSample)
                maxRange = short.MaxValue/2;
            if (8 == wfx.wBitsPerSample)
                maxRange = byte.MaxValue/2;

            return maxRange;
        }

        //unsafe public static double GetAmplitudeAverage(AudioEvent anEvent)
        //{
        //    GCHandle gcHandle = GCHandle.Alloc(anEvent.DataBuffer, GCHandleType.Pinned);
        //    double avg = 0;
        //    double sum = 0;

        //    if (16 == anEvent.WaveFormat.wBitsPerSample)
        //    {
        //        // Find the average assuming 'short' as the data type
        //        short* values = (short *)(gcHandle.AddrOfPinnedObject());
        //        int numOfDatum = (int)anEvent.DataLength / 2;

        //        for (int i = 0; i < numOfDatum; i++)
        //        {
        //            sum += values[i];
        //        }

        //        avg = sum / numOfDatum;
        //    }
        //    else if (8 == anEvent.WaveFormat.wBitsPerSample)
        //    {
        //        // Find the average assuming 'byte' as the data type
        //        byte * values = (byte*)gcHandle.AddrOfPinnedObject();
        //        int numOfDatum = (int)anEvent.DataLength;

        //        for (int i = 0; i < numOfDatum; i++)
        //        {
        //            sum += Math.Abs(values[i] - 128);
        //        }

        //        avg = sum / numOfDatum;
        //    }

        //    gcHandle.Free();

        //    return avg;
        //}


    }
}
