using System;
using System.Text;

namespace TOAPI.Types
{
    ///* standard four character codes */
    public class FOURCC
    {
        public const UInt32 RIFF = 0x46464952;
        public static UInt32 LIST = MakeFourCC('L', 'I', 'S', 'T');

        // four character codes used to identify standard built-in I/O procedures
        public static UInt32 DOS = MakeFourCC('D', 'O', 'S', ' ');
        public static UInt32 MEM = MakeFourCC('M', 'E', 'M', ' ');

        public static UInt32 vidc = MakeFourCC('v', 'i', 'd', 'c');
        public static UInt32 vcap = MakeFourCC('v', 'c', 'a', 'p');

        // For .wav files
        public const UInt32 fmt = 0x20746d66;
        public const UInt32 fact = 0x74636166;
        public const UInt32 data = 0x61746164;
        public const UInt32 WAVE = 0x45564157;

        public static UInt32 MakeFourCC(byte[] bytes)
        {
            UInt32 result = ((UInt32)bytes[0] | ((UInt32)bytes[1] << 8) |
                    ((UInt32)bytes[2] << 16) | ((UInt32)bytes[3] << 24));

            return result;
        }

        public static UInt32 MakeFourCC(char ch0, char ch1, char ch2, char ch3)
        {
            UInt32 result = ((UInt32)(byte)(ch0) | ((UInt32)(byte)(ch1) << 8) |
                    ((UInt32)(byte)(ch2) << 16) | ((UInt32)(byte)(ch3) << 24));

            return result;
        }

        public static string FourCCToString(uint fourcc)
        {
            char c1 = (char)(fourcc & 0x000000ff);
            char c2 = (char)((fourcc & 0x0000ff00) >> 8);
            char c3 = (char)((fourcc & 0x00ff0000) >> 16);
            char c4 = (char)((fourcc & 0xff000000) >> 24);

            StringBuilder builder = new StringBuilder(4);
            builder.AppendFormat("{0}{1}{2}{3}", c1, c2, c3, c4);

            return builder.ToString();
        }
    }
}
