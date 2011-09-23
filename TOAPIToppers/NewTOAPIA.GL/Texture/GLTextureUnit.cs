using System;
using System.Collections.Generic;
using System.Text;
using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    /// <summary>
    /// The GPU may have multiple texture units.  By definition there is at least
    /// one, and in many cases there will be multiple.  When a texture is 'bound',
    /// it is bound to a specific texture unit.  TextureUnit0, is the default
    /// texture unit.
    /// 
    /// In order to access a texture from within GLSL code, the texture is referenced
    /// based on the TextureUnit that it is bound to.  So, for example, a texture
    /// that is bound to TextureUnit3 would be accessed in a sampler2D using the
    /// ordinal value '3'.
    /// 
    /// 
    /// 
    /// The property 'OrdinalForShaders' gives this value.
    /// </summary>
    public class GLTextureUnit : IBindable
    {
        #region Fields
        TextureUnitID fTextureUnitID;
        GraphicsInterface fGI;
        #endregion

        #region Constructor
        internal GLTextureUnit(GraphicsInterface gi, TextureUnitID unitID)
        {
            fGI = gi;
            fTextureUnitID = unitID;
        }
        #endregion

        #region Properties
        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        public int OrdinalForShaders
        {
            get
            {
                int retValue = (int)fTextureUnitID - (int)TextureUnitID.Unit0;
                return retValue;
            }
        }
        
        public TextureUnitID UnitID
        {
            get { return fTextureUnitID; }
        }

        #endregion

        #region Methods
        public virtual void AssignTexture(GLTexture2D aTexture)
        {
            Bind();
            aTexture.Bind();
        }

        //public virtual void Enable2DTexture()
        //{
        //    Bind();
        //    GI.Features.Texturing2D.Enable();
        //}

        //public virtual void Disable2DTexture()
        //{
        //    Bind();
        //    GI.Features.Texturing2D.Disable();
        //}
        #endregion

        #region IBindable
        public virtual void Bind()
        {
            GI.ActiveTexture(UnitID);
        }

        public virtual void Unbind()
        {
            GI.ActiveTexture(TextureUnitID.Unit0);
        }
        #endregion

        #region Static Instances
        public static int GetMaxTextureUnits(GraphicsInterface gi)
        {
            if (null == gi)
                return 0;

            return gi.State.MaxTextureUnits;
        }

        public static GLTextureUnit GetTextureUnit(GraphicsInterface gi, TextureUnitID unitID)
        {
            GLTextureUnit aUnit = new GLTextureUnit(gi, unitID);

            return aUnit;
        }
        
        //public static GLTextureUnit Base = new GLTextureUnit(TextureUnitID.Unit0);
        //public static GLTextureUnit Unit0 = new GLTextureUnit(TextureUnitID.Unit0);
        //public static GLTextureUnit Unit1 = new GLTextureUnit(TextureUnitID.Unit1);
        //public static GLTextureUnit Unit2 = new GLTextureUnit(TextureUnitID.Unit2);
        //public static GLTextureUnit Unit3 = new GLTextureUnit(TextureUnitID.Unit3);
        //public static GLTextureUnit Unit4 = new GLTextureUnit(TextureUnitID.Unit4);
        //public static GLTextureUnit Unit5 = new GLTextureUnit(TextureUnitID.Unit5);
        //public static GLTextureUnit Unit6 = new GLTextureUnit(TextureUnitID.Unit6);
        //public static GLTextureUnit Unit7 = new GLTextureUnit(TextureUnitID.Unit7);
        //public static GLTextureUnit Unit8 = new GLTextureUnit(TextureUnitID.Unit8);
        //public static GLTextureUnit Unit9 = new GLTextureUnit(TextureUnitID.Unit9);
        //public static GLTextureUnit Unit10 = new GLTextureUnit(TextureUnitID.Unit10);
        //public static GLTextureUnit Unit11 = new GLTextureUnit(TextureUnitID.Unit11);
        //public static GLTextureUnit Unit12 = new GLTextureUnit(TextureUnitID.Unit12);
        //public static GLTextureUnit Unit13 = new GLTextureUnit(TextureUnitID.Unit13);
        //public static GLTextureUnit Unit14 = new GLTextureUnit(TextureUnitID.Unit14);
        //public static GLTextureUnit Unit15 = new GLTextureUnit(TextureUnitID.Unit15);
        //public static GLTextureUnit Unit16 = new GLTextureUnit(TextureUnitID.Unit16);
        //public static GLTextureUnit Unit17 = new GLTextureUnit(TextureUnitID.Unit17);
        //public static GLTextureUnit Unit18 = new GLTextureUnit(TextureUnitID.Unit18);
        //public static GLTextureUnit Unit19 = new GLTextureUnit(TextureUnitID.Unit19);
        //public static GLTextureUnit Unit20 = new GLTextureUnit(TextureUnitID.Unit20);
        //public static GLTextureUnit Unit21 = new GLTextureUnit(TextureUnitID.Unit21);
        //public static GLTextureUnit Unit22 = new GLTextureUnit(TextureUnitID.Unit22);
        //public static GLTextureUnit Unit23 = new GLTextureUnit(TextureUnitID.Unit23);
        //public static GLTextureUnit Unit24 = new GLTextureUnit(TextureUnitID.Unit24);
        //public static GLTextureUnit Unit25 = new GLTextureUnit(TextureUnitID.Unit25);
        //public static GLTextureUnit Unit26 = new GLTextureUnit(TextureUnitID.Unit26);
        //public static GLTextureUnit Unit27 = new GLTextureUnit(TextureUnitID.Unit27);
        //public static GLTextureUnit Unit28 = new GLTextureUnit(TextureUnitID.Unit28);
        //public static GLTextureUnit Unit29 = new GLTextureUnit(TextureUnitID.Unit29);
        //public static GLTextureUnit Unit30 = new GLTextureUnit(TextureUnitID.Unit30);
        //public static GLTextureUnit Unit31 = new GLTextureUnit(TextureUnitID.Unit31);
        #endregion
    }


}
