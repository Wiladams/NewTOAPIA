using System;

using TOAPI.Types;

namespace HumLog
{
    public delegate void CreateSurfaceEventHandler(string title, RECT frame, Guid uniqueID);
    public delegate void OnCreateSurfaceEventHandler(string title, RECT frame, Guid uniqueID);
    public delegate void OnSurfaceCreatedEventHandler(Guid uniqueID);

    public delegate void InvalidateSurfaceRectEventHandler(Guid surfaceID, RECT rect);
    public delegate void OnInvalidateSurfaceRectEventHandler(Guid surfaceID, RECT rect);
    
    public delegate void ValidateSurfaceEventHandler(Guid aSurface);
    public delegate void OnValidateSurfaceEventHandler(Guid aSurface);

    public interface IManageSurfaces
    {
        void CreateSurface(string title, RECT frame, Guid uniqueID);
        void OnCreateSurface(string title, RECT frame, Guid uniqueID);
        void OnSurfaceCreated(Guid uniqueID);

        void InvalidateSurfaceRect(Guid surfaceID, RECT rect);
        void OnInvalidateSurfaceRect(Guid surfaceID, RECT rect);

        void ValidateSurface(Guid surfaceID);
        void OnValidateSurface(Guid surfaceID);
    
    }
}
