using System;
using System.Collections.Generic;
using System.Threading;

namespace NewTOAPIA.Net.Rtp
{
    #region RtpStreamFec

    internal abstract class RtpStreamFec : RtpStream
    {
        #region Members

        // parameters we were told to construct the stream with
        protected ushort dataPxExp;
        protected ushort fecPxExp;

        protected RtpPacketBase[] dataPx = null;
        protected BufferChunk[] data = null;
        protected BufferChunk[] recovery = null;

        private RtpListener.GetBufferHandler getBufferHandler = null;

        private bool rangeInitialized = false;

        private ushort minSeq;
        private ushort maxSeq;

        private int maxDataSeq = -1;
        private int maxFecSeq = -1;

        private int dataPxAct;
        private int fecPxAct;

        private int recoveryIndex;

        /// <summary>
        /// Decoder class which will handle the decoding
        /// </summary>
        protected IFec fecDecoder;

        #region Performance Counters

        private int pcDataBytes;
        private int pcDataPackets;
        private int pcDataPacketsLate;
        private int pcDataPacketsLost;

        private int pcDecodePath;

        private int pcFecBytes;
        private int pcFecPackets;
        private int pcFecPacketsLate;
        private int pcFecPacketsLost;

        private int pcUndecodable;

        protected int pcFecType;

        #endregion Performance Counters

        #endregion Members

        #region Constructor

        internal RtpStreamFec(RtpListener rtpListener, uint ssrc, SdesData sdes, PayloadType pt)
            : base(rtpListener.ReturnBufferCallback, ssrc, sdes, pt)
        {
            this.getBufferHandler = rtpListener.GetBufferCallback;
        }


        #endregion Constructor

        /// <summary>
        /// Updates the local perf counters
        /// </summary>
        /// <param name="ms">An approximate delay in milli-seconds since the last update</param>
        internal override void UpdatePerformanceCounters(int ms)
        {
            //lock(pcLock)
            //{
            //    if(pc != null)
            //    {
            //        pc[RtpStreamPC.ID.f_DataBytes] = pcDataBytes;
            //        pc[RtpStreamPC.ID.f_DataPackets] = pcDataPackets;
            //        pc[RtpStreamPC.ID.f_DataPacketsLate] = pcDataPacketsLate;
            //        pc[RtpStreamPC.ID.f_DataPacketsLost] = pcDataPacketsLost;
            //        pc[RtpStreamPC.ID.f_DecodePath] = pcDecodePath;
            //        pc[RtpStreamPC.ID.f_FecBytes] = pcFecBytes;
            //        pc[RtpStreamPC.ID.f_FecPackets] = pcFecPackets;
            //        pc[RtpStreamPC.ID.f_FecPacketsLate] = pcFecPacketsLate;
            //        pc[RtpStreamPC.ID.f_FecPacketsLost] = pcFecPacketsLost;
            //        pc[RtpStreamPC.ID.f_RatioChecksum] = fecPxExp;
            //        pc[RtpStreamPC.ID.f_RatioData] = dataPxExp;
            //        pc[RtpStreamPC.ID.f_Type] = pcFecType;
            //        pc[RtpStreamPC.ID.f_Undecodable] = pcUndecodable;
            //    }

            //    base.UpdatePerformanceCounters(ms);
            //}
        }


        internal override void ProcessPacket(RtpPacketBase packet)
        {
            // If any part of this code throws an exception we need to clean up what we have and
            // reset back to a default state so we can start over.
            try
            {
                PayloadType pt = packet.PayloadType;
                ValidatePayloadType(pt);

                // Process packet, depending on payload type
                if (pt == this.pt)
                {
                    ProcessPacketData((RtpPacket)packet);
                }
                else
                {
                    ProcessPacketFec((RtpPacketFec)packet);
                }

                // See if we have collected enough packets
                if (dataPxAct == dataPxExp)
                {
                    ForwardDataPacketsToBase();
                }
                else if (dataPxAct + fecPxAct == dataPxExp)
                {
                    Decode();
                    ForwardDataPacketsToBase();
                }
            }
            catch (ThreadAbortException) { } // Because we catch a generic exception next
            catch (Exception e)
            {
                //eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.Error);
                ForwardDataPacketsToBase();
            }
        }


        protected void InitializeDCRStorage()
        {
            // Data and checksum are combined
            dataPx = new RtpPacketBase[dataPxExp + fecPxExp];
            data = new BufferChunk[dataPxExp + fecPxExp];

            recovery = new BufferChunk[fecPxExp];
        }


        #region Private

        private void ProcessPacketData(RtpPacket packet)
        {
            pcDataPacketsLost += LostPackets(ref maxDataSeq, packet.Sequence);
            pcDataPackets++;
            pcDataBytes += packet.PayloadSize;

            if (InRangeData(packet))
            {
                // New sequence of packets
                if (dataPxAct + fecPxAct == 0)
                {
                    ResetDecodingState(packet);
                }

                int dataFecIndex = packet.FecIndex;

                // Duplicate packet check for flaky networks
                if (dataPx[dataFecIndex] != null)
                {
                    //eventLog.WriteEntry(Strings.DuplicatePacketReceived, EventLogEntryType.Warning,
                    //    (int)RtpEL.ID.DuplicatePacketReceived);
                    return;
                }

                dataPxAct++;

                dataPx[dataFecIndex] = packet;
                data[dataFecIndex] = (BufferChunk)packet;
            }
        }

        private void ProcessPacketFec(RtpPacketFec packet)
        {
            pcFecPacketsLost += LostPackets(ref maxFecSeq, packet.Sequence);
            pcFecPackets++;
            pcFecBytes += packet.PayloadSize;

            if (InRangeFec(packet))
            {
                // New sequence of packets
                if (dataPxAct + fecPxAct == 0)
                {
                    ResetDecodingState(packet);
                }

                int checksumFecIndex = packet.FecIndex;

                // Duplicate packet check for flaky networks
                if (dataPx[dataPxExp + checksumFecIndex] != null)
                {
                    //eventLog.WriteEntry(Strings.DuplicatePacketReceived, EventLogEntryType.Warning,
                    //    (int)RtpEL.ID.DuplicatePacketReceived);
                    return;
                }

                fecPxAct++;

                dataPx[dataPxExp + checksumFecIndex] = packet;
                data[dataPxExp + checksumFecIndex] = packet.Payload;

                recovery[recoveryIndex++] = getBufferHandler();
            }
        }

        // Check to see if packet is inside the recovery range
        protected bool InRangeFec(RtpPacketFec packet)
        {
            bool inRange = true;

            if (rangeInitialized)
            {
                ushort minDiff = unchecked((ushort)(packet.DataRangeMin - minSeq));

                // Correct data range but not needed - late (chunk already complete)
                // OR Wrong data range - really late
                if ((minDiff == 0 && (dataPxAct + fecPxAct == 0)) || minDiff > HALF_USHORT_MAX)
                {
                    inRange = false;
                    pcFecPacketsLate++;
                    ReturnFecBuffer(packet.ReleaseBuffer());
                }
                else if (minDiff != 0) // Wrong data range - too new
                {
                    Undecodable();
                }
            }

            return inRange;
        }

        // Check to see if packet is inside the recovery range
        protected bool InRangeData(RtpPacket packet)
        {
            bool inRange = true;

            if (rangeInitialized)
            {
                ushort minDiff = unchecked((ushort)(packet.Sequence - minSeq));
                ushort maxDiff = unchecked((ushort)(maxSeq - packet.Sequence));

                if (minDiff > HALF_USHORT_MAX) // Wrong data range - too late
                {
                    inRange = false;
                    pcDataPacketsLate++;
                    base.ProcessPacket(packet);
                }
                else if (maxDiff > HALF_USHORT_MAX) // Wrong data range - too new
                {
                    Undecodable();
                }
            }

            return inRange;
        }


        /// <summary>
        /// Sends the data packets (original or reconstructed) to the base RtpStream to process
        /// into a chunk.
        /// </summary>
        protected void ForwardDataPacketsToBase()
        {
            try
            {
                if (dataPxAct > 0)
                {
                    for (int i = 0; i < dataPxExp; i++)
                    {
                        if (dataPx[i] != null)
                        {
                            // Base should not raise an exception
                            // Instead, it should log the problem and return the packet to the pool
                            base.ProcessPacket(dataPx[i]);
                            dataPx[i] = null;
                            data[i] = null;
                        }
                    }
                }

                if (fecPxAct > 0)
                {
                    for (int i = 0; i < fecPxExp; i++)
                    {
                        int index = dataPxExp + i;

                        if (dataPx[index] != null)
                        {
                            ReturnFecBuffer(dataPx[index].ReleaseBuffer());
                            dataPx[index] = null;
                            data[index] = null;
                        }
                    }
                }
            }
            finally // Just in case something has gone terribly wrong, attempt to reset
            {
                dataPxAct = 0;
                fecPxAct = 0;
            }
        }


        protected void ValidatePayloadType(PayloadType pt)
        {
            if (pt != PayloadType && pt != PayloadType.FEC)
            {
                throw new ArgumentException(Strings.UnexpectedPayloadType);
            }
        }


        protected void ReturnDataBuffer(BufferChunk buffer)
        {
            returnBufferHandler(buffer);
        }


        protected void ReturnFecBuffer(BufferChunk buffer)
        {
            returnBufferHandler(buffer);
        }


        protected virtual void ResetDecodingState(RtpPacketBase packet)
        {
            if (packet.PayloadType == PayloadType)
            {
                RtpPacket rtpPacket = (RtpPacket)packet;
                minSeq = unchecked((ushort)(rtpPacket.Sequence - rtpPacket.FecIndex));
            }
            else
            {
                minSeq = ((RtpPacketFec)packet).DataRangeMin;
            }

            maxSeq = unchecked((ushort)(minSeq + dataPxExp - 1));
            rangeInitialized = true;
            recoveryIndex = 0;
        }


        private void Decode()
        {
            pcDecodePath++;

            try
            {
                fecDecoder.Decode(data, fecPxExp, recovery);
                dataPxAct = dataPxExp; // Trigger ForwardDataPacketsToBase

                // Cast BufferChunks back into RtpPackets
                int recoveryIndex = 0;
                for (int i = 0; i < dataPxExp; i++)
                {
                    if (dataPx[i] == null)
                    {
                        try
                        {
                            dataPx[i] = new RtpPacket(recovery[recoveryIndex]);
                        }
                        catch (InvalidRtpPacketException e)
                        {
                            //eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.InvalidPacket);
                            ReturnDataBuffer(recovery[recoveryIndex]);
                        }
                        finally
                        {
                            recovery[recoveryIndex] = null;
                            recoveryIndex++;
                        }
                    }
                }
            }
            catch (ThreadAbortException) { } // Because we catch a generic exception next
            catch (Exception e)
            {
                //eventLog.WriteEntry(e.ToString(), EventLogEntryType.Error, (int)RtpEL.ID.Error);

                // Return recovery packets
                for (int recoveryIndex = 0; recoveryIndex < fecPxAct; recoveryIndex++)
                {
                    ReturnDataBuffer(recovery[recoveryIndex]);
                    recovery[recoveryIndex] = null;
                }
            }
        }

        private void Undecodable()
        {
            // We're undecodable if there are packets we couldn't process
            if (dataPxAct + fecPxAct == 0) return;

            pcUndecodable++;

            // Return any recovery packets we weren't able to use
            // Do this before the call to ForwardDataPacketsToBase
            for (int i = 0; i < fecPxAct; i++)
            {
                ReturnDataBuffer(recovery[i]);
            }

            ForwardDataPacketsToBase();
        }


        #endregion Private
    }

    #endregion RtpStreamFec

    #region RtpStreamCFec

    internal class RtpStreamCFec : RtpStreamFec
    {
        #region Constructor

        internal RtpStreamCFec(RtpListener rtpListener, uint ssrc, SdesData sdes, PayloadType pt, ushort dataPxExp, ushort fecPxExp)
            : base(rtpListener, ssrc, sdes, pt)
        {
            pcFecType = 1;

            this.dataPxExp = dataPxExp;
            this.fecPxExp = fecPxExp;

            SetDecoder();
            InitializeDCRStorage();
        }


        #endregion Constructor

        private void SetDecoder()
        {
            if (fecPxExp == 1)
            {
                fecDecoder = new XOR_Fec();
            }
            else
            {
                fecDecoder = new RS_Fec();
            }
        }
    }


    #endregion RtpStreamFec

    #region RtpStreamFFec

    internal class RtpStreamFFec : RtpStreamFec
    {
        #region Members

        private int fecPercent;

        private XOR_Fec xorDecoder = new XOR_Fec();
        private RS_Fec rsDecoder = new RS_Fec();

        #endregion Members

        #region Constructor

        internal RtpStreamFFec(RtpListener rtpListener, uint ssrc, SdesData sdes, PayloadType pt, ushort fecPxExp)
            : base(rtpListener, ssrc, sdes, pt)
        {
            fecPercent = fecPxExp;
            pcFecType = 2;
        }


        #endregion Constructor

        protected override void ResetDecodingState(RtpPacketBase packet)
        {
            ushort packetsInFrame;

            if (packet.PayloadType == PayloadType)
            {
                packetsInFrame = ((RtpPacket)packet).PacketsInFrame;
            }
            else
            {
                packetsInFrame = ((RtpPacketFec)packet).PacketsInFrame;
            }

            if (dataPxExp != packetsInFrame)
            {
                // Resize data structures appropriately
                dataPxExp = packetsInFrame;

                fecPxExp = (ushort)(dataPxExp * fecPercent / 100);
                if (dataPxExp * fecPercent % 100 > 0)
                {
                    fecPxExp++;
                }

                fecPxExp = Math.Min(fecPxExp, RS_Fec.MaxChecksumPackets);

                SetDecoder();
                InitializeDCRStorage();
            }

            base.ResetDecodingState(packet);
        }


        private void SetDecoder()
        {
            if (fecPxExp == 1)
            {
                fecDecoder = xorDecoder;
            }
            else
            {
                fecDecoder = rsDecoder;
            }
        }
    }

    #endregion RtpStreamFec

}
