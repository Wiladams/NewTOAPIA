/* Copyright (C) 2002 Jean-Marc Valin 
   File: speex_header.c
   Describes the Speex header

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
    using System.Runtime.InteropServices;

    using spx_int16_t = System.Int16;
    using spx_uint16_t = System.UInt16;
    using spx_int32_t = System.Int32;
    using spx_uint32_t = System.UInt32;

    /** Speex header info for file-based formats */
    [StructLayout(LayoutKind = LayoutKind.Sequential)]
    public class SpeexHeader
    {
        /** Length of the Speex header identifier */
        public const int SPEEX_HEADER_STRING_LENGTH = 8;

        /** Maximum number of characters for encoding the Speex version number in the header */
        public const int SPEEX_HEADER_VERSION_LENGTH = 20;

        byte[] speex_string = new byte[SPEEX_HEADER_STRING_LENGTH];   /**< Identifies a Speex bit-stream, always set to "Speex   " */
        byte[] speex_version = new byte[SPEEX_HEADER_VERSION_LENGTH]; /**< Speex version */
        spx_int32_t speex_version_id;       /**< Version for Speex (for checking compatibility) */
        spx_int32_t header_size;            /**< Total size of the header ( sizeof(SpeexHeader) ) */
        spx_int32_t rate;                   /**< Sampling rate used */
        spx_int32_t mode;                   /**< Mode used (0 for narrowband, 1 for wideband) */
        spx_int32_t mode_bitstream_version; /**< Version ID of the bit-stream */
        spx_int32_t nb_channels;            /**< Number of channels encoded */
        spx_int32_t bitrate;                /**< Bit-rate used */
        spx_int32_t frame_size;             /**< Size of frames */
        spx_int32_t vbr;                    /**< 1 for a VBR encoding, 0 otherwise */
        spx_int32_t frames_per_packet;      /**< Number of frames stored per Ogg packet */
        spx_int32_t extra_headers;          /**< Number of additional headers after the comments */
        spx_int32_t reserved1;              /**< Reserved for future use, must be zero */
        spx_int32_t reserved2;              /**< Reserved for future use, must be zero */


        /** Convert little endian */
        static spx_int32_t le_int(spx_int32_t i)
        {
            //#if !defined(__LITTLE_ENDIAN__) && ( defined(WORDS_BIGENDIAN) || defined(__BIG_ENDIAN__) )
            //   spx_uint32_t ui, ret;
            //   ui = i;
            //   ret =  ui>>24;
            //   ret |= (ui>>8)&0x0000ff00;
            //   ret |= (ui<<8)&0x00ff0000;
            //   ret |= (ui<<24);
            //   return ret;
            //#else
            return i;
            //#endif
        }

        static int ENDIAN_SWITCH(int x)
        {
            x = le_int(x);
            return x;
        }


        public SpeexHeader(SpeexHeader header)
        {
        }

        public SpeexHeader(int rate, int nb_channels, SpeexMode m)
        {
            int i;
            byte[] h = { 'S', 'p', 'e', 'e', 'x', ' ', ' ', ' ' };

            /*
            strncpy(header->speex_string, "Speex   ", 8);
            strncpy(header->speex_version, SPEEX_VERSION, SPEEX_HEADER_VERSION_LENGTH-1);
            header->speex_version[SPEEX_HEADER_VERSION_LENGTH-1]=0;
            */
            for (i = 0; i < 8; i++)
                this.speex_string[i] = h[i];
            for (i = 0; i < SPEEX_HEADER_VERSION_LENGTH - 1 && SPEEX_VERSION[i]; i++)
                this.speex_version[i] = SPEEX_VERSION[i];
            for (; i < SPEEX_HEADER_VERSION_LENGTH; i++)
                this.speex_version[i] = 0;

            this.speex_version_id = 1;
            this.header_size = Marshal.SizeOf(this);

            this.rate = rate;
            this.mode = m.modeID;
            this.mode_bitstream_version = m.bitstream_version;

            if (m.modeID < 0)
                speex.warning("This mode is meant to be used alone");

            this.nb_channels = nb_channels;
            this.bitrate = -1;
            speex_mode_query(m, SPEEX_MODE_FRAME_SIZE, &this.frame_size);
            this.vbr = 0;

            this.frames_per_packet = 0;
            this.extra_headers = 0;
            this.reserved1 = 0;
            this.reserved2 = 0;
        }

        public byte[] speex_header_to_packet(SpeexHeader header, out int size)
        {
            SpeexHeader le_header = new SpeexHeader(header);

            speex.SPEEX_COPY(le_header, header, 1);

            /*Make sure everything is now little-endian*/
            ENDIAN_SWITCH(le_header.speex_version_id);
            ENDIAN_SWITCH(le_header.header_size);
            ENDIAN_SWITCH(le_header.rate);
            ENDIAN_SWITCH(le_header.mode);
            ENDIAN_SWITCH(le_header.mode_bitstream_version);
            ENDIAN_SWITCH(le_header.nb_channels);
            ENDIAN_SWITCH(le_header.bitrate);
            ENDIAN_SWITCH(le_header.frame_size);
            ENDIAN_SWITCH(le_header.vbr);
            ENDIAN_SWITCH(le_header.frames_per_packet);
            ENDIAN_SWITCH(le_header.extra_headers);

            size = Marshal.SizeOf(SpeexHeader);
            return (byte[])le_header;
        }

        public SpeexHeader speex_packet_to_header(byte[] packet, int size)
        {
            int i;
            SpeexHeader le_header;
            const char* h = "Speex   ";
            for (i = 0; i < 8; i++)
                if (packet[i] != h[i])
                {
                    speex.notify("This doesn't look like a Speex file");
                    return null;
                }

            /*FIXME: Do we allow larger headers?*/
            if (size < (int)Marshal.SizeOf(SpeexHeader))
            {
                speex.notify("Speex header too small");
                return null;
            }

            le_header = new SpeexHeader();

            speex.SPEEX_COPY(le_header, (SpeexHeader)packet, 1);

            /*Make sure everything is converted correctly from little-endian*/
            ENDIAN_SWITCH(le_header.speex_version_id);
            ENDIAN_SWITCH(le_header.header_size);
            ENDIAN_SWITCH(le_header.rate);
            ENDIAN_SWITCH(le_header.mode);
            ENDIAN_SWITCH(le_header.mode_bitstream_version);
            ENDIAN_SWITCH(le_header.nb_channels);
            ENDIAN_SWITCH(le_header.bitrate);
            ENDIAN_SWITCH(le_header.frame_size);
            ENDIAN_SWITCH(le_header.vbr);
            ENDIAN_SWITCH(le_header.frames_per_packet);
            ENDIAN_SWITCH(le_header.extra_headers);

            if (le_header.mode >= SPEEX_NB_MODES || le_header.mode < 0)
            {
                speex.notify("Invalid mode specified in Speex header");
                //speex_free (le_header);
                return null;
            }

            if (le_header.nb_channels > 2)
                le_header.nb_channels = 2;
            if (le_header.nb_channels < 1)
                le_header.nb_channels = 1;

            return le_header;

        }

    }
}