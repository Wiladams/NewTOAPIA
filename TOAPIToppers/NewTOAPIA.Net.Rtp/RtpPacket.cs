using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Runtime.InteropServices;

using NewTOAPIA;

namespace NewTOAPIA.Net.Rtp
{


    /// <summary>
    /// RtpPacket is based on RFC 1889.  This class knows how to form a 
    /// byte array for sending out over the network and how to turn a byte 
    /// array into Rtp fields and a payload.
    /// 
    /// It is meant to be used as a translation mechanism from bytes to 
    /// structure and vice versa.  This is a lower level class exposed 
    /// only for use by applications that want intimate details about an 
    /// individual Rtp packet or want to provide their own transport mechanism.  
    /// 
    /// Applications that simply want to send/receive real time data over 
    /// IP Multicast should instead use RtpSender / RtpListener which handle 
    /// all aspects of network transport and framing (AKA breaking/assembling 
    /// large datasets into packet sized chunks).
    /// 
    /// There is a small amount of Rtp protocol intelligence in the class 
    /// when you use the Next methods.  
    /// The Next methods assume you are working on RtpPackets in a series 
    /// and will perform helper functions such as compare Sequence numbers 
    /// for linearness and NextPayload increments the Sequence number between 
    /// new packets.
    /// 
    /// This implementation has no support for CSRC identifiers.
    /// 
    /// 
    ///       The Rtp header has the following format:
    ///
    ///0                   1                   2                   3
    ///0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|V=2|P|X|  CC   |M|     PT      |       sequence number         |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|                           timestamp                           |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|            synchronization source (SSRC) identifier           |
    ///+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+
    ///|    contributing source (CSRC) identifiers  (if mixers used)   |
    ///|                             ....                              |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///
    /// V = Version
    /// P = Padding
    /// X = Extensions
    /// CC = Count of Contributing Sources
    /// M = Marker
    /// PT = Payload Type
    /// </summary>
    public class RtpPacket : RtpPacketBase
    {
        #region Statics

        /// <summary>
        /// We use a fixed size header extension
        /// </summary>
        private const int HEADER_EXTENSIONS_SIZE = PACKETS_IN_FRAME_SIZE +
                                                   FRAME_INDEX_SIZE +
                                                   FEC_INDEX_SIZE;

        private const int PACKETS_IN_FRAME_SIZE = 2;
        private const int FRAME_INDEX_SIZE = 2;
        private const int FEC_INDEX_SIZE = 2;

        #endregion Statics

        #region Constructors

        public RtpPacket() : base() {}

        public RtpPacket(int packetSize) : base(packetSize){}

        public RtpPacket(BufferChunk buffer) : base(buffer){}

        public RtpPacket(RtpPacketBase packet) : base(packet){}

        
        #endregion

        #region Internal

        internal ushort PacketsInFrame
        {
            get{return Buffer.GetUInt16(PacketsInFrame_Index);}
            set{Buffer.SetUInt16(PacketsInFrame_Index, value);}
        }

        internal ushort FrameIndex
        {
            get
            {
                return Buffer.GetUInt16(FrameIndex_Index);
            }
            set
            {
                Buffer.SetUInt16(FrameIndex_Index, value);
            }
        }

        internal ushort FecIndex
        {
            get
            {
                return Buffer.GetUInt16(FecIndex_Index);
            }
            set
            {
                Buffer.SetUInt16(FecIndex_Index, value);
            }
        }


        public override int HeaderSize
        {
            get
            {
                return base.HeaderSize + HEADER_EXTENSIONS_SIZE;
            }
        }


        #endregion Internal

        #region Private

        private int PacketsInFrame_Index
        {
            get{return base.HeaderSize;}
        }

        private int FrameIndex_Index
        {
            get{return PacketsInFrame_Index + PACKETS_IN_FRAME_SIZE;}
        }

        private int FecIndex_Index
        {
            get{return FrameIndex_Index + FRAME_INDEX_SIZE;}
        }

        
        #endregion Private
    }

    #region RtpPacketFec

    /// <summary>
    /// RtpPacketFec is a forward error correction packet.  It is used to provide error correction
    /// for data packets that may become lost.
    /// 
    /// It has a fixed payload type PayloadType.FEC
    /// The normal Rtp Timestamp has been repurposed in order to save bytes.  It is split into...
    /// 
    /// FecIndex - the index of this packet within the fec packet[].  The size of the fec packet[]
    /// is either determined by the constant fec ratio, or the percent coverage across a chunk.
    /// 
    /// DataRangeMin - the starting data packet sequence number for which this packet provides
    /// coverage.
    /// 
    /// PacketsInFrame - how many packets are in a chunk.  Used in the event that no data packets
    /// are received, but enough fec packets arrive to recover the data.
    /// 
    /// 0                   1                   2                   3
    /// 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|V=2|P|X|  CC   |M|    PT.FEC   |       sequence number         |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|          DataRangeMin         |         PacketsInFrame        |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|           synchronization source (SSRC) identifier            |
    ///+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
    ///|           FecIndex            |
    ///+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+
    /// </summary>
    internal class RtpPacketFec : RtpPacketBase
    {
        #region Statics

        // This is a hack.  The general idea is that we don't want to fragment an Fec Packet.
        // In order to prevent fragmentation, we have to limit an Rtp Packet to a known size, so
        // there is room for the Fec Packet overhead.  When we add support for CSRCs or Header
        // extensions, this value will be incorrect.  And we won't have allocated enough space for
        // the Fec Packet.  JVE 6/16/2004
        internal const int HEADER_SIZE_HACK = RtpPacketBase.RTP_HEADER_SIZE + 
                                              HEADER_OVERHEAD_SIZE          +
                                              CFec.SIZE_OVERHEAD;

        /// <summary>
        /// The 4 bytes of Timestamp have been re-purposed, but we needed 2 extra bytes for header
        /// </summary>
        private const int HEADER_OVERHEAD_SIZE = 2;

        #endregion Statics

        #region Constructors

        internal RtpPacketFec() : base()
        {
            PayloadType = PayloadType.FEC;
        }

        internal RtpPacketFec(RtpPacketBase packet) : base(packet)
        {
            if(PayloadType != PayloadType.FEC)
            {
                throw new ArgumentException(Strings.PacketIsNotAnFECPacket);
            }
        }

        
        #endregion Constructors

        #region Internal

        public override int HeaderSize
        {
            get
            {
                int size = base.HeaderSize;

                // See HEADER_SIZE_HACK comments
                Debug.Assert(size == RtpPacketBase.RTP_HEADER_SIZE);

                size += HEADER_OVERHEAD_SIZE;

                return size;
            }
        }

        
        public override uint TimeStamp
        {
            get
            {
                throw new InvalidOperationException(Strings.RtpPacketFecDoesNotSupportTimestamp);
            }
            set
            {
                throw new InvalidOperationException(Strings.RtpPacketFecDoesNotSupportTimestamp);
            }
        }

        public override void  Reset()
        {
            Buffer.Clear();
            base.Reset ();
            PayloadType = PayloadType.FEC;
        }


        internal ushort DataRangeMin
        {
            get{return Buffer.GetUInt16(DataRangeMin_Index);}
            set{Buffer.SetUInt16(DataRangeMin_Index, value);}
        }


        internal ushort PacketsInFrame
        {
            get{return Buffer.GetUInt16(PacketsInFrame_Index);}
            set{Buffer.SetUInt16(PacketsInFrame_Index, value);}
        }

        
        internal ushort FecIndex
        {
            get{return Buffer.GetUInt16(FecIndex_Index);}
            set{Buffer.SetUInt16(FecIndex_Index, value);}
        }

        
        #endregion Internal

        #region Private

        private int DataRangeMin_Index
        {
            get{return TS_INDEX;}
        }

        private int PacketsInFrame_Index
        {
            get{return DataRangeMin_Index + 2;}
        }

        private int FecIndex_Index
        {
            get{return SSRC_INDEX + SSRC_SIZE;}
        }
        
        #endregion Private
    }

    #endregion RtpPacketFec

}
