using System;
using System.Runtime.InteropServices;

using TOAPI.WinMM;

namespace NewTOAPIA.Media.WinMM
{
    public class MediaSample : IDisposable
    {
        public WAVEHDR fHeader;
        GCHandle fHeaderHandle;

        public MediaSample(int bufferSize)
        {
            // Create the header
            fHeader = new WAVEHDR(bufferSize);

            // Get a pinned handle on the header
            fHeaderHandle = GCHandle.Alloc(fHeader, GCHandleType.Pinned);
        }


        public WAVEHDR GetHeader()
        {
            return fHeader;
        }

        public IntPtr GetHeaderPointer()
        {
            return fHeaderHandle.AddrOfPinnedObject();
        }

        /// <summary>
        /// Indicates whether the Done flag is set on this buffer
        /// </summary>
        public bool Done
        {
            get
            {
                return ((int)fHeader.dwFlags & winmm.WHDR_DONE) == winmm.WHDR_DONE;
            }
        }


        #region IDisposable
        /// <summary>
        /// Finalizer for this wave buffer
        /// </summary>
        ~MediaSample()
        {
            Dispose(false);
            System.Diagnostics.Debug.Assert(true, "MediaSample was not disposed");
        }

        /// <summary>
        /// Releases resources held by this WaveBuffer
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        /// <summary>
        /// Releases resources held by this WaveBuffer
        /// </summary>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }

            if (fHeaderHandle.IsAllocated)
                fHeaderHandle.Free();

        }

        #endregion

    }

}
