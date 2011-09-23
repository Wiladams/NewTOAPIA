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

    /** Struct defining the encoding/decoding mode*/
    public class SpeexNBMode : SpeexMode
    {
        public const int NB_SUBMODES = 16;
        public const int NB_SUBMODE_BITS = 4;

        int frameSize;      /**< Size of frames used for encoding */
        int subframeSize;   /**< Size of sub-frames used for encoding */
        int lpcSize;        /**< Order of LPC filter */
        int pitchStart;     /**< Smallest pitch value allowed */
        int pitchEnd;       /**< Largest pitch value allowed */

        spx_word16_t gamma1;    /**< Perceptual filter parameter #1 */
        spx_word16_t gamma2;    /**< Perceptual filter parameter #2 */
        spx_word16_t lpc_floor;      /**< Noise floor for LPC analysis */

        SpeexSubmode[] submodes = new SpeexSubmode[NB_SUBMODES]; /**< Sub-mode data for the mode */
        int defaultSubmode; /**< Default sub-mode to use when encoding */
        int[] quality_map = new int[11]; /**< Mode corresponding to each quality setting */

        int nb_mode_query(object mode, int request, ref object ptr)
        {
            const SpeexNBMode m = (SpeexNBMode)mode;

            switch (request)
            {
                case speex.SPEEX_MODE_FRAME_SIZE:
                    ((int)ptr) = m.frameSize;
                    break;
                case speex.SPEEX_SUBMODE_BITS_PER_FRAME:
                    if ((int)ptr == 0)
                        ((int)ptr) = NB_SUBMODE_BITS + 1;
                    else if (m.submodes[((int)ptr)] == null)
                        ((int)ptr) = -1;
                    else
                        ((int)ptr) = m.submodes[((int)ptr)].bits_per_frame;
                    break;
                default:
                    speex.warning_int("Unknown nb_mode_query request: ", request);
                    return -1;
            }
            return 0;
        }
    }
}