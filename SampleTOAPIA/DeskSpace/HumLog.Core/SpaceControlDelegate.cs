using System;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public class SpaceControlDelegate : IControlSpace
    {
        // Surface creation
        public event CreateSurfaceEventHandler CreateSurfaceEvent;
        public event OnCreateSurfaceEventHandler OnCreateSurfaceEvent;
        public event OnSurfaceCreatedEventHandler OnSurfaceCreatedEvent;

        // Surface Management
        // Surface Drawing Management
        public event InvalidateSurfaceRectEventHandler InvalidateSurfaceRectEvent;
        public event OnInvalidateSurfaceRectEventHandler OnInvalidateSurfaceRectEvent;
        public event ValidateSurfaceEventHandler ValidateSurfaceEvent;
        public event OnValidateSurfaceEventHandler OnValidateSurfaceEvent;

        /// Draw bitmaps
        /// 
        public event CopyPixelsEventHandler CopyPixelsEvent;
        public event CopyPixelsEventHandler OnCopyPixelsEvent;
        public event NewTOAPIA.Drawing.BitBlt BitBltHandler;
        public event NewTOAPIA.Drawing.BitBlt OnBitBltEvent;
        public event NewTOAPIA.Drawing.AlphaBlend AlphaBlendHandler;
        public event NewTOAPIA.Drawing.OnAlphaBlendEventHandler OnAlphaBlendEvent;

        // Mouse events
        public event NewTOAPIA.UI.MouseEventHandler MouseActivityEvent;
        public event NewTOAPIA.UI.MouseEventHandler OnMouseActivityEvent;


        public SpaceControlDelegate()
        {
        }

        public SpaceControlDelegate(MetaSpace aSpace)
        {
        }

        public void AddSpaceController(IControlSpace aController)
        {
            // Surface management
            CreateSurfaceEvent += new CreateSurfaceEventHandler(aController.CreateSurface);
            OnCreateSurfaceEvent += new OnCreateSurfaceEventHandler(aController.OnCreateSurface);
            OnSurfaceCreatedEvent += new OnSurfaceCreatedEventHandler(aController.OnSurfaceCreated);

            // Surface Drawing Management
            InvalidateSurfaceRectEvent += new InvalidateSurfaceRectEventHandler(aController.InvalidateSurfaceRect);
            OnInvalidateSurfaceRectEvent += new OnInvalidateSurfaceRectEventHandler(aController.OnInvalidateSurfaceRect);
            ValidateSurfaceEvent += new ValidateSurfaceEventHandler(aController.ValidateSurface);
            OnValidateSurfaceEvent += new OnValidateSurfaceEventHandler(aController.OnValidateSurface);

            ///// Draw bitmaps
            //BitBltHandler += new NewTOAPIA.Drawing.BitBlt(aController.BitBlt);
            //OnBitBltEvent += new BitBlt(aController.OnBitBlt);
            CopyPixelsEvent += new CopyPixelsEventHandler(aController.CopyPixels);
            OnCopyPixelsEvent += new CopyPixelsEventHandler(aController.OnCopyPixels);
            AlphaBlendHandler += new NewTOAPIA.Drawing.AlphaBlend(aController.AlphaBlend);
            OnAlphaBlendEvent += new OnAlphaBlendEventHandler(aController.OnAlphaBlend);

            MouseActivityEvent += new MouseEventHandler(aController.MouseActivity);
            OnMouseActivityEvent += new MouseEventHandler(aController.OnMouseActivity);
        }

        #region Surface Management
        public virtual void CreateSurface(string title, RECT frame, Guid uniqueID)
        {
            if (null != CreateSurfaceEvent)
                CreateSurfaceEvent(title, frame, uniqueID);
        }

        public virtual void OnCreateSurface(string title, RECT frame, Guid uniqueID)
        {
            if (null != OnCreateSurfaceEvent)
                OnCreateSurfaceEvent(title, frame, uniqueID);

            OnSurfaceCreated(uniqueID);
        }

        public virtual void OnSurfaceCreated(Guid uniqueID)
        {
            if (null != OnSurfaceCreatedEvent)
                OnSurfaceCreatedEvent(uniqueID);
        }
        #endregion

        #region Handling Mouse
        public virtual void MouseActivity(Object sender, MouseActivityArgs mevent)
        {
            if (null != MouseActivityEvent)
                MouseActivityEvent(sender, mevent);
        }

        public virtual void OnMouseActivity(Object sender, MouseActivityArgs mevent)
        {
            if (null != OnMouseActivityEvent)
                OnMouseActivityEvent(sender, mevent);
        }
        #endregion

        #region Surface Drawing 
        #region Surface Validation
        public virtual void InvalidateSurfaceRect(Guid surfaceID, RECT rect)
        {
            if (null != InvalidateSurfaceRectEvent)
                InvalidateSurfaceRectEvent(surfaceID, rect);
        }

        public virtual void OnInvalidateSurfaceRect(Guid surfaceID, RECT rect)
        {
            if (null != OnInvalidateSurfaceRectEvent)
                OnInvalidateSurfaceRectEvent(surfaceID, rect);
        }

        public virtual void ValidateSurface(Guid surfaceID)
        {
            if (null != ValidateSurfaceEvent)
                ValidateSurfaceEvent(surfaceID);
        }

        public virtual void OnValidateSurface(Guid surfaceID)
        {
            if (null != OnValidateSurfaceEvent)
                OnValidateSurfaceEvent(surfaceID);
        }
        #endregion
        #region Drawing Bitmaps
        // Generalized bit block transfer
        // Can transfer from any device context to this one.
        public virtual void BitBlt(int x, int y, PixelBuffer24 pixBuff)
        {
            if (null != BitBltHandler)
                BitBltHandler(x, y, pixBuff);
        }

        public virtual void OnBitBlt(int x, int y, PixelBuffer24 pixBuff)
        {
            if (null != OnBitBltEvent)
                OnBitBltEvent(x, y, pixBuff);
        }

        public virtual void CopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            if (null != CopyPixelsEvent)
                CopyPixelsEvent(x, y, width, height, pixBuff);
        }

        public virtual void OnCopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff)
        {
            if (null != OnCopyPixelsEvent)
                OnCopyPixelsEvent(x, y, width, height, pixBuff);
        }

        public virtual void AlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            if (null != AlphaBlendHandler)
                AlphaBlendHandler(x, y, width, height,
                    bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void OnAlphaBlend(int x, int y, int width, int height,
                PixelBuffer bitmap, int srcX, int srcY, int srcWidth, int srcHeight,
                byte alpha)
        {
            if (null != OnAlphaBlendEvent)
                OnAlphaBlendEvent(x, y, width, height,
                    bitmap, srcX, srcY, srcWidth, srcHeight, alpha);
        }

        public virtual void ScaleBitmap(PixelBuffer aBitmap, RECT aFrame)
        {
            AlphaBlend(aFrame.Left, aFrame.Top, aFrame.Width, aFrame.Height,
                aBitmap, 0, 0, aBitmap.Width, aBitmap.Height, aBitmap.Alpha);
        }
        #endregion
        #endregion
    }
}
