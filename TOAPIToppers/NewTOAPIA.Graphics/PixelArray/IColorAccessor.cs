using System;

namespace NewTOAPIA.Graphics
{
    public interface IRetrieveTextureColor
    {
        ColorRGBA GetTextureColor(float u, float v);
    }

    public interface IRetrieveColor
    {
        ColorRGBA GetColor(int x, int y);
    }

    public interface ISetColor
    {
        void SetColor(int x, int y, ColorRGBA aColor);
    }


    public interface IColorAccessor : IRetrieveColor, ISetColor
    {
        /// <summary>
        /// Represents the number of pixels that are in each row.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Indicates how many rows there are in the image.
        /// </summary>
        int Height { get; }

    }
}
