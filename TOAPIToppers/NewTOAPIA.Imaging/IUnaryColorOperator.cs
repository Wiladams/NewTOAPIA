using System;

namespace NewTOAPIA.Graphics.Imaging
{
    using NewTOAPIA.Graphics;

    public interface ITransformColor
    {
        ColorRGBA Transform(ColorRGBA origin);
    }
}
