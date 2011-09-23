/* Copyright (C) 2002 Jean-Marc Valin 
   File: stereo.c

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
    using System;

     using spx_int16_t = System.Int16;
     using spx_uint16_t = System.UInt16;
     using spx_int32_t = System.Int32;
     using spx_uint32_t = System.UInt32;
    using spx_word32_t = System.Int32;
    using spx_word16_t = System.Int16;

struct RealSpeexStereoState {
   public spx_word32_t balance;      /**< Left/right balance info */
   public spx_word32_t e_ratio;      /**< Ratio of energies: E(left+right)/[E(left)+E(right)]  */
   public spx_word32_t smooth_left;  /**< Smoothed left channel gain */
   public spx_word32_t smooth_right; /**< Smoothed right channel gain */
   public spx_uint32_t reserved1;     /**< Reserved for future use */
   public spx_int32_t reserved2;     /**< Reserved for future use */
} 


public class SpeexStereoState 
{
   float balance;      /**< Left/right balance info */
   float e_ratio;      /**< Ratio of energies: E(left+right)/[E(left)+E(right)]  */
   float smooth_left;  /**< Smoothed left channel gain */
   float smooth_right; /**< Smoothed right channel gain */
   float reserved1;    /**< Reserved for future use */
   float reserved2;    /**< Reserved for future use */


/*float e_ratio_quant[4] = {1, 1.26, 1.587, 2};*/
//#ifndef FIXED_POINT
static float[] e_ratio_quant = {.25f, .315f, .397f, .5f};
static float[] e_ratio_quant_bounds = {0.2825f, 0.356f, 0.4485f};
//#else
//static const spx_word16_t e_ratio_quant[4] = {8192, 10332, 13009, 16384};
//static const spx_word16_t e_ratio_quant_bounds[3] = {9257, 11665, 14696};
//static const spx_word16_t balance_bounds[31] = {18, 23, 30, 38, 49, 63,  81, 104,
//   134, 172, 221,  284, 364, 468, 600, 771,
//   990, 1271, 1632, 2096, 2691, 3455, 4436, 5696,
//   7314, 9392, 12059, 15484, 19882, 25529, 32766};
//#endif

/* This is an ugly compatibility hack that properly resets the stereo state
   In case it it compiled in fixed-point, but initialised with the deprecated
   floating point static initialiser */
//#ifdef FIXED_POINT
//#define COMPATIBILITY_HACK(s) do {if ((s)->reserved1 != 0xdeadbeef) speex_stereo_state_reset((SpeexStereoState*)s); } while (0);
//#else
//#define COMPATIBILITY_HACK(s) 
//#endif

public SpeexStereoState()
{
   //SpeexStereoState stereo = speex_alloc(sizeof(SpeexStereoState));
   speex_stereo_state_reset();
}

public void speex_stereo_state_reset()
{
   balance = 1.0f;
   e_ratio = .5f;
   smooth_left = 1.f;
   smooth_right = 1.f;
   reserved1 = 0;
   reserved2 = 0;
}


public void speex_encode_stereo(float[] data, int frame_size, SpeexBits bits)
{
   int i, tmp;
   float e_left=0, e_right=0, e_tot=0;
   float balance, e_ratio;
   for (i=0;i<frame_size;i++)
   {
      e_left  += ((float)data[2*i])*data[2*i];
      e_right += ((float)data[2*i+1])*data[2*i+1];
      data[i] =  .5*(((float)data[2*i])+data[2*i+1]);
      e_tot   += ((float)data[i])*data[i];
   }
   balance=(e_left+1)/(e_right+1);
   e_ratio = e_tot/(1+e_left+e_right);

   /*Quantization*/
   bits.speex_bits_pack(14, 5);
   bits.speex_bits_pack(SPEEX_INBAND_STEREO, 4);
   
   balance=4*log(balance);

   /*Pack sign*/
   if (balance>0)
      bits.speex_bits_pack(0, 1);
   else
      bits.speex_bits_pack(1, 1);
   balance=floor(.5+Math.Abs(balance));
   if (balance>30)
      balance=31;
   
   bits.speex_bits_pack((int)balance, 5);
   
   /* FIXME: this is a hack */
   tmp=scal_quant(e_ratio*Q15_ONE, e_ratio_quant_bounds, 4);
   bits.speex_bits_pack(tmp, 2);
}

public void speex_encode_stereo_int(spx_int16_t[] data, int frame_size, SpeexBits bits)
{
   int i, tmp;
   spx_word32_t e_left=0, e_right=0, e_tot=0;
   spx_word32_t balance, e_ratio;
   spx_word32_t largest, smallest;
   int balance_id;
   
   /* In band marker */
   bits.speex_bits_pack(14, 5);
   /* Stereo marker */
   bits.speex_bits_pack(SPEEX_INBAND_STEREO, 4);

   for (i=0;i<frame_size;i++)
   {
      e_left  += SHR32(MULT16_16(data[2*i],data[2*i]),8);
      e_right += SHR32(MULT16_16(data[2*i+1],data[2*i+1]),8);
      data[i] =  .5*(((float)data[2*i])+data[2*i+1]);
      e_tot   += SHR32(MULT16_16(data[i],data[i]),8);
   }
   if (e_left > e_right)
   {
      speex_bits_pack(bits, 0, 1);
      largest = e_left;
      smallest = e_right;
   } else {
      speex_bits_pack(bits, 1, 1);
      largest = e_right;
      smallest = e_left;
   }

   /* Balance quantization */
   balance=(largest+1.0f)/(smallest+1.0f);
   balance=4*log(balance);
   balance_id=floor(.5+fabs(balance));
   if (balance_id>30)
      balance_id=31;
   
   speex_bits_pack(bits, balance_id, 5);
   
   /* "coherence" quantisation */
   e_ratio = e_tot/(1.0f+e_left+e_right);
   
   tmp=scal_quant(EXTRACT16(e_ratio), e_ratio_quant_bounds, 4);
   /*fprintf (stderr, "%d %d %d %d\n", largest, smallest, balance_id, e_ratio);*/
   speex_bits_pack(bits, tmp, 2);
}

//#ifndef DISABLE_FLOAT_API
//public void speex_decode_stereo(float *data, int frame_size, SpeexStereoState *_stereo)
//{
//   int i;
//   spx_word32_t balance;
//   spx_word16_t e_left, e_right, e_ratio;
//   RealSpeexStereoState *stereo = (RealSpeexStereoState*)_stereo;
   
//   //COMPATIBILITY_HACK(stereo);
   
//   balance=stereo->balance;
//   e_ratio=stereo->e_ratio;
   
//   /* These two are Q14, with max value just below 2. */
//   e_right = DIV32(QCONST32(1., 22), spx_sqrt(MULT16_32_Q15(e_ratio, ADD32(QCONST32(1., 16), balance))));
//   e_left = SHR32(MULT16_16(spx_sqrt(balance), e_right), 8);

//   for (i=frame_size-1;i>=0;i--)
//   {
//      spx_word16_t tmp=data[i];
//      stereo->smooth_left = EXTRACT16(PSHR32(MAC16_16(MULT16_16(stereo->smooth_left, QCONST16(0.98, 15)), e_left, QCONST16(0.02, 15)), 15));
//      stereo->smooth_right = EXTRACT16(PSHR32(MAC16_16(MULT16_16(stereo->smooth_right, QCONST16(0.98, 15)), e_right, QCONST16(0.02, 15)), 15));
//      data[2*i] = (float)MULT16_16_P14(stereo->smooth_left, tmp);
//      data[2*i+1] = (float)MULT16_16_P14(stereo->smooth_right, tmp);
//   }
//}
//#endif /* #ifndef DISABLE_FLOAT_API */

public void speex_decode_stereo_int(spx_int16_t[] data, int frame_size, SpeexStereoState _stereo)
{
   int i;
   spx_word32_t balance;
   spx_word16_t e_left, e_right, e_ratio;
   //RealSpeexStereoState *stereo = (RealSpeexStereoState*)_stereo;

   
   balance=stereo->balance;
   e_ratio=stereo->e_ratio;
   
   /* These two are Q14, with max value just below 2. */
   e_right = DIV32(QCONST32(1.0f, 22), spx_sqrt(MULT16_32_Q15(e_ratio, ADD32(QCONST32(1.0f, 16), balance))));
   e_left = SHR32(MULT16_16(spx_sqrt(balance), e_right), 8);

   for (i=frame_size-1;i>=0;i--)
   {
      spx_int16_t tmp=data[i];
      stereo->smooth_left = EXTRACT16(PSHR32(MAC16_16(MULT16_16(stereo->smooth_left, QCONST16(0.98, 15)), e_left, QCONST16(0.02, 15)), 15));
      stereo->smooth_right = EXTRACT16(PSHR32(MAC16_16(MULT16_16(stereo->smooth_right, QCONST16(0.98, 15)), e_right, QCONST16(0.02, 15)), 15));
      data[2*i] = (spx_int16_t)MULT16_16_P14(stereo->smooth_left, tmp);
      data[2*i+1] = (spx_int16_t)MULT16_16_P14(stereo->smooth_right, tmp);
   }
}

public int speex_std_stereo_request_handler(SpeexBits bits, object state, object data)
{
   RealSpeexStereoState stereo;
   spx_word16_t sign=1, dexp;
   int tmp;

   stereo = (RealSpeexStereoState)data;
   

   if (bits.speex_bits_unpack_unsigned(1))
      sign=-1;
   dexp = bits.speex_bits_unpack_unsigned(5);
   stereo.balance = exp(sign*.25*dexp);
   tmp = bits.speex_bits_unpack_unsigned(2);
   stereo.e_ratio = e_ratio_quant[tmp];

   return 0;
}
    }
}
