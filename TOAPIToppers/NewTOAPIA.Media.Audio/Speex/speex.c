/* Copyright (C) 2002 Jean-Marc Valin 
   File: speex.c

   Basic Speex functions

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

#ifdef FIXED_POINT

#ifndef DISABLE_FLOAT_API
EXPORT int speex_encode(void *state, float *in, SpeexBits *bits)
{
   int i;
   spx_int32_t N;
   spx_int16_t short_in[MAX_IN_SAMPLES];
   speex_encoder_ctl(state, SPEEX_GET_FRAME_SIZE, &N);
   for (i=0;i<N;i++)
   {
      if (in[i]>32767.f)
         short_in[i] = 32767;
      else if (in[i]<-32768.f)
         short_in[i] = -32768;
      else
         short_in[i] = (spx_int16_t)floor(.5+in[i]);
   }
   return (*((SpeexMode**)state))->enc(state, short_in, bits);
}
#endif /* #ifndef DISABLE_FLOAT_API */

EXPORT int speex_encode_int(void *state, spx_int16_t *in, SpeexBits *bits)
{
   SpeexMode *mode;
   mode = *(SpeexMode**)state;
   return (mode)->enc(state, in, bits);
}

#ifndef DISABLE_FLOAT_API
EXPORT int speex_decode(void *state, SpeexBits *bits, float *out)
{
   int i, ret;
   spx_int32_t N;
   spx_int16_t short_out[MAX_IN_SAMPLES];
   speex_decoder_ctl(state, SPEEX_GET_FRAME_SIZE, &N);
   ret = (*((SpeexMode**)state))->dec(state, bits, short_out);
   for (i=0;i<N;i++)
      out[i] = short_out[i];
   return ret;
}
#endif /* #ifndef DISABLE_FLOAT_API */

EXPORT int speex_decode_int(void *state, SpeexBits *bits, spx_int16_t *out)
{
   SpeexMode *mode = *(SpeexMode**)state;
   return (mode)->dec(state, bits, out);
}

#else

EXPORT int speex_encode(void *state, float *in, SpeexBits *bits)
{
   return (*((SpeexMode**)state))->enc(state, in, bits);
}

EXPORT int speex_encode_int(void *state, spx_int16_t *in, SpeexBits *bits)
{
   int i;
   spx_int32_t N;
   float float_in[MAX_IN_SAMPLES];
   speex_encoder_ctl(state, SPEEX_GET_FRAME_SIZE, &N);
   for (i=0;i<N;i++)
      float_in[i] = in[i];
   return (*((SpeexMode**)state))->enc(state, float_in, bits);
}

EXPORT int speex_decode(void *state, SpeexBits *bits, float *out)
{
   return (*((SpeexMode**)state))->dec(state, bits, out);
}

EXPORT int speex_decode_int(void *state, SpeexBits *bits, spx_int16_t *out)
{
   int i;
   spx_int32_t N;
   float float_out[MAX_IN_SAMPLES];
   int ret;
   speex_decoder_ctl(state, SPEEX_GET_FRAME_SIZE, &N);
   ret = (*((SpeexMode**)state))->dec(state, bits, float_out);
   for (i=0;i<N;i++)
   {
      if (float_out[i]>32767.f)
         out[i] = 32767;
      else if (float_out[i]<-32768.f)
         out[i] = -32768;
      else
         out[i] = (spx_int16_t)floor(.5+float_out[i]);
   }
   return ret;
}
#endif



EXPORT int speex_encoder_ctl(void *state, int request, void *ptr)
{
   return (*((SpeexMode**)state))->enc_ctl(state, request, ptr);
}

EXPORT int speex_decoder_ctl(void *state, int request, void *ptr)
{
   return (*((SpeexMode**)state))->dec_ctl(state, request, ptr);
}



EXPORT int speex_lib_ctl(int request, void *ptr)
{
   switch (request)
   {
      case SPEEX_LIB_GET_MAJOR_VERSION:
         *((int*)ptr) = SPEEX_MAJOR_VERSION;
         break;
      case SPEEX_LIB_GET_MINOR_VERSION:
         *((int*)ptr) = SPEEX_MINOR_VERSION;
         break;
      case SPEEX_LIB_GET_MICRO_VERSION:
         *((int*)ptr) = SPEEX_MICRO_VERSION;
         break;
      case SPEEX_LIB_GET_EXTRA_VERSION:
         *((const char**)ptr) = SPEEX_EXTRA_VERSION;
         break;
      case SPEEX_LIB_GET_VERSION_STRING:
         *((const char**)ptr) = SPEEX_VERSION;
         break;
      /*case SPEEX_LIB_SET_ALLOC_FUNC:
         break;
      case SPEEX_LIB_GET_ALLOC_FUNC:
         break;
      case SPEEX_LIB_SET_FREE_FUNC:
         break;
      case SPEEX_LIB_GET_FREE_FUNC:
         break;*/
      default:
         speex_warning_int("Unknown wb_mode_query request: ", request);
         return -1;
   }
   return 0;
}
