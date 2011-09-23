
namespace NewTOAPIA.Net
{
    using System;
    using System.Collections.Generic;

    public class PacketPool<T>
        where T : PacketBase, new()
    {
        #region Statics

        // The growth of the chunk is limited to ~= 48MB
        //
        // 1 packet (PoolInitialSize) Doubled (PoolGrowthFactor) 15 times (PoolMaxGrows)
        // 1 * 2^15 = 32768 packets * ~1.5 K per buffer ~= 48MB of data.

        /// <summary>
        /// Initial number of pre-allocated packets for sending data
        /// </summary>
        private const int PoolInitialSizeC = 1;

        /// <summary>
        /// Multiplier - amount of packets to grow the chunk by
        /// </summary>
        private const int PoolGrowthFactorC = 2;

        /// <summary>
        /// How many times do we want the chunk to grow by GrowthFactor before we consider
        /// the situation to be out of control (unbounded)
        /// </summary>
        private const int PoolGrowsMaxC = 15;

        #endregion

        private List<T> fPackets = new List<T>();
        private int poolLength;
        private uint packetsInFramePool;

        private ushort poolGrows;
        private ushort poolInitialSize = PoolInitialSizeC;
        private ushort poolGrowsMax = PoolGrowsMaxC;
        private ushort poolGrowthFactor = PoolGrowthFactorC;

        #region Constructors
        public PacketPool()
            : this(PoolInitialSizeC)
        {
        }

        public PacketPool(int initialPacketsInPool)
        {
            for (int i = 0; i < initialPacketsInPool; i++)
                fPackets.Add(new T());
            poolLength = initialPacketsInPool;
        }
        #endregion

        #region Indexer
        public T this[int index]
        {
            get
            {
                return fPackets[index];
            }

            set
            {
                fPackets[index] = value;
            }
        }
        #endregion

        #region Properties
        public int Length
        {
            get { return fPackets.Count; }
        }

        public uint PacketsInFramePool
        {
            get { return packetsInFramePool; }
            set { packetsInFramePool = value; }
        }
        #endregion

        public void GrowPool(uint packets)
        {
            // Use a local so that we don't change the member variable until everything is committed
            int localPoolLength = fPackets.Count;

            // Startup condition
            if (localPoolLength == 0)
            {
                localPoolLength = poolInitialSize;
            }

            // Determine amount of growth necessary
            while (packets > localPoolLength)
            {
                // Increment the number of times the pool has grown
                poolGrows++;

                if (poolGrows > poolGrowsMax)
                {
                    throw new Exception("Exceeded Packet Pool Growth Maximum");
                }

                localPoolLength *= poolGrowthFactor;
            }

            // Add more blank packets onto the end
            int packetDiff = localPoolLength - fPackets.Count;
            for (int i = 0; i < packetDiff; i++)
                fPackets.Add(new T());

            poolLength = localPoolLength;
        }
    }
}