using System;
using System.Collections.Generic;
using System.Text;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Modeling;
using NewTOAPIA.Shapes;

namespace ShowIt
{
    public class PedestalTable : IRenderable
    {
        Vector3D fPedestalSize;
        Vector3D fPedestalOffset;
        AABBSurface fPedestal;
        GLTexture fPedestalTexture;

        Vector3D fTableSize;
        Vector3D fTableOffset;
        AABBSurface fTableTop;
        GLTexture fTableTopTexture;


        GLAxes fAxes;

        public PedestalTable(GraphicsInterface gi)
            : this(gi,new Vector3D(0.610f,0.914f-0.05f, 1.219f),new Vector3D(1.219f, 0.05f, 1.829f))
        {
        }

        public PedestalTable(GraphicsInterface gi, Vector3D pedestalSize, Vector3D tableTopSize)
        {
            fTableTopTexture = TextureHelper.CreateTextureFromFile(gi, "Textures\\TableTop.jpg", false);
            fPedestalTexture = TextureHelper.CreateTextureFromFile(gi, "Textures\\Wood25l.jpg", false);
            

            fPedestalSize = new Vector3D(pedestalSize.X, pedestalSize.Y, pedestalSize.Z);
            fPedestalOffset = new Vector3D(0, 0, 0);
            fPedestal = new AABBSurface(gi, pedestalSize);
            fPedestal.SetWallTexture(AABBFace.Left, fPedestalTexture);
            fPedestal.SetWallTexture(AABBFace.Right, fPedestalTexture);
            fPedestal.SetWallTexture(AABBFace.Front, fPedestalTexture);
            fPedestal.SetWallTexture(AABBFace.Back, fPedestalTexture);

            fTableSize = new Vector3D(tableTopSize.X, tableTopSize.Y, tableTopSize.Z);
            fTableOffset = new Vector3D(0, pedestalSize.Y, 0);
            fTableTop = new AABBSurface(gi, tableTopSize);
            fTableTop.SetWallTexture(AABBFace.Ceiling, fTableTopTexture);

            fAxes = new GLAxes(fPedestalSize.Y);
        }

        public void Render(GraphicsInterface gi)
        {
            
            gi.ShadeModel(ShadingModel.Flat);
            gi.PolygonMode(GLFace.FrontAndBack, PolygonMode.Fill);
            
            // Draw Pedestal
            fPedestal.Render(gi);

            // Draw table top
            //if (null != fTableTopTexture)
            //    fTableTopTexture.Bind();
            
            //gi.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            //gi.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            //gi.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Clamp);
            //gi.TexParameter(TextureParameterTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Clamp);
            //gi.TexEnv(TextureEnvModeParam.Modulate);
            //gi.Features.Texturing2D.Enable();

            gi.PushMatrix();
            gi.Translate(fTableOffset.X, fTableOffset.Y, fTableOffset.Z);
            fTableTop.Render(gi);
            gi.PopMatrix();
        
            //if (null != fTableTopTexture)
            //    fTableTopTexture.Unbind();

        }
    }
}
