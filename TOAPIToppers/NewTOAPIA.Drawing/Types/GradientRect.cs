using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.Types;

namespace NewTOAPIA.Drawing
{

    public enum GradientRectDirection
    {
        Horizontal = 0x00000000,
        Vertical = 0x00000001,
    }

    [Serializable]
    public class GradientRect
    {
        GRADIENT_RECT[] fGradientRect;
        TRIVERTEX[] fVertices;
        RECT fRect;
        GradientRectDirection fGradientDirection;

        public GradientRect(TRIVERTEX[] vertices, GRADIENT_RECT[] mesh, GradientRectDirection style)
        {
            fVertices = vertices;
            fGradientRect = mesh;
            fGradientDirection = style;
        }

        public GradientRect(int left, int top, int width, int height, uint startColor, uint endColor, GradientRectDirection style)
        {
            fRect = new RECT(left, top, width, height);

            fVertices = new TRIVERTEX[2];
            fGradientRect = new GRADIENT_RECT[1];

            fGradientRect[0] = new GRADIENT_RECT();

            SetVertices(left, top, width, height);

            // Set Colors for left/top
            fVertices[0].Red = (ushort)(RGBColor.R(startColor) << 8);
            fVertices[0].Green = (ushort)(RGBColor.G(startColor) << 8);
            fVertices[0].Blue = (ushort)(RGBColor.B(startColor) << 8);
            fVertices[0].Alpha = 0x0000;

            // Set Colors for right/bottom
            fVertices[1].Red = (ushort)(RGBColor.R(endColor) << 8);
            fVertices[1].Green = (ushort)(RGBColor.G(endColor) << 8);
            fVertices[1].Blue = (ushort)(RGBColor.B(endColor) << 8);
            fVertices[1].Alpha = 0x0000;

            fGradientRect[0].UpperLeft = 0;
            fGradientRect[0].LowerRight = 1;

            fGradientDirection = style;
        }

        public virtual void SetVertices(int left, int top, int width, int height)
        {
            fVertices[0].x = left;
            fVertices[0].y = top;

            fVertices[1].x = left+width;
            fVertices[1].y = top + height;
        }

        #region Properties
        public GRADIENT_RECT[] Boundary
        {
            get { return fGradientRect; }
        }

        public TRIVERTEX[] Vertices
        {
            get { return fVertices; }
        }

        public GradientRectDirection Direction
        {
            get { return fGradientDirection; }
        }
        #endregion
    }
}
