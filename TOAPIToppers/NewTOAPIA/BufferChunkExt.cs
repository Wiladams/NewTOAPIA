using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA
{
    public static class BufferChunkExt
    {
        public static bool Compare(this byte[] obj1, byte[] obj2)
        {
            bool ret = false;

            if (obj1 == null || obj2 == null)
            {
                if (obj1 == obj2)
                {
                    ret = true;
                }
            }
            else if (obj1.Length == obj2.Length)
            {
                int i = 0;

                for (; i < obj1.Length; i++)
                {
                    if (obj1[i] != obj2[i])
                    {
                        break;
                    }
                }

                if (i == obj1.Length)
                {
                    ret = true;
                }
            }

            return ret;
        }

        public static byte[] Copy(this byte[] source)
        {
            byte[] ret = null;

            if (source != null)
            {
                ret = new byte[source.Length];
                Array.Copy(source, 0, ret, 0, source.Length);
            }

            return ret;
        }

    }
}
