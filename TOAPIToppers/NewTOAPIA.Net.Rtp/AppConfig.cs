using System;


namespace NewTOAPIA.Net.Rtp
{
    internal class AppConfig
    {
        private const string RTP = "NewTOAPIA.Net.Rtp.";

        public const string RTP_TimeToLive              = RTP + "TimeToLive";

        private const string CD = "NewTOAPIA.Net.ConnectivityDetector.";

        public const string CD_UpdateIntervalSeconds = CD + "UpdateIntervalSeconds";
        public const string CD_IPAddress = CD + "IPAddress";
        public const string CD_Port = CD + "Port";
    }
}