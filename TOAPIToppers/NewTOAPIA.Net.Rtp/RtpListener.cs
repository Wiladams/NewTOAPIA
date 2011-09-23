using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading;


namespace NewTOAPIA.Net.Rtp
{
    using NewTOAPIA.BCL;

    /// <summary>
    /// Listens for packets from the network and distributes them to the appropriate RtpStream
    /// </summary>
    public class RtpListener: IDisposable
    {
        public interface IRtpSession
        {
            RtpStream GetStream(uint ssrc, IPAddress ip);
            void LogEvent(string source, string msg, EventLogEntryType et, int id);
            void RaiseInvalidPacketEvent(string msg);
            
            int DropPacketsPercent{get;}
            Random Rnd{get;}
            SdesData Sdes{get;}
            IPEndPoint RtpEndPoint{get;}
        }

        
        #region Statics
        
        // The growth of the buffer pool is limited to ~= 95MB of data active at one time
        //
        // 64 packets (PoolInitialSize) Doubled (PoolGrowthFactor) 10 times (PoolMaxGrows)
        // 64 * 2 * 10 = 65,536 buffers * ~1.5 K per buffer ~= 95MB of data.
        
        /// <summary>
        /// Initial number of pre-allocated buffers for receiving data
        /// </summary>
        private const int PoolInitialSize = 64;

        /// <summary>
        /// Multiplier - amount of buffers to grow the pools by when the pool is empty
        /// </summary>
        private const int PoolGrowthFactor = 2;

        /// <summary>
        /// How many times do we want the pools to grow by GrowthFactor before we consider
        /// the situation to be out of control (unbounded)
        /// </summary>
        private const int PoolMaxGrows = 10;

        #endregion Statics

        #region Members

        /// <summary>
        /// Reference back to the RtpSession that created this Listener
        /// See IRtpSession for the methods used
        /// </summary>
        private IRtpSession rtpSession = null;

        /// <summary>
        /// Queue that holds a pool of BufferChunks for receiving data off the wire
        /// </summary>
        private Stack bufferPool = Stack.Synchronized(new Stack(PoolInitialSize));

        /// <summary>
        /// How many packets has this listener allocated
        /// </summary>
        private int packets = PoolInitialSize;

        /// <summary>
        /// Number of times we have grown the bufferPool
        /// </summary>
        private int poolGrows = 0;

        /// <summary>
        /// Queue that holds all RtpPackets that have been received but not yet forwarded to the 
        /// RtpStreams for processing
        /// </summary>
        //private Queue receivedPackets = Queue.Synchronized(new Queue());
        LockFreeQueue<object[]> receivedPackets = new LockFreeQueue<object[]>();

        /// <summary>
        /// Receives packets off the network and stores them for sorting and distribution
        /// </summary>
        private Thread threadReceivePackets;
        bool receivePacketsRunning;

        /// <summary>
        /// Sorts the packets which have been received from the network and forwards them on to the
        /// appropriate RtpStream for processing
        /// </summary>
        private Thread threadDistributePackets;
        bool distributePacktesRunning;

        /// <summary>
        /// A socket that listens for incoming multicast Rtp packets.
        /// </summary>
        private IReceiveBufferChunk rtpNetworkListener = null;

        /// <summary>
        /// Signals the Distribution thread it has work to do
        /// </summary>
        private AutoResetEvent newPacket = new AutoResetEvent(false);
        
        /// <summary>
        /// Local performance counter object
        /// </summary>
        //private RtpListenerPC pc;

        /// <summary>
        /// Lock to make sure performance counters are valid during use
        /// </summary>
        private object pcLock = new object();

        /// <summary>
        /// The number of packets the RtpListener has received off the wire
        /// </summary>
        private uint pcPackets;

        /// <summary>
        /// The number of packets the RtpListener has received for an unassociated stream
        /// </summary>
        private uint pcStreamlessPackets;


        private ReturnBufferHandler returnBufferHandler;

        public delegate BufferChunk GetBufferHandler();
        private GetBufferHandler getBufferHandler;

        #endregion Members

        #region Constructors
        
        public RtpListener(RtpSession rtpSession)
        {
            this.rtpSession = rtpSession;

            returnBufferHandler = new ReturnBufferHandler(ReturnBuffer);
            getBufferHandler = new GetBufferHandler(GetBuffer);

            InitializeBufferPool();
            InitializeNetwork();
            InitializeThreads();
        }

        
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            
            //try
            //{
                DisposeThreads();
                DisposeNetwork();
                DisposeBufferPool();
            //}
            // WAA - don't catch since we're not logging
            //catch(Exception e)
            //{
            //    //LogEvent(string.Format(CultureInfo.CurrentCulture, Strings.ErrorDisposingRtpListener, 
            //    //    e.ToString()), EventLogEntryType.Error, (int)RtpEL.ID.Error);
            //}
        }

        
        ~RtpListener()
        {
            Dispose();
        }

        #endregion
        
        #region Public

        public ReturnBufferHandler ReturnBufferCallback
        {
            get{return returnBufferHandler;}
        }

        public GetBufferHandler GetBufferCallback
        {
            get{return getBufferHandler;}
        }


        /// <summary>
        /// Updates performance counters
        /// </summary>
        /// <param name="ms">An approximate delay in milli-seconds since the last update</param>
        //public void UpdatePerformanceCounters(int ms)
        //{
        //    //lock(pcLock)
        //    //{
        //    //    if(pc != null)
        //    //    {
        //    //        int p = packets;
        //    //        int q = bufferPool.Count;

        //    //        pc[RtpListenerPC.ID.BufferPoolSize] = p;
        //    //        pc[RtpListenerPC.ID.BuffersFree] = q;
        //    //        pc[RtpListenerPC.ID.BuffersInUse] = p - q;


        //    //        pc[RtpListenerPC.ID.GrowthFactor] = PoolGrowthFactor;
        //    //        pc[RtpListenerPC.ID.InitialBuffers] = PoolInitialSize;
        //    //        pc[RtpListenerPC.ID.GrowthCount] = poolGrows;
        //    //        pc[RtpListenerPC.ID.Packets] = pcPackets;
        //    //        pc[RtpListenerPC.ID.StreamlessPackets] = pcStreamlessPackets;
        //    //    }
        //    //}
        //}

        
        #endregion Public

        #region Private
        
        #region BufferPool

        /// <summary>
        /// Grow the buffer pool
        /// 
        /// Called when all the previously allocated packets are in use.  Unless we are handling a
        /// huge chunk (~= 95MB), it indicates a problem - like packets that aren't being returned
        /// to the pool and so we put a limit on the growth.
        /// </summary>
        private void GrowBufferPool()
        {
            // Only allow one thread at a time to grow the pool (Receive or Distribute)
            lock(bufferPool)
            {
                if(poolGrows < PoolMaxGrows)
                {
                    // Check to see if the pool really needs to grow 
                    // (in case two threads were waiting at the lock)
                    if(bufferPool.Count < packets / 4)
                    {
                        poolGrows++;

                        int packetsToAdd = (packets * PoolGrowthFactor) - packets;

                        for (int i = 0; i < packetsToAdd; i++)
                        {
                            ReturnBuffer(new BufferChunk(UdpSocket.BEST_PACKET_SIZE));
                        }

                        packets *= PoolGrowthFactor;
                    }
                }
                else
                {
                    //LogEvent(Strings.TryingToGrowTheBufferPool, EventLogEntryType.Warning, 
                    //    (int)RtpEL.ID.UnboundedGrowth);

                    // Maybe enough packets have trickled in to continue
                    if(bufferPool.Count == 0)
                    {
                        throw new PoolExhaustedException(Strings.RtpListenerBufferPoolGrowing);
                    }
                }
            }
        }

        
        /// <summary>
        /// Returns a BufferChunk from the pool, or grows the pool if need be
        /// </summary>
        /// <returns></returns>
        private BufferChunk GetBuffer()
        {
            // NOTE: multi-threaded gambling:
            //   After a "risk assesment" of doing this with a lock, we opted for the performance
            //   of doing this "lockless" and letting us catch the exception, should it occur.

            try
            {
                // *Attempt* to avoid InvalidOperationException, if possible
                if( bufferPool.Count == 0 )
                {
                    GrowBufferPool();
                }

                return (BufferChunk)bufferPool.Pop();
            }
            catch (InvalidOperationException)
            {
                GrowBufferPool();
                return GetBuffer(); // Recursive Call
            }
        }

        
        /// <summary>
        /// This method is called to return buffers to the bufferPool
        /// </summary>
        /// <param name="buffer"></param>
        private void ReturnBuffer(BufferChunk buffer)
        {
            buffer.Reset();

            if (null != bufferPool)
            {
                bufferPool.Push(buffer);
            }
            else
            {
                Console.WriteLine("RtpListener.ReturnBuffer() - Buffer Pool is null"); 
            }
        }

        
        #endregion BufferPool
        
        #region Initialize / Dispose
        
        private void InitializeNetwork()
        {
            rtpNetworkListener = new UdpReceiver(rtpSession.RtpEndPoint, RtpSession.DefaultNetworkTimeout);
        }

        private void DisposeNetwork()
        {
            rtpNetworkListener.Dispose();
            rtpNetworkListener = null;
        }

        private void InitializeBufferPool()
        {
            for (int i = 0; i < packets; i++)
            {
                ReturnBuffer(new BufferChunk(UdpSocket.BEST_PACKET_SIZE));
            }
        }

        private void DisposeBufferPool()
        {
            bufferPool.Clear();
            bufferPool = null;
        }

        private void InitializeThreads()
        {
            distributePacktesRunning = true;
            threadDistributePackets = new Thread(new ThreadStart(DistributePackets));
            threadDistributePackets.IsBackground = true;
            threadDistributePackets.Name = "RtpListener Distribute Packets";
            threadDistributePackets.Start();

            receivePacketsRunning = true;
            threadReceivePackets = new Thread(new ThreadStart(ReceivePackets));
            threadReceivePackets.IsBackground = true;
            threadReceivePackets.Name = "RtpListener Receive Packets";
            threadReceivePackets.Start();
        }

        private void DisposeThreads()
        {
            distributePacktesRunning = false;
            threadDistributePackets.Abort();
            threadDistributePackets = null;

            threadReceivePackets.Abort();
            receivePacketsRunning = false;
            threadReceivePackets = null;

            // Sleep for a little bit to give the threads a chance to exit
            //Thread.Sleep(100);
        }


        
        #endregion Initialize / Dispose

        #region Thread Methods
        
        private void ReceivePackets()
        {
            BufferChunk bc = null;

            while(true)
            {
                try
                {
                    bc = GetBuffer();
                    EndPoint ep;

                    rtpNetworkListener.ReceiveFrom(bc, out ep);
                    receivedPackets.Enqueue(new object[] {bc, ep});

                    // Signal that a new packet has come in
                    newPacket.Set();

                    unchecked{pcPackets++;}
                }
                catch(ThreadAbortException){}
                catch(PoolExhaustedException e)
                {
                    //LogEvent(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.UnboundedGrowth);
                    return; // Exit the thread gracefully
                }
                catch (System.Net.Sockets.SocketException e)
                {
                    ReturnBuffer(bc);

                    // Something other than - No data received before timeout
                    if (e.ErrorCode != 10060)
                    {
                        Object[] args = new Object[]{this, new RtpEvents.HiddenSocketExceptionEventArgs((RtpSession)rtpSession, e)};
                        EventThrower.QueueUserWorkItem( new RtpEvents.RaiseEvent(RtpEvents.RaiseHiddenSocketExceptionEvent), args );

                        //LogEvent(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.Error);
                    }
                }
                catch(Exception e)
                {
                    ReturnBuffer(bc);
                    //LogEvent(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.Error);
                }
            }
        }


        /// <summary>
        /// DistributePackets turns packets that came from the listener thread into
        /// RtpPackets, creating new RtpStreams if one doesn't exist for the RtpPacket's 
        /// SSRC, then calling RtpStream.newPacket(rtpPacket) to place the RtpPacket 
        /// into the RtpStream's Queue for processing.
        /// </summary>
        private void DistributePackets()
        {
            while (newPacket.WaitOne())
            {
                while(receivedPackets.Count > 0)
                {
                    //object[] ao = (object[])receivedPackets.Dequeue();
                    object[] ao = null;
                    if (!receivedPackets.TryDequeue(out ao))
                        continue;

                    BufferChunk bc = (BufferChunk)ao[0];
                    IPEndPoint ep = (IPEndPoint)ao[1];

                    try
                    {
                        //Turn the raw UDP packet data into an Rtp packet
                        RtpPacketBase packet = new RtpPacketBase(bc);
                        
                        // For now, we support 2 types of packets - RtpPacket and RtpPacketFec.  If
                        // the number of packet types grows, we may need to push the casting onto
                        // the streams, since they know what type of packets they expect.  For now
                        // we will handle the casting here. JVE 6/17/2004
                        if(packet.PayloadType == PayloadType.FEC)
                        {
                            packet = new RtpPacketFec(packet);
                        }
                        else
                        {
                            packet = new RtpPacket(packet);
                        }
                        
                        // Get the stream if it exists
                        RtpStream stream = rtpSession.GetStream(packet.SSRC, ep.Address);
                        
                        if (stream != null)
                        {
                            stream.ProcessPacket(packet);
                        }
                        else
                        {
                            // Otherwise return the packet to the pool
                            ReturnBuffer(bc);
                            unchecked{pcStreamlessPackets++;}
                        }
                    }
                    catch(ThreadAbortException){}
                    catch (InvalidRtpPacketException e)
                    {
                        HandleInvalidPacket(bc, ep, e.ToString());
                        ReturnBuffer(bc);
                    }
                    catch(PoolExhaustedException e)
                    {
                        //LogEvent(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.UnboundedGrowth);
                        return; // Exit the thread gracefully
                    }
                    catch(Exception e)
                    {
                        ReturnBuffer(bc);
                        //LogEvent(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.Error);
                    }
                }
            }
        }

        
        #endregion Thread Methods

        #region Logging

        private void LogEvent(string msg, EventLogEntryType et, int id)
        {
            rtpSession.LogEvent("RtpListener", msg, et, id);
        }

        #endregion Logging

        private void HandleInvalidPacket(BufferChunk packet, IPEndPoint src, string exception)
        {
            rtpSession.RaiseInvalidPacketEvent(string.Format(CultureInfo.CurrentCulture, 
                "First byte: {0}, Source IP: {1}, Exception: {2}", packet.NextByte(), src, exception));
        }

        
        #endregion Private
    }
}
