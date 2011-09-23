﻿using System;

namespace NewTOAPIA
{
    /// <summary>
    /// An abstract class that wrap around an unmanaged object
    /// </summary>
    public abstract class UnmanagedObject : DisposableObject
    {
        /// <summary>
        /// A pointer to the unmanaged object
        /// </summary>
        protected IntPtr _ptr;

        /// <summary>
        /// Pointer to the unmanaged object
        /// </summary>
        public IntPtr Ptr
        {
            get
            {
                return _ptr;
            }
        }

        /// <summary>
        /// Implicit operator for IntPtr
        /// </summary>
        /// <param name="obj">The UnmanagedObject</param>
        /// <returns>The unmanaged pointer for this object</returns>
        public static implicit operator IntPtr(UnmanagedObject obj)
        {
            return obj == null ? IntPtr.Zero : obj._ptr;
        }
    }
}
