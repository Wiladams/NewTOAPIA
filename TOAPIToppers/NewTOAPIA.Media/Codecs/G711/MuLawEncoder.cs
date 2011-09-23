using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// Turns 16-bit linear PCM values into 8-bit µ-law bytes.
    /// </summary>
    public class MuLawEncoder : G711AudioEncoder
    {
        public const int BIAS   = 0x84;           // aka 132, or 1000 0100
        public const int MAX = G711AudioEncoder.Max_15Bit - BIAS;   // 32767 (max 15-bit integer) minus BIAS

        /// <summary>
        /// Sets whether or not all-zero bytes are encoded as 2 instead.
        /// The pcm values this concerns are in the range [32768,33924] (unsigned).
        /// </summary>
        public bool ZeroTrap
        {
            get { return (pcmToMuLawMap[33000] != 0); }
            set
            {
                byte val = (byte)(value ? 2 : 0);
                for (int i = 32768; i <= 33924; i++)
                    pcmToMuLawMap[i] = val;
            }
        }

        /// <summary>
        /// An array where the index is the 16-bit PCM input, and the value is
        /// the mu-law result.
        /// </summary>
        private static byte[] pcmToMuLawMap;

        #region Class Constructor
        static MuLawEncoder()
        {
            pcmToMuLawMap = new byte[65536];
            for (int i = short.MinValue; i <= short.MaxValue; i++)
                pcmToMuLawMap[(i & 0xffff)] = encode(i);
        }
        #endregion

        #region Constructor
        public MuLawEncoder()
        {
        }
        #endregion

        #region Property
        public override byte[] PCMToG711Map
        {
            get { return pcmToMuLawMap; }
        }
        #endregion

        /// <summary>
        /// Encode one mu-law byte from a 16-bit signed integer. Internal use only.
        /// </summary>
        /// <param name="pcm">A 16-bit signed pcm value</param>
        /// <returns>A mu-law encoded byte</returns>
        private static byte encode(int pcm)
        {
            //Get the sign bit.  Shift it for later use without further modification
            int sign = (pcm & 0x8000) >> 8;
            //If the number is negative, make it positive (now it's a magnitude)
            if (sign != 0)
                pcm = -pcm;
            //The magnitude must be less than 32635 to avoid overflow
            if (pcm > MAX) pcm = MAX;
            //Add 132 to guarantee a 1 in the eight bits after the sign bit
            pcm += BIAS;

            /* Finding the "exponent"
             * Bits:
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 7 6 5 4 3 2 1 0 . . . . . . .
             * We want to find where the first 1 after the sign bit is.
             * We take the corresponding value from the second row as the exponent value.
             * (i.e. if first 1 at position 7 -> exponent = 2) */
            int exponent = 7;
            //Move to the right and decrement exponent until we hit the 1
            for (int expMask = 0x4000; (pcm & expMask) == 0; exponent--, expMask >>= 1) { }

            /* The last part - the "mantissa"
             * We need to take the four bits after the 1 we just found.
             * To get it, we shift 0x0f :
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 0 0 0 0 0 1 . . . . . . . . . (meaning exponent is 2)
             * . . . . . . . . . . . . 1 1 1 1
             * We shift it 5 times for an exponent of two, meaning
             * we will shift our four bits (exponent + 3) bits.
             * For convenience, we will actually just shift the number, then and with 0x0f. */
            int mantissa = (pcm >> (exponent + 3)) & 0x0f;

            //The mu-law byte bit arrangement is SEEEMMMM (Sign, Exponent, and Mantissa.)
            byte mulaw = (byte)(sign | exponent << 4 | mantissa);

            //Last is to flip the bits
            return (byte)~mulaw;
        }
    }
}
