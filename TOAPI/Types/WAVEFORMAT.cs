using System;
using System.Runtime.InteropServices;
using System.IO;

namespace TOAPI.Types
{

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Auto)]
    public class WAVEFORMAT
    {
        // Used for WAVEFORMATEX.wFormatTag, to indicate PCM Wave Format
        public const int WAVE_FORMAT_PCM = 1;

        public short wFormatTag;       /// WORD->unsigned short
        public short nChannels;        /// Number of channels of data
        public int nSamplesPerSec;     /// Number of samples per second.  This is essentially doubling the sampling frequency
        public int nAvgBytesPerSec;    /// Number of bytes per second
        public short nBlockAlign;      /// WORD->unsigned short
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class WAVEFORMATEX
    {
        public short wFormatTag;       /// WORD->unsigned short
        public short nChannels;        /// Number of channels of data
        public int nSamplesPerSec;     /// Number of samples per second.  This is essentially doubling the sampling frequency
        public int nAvgBytesPerSec;    /// Number of bytes per second
        public short nBlockAlign;      /// WORD->unsigned short
                                       /// 
        public ushort wBitsPerSample;   /// WORD->unsigned short
        public ushort cbSize;           /// 0 == PCM, otherwise, more specific
                                        


        public uint Read(BinaryReader rdr)
        {
            wFormatTag = rdr.ReadInt16();
            nChannels = rdr.ReadInt16();
            nSamplesPerSec = rdr.ReadInt32();
            nAvgBytesPerSec = rdr.ReadInt32();
            nBlockAlign = rdr.ReadInt16();
            wBitsPerSample = rdr.ReadUInt16();

            // Unused subchunk Id and size
            uint dataId = rdr.ReadUInt32();
            uint dataLength = rdr.ReadUInt32();

            return dataLength;
        }

        public void Write(BinaryWriter wrtr)
        {
            wrtr.Write(wFormatTag);
            wrtr.Write(nChannels);
            wrtr.Write(nSamplesPerSec);
            wrtr.Write(nAvgBytesPerSec);
            wrtr.Write(nBlockAlign);
            wrtr.Write(wBitsPerSample);
        }

        public static WAVEFORMATEX CreatePCMFormat(int channels, int sampleRate, int bitsPerSample)
        {
            int bytesPerSample = bitsPerSample / 8;
            WAVEFORMATEX wfx = new WAVEFORMATEX();

            wfx.wFormatTag = WAVEFORMAT.WAVE_FORMAT_PCM;
            wfx.nChannels = (short)channels;
            wfx.wBitsPerSample = (ushort)bitsPerSample;      // 8 or 16
            wfx.nSamplesPerSec = (ushort)sampleRate;

            wfx.nAvgBytesPerSec = (int)(sampleRate * channels * bytesPerSample);
            wfx.nBlockAlign = (short)(channels * bytesPerSample);

            wfx.cbSize = 0;

            return wfx;
        }
    }
}
