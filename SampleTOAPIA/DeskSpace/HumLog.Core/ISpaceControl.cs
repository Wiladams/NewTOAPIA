using System;

using TOAPI.Types;
using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace HumLog
{
    public delegate void CopyPixelsEventHandler(int x, int y, int width, int height, PixelBuffer pixBuff);

    public interface ISendSpaceControl
    {
        // Mouse interaction
        void MouseActivity(object sender, MouseActivityArgs e);
    
        // Bitmap manipulation
        void AlphaBlend(int x, int y, int width, int height, PixelBuffer srchDC, int srcX, int srcY, int srcWidth, int srcHeight, byte alpha);
        void CopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff);

        // Surface management
        void CreateSurface(string title, RECT frame, Guid uniqueID);
        void InvalidateSurfaceRect(Guid surfaceID, RECT rect);
        void ValidateSurface(Guid surfaceID);
    }

    public interface IReceiveSpaceControl
    {
        // Mouse interaction
        void OnMouseActivity(object sender, MouseActivityArgs e);

        // Bitmap manipulation
        void OnCopyPixels(int x, int y, int width, int height, PixelBuffer pixBuff);
        void OnAlphaBlend(int x, int y, int width, int height,
            PixelBuffer srchDC, int srcX, int srcY, int srcWidth, int srcHeight,
            byte alpha);

        // Surface management
        void OnCreateSurface(string title, RECT frame, Guid uniqueID);
        void OnSurfaceCreated(Guid uniqueID);
        void OnInvalidateSurfaceRect(Guid surfaceID, RECT rect);
        void OnValidateSurface(Guid surfaceID);

    }

    public interface IControlSpace : ISendSpaceControl, IReceiveSpaceControl
    {

    }
}
