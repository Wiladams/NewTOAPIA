using System;


namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// This is the base exception from which all others inherit
    /// </summary>
    public class RtpException : ApplicationException
    {
        public RtpException(){}
        public RtpException(string message) : base(message){}
        public RtpException(string message, Exception inner) : base(message, inner){}
    }

    /// <summary>
    /// This exception is thrown when the RtpListener runs out of buffers
    /// </summary>
    public class PoolExhaustedException : RtpException
    {
        public PoolExhaustedException(){}
        public PoolExhaustedException(string message) : base(message){}
        public PoolExhaustedException(string message, Exception inner) : base(message, inner){}
    }

    /// <summary>
    /// This exception is thrown when a chunk that is too large is sent or received
    /// </summary>
    public class FrameTooLargeException : RtpException
    {
        public FrameTooLargeException(){}
        public FrameTooLargeException(string message) : base(message){}
        public FrameTooLargeException(string message, Exception inner) : base(message, inner){}
    }


    /// <summary>
    /// This exception is thrown when a packet with an incorrect timestamp is added to a chunk
    /// </summary>
    public class IncorrectTimestampException : RtpException
    {
        public IncorrectTimestampException(){}
        public IncorrectTimestampException(string message) : base(message){}
        public IncorrectTimestampException(string message, Exception inner) : base(message, inner){}
    }


    /// <summary>
    /// This exception is thrown when the NextFrame method is unblocked from a manual call or the
    /// Dispose method.
    /// </summary>
    public class NextFrameUnblockedException : RtpException
    {
        public NextFrameUnblockedException(){}
        public NextFrameUnblockedException(string message) : base(message){}
        public NextFrameUnblockedException(string message, Exception inner) : base(message, inner){}
    }

    #region Exception Classes
    /// <summary>
    /// OutOfOrder exception is thrown when issues are found with the Sequence or TimeStamp where they don't match up with the expected values an
    /// individual packet in a stream of Rtp packets should have.
    /// 
    /// Note that this exception is also thrown by the RtpPacket class when using the RtpPacket.Next() method.
    /// </summary>
    public class PacketOutOfSequenceException : ApplicationException
    {
        public int LostPackets = 0;
        public PacketOutOfSequenceException()
        {
        }
        public PacketOutOfSequenceException(string message)
            : base(message)
        {
        }
        public PacketOutOfSequenceException(string message, int lostPackets)
            : base(message)
        {
            LostPackets = lostPackets;
        }
        public PacketOutOfSequenceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// InvalidRtpPacket exception is thrown when an Rtp packet has invalid contents either due to an invalid Rtp header or due to unexpected
    /// data in a stream such as a HeaderExtension where none should be present or an invalid TimeStamp value in an Rtp Frame.
    /// </summary>
    /// <remarks>
    /// This can be caused by other traffic than just Rtp on an IPEndPoint or Rtp traffic from another sending program that doesn't follow the same
    /// framing rules.  It shouldn't be caused by packet data corruption on UDP streams since each UDP packet is CRC32 validated before being accepted
    /// and passed up by System.Net.Sockets.  It also should only be an issue if there is SSRC &amp; PayloadType collision between different sending
    /// applications, which should be rare if SSRCs are chosen according to the RFC 1889 specification.
    /// 
    /// Perhaps we should rename this to be consistent with some form of 'streaming/framing error'.  If we get a true 'invalid Rtp Packet' error, it's
    /// probably due to non-Rtp data being on the IP address and this should be filtered rather than Excepted.  Perhaps we should republish this an
    /// event like was done with OutOfOrder so that non-Rtp traffic could be detected and logged.
    /// </remarks>
    public class InvalidRtpPacketException : ApplicationException
    {
        public InvalidRtpPacketException()
        {
        }
        public InvalidRtpPacketException(string message)
            : base(message)
        {
        }
        public InvalidRtpPacketException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
    #endregion

}
