using System;

using NewTOAPIA;
using NewTOAPIA.UI;
using NewTOAPIA.GL;

using MS3D;

namespace PointSprites
{
    class MS3DScene : GLModel
    {
        public const int SCREEN_X = 800;
        public const int SCREEN_Y = 600;
        float2 fViewPortSize;
        float fAspect;

        // Camera Position
        Vector3f fCameraLocation;
        Vector3f fCameraRotation;

        bool fRenderWithLighting;
        bool fRenderWireFrame;
        bool fRenderWithMaterial;
        bool fRenderJoints;
        bool fRenderFlatShaded;
        bool fTexturesLoaded;

        MS3DModel fModel;
        MS3DMesh3D fMesh;
        MS3DModelRenderer fModelRenderer;
        //float3 dim;
        //float3 center;
        //float radius;

        public MS3DScene()
        {
            fModel = null;
            fCameraLocation = new Vector3f(0.0f, -4.0f, -20.0f);
            fCameraRotation = new Vector3f(0.0f, 0.0f, 0.0f);

            fRenderWithLighting = true;
            fRenderWireFrame = false;
            fRenderWithMaterial = true;
            fRenderJoints = true;
            fRenderFlatShaded = false;

            //dim = new float3(1, 1, 1);
            //center = new float3(0, 0, 0);
            //radius = 1.41f;
        }

        protected override void OnSetContext()
        {
            // Load in a model
            fModel = MS3DModel.CreateFromFile("animated_cylinder.ms3d");
            if (null != fModel)
            {
                fMesh = new MS3DMesh3D(fModel);
                fModelRenderer = new MS3DModelRenderer(fModel);

                //float3 mins = new float3();
                //float3 maxs = new float3();

                //MS3DMath.ClearBounds(ref mins, ref maxs);
                //foreach (MS3DVertex vertex in fModel.Vertices)
                //{
                //    MS3DMath.AddPointToBounds(vertex.vertex, ref mins, ref maxs);
                //}

                //dim[0] = maxs[0] - mins[0];
                //dim[1] = maxs[1] - mins[1];
                //dim[2] = maxs[2] - mins[2];
                //center[0] = mins[0] + dim[0] / 2.0f;
                //center[1] = mins[1] + dim[1] / 2.0f;
                //center[2] = mins[2] + dim[2] / 2.0f;
                //radius = dim[0];
                //if (dim[1] > radius)
                //    radius = dim[1];
                //if (dim[2] > radius)
                //    radius = dim[2];
                //radius *= 1.41f;

                fTexturesLoaded = false;


                ResetView();
            }



            GI.Buffers.ColorBuffer.Color = new GLColor(0.2f, 0.5f, 0.7f, 1.0f);
        }


        protected override void DrawBegin()
        {
            //base.BeginScene();

            GI.Viewport(0, 0, (int)fViewPortSize.x, (int)fViewPortSize.y);
            GI.Buffers.ColorBuffer.Color = new GLColor(0.2f, 0.5f, 0.7f, 1.0f);
            GI.Buffers.ColorBuffer.Clear();
            GI.Buffers.DepthBuffer.Clear();


            GI.MatrixMode(MatrixMode.Projection);
            GI.LoadIdentity();
            float nearPlane = fCameraLocation.z - fModel.Radius;
            if (nearPlane < 0.1f)
                nearPlane = 0.1f;
            float farPlane = fCameraLocation.z + fModel.Radius;
            if (farPlane < nearPlane)
                farPlane = 4096.0f;
            GI.Glu.Perspective(55.0f, (float)fViewPortSize.x / (float)fViewPortSize.y, nearPlane, farPlane);


            if (fRenderWithLighting)
                GI.Features.Lighting.Enable();
            else
                GI.Features.Lighting.Disable();
            
            GI.Features.Lighting.Light0.Enable();
            GI.Features.DepthTest.Enable();
            GI.Features.CullFace.Enable();

            GI.MatrixMode(MatrixMode.Modelview);
            GI.LoadIdentity();
            float4 lightPos = new float4( 0.0f, 0.0f, fCameraLocation.z, 0.0f );
            GI.Features.Lighting.Light0.Location = lightPos;

            GI.Translate(-fCameraLocation.x, -fCameraLocation.y, -fCameraLocation.z);
            GI.Rotate(fCameraRotation.x, 1.0f, 0.0f, 0.0f);
            GI.Rotate(fCameraRotation.y, 0.0f, 1.0f, 0.0f);
            GI.Translate(-fModel.Center[0], -fModel.Center[1], -fModel.Center[2]);
        }

        protected override void DrawContent()
        {
            // Render the mesh
            if (fRenderWireFrame)
                GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Line);
            else
                GI.PolygonMode(GLFace.FrontAndBack, PolygonMode.Fill);

            //fMesh.Render(GI); 
            //fModelRenderer.Render(GI, false, true);
            fModelRenderer.RenderBaseModel(GI, true, false);
        }

        protected override void DrawEnd()
        {
            GI.Features.Lighting.Disable();
            GI.Features.DepthTest.Disable();

            GI.Flush();
        }

        public override void OnKeyboardActivity(object sender, KeyboardActivityArgs kbde)
        {
            base.OnKeyboardActivity(sender, kbde);

            if (kbde.EventType == KeyEventType.KeyDown)
            {
                switch (kbde.VirtualKeyCode)
                {
                    case VirtualKeyCodes.Up:
                        fCameraLocation.z += 0.5f;
                        break;

                    case VirtualKeyCodes.Down:
                        fCameraLocation.z -= 0.5f;
                        break;

                    case VirtualKeyCodes.Left:
                        if (!kbde.Shift)
                            fCameraRotation.y += 10;
                        else
                            fCameraLocation.x += 0.5f;
                        break;

                    case VirtualKeyCodes.Right:
                        if (!kbde.Shift)
                            fCameraRotation.y -= 10;
                        else
                            fCameraLocation.x -= 0.5f;
                        break;

                    case VirtualKeyCodes.PageUp:
                        fCameraLocation.y -= 0.5f;
                        break;

                    case VirtualKeyCodes.PageDown:
                        fCameraLocation.y += 0.5f;
                        break;

                    default:
                        break;
                }

            }
        }

        void ResetView()
        {
            fCameraLocation.x = 0.0f;
            fCameraLocation.y = 0.0f;
            fCameraLocation.z = fModel.Radius;
            fCameraRotation.y = 0.0f;
            fCameraRotation.x = 0.0f;

            //_i->lastTranslationX = _i->translationX;
            //_i->lastTranslationY = _i->translationY;
            //_i->lastTranslationZ = _i->translationZ;
            //_i->lastRotationY = _i->rotationY;
            //_i->lastRotationX = _i->rotationX;

            //_i->mouseButtonLeft = false;
            //_i->mouseButtonMiddle = false;
            //_i->mouseButtonRight = false;
            //_i->mouseModifiers = 0;
            //_i->lastMouseX = 0;
            //_i->lastMouseY = 0;
        }

            public override void  OnSetViewport(int w, int h)
        {
            // Prevent a divide by zero, when window is too short
            // (you cant make a window of zero width).
            if (h == 0)
                h = 1;

            //GI.Viewport(0, 0, w, h);

            fAspect = (float)w / (float)h;
            fViewPortSize.x = w;
            fViewPortSize.y = h;

        }
    }
}

