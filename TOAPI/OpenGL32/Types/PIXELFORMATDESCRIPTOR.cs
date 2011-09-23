using System;
using System.Runtime.InteropServices;

namespace TOAPI.OpenGL
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PIXELFORMATDESCRIPTOR
    {
        public short nSize;
        public ushort nVersion;
        public uint dwFlags;
        public byte iPixelType;
        public byte cColorBits;
        public byte cRedBits;
        public byte cRedShift;
        public byte cGreenBits;
        public byte cGreenShift;
        public byte cBlueBits;
        public byte cBlueShift;
        public byte cAlphaBits;
        public byte cAlphaShift;
        public byte cAccumBits;
        public byte cAccumRedBits;
        public byte cAccumGreenBits;
        public byte cAccumBlueBits;
        public byte cAccumAlphaBits;
        public byte cDepthBits;
        public byte cStencilBits;
        public byte cAuxBuffers;
        public byte iLayerType;
        public byte bReserved;
        public int dwLayerMask;
        public uint dwVisibleMask;
        public uint dwDamageMask;


        public void Init()
        {
            nSize = (short)Marshal.SizeOf(this);
            nVersion = 1;
        }
    }


    /* pixel types */
    public enum PFDPixelType
    {
        RGBA = 0,
        ColorIndex = 1
    }

    /* layer types */
    public enum PFDLayerPlanes
    {
        Main = 0,
        Overlay = 1,
        Underlay = (-1)
    }

    /* PIXELFORMATDESCRIPTOR flags */
    [Flags]
    public enum PFDFlags : uint
    {
        DoubleBuffer = 0x00000001,
        Stereo = 0x00000002,
        DrawToWindow = 0x00000004,
        DrawToBitmap = 0x00000008,
        SupportGDI = 0x00000010,
        SupportOpenGL = 0x00000020,
        GenericFormat = 0x00000040,
        NeedPalette = 0x00000080,
        NeedSystemPalette = 0x00000100,
        SwapExchange = 0x00000200,
        SwapCopy = 0x00000400,
        SwapLayerBuffers = 0x00000800,
        GenericAccelerated = 0x00001000,
        SupportDirectDraw = 0x00002000,
        Direct3DAccelerated = 0x00004000,
        SupportComposition = 0x00008000,
    }

    /* PIXELFORMATDESCRIPTOR flags for use in ChoosePixelFormat only */
    public enum ChoosePixelFlags : uint
    {
        PFD_DEPTH_DONTCARE = 0x20000000,
        PFD_DOUBLEBUFFER_DONTCARE = 0x40000000,
        PFD_STEREO_DONTCARE = 0x80000000,
    }
}
