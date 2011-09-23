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


    /** Encoder state initialization function */
    delegate object encoder_init_func(SpeexMode mode);

    /** Encoder state destruction function */
    delegate void encoder_destroy_func(object st);

    /** Main encoding function */
    delegate int encode_func(object state, object inner, SpeexBits bits);

    /** Function for controlling the encoder options */
    delegate int encoder_ctl_func(void* state, int request, object ptr);

    /** Decoder state initialization function */
    delegate object decoder_init_func(SpeexMode mode);

    /** Decoder state destruction function */
    delegate void decoder_destroy_func(object st);

    /** Main decoding function */
    delegate int decode_func(object state, SpeexBits bits, object outer);

    /** Function for controlling the decoder options */
    delegate int decoder_ctl_func(object state, int request, object ptr);


    /** Query function for a mode */
    delegate int mode_query_func(object mode, int request, object ptr);

    /** Struct defining a Speex mode */
    public class SpeexMode
    {
        public const int MAX_IN_SAMPLES = 640;

        /* Used internally, NOT TO BE USED in applications */
        /** Used internally*/
        public const int SPEEX_GET_PI_GAIN = 100;
        /** Used internally*/
        public const int SPEEX_GET_EXC = 101;
        /** Used internally*/
        public const int SPEEX_GET_INNOV = 102;
        /** Used internally*/
        public const int SPEEX_GET_DTX_STATUS = 103;
        /** Used internally*/
        public const int SPEEX_SET_INNOVATION_SAVE = 104;
        /** Used internally*/
        public const int SPEEX_SET_WIDEBAND = 105;

        /** Used internally*/
        public const int SPEEX_GET_STACK = 106;


        /** Pointer to the low-level mode data */
        object mode;

        /** Pointer to the mode query function */
        mode_query_func query;

        /** The name of the mode (you should not rely on this to identify the mode)*/
        string modeName;

        /**ID of the mode*/
        int modeID;

        /**Version number of the bitstream (incremented every time we break
         bitstream compatibility*/
        int bitstream_version;

        /** Pointer to encoder initialization function */
        encoder_init_func enc_init;
        public static object speex_encoder_init(SpeexMode mode)
        {
            return mode.enc_init(mode);
        }

        /** Pointer to encoder destruction function */
        encoder_destroy_func enc_destroy;
        public void speex_encoder_destroy(object state)
        {
            (((SpeexMode)state)).enc_destroy(state);
        }

        /** Pointer to frame encoding function */
        encode_func enc;
        public int speex_encode_native(object state, spx_word16_t[] inner, SpeexBits bits)
        {
            return (((SpeexMode)state)).enc(state, inner, bits);
        }

        /** Pointer to decoder initialization function */
        decoder_init_func dec_init;
        public object speex_decoder_init(SpeexMode mode)
        {
            return mode.dec_init(mode);
        }

        /** Pointer to decoder destruction function */
        decoder_destroy_func dec_destroy;
        public void speex_decoder_destroy(object state)
        {
            (((SpeexMode)state)).dec_destroy(state);
        }

        /** Pointer to frame decoding function */
        decode_func dec;
        int speex_decode_native(object state, SpeexBits bits, spx_word16_t[] outer)
        {
            return (((SpeexMode)state)).dec(state, bits, outer);
        }

        /** ioctl-like requests for encoder */
        encoder_ctl_func enc_ctl;
        public int speex_encoder_ctl(SpeexMode state, int request, object ptr)
        {
            return state.enc_ctl(state, request, ptr);
        }


        /** ioctl-like requests for decoder */
        decoder_ctl_func dec_ctl;
        public int speex_decoder_ctl(SpeexMode state, int request, void* ptr)
        {
            return state.dec_ctl(state, request, ptr);
        }

    }
}
