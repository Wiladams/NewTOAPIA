using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    //public class RtpFrameAssembler
    //{
    //    private PacketPool<RtpPacket> fPool;    // packet pool used for assembly

    //    private uint packetsInFrame;
    //    private uint timeStamp;     // An initial timestamp is set
    //    private ReturnBufferHandler returnBufferHandler;
    //    private bool allowDuplicatePackets;

    //    // Packet reception accounting
    //    int length;
    //    private ushort packetsReceived;

    //    /// <summary>
    //    /// Constructs a chunk for Receiving data
    //    /// Defaults allowDuplicatePackets to false
    //    /// </summary>
    //    public RtpFrameAssembler(uint packetsInFrame, uint timeStamp, ReturnBufferHandler returnBufferHandler) 
    //        : this(packetsInFrame, timeStamp, returnBufferHandler, false)
    //    {
    //    }

    //    /// <summary>
    //    /// Constructs a chunk for Receiving data
    //    /// </summary>
    //    public RtpFrameAssembler(uint packetsInFrame, uint timeStamp, ReturnBufferHandler returnBufferHandler, bool allowDuplicatePackets)
    //    {
    //        this.packetsInFrame = packetsInFrame;
    //        this.timeStamp = timeStamp;
    //        this.returnBufferHandler = returnBufferHandler;
    //        this.allowDuplicatePackets = allowDuplicatePackets;

    //        fPool = new PacketPool<RtpPacket>(packetsInFrame);
    //    }
    //    #region Properties

    //    public bool IsComplete
    //    {
    //        get { return packetsReceived == packetsInFrame; }
    //    }

    //    public int PacketCount
    //    {
    //        get { return (int)packetsInFrame; }
    //    }

    //    public uint TimeStamp
    //    {
    //        set { timeStamp = value; }
    //    }
    //    #endregion

    //    public BufferChunk GetAssembledFrame()
    //    {
    //        if (!IsComplete)
    //        {
    //            throw new FrameIncompleteException();
    //        }

    //        BufferChunk chunk = new BufferChunk((int)length);

    //        for (int i = 0; i < packetsInFrame; i++)
    //        {
    //            chunk += fPool[i].Payload;
    //        }

    //        return chunk;
    //    }


    //    public RtpPacket this[int index]
    //    {
    //        get
    //        {
    //            if (index >= packetsInFrame)
    //            {
    //                throw new IndexOutOfRangeException();
    //            }

    //            return fPool[index];
    //        }

    //        set
    //        {
    //            // Validate the timestamp
    //            if (value.TimeStamp != timeStamp)
    //            {
    //                throw new IncorrectTimestampException();
    //            }

    //            if (index >= packetsInFrame)
    //            {
    //                throw new IndexOutOfRangeException();
    //            }

    //            if (null == fPool[index])
    //            {
    //                packetsReceived++;
    //                length += value.PayloadSize;
    //                fPool[index] = value;
    //            }
    //            else
    //            {
    //                if (!allowDuplicatePackets)
    //                {
    //                    throw new DuplicatePacketException();
    //                }
    //            }
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        // Return packets back to where they came from
    //        for (int i = 0; i < packetsInFrame; i++)
    //        {
    //            RtpPacket packet = fPool[i];

    //            // A packet could be null if we never received a packet for that location
    //            if (packet != null)
    //            {
    //                if (null != returnBufferHandler)
    //                {
    //                    returnBufferHandler(packet.ReleaseBuffer());
    //                }

    //                fPool[i] = null;
    //            }
    //        }
    //    }

    //}
}
