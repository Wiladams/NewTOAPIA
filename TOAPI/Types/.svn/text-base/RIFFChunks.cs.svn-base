using System;

namespace TOAPI.Types
{
    public class RIFFChunk
    {
        public uint chunkID;      // foure bytes: "data", "fact", "fmt", etc.
        public uint dwChunkSize;    // length of header in bytes
    }

    public struct RIFFHeader
    {
        public uint sGroupID;       // this is always "RIFF"
        public uint dwFileLength;   //File length in bytes, measured from offset 8
        public uint sRiffType;      //In wave audio files, this is always "WAVE"
    }

    public class RIFFFormatChunk : RIFFChunk
    {
        // chunkID == "fmt "
        // dwChunkSize == 
        public ushort  wFormatTag;      //1 if uncompressed Microsoft PCM audio
        public ushort  wChannels;       //Number of channels
        public uint    dwSamplesPerSec; //Frequency of the audio in Hz
        public uint    dwAvgBytesPerSec;//For estimating RAM allocation
        public ushort  wBlockAlign;     //Sample frame size in bytes
        public uint    dwBitsPerSample; //Bits per sample    
    }

    public class RIFFDataChunk : RIFFChunk
    {
        // chunkID;       //Four bytes: "data"
        // dwChunkSize;    //Length of header in bytes
        public long lFilePosition;     //Position of data chunk in file
        public uint dwMinLength;       //Length of audio in minutes
        public double dSecLength;      //Length of audio in seconds
        public uint dwNumSamples;      //Number of audio frames

        // Different arrays for the different frame sizes
        public byte[] byteArray;       // 8 bit unsigned data; or...
        public short [] shortArray;    // 16 bit signed data
    }

    public class RIFFFactChunk : RIFFChunk
    {
        // chunkID;       //Four bytes: "fact"
        // dwChunkSize;    //Length of header in bytes
        public uint dwNumSamples;        //Number of audio frames</PRE>
    }
}
