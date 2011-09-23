using System;
using System.Runtime.InteropServices;

using TOAPI.Types;
using TOAPI.WinMM;
//using TOAPI.Kernel32;

namespace NewTOAPIA.Media.WinMM
{
    public class WaveAudioPlayer
    {
        WaveOutputPort fOutPort;
        WaveFile fWaveFile;

        // One buffer for output
        WAVEHDR fOutHdr;

        public WaveAudioPlayer(WaveOutputPort outPort, WaveFile aFile)
        {
            fOutPort = outPort;
            fWaveFile = aFile;

            //fOutHdr = WAVEHDR.CreateWithLength((int)fWaveFile.fData.dwChunkSize);
            fOutHdr.dwFlags = 0;
            fOutHdr.dwLoops = 0;

            // Copy the bytes to the fixed memory
            // BUGBUG - need to allocate the memory
            //fOutHdr.lpData = fMemHandle;
            Marshal.Copy(fWaveFile.fData.byteArray, 0, fOutHdr.lpData, (int)fWaveFile.fData.dwChunkSize);

            //fOutPort.PrepareHeader(fOutHdr);
        }

        public void Play()
        {
            //fOutPort.Write(fOutHdr);
        }

        public virtual void Pause()
        {
        }

        public virtual void Stop()
        {
        }


        #region Construction Helpers
        public static WaveAudioPlayer CreateForFile(string filename)
        {
            WaveFile waveFile = new WaveFile(filename);

            WaveOutputPort outPort = WaveOutputPort.CreateOutputPort(winmm.WAVE_MAPPER,
                waveFile.fFormat.wChannels, (int)waveFile.fFormat.dwSamplesPerSec, (int)waveFile.fFormat.dwBitsPerSample);

            WaveAudioPlayer aPlayer = new WaveAudioPlayer(outPort, waveFile);

            return aPlayer;
        }
        #endregion
    }
}
