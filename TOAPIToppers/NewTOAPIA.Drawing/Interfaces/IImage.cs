

namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    public interface IImage
    {
        Vector2D OriginOffset
        {
            get;
            set;
        }

        int Width();
        int Height();
        int StrideInBytes();
        int StrideInBytesAbs();

        RendererBase NewRenderer();

        IBlender GetBlender();
        void SetBlender(IBlender value);
        int GetDistanceBetweenPixelsInclusive();

        int BitDepth { get; }

        byte[] GetBuffer(out int bufferOffset);
        byte[] GetPixelPointerY(int y, out int bufferOffset);
        byte[] GetPixelPointerXY(int x, int y, out int bufferOffset);

        RGBA_Bytes pixel(int x, int y);
        void copy_pixel(int x, int y, byte[] c, int ByteOffset);

        void CopyFrom(IImage sourceRaster);
        void CopyFrom(IImage sourceRaster, int xdst, int ydst, int xsrc, int ysrc, int len);

        void SetPixelFromColor(byte[] pixelToSet, IColorType colorToGet);
        void BlendPixel(int x, int y, RGBA_Bytes sourceColor, byte cover);

        // line stuff
        void copy_hline(int x, int y, int len, RGBA_Bytes sourceColor);
        void copy_vline(int x, int y, int len, RGBA_Bytes sourceColor);

        void blend_hline(int x, int y, int x2, RGBA_Bytes sourceColor, byte cover);
        void blend_vline(int x, int y1, int y2, RGBA_Bytes sourceColor, byte cover);

        // color stuff
        void copy_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorIndex);
        void copy_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorIndex);

        void blend_solid_hspan(int x, int y, int len, RGBA_Bytes sourceColor, byte[] covers, int coversIndex);
        void blend_solid_vspan(int x, int y, int len, RGBA_Bytes sourceColor, byte[] covers, int coversIndex);

        void blend_color_hspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll);
        void blend_color_vspan(int x, int y, int len, RGBA_Bytes[] colors, int colorsIndex, byte[] covers, int coversIndex, bool firstCoverForAll);
    };
}
