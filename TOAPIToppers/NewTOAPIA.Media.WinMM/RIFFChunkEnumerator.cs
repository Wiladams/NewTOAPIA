namespace NewTOAPIA.Media.WinMM
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using TOAPI.Types;
    using TOAPI.WinMM;

    public class RIFFChunkEnumerator : IEnumerable<RIFFChunk>
    {
        Stream fInputStream;
        BinaryReader fReader;

        public RIFFChunkEnumerator(Stream inStream)
        {
            fInputStream = inStream;
            fReader = new BinaryReader(inStream);
        }

        public UInt32 ReadFourCC()
        {
            byte[] bytes = fReader.ReadBytes(4);
            return FOURCC.MakeFourCC(bytes);
        }

        public IEnumerator<RIFFChunk> GetEnumerator()
        {
            while (true)
            {
                yield return new RIFFChunk();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<RIFFChunk>)this).GetEnumerator();
        }

    }
}