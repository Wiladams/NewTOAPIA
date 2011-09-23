using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Net.Rtp
{
    #region Public Enumerations
    /// <summary>
    /// Enumeration for Rtp PayloadTypes using values per RFC 1890.  PayloadType was previously used to tightly couple the data stream to an
    /// exact data type, but this has been falling out of favor as payload types diverge and systems such as DirectShow and QuickTime carry
    /// the media type in much greater detail in band with the data.
    /// 
    /// We use the terms 'dynamicVideo' and 'dynamicAudio', specified at the end of the dynamic band of PayloadTypes and then include a
    /// DirectShow AM_MEDIA_TYPE structure in the Rtp header extension of the first packet in a chunk.  See the packet format on the website
    /// for detailed information on how this is transmitted.
    /// </summary>
    /// <example>
    ///       PT         encoding      audio/video    clock rate    channels
    ///                  name          (A/V)          (Hz)          (audio)
    ///             _______________________________________________________________
    ///             0          PCMU          A              8000          1
    ///             1          1016          A              8000          1
    ///             2          G721          A              8000          1
    ///             3          GSM           A              8000          1
    ///             4          unassigned    A              8000          1
    ///             5          DVI4          A              8000          1
    ///             6          DVI4          A              16000         1
    ///             7          LPC           A              8000          1
    ///             8          PCMA          A              8000          1
    ///             9          G722          A              8000          1
    ///             10         L16           A              44100         2
    ///             11         L16           A              44100         1
    ///             12         unassigned    A
    ///             13         unassigned    A
    ///             14         MPA           A              90000        (see text)
    ///             15         G728          A              8000          1
    ///             16--23     unassigned    A
    ///             24         unassigned    V
    ///             25         CelB          V              90000
    ///             26         JPEG          V              90000
    ///             27         unassigned    V
    ///             28         nv            V              90000
    ///             29         unassigned    V
    ///             30         unassigned    V
    ///             31         H261          V              90000
    ///             32         MPV           V              90000
    ///             33         MP2T          AV             90000
    ///             34--71     unassigned    ?
    ///             72--76     reserved      N/A            N/A           N/A
    ///             77--95     unassigned    ?
    ///             96--127    dynamic       ?
    /// </example>
    public enum PayloadType : byte
    {
        PCMU = 0, 
        PT1016, 
        G721, 
        GSM,
        DVI4 = 5,
        LPC = 7, 
        PCMA, 
        G722, 
        L16,
        MPA = 14, 
        G728,
        CelB = 25, 
        JPEG,
        nv = 28,
        H261 = 31, 
        MPV, 
        MP2T,
        // 96-127 are intended for dynamic assignment
        Chat = 96,
        xApplication2,
        xApplication3,
        xApplication4,
        xApplication5,
        xApplication6,
        xApplication7,
        xApplication8,
        xApplication9,
        xApplication10,
        Venue1 = 106,
        Venue2,
        Venue3,
        Venue4,
        Venue5,
        Venue6,
        Venue7,
        Venue8,
        Venue9,
        GroupChat = 115,
        FileTransfer = 116,
        ManagedDirectX = 117,
        Whiteboard = 118,
        SharedBrowser = 119,
        RichTextChat = 120,
        RTDocument = 121,               // Serialization of an RTDocument object, network protocol TBD
        PipecleanerSignal = 122,        // Diagnostic signal used by the Pipecleaner applications to test connectivity between nodes
        Test = 123,                     // Used for test cases
        FEC = 124,                      // Identifies a packet as containing Forward Error Correction information
        dynamicPresentation = 125,      // Obsolete, being replaced by RTDocument -- lifetime TBD
        dynamicVideo = 126,             // A video signal.  The format of the video signal is embedded in the data stream itself
        dynamicAudio = 127,              // An audio signal.  The format of the audio signal is embedded in the data stream itself
    }
    #endregion
}
