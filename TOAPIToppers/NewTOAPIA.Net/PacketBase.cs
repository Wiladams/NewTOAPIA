using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace NewTOAPIA.Net
{
    /// <summary>
    /// The base class for network packet data.
    /// 
    /// <remarks>
    /// A network packet consists of a payload, and a header.  The implementation
    /// is based on the BufferChunk object.  This additional layer of abstraction
    /// allows a subclass to implement packets that represent a specific protocol, such
    /// as RTP, without having to subclass the BufferChunk object.
    /// Additionally, the Packetizer object deals with packets in the abstract,
    /// so, this class serves as the base class for all streams that are to be packetized.
    /// </remarks>
    /// </summary>
    public class PacketBase
    {
        #region Members

        /// <summary>
        ///  Buffer to contain the raw data
        /// </summary>
        protected BufferChunk buffer;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Creates a max size packet
        /// </summary>
        public PacketBase()
            : this(UdpSocket.BEST_PACKET_SIZE)
        {
        }

        /// <summary>
        /// Creates a packet of the given size
        /// </summary>
        public PacketBase(int packetSize)
        {
            buffer = new BufferChunk(new byte[packetSize]);
            Reset();
        }

        /// <summary>
        /// Create a packet from an existing buffer
        /// </summary>
        /// <param name="buffer"></param>
        public PacketBase(BufferChunk buffer)
        {
            ValidateBuffer(buffer);

            this.buffer = buffer;
        }

        /// <summary>
        /// Create a packet from an existing packet
        /// </summary>
        /// <param name="packet"></param>
        public PacketBase(PacketBase packet)
        {
            buffer = packet.buffer;
        }


        #endregion

        #region Public Properties

        /// <summary>
        /// For the packet, how many bytes are header information.
        /// <remarks>Subclasses should override this.</remarks>
        /// </summary>
        public virtual int HeaderSize
        {
            get { return 0; }
        }


        public BufferChunk Buffer
        {
            get { return buffer; }
        }

        /// <summary>
        /// Payload data of the RtpPacket
        /// </summary>
        public BufferChunk Payload
        {
            set
            {
                // Make sure they haven't tried to add more data than we can handle
                if (value.Length > MaxPayloadSize)
                {
                    throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture,
                        Strings.ValueMaximumPayload, value.Length, MaxPayloadSize));
                }

                // Reset Buffer to just after the header because packets are re-used and so that
                // operator+ works properly when copying the payload
                buffer.Reset(0, HeaderSize);
                buffer += value;
            }
            get
            {
                return buffer.Peek(HeaderSize, PayloadSize);
            }
        }

        public virtual int PayloadSize
        {
            get
            {
                int size = buffer.Length - HeaderSize;

                Debug.Assert(size >= 0);

                return size;
            }
            set
            {
                buffer.Reset(0, HeaderSize + value);
            }
        }

        /// <summary>
        /// How much payload data can this packet accept
        /// 
        /// Be sure and set all of the header information before making this call otherwise it will
        /// be incorrect.
        /// </summary>
        public int MaxPayloadSize
        {
            get { return buffer.Buffer.Length - HeaderSize; }
        }

#endregion

        #region Public Methods
        public void AppendPayload(Int32 data)
        {
            buffer += data;
        }

        public void AppendPayload(BufferChunk data)
        {
            buffer += data;
        }

        public void AppendPayload(IntPtr ptr, int length)
        {
            buffer.CopyFrom(ptr, length);
        }

        /// <summary>
        /// Release the BufferChunk held by this packet so it can be reused outside the scope of this packet.
        /// </summary>
        /// <returns></returns>
        public BufferChunk ReleaseBuffer()
        {
            BufferChunk ret = buffer;
            buffer = null;

            return ret;
        }


        public virtual void Reset()
        {
            buffer.Reset(0, HeaderSize);

            OnReset();
        }

        protected virtual void OnReset()
        {
        }

        #endregion Internal

        #region Private

        /// <summary>
        /// Make sure the provided buffer might be a real Rtp Packet (version == 2)
        /// </summary>
        protected virtual void ValidateBuffer(BufferChunk buffer)
        {
        }
        #endregion Private

        #region Statics


        /// <summary>
        ///  Cast operator for forming a BufferChunk from an PacketBase.
        /// </summary>
        public static explicit operator BufferChunk(PacketBase packet)
        {
            Debug.Assert(packet.buffer.Length == (packet.HeaderSize + packet.PayloadSize));
            return packet.buffer;
        }


        /// <summary>
        ///  Cast operator for forming a BufferChunk from an RtpPacketBase.
        /// </summary>
        public static explicit operator PacketBase(BufferChunk buffer)
        {
            return new PacketBase(buffer);
        }


        #endregion Statics

    }
}
