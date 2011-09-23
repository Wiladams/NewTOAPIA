using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    #region RtpPacketBase

    /// <summary>
    /// The standard Rtp packet
    /// 
    /// TODO - we may need to re-add support for CSRCs and HeaderExtensions, although header
    /// extensions could just as easily be implemented as payload headers
    /// </summary>
    public class RtpPacketBase : PacketBase
    {
        #region Statics

        private const int VPXCC_SIZE = 1;   // Size in bytes of V, P, X, CC fields
        private const int MPT_SIZE = 1;     // Size in bytes of M, and PT fields
        private const int SEQ_SIZE = 2;     // Size in bytes of sequence number
        private const int TS_SIZE = 4;      // Size in bytes of timestamp
        protected const int SSRC_SIZE = Rtp.SSRC_SIZE;  // Size in bytes of SSRC field

        private const int VPXCC_INDEX = 0;
        private const int MPT_INDEX = VPXCC_INDEX + VPXCC_SIZE;
        private const int SEQ_INDEX = MPT_INDEX + MPT_SIZE;
        protected const int TS_INDEX = SEQ_INDEX + SEQ_SIZE;
        protected const int SSRC_INDEX = TS_INDEX + TS_SIZE;

        internal const int RTP_HEADER_SIZE = SSRC_INDEX + SSRC_SIZE;

        /// <summary>
        ///  Cast operator for forming a BufferChunk from an RtpPacketBase.
        /// </summary>
        public static explicit operator BufferChunk(RtpPacketBase packet)
        {
            Debug.Assert(packet.buffer.Length == (packet.HeaderSize + packet.PayloadSize));
            return packet.buffer;
        }

        /// <summary>
        ///  Cast operator for forming a BufferChunk from an RtpPacketBase.
        /// </summary>
        public static explicit operator RtpPacketBase(BufferChunk buffer)
        {
            return new RtpPacketBase(buffer);
        }
        #endregion Statics

        #region Constructors

        /// <summary>
        /// Creates a max size packet
        /// </summary>
        internal RtpPacketBase()
            : this(UdpSocket.BEST_PACKET_SIZE)
        {
        }

        /// <summary>
        /// Creates a packet of the given size
        /// </summary>
        internal RtpPacketBase(int packetSize)
            :base(packetSize)
        {
        }

        /// <summary>
        /// Create a packet from an existing buffer
        /// </summary>
        /// <param name="buffer"></param>
        internal RtpPacketBase(BufferChunk buffer)
            :base(buffer)
        {
            ValidateBuffer(buffer);
        }

        /// <summary>
        /// Create a packet from an existing packet
        /// </summary>
        /// <param name="packet"></param>
        internal RtpPacketBase(RtpPacketBase packet)
            :base(packet)
        {
            //buffer = packet.buffer;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Marker reserved for payload/protocol specific information.
        /// </summary>
        public bool Marker
        {
            get { return ((Buffer[MPT_INDEX] & 128) == 128); }

            set
            {
                if (value)
                {
                    // Set it
                    Buffer[MPT_INDEX] |= (byte)(128);
                }
                else
                {
                    // Clear the bit
                    Buffer[MPT_INDEX] ^= (byte)(Buffer[MPT_INDEX] & 128);
                }
            }
        }

        /// <summary>
        /// The type of data contained in the packet
        /// </summary>
        public PayloadType PayloadType
        {
            get { return (PayloadType)(Buffer[MPT_INDEX] & 127); }

            set
            {
                if ((int)value > 127)
                {
                    throw new ArgumentOutOfRangeException(Strings.PayloadTypeIsASevenBitStructure);
                }

                // Preserve most significant bit
                Buffer[MPT_INDEX] = (byte)(Buffer[MPT_INDEX] & 128);
                Buffer[MPT_INDEX] += (byte)value;
            }
        }

        /// <summary>
        /// Sequence number of the packet, used to keep track of the order packets were sent in
        /// 
        /// public because it is used by NetworkDumper
        ///</summary>
        public ushort Sequence
        {
            get { return Buffer.GetUInt16(SEQ_INDEX); }
            set { Buffer.SetUInt16(SEQ_INDEX, value); }
        }

        /// <summary>
        /// According to the spec - timestamp is the sampling instant for the first octet of the
        /// media data in a packet, and is used to schedule playout of the media data.
        /// 
        /// In our implementation, it is an incrementing counter used to group packets into a chunk
        /// </summary>
        public virtual uint TimeStamp
        {
            get { return Buffer.GetUInt32(TS_INDEX); }
            set { Buffer.SetUInt32(TS_INDEX, value); }
        }

        /// <summary>
        /// Synchronization source used to identify streams within a session
        /// 
        /// public because it is used by NetworkDumper
        /// </summary>
        public uint SSRC
        {
            get { return Buffer.GetUInt32(SSRC_INDEX); }
            set { Buffer.SetUInt32(SSRC_INDEX, value); }
        }

        public override int HeaderSize
        {
            get { return RTP_HEADER_SIZE; }
        }


        protected override void  OnReset()
        {
            // Initialize the first byte: V==2, P==0, X==0, CC==0
            Buffer[VPXCC_INDEX] = (byte)(Rtp.VERSION << 6);
        }

        #endregion Internal

        #region Private

        /// <summary>
        /// Make sure the provided buffer might be a real Rtp Packet (version == 2)
        /// </summary>
        protected override void  ValidateBuffer(BufferChunk buffer)
        {
            int version = buffer[VPXCC_INDEX] >> 6;

            if (Rtp.VERSION != version)
                throw new InvalidRtpPacketException(string.Format(CultureInfo.CurrentCulture,
                    Strings.InvalidVersion, version, Rtp.VERSION));
        }
        #endregion Private

    }
    #endregion RtpPacketBase
}
