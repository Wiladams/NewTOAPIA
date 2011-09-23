using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Threading;


namespace NewTOAPIA.Net.Rtp
{
    #region RtpSender

    /// <summary>
    /// RtpSender is the class that applications should use to send data over the network.
    /// It handles the details of breaking data (frames) into packets
    /// </summary>
    public class RtpSender
    {
        #region Interfaces

        internal interface IRtpSession
        {
            void AddBye(uint ssrc);
            void AddSdes(uint ssrc, SdesData sdes);
            void AddSenderReport(uint ssrc, SenderReport sr);
            uint NextSSRC();
            void Dispose(uint ssrc);

            SdesData Sdes{get;}
            System.Net.IPEndPoint RtpEndPoint{get;}
            short TTL{get;}
        }

        
        #endregion Interfaces
        
        #region Statics
        
        // The growth of the chunk is limited to ~= 40MB
        //
        // 1 packet (FrameInitialSize) Doubled (FrameGrowthFactor) 15 times (FrameMaxGrows)
        // 1 * 2^15 = 32768 packets * ~1.3 K per buffer ~= 40MB of data.
        
        /// <summary>
        /// Initial number of pre-allocated packets for sending data
        /// </summary>
        private const int FrameInitialSize = 1;

        /// <summary>
        /// Multiplier - amount of packets to grow the chunk by
        /// </summary>
        private const int FrameGrowthFactor = 2;

        /// <summary>
        /// How many times do we want the chunk to grow by GrowthFactor before we consider
        /// the situation to be out of control (unbounded)
        /// </summary>
        private const int FrameMaxGrows = 15;
        
        /// <summary>
        /// Class factory for creating all RtpSenders
        /// </summary>
        internal static RtpSender CreateInstance(IRtpSession session, string name, PayloadType pt, Hashtable priExns)
        {
            if(priExns != null)
            {
                // Fec RtpSenders
                string fecData = (string)priExns[Rtcp.PEP_FEC];
                if(fecData != null)
                {
                    string[] args = fecData.Split(new char[]{':'});
                    ushort cDataPx = ushort.Parse(args[0], CultureInfo.InvariantCulture);
                    ushort cFecPx = ushort.Parse(args[1], CultureInfo.InvariantCulture);

                    // Validates in case private extensions were modified elsewhere than method below
                    if(cFecPx == 0)
                    {
                        throw new ArgumentOutOfRangeException("cFecPx", cFecPx, Strings.MustBePositive);
                    }

                    if(cDataPx == 0)
                    {
                        // Frame based Fec
                        return new RtpSenderFFec(session, name, pt, priExns, cFecPx);
                    }
                    else 
                    {
                        // Constant Fec
                        return new RtpSenderCFec(session, name, pt, priExns, cDataPx, cFecPx);
                    }
                }
            }

            // Regular old RtpSender
            return new RtpSender(session, name, pt, priExns);
        }

        internal static RtpSender CreateInstance(IRtpSession session, string name, PayloadType pt, 
            Hashtable priExns, ushort cDataPx, ushort cFecPx)
        {
            // Validate before adding to private extensions
            if(cFecPx == 0)
            {
                throw new ArgumentOutOfRangeException("cFecPx", cFecPx, Strings.MustBePositive);
            }

            // Add the relevant private extension for FEC
            Hashtable fecExns = null;
            
            if(priExns != null)
            {
                fecExns = (Hashtable)priExns.Clone();
            }
            else
            {
                fecExns = new Hashtable();
            }

            fecExns[Rtcp.PEP_FEC] = cDataPx.ToString(CultureInfo.InvariantCulture) + ":" + cFecPx.ToString(CultureInfo.InvariantCulture);

            // Call generic class factory
            return CreateInstance(session, name, pt, fecExns);
        }

        
        #endregion Statics

        #region Members
        
        /// <summary>
        /// Class that turns raw BufferChunk frames into packets
        /// </summary>
        //internal RtpFrame frame;
        internal PacketPool<RtpPacket> fPool;
        internal RtpFramePacketizer fPacketizer;

        /// <summary>
        /// A reference to the rtpSession that created this RtpSender
        /// 
        /// Used to access properties that are common between Rtp components
        /// </summary>
        private IRtpSession rtpSession = null;

        /// <summary>
        /// Rtcp SenderReport which holds how much data this sender has sent
        /// </summary>
        private SenderReport sr = new SenderReport();

        /// <summary>
        /// Payload type of this stream
        /// </summary>
        private PayloadType payloadType;

        /// <summary>
        /// Disposed marker per IDispoable pattern.
        /// </summary>
        private volatile bool disposed = false;

        /// <summary>
        /// Increment by which to increase the TimeStamp value between frames
        /// </summary>
        private ushort tsIncrement = 1;

        /// <summary>
        /// SSRC of this stream
        /// </summary>
        private uint ssrc;

        /// <summary>
        /// Track the TS of the last Rtp Frame
        /// </summary>
        private uint ts;

        /// <summary>
        /// Sequence number of the last packet sent
        /// </summary>
        private ushort seq;

        // For use by SenderReport and perf counters (including per second calculations) 
        private uint bytesSent;
        private uint bytesSentLast;

        private uint packetsSent;
        private uint packetsSentLast;

        private uint framesSent;
        private uint framesSentLast;

        private int frameLength;

        /// <summary>
        /// SdesData associated with this RtpSender
        /// We set CName and optionally Name
        /// </summary>
        protected SdesData sdes;

        /// <summary>
        /// Object we use to send our Rtp data.
        /// </summary>
        protected ISendBufferChunk rtpNetworkSender;
        
        // Hack until RtpPacket has a proper inheritance model JVE
        protected int packetSize = UdpSocket.BEST_PACKET_SIZE;

        /// <summary>
        /// Performance counter for this class
        /// </summary>
        //internal RtpSenderPC pc;
        protected object pcLock = new object();

        private int pcPxPerFrameMax;
        private int pcPxPerFrameMin = int.MaxValue;
        private int pcBytesPerFrameMax;
        private int pcBytesPerFrameMin = int.MaxValue;

        #endregion Members
        
        #region Constructors

        internal RtpSender(IRtpSession rtpSession, string friendlyName, PayloadType payloadType, Hashtable priExns)
        {
            if(Thread.CurrentThread.Name == null)
            {
                Thread.CurrentThread.Name = "RtpSender - " + friendlyName;
            }

            this.rtpSession = rtpSession;
            this.payloadType = payloadType;

            // Leave everything but CName and Name blank in order to reduce Rtcp bandwidth
            sdes = new SdesData(rtpSession.Sdes.CName, friendlyName);

            // Add private extensions
            sdes.SetPrivateExtension(Rtcp.PEP_SOURCE, Rtcp.PED_STREAM);
            sdes.SetPrivateExtension(Rtcp.PEP_PAYLOADTYPE, ((int)payloadType).ToString(CultureInfo.InvariantCulture));

            InitializeNetwork();
            InitializePacketizer();

            // This needs to be called after InitializeNetwork
            if( priExns != null )
            {
                foreach(DictionaryEntry de in priExns)
                {
                    sdes.SetPrivateExtension((string)de.Key, (string)de.Value);
                }

                string dbpString = (string)priExns[Rtcp.PEP_DBP];
                if( dbpString != null )
                    DelayBetweenPackets = short.Parse(dbpString, CultureInfo.InvariantCulture);
            }

            ResetState();
        }

        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            //try
            //{
                lock(this)
                {
                    if(!disposed) 
                    {
                        disposed = true;

                        // Inform the session we are disposing
                        rtpSession.Dispose(ssrc);

                        // Stop the sending of outbound data immediately
                        DisposeNetwork();

                        DisposeFrame();

                        // Let others know we are leaving
                        UpdateRtcpData();
                        rtpSession.AddBye(ssrc);
                    }
                }
            //}
                // WAA - don't catch since we're not logging
            //catch (Exception e)
            //{
            //    //LogEvent(string.Format(CultureInfo.CurrentCulture, Strings.ErrorWhileDisposingRtpSender, 
            //    //    e.ToString()), EventLogEntryType.Error, (int)RtpEL.ID.Error);
            //}
        }


        ~RtpSender()
        {
            Dispose();
        }

        
        #endregion Constructors
        
        #region Public
        
        /// <summary>
        /// Send an arbitrarily large set of data contained in a byte[].  All data in the byte[] is sent.  For efficiency reasons, you may consider
        /// using the BufferChunk version of this method that allows you to specify an offset and length in the byte[] to send.
        /// </summary>
        public void Send(byte[] frameBuffer)
        {
            Send((BufferChunk)frameBuffer);
        }

        /// <summary>
        /// This method is intended to improve the efficiency of working with unmanaged code
        /// by not forcing an extra copy into a "Frame" BufferChunk before we packetize.  We'll
        /// just packetize from the ptrs.
        /// </summary>
        /// <param name="ptrs"></param>
        /// <param name="lengths"></param>
        public void Send(IntPtr[] ptrs, int[] lengths, bool prependLengths)
        {
            if (disposed)
                throw new ObjectDisposedException(Strings.RtpSenderAlreadyDisposed);

            if(ptrs.Length != lengths.Length)
                throw new ArgumentException(Strings.ArraySizesMustAgree);

            if(ptrs.Length == 0)
                throw new ArgumentException(Strings.ThereMustBeAtLeastOneIntPtr);

            unchecked{
                ts += tsIncrement;
            }

            //this.frame.TimeStamp = ts;
            //this.frame.UnmanagedData(ptrs, lengths, prependLengths);

            fPacketizer.TimeStamp = ts;
            //fPacketizer.UnmanagedData(ptrs, lengths, prependLengths);

            Send();
        }
        
        /// <summary>
        /// Send an arbitrarily large set of data contained in a BufferChunk.
        /// </summary>
        public void Send(BufferChunk frame)
        {
            if (disposed) 
                throw new ObjectDisposedException(Strings.RtpSenderAlreadyDisposed);

            if (frame.Length == 0)
                throw new IndexOutOfRangeException(Strings.FrameLengthShouldBePositive);

            unchecked{ts += tsIncrement;}
            fPacketizer.Packetize(frame, ts, payloadType, ssrc, fPool);

            Send();
        }

        private void Send()
        {
            try
            {
                SendPackets();
            }
            catch(Exception e)
            {
                //eventLog.WriteEntry(string.Format(CultureInfo.CurrentCulture, Strings.ErrorDuringRtpSenderSend, 
                //    e.ToString()), EventLogEntryType.Error, (int)RtpEL.ID.Error);

                Dispose();
                throw;
            }

            // Record performance data
            //int pxInFrame = this.frame.PacketCount;
            //frameLength = (int)frame.Length;
            int pxInFrame = fPacketizer.PacketCount;
            frameLength = fPacketizer.Length;

            framesSent++;
            packetsSent += (uint)pxInFrame;
            bytesSent += (uint)frameLength;

            if(pxInFrame < pcPxPerFrameMin)
            {
                pcPxPerFrameMin = pxInFrame;
            }

            if(pxInFrame > pcPxPerFrameMax)
            {
                pcPxPerFrameMax = pxInFrame;
            }

            if(frameLength < pcBytesPerFrameMin)
            {
                pcBytesPerFrameMin = frameLength;
            }

            if(frameLength > pcBytesPerFrameMax)
            {
                pcBytesPerFrameMax = frameLength;
            }
        }
        
        /// <summary>
        /// Adjust this to add a delay between sending packets.  This is a rough form of bandwidth control.
        /// This is useful for constrained lossy networks like 802.11b.
        /// A value of 10 would constrain the throughput to a maximum of 1500 bytes * 1000 ms per second / 10 = 150 kb/s.
        /// Pri3: Should be setting this in reverse, AKA bps maximum allowed which is used to calculate delay
        /// </summary>
        public short DelayBetweenPackets
        {
            get
            {
                return rtpNetworkSender.DelayBetweenPackets;
            }
            set
            {
                rtpNetworkSender.DelayBetweenPackets = value;

                sdes.SetPrivateExtension(Rtcp.PEP_DBP, value.ToString(CultureInfo.InvariantCulture));
            }
        }

        public SdesData RtcpProperties
        {
            get{ return sdes;}
        }

        public PayloadType PayloadType
        {
            get{return payloadType;}
        }

        public ushort TimeStampIncrement
        {
            get{return tsIncrement;}
            set{tsIncrement = value;}
        }

        public uint SSRC
        {
            get{return ssrc;}
        }

        
        #endregion Public

        #region Protected
        
        protected virtual void SendPackets()
        {
            for (int i = 0; i < fPacketizer.PacketCount; i++)
            {
                SendPacket(fPool[i]);
            }
        }
        

        protected virtual void InitializePacketSize(){}


        protected virtual void ResetState()
        {
            ssrc = rtpSession.NextSSRC();

            ts = (uint)RtpSession.rnd.Next(1, int.MaxValue);
            seq = (ushort)RtpSession.rnd.Next(1, ushort.MaxValue);

            bytesSent = 0;
            packetsSent = 0;
            framesSent = 0;
            bytesSentLast = 0;
            packetsSentLast = 0;
            framesSentLast = 0;
        }

        
        //protected void LogEvent(string msg, EventLogEntryType et, int id)
        //{
        //    //eventLog.WriteEntry(msg, et, id);
        //}

        
        #endregion Protected

        #region Internal
        
        internal virtual void SendPacket(RtpPacket rtpPacket)
        {
            rtpPacket.SSRC = ssrc;
            rtpPacket.Sequence = seq++;

            rtpNetworkSender.Send((BufferChunk)rtpPacket);
        }


        /// <summary>
        /// Updates performance counters
        /// </summary>
        /// <param name="ms">An approximate delay in milli-seconds since the last update</param>
        internal virtual void UpdatePerformanceCounters(int ms)
        {
            lock(pcLock)
            {
            //    pc[RtpSenderPC.ID.Bytes] = bytesSent;
            //    pc[RtpSenderPC.ID.BytesPerSecond] = (long)((bytesSent - bytesSentLast) * (1000.0/ms));
                bytesSentLast = bytesSent;

            //    pc[RtpSenderPC.ID.Packets] = packetsSent;
            //    pc[RtpSenderPC.ID.PacketsPerSecond] = (long)((packetsSent - packetsSentLast) * (1000.0/ms));
                packetsSentLast = packetsSent;

            //    pc[RtpSenderPC.ID.Frames] = framesSent;
            //    pc[RtpSenderPC.ID.FramesPerSecond] = (long)((framesSent - framesSentLast) * (1000.0/ms));
                framesSentLast = framesSent;

            //    if(framesSent > 0)
            //    {
            //        pc[RtpSenderPC.ID.PxPerFrameAvg] = packetsSent / framesSent;
            //        pc[RtpSenderPC.ID.BytesPerFrameAvg] = bytesSent / framesSent;
            //    }

            //    pc[RtpSenderPC.ID.PxPerFrameCurrent] = chunk.PacketCount;
            //    pc[RtpSenderPC.ID.PxPerFrameMax] = pcPxPerFrameMax;
            //    pc[RtpSenderPC.ID.PxPerFrameMin] = pcPxPerFrameMin;
                
            //    pc[RtpSenderPC.ID.BytesPerFrameCurrent] = frameLength;
            //    pc[RtpSenderPC.ID.BytesPerFrameMax] = pcBytesPerFrameMax;
            //    pc[RtpSenderPC.ID.BytesPerFrameMin] = pcBytesPerFrameMin;

            //    pc[RtpSenderPC.ID.Ssrc] = ssrc;
            }
        }

        
        /// <summary>
        /// Resets the RtpSenders ssrc, and other SenderReport data per pg. 110, para. 3 in Colin's book
        /// </summary>
        /// <param name="ipAddress"></param>
        internal void SSRCConflictDetected()
        {
            rtpSession.AddBye(ssrc);
            ResetState();
        }


        internal void UpdateRtcpData()
        {
            // SenderReport
            sr.BytesSent = bytesSent;
            sr.PacketCount = packetsSent;
            sr.Time = (ulong)DateTime.Now.Ticks;
            sr.TimeStamp = ts; // TODO - not sure this is correct JVE

            rtpSession.AddSenderReport(ssrc, sr);
            
            // Sdes
            rtpSession.AddSdes(ssrc, sdes);
        }
        

        #endregion Internal
        
        #region Private

        private void InitializeNetwork()
        {
            rtpNetworkSender = new UdpSender(rtpSession.RtpEndPoint, rtpSession.TTL);
        }
        private void DisposeNetwork()
        {
            rtpNetworkSender.Dispose();
        }


        private void InitializePacketizer()
        {
            InitializePacketSize();

            fPool = new PacketPool<RtpPacket>(1);
            fPacketizer = new RtpFramePacketizer(packetSize, fPool,ts);
        }

        private void DisposeFrame()
        {
            //fPacketizer.Dispose();
            //fPool.Dispose();

            fPacketizer = null;
            fPool = null;
            //frame.Dispose();
            //frame = null;
        }

        #endregion Private
    }
    
    #endregion RtpSender

    #region RtpSenderFec

    internal abstract class RtpSenderFec : RtpSender
    {
        #region Statics
        
        // The growth of the buffer pool is limited to ~= 48MB of data active at one time
        //
        // 1 packets (PoolInitialSize) Doubled (PoolGrowthFactor) 15 times (PoolMaxGrows)
        // 1 * 2^15 = 32768 packets * ~1.5 K per buffer ~= 48MB of data.
        
        /// <summary>
        /// Initial number of pre-allocated buffers for receiving data
        /// </summary>
        private const int PoolInitialSize = 1;

        /// <summary>
        /// Multiplier - amount of buffers to grow the pools by when the pool is empty
        /// </summary>
        private const int PoolGrowthFactor = 2;

        #endregion Statics
                
        #region Members

        /// <summary>
        /// A pool of packets for reuse
        /// </summary>
        private Stack fecPool = new Stack(PoolInitialSize);

        /// <summary>
        /// How many packets has this sender allocated
        /// </summary>
        private int fecPoolPackets;

        /// <summary>
        /// How many data packets are protected by the FEC
        /// </summary>
        protected ushort cDataPx;
        
        /// <summary>
        /// How many fec packets protect the data packets
        /// </summary>
        protected ushort cFecPx;

        /// <summary>
        /// Where in the data array the current data packet belongs
        /// </summary>
        protected ushort dataFecIndex;

        /// <summary>
        /// Where in the checksum array the current checksum packet belongs
        /// </summary>
        protected ushort checksumFecIndex;

        /// <summary>
        /// Collection of RtpPackets (cast to BufferChunk), across which we'll generate the FEC payload
        /// </summary>
        protected BufferChunk[] data;
        internal RtpPacket[] dataPx = null;

        /// <summary>
        /// Encoder class which will handle the encoding
        /// </summary>
        protected IFec fecEncoder;
        
        /// <summary>
        /// A holder of sent packets, to be used for retransmission purposes
        /// </summary>
        protected BufferChunk[] checksum;
        internal RtpPacketFec[] checksumPx = new RtpPacketFec[0];

        private int fecPackets = 0;
        private int fecBytes = 0;

        private ushort fecSeq;

        protected ushort dataRangeMin;
        
        protected int pcFecType;

        #endregion Members

        #region Constructor

        internal RtpSenderFec(IRtpSession session, string name, PayloadType pt, Hashtable priExns) : 
            base(session, name, pt, priExns) {}


        #endregion Constructor

        #region Protected
        
        protected override void InitializePacketSize()
        {
            packetSize -= RtpPacketFec.HEADER_SIZE_HACK;
            base.InitializePacketSize ();
        }

        protected override void ResetState()
        {
            base.ResetState ();

            // Important member variables
            fecSeq = (ushort)RtpSession.rnd.Next(1, ushort.MaxValue);
            dataFecIndex = 0;

            // Performance counters
            fecBytes = 0;
            fecPackets = 0;
        }

        
        protected void SendPacketsFec()
        {
            checksumFecIndex = 0;

            for(int i = 0; i < cFecPx; i++)
            {
                RtpPacketFec packet = checksumPx[i];
                packet.PayloadSize = checksum[i].Length;

                SendPacketFec(packet);

                checksum[i].Reset(checksum[i].Index, 0);
            }

            // Clear cache of data packets
            ClearData();
        }

        protected void ClearData()
        {
            for(int i = 0; i < cDataPx; i++)
            {
                dataPx[i] = null;
                data[i] = null;
            }
        }

        
        /// <summary>
        /// Initialize storage for Data and Checksum
        /// </summary>
        protected void InitializeDCStorage()
        {
            dataPx = new RtpPacket[cDataPx];
            data = new BufferChunk[cDataPx];

            ReturnFecPackets();

            checksumPx = new RtpPacketFec[cFecPx];
            checksum = new BufferChunk[cFecPx];

            for(int i = 0; i < cFecPx; i++)
            {
                RtpPacketFec packet = GetPacketFec();
                
                checksumPx[i] = packet;
                checksum[i] = packet.Payload;
            }
        }

        protected abstract void SetEncoder();


        #endregion Protected

        #region Internal

        internal override void SendPacket(RtpPacket rtpPacket)
        {
            // Set the FecIndex of the packet
            rtpPacket.FecIndex = dataFecIndex;

            // Store the packet in 2 formats
            dataPx[dataFecIndex] = rtpPacket;
            data[dataFecIndex] = (BufferChunk)rtpPacket;

            dataFecIndex++;

            // The packet is still valid after the Send
            base.SendPacket(rtpPacket);

            // See if we have reached the time to send FEC packets
            if(dataFecIndex == cDataPx)
            {
                dataFecIndex = 0;
                dataRangeMin = dataPx[0].Sequence;

                fecEncoder.Encode(data, checksum);
                SendPacketsFec();
            }
        }


        internal void SendPacketFec(RtpPacketFec packet)
        {
            packet.Sequence = fecSeq++;
            packet.DataRangeMin = dataRangeMin;
            packet.PacketsInFrame = cDataPx;
            packet.SSRC = SSRC;
            packet.FecIndex = checksumFecIndex++;
            rtpNetworkSender.Send((BufferChunk)packet);

            fecPackets++;
            fecBytes += packet.PayloadSize;
        }

        internal void ReturnFecPacket(RtpPacketFec packet)
        {
            Debug.Assert(packet != null);

            packet.Reset();
            fecPool.Push(packet);
        }

        internal RtpPacketFec GetPacketFec()
        {
            try
            {
                if( fecPool.Count == 0 )
                {
                    GrowFecPacketPool();
                }

                return (RtpPacketFec)fecPool.Pop();
            }
            catch(InvalidOperationException)
            {
                // Why are we asking for more than RS_Fec.MaxChecksumPackets packets?"
                Debug.Assert(false);
                throw;
            }
        }
        
        /// <summary>
        /// Updates performance counters
        /// </summary>
        /// <param name="ms">An approximate delay in milli-seconds since the last update</param>
        internal override void UpdatePerformanceCounters(int ms)
        {
            //lock(pcLock)
            //{
            //    pc[RtpSenderPC.ID.f_Bytes] = fecBytes;
            //    pc[RtpSenderPC.ID.f_Packets] = fecPackets;
            //    pc[RtpSenderPC.ID.f_RatioChecksum] = cFecPx;
            //    pc[RtpSenderPC.ID.f_RatioData] = cDataPx;
            //    pc[RtpSenderPC.ID.f_Type] = pcFecType;

            //    base.UpdatePerformanceCounters(ms);
            //}
        }


        #endregion Internal

        #region Private

        private void GrowFecPacketPool()
        {
            if(fecPoolPackets < RS_Fec.MaxChecksumPackets)
            {
                // Store the current count of fec packets, so we know how many to add to the pool
                int fecPoolPacketsCurrent = fecPoolPackets;

                // Calculate how many packets we will have after growth
                if(fecPoolPackets > 0)
                {
                    fecPoolPackets *= PoolGrowthFactor;
                }
                else
                {
                    fecPoolPackets = PoolInitialSize;
                }

                if(fecPoolPackets > RS_Fec.MaxChecksumPackets)
                {
                    fecPoolPackets = RS_Fec.MaxChecksumPackets;
                }

                // Calculate how many packets to add to the pool, and do it
                int packetsToAdd = fecPoolPackets - fecPoolPacketsCurrent;

                for (int i = 0; i < packetsToAdd; i++)
                {
                    fecPool.Push(new RtpPacketFec());
                }
            }
        }
        

        private void ReturnFecPackets()
        {
            for(int i = 0; i < checksumPx.Length; i++)
            {
                ReturnFecPacket(checksumPx[i]);
            }
        }

        #endregion Private
    }

    
    #endregion RtpSenderFec

    #region RtpSenderCFec

    internal class RtpSenderCFec : RtpSenderFec
    {
        internal RtpSenderCFec(IRtpSession session, string name, PayloadType pt, Hashtable priExns, 
            ushort cDataPx, ushort cFecPx)
            : base(session, name, pt, priExns)
        {
            pcFecType = 1;

            this.cDataPx = cDataPx;
            this.cFecPx = cFecPx;

            SetEncoder();
            InitializeDCStorage();
        }


        protected override void SetEncoder()
        {
            if(cFecPx == 1)
            {
                fecEncoder = new XOR_Fec();
            }
            else
            {
                fecEncoder = new RS_Fec();
            }
        }
    }

    
    #endregion RtpSenderCFec

    #region RtpSenderFFec

    internal class RtpSenderFFec : RtpSenderFec
    {
        #region Members
        
        private int fecPercent;
        
        private XOR_Fec xorEncoder = new XOR_Fec();
        private RS_Fec rsEncoder = new RS_Fec();

        #endregion Members

        #region Constructor

        internal RtpSenderFFec(IRtpSession session, string name, PayloadType pt, Hashtable priExns, 
            ushort cFecPx)
            : base(session, name, pt, priExns)
        {
            fecPercent = cFecPx;
            pcFecType = 2;
        }


        #endregion Constructor

        protected override void SendPackets()
        {
            ResetEncodingState(fPacketizer.PacketCount);
            base.SendPackets();
        }

        protected override void SetEncoder()
        {
            if(cFecPx == 1)
            {
                fecEncoder = xorEncoder;
            }
            else
            {
                fecEncoder = rsEncoder;
            }
        }

        
        private void ResetEncodingState(int packetsInFrame)
        {
            if(packetsInFrame != cDataPx)
            {
                cDataPx = (ushort)packetsInFrame;
                cFecPx = (ushort)(cDataPx * fecPercent / 100);
                if(cDataPx * fecPercent % 100 > 0)
                {
                    cFecPx++;
                }

                cFecPx = Math.Min(cFecPx, RS_Fec.MaxChecksumPackets);

                SetEncoder();
                InitializeDCStorage();
            }
        }
    }

    
    #endregion RtpSenderFFec
}
