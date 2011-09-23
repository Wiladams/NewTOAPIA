using System;
//using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net;


namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// RtpParticipant is the object shared between Rtp and Rtcp for describing a participant.
    /// 
    /// Most of the public properties are a thin wrapper around the SdesData used in Rtcp.
    /// This class was created in order to shield the user from needing to understand Rtcp/Sdes,
    /// and give them a familiar concept that is easy to understand.
    /// 
    /// The most important aspect of a participant is their uniqueName.  It needs to be unique within
    /// the context of a session in order to differentiate them from other participants.
    /// A domain account, email, GUID, etc. would be good unique names.
    /// 
    /// The participant also contains some other properties, such as the IPAddress the participant
    /// originates from, as well as all the streams the participant "owns" (is sending).
    /// </summary>
    public class RtpParticipant : SdesData
    {
        #region Members

        /// <summary>
        /// The originating IP address of the participant
        /// </summary>
        private IPAddress ipAddress;
        
        /// <summary>
        /// The ssrc of this participant
        /// </summary>
        private uint ssrc;

        /// <summary>
        /// The SSRCs originating from this participant
        /// </summary>
        private List<uint> ssrcs;

        /// <summary>
        /// A marker indicating the staleness of the participant. A participant is considered stale
        /// if we aren't receiving Rtcp or Rtp traffic from them.
        /// 
        /// If a participant goes N Rtcp intervals (currently 5, see RtpSession.RtcpMissedIntervalsTimeout)
        /// without traffic, it is timed out and cleaned up by the manager, which checks this
        /// value during each RtcpSender.RtcpTransmissionInterval
        /// </summary>
        private int stale;

        #endregion Members

        #region Constructors

        /// <summary>
        /// Called when creating an RtpSession and providing it with the local participant's data
        /// </summary>
        /// <param name="cName">Canonical name - Unique network identifier for this participant</param>
        /// <param name="name">Friendly name for this participant</param>
        public RtpParticipant(string uniqueName, string friendlyName)
            : base(uniqueName, friendlyName) 
        {
            this.ipAddress = null;
            this.ssrcs = new List<uint>();
            this.stale = 0;

            SetPrivateExtension(Rtcp.PEP_SOURCE, Rtcp.PED_PARTICIPANT);
        }

        /// <summary>
        /// Called when an SdesPacket arrives with an ID (CNAME) that no other session Participant has
        /// </summary>
        /// <param name="rtcp">SdesData containing the information for the participant</param>
        /// <param name="ipAddress">IpAddress this participant originates from</param>
        internal RtpParticipant(SdesData data, IPAddress ipAddress) 
            : base(data)
        {
            this.ipAddress = ipAddress;
            this.ssrcs = new List<uint>();
            this.stale = 0;

            SetPrivateExtension(Rtcp.PEP_SOURCE, Rtcp.PED_PARTICIPANT);
        }

        
        #endregion Constructors

        #region Public

        /// <summary>
        /// The SSRCs originating from this participant
        /// </summary>
        public List<uint> SSRCs
        {
            get{return ssrcs;}
        }


        /// <summary>
        /// The ssrc of this participant
        /// </summary>
        public uint SSRC
        {
            get{return ssrc;}
            set{ssrc = value;}
        }


        /// <summary>
        /// The IP address of the participant.
        /// </summary>
        public IPAddress IPAddress
        {
            get{return ipAddress;}

            // We only want to allow setting this internally to the library
            internal set { ipAddress = value; }

        }


        /// <summary>
        /// Override for RtpParticipant
        /// </summary>
        /// <returns>Object state, represented as a string</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "Participant [ Properties := {0} ]", base.ToString());
        }
        
        
        #endregion Public

        #region Internal

        
        /// <summary>
        /// A marker indicating the staleness of the participant. A participant is considered stale
        /// if we aren't receiving Rtcp or Rtp traffic from them
        /// 
        /// If a participant goes N Rtcp intervals (currently 5, see RtpSession.RtcpMissedIntervalsTimeout)
        /// without traffic, it is timed out and cleaned up by the manager, which checks this
        /// value during each RtcpSender.RtcpTransmissionInterval
        /// </summary>
        internal int Stale
        {
            get{return stale;}
            set{stale = value;}
        }
        

        internal void AddSSRC(uint ssrc)
        {
            ssrcs.Add(ssrc);
        }

        internal void RemoveSSRC(uint ssrc)
        {
            ssrcs.Remove(ssrc);
        }


        /// <summary>
        /// Raises the ParticipantStatusChanged event if the data changed
        /// </summary>
        /// <param name="data"></param>
        internal new void UpdateData(SdesData data)
        {
            if(base.UpdateData(data))
            {
                object[] args = {this, new RtpEvents.RtpParticipantEventArgs(this)};
                EventThrower.QueueUserWorkItem(new RtpEvents.RaiseEvent(RtpEvents.RaiseRtpParticipantDataChangedEvent), args);
            }
        }


        #endregion Internal
    }
}
