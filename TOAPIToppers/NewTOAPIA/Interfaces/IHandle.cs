
namespace NewTOAPIA
{
    using System;

    /// <summary>
    /// There are many interfaces within base Windows libraries that return a general
    /// 'handle' that must be used to operate on a particular operating system object.
    /// This interface can be implemented to ensure that handle access is performed
    /// in the same way.
    /// </summary>
    public interface IHandle
    {
        /// <summary>
        /// Represents the actual handle returned by an operating system call.
        /// </summary>
        IntPtr Handle { get; }
    }
}