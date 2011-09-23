using System;

namespace NewTOAPIA.Graphics
{
    public interface IPixelInformation
    {
        int BytesPerPixel { get; }
        PixelLayout Layout { get; }
        PixelComponentType ComponentType { get; }
        int Dimension { get; }
    }

}