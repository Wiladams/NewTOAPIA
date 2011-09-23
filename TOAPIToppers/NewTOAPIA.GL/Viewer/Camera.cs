using System;
using System.Collections.Generic;
using System.Text;

namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public class Camera : IRealizable, IPlaceable
    {
        public GraphicsInterface GI { get; set; }

        Point3D fLookAt;
        Point3D fPosition;
        Vector3D fUp;

        public Camera(GraphicsInterface gi, Point3D location, Point3D lookAt, Vector3D up)
        {
            GI = gi;

            fPosition = location;
            fLookAt = lookAt;
            fUp = up;
        }

                public virtual Point3D Position
        {
            get { return fPosition; }
            set
            {
                SetPosition(value);
            }
        }

        public virtual void SetPosition(Point3D vec)
        {
            fPosition = vec;
            OnPositionChanged();
        }

        protected virtual void OnPositionChanged()
        {
        }

        public Point3D LookAt
        {
            get { return fLookAt; }
            set
            {
                SetLookAt(value);
            }
        }

        public void SetLookAt(Point3D vec)
        {
            fLookAt = vec;
            OnLookAtChanged();
        }

        protected virtual void OnLookAtChanged()
        {
        }

        public Vector3D Up
        {
            get { return fUp; }
            set
            {
                SetUp(value);
            }
        }

        public virtual void SetUp(Vector3D vec)
        {
            fUp = vec;
            OnUpChanged();
        }

        protected virtual void OnUpChanged()
        {
        }

        protected virtual void Changed()
        {
            OnLookAtChanged();
            OnPositionChanged();
            OnUpChanged();
        }

        public virtual void Realize()
        {
        }
    }
}
