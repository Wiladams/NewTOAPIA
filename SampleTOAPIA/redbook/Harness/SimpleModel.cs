using System;
using System.Collections.Generic;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.UI;
using TOAPI.OpenGL;

    public class SimpleInputModel : GLModel
    {
        float3 fCameraRotation;
        float3 fCameraLocation;

        bool fMouseLeftDown;
        bool fMouseRightDown;
        Vector2i fMousePosition;


        public SimpleInputModel()
        {
            fCameraRotation = new float3(0, 0, 0);
            fCameraLocation = new float3(0, 0, 5);
        }



        protected override void OnSetContext()
        {
            GraphicsInterface.gCheckErrors = true;

            GI.Buffers.ColorBuffer.Color = ColorRGBA.Cyan;   // background color

            GI.ShadeModel(ShadingModel.Smooth);

            // Set the default camera position and where it is looking
            SetCamera(new float3(0, 0, 10), new float3(0, 0, 0));
        }


        #region Camera Management
        public override void SetCamera(float3 pos, float3 target)
        {
            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            GI.Glu.LookAt(pos.x, pos.y, pos.z, target.x, target.y, target.z, 0, 1, 0); // eye(x,y,z), focal(x,y,z), up(x,y,z)
        }

        public override void RotateCamera(Vector2i rotation)
        {
            if (fMouseLeftDown)
            {
                fCameraRotation.y += (rotation.x - fMousePosition.x);
                fCameraRotation.x += (rotation.y - fMousePosition.y);
                fMousePosition.x = rotation.x;
                fMousePosition.y = rotation.y;
            }

        }

        public override void ZoomCamera(float distance)
        {
        }
        #endregion

        protected override void DrawBegin()
        {
            // set perspective viewing frustum
            float aspectRatio = (float)ViewportWidth / ViewportHeight;
            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            GI.Glu.Perspective(50.0f, (float)(ViewportWidth) / ViewportHeight, 0.1f, 20.0f); // FOV, AspectRatio, NearClip, FarClip

            // switch to modelview matrix in order to set scene
            GI.MatrixMode(MatrixMode.Modelview);

            // clear buffer
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();

            // Lighting model
            GI.Features.Lighting.Disable();
            GI.Features.Lighting.Light0.Disable();
        }

        protected override void DrawContent()
        {
            // save the initial ModelView matrix before modifying ModelView matrix
            GI.PushMatrix();

            // tramsform camera
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




        #region Mouse Management
        public override void OnMouseActivity(Object sender, MouseActivityArgs mea)
        {
            Console.WriteLine("SimputInputModel.OnMouseActivity - Event Type: {0},  Button Activity: {1}",
                mea.ActivityType, mea.ButtonActivity);

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
                    if (mea.ButtonActivity == MouseButtonActivity.LeftButtonDown)
                    {
                        RotateCamera(new Vector2i(mea.X, mea.Y));
                    }
                    if (mea.ButtonActivity == MouseButtonActivity.RightButtonDown)
                    {
                        ZoomCamera(mea.Y);
                    }
                    break;
            }
        }

        public override void SetMouseLeft(bool flag)
        {
            fMouseLeftDown = flag;
        }

        public override void SetMouseRight(bool flag)
        {
            fMouseRightDown = flag;
        }

        public override void SetMousePosition(int x, int y)
        {
            fMousePosition.x = x;
            fMousePosition.y = y;
        }
        #endregion

    }

