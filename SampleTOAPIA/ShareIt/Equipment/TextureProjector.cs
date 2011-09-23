using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;

namespace ShowIt
{
    public class TextureProjector : IRenderable, IProjectImages
    {
        AABBSurface fSurface;
        Vector3D fTranslation;

        #region Constructors
        public TextureProjector(GraphicsInterface gi, Vector3D size, Vector3D translation)
        {
            fTranslation = translation;
            fSurface = new AABBSurface(gi, size, translation, new Resolution(4,2));
        }
        #endregion


        #region IProjectImages
        public virtual void ProjectTexture(GLTexture texture)
        {
            fSurface.SetWallTexture(AABBFace.Front, texture);
            fSurface.SetWallTexture(AABBFace.Back, texture);
        }
        #endregion

        #region IRenderable
        public virtual void Render(GraphicsInterface gi)
        {
            fSurface.Render(gi);
        }
        #endregion
    }
}
