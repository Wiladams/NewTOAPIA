	public class BitUtils
	{
		// A couple of utility routines.

        // This takes a 32-bit value, and returns 
		// the High 16 bits of that value.
		public static short HIWORD(int n)
		{
			return (short)((n >> 16) & 0xffff);
		}

        // This takes a 32-bit value, and returns 
		// the Low 16 bits of that value.
		public static short LOWORD(int n)
		{
			return (short)(n & 0xffff);
		}
    }
