using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.Graphics.Imaging
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    /// <summary>
    /// For this one, we take the alpha channel of the source, and multiply it
    /// against the pixel values in the destination.  Return the destination.
    /// </summary>
    public class ApplySourceAlpha : IBinaryColorOperator
    {
        public ColorRGBA ApplyBinaryOperator(ColorRGBA dst, ColorRGBA src)
        {
            ColorRGBA retValue = new ColorRGBA(dst.R * src.A, dst.G * src.A, dst.B * src.A);

            return retValue;
        }
    }

    /// <summary>
    /// This is the most basic of operators.  It simply returns the source
    /// color, which amounts to a copy over the destination pixel.
    /// </summary>
    public class SourceCopy : IBinaryColorOperator
    {
        public ColorRGBA ApplyBinaryOperator(ColorRGBA dst, ColorRGBA src)
        {
            return src;
        }
    }

    public class SubtractSource : IBinaryColorOperator
    {
        public ColorRGBA ApplyBinaryOperator(ColorRGBA dst, ColorRGBA src)
        {
            ColorRGBA retValue = SubtractColor.PerformOperation(dst, src);
            
            return retValue;
        }
    }

}
