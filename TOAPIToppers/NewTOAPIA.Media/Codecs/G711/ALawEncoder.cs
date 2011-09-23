using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// Turns 16-bit linear PCM values into 8-bit A-law bytes.
    /// </summary>
    public class ALawEncoder : G711AudioEncoder
    {
        /// <summary>
        /// An array where the index is the 16-bit PCM input, and the value is
        /// the a-law result.
        /// </summary>
        private static byte[] gPcmToALawMap;

        #region Static Class Constructor
        static ALawEncoder()
        {
            gPcmToALawMap = new byte[65536];
            for (int i = short.MinValue; i <= short.MaxValue; i++)
                gPcmToALawMap[(i & 0xffff)] = encode(i);
        }
        #endregion

        #region Constructor
        public ALawEncoder()
        {
        }
        #endregion

        #region Property
        public override byte[] PCMToG711Map
        {
            get { return gPcmToALawMap; }
        }
        #endregion

        /// <summary>
        /// Encode one a-law byte from a 16-bit signed integer. Internal use only.
        /// </summary>
        /// <param name="pcm">A 16-bit signed pcm value</param>
        /// <returns>A a-law encoded byte</returns>
        private static byte encode(int pcm)
        {
            //Get the sign bit.  Shift it for later use without further modification
            int sign = (pcm & 0x8000) >> 8;

            //If the number is negative, make it positive (now it's a magnitude)
            if (sign != 0)
                pcm = -pcm;

            //The magnitude must fit in 15 bits to avoid overflow
            if (pcm > G711AudioEncoder.Max_15Bit)
                pcm = G711AudioEncoder.Max_15Bit;

            /* Finding the "exponent"
             * Bits:
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 7 6 5 4 3 2 1 0 0 0 0 0 0 0 0
             * We want to find where the first 1 after the sign bit is.
             * We take the corresponding value from the second row as the exponent value.
             * (i.e. if first 1 at position 7 -> exponent = 2)
             * The exponent is 0 if the 1 is not found in bits 2 through 8.
             * This means the exponent is 0 even if the "first 1" doesn't exist.
             */
            int exponent = 7;
            //Move to the right and decrement exponent until we hit the 1 or the exponent hits 0
            for (int expMask = 0x4000; (pcm & expMask) == 0 && exponent > 0; exponent--, expMask >>= 1) { }

            /* The last part - the "mantissa"
             * We need to take the four bits after the 1 we just found.
             * To get it, we shift 0x0f :
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 0 0 0 0 0 1 . . . . . . . . . (say that exponent is 2)
             * . . . . . . . . . . . . 1 1 1 1
             * We shift it 5 times for an exponent of two, meaning
             * we will shift our four bits (exponent + 3) bits.
             * For convenience, we will actually just shift the number, then AND with 0x0f. 
             * 
             * NOTE: If the exponent is 0:
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * S 0 0 0 0 0 0 0 Z Y X W V U T S (we know nothing about bit 9)
             * . . . . . . . . . . . . 1 1 1 1
             * We want to get ZYXW, which means a shift of 4 instead of 3
             */
            int mantissa = (pcm >> ((exponent == 0) ? 4 : (exponent + 3))) & 0x0f;

            //The a-law byte bit arrangement is SEEEMMMM (Sign, Exponent, and Mantissa.)
            byte alaw = (byte)(sign | exponent << 4 | mantissa);

            //Last is to flip every other bit, and the sign bit (0xD5 = 1101 0101)
            return (byte)(alaw ^ 0xD5);
        }

    }
}
