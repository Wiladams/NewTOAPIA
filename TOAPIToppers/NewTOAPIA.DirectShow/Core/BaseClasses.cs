using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.DirectShow.Core
{
    /// <summary>
    /// Delegate signature to notify of a new surface
    /// </summary>
    /// <param name="sender">The sender of the event</param>
    /// <param name="pSurface">The pointer to the D3D surface</param>
    public delegate void NewAllocatorSurfaceDelegate(object sender, IntPtr pSurface);

    /// <summary>
    /// The custom allocator interface.  All custom allocators need
    /// to implement this interface.
    /// </summary>
    //public interface ICustomAllocator : IDisposable
    //{
    //    /// <summary>
    //    /// Invokes when a new frame has been allocated
    //    /// to a surface
    //    /// </summary>
    //    event Action NewAllocatorFrame;

    //    /// <summary>
    //    /// Invokes when a new surface has been allocated
    //    /// </summary>
    //    event NewAllocatorSurfaceDelegate NewAllocatorSurface;
    //}

}
