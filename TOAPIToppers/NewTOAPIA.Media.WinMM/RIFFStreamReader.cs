using System;
using System.Text;
using System.IO;

using TOAPI.Types;
//using TOAPI.AviCap32;
using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    class RIFFStreamReader
    {
        BinaryReader fReader;
        RIFFFactChunk fFact;
        RIFFFormatChunk fFormat;

        public RIFFStreamReader(string filename)
            : this(new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
        }

        public RIFFStreamReader(Stream inStream)
        {
            fFact = null;
            fFormat = null;
            fReader = new BinaryReader(inStream);
        }

        public BinaryReader BinaryReader
        {
            get { return fReader; }
        }

        public UInt32 ReadFourCC()
        {
            byte[] bytes = fReader.ReadBytes(4);
            return FOURCC.MakeFourCC(bytes);
        }

        public RIFFHeader ReadHeader()
        {
            RIFFHeader header = new RIFFHeader();
            header.sGroupID = ReadFourCC();
            header.dwFileLength = fReader.ReadUInt32();
            header.sRiffType = ReadFourCC();

            return header;
        }

        public RIFFFactChunk ReadFactChunk()
        {
            fFact = new RIFFFactChunk();

            fFact.chunkID = FOURCC.fact;
            fFact.dwChunkSize = fReader.ReadUInt32();
            fFact.dwNumSamples = fReader.ReadUInt32();

            return fFact;
        }
        
        public RIFFFormatChunk ReadFormatChunk()
        {
            fFormat = new RIFFFormatChunk();

            fFormat.chunkID = FOURCC.fmt;
            fFormat.dwChunkSize = fReader.ReadUInt32();
            fFormat.wFormatTag = fReader.ReadUInt16();
            fFormat.wChannels = fReader.ReadUInt16();
            fFormat.dwSamplesPerSec = fReader.ReadUInt32();
            fFormat.dwAvgBytesPerSec = fReader.ReadUInt32();
            fFormat.wBlockAlign = fReader.ReadUInt16();
            
            // If the wFormatTag indicates WAVE_FORMAT_PCM
            // Then the following bitsPerSample is read in as 16 bits
            if ((ushort)WAVEFORMAT.WAVE_FORMAT_PCM == fFormat.wFormatTag)
                fFormat.dwBitsPerSample = fReader.ReadUInt16();

            return fFormat;
        }

        public RIFFDataChunk ReadDataChunk()
        {
            RIFFDataChunk data = new RIFFDataChunk();

            data.chunkID = FOURCC.data;
            data.dwChunkSize = fReader.ReadUInt32();
            //ReadUInt32 is the most important function here.

            //Once we've read in the ChunkSize, 
            //we're at the start of the actual data.
            data.lFilePosition = fReader.BaseStream.Position;

            //If the fact chunk exists, we don't have to calculate 
            //the number of samples ourselves.
            if (null != fFact)
                data.dwNumSamples = fFact.dwNumSamples;
            else
                data.dwNumSamples = data.dwChunkSize /
                  (fFormat.dwBitsPerSample / 8 * fFormat.wChannels);
            //The above could be written as data.dwChunkSize / format.wBlockAlign, 
            //but I want to emphasize
            //what the frames look like.

            data.dwMinLength = (data.dwChunkSize / fFormat.dwAvgBytesPerSec) / 60;
            data.dSecLength = ((double)data.dwChunkSize /
                              (double)fFormat.dwAvgBytesPerSec) -
                              (double)data.dwMinLength * 60;

            if (fFormat.dwBitsPerSample == 8)
                data.byteArray = fReader.ReadBytes((int)data.dwNumSamples);

            return data;
        }

        public long Position
        {
            get
            {
                return fReader.BaseStream.Position;
            }
        }


        /// <summary>
        /// Skip to the next chunk without reading the
        /// content of the current chunk.
        /// </summary>
        public void AdvanceToNext()
        {
            //Get next chunk offset
            long NextOffset = (long)fReader.ReadUInt32();

            //Seek to the next offset from current position
            fReader.BaseStream.Seek(NextOffset, SeekOrigin.Current);
        }
    }
}
