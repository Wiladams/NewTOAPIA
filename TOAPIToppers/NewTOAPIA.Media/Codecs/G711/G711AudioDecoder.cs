using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media
{
    abstract public class G711AudioDecoder
    {
        // The subclasser must implement this property
        abstract public short[] G711ToPCMMap { get; }

        /// <summary>
        /// Decode one mu-law byte
        /// </summary>
        /// <param name="mulaw">The encoded mu-law byte</param>
        /// <returns>A short containing the 16-bit result</returns>
        public short Decode(byte mulaw)
        {
            return G711ToPCMMap[mulaw];
        }

        /// <summary>
        /// Decode an array of mu-law encoded bytes
        /// </summary>
        /// <param name="data">An array of mu-law encoded bytes</param>
        /// <returns>An array of shorts containing the results</returns>
        public short[] Decode(byte[] data)
        {
            int size = data.Length;
            short[] decoded = new short[size];
            for (int i = 0; i < size; i++)
                decoded[i] = G711ToPCMMap[data[i]];
            return decoded;
        }

        /// <summary>
        /// Decode an array of mu-law encoded bytes
        /// </summary>
        /// <param name="data">An array of mu-law encoded bytes</param>
        /// <param name="decoded">An array of shorts containing the results</param>
        /// <remarks>Same as the other method that returns an array of shorts</remarks>
        public void Decode(byte[] data, out short[] decoded)
        {
            int size = data.Length;
            decoded = new short[size];
            for (int i = 0; i < size; i++)
                decoded[i] = G711ToPCMMap[data[i]];
        }

        /// <summary>
        /// Decode an array of mu-law encoded bytes
        /// </summary>
        /// <param name="data">An array of mu-law encoded bytes</param>
        /// <param name="decoded">An array of bytes in Little-Endian format containing the results</param>
        public void Decode(byte[] data, out byte[] decoded)
        {
            int size = data.Length;
            decoded = new byte[size * 2];
            for (int i = 0; i < size; i++)
            {
                //First byte is the less significant byte
                decoded[2 * i] = (byte)(G711ToPCMMap[data[i]] & 0xff);
                //Second byte is the more significant byte
                decoded[2 * i + 1] = (byte)(G711ToPCMMap[data[i]] >> 8);
            }
        }
    }
}
