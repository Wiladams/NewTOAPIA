using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;

namespace ShowIt
{
    public class DisplayMonitor : Node, IRenderable, IHaveTexture
    {
        protected GraphicsInterface GI { get; set; }
        AABB fBoundary;
        Resolution fResolution;
        DisplaySurface fDisplaySurface;
        IHaveTexture fImageSource;
        AABBSurface fCube;
        AABBFaceMesh fFaceMesh;

        public DisplayMonitor(GraphicsInterface gi, AABB boundary, Resolution res)
        {
            GI = gi;
            GLTexture fDefaultTexture = TextureHelper.CreateCheckerboardTexture(gi, 256, 256, 4);

            fBoundary = boundary;
            fResolution = res;
            
            fFaceMesh = AABBFaceMesh.CreateFace(gi, boundary.size, AABBFace.Front, new Resolution(1,1), fDefaultTexture);

            fDisplaySurface = new DisplaySurface(gi, res, null);
        }

        public IHaveTexture ImageSource
        {
            get { return fImageSource; }
            set { 
                fImageSource = value;
                fDisplaySurface.ImageSource = fImageSource;
            }
        }

        public void Rotate(float degrees, float3 axes)
        {
            float radians = NewTOAPIA.GL.MathUtils.DegreesToRadians(degrees);
            Transformation trans = new Transformation();
            trans.SetRotate(float3x3.Rotate(radians, axes));

            Transform(trans);
        }

        public void Translate(Vector3D dtrans)
        {
            Transformation trans = new Transformation();
            trans.Translate(new float3(dtrans.X, dtrans.Y, dtrans.Z));

            Transform(trans);
        }

        public void Transform(Transformation trans)
        {
            fFaceMesh.ApplyTransform(trans);
        }

        public GLTexture Texture
        {
            get
            {
                return fDisplaySurface.Texture;
            }
        }

        public virtual void Render(GraphicsInterface gi)
        {

            // Refresh the display surface.  Ideally, we only 
            // do this when we know the image has changed from
            // the last time we rendered.
            //fDisplaySurface.RefreshDisplayImage();

            //fCube.SetWallTexture(AABBFace.Front, fDisplaySurface.Texture);
            //fCube.SetWallTexture(AABBFace.Back, fDisplaySurface.Texture);
            if (null != fDisplaySurface)
                fFaceMesh.Texture = fDisplaySurface.Texture;

            gi.PushMatrix();
            {
                gi.Translate(new Vector3D(fBoundary.position.X, fBoundary.position.Y, fBoundary.position.Z));

                fFaceMesh.Render(gi);
                //fCube.Render(gi);
            }
            gi.PopMatrix();
        }
    }
}
