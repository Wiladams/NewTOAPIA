

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public interface IDraw
    {
        void MoveTo(int x, int y);

        void GetPixels(PixelAccessor accessor, int x, int y);
        void DrawPixels(PixelAccessor accessor, int x, int y);
        void CopyPixels(RectangleI srcRect, int x, int y);

    }
}
