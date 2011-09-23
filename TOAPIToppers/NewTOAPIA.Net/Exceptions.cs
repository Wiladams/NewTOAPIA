using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net
{
    /// <summary>
    /// This exception is thrown when a duplicate packet arrives and it is not expected
    /// </summary>
    public class DuplicatePacketException : ApplicationException
    {
        public DuplicatePacketException() { }
        public DuplicatePacketException(string message) : base(message) { }
        public DuplicatePacketException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// This exception is thrown when a chunk is incomplete but someone has asked for the data
    /// </summary>
    public class FrameIncompleteException : ApplicationException
    {
        public FrameIncompleteException() { }
        public FrameIncompleteException(string message) : base(message) { }
        public FrameIncompleteException(string message, Exception inner) : base(message, inner) { }
    }
}
