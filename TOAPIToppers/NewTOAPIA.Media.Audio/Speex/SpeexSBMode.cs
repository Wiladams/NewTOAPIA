namespace NewTOAPIA.Media.Audio.Speex
{
    using System;

    using spx_int16_t = System.Int16;
    using spx_uint16_t = System.UInt16;
    using spx_int32_t = System.Int32;
    using spx_uint32_t = System.UInt32;
    using spx_word16_t = System.Int16;
    using spx_word32_t = System.Int32;
    using spx_sig_t = System.Single;
    using spx_coef_t = System.Single;

    /** Struct defining the encoding/decoding mode for SB-CELP (wideband) */
    public class SpeexSBMode : SpeexMode
    {
        public const int SB_SUBMODES = 8;
        public const int SB_SUBMODE_BITS = 3;

        SpeexMode nb_mode;    /**< Embedded narrowband mode */
        int frameSize;     /**< Size of frames used for encoding */
        int subframeSize;  /**< Size of sub-frames used for encoding */
        int lpcSize;       /**< Order of LPC filter */
        spx_word16_t gamma1;   /**< Perceptual filter parameter #1 */
        spx_word16_t gamma2;   /**< Perceptual filter parameter #1 */
        spx_word16_t lpc_floor;     /**< Noise floor for LPC analysis */
        spx_word16_t folding_gain;

        SpeexSubmode[] submodes = new SpeexSubmmode[SB_SUBMODES]; /**< Sub-mode data for the mode */
        int defaultSubmode; /**< Default sub-mode to use when encoding */
        int[] low_quality_map = new int[11]; /**< Mode corresponding to each quality setting */
        int[] quality_map = new int[11]; /**< Mode corresponding to each quality setting */
        //float[] (*vbr_thresh) = new [11];
        int nb_modes;
    }
}