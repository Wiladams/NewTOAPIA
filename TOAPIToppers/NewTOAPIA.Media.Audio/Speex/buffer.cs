/* Copyright (C) 2007 Jean-Marc Valin
      
   File: buffer.c
   This is a very simple ring buffer implementation. It is not thread-safe
   so you need to do your own locking.

   Redistribution and use in source and binary forms, with or without
   modification, are permitted provided that the following conditions are
   met:

   1. Redistributions of source code must retain the above copyright notice,
   this list of conditions and the following disclaimer.

   2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.

   3. The name of the author may not be used to endorse or promote products
   derived from this software without specific prior written permission.

   THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR
   IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
   OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
   DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
   INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
   (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
   SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
   HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
   STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
   ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
   POSSIBILITY OF SUCH DAMAGE.
*/

namespace NewTOAPIA.Media.Audio.Speex
{
    using System;

    public class SpeexBuffer
    {
        byte[] data;
        int size;
        int read_ptr;
        int write_ptr;
        int available;

        public SpeexBuffer(int size)
        {
            this.data = speex.alloc(size);
            this.size = size;
            this.read_ptr = 0;
            this.write_ptr = 0;
            this.available = 0;
        }

        public int write(byte[] _data, int len)
        {
            int end;
            int end1;
            int dataoffset = 0;

            if (len > this.size)
            {
                dataoffset += len - this.size;
                len = this.size;
            }

            end = this.write_ptr + len;
            end1 = end;
            
            if (end1 > this.size)
                end1 = this.size;
            
            speex.SPEEX_COPY(this.data, this.write_ptr, _data, dataoffset, end1 - this.write_ptr);
            
            if (end > this.size)
            {
                end -= this.size;
                speex.SPEEX_COPY(this.data, 0, data, end1 - this.write_ptr, end);
            }
            this.available += len;
            if (this.available > this.size)
            {
                this.available = this.size;
                this.read_ptr = this.write_ptr;
            }
            this.write_ptr += len;
            if (this.write_ptr > this.size)
                this.write_ptr -= this.size;
            return len;
        }

        public int writezeros(int len)
        {
            /* This is almost the same as for write() but using 
            SPEEX_MEMSET() instead of SPEEX_COPY(). Update accordingly. */
            int end;
            int end1;
            if (len > this.size)
            {
                len = this.size;
            }
            end = this.write_ptr + len;
            end1 = end;
            if (end1 > this.size)
                end1 = this.size;
            speex.SPEEX_MEMSET(this.data, this.write_ptr, 0, end1 - this.write_ptr);
            if (end > this.size)
            {
                end -= this.size;
                speex.SPEEX_MEMSET(this.data, 0, end);
            }
            this.available += len;
            if (this.available > this.size)
            {
                this.available = this.size;
                this.read_ptr = this.write_ptr;
            }
            this.write_ptr += len;
            if (this.write_ptr > this.size)
                this.write_ptr -= this.size;
            return len;
        }

        public int read(byte[] _data, int len)
        {
            int end, end1;
            byte[] data = _data;
            if (len > this.available)
            {
                speex.SPEEX_MEMSET(data, this.available, 0, this.size - this.available);
                len = this.available;
            }
            end = this.read_ptr + len;
            end1 = end;
            if (end1 > this.size)
                end1 = this.size;
            speex.SPEEX_COPY(data, 0, this.data, this.read_ptr, end1 - this.read_ptr);

            if (end > this.size)
            {
                end -= this.size;
                speex.SPEEX_COPY(data, end1 - this.read_ptr, this.data, 0, end);
            }
            this.available -= len;
            this.read_ptr += len;
            if (this.read_ptr > this.size)
                this.read_ptr -= this.size;
            return len;
        }

        public int get_available()
        {
            return this.available;
        }

        public int resize(int len)
        {
            int old_len = this.size;
            if (len > old_len)
            {
                this.data = speex.realloc(this.data, len);
                /* FIXME: move data/pointers properly for growing the buffer */
            }
            else
            {
                /* FIXME: move data/pointers properly for shrinking the buffer */
                this.data = speex.realloc(this.data, len);
            }
            return len;
        }
    }
}