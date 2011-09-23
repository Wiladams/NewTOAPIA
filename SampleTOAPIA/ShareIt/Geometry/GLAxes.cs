using System;

using NewTOAPIA;
using NewTOAPIA.GL;
using NewTOAPIA.Drawing;
using NewTOAPIA.Modeling;
using NewTOAPIA.GLU.Shapes;

namespace NewTOAPIA.Shapes
{
    public class GLSpear : IRenderable
    {
        ColorRGBA fShaftColor;
        float3 fVector;

        float fAxisRadius = 0.025f;
        float fSpearLength = 1.0f;
        float fArrowRadius = 0.06f;
        float fArrowHeight = 0.1f;

        GLUDisk fDisk;
        GLUCylinder fAxisShaft;
        GLUCone fArrowHead;

        public GLSpear(float3 vec, ColorRGBA shaftColor)
        {
            fShaftColor = shaftColor;
            fVector = vec;

            fSpearLength = fVector.Length;

            fDisk = new GLUDisk(fAxisRadius, fAxisRadius, 10, 1);
            fDisk.DrawingStyle = QuadricDrawStyle.Fill;
            fDisk.NormalType = QuadricNormalType.Smooth;
            fDisk.Orientation = QuadricOrientation.Outside;
            fDisk.UsesTexture = 0;

            fAxisShaft = new GLUCylinder(fAxisRadius, fSpearLength, 10, 1);
            fAxisShaft.NormalType = QuadricNormalType.Smooth;
            fAxisShaft.Orientation = QuadricOrientation.Outside;
            fAxisShaft.UsesTexture = 0;

            fArrowHead = new GLUCone(fArrowRadius, fArrowHeight, 10, 1);
            fArrowHead.NormalType = QuadricNormalType.Smooth;
            fArrowHead.Orientation = QuadricOrientation.Outside;
            fArrowHead.UsesTexture = 0;

        }

        public virtual void Render(GraphicsInterface gi)
        {
            gi.Color(fShaftColor);
            fAxisShaft.Render(gi);
            gi.PushMatrix();
                gi.Translate(0.0f, 0.0f, fSpearLength);
                fArrowHead.Render(gi);
                gi.Rotate(180.0f, 1.0f, 0.0f, 0.0f);
                fDisk.Render(gi);
            gi.PopMatrix();

        }
    }

    public class GLAxes : IRenderable
    {
        GLUSphere fSphere;

        GLSpear fXAxis;     // Red
        GLSpear fYAxis;     // Green
        GLSpear fZAxis;     // Blue

        public GLAxes()
            :this(1.0f)
        {
        }

        public GLAxes(float axisLength)
        {
            fXAxis = new GLSpear(new float3(axisLength, 0, 0), ColorRGBA.Red);
            fYAxis = new GLSpear(new float3(0, axisLength, 0), ColorRGBA.Green);
            fZAxis = new GLSpear(new float3(0, 0, axisLength), ColorRGBA.Blue);

            fSphere = new GLUSphere(0.05f, 15, 15);
        }

        /// <summary>
        // Draw the unit axis. A small white sphere represents the origin
        // and the three axes are colored Red, Green, and Blue, which 
        // corresponds to positive X, Y, and Z respectively. 
        // Each axis has an arrow on the end.
        // normals are provided should the axes be lit. 
        /// 
        /// </summary>
        /// <param name="gi"></param>
        public virtual void Render(GraphicsInterface gi)
        {
            // Draw the blue Z axis spear first
            // It's easy as it does not require any rotations as the 
            // quadrics naturally align with the positive z axis
            fZAxis.Render(gi);

            // Draw the Red X axis 2nd, with arrowed head
            // To draw along the x-axis, we rotate around the 
            // y axis
            gi.PushMatrix();
                gi.Rotate(90.0f, 0.0f, 1.0f, 0.0f);
                fXAxis.Render(gi);
            gi.PopMatrix();

            // Draw the Green Y axis 3rd, with arrowed head
            gi.PushMatrix();
                gi.Rotate(-90.0f, 1.0f, 0.0f, 0.0f);
                fYAxis.Render(gi);
            gi.PopMatrix();

            // White Sphere at origin
            gi.Color(ColorRGBA.White);
            fSphere.Render(gi);

        }
    }
}
