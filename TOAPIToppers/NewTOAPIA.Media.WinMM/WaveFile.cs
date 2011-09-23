using System;
using System.IO;
using System.Text;

using TOAPI.Types;
//using TOAPI.AviCap32;

namespace NewTOAPIA.Media.WinMM
{
    public class WaveFile
    {
        public string fFileName;
        public RIFFHeader fHeader;
        public RIFFFormatChunk fFormat;
        public RIFFFactChunk fFact;
        public RIFFDataChunk fData;
        public BinaryReader fBinaryReader;

        public WaveFile(string filename)
        {
            fFileName = filename;
            RIFFStreamReader fReader = new RIFFStreamReader(filename);
            fBinaryReader = fReader.BinaryReader;
            fHeader = fReader.ReadHeader();

            // Read the chunks from the file
            uint chunkName = 0;
            while (fReader.Position < (long) fHeader.dwFileLength)
            {
                chunkName = fReader.ReadFourCC();

                switch (chunkName)
                {
                    case FOURCC.fmt:
                        fFormat = fReader.ReadFormatChunk();
                        if (fReader.Position + fFormat.dwChunkSize == fHeader.dwFileLength)
                            return;
                    break;

                    case FOURCC.fact:

                        fFact = fReader.ReadFactChunk();
                        if (fReader.Position + fFact.dwChunkSize == fHeader.dwFileLength)
                            return;
                    break;

                    case FOURCC.data:
                        fData = fReader.ReadDataChunk();
                        if (fReader.Position + fData.dwChunkSize == fHeader.dwFileLength)
                            return;
                        break;

                    default:
                        // Skip over unsupported chunks.
                        fReader.AdvanceToNext();
                        break;
                }
            }
        }

        public BinaryReader BinaryReader
        {
            get { return fBinaryReader; }
        }
    }
}
