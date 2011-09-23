using System;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HamSketch
{
    public class SpaceControlDelegate : ISpaceControl
    {
        /// Draw bitmaps
        public event NewTOAPIA.Drawing.BitBlt BitBltHandler;
        public event NewTOAPIA.Drawing.AlphaBlend AlphaBlendHandler;

        // Mouse events
        public event NewTOAPIA.UI.MouseActivityEventHandler MouseActivityEvent;
        public event NewTOAPIA.UI.MouseActivityEventHandler OnMouseActivityEvent;

        public SpaceControlDelegate()
        {
        }

        public SpaceControlDelegate(MetaSpace aSpace)
        {
        }

        public void AddSpaceController(ISpaceControl aController)
        {
            ///// Draw bitmaps
            BitBltHandler += new NewTOAPIA.Drawing.BitBlt(aController.BitBlt);
            AlphaBlendHandler += new NewTOAPIA.Drawing.AlphaBlend(aController.AlphaBlend);

            MouseActivityEvent += new MouseActivityEventHandler(aController.MouseActivity);
            OnMouseActivityEvent += new MouseActivityEventHandler(aController.OnMouseActivity);
        }

        #region Handling Mouse
        public virtual void MouseActivity(Object sender, MouseEventArgs mevent)
        {
            if (null != MouseActivityEvent)
                MouseActivityEvent(sender, mevent);
        }

        public virtual void OnMouseActivity(Object sender, MouseEventArgs mevent)
        {
            if (null != OnMouseActivityEvent)
                OnMouseActivityEvent(sender, mevent);
        }
        #endregion

        #region Drawing Bitmaps
        // Generalized bit block transfer
        // Can transfer from any device context to this one.
        public virtual void BitBlt(int x, int y, PixelBuffer pixBuff)
        {
            if (null != BitBltHandler)
                BitBltHandler(x, y, pixBuff);
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            if (null != AlphaBlendHandler)
                AlphaBlendHandler(x, y, width, height,
                    bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void ScaleBitmap(PixelBuffer aBitmap, RECT aFrame)
        {
            AlphaBlend(aFrame.Left, aFrame.Top, aFrame.Width, aFrame.Height,
                aBitmap, 0, 0, aBitmap.Width, aBitmap.Height, aBitmap.Alpha);
        }
        #endregion
    }
}
