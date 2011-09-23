/* Copyright (C) 2002 Jean-Marc Valin*/
/**
  @file speex_callbacks.h
  @brief Describes callback handling and in-band signalling
*/
/*
   Redistribution and use in source and binary forms, with or without
   modification, are permitted provided that the following conditions
   are met:
   
   - Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.
   
   - Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.
   
   - Neither the name of the Xiph.org Foundation nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.
   
   THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
   ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
   LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
   A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL THE FOUNDATION OR
   CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
   EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
   PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
   PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
   LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
   NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
   SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

namespace NewTOAPIA.Media.Audio.Speex
{
    using spx_int16_t = System.Int16;
    using spx_uint16_t = System.UInt16;
    using spx_int32_t = System.Int32;
    using spx_uint32_t = System.UInt32;

    /** Handle in-band request */
    delegate int speex_inband_handler(SpeexBits bits, SpeexCallback callback_list, object state);

    /** Standard handler for mode request (change mode, no questions asked) */
    delegate int speex_std_mode_request_handler(SpeexBits bits, object state, object data);

    /** Standard handler for high mode request (change high mode, no questions asked) */
    delegate int speex_std_high_mode_request_handler(SpeexBits bits, object state, object data);

    /** Standard handler for in-band characters (write to stderr) */
    delegate int speex_std_char_handler(SpeexBits bits, object state, object data);

    /** Default handler for user-defined requests: in this case, just ignore */
    delegate int speex_default_user_handler(SpeexBits bits, object state, object data);


    /** Standard handler for low mode request (change low mode, no questions asked) */
    delegate int speex_std_low_mode_request_handler(SpeexBits bits, object state, object data);

    /** Standard handler for VBR request (Set VBR, no questions asked) */
    delegate int speex_std_vbr_request_handler(SpeexBits bits, object state, object data);

    /** Standard handler for enhancer request (Turn enhancer on/off, no questions asked) */
    delegate int speex_std_enh_request_handler(SpeexBits bits, object state, object data);

    /** Standard handler for VBR quality request (Set VBR quality, no questions asked) */
    delegate int speex_std_vbr_quality_request_handler(SpeexBits bits, object state, object data);

    /** Callback function type */
    delegate int speex_callback_func(SpeexBits bits, object state, object data);

    /** Callback information */
    public class SpeexCallback
    {
        /** Total number of callbacks */
        public const int SPEEX_MAX_CALLBACKS = 16;

        /* Describes all the in-band requests */

        /*These are 1-bit requests*/
        /** Request for perceptual enhancement (1 for on, 0 for off) */
        public const int SPEEX_INBAND_ENH_REQUEST = 0;
        /** Reserved */
        public const int SPEEX_INBAND_RESERVED1 = 1;

        /*These are 4-bit requests*/
        /** Request for a mode change */
        public const int SPEEX_INBAND_MODE_REQUEST = 2;
        /** Request for a low mode change */
        public const int SPEEX_INBAND_LOW_MODE_REQUEST = 3;
        /** Request for a high mode change */
        public const int SPEEX_INBAND_HIGH_MODE_REQUEST = 4;
        /** Request for VBR (1 on, 0 off) */
        public const int SPEEX_INBAND_VBR_QUALITY_REQUEST = 5;
        /** Request to be sent acknowledge */
        public const int SPEEX_INBAND_ACKNOWLEDGE_REQUEST = 6;
        /** Request for VBR (1 for on, 0 for off) */
        public const int SPEEX_INBAND_VBR_REQUEST = 7;

        /*These are 8-bit requests*/
        /** Send a character in-band */
        public const int SPEEX_INBAND_CHAR = 8;
        /** Intensity stereo information */
        public const int SPEEX_INBAND_STEREO = 9;

        /*These are 16-bit requests*/
        /** Transmit max bit-rate allowed */
        public const int SPEEX_INBAND_MAX_BITRATE = 10;

        /*These are 32-bit requests*/
        /** Acknowledge packet reception */
        public const int SPEEX_INBAND_ACKNOWLEDGE = 12;

        int callback_id;             /**< ID associated to the callback */
        speex_callback_func func;    /**< Callback handler function */
        object data;                  /**< Data that will be sent to the handler */
        //object reserved1;             /**< Reserved for future use */
        //int reserved2;             /**< Reserved for future use */

        public static int speex_inband_handler(SpeexBits bits, SpeexCallback[] callback_list, object state)
        {
            int id;
            SpeexCallback callback;
            /*speex_bits_advance(bits, 5);*/
            id = (int)bits.speex_bits_unpack_unsigned(4);
            callback = callback_list[id];

            if (callback.func != null)
            {
                return callback.func(bits, state, callback.data);
            }
            else
            /*If callback is not registered, skip the right number of bits*/
            {
                int adv;
                if (id < 2)
                    adv = 1;
                else if (id < 8)
                    adv = 4;
                else if (id < 10)
                    adv = 8;
                else if (id < 12)
                    adv = 16;
                else if (id < 14)
                    adv = 32;
                else
                    adv = 64;
                bits.speex_bits_advance(adv);
            }
            return 0;
        }

        public static int speex_std_mode_request_handler(SpeexBits bits, object state, object data)
        {
            spx_int32_t m;
            m = (spx_int32_t)bits.speex_bits_unpack_unsigned(4);
            speex_encoder_ctl(data, SPEEX_SET_MODE, &m);

            return 0;
        }

        public static int speex_std_low_mode_request_handler(SpeexBits bits, object state, object data)
        {
            spx_int32_t m;
            m = (spx_int32_t)bits.speex_bits_unpack_unsigned(4);
            speex_encoder_ctl(data, SPEEX_SET_LOW_MODE, &m);

            return 0;
        }

        public static int speex_std_high_mode_request_handler(SpeexBits bits, object state, object data)
        {
            spx_int32_t m;
            m = (spx_int32_t)bits.speex_bits_unpack_unsigned(4);
            speex_encoder_ctl(data, SPEEX_SET_HIGH_MODE, &m);
            return 0;
        }

        public static int speex_std_vbr_request_handler(SpeexBits bits, object state, object data)
        {
            spx_int32_t vbr;
            vbr = (spx_int32_t)bits.speex_bits_unpack_unsigned(1);
            speex_encoder_ctl(data, SPEEX_SET_VBR, &vbr);
            return 0;
        }

        public static int speex_std_enh_request_handler(SpeexBits bits, object state, object data)
        {
            spx_int32_t enh;
            enh = (spx_int32_t)bits.speex_bits_unpack_unsigned(1);
            speex_decoder_ctl(data, SPEEX_SET_ENH, &enh);
            return 0;
        }

        public static int speex_std_vbr_quality_request_handler(SpeexBits bits, object state, object data)
        {
            float qual;
            qual = bits.speex_bits_unpack_unsigned(4);
            speex_encoder_ctl(data, SPEEX_SET_VBR_QUALITY, &qual);
            return 0;
        }

        public static int speex_std_char_handler(SpeexBits bits, object state, object data)
        {
            byte ch;
            ch = (byte)bits.speex_bits_unpack_unsigned(8);
            _speex_putc(ch, data);
            /*printf("speex_std_char_handler ch=%x\n", ch);*/

            return 0;
        }

        /* Default handler for user callbacks: skip it */
        public static int speex_default_user_handler(SpeexBits bits, object state, object data)
        {
            int req_size = (int)bits.speex_bits_unpack_unsigned(4);
            bits.speex_bits_advance(5 + 8 * req_size);

            return 0;
        }
    }
}
