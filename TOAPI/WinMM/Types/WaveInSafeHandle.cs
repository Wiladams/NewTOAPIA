namespace TOAPI.WinMM
{
    using System;
    using Microsoft.Win32.SafeHandles;

    /// <summary>
    /// Encapsulates a handle to a waveIn device.
    /// </summary>
    public sealed class WaveInSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the WaveInSafeHandle class.
        /// </summary>
        public WaveInSafeHandle()
            : base(true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the WaveInSafeHandle class.
        /// </summary>
        /// <param name="tempHandle">A temporary handle from which to initialize.  
        /// The temporart handle MUST NOT be released after this instance has been created.</param>
        public WaveInSafeHandle(IntPtr tempHandle)
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
                MMSYSERROR ret = winmm.waveInClose(this);
                return ret == MMSYSERROR.MMSYSERR_NOERROR;
            }
            else
            {
                return true;
            }
        }
    }
}
