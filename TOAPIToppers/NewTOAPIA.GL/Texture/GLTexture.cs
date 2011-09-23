using System;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Graphics;

    abstract public class GLTexture : IBindable, IDisposable
    {
        #region protected Fields
        protected GraphicsInterface fGI;
        protected uint fTextureID;
        protected TextureBindTarget fBindTarget;


        // Texture format
        protected TextureInternalFormat fInternalFormat;
        protected TexturePixelFormat fPixelFormat;
        protected PixelComponentType fPixelType;
        #endregion

        #region Constructors
        protected GLTexture(GraphicsInterface gi, TextureBindTarget target)
        {
            fGI = gi;
            fBindTarget = target;

            // Get a texture ID
            fTextureID = GetNewTextureID();

            // Bind the texture to create the default texture
            GI.BindTexture(BindTarget, TextureID);
        }
        #endregion

        #region Public Methods
        public virtual void Bind()
        {
            GI.BindTexture(BindTarget, TextureID);
        }

        public virtual void Unbind()
        {
            GI.BindTexture(BindTarget, 0);
        }
        #endregion

        #region Properties
        public TextureBindTarget BindTarget
        {
            get { return fBindTarget; }
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public TextureMagFilter MagFilter
        {
            get
            {
                int [] values = new int[1];
                GI.GetTexParameter((GetTextureParameterTarget)BindTarget, GetTextureParameter.TextureMagFilter, values);
                return (TextureMagFilter)values[0];
            }
        }

        public TextureMinFilter MinFilter
        {
            get
            {
                int[] values = new int[1];
                GI.GetTexParameter((GetTextureParameterTarget)BindTarget, GetTextureParameter.TextureMinFilter, values);
                return (TextureMinFilter)values[0];
            }
        }

        public TextureWrapMode WrapS
        {
            get
            {
                int[] values = new int[1];
                GI.GetTexParameter((GetTextureParameterTarget)BindTarget, GetTextureParameter.TextureWrapS, values);
                return (TextureWrapMode)values[0];
            }
        }

        public TextureWrapMode WrapT
        {
            get
            {
                int[] values = new int[1];
                GI.GetTexParameter((GetTextureParameterTarget)BindTarget, GetTextureParameter.TextureWrapT, values);
                return (TextureWrapMode)values[0];
            }
        }

        public uint TextureID
        {
            get { return fTextureID; }
            protected set { fTextureID = value; }
        }

        #endregion

        #region IDisposable
        protected void DeleteTextureId()
        {
            if (TextureID > 0)
                GI.DeleteTextures(1, new uint[] { TextureID });
        }

        public virtual void Dispose()
        {
            GI.DeleteTextures(1, new uint[] { TextureID });
        }
        #endregion

        #region Public Static Methods
        
        public static uint GetNewTextureID()
        {
            uint[] names = new uint[1];
            
            GL.GenTextures(1, names);

            return names[0];
        }

        public static uint[] GenerateTextureNames(uint numNames)
        {
            uint[] names = new uint[numNames];

            GL.GenTextures((int)numNames, names);

            return names;
        }
        #endregion Private Methods
    }
}
