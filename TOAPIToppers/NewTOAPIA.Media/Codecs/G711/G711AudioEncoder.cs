using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// G.711 is a standard for encoding waveform audio.  The standard
    /// comes in two flavors, MuLaw, and ALaw.
    /// This class 'G711AudioEncoder' serves as an abstract base for the two
    /// variants of the standard.  As all the implementation is based on lookups
    /// into arrays, the sublcassers only need to implement the lookup array.
    /// The base class will perform the data conversions.
    /// </summary>
    abstract public class G711AudioEncoder
    {
        public const int Max_15Bit = 0x7fff;

        // The subclasser must implement this property
        abstract public byte[] PCMToG711Map { get; }

        public virtual byte Encode(int pcm)
        {
            return PCMToG711Map[pcm & 0xffff];
        }

        public virtual byte Encode(short pcm)
        {
            return PCMToG711Map[pcm & 0xffff];
        }

        public virtual byte[] Encode(int[] data)
        {
            int size = data.Length;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = Encode(data[i]);

            return encoded;
        }

        public virtual byte[] Encode(short[] data)
        {
            int size = data.Length;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = Encode(data[i]);

            return encoded;
        }

        public virtual byte[] Encode(byte[] data)
        {
            int size = data.Length / 2;
            byte[] encoded = new byte[size];
            for (int i = 0; i < size; i++)
                encoded[i] = Encode((data[2 * i + 1] << 8) | data[2 * i]);

            return encoded;
        }

        public virtual void Encode(byte[] data, byte[] target)
        {
            int size = data.Length / 2;
            for (int i = 0; i < size; i++)
                target[i] = Encode((data[2 * i + 1] << 8) | data[2 * i]);
        }
    }
}
