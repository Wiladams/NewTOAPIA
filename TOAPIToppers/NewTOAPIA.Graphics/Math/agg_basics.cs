namespace NewTOAPIA.Graphics
{
    using System;

    public static class agg_basics
    {
        //----------------------------------------------------------filling_rule_e
        public enum filling_rule_e
        {
            fill_non_zero,
            fill_even_odd
        };

        public static void memcpy(Byte[] dest, int destIndex, Byte[] source, int sourceIndex, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[destIndex + i] = source[sourceIndex + i];
            }
        }

        public static void memcpy(int[] dest, int destIndex, int[] source, int sourceIndex, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[destIndex + i] = source[sourceIndex + i];
            }
        }

        public static void memmove(Byte[] dest, int destIndex, Byte[] source, int sourceIndex, int Count)
        {
            if (source != dest
                || destIndex < sourceIndex)
            {
                memcpy(dest, destIndex, source, sourceIndex, Count);
            }
            else
            {
                throw new Exception("this code needs to be tested");
                /*
                for (int i = Count-1; i > 0; i--)
                {
                    dest[destIndex + i] = source[sourceIndex + i];
                }
                 */
            }

        }

        public static void memmove(int[] dest, int destIndex, int[] source, int sourceIndex, int Count)
        {
            if (source != dest
                || destIndex < sourceIndex)
            {
                memcpy(dest, destIndex, source, sourceIndex, Count);
            }
            else
            {
                throw new Exception("this code needs to be tested");
                /*
                for (int i = Count-1; i > 0; i--)
                {
                    dest[destIndex + i] = source[sourceIndex + i];
                }
                 */
            }

        }

        public static void memset(int[] dest, int destIndex, int Val, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[i] = Val;
            }
        }

        public static void memset(Byte[] dest, int destIndex, byte ByteVal, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[i] = ByteVal;
            }
        }

        public static void MemClear(int[] dest, int destIndex, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[i] = 0;
            }
        }

        public static void MemClear(Byte[] dest, int destIndex, int Count)
        {
            for (int i = 0; i < Count; i++)
            {
                dest[i] = 0;
            }
            /*
            // dword align to dest
            while (((int)pDest & 3) != 0
                && Count > 0)
            {
                *pDest++ = 0;
                Count--;
            }

            int NumLongs = Count / 4;

            while (NumLongs-- > 0)
            {
                *((int*)pDest) = 0;

                pDest += 4;
            }

            switch (Count & 3)
            {
                case 3:
                    pDest[2] = 0;
                    goto case 2;
                case 2:
                    pDest[1] = 0;
                    goto case 1;
                case 1:
                    pDest[0] = 0;
                    break;
            }
             */
        }

        public static bool is_equal_eps(double v1, double v2, double epsilon)
        {
            return Math.Abs(v1 - v2) <= (double)(epsilon);
        }

        //------------------------------------------------------------------deg2rad
        public static double deg2rad(double deg)
        {
            return deg * Math.PI / 180.0;
        }

        //------------------------------------------------------------------rad2deg
        public static double rad2deg(double rad)
        {
            return rad * 180.0 / Math.PI;
        }

        public static int iround(double v)
        {
            return (int)((v < 0.0) ? v - 0.5 : v + 0.5);
        }

        public static int iround(double v, int saturationLimit)
        {
            if (v < (double)(-saturationLimit)) return -saturationLimit;
            if (v > (double)(saturationLimit)) return saturationLimit;
            return iround(v);
        }

        public static int uround(double v)
        {
            return (int)(uint)(v + 0.5);
        }

        public static int ufloor(double v)
        {
            return (int)(uint)(v);
        }

        public static int uceil(double v)
        {
            return (int)(uint)(Math.Ceiling(v));
        }

        //----------------------------------------------------poly_subpixel_scale_e
        // These constants determine the subpixel accuracy, to be more precise, 
        // the number of bits of the fractional part of the coordinates. 
        // The possible coordinate capacity in bits can be calculated by formula:
        // sizeof(int) * 8 - poly_subpixel_shift, i.e, for 32-bit integers and
        // 8-bits fractional part the capacity is 24 bits.
        public enum poly_subpixel_scale_e
        {
            poly_subpixel_shift = 8,                      //----poly_subpixel_shift
            poly_subpixel_scale = 1 << poly_subpixel_shift, //----poly_subpixel_scale 
            poly_subpixel_mask = poly_subpixel_scale - 1,  //----poly_subpixel_mask 
        }
    }
}