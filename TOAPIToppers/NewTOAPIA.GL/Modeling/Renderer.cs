

namespace NewTOAPIA.Modeling
{
    using System.Collections.Generic;

    using NewTOAPIA;

    abstract public class Renderer
    {
        #region Protected Fields
        // Resource limits.
        // The values are set by the Renderer-derived objects.
        int fMaxLights;
        int fMaxColors;
        int fMaxTCoords;
        int fMaxVShaderImages;
        int fMaxPShaderImages;
        int fMaxStencilIndices;
        int fMaxUserClipPlanes;

        Camera fCamera;
        #endregion

        #region Properties
        public ColorRGBA BackgroundColor
        {
            get { return new ColorRGBA(0, 0, 0, 0); }
            set { }
        }

        // Resource limits.
        public int MaxLights
        {
            get { return fMaxLights; }
            protected set { fMaxLights = value; }
        }

        public int MaxColors
        {
            get { return fMaxColors; }
            protected set { fMaxColors = value; }
        }

        public int MaxTCoords
        {
            get { return fMaxTCoords; }
            protected set { fMaxTCoords = value; }
        }

        public int MaxVShaderImages
        {
            get { return fMaxVShaderImages; }
            protected set { fMaxVShaderImages = value; }
        }

        public int MaxPShaderImages
        {
            get { return fMaxPShaderImages; }
            protected set { fMaxPShaderImages = value; }
        }

        public int MaxStencilIndices
        {
            get { return fMaxStencilIndices; }
            protected set { fMaxStencilIndices = value; }
        }

        public int MaxUserClipPlanes
        {
            get { return fMaxUserClipPlanes; }
            set { fMaxUserClipPlanes = value; }
        }
        #endregion

        #region Camera Management
        public void SetCamera(Camera aCamera)
        {
            fCamera = aCamera;
        }

        public Camera GetCamera()
        {
            return fCamera;
        }
        #endregion

        #region Window Management
        public int GetWidth()
        {
            return 0;
        }

        public int GetHeight()
        {
            return 0;
        }

        public void Resize(int width, int height)
        {
        }

        public void ToggleFullScreen()
        {
        }
        #endregion

        public void BeginScene()
        {
        }

        public void EndScene()
        {
        }

        // Drawing
        public virtual void DrawScene(IEnumerable<VisibleObject> aVisibleSet)
        {
        }

        public virtual void Draw(Geometry aGeometry)
        {
        }
        
        // text drawing
        public abstract int LoadFont (string acFace, int iSize, bool bBold, bool bItalic);
        public abstract void UnloadFont (int iFontID);
        public abstract bool SelectFont (int iFontID);
        public abstract void Draw (int iX, int iY, ColorRGBA aColor, string aText);

        // 2D drawing
        public abstract void Draw (byte[] aucBuffer);


        public abstract void ClearBackBuffer();
        public abstract void ClearDepthBuffer();
        public abstract void ClearStencilBuffer();
        public abstract void ClearBuffers();
        public abstract void DisplayBackBuffer();
    }
}
