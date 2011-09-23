namespace Papyrus.Types
{
	using System;

    /// <summary>
    /// The RGBFormat object specifies how pixels are laid out in bits
    /// and bytes.  Using this object, a rendering engine can generically
    /// set and get pixel values in a pixel buffer without having to 
    /// explicitly hardcode for the format.
    /// 
    /// Drawing using this mechanism would likely be very slow, but it 
    /// makes it possible for all RGB circumstances.
    /// 
    /// The format assumes a miximum of 32 bits per pixel
    /// </summary>
	public class RGBFormat
	{
        /// <summary>
        /// Number of bits used to represent the pixel
        /// </summary>
		private byte	fBitsPerPixel;
		/// <summary>
		/// Maximum number of bytes used to represent each pixel
		/// </summary>
		private byte	fBytesPerPixel;		
	
        /// <summary>
        /// How many bits are used to represent the red component
        /// </summary>
		private byte	fRedSize;			
        /// <summary>
        /// Represents the number of bits used to represent the green component.
        /// </summary>
		private byte	fGreenSize;
        /// <summary>
        /// Represents the number of bits used to represent the blue component.
        /// </summary>
		private byte	fBlueSize;
        /// <summary>
        /// Represents the number of bits used to represent the alpha component.
        /// </summary>
		private byte	fAlphaSize;
	
		private byte	fRedLoss;			// How many pixels lost from 8 (8-size)
		private byte	fGreenLoss;
		private byte	fBlueLoss;
		private byte	fAlphaLoss;
	
		// Bits to shift for this component
		private byte	fRedShift;
		private byte	fGreenShift;
		private byte	fBlueShift;
		private byte	fAlphaShift;
	
		// Which bits give you this component
		private uint	fRedMask;
		private uint	fGreenMask;
		private uint	fBlueMask;
		private uint	fAlphaMask;



		public RGBFormat(byte bperpixel, byte byperpixel,
				byte rsize, byte gsize, byte bsize, byte asize,
				byte rloss, byte gloss, byte bloss, byte aloss,
				byte rshft, byte gshft, byte bshft, byte ashft,
				uint rmsk, uint gmsk, uint bmsk, uint amsk)
		{
			fBitsPerPixel = bperpixel;
			fBytesPerPixel = byperpixel;
	
			fRedSize = rsize;
			fGreenSize = gsize;
			fBlueSize = bsize;
			fAlphaSize = asize;
	
			fRedLoss = rloss;
			fGreenLoss = gloss;
			fBlueLoss = bloss;
			fAlphaLoss = aloss;
	
			fRedShift = rshft;
			fGreenShift = gshft;
			fBlueShift = bshft;
			fAlphaShift = ashft;
	
			fRedMask = rmsk;
			fGreenMask = gmsk;
			fBlueMask = bmsk;
			fAlphaMask = amsk;
		
			bool result = CheckConsistancy();
			if (!result)
			{
				// Throw a invalid argument exception
			}
		}

		public byte BitsPerPixel 
		{
			get 
			{
				return fBitsPerPixel;
			}
		}

		public byte BytesPerPixel
		{
			get 
			{
				return fBytesPerPixel;
			}
		}

		public byte AlphaBits 
		{
			get 
			{
				return fAlphaSize;
			}
		}
/*
	Method: CheckConsistancy()

	Checks the consistancy of the structure.  In order
	for it to be consistant, the size must and offset
	given by the instance variables must match those
	given by the mask for each component.

	Also, the total number of bits reported by the mask
	of each component must be the same as the number
	of bits reported for the whole format.
*/

		bool CheckConsistancy()
		{
			byte rshift=0, gshift=0, bshift=0, ashift=0;
			byte rsize=0, gsize=0, bsize=0, asize=0;
 
			GetMaskInfo(fRedMask, ref rshift, ref rsize);
			GetMaskInfo(fGreenMask, ref gshift, ref gsize);
			GetMaskInfo(fBlueMask, ref bshift, ref bsize);
			GetMaskInfo(fAlphaMask, ref ashift, ref asize);

			// Red Checks
			if ((rshift != fRedShift) && (fRedMask != 0))
			{
				//printf("Red Shift: %d != %d\n", rshift, fRedShift);
				return false;
			}

			if (rsize != fRedSize)
			{
				//printf("Red Size: %d != %d\n", rsize, fRedSize);
				return false;
			}

			if ((8-rsize) != fRedLoss)
			{
				//printf("Red Loss: %d != %d\n", (8-rsize), fRedSize);
				return false;
			}

			// Green Checks
			if ((gshift != fGreenShift) && (fGreenMask != 0))
			{
				//printf("Green Shift: %d != %d\n", gshift, fGreenShift);
				return false;
			}

			if (gsize != fGreenSize)
			{
				//printf("Green Size: %d != %d\n", gsize, fGreenSize);
				return false;
			}

			if ((8-gsize) != fGreenLoss)
			{
				//printf("Green Loss: %d != %d\n", (8-gsize), fGreenSize);
				return false;
			}

			// Blue Checks
			if ((bshift != fBlueShift) && (fBlueMask != 0))
			{
				//printf("Blue Shift: %d != %d\n", bshift, fBlueShift);
				return false;
			}

			if (bsize != fBlueSize)
			{
				//printf("Blue Size: %d != %d\n", bsize, fBlueSize);
				return false;
			}

			if ((8-bsize) != fBlueLoss)
			{
				//printf("Blue Loss: %d != %d\n", (8-bsize), fBlueSize);
				return false;
			}

			// Alpha Checks
			if ((ashift != fAlphaShift) && (fAlphaMask != 0))
			{
				//printf("Alpha Shift: %d != %d\n", ashift, fAlphaShift);
				return false;
			}

			if (asize != fAlphaSize)
			{
				//printf("Alpha Size: %d != %d\n", asize, fAlphaSize);
				return false;
			}

			if ((8-asize) != fAlphaLoss)
			{
				//printf("Alpha Loss: %d != %d\n", (8-asize), fAlphaSize);
				return false;
			}

			// Overall checks
			uint totalsize = (uint)(rsize + gsize + bsize + asize);
			if (totalsize != fBitsPerPixel)
			{
				//printf("Total Size: %ld != %d\n", totalsize, fBitsPerPixel);	
				return false;
			}

			return true;	
		}


/*
	Function: GetMaskInfo()
	
	The idea of this function is that you want to know from
	a given mask, where the ones start and how many of them
	there are.  For APixelFormat, this would be the precision
	and shift fields.
*/

		void GetMaskInfo(uint mask, ref byte shift, ref byte precision)
		{
			int Precision=0;
			int Shift=0;
			int remaining = 32;
			uint Bitmask = mask;
	
			shift = 0;
			precision = 0;

			// Count the zeros on the right
	
			while (remaining > 0)
			{
				if ((Bitmask & 0x01L) == 0)
				{
					Shift++;
				} else
					break;
		
				Bitmask >>= 1;
				remaining--;
			}

			//printf("shift: %d remaining: %d\n", Shift, remaining);

			// Now count how many ones there are
			// until we run into a 0
			while (remaining > 0)
			{
				if ((Bitmask & 0x01L) > 0)
					Precision++;
				else
					break;

				Bitmask >>= 1;
				remaining--;
			}
	
			shift = (byte)Shift;
			precision = (byte)Precision;
		}

		public void PrintBitmask(uint mask)
		{
			uint Bitmask = mask;

			for (int i =0; i< 32; i++)
			{
				if ((Bitmask&0x01L)>0)
					Console.Write("1");
				else
					Console.Write("0");

				Bitmask >>= 1;
			}
		}

		// RGB - R8G8B8 format B[7:0]  G[7:0]  R[7:0]  -[7:0]
		public static RGBFormat RGB32 = new RGBFormat(
					24, 4,
					8, 8, 8, 0,
					0, 0, 0, 8,
					16, 8, 0, 0,
					0x00ff0000, 0x0000ff00, 0x000000ff, 0x00);

		// RGB - R8G8B8A8 format B[7:0]  G[7:0]  R[7:0]  A[7:0]
		public static RGBFormat RGBA32 = new RGBFormat(
				32, 4,
				8, 8, 8, 8,
				0, 0, 0, 0,
				16, 8, 0, 24,
				0x00ff0000, 0x0000ff00, 0x000000ff, 0xff000000);

		// RGB - R8G8B8 format   B[7:0]  G[7:0]  R[7:0]
		public static RGBFormat RGB24 = new RGBFormat(
				24, 3,
				8, 8, 8, 0,
				0, 0, 0, 8,
				16, 8, 0, 0,
				0x00ff0000, 0x0000ff00, 0x000000ff, 0x00);

		// RGB - R5G6B5 format   G[2:0],B[4:0]	R[4:0],G[5:3] 
		public static RGBFormat RGB16 = new RGBFormat(
				16, 2,
				5, 6, 5, 0,
				3, 2, 3, 8,
				11, 5, 0, 0,
				0x0000f800, 0x000007e0, 0x0000001f, 0x00);

		// RGB - R5G5B5 format   G[2:0],B[4:0]	-[0],R[4:0],G[4:3]
		public static RGBFormat RGB15 = new RGBFormat(
				15, 2,
				5, 5, 5, 0,
				3, 3, 3, 8,
				10, 5, 0, 0,
				0x00007c00, 0x000003e0, 0x0000001f, 0x00);

		// RGB - R5G5B5 format   G[2:0],B[4:0]	A[0],R[4:0],G[4:3]
		public static RGBFormat RGBA15 = new RGBFormat(
				16, 2,
				5, 5, 5, 1,
				3, 3, 3, 7,
				10, 5, 0, 15,
				0x00007c00, 0x000003e0, 0x0000001f, 0x00008000);
	}
}

/*
		case A_CMAP8:
		case A_GRAY8:
			bitsPerPixel = 8;
			bytesPerRow = aWidth;
		break;
		
		case A_GRAY1:
			bitsPerPixel = 1;
			bytesPerRow = (int32)floor(float(aWidth+7) / 8.0);
		break;
*/
