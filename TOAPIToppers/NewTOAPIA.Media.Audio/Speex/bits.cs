/* Copyright (C) 2002 Jean-Marc Valin 
   File: speex_bits.c

   Handles bit packing/unpacking

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

    public class SpeexBits
    {
        public const int BITS_PER_CHAR = 8;
        public const int BYTES_PER_CHAR = 1;
        public const int LOG2_BITS_PER_CHAR = 3;

        /* Maximum size of the bit-stream (for fixed-size allocation) */
        public const int MAX_CHARS_PER_FRAME = (2000 / BYTES_PER_CHAR);

        byte[] chars;   /**< "raw" data */
        int nbBits;  /**< Total number of bits stored in the stream*/
        int charPtr; /**< Position of the byte "cursor" */
        int bitPtr;  /**< Position of the bit "cursor" within the current char */
        bool owner;   /**< Does the struct "own" the "raw" buffer (member "chars") */
        bool overflow;/**< Set to one if we try to read past the valid data */
        int buf_size;/**< Allocated size for buffer */
        //int reserved1; /**< Reserved for future use */
        //IntPtr reserved2; /**< Reserved for future use */


        public void speex_bits_init()
        {
            this.chars = new byte[MAX_CHARS_PER_FRAME];
            if (this.chars == null)
                return;

            this.buf_size = MAX_CHARS_PER_FRAME;

            this.owner = true;

            speex_bits_reset();
        }

        public void speex_bits_init_buffer(byte[] buff, int buf_size)
        {
            this.chars = buff;
            this.buf_size = buf_size;

            this.owner = false;

            speex_bits_reset();
        }

        public void speex_bits_set_bit_buffer(byte[] buff, int buf_size)
        {
            this.chars = buff;
            this.buf_size = buf_size;

            this.owner = false;

            this.nbBits = buf_size << LOG2_BITS_PER_CHAR;
            this.charPtr = 0;
            this.bitPtr = 0;
            this.overflow = false;

        }

        public void speex_bits_destroy()
        {
            if (this.owner)
                this.chars = null;
        }

        public void speex_bits_reset()
        {
            /* We only need to clear the first byte now */
            this.chars[0] = 0;
            this.nbBits = 0;
            this.charPtr = 0;
            this.bitPtr = 0;
            this.overflow = false;
        }

        public void speex_bits_rewind()
        {
            this.charPtr = 0;
            this.bitPtr = 0;
            this.overflow = false;
        }

        public void speex_bits_read_from(byte[] chars, int len)
        {
            int nchars = len / BYTES_PER_CHAR;
            if (nchars > this.buf_size)
            {
                //speex_notify("Packet is larger than allocated buffer");
                if (this.owner)
                {
                    byte[] tmp = speex.realloc(this.chars, nchars);
                    if (tmp != null)
                    {
                        this.buf_size = nchars;
                        this.chars = tmp;
                    }
                    else
                    {
                        nchars = this.buf_size;
                        speex.warning("Could not resize input buffer: truncating input");
                    }
                }
                else
                {
                    speex.warning("Do not own input buffer: truncating oversize input");
                    nchars = this.buf_size;
                }
            }
            //#if (BYTES_PER_CHAR==2)
            ///* Swap bytes to proper endian order (could be done externally) */
            //#define HTOLS(A) ((((A) >> 8)&0xff)|(((A) & 0xff)<<8))
            //#else
            //#define HTOLS(A) (A)
            //#endif
            //for (i=0;i<nchars;i++)
            //   this.chars[i]=HTOLS(chars[i]);

            this.nbBits = nchars << LOG2_BITS_PER_CHAR;
            this.charPtr = 0;
            this.bitPtr = 0;
            this.overflow = false;
        }

        public void speex_bits_flush()
        {
            int nchars = ((this.nbBits + BITS_PER_CHAR - 1) >> LOG2_BITS_PER_CHAR);
            if (this.charPtr > 0)
                speex.SPEEX_MOVE(this.chars, this.charPtr, nchars - this.charPtr);
            this.nbBits -= this.charPtr << LOG2_BITS_PER_CHAR;
            this.charPtr = 0;
        }

        public void speex_bits_read_whole_bytes(byte[] chars, int nbytes)
        {
            int pos;
            int nchars = nbytes / BYTES_PER_CHAR;

            if (((this.nbBits + BITS_PER_CHAR - 1) >> LOG2_BITS_PER_CHAR) + nchars > this.buf_size)
            {
                /* Packet is larger than allocated buffer */
                if (this.owner)
                {
                    byte[] tmp = speex.realloc(this.chars, (this.nbBits >> LOG2_BITS_PER_CHAR) + nchars + 1);
                    if (tmp != null)
                    {
                        this.buf_size = (this.nbBits >> LOG2_BITS_PER_CHAR) + nchars + 1;
                        this.chars = tmp;
                    }
                    else
                    {
                        nchars = this.buf_size - (this.nbBits >> LOG2_BITS_PER_CHAR) - 1;
                        speex.warning("Could not resize input buffer: truncating oversize input");
                    }
                }
                else
                {
                    speex.warning("Do not own input buffer: truncating oversize input");
                    nchars = this.buf_size;
                }
            }

            speex_bits_flush();
            pos = this.nbBits >> LOG2_BITS_PER_CHAR;
            //for (i = 0; i < nchars; i++)
            //    this.chars[pos + i] = HTOLS(chars[i]);
            this.nbBits += nchars << LOG2_BITS_PER_CHAR;
        }

        public int speex_bits_write(byte[] chars, int max_nbytes)
        {
            int max_nchars = max_nbytes / BYTES_PER_CHAR;
            int charPtr, bitPtr, nbBits;

            /* Insert terminator, but save the data so we can put it back after */
            bitPtr = this.bitPtr;
            charPtr = this.charPtr;
            nbBits = this.nbBits;
            speex_bits_insert_terminator();
            this.bitPtr = bitPtr;
            this.charPtr = charPtr;
            this.nbBits = nbBits;

            if (max_nchars > ((this.nbBits + BITS_PER_CHAR - 1) >> LOG2_BITS_PER_CHAR))
                max_nchars = ((this.nbBits + BITS_PER_CHAR - 1) >> LOG2_BITS_PER_CHAR);

            //for (i = 0; i < max_nchars; i++)
            //    chars[i] = HTOLS(this.chars[i]);
            return max_nchars * BYTES_PER_CHAR;
        }

        public int speex_bits_write_whole_bytes(byte[] chars, int max_nbytes)
        {
            int max_nchars = max_nbytes / BYTES_PER_CHAR;

            if (max_nchars > ((this.nbBits) >> LOG2_BITS_PER_CHAR))
                max_nchars = ((this.nbBits) >> LOG2_BITS_PER_CHAR);
            //for (i=0;i<max_nchars;i++)
            //   chars[i]=HTOLS(this.chars[i]);

            if (this.bitPtr > 0)
                this.chars[0] = this.chars[max_nchars];
            else
                this.chars[0] = 0;
            this.charPtr = 0;
            this.nbBits &= (BITS_PER_CHAR - 1);

            return max_nchars * BYTES_PER_CHAR;
        }

        public void speex_bits_pack(int data, int nbBits)
        {
            uint d = (uint)data;

            if (this.charPtr + ((nbBits + this.bitPtr) >> LOG2_BITS_PER_CHAR) >= this.buf_size)
            {
                speex.notify("Buffer too small to pack bits");
                if (this.owner)
                {
                    int new_nchars = ((this.buf_size + 5) * 3) >> 1;
                    byte[] tmp = speex.realloc(this.chars, new_nchars);
                    if (tmp != null)
                    {
                        this.buf_size = new_nchars;
                        this.chars = tmp;
                    }
                    else
                    {
                        speex.warning("Could not resize input buffer: not packing");
                        return;
                    }
                }
                else
                {
                    speex.warning("Do not own input buffer: not packing");
                    return;
                }
            }

            while (nbBits > 0)
            {
                int bit;
                bit = (int)(d >> (nbBits - 1)) & 1;
                this.chars[this.charPtr] |= (byte)(bit << (BITS_PER_CHAR - 1 - this.bitPtr));
                this.bitPtr++;

                if (this.bitPtr == BITS_PER_CHAR)
                {
                    this.bitPtr = 0;
                    this.charPtr++;
                    this.chars[this.charPtr] = 0;
                }
                this.nbBits++;
                nbBits--;
            }
        }

        public int speex_bits_unpack_signed(int nbBits)
        {
            uint d = speex_bits_unpack_unsigned(nbBits);
            /* If number is negative */
            if ((d >> (nbBits - 1)) > 0)
            {
                d |= (uint)((-1) << nbBits);
            }
            return (int)d;
        }

        public uint speex_bits_unpack_unsigned(int nbBits)
        {
            uint d = 0;
            if ((this.charPtr << LOG2_BITS_PER_CHAR) + this.bitPtr + nbBits > this.nbBits)
                this.overflow = true;
            if (this.overflow)
                return 0;
            while (nbBits > 0)
            {
                d <<= 1;
                d |= (uint)((this.chars[this.charPtr] >> (BITS_PER_CHAR - 1 - this.bitPtr)) & 1);
                this.bitPtr++;
                if (this.bitPtr == BITS_PER_CHAR)
                {
                    this.bitPtr = 0;
                    this.charPtr++;
                }
                nbBits--;
            }
            return d;
        }

        public uint speex_bits_peek_unsigned(int nbBits)
        {
            uint d = 0;
            int bitPtr, charPtr;
            byte[] lchars;

            if ((this.charPtr << LOG2_BITS_PER_CHAR) + this.bitPtr + nbBits > this.nbBits)
                this.overflow = true;
            if (this.overflow)
                return 0;

            bitPtr = this.bitPtr;
            charPtr = this.charPtr;
            lchars = this.chars;
            while (nbBits > 0)
            {
                d <<= 1;
                d |= (uint)(lchars[charPtr] >> (BITS_PER_CHAR - 1 - bitPtr)) & 1;
                bitPtr++;
                if (bitPtr == BITS_PER_CHAR)
                {
                    bitPtr = 0;
                    charPtr++;
                }
                nbBits--;
            }

            return d;
        }

        public int speex_bits_peek()
        {
            if ((this.charPtr << LOG2_BITS_PER_CHAR) + this.bitPtr + 1 > this.nbBits)
                this.overflow = true;
            if (this.overflow)
                return 0;

            return (this.chars[this.charPtr] >> (BITS_PER_CHAR - 1 - this.bitPtr)) & 1;
        }

        public void speex_bits_advance(int n)
        {
            if (((this.charPtr << LOG2_BITS_PER_CHAR) + this.bitPtr + n > this.nbBits) || this.overflow)
            {
                this.overflow = true;
                return;
            }
            this.charPtr += (this.bitPtr + n) >> LOG2_BITS_PER_CHAR; /* divide by BITS_PER_CHAR */
            this.bitPtr = (this.bitPtr + n) & (BITS_PER_CHAR - 1);       /* modulo by BITS_PER_CHAR */
        }

        public int speex_bits_remaining()
        {
            if (this.overflow)
                return -1;
            else
                return this.nbBits - ((this.charPtr << LOG2_BITS_PER_CHAR) + this.bitPtr);
        }

        public int speex_bits_nbytes()
        {
            return ((this.nbBits + BITS_PER_CHAR - 1) >> LOG2_BITS_PER_CHAR);
        }

        public void speex_bits_insert_terminator()
        {
            if (this.bitPtr > 0)
                speex_bits_pack(0, 1);
            while (this.bitPtr > 0)
                speex_bits_pack(1, 1);
        }
    }
}