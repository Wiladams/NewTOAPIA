using System;
using System.Runtime.InteropServices;

namespace TOAPI.WinMM
{
    /// <summary>
    /// This structure is used by the Waveform Audio routines.  It is a class instead of a structure
    /// because it is always used by reference.  
    /// 
    /// As such though, the copy assignments need to be implemented.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class WAVEHDR : IDisposable
    {
        public IntPtr lpData;     /// Pointer to data buffer
        public int dwBufferLength;         /// Length of data buffer
        public int dwBytesRecorded;        /// When used for input, number of bytes recorded
        public IntPtr dwUser;               /// User specified data
        public WAVHDRFlags dwFlags;                /// DWORD->unsigned int
        public uint dwLoops;                /// Number of times to play the loop.
        public IntPtr lpNext;     /// RESERVED - wavehdr_tag*
        public IntPtr reserved;             /// Must be Zero

        public WAVEHDR()
        {
        }

        public WAVEHDR(int desiredSize)
        {
            lpData = Marshal.AllocCoTaskMem(desiredSize);
            dwBufferLength = desiredSize;
            dwUser = IntPtr.Zero;
            dwFlags = WAVHDRFlags.None;
            dwLoops = 0;
            lpNext = IntPtr.Zero;
            reserved = IntPtr.Zero;
        }

        #region IDisposable
        public void Dispose()
        {
            if (lpData != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(lpData);
                lpData = IntPtr.Zero;
            }
        }

        #endregion
    }
}
