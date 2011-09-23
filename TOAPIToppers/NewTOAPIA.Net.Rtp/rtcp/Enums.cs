namespace NewTOAPIA.Net.Rtp
{
    /// <summary>
    /// Rtcp SDES Types, per RFC 3550 spec
    /// </summary>
    public enum SDESType : byte
    {
        END = 0,    // Indicates the end of SDES item processing when SDES data is received. Has no meaning with rtp_set_sdes and rtp_get_sdes.
        CNAME = 1,  // The canonical name associated with participant. It is algorithmically derived and should never be changed.
        NAME = 2,   // The local participant's name, typically displayed in RTP session participant list. The name can take any form, and should remain constant during a session to avoid confusion.
        EMAIL = 3,  // The local participant's email address (optional).
        PHONE = 4,  // The local participant's telephone number (optional).
        LOC = 5,    // The local participant's geographic location (optional).
        TOOL = 6,   // The local participant's tool (optional).
        NOTE = 7,   // Any additional information the local participant wishes to communicate about themselves (optional).
        PRIV = 8    // Private extension SDES item see RFC1889 for details. 
    }
}