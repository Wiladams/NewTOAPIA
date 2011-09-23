using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// Turns 8-bit µ-law bytes back into 16-bit PCM values.
    /// </summary>
    public class MuLawDecoder : G711AudioDecoder
    {
        /// <summary>
        /// An array where the index is the mu-law input, and the value is
        /// the 16-bit PCM result.
        /// </summary>
        private static short[] muLawToPcmMap;

        static MuLawDecoder()
        {
            muLawToPcmMap = new short[256];
            for (byte i = 0; i < byte.MaxValue; i++)
                muLawToPcmMap[i] = decode(i);
        }

        public override short[] G711ToPCMMap
        {
            get { return muLawToPcmMap; }
        }

        /// <summary>
        /// Decode one mu-law byte. For internal use only.
        /// </summary>
        /// <param name="mulaw">The encoded mu-law byte</param>
        /// <returns>A short containing the 16-bit result</returns>
        private static short decode(byte mulaw)
        {
            //Flip all the bits
            mulaw = (byte)~mulaw;

            //Pull out the value of the sign bit
            int sign = mulaw & 0x80;
            //Pull out and shift over the value of the exponent
            int exponent = (mulaw & 0x70) >> 4;
            //Pull out the four bits of data
            int data = mulaw & 0x0f;

            //Add on the implicit fifth bit (we know the four data bits followed a one bit)
            data |= 0x10;
            /* Add a 1 to the end of the data by shifting over and adding one.  Why?
             * Mu-law is not a one-to-one function.  There is a range of values that all
             * map to the same mu-law byte.  Adding a one to the end essentially adds a
             * "half byte", which means that the decoding will return the value in the
             * middle of that range.  Otherwise, the mu-law decoding would always be
             * less than the original data. */
            data <<= 1;
            data += 1;
            /* Shift the five bits to where they need to be: left (exponent + 2) places
             * Why (exponent + 2) ?
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * . 7 6 5 4 3 2 1 0 . . . . . . . <-- starting bit (based on exponent)
             * . . . . . . . . . . 1 x x x x 1 <-- our data
             * We need to move the one under the value of the exponent,
             * which means it must move (exponent + 2) times
             */
            data <<= exponent + 2;
            //Remember, we added to the original, so we need to subtract from the final
            data -= MuLawEncoder.BIAS;
            //If the sign bit is 0, the number is positive. Otherwise, negative.
            return (short)(sign == 0 ? data : -data);
        }
    }
}
