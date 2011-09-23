/*
  Copyright (C) 1999, 2000, 2002 Aladdin Enterprises.  All rights reserved.

  This software is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this software.

  Permission is granted to anyone to use this software for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this software must not be misrepresented; you must not
     claim that you wrote the original software. If you use this software
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original software.
  3. This notice may not be removed or altered from any source distribution.

  L. Peter Deutsch
  ghost@aladdin.com

 */
/* $Id: md5.cpp,v 1.3 2008/01/20 22:52:04 lilyco Exp $ */
/*
  Independent implementation of MD5 (RFC 1321).

  This code implements the MD5 Algorithm defined in RFC 1321, whose
  text is available at
	http://www.ietf.org/rfc/rfc1321.txt
  The code is derived from the text of the RFC, including the test suite
  (section A.5) but excluding the rest of Appendix A.  It does not include
  any code or documentation that is identified in the RFC as being
  copyrighted.

  The original and principal author of md5.c is L. Peter Deutsch
  <ghost@aladdin.com>.  Other authors are noted in the change history
  that follows (in reverse chronological order):

  2002-04-13 lpd Clarified derivation from RFC 1321; now handles byte order
	either statically or dynamically; added missing #include <string.h>
	in library.
  2002-03-11 lpd Corrected argument list for main(), and added int return
	type, in test program and T value program.
  2002-02-21 lpd Added missing #include <stdio.h> in test program.
  2000-07-03 lpd Patched to eliminate warnings about "constant is
	unsigned in ANSI C, signed in traditional"; made test program
	self-checking.
  1999-11-04 lpd Edited comments slightly for automatic TOC extraction.
  1999-10-18 lpd Fixed typo in header comment (ansi2knr rather than md5).
  1999-05-03 lpd Original version.
 */

using System;

using md5_byte_t = System.Byte;     /* 8-bit byte */
using md5_word_t = System.UInt32;   /* 32-bit word */

/* Define the state of the MD5 Algorithm. */
public class md5_state_t
{
    md5_word_t[] count = new md5_word_t[2];	    /* message length in bits, lsw first */
    md5_word_t[] abcd = new md5_word_t[4];		/* digest buffer */
    md5_byte_t[] buf = new md5_byte_t[64];		/* accumulate block */
}



public class MD5
{
const UInt32 T_MASK = ((md5_word_t)~0);
const UInt32 T1 = (T_MASK ^ 0x28955b87); /* 0xd76aa478 */
const UInt32 T2 = (T_MASK ^ 0x173848a9); /* 0xe8c7b756 */
const UInt32 T3 =   0x242070db;
const UInt32 T4 = (T_MASK ^ 0x3e423111); /* 0xc1bdceee */
const UInt32 T5 = (T_MASK ^ 0x0a83f050);    /* 0xf57c0faf */
const UInt32 T6 =   0x4787c62a;
const UInt32 T7 = (T_MASK ^ 0x57cfb9ec); /* 0xa8304613 */
const UInt32 T8 = (T_MASK ^ 0x02b96afe);    /* 0xfd469501 */
const UInt32 T9 =   0x698098d8;
const UInt32 T10 = (T_MASK ^ 0x74bb0850);    /* 0x8b44f7af */
const UInt32 T11 = (T_MASK ^ 0x0000a44e);   /* 0xffff5bb1 */
const UInt32 T12 = (T_MASK ^ 0x76a32841);   /* 0x895cd7be */
const UInt32 T13 =   0x6b901122;
const UInt32 T14 = (T_MASK ^ 0x02678e6c);   /* 0xfd987193 */
const UInt32 T15 = (T_MASK ^ 0x5986bc71);   /* 0xa679438e */
const UInt32 T16 =   0x49b40821;
const UInt32 T17 = (T_MASK ^ 0x09e1da9d);   /* 0xf61e2562 */
const UInt32 T18 = (T_MASK ^ 0x3fbf4cbf);   /* 0xc040b340 */
const UInt32 T19 =   0x265e5a51;
const UInt32 T20 = (T_MASK ^ 0x16493855);   /* 0xe9b6c7aa */
const UInt32 T21 = (T_MASK ^ 0x29d0efa2);   /* 0xd62f105d */
const UInt32 T22 =   0x02441453;
const UInt32 T23 = (T_MASK ^ 0x275e197e);   /* 0xd8a1e681 */
const UInt32 T24 = (T_MASK ^ 0x182c0437);   /* 0xe7d3fbc8 */
const UInt32 T25 =   0x21e1cde6;
const UInt32 T26 = (T_MASK ^ 0x3cc8f829);   /* 0xc33707d6 */
const UInt32 T27 = (T_MASK ^ 0x0b2af278);   /* 0xf4d50d87 */
const UInt32 T28 =   0x455a14ed;
const UInt32 T29 = (T_MASK ^ 0x561c16fa);   /* 0xa9e3e905 */
const UInt32 T30 = (T_MASK ^ 0x03105c07);     /* 0xfcefa3f8 */
const UInt32 T31 =   0x676f02d9;
const UInt32 T32 = (T_MASK ^ 0x72d5b375);     /* 0x8d2a4c8a */
const UInt32 T33 = (T_MASK ^ 0x0005c6bd);     /* 0xfffa3942 */
const UInt32 T34 = (T_MASK ^ 0x788e097e);     /* 0x8771f681 */
const UInt32 T35 =   0x6d9d6122;
const UInt32 T36 = (T_MASK ^ 0x021ac7f3);     /* 0xfde5380c */
const UInt32 T37 = (T_MASK ^ 0x5b4115bb);     /* 0xa4beea44 */
const UInt32 T38 =   0x4bdecfa9;
const UInt32 T39 = (T_MASK ^ 0x0944b49f);     /* 0xf6bb4b60 */
const UInt32 T40 = (T_MASK ^ 0x4140438f);     /* 0xbebfbc70 */
const UInt32 T41 =   0x289b7ec6;
const UInt32 T42 = (T_MASK ^ 0x155ed805);     /* 0xeaa127fa */
const UInt32 T43 = (T_MASK ^ 0x2b10cf7a);     /* 0xd4ef3085 */
const UInt32 T44 =   0x04881d05;
const UInt32 T45 = (T_MASK ^ 0x262b2fc6);     /* 0xd9d4d039 */
const UInt32 T46 = (T_MASK ^ 0x1924661a);     /* 0xe6db99e5 */
const UInt32 T47 =   0x1fa27cf8;
const UInt32 T48 = (T_MASK ^ 0x3b53a99a);     /* 0xc4ac5665 */
const UInt32 T49 = (T_MASK ^ 0x0bd6ddbb);     /* 0xf4292244 */
const UInt32 T50 =   0x432aff97;
const UInt32 T51 = (T_MASK ^ 0x546bdc58);     /* 0xab9423a7 */
const UInt32 T52 = (T_MASK ^ 0x036c5fc6);     /* 0xfc93a039 */
const UInt32 T53 =   0x655b59c3;
const UInt32 T54 = (T_MASK ^ 0x70f3336d);     /* 0x8f0ccc92 */
const UInt32 T55 = (T_MASK ^ 0x00100b82);     /* 0xffeff47d */
const UInt32 T56 = (T_MASK ^ 0x7a7ba22e);     /* 0x85845dd1 */
const UInt32 T57 =   0x6fa87e4f;
const UInt32 T58 = (T_MASK ^ 0x01d3191f);     /* 0xfe2ce6e0 */
const UInt32 T59 = (T_MASK ^ 0x5cfebceb);     /* 0xa3014314 */
const UInt32 T60 =   0x4e0811a1;
const UInt32 T61 = (T_MASK ^ 0x08ac817d);     /* 0xf7537e82 */
const UInt32 T62 = (T_MASK ^ 0x42c50dca);     /* 0xbd3af235 */
const UInt32 T63 =   0x2ad7d2bb;
const UInt32 T64 = (T_MASK ^ 0x14792c6e);     /* 0xeb86d391 */
    
    static void md5_process(md5_state_t pms, md5_byte_t data /*[64]*/)
{
    md5_word_t a = pms.abcd[0];
    md5_word_t b = pms.abcd[1];
	md5_word_t c = pms.abcd[2]; 
    md5_word_t d = pms.abcd[3];
    md5_word_t t;
#if BYTE_ORDER > 0
    /* Define storage only for big-endian CPUs. */
    md5_word_t X[16];
#else
    /* Define storage for little-endian or both types of CPUs. */
    md5_word_t xbuf[16];
    const md5_word_t *X;
#endif

    {
#if BYTE_ORDER == 0
	/*
	 * Determine dynamically whether this is a big-endian or
	 * little-endian machine, since we can use a more efficient
	 * algorithm on the latter.
	 */
	static const int w = 1;

	if (*((const md5_byte_t *)&w)) /* dynamic little-endian */
#endif
#if BYTE_ORDER <= 0		/* little-endian */
	{
	    /*
	     * On little-endian machines, we can process properly aligned
	     * data without copying it.
	     */
	    if (!((data - (const md5_byte_t *)0) & 3)) {
		/* data are properly aligned */
		X = (const md5_word_t *)data;
	    } else {
		/* not aligned */
		memcpy(xbuf, data, 64);
		X = xbuf;
	    }
	}
#endif
#if BYTE_ORDER == 0
	else			/* dynamic big-endian */
#endif
#if BYTE_ORDER >= 0		/* big-endian */
	{
	    /*
	     * On big-endian machines, we must arrange the bytes in the
	     * right order.
	     */
	    const md5_byte_t *xp = data;
	    int i;

#  if BYTE_ORDER == 0
	    X = xbuf;		/* (dynamic only) */
#  else
#    define xbuf X		/* (static only) */
#  endif
	    for (i = 0; i < 16; ++i, xp += 4)
		xbuf[i] = xp[0] + (xp[1] << 8) + (xp[2] << 16) + (xp[3] << 24);
	}
#endif
    }

UInt32 ROTATE_LEFT(UInt32 x, int n) 
        {
            return (((x) << (n)) | ((x) >> (32 - (n))));
        }

    /* Round 1. */
    /* Let [abcd k s i] denote the operation
       a = b + ((a + F(b,c,d) + X[k] + T[i]) <<< s). */
#define F(x, y, z) (((x) & (y)) | (~(x) & (z)))
#define SET(a, b, c, d, k, s, Ti)\
  t = a + F(b,c,d) + X[k] + Ti;\
  a = ROTATE_LEFT(t, s) + b
    /* Do the following 16 operations. */
    SET(a, b, c, d,  0,  7,  T1);
    SET(d, a, b, c,  1, 12,  T2);
    SET(c, d, a, b,  2, 17,  T3);
    SET(b, c, d, a,  3, 22,  T4);
    SET(a, b, c, d,  4,  7,  T5);
    SET(d, a, b, c,  5, 12,  T6);
    SET(c, d, a, b,  6, 17,  T7);
    SET(b, c, d, a,  7, 22,  T8);
    SET(a, b, c, d,  8,  7,  T9);
    SET(d, a, b, c,  9, 12, T10);
    SET(c, d, a, b, 10, 17, T11);
    SET(b, c, d, a, 11, 22, T12);
    SET(a, b, c, d, 12,  7, T13);
    SET(d, a, b, c, 13, 12, T14);
    SET(c, d, a, b, 14, 17, T15);
    SET(b, c, d, a, 15, 22, T16);
#undef SET

     /* Round 2. */
     /* Let [abcd k s i] denote the operation
          a = b + ((a + G(b,c,d) + X[k] + T[i]) <<< s). */
#define G(x, y, z) (((x) & (z)) | ((y) & ~(z)))
#define SET(a, b, c, d, k, s, Ti)\
  t = a + G(b,c,d) + X[k] + Ti;\
  a = ROTATE_LEFT(t, s) + b
     /* Do the following 16 operations. */
    SET(a, b, c, d,  1,  5, T17);
    SET(d, a, b, c,  6,  9, T18);
    SET(c, d, a, b, 11, 14, T19);
    SET(b, c, d, a,  0, 20, T20);
    SET(a, b, c, d,  5,  5, T21);
    SET(d, a, b, c, 10,  9, T22);
    SET(c, d, a, b, 15, 14, T23);
    SET(b, c, d, a,  4, 20, T24);
    SET(a, b, c, d,  9,  5, T25);
    SET(d, a, b, c, 14,  9, T26);
    SET(c, d, a, b,  3, 14, T27);
    SET(b, c, d, a,  8, 20, T28);
    SET(a, b, c, d, 13,  5, T29);
    SET(d, a, b, c,  2,  9, T30);
    SET(c, d, a, b,  7, 14, T31);
    SET(b, c, d, a, 12, 20, T32);
#undef SET

     /* Round 3. */
     /* Let [abcd k s t] denote the operation
          a = b + ((a + H(b,c,d) + X[k] + T[i]) <<< s). */
#define H(x, y, z) ((x) ^ (y) ^ (z))
#define SET(a, b, c, d, k, s, Ti)\
  t = a + H(b,c,d) + X[k] + Ti;\
  a = ROTATE_LEFT(t, s) + b
     /* Do the following 16 operations. */
    SET(a, b, c, d,  5,  4, T33);
    SET(d, a, b, c,  8, 11, T34);
    SET(c, d, a, b, 11, 16, T35);
    SET(b, c, d, a, 14, 23, T36);
    SET(a, b, c, d,  1,  4, T37);
    SET(d, a, b, c,  4, 11, T38);
    SET(c, d, a, b,  7, 16, T39);
    SET(b, c, d, a, 10, 23, T40);
    SET(a, b, c, d, 13,  4, T41);
    SET(d, a, b, c,  0, 11, T42);
    SET(c, d, a, b,  3, 16, T43);
    SET(b, c, d, a,  6, 23, T44);
    SET(a, b, c, d,  9,  4, T45);
    SET(d, a, b, c, 12, 11, T46);
    SET(c, d, a, b, 15, 16, T47);
    SET(b, c, d, a,  2, 23, T48);
#undef SET

     /* Round 4. */
     /* Let [abcd k s t] denote the operation
          a = b + ((a + I(b,c,d) + X[k] + T[i]) <<< s). */
#define I(x, y, z) ((y) ^ ((x) | ~(z)))
#define SET(a, b, c, d, k, s, Ti)\
  t = a + I(b,c,d) + X[k] + Ti;\
  a = ROTATE_LEFT(t, s) + b
     /* Do the following 16 operations. */
    SET(a, b, c, d,  0,  6, T49);
    SET(d, a, b, c,  7, 10, T50);
    SET(c, d, a, b, 14, 15, T51);
    SET(b, c, d, a,  5, 21, T52);
    SET(a, b, c, d, 12,  6, T53);
    SET(d, a, b, c,  3, 10, T54);
    SET(c, d, a, b, 10, 15, T55);
    SET(b, c, d, a,  1, 21, T56);
    SET(a, b, c, d,  8,  6, T57);
    SET(d, a, b, c, 15, 10, T58);
    SET(c, d, a, b,  6, 15, T59);
    SET(b, c, d, a, 13, 21, T60);
    SET(a, b, c, d,  4,  6, T61);
    SET(d, a, b, c, 11, 10, T62);
    SET(c, d, a, b,  2, 15, T63);
    SET(b, c, d, a,  9, 21, T64);
#undef SET

     /* Then perform the following additions. (That is increment each
        of the four registers by the value it had before this block
        was started.) */
    pms->abcd[0] += a;
    pms->abcd[1] += b;
    pms->abcd[2] += c;
    pms->abcd[3] += d;
}

public void
md5_init(md5_state_t pms)
{
    pms.count[0] = pms->count[1] = 0;
    pms.abcd[0] = 0x67452301;
    pms.abcd[1] = /*0xefcdab89*/ T_MASK ^ 0x10325476;
    pms.abcd[2] = /*0x98badcfe*/ T_MASK ^ 0x67452301;
    pms.abcd[3] = 0x10325476;
}

void
md5_append(md5_state_t *pms, const md5_byte_t *data, int nbytes)
{
    const md5_byte_t *p = data;
    int left = nbytes;
    int offset = (pms->count[0] >> 3) & 63;
    md5_word_t nbits = (md5_word_t)(nbytes << 3);

    if (nbytes <= 0)
	return;

    /* Update the message length. */
    pms->count[1] += nbytes >> 29;
    pms->count[0] += nbits;
    if (pms->count[0] < nbits)
	pms->count[1]++;

    /* Process an initial partial block. */
    if (offset) {
	int copy = (offset + nbytes > 64 ? 64 - offset : nbytes);

	memcpy(pms->buf + offset, p, copy);
	if (offset + copy < 64)
	    return;
	p += copy;
	left -= copy;
	md5_process(pms, pms->buf);
    }

    /* Process full blocks. */
    for (; left >= 64; p += 64, left -= 64)
	md5_process(pms, p);

    /* Process a final partial block. */
    if (left)
	memcpy(pms->buf, p, left);
}

void
md5_finish(md5_state_t *pms, md5_byte_t digest[16])
{
    static const md5_byte_t pad[64] = {
	0x80, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
    };
    md5_byte_t data[8];
    int i;

    /* Save the length before padding. */
    for (i = 0; i < 8; ++i)
	data[i] = (md5_byte_t)(pms->count[i >> 2] >> ((i & 3) << 3));
    /* Pad to 56 bytes mod 64. */
    md5_append(pms, pad, ((55 - (pms->count[0] >> 3)) & 63) + 1);
    /* Append the length. */
    md5_append(pms, data, 8);
    for (i = 0; i < 16; ++i)
	digest[i] = (md5_byte_t)(pms->abcd[i >> 2] >> ((i & 3) << 3));
}
}