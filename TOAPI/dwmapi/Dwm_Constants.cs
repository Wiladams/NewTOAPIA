using UINT = System.UInt32;

namespace TOAPI.DwmApi
{
    public partial class Dwm
    {
        // Blur behind data structures
        public const int DWM_BB_ENABLE                = 0x00000001;  // fEnable has been specified
        public const int DWM_BB_BLURREGION            = 0x00000002;  // hRgnBlur has been specified
        public const int DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;  // fTransitionOnMaximized has been specified

        public const int DWM_TNP_RECTDESTINATION      = 0x00000001;
        public const int DWM_TNP_RECTSOURCE           = 0x00000002;
        public const int DWM_TNP_OPACITY              = 0x00000004;
        public const int DWM_TNP_VISIBLE              = 0x00000008;
        public const int DWM_TNP_SOURCECLIENTAREAONLY = 0x00000010;

        public const UINT c_DwmMaxQueuedBuffers = 8;
        public const UINT c_DwmMaxMonitors = 16;
        public const UINT c_DwmMaxAdapters = 16;

        public const int DWM_FRAME_DURATION_DEFAULT = -1;

        public const int DWM_EC_DISABLECOMPOSITION  = 0;
        public const int DWM_EC_ENABLECOMPOSITION = 1;

    }
}
