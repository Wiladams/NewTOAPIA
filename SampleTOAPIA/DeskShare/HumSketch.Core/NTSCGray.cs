using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;

namespace NewTOAPIA
{
    public class NTSCGray
    {
        // Based on old NTSC
        //static float redcoeff = 0.299f;
        //static float greencoeff = 0.587f;
        //static float bluecoeff = 0.114f;
        
        // New CRT and HDTV phosphors
        static float redcoeff = 0.2225f;
        static float greencoeff = 0.7154f;
        static float bluecoeff = 0.0721f;

        static byte[] redfactor;
        static byte[] bluefactor;
        static byte[] greenfactor;

        static NTSCGray()
        {
            redfactor = new byte[256];
            greenfactor = new byte[256];
            bluefactor = new byte[256];

            int counter=0;

            for (counter = 0; counter < 256; counter++)
            {
                redfactor[counter] = (byte)Math.Min((byte)56,(byte)Math.Floor((counter * redcoeff)+0.5f));
                greenfactor[counter] = (byte)Math.Min((byte)181,(byte)Math.Floor((counter * greencoeff)+0.5f));
                bluefactor[counter] = (byte)Math.Min((byte)18, (byte)Math.Floor((counter * bluecoeff)+0.5f));
            }
        }

        /// <summary>
        /// Convert three byte values of Red, Green, Blue, into their luminace values.
        /// The returned result is guaranteed to be in the range of [0 - 255] based
        /// on the calculation of the factor arrays.
        /// </summary>
        /// <param name="red">A byte of red</param>
        /// <param name="green">A byte of green</param>
        /// <param name="blue">A byte of blue</param>
        /// <returns></returns>
        public static byte ToLuminance(byte red, byte green, byte blue)
        {
            byte lum = (byte)(redfactor[red] + greenfactor[green] + bluefactor[blue]);

            return lum;
        }

        public static byte ToLuminance(BGRb src)
        {
            byte lum = (byte)(redfactor[src.Red] + greenfactor[src.Green] + bluefactor[src.Blue]);

            return lum;
        }

    }
}
