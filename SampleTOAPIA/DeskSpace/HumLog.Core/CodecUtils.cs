using System;

using NewTOAPIA;

namespace HumLog
{
    public class CodecUtils
    {
        #region Packing Utilities
        public static void Pack(BufferChunk chunk, Guid uniqueID)
        {
            //chunk += uniqueID.ToString();
            chunk += (int)16; // For size of the following array
            chunk += uniqueID.ToByteArray();
        }

        public static void Pack(BufferChunk aChunk, int x, int y)
        {
            aChunk += x;
            aChunk += y;
        }

        public static void Pack(BufferChunk aChunk, int left, int top, int right, int bottom)
        {
            aChunk += left;
            aChunk += top;
            aChunk += right;
            aChunk += bottom;
        }
        #endregion

        #region Unpacking Utilities
        public static Guid UnpackGuid(BufferChunk chunk)
        {
            int bufferLength = chunk.NextInt32(); // How many bytes did we pack
            byte[] bytes = (byte[])chunk.NextBufferChunk(bufferLength);
            Guid aGuid = new Guid(bytes);

            return aGuid;
        }
        #endregion
    }
}
