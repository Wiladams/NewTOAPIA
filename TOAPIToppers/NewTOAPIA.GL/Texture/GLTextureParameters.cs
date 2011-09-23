using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    public class GLTextureParameters
    {
        TextureMagFilter fMagFilter;
        TextureMinFilter fMinFilter;
        TextureWrapMode fSWrapMode;
        TextureWrapMode fTWrapMode;

        public GLTextureParameters()
        {
            fMagFilter = TextureMagFilter.Linear;
            fMinFilter = TextureMinFilter.Linear;
            fSWrapMode = TextureWrapMode.Clamp;
            fTWrapMode = TextureWrapMode.Clamp;
        }

        public GLTextureParameters(TextureMagFilter magFilter, TextureMinFilter minFilter, TextureWrapMode wrapMode)
        {
            fMagFilter = magFilter;
            fMinFilter = minFilter;
            fSWrapMode = wrapMode;
            fTWrapMode = wrapMode;
        }

        public TextureMagFilter MagFilter
        {
            get {return fMagFilter;}
            set
            {
                fMagFilter = value;
            }
        }

        public TextureMinFilter MinFilter
        {
            get { return fMinFilter; }
            set
            {
                fMinFilter = value;
            }
        }

        public TextureWrapMode WrapModeS
        {
            get { return fSWrapMode; }
            set
            {
                fSWrapMode = value;
            }
        }

        public TextureWrapMode WrapModeT
        {
            get { return fTWrapMode; }
            set
            {
                fTWrapMode = value;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
