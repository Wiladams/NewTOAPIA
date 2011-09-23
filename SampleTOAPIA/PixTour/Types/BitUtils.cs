	public class BitUtils
	{
		// A couple of utility routines.
		// This takes a presumably 32-bit value, and returns 
		// the High 16 bits of that value.
		public static ushort HIWORD(int n)
		{
			return (ushort)((n >> 16) & 0xffff);
		}

		// This takes a presumably 32-bit value, and returns 
		// the Low 16 bits of that value.
		public static ushort LOWORD(int n)
		{
			return (ushort)(n & 0xffff);
		}

        public static ushort LOWORD(uint n)
        {
            return (ushort)(n & 0xffff);
        }
    }
