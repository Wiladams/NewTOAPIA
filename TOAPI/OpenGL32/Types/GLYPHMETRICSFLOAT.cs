using System;
using System.Runtime.InteropServices;

namespace TOAPI.OpenGL
{

    [StructLayout(LayoutKind.Sequential)]
    public struct GLYPHMETRICSFLOAT
    {
        public float gmfBlackBoxX;
        public float gmfBlackBoxY;
        public POINTFLOAT gmfptGlyphOrigin;
        public float gmfCellIncX;
        public float gmfCellIncY;
    }
}