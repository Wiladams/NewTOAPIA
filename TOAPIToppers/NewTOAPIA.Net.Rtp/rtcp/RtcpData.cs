
namespace NewTOAPIA.Net.Rtp
{
    using System;
    using System.Runtime.Serialization;
    using System.Collections;
    using System.Diagnostics;
    using System.Globalization;
    using System.Reflection;

    internal interface IRtcpData
    {
        void WriteDataToBuffer(BufferChunk buffer);
        void ReadDataFromBuffer(BufferChunk buffer);
        int Size {get;}
    }

    [Serializable]
    public class RtcpData : IRtcpData
    {
        public virtual void WriteDataToBuffer(BufferChunk buffer)
        {
        }

        public virtual void ReadDataFromBuffer(BufferChunk buffer)
        {
        }

        public virtual int Size { get; private set; }

    }


    /// <summary>
    /// Structure containing a SenderReport, See RFC 3550 for details of the data it contains
    /// </summary>
    public class SenderReport : IRtcpData
    {
        #region Statics

        // ntpTS(8), rtpTS(4), packets(4), octets(4)
        public const int SIZE = 20;

        #endregion Statics

        #region Members

        private ulong ntpTS; // 8 bytes
        private uint rtpTS;
        private uint packets;
        private uint octets;

        #endregion Members

        #region Methods
        
        public ulong Time
        {
            get{return ntpTS;}
            set{ntpTS = value;}
        }

        public uint TimeStamp
        {
            get{return rtpTS;}
            set{rtpTS = value;}
        }

        public uint PacketCount
        {
            get{return packets;}
            set{packets = value;}
        }

        public uint BytesSent
        {
            get{return octets;}
            set{octets = value;}
        }


        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "SenderReport [ Time := {0} TimeStamp := {1} " + 
                "PacketCount := {2} BytesSent := {3} ]", Time, TimeStamp, PacketCount, BytesSent);
        }

    
        public void ReadDataFromBuffer(BufferChunk buffer)
        {
            Time = buffer.NextUInt64();
            TimeStamp = buffer.NextUInt32();
            PacketCount = buffer.NextUInt32();
            BytesSent = buffer.NextUInt32();
        }

        public void WriteDataToBuffer(BufferChunk buffer)
        {
            buffer += Time;
            buffer += TimeStamp;
            buffer += PacketCount;
            buffer += BytesSent;
        }

        public int Size
        {
            get{return SIZE;}
        }
    
        #endregion Methods
    }


    /// <summary>
    /// Structure containing a ReceiverReport, see RFC 3550 for details on the data it contains
    /// </summary>
    public class ReceiverReport : IRtcpData
    {
        #region Statics

        // ssrc(4) + fractionLost(1) + packetsLost(3) + seq(4) + jitter(4) + lsr(4) + dlsr(4)
        public const int SIZE = 24;

        private const int MIN_PACKETS_LOST = -8388607; // 0xFF800001
        private const int MAX_PACKETS_LOST =  8388607; // 0x007FFFFF 

        #endregion Statics

        #region Members

        private uint ssrc;
        private byte fractionLost;
        private int  packetsLost;
        private uint seq;
        private uint jitter;
        private uint lsr;
        private uint dlsr;

        #endregion Members

        #region Methods

        #region Public

        public uint SSRC
        {
            get{return ssrc;}
            set{ssrc = value;}
        }

        public byte FractionLost
        {
            get{return fractionLost;}
            set{fractionLost = value;}
        }

    
        /// <summary>
        /// A 24 bit signed integer - we handle conversion between 24 and 32 bits
        /// 
        /// According to Colin's book, pg 102 - "The field saturates at the maximum positive value
        /// of 0x7FFFFF if more packets than that are lost during the session"
        /// </summary>
        public int PacketsLost
        {
            get{return packetsLost;}
            set
            {
                // Make sure the 32 bit value fits within the 24 bit container
                if(value < MIN_PACKETS_LOST)
                {
                    value = MIN_PACKETS_LOST;
                }
                else if(value > MAX_PACKETS_LOST)
                {
                    value = MAX_PACKETS_LOST;
                }

                packetsLost = value;
            }
        }

        public uint ExtendedHighestSequence
        {
            get{return seq;}
            set{seq = value;}
        }

        public uint Jitter
        {
            get{return jitter;}
            set{jitter = value;}
        }

        public uint LastSenderReport
        {
            get{return lsr;}
            set{lsr = value;}
        }

        public uint DelaySinceLastSenderReport
        {
            get{return dlsr;}
            set{dlsr = value;}
        }

        
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "ReceiverReport [ SSRC := {0} FractionLost := {1} " + 
                "PacketsLost := {2} SequenceReceiver := {3} Jitter := {4} LastSenderReport := {5} " + 
                "DelaySinceLastSenderReport := {6} ]", SSRC, FractionLost, PacketsLost, ExtendedHighestSequence, 
                Jitter, LastSenderReport, DelaySinceLastSenderReport);
        }

        
        public void ReadDataFromBuffer(BufferChunk buffer)
        {
            SSRC = buffer.NextUInt32();
            FractionLost = buffer.NextByte();
            PacketsLost = ThreeBytesToInt(buffer.NextBufferChunk(3));
            ExtendedHighestSequence = buffer.NextUInt32();
            Jitter = buffer.NextUInt32();
            LastSenderReport = buffer.NextUInt32();
            DelaySinceLastSenderReport = buffer.NextUInt32();
        }

        public void WriteDataToBuffer(BufferChunk buffer)
        {
            buffer += SSRC;
            buffer += FractionLost;
            buffer += IntToThreeBytes(PacketsLost);
            buffer += ExtendedHighestSequence;
            buffer += Jitter;
            buffer += LastSenderReport;
            buffer += DelaySinceLastSenderReport;
        }


        public int Size
        {
            get{return SIZE;}
        }

        
        #endregion Public

        #region Private

        /// <summary>
        /// Converts the least significant 3 bytes of an integer into a byte[]
        /// </summary>
        /// <param name="data">Must be in big-endian format!</param>
        /// <returns></returns>
        private static BufferChunk IntToThreeBytes(int data)
        {
            BufferChunk ret = new BufferChunk(3);
        
            // We don't want the most significant byte, which due to the big-endianness conversion
            // now occupies byte 1 (of our little endian architecture)
            ret += (byte)(data >> 1 * 8);
            ret += (byte)(data >> 2 * 8);
            ret += (byte)(data >> 3 * 8);

            return ret;
        }

    
        /// <summary>
        /// Converts 3 bytes into a big-endian integer
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Big-endian formatted integer</returns>
        private static int ThreeBytesToInt(BufferChunk data)
        {
            int ret = 0;

            ret += (data.NextByte() << 1 * 8);
            ret += (data.NextByte() << 2 * 8);
            ret += (data.NextByte() << 3 * 8);

            // If the 8th bit of the second byte (what will be the 24th bit)
            // is turned on, the value is signed, so sign extend our integer
            if(((byte)(ret >> 1 * 8) & 0x80) == 0x80)
            {
                ret += 0xFF;
            }

            return ret;
        }

    
        #endregion Private

        #endregion Methods
    }


}
