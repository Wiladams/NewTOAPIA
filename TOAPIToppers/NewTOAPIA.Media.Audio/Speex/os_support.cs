/* Copyright (C) 2007 Jean-Marc Valin
      
   File: os_support.h
   This is the (tiny) OS abstraction layer. Aside from math.h, this is the
   only place where system headers are allowed.

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
    using System.IO;

    public partial class speex
    {
        /** Speex wrapper for calloc. To do your own dynamic allocation, all you need to do is replace this function, speex_realloc and speex_free 
            NOTE: speex_alloc needs to CLEAR THE MEMORY */
        public static byte[] alloc(int size)
        {
            /* WARNING: this is not equivalent to malloc(). If you want to use malloc() 
               or your own allocator, YOU NEED TO CLEAR THE MEMORY ALLOCATED. Otherwise
               you will experience strange bugs */
            return new byte[size];
        }

        /** Same as speex_alloc, except that the area is only needed inside a Speex call (might cause problem with wideband though) */
        public static byte[] alloc_scratch(int size)
        {
            /* Scratch space doesn't need to be cleared */
            return alloc(size);
        }

        /** Speex wrapper for realloc. To do your own dynamic allocation, all you need to do is replace this function, speex_alloc and speex_free */
        public static byte[] realloc(byte[] ptr, int size)
        {
            byte[] newBytes = new byte[size];
            int nBytes = Math.Min(ptr.Length, size);

            for (int i = 0; i < nBytes; i++)
                newBytes[i] = ptr[i];

            return newBytes;
        }

        /** Speex wrapper for calloc. To do your own dynamic allocation, all you need to do is replace this function, speex_realloc and speex_alloc */
        public static void free(byte[] ptr)
        {
        }

        /** Same as speex_free, except that the area is only needed inside a Speex call (might cause problem with wideband though) */
        public static void free_scratch(byte[] ptr)
        {
            free(ptr);
        }

        /** Copy n bytes of memory from src to dst.  */
        public static void SPEEX_COPY(byte[] dst, byte[] src, int n)
        {
            SPEEX_COPY(dst, 0, src, 0, n);
        }

        public static void SPEEX_COPY(byte[] dst, int dstoffset, byte[] src, int srcoffset, int n)
        {
            for (int i = 0; i < n; i++)
                dst[i+dstoffset] = src[i+srcoffset];
        }

        /** Copy n bytes of memory from src to dst, allowing overlapping regions. 
         */
        public static void SPEEX_MOVE(byte[] dst, int offset, int n)
        {
            for (int i = 0; i < n; i++)
                dst[i] = dst[offset + i];
        }

        /** Set n bytes of memory to value of c, starting at address s */
        public static void SPEEX_MEMSET(byte[] dst, byte c, int n)
        {
            for (int i = 0; i < n; i++)
                dst[i] = c;
        }

        public static void SPEEX_MEMSET(byte[] dst, int offset, byte c, int n)
        {
            for (int i = 0; i < n; i++)
                dst[i+offset] = c;
        }

        public static void _speex_fatal(string str, string file, int line)
        {
            string msg = string.Format("Fatal (internal) error in {0}, line {1}: {2}", file, line, str);

            throw new Exception(msg);
        }

        public static void warning(string str)
        {
            Console.Error.WriteLine("warning: {0}", str);
        }

        public static void warning_int(string str, int val)
        {
            Console.Error.WriteLine("warning: {0} {1}", str, val);
        }

        public static void notify(string str)
        {
            Console.Error.WriteLine("notification: {0}", str);
        }

        /** Speex wrapper for putc */
        public static void _speex_putc(byte ch, Stream file)
        {
            //FILE *f = (FILE *)file;
            //fprintf(f, "%c", ch);
            file.WriteByte(ch);
        }

        //#define speex_fatal(str) _speex_fatal(str, __FILE__, __LINE__);
        //#define speex_assert(cond) {if (!(cond)) {speex_fatal("assertion failed: " #cond);}}

        public static void print_vec(float[] vec, int len, string name)
        {
            int i;
            Console.Write("{0} ", name);
            for (i = 0; i < len; i++)
                Console.Write(" {0}", vec[i]);
            Console.WriteLine();
        }
    }
}