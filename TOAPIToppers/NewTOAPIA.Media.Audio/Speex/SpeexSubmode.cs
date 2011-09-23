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

        /** Quantizes LSPs */
delegate void lsp_quant_func(spx_lsp_t *, spx_lsp_t *, int, SpeexBits bits);

/** Decodes quantized LSPs */
delegate void lsp_unquant_func(spx_lsp_t *, int, SpeexBits bits);


/** Long-term predictor quantization */
delegate int ltp_quant_func(spx_word16_t *, spx_word16_t *, spx_coef_t *, spx_coef_t *, 
                              spx_coef_t *, spx_sig_t *,  object, int, int, spx_word16_t, 
                              int, int, SpeexBits, byte[], spx_word16_t *, spx_word16_t *, int, int, int, spx_word32_t *);

/** Long-term un-quantize */
delegate void ltp_unquant_func(spx_word16_t *, spx_word32_t *, int, int, spx_word16_t, const void *, int, int *,
                                 spx_word16_t *, SpeexBits*, char*, int, int, spx_word16_t, int);


/** Innovation quantization function */
delegate void innovation_quant_func(spx_word16_t *, spx_coef_t *, spx_coef_t *, spx_coef_t *, const void *, int, int, 
                                      spx_sig_t *, spx_word16_t *, SpeexBits *, char *, int, int);

/** Innovation unquantization function */
delegate void innovation_unquant_func(spx_sig_t *, const void *, int, SpeexBits bits, char *, spx_int32_t *);


    /** Description of a Speex sub-mode (wither narrowband or wideband */
    public class SpeexSubmode
    {
        int lbr_pitch;          /**< Set to -1 for "normal" modes, otherwise encode pitch using a global pitch and allowing a +- lbr_pitch variation (for low not-rates)*/
        int forced_pitch_gain;  /**< Use the same (forced) pitch gain for all sub-frames */
        int have_subframe_gain; /**< Number of bits to use as sub-frame innovation gain */
        int double_codebook;    /**< Apply innovation quantization twice for higher quality (and higher bit-rate)*/

        /*LSP functions*/
        lsp_quant_func lsp_quant; /**< LSP quantization function */
        lsp_unquant_func lsp_unquant; /**< LSP unquantization function */

        /*Long-term predictor functions*/
        ltp_quant_func ltp_quant; /**< Long-term predictor (pitch) quantizer */
        ltp_unquant_func ltp_unquant; /**< Long-term predictor (pitch) un-quantizer */
        object ltp_params; /**< Pitch parameters (options) */

        /*Quantization of innovation*/
        innovation_quant_func innovation_quant; /**< Innovation quantization */
        innovation_unquant_func innovation_unquant; /**< Innovation un-quantization */
        object innovation_params; /**< Innovation quantization parameters*/

        spx_word16_t comb_gain;  /**< Gain of enhancer comb filter */

        int bits_per_frame; /**< Number of bits per frame after encoding*/
    }
}