using System;
using System.Collections.Generic;
using System.Text;

using TOAPI.OpenGL;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public partial class GraphicsInterface
    {
        #region Names
        internal void InitNames()
        {
            gl.glInitNames();
        }


        internal void LoadName(uint name)
        {
            gl.glLoadName(name);
        }

        internal void PopName()
        {
            gl.glPopName();
        }

        internal void PushName(uint name)
        {
            gl.glPushName(name);
        }
        #endregion
 
        #region Clearing Buffers
        internal void Clear(ClearBufferMask flags)
        {
            gl.glClear((int)flags);
            CheckException();
        }


        internal void ClearAccumBuffer()
        {
            Clear(ClearBufferMask.AccumBufferBit);
        }

        internal void ClearStencilBuffer()
        {
            Clear(ClearBufferMask.StencilBufferBit);
        }


        internal void ClearAccum(float red, float green, float blue, float alpha)
        {
            gl.glClearAccum(red, green, blue, alpha);
            CheckException();
        }

        internal void ClearColor(ColorRGBA aColor)
        {
            ClearColor(aColor.R, aColor.G, aColor.B, aColor.A);
        }

        internal void ClearColor(float red, float green, float blue, float alpha)
        {
            gl.glClearColor(red, green, blue, alpha);
            CheckException();
        }


        internal void ClearDepth(double depth)
        {
            gl.glClearDepth(depth);
            CheckException();
        }

        internal void ClearIndex(float c)
        {
            gl.glClearIndex(c);
            CheckException();
        }

        internal void ClearStencil(int s)
        {
            gl.glClearStencil(s);
            CheckException();
        }
        #endregion

        #region Client State
        internal void PopClientAttrib()
        {
            gl.glPopClientAttrib();
            CheckException();
        }

        internal void PushClientAttrib(ClientAttribMask mask)
        {
            gl.glPushClientAttrib((int)mask);
            CheckException();
        }

        internal void DisableClientState(ClientArrayType array)
        {
            gl.glDisableClientState((int)array);
            CheckException();
        }

        internal void EnableClientState(ClientArrayType array)
        {
            gl.glEnableClientState((int)array);
            CheckException();
        }
        #endregion

        #region Display List
        internal void CallList(uint list)
        {
            gl.glCallList(list);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        internal void CallLists(int n, ListNameType type, uint[] lists)
        {
            gl.glCallLists(n, (int)type, lists);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        internal void EndList()
        {
            gl.glEndList();
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }

        internal void ListBase(uint abase)
        {
            gl.glListBase(abase);
            CheckException();
        }

        internal void NewList(uint list, ListMode mode)
        {
            gl.glNewList(list, (int)mode);
            CheckException();
        }

        internal uint GenLists(int range)
        {
            return gl.glGenLists(range);
        }

        internal void DeleteLists(uint list, int range)
        {
            gl.glDeleteLists(list, range);
            CheckException();
        }

        internal Boolean IsList(uint list)
        {
            return gl.glIsList(list);
        }

        #endregion Display Lists

        #region Lighting
        internal void GetLightfv(GLLightName light, LightParameter pname, float[] parameters)
        {
            gl.glGetLightfv((int)light, (int)pname, parameters);
        }

        internal void GetLightiv(GLLightName light, LightParameter pname, int[] parameters)
        {
            gl.glGetLightiv((int)light, (int)pname, parameters);
        }

        internal void Light(GLLightName light, LightParameter pname, float param)
        {
            gl.glLightf((int)light, (int)pname, param);
            CheckException();
        }

        internal void Light(GLLightName light, LightParameter pname, float[] parameters)
        {
            gl.glLightfv((int)light, (int)pname, parameters);
            CheckException();
        }

        internal void Light(GLLightName light, LightParameter pname, Point3D position)
        {
            gl.glLightfv((int)light, (int)pname, (float[])position);
            CheckException();
        }

        internal void Light(GLLightName light, LightParameter pname, int param)
        {
            gl.glLighti((int)light, (int)pname, param);
            CheckException();
        }

        internal void Light(GLLightName light, LightParameter pname, int[] parameters)
        {
            gl.glLightiv((int)light, (int)pname, parameters);
            CheckException();
        }
        #endregion
        
        #region LightModel
        public void LightModel(LightModelParameter pname, float param)
        {
            gl.glLightModelf((int)pname, param);
            CheckException();
        }

        public void LightModel(LightModelParameter pname, float[] parameters)
        {
            gl.glLightModelfv((int)pname, parameters);
            CheckException();
        }

        public void LightModel(LightModelParameter pname, int param)
        {
            gl.glLightModeli((int)pname, param);
            CheckException();
        }

        public void LightModel(LightModelParameter pname, int[] parameters)
        {
            gl.glLightModeliv((int)pname, parameters);
            CheckException();
        }

        public void LightModel(LightModelParameter pname, LightModel param)
        {
            gl.glLightModeli((int)pname, (int)param);
            CheckException();
        }
        #endregion

        #region Begin and End Modes
        internal void Begin(BeginMode aMode)
        {
            gl.glBegin((int)aMode);
            // Don't check for error, because this is called between
            // glBegin()/glEnd()
            //CheckException();
        }


        internal void End()
        {
            gl.glEnd();
            CheckException();
        }
        #endregion

        #region Draw Pixels
        //internal void DrawPixels(GLPixelRectangleInfo pixelRect)
        //{
        //    if (pixelRect.BufferObject == null)
        //        return ;

        //    IntPtr mappedPtr = pixelRect.BufferObject.MapBuffer(BufferAccess.ReadOnly);

        //    if (mappedPtr == null)
        //        return ;

        //    DrawPixels(pixelRect.Width, pixelRect.Height, pixelRect.PixelFormat, pixelRect.PixelType, mappedPtr);

        //    pixelRect.BufferObject.UnmapBuffer();
        //}

        //public void DrawPixels(int x, int y, GLPixelRectangleInfo pixelRect)
        //{
        //    RasterPos2i(x, y);
        //    DrawPixels(pixelRect);
        //}

        public void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, IntPtr pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }


        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, byte[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, sbyte[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, ushort[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, short[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, uint[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, int[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void DrawPixels(int width, int height, PixelLayout format, PixelComponentType type, float[] pixels)
        {
            gl.glDrawPixels(width, height, (int)format, (int)type, pixels);
            CheckException();
        }
        #endregion


        internal void Bitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, byte[] bitmap)
        {
            gl.glBitmap(width, height, xorig, yorig, xmove, ymove, bitmap);
            CheckException();
        }

        internal void CopyPixels(int x, int y, int width, int height, PixelCopyType type)
        {
            gl.glCopyPixels(x, y, width, height, (int)type);
            CheckException();
        }

        #region ReadPixels
        internal void ReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, IntPtr pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        internal void ReadPixels(int x, int y, int width, int height, PixelLayout format, PixelComponentType type, byte[] pixels)
        {
            gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
            CheckException();
        }

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, sbyte[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, short[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, ushort[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, int[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, uint[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        //internal void ReadPixels(int x, int y, int width, int height, GLPixelFormat format, PixelComponentType type, float[] pixels)
        //{
        //    gl.glReadPixels(x, y, width, height, (int)format, (int)type, pixels);
        //    CheckException();
        //}

        #endregion

        internal void CopyTexImage1D(int level, TextureInternalFormat internalFormat, int x, int y, int width, int border)
        {
            gl.glCopyTexImage1D((int)Texture1DTarget.Texture1d, level, (int)internalFormat, x, y, width, border);
            CheckException();
        }

        internal void CopyTexImage2D(Texture2DTarget target, int level, TextureInternalFormat internalFormat, int x, int y, int width, int height, int border)
        {
            gl.glCopyTexImage2D((int)target, level, (int)internalFormat, x, y, width, height, border);
            CheckException();
        }

        internal void CopyTexSubImage1D(int level, int xoffset, int x, int y, int width)
        {
            gl.glCopyTexSubImage1D((int)Texture1DTarget.Texture1d, level, xoffset, x, y, width);
            CheckException();
        }

        internal void CopyTexSubImage2D(Texture2DTarget target, int level, int xoffset, int yoffset, int x, int y, int width, int height)
        {
            gl.glCopyTexSubImage2D((int)target, level, xoffset, yoffset, x, y, width, height);
            CheckException();
        }

        #region Server State
        public void PopAttrib()
        {
            gl.glPopAttrib();
            CheckException();
        }

        public void PushAttrib(AttribMask mask)
        {
            gl.glPushAttrib((int)mask);
            CheckException();
        }

        #region Enabling Server side featurees
        internal void Enable(GLOption cap)
        {
            gl.glEnable((int)cap);
            CheckException();
        }

        internal void Disable(GLOption cap)
        {
            gl.glDisable((int)cap);
            CheckException();
        }
        #endregion Server side features
        #endregion

        #region Texturing
        internal void GenTextures(int n, uint[] textures)
        {
            gl.glGenTextures(n, textures);
            CheckException();
        }

        #region Texture Residence
        internal bool AreTexturesResident(int n, uint[] textures, byte[] residences)
        {
            return gl.glAreTexturesResident(n, textures, residences);
        }

        internal bool AreTexturesResident(int n, uint[] textures, bool[] residences)
        {
            return gl.glAreTexturesResident(n, textures, residences);
        }
        #endregion

        #endregion
    }
}
