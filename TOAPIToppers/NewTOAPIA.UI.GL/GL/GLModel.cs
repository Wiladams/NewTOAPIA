using System;
using System.Collections.Generic;

using TOAPI.OpenGL;

namespace NewTOAPIA.UI.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.GL;
    using NewTOAPIA.Graphics;

    public class GLModel
    {
        vec3 fCameraRotation;
        vec3 fCameraLocation;

        bool fMouseLeftDown;
        bool fMouseRightDown;
        ivec2 fMousePosition;

        GraphicsInterface fGI;

        public int ViewportWidth { get; set; }
        public int ViewportHeight { get; set; }

        public GLModel()
        {
            fCameraRotation = new vec3(0, 0, 0);
            fCameraLocation = new vec3(0, 0, 5);
        }


        public void SetContext(GLContext glContext)
        {
            fGI = new GraphicsInterface(glContext);
            OnSetContext();
        }

        protected virtual void OnSetContext()
        {
            GraphicsInterface.gCheckErrors = true;

            // Enable some features
            GI.Features.DepthTest.Enable();

            // track material ambient and diffuse from surface color, 
            // call it before glEnable(GL_COLOR_MATERIAL)
            //GI.ColorMaterial(GLFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            //GI.Features.ColorMaterial.Enable();

            GI.Buffers.ColorBuffer.Color = ColorRGBA.Cyan;  // background color

            //InitializeLighting();

            // Set the default camera position and where it is looking
            SetCamera(new vec3(0, 0, 10), new vec3(0, 0, 0));
        }


        public void ReleaseContext()
        {
            OnReleaseContext();
        }

        /// <summary>
        /// This routine is called just before the context is released.  A subclasser
        /// should override this and perform any actions to shutdown before the 
        /// context is released.
        /// </summary>
        protected virtual void OnReleaseContext()
        {
        }

        public GraphicsInterface GI
        {
            get { return fGI; }
        }

        #region Camera Management
        public virtual void SetCamera(vec3 pos, vec3 target)
        {
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Glu.LookAt(pos.x, pos.y, pos.z, target.x, target.y, target.z, 0, 1, 0); // eye(x,y,z), focal(x,y,z), up(x,y,z)
        }

        public virtual void RotateCamera(Vector2i rotation)
        {
            if (fMouseLeftDown)
            {
                fCameraRotation.y += (rotation.x - fMousePosition.x);
                fCameraRotation.x += (rotation.y - fMousePosition.y);
                fMousePosition.x = rotation.x;
                fMousePosition.y = rotation.y;
            }

        }

        public virtual void ZoomCamera(float distance)
        {
        }
        #endregion

        public virtual void OnSetViewport(int width, int height)
        {
            ViewportWidth = width;
            ViewportHeight = height;
        }

        public ColorRGBA AmbientColor
        {
            set
            {
                GI.LightModel(LightModelParameter.LightModelAmbient, (float[])value);
            }
        }

        public virtual void InitializeLighting()
        {
            GI.Features.Lighting.Enable();

            AmbientColor = ColorRGBA.White;
        }

        protected virtual void DrawBegin()
        {
            // set viewport to be the entire window
            GI.Viewport(0, 0, ViewportWidth, ViewportHeight);

            // set perspective viewing frustum
            float aspectRatio = (float)ViewportWidth / ViewportHeight;
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            GI.Glu.Perspective(50.0f, (float)(ViewportWidth) / ViewportHeight, 0.1f, 20.0f); // FOV, AspectRatio, NearClip, FarClip

            // switch to modelview matrix in order to set scene
            GI.MatrixMode(MatrixMode.Modelview);


            // Clear color and depth buffers
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();
        }

        protected virtual void DrawContent()
        {
            // save the initial ModelView matrix before modifying ModelView matrix
            GI.PushMatrix();

            // trassform camera
            GI.Translate(0, 0, fCameraLocation.z);
            GI.Rotate(fCameraRotation.x, 1, 0, 0);   // pitch
            GI.Rotate(fCameraRotation.y, 0, 1, 0);   // heading

            // draw a triangle
            GI.Drawing.Triangles.Begin();
                //GI.Normal(0, 0, 1);
                GI.Color(1.0f, 0, 0);
                GI.Vertex(3, -2, 0);
                GI.Color(0, 1f, 0);
                GI.Vertex(0, 2, 0);
                GI.Color(0, 0, 1f);
                GI.Vertex(-3, -2, 0);
            GI.Drawing.Triangles.End();

            GI.PopMatrix();
        }

        protected virtual void DrawEnd()
        {
            GI.Flush();
        }

        public virtual void Draw()
        {
            DrawBegin();
            DrawContent();
            DrawEnd();
        }

        #region Keyboard Management
        public virtual IntPtr OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            return new IntPtr(1);
        }

        #endregion

        #region Mouse Management
        public virtual void OnMouseActivity(Object sender, MouseActivityArgs mea)
        {
            switch (mea.ActivityType)
            {
                case MouseActivityType.MouseDown:
                    SetMousePosition(mea.X, mea.Y);
                    if (mea.ButtonActivity == MouseButtonActivity.LeftButtonDown)
                        SetMouseLeft(true);
                    if (mea.ButtonActivity == MouseButtonActivity.RightButtonDown)
                        SetMouseRight(true);
                    break;

                case MouseActivityType.MouseUp:
                    SetMousePosition(mea.X, mea.Y);
                    if (mea.ButtonActivity == MouseButtonActivity.LeftButtonUp)
                        SetMouseLeft(false);
                    if (mea.ButtonActivity == MouseButtonActivity.RightButtonUp)
                        SetMouseRight(false);
                    break;

                case MouseActivityType.MouseMove:
                    if (this.fMouseLeftDown)
                    {
                        RotateCamera(new Vector2i(mea.X, mea.Y));
                    }
                    if (this.fMouseRightDown)
                    {
                        ZoomCamera(mea.Y);
                    }
                    break;
            }
        }

        public virtual void SetMouseLeft(bool flag) 
        { 
            fMouseLeftDown = flag; 
        }

        public virtual void SetMouseRight(bool flag) 
        { 
            fMouseRightDown = flag; 
        }

        public virtual void SetMousePosition(int x, int y) 
        { 
            fMousePosition.x = x; 
            fMousePosition.y = y;
        }
        #endregion

    }
}
