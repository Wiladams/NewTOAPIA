using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLVertex
    {
        public float3 fPosition;
        public float3 fNormal;
        public ColorRGBA fColor;

        public GLVertex(float3 position, ColorRGBA color)
        {
            fPosition = position;
            fColor = color;
        }
    }
}
