
namespace NewTOAPIA.GL
{
    using System;

    using NewTOAPIA.Graphics;

    public class GLDraw : GIObject, IDraw
    {
        public GLDraw(GraphicsInterface gi)
            : base(gi)
        {
        }


        /// <summary>
        /// Move to an absolute position without regard to transformation matrices.
        /// </summary>
        /// <param name="x">X coordinate, 0 is left side</param>
        /// <param name="y">Y coordinate, 0 is bottom</param>
        public virtual void MoveTo(int x, int y)
        {
            GI.WindowPos(x, y);
        }

        public virtual void GetPixels(PixelAccessor accessor, int x, int y)
        {
            GI.ReadPixels(x, y, accessor.Width, accessor.Height,
                (PixelLayout)accessor.Layout, (PixelComponentType)accessor.ComponentType,
                (IntPtr)accessor.Pixels);
        }

        public void DrawPixels(PixelAccessor accessor, int x, int y)
        {
            MoveTo(x, y);
            GI.DrawPixels(accessor.Width, accessor.Height,
                (PixelLayout)accessor.Layout, (PixelComponentType)accessor.ComponentType,
                accessor.Pixels);
        }

        //public void DrawPixels(int x, int y, GLPixelRectangleInfo pixelRect)
        //{
        //    GI.DrawPixels(x, y, pixelRect);
        //}

        public void CopyPixels(RectangleI srcRect, int x, int y)
        {
            MoveTo(x, y);
            GI.CopyPixels(srcRect.Left, srcRect.Top, srcRect.Width, srcRect.Height, PixelCopyType.Color);
        }
    }
}
