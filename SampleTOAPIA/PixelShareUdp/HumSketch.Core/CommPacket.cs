
namespace PixelShare.Core
{
    using NewTOAPIA;
    using NewTOAPIA.Net;

    public class CommPacket : PacketBase
    {
        const int COMM_PACKET_HEADER_SIZE = 32;

        const int PACKETS_IN_FRAME_SIZE = 4;
        const int PACKET_SEQUENCE_SIZE = 4;

        const int PACKETS_IN_FRAME_INDEX = 0;
        const int PACKET_SEQUENCE_INDEX = PACKETS_IN_FRAME_INDEX + PACKETS_IN_FRAME_SIZE;

        #region Constructors
        public CommPacket()
            :base()
        {
        }

        public CommPacket(BufferChunk aChunk)
            :base(aChunk)
        {
        }
        #endregion

        public override int HeaderSize
        {
            get
            {
                return COMM_PACKET_HEADER_SIZE;
            }
        }

        public int PacketsInFrame
        {
            get
            {
                return Buffer.GetInt32(PACKETS_IN_FRAME_INDEX);
            }

            set
            {
                Buffer.SetInt32(PACKETS_IN_FRAME_INDEX, value);
            }
        }

        public int PacketSequenceNumber
        {
            get
            {
                return Buffer.GetInt32(PACKET_SEQUENCE_INDEX);
            }

            set
            {
                Buffer.SetInt32(PACKET_SEQUENCE_INDEX, value);
            }
        }
    }
}
