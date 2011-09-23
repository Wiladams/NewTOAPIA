namespace TOAPI.WinMM
{
    using System;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Encapsulates a handle to a waveOut device.
    /// </summary>
    public sealed class WaveOutSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the WaveOutSafeHandle class.
        /// </summary>
        public WaveOutSafeHandle()
            : base(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WaveOutSafeHandle class.
        /// </summary>
        /// <param name="tempHandle">A temporary handle from which to initialize.  The temporart handle MUST NOT be released after this instance has been created.</param>
        public WaveOutSafeHandle(IntPtr tempHandle)
            : base(true)
        {
            this.handle = tempHandle;
        }

        /// <summary>
        /// Releases the resuorces used by this handle.
        /// </summary>
        /// <returns>true, if disposing of the handle succeeded; false, otherwise.</returns>
        protected override bool ReleaseHandle()
        {
            if (!this.IsClosed)
            {
                MMSYSERROR ret = winmm.waveOutClose(this);
                return ret == MMSYSERROR.MMSYSERR_NOERROR;
            }
            else
            {
                return true;
            }
        }
    }
}