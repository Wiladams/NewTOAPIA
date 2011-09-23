using System.Runtime.InteropServices;

namespace TOAPI.OpenGL
{

    [StructLayout(LayoutKind.Sequential)]
    public struct LAYERPLANEDESCRIPTOR
    {

        /// WORD->unsigned short
        public short nSize;

        /// WORD->unsigned short
        public ushort nVersion;

        /// DWORD->unsigned int
        public uint dwFlags;

        /// BYTE->unsigned char
        public byte iPixelType;

        /// BYTE->unsigned char
        public byte cColorBits;

        /// BYTE->unsigned char
        public byte cRedBits;

        /// BYTE->unsigned char
        public byte cRedShift;

        /// BYTE->unsigned char
        public byte cGreenBits;

        /// BYTE->unsigned char
        public byte cGreenShift;

        /// BYTE->unsigned char
        public byte cBlueBits;

        /// BYTE->unsigned char
        public byte cBlueShift;

        /// BYTE->unsigned char
        public byte cAlphaBits;

        /// BYTE->unsigned char
        public byte cAlphaShift;

        /// BYTE->unsigned char
        public byte cAccumBits;

        /// BYTE->unsigned char
        public byte cAccumRedBits;

        /// BYTE->unsigned char
        public byte cAccumGreenBits;

        /// BYTE->unsigned char
        public byte cAccumBlueBits;

        /// BYTE->unsigned char
        public byte cAccumAlphaBits;

        /// BYTE->unsigned char
        public byte cDepthBits;

        /// BYTE->unsigned char
        public byte cStencilBits;

        /// BYTE->unsigned char
        public byte cAuxBuffers;

        /// BYTE->unsigned char
        public byte iLayerPlane;

        /// BYTE->unsigned char
        public byte bReserved;

        /// COLORREF->DWORD->unsigned int
        public uint crTransparent;

        public void Init()
        {
            nSize = (short)Marshal.SizeOf(this);
            nVersion = 1;
        }
    }

}
