﻿

namespace NewTOAPIA
{
    using System;
    using System.Runtime.InteropServices;
    
    /// <summary>
    /// A Pinnned array of type <paramref name="T"/>
    /// </summary>
    /// <typeparam name="T">The type of the array</typeparam>
    public class PinnedArray<T> : DisposableObject
    {
        private T[] _array;
        private GCHandle _handle;

        /// <summary>
        /// Create a Pinnned array of type <paramref name="T"/>
        /// </summary>
        /// <param name="size">The size of the array</param>
        public PinnedArray(int size)
        {
            _array = new T[size];
            _handle = GCHandle.Alloc(_array, GCHandleType.Pinned);
        }

        /// <summary>
        /// Get the address of the pinned array
        /// </summary>
        /// <returns>A pointer to the address of the the pinned array</returns>
        public IntPtr AddrOfPinnedObject()
        {
            return _handle.AddrOfPinnedObject();
        }

        /// <summary>
        /// Get the array
        /// </summary>
        public T[] Array
        {
            get
            {
                return _array;
            }
        }

        /// <summary>
        /// Release the GCHandle
        /// </summary>
        protected override void ReleaseManagedResources()
        {
            _handle.Free();
        }

        /// <summary>
        /// Disposed the unmanaged data
        /// </summary>
        protected override void DisposeObject()
        {
        }
    }
}