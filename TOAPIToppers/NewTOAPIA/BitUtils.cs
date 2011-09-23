namespace NewTOAPIA
{
    /// <summary>
    /// The BitUtils class provides some bit manipulation routines that are common
    /// for Windows programming.
    /// </summary>
    public static class BitUtils
    {
        /// <summary>
        /// Given a 32-bit value, return the HIGHest 16 bits as a short
        /// integer.
        /// </summary>
        /// <param name="n">The 32-bit input value who's bits will be filtered.</param>
        /// <returns>A 16-bit value</returns>
        public static short Hiword(int value)
        {
            return (short)((value >> 16) & 0xffff);
        }

        /// <summary>
        /// Given a 32-bit value, return the LOWWORD 16 bits as a short
        /// integer.
        /// </summary>
        /// <param name="n">The 32-bit input value who's bits will be filtered.</param>
        /// <returns>A 16-bit value</returns>
        public static short Loword(int value)
        {
            return (short)(value & 0xffff);
        }
    }
}