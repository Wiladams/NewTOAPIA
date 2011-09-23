using System;

namespace NewTOAPIA.Media
{
    /// <summary>
    /// Turns 8-bit A-law bytes back into 16-bit PCM values.
    /// </summary>
    public class ALawDecoder : G711AudioDecoder
    {
        /// <summary>
        /// An array where the index is the a-law input, and the value is
        /// the 16-bit PCM result.
        /// </summary>
        private static short[] aLawToPcmMap;

        static ALawDecoder()
        {
            aLawToPcmMap = new short[256];
            for (byte i = 0; i < byte.MaxValue; i++)
                aLawToPcmMap[i] = decode(i);
        }


        public override short[] G711ToPCMMap
        {
            get { return aLawToPcmMap; }
        }

        /// <summary>
        /// Decode one a-law byte. For internal use only.
        /// </summary>
        /// <param name="alaw">The encoded a-law byte</param>
        /// <returns>A short containing the 16-bit result</returns>
        private static short decode(byte alaw)
        {
            //Invert every other bit, and the sign bit (0xD5 = 1101 0101)
            alaw ^= 0xD5;

            //Pull out the value of the sign bit
            int sign = alaw & 0x80;
            //Pull out and shift over the value of the exponent
            int exponent = (alaw & 0x70) >> 4;
            //Pull out the four bits of data
            int data = alaw & 0x0f;

            //Shift the data four bits to the left
            data <<= 4;
            //Add 8 to put the result in the middle of the range (like adding a half)
            data += 8;

            //If the exponent is not 0, then we know the four bits followed a 1,
            //and can thus add this implicit 1 with 0x100.
            if (exponent != 0)
                data += 0x100;
            /* Shift the bits to where they need to be: left (exponent - 1) places
             * Why (exponent - 1) ?
             * 1 2 3 4 5 6 7 8 9 A B C D E F G
             * . 7 6 5 4 3 2 1 . . . . . . . . <-- starting bit (based on exponent)
             * . . . . . . . Z x x x x 1 0 0 0 <-- our data (Z is 0 only when exponent is 0)
             * We need to move the one under the value of the exponent,
             * which means it must move (exponent - 1) times
             * It also means shifting is unnecessary if exponent is 0 or 1.
             */
            if (exponent > 1)
                data <<= (exponent - 1);

            return (short)(sign == 0 ? data : -data);
        }
    }
}
