using System;

public interface IPixelData
{
    int BitsPerPixel { get; }
    int Width { get; }
    int Height { get; }

    int Stride { get; }

    IntPtr Data { get; }
}

public interface IPixelAddressable
{
    bool SetPixel(int x, int y, uint color);
    uint GetPixel(int x, int y);
}

public interface IPixelBuffer : IPixelAddressable
{
    int Width { get; }
    int Height { get; }

    IPixelData Pixels { get; }
}
