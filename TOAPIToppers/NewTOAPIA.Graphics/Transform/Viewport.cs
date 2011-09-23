namespace NewTOAPIA.Graphics
{
    using System;

    public sealed class Viewport : ITransform
    {
        public enum aspect_ratio_e
        {
            Stretch,
            Meet,
            Slice
        }

        #region Properties
        double fWorldX1;
        double fWorldY1;
        double fWorldX2;
        double fWorldY2;

        double m_device_x1;
        double m_device_y1;
        double m_device_x2;
        double m_device_y2;

        public aspect_ratio_e AspectRatio { get; private set; }
        public double AlignX { get; private set; }
        public double AlignY { get; private set; }

        public bool IsValid { get; set; }

        double m_wx1;
        double m_wy1;
        double m_wx2;
        double m_wy2;
        double m_dx1;
        double m_dy1;
        public double ScaleX { get; private set; }
        public double ScaleY { get; private set; }

        #endregion

        #region Constructor
        public Viewport()
        {
            fWorldX1 = (0.0);
            fWorldY1 = (0.0);
            fWorldX2 = (1.0);
            fWorldY2 = (1.0);

            m_device_x1 = (0.0);
            m_device_y1 = (0.0);
            m_device_x2 = (1.0);
            m_device_y2 = (1.0);

            AspectRatio = aspect_ratio_e.Stretch;
            IsValid = true;
            AlignX = (0.5);
            AlignY = (0.5);
            m_wx1 = (0.0);
            m_wy1 = (0.0);
            m_wx2 = (1.0);
            m_wy2 = (1.0);
            m_dx1 = (0.0);
            m_dy1 = (0.0);
            ScaleX = (1.0);
            ScaleY = (1.0);
        }
        #endregion

        #region Device
        public double DeviceDx() { return m_dx1 - m_wx1 * ScaleX; }
        public double DeviceDy() { return m_dy1 - m_wy1 * ScaleY; }

        public RectangleD DeviceViewport
        {
            get
            {
                return new RectangleD(m_device_x1, m_device_y1, m_device_x2, m_device_y2);
            }

            set
            {
                m_device_x1 = value.x1;
                m_device_y1 = value.y1;
                m_device_x2 = value.x2;
                m_device_y2 = value.y2;

                Update();
            }
        }

        public void SetDeviceViewport(double x1, double y1, double x2, double y2)
        {
            m_device_x1 = x1;
            m_device_y1 = y1;
            m_device_x2 = x2;
            m_device_y2 = y2;

            Update();
        }

        public void GetDeviceViewport(out double x1, out double y1, out double x2, out double y2)
        {
            x1 = m_device_x1;
            y1 = m_device_y1;
            x2 = m_device_x2;
            y2 = m_device_y2;
        }
        #endregion

        #region World
        public RectangleD WorldViewport
        {
            get
            {
                return new RectangleD(fWorldX1, fWorldY1, fWorldX2, fWorldY2);
            }

            set
            {
                fWorldX1 = value.x1;
                fWorldY1 = value.y1;
                fWorldX2 = value.x2;
                fWorldY2 = value.y2;

                Update();
            }
        }

        public void SetWorldViewport(double x1, double y1, double x2, double y2)
        {
            fWorldX1 = x1;
            fWorldY1 = y1;
            fWorldX2 = x2;
            fWorldY2 = y2;

            Update();
        }

        public void GetWorldViewport(out double x1, out double y1, out double x2, out double y2)
        {
            x1 = fWorldX1;
            y1 = fWorldY1;
            x2 = fWorldX2;
            y2 = fWorldY2;
        }


        //-------------------------------------------------------------------
        public void GetWorldViewportActual(out double x1, out double y1, out double x2, out double y2)
        {
            x1 = m_wx1;
            y1 = m_wy1;
            x2 = m_wx2;
            y2 = m_wy2;
        }
        #endregion

        #region Transform
        //-------------------------------------------------------------------
        public void Transform(ref double x, ref double y)
        {
            x = (x - m_wx1) * ScaleX + m_dx1;
            y = (y - m_wy1) * ScaleY + m_dy1;
        }

        //-------------------------------------------------------------------
        public void TransformScaleOnly(ref double x, ref double y)
        {
            x *= ScaleX;
            y *= ScaleY;
        }

        //-------------------------------------------------------------------
        public void InverseTransform(ref double x, ref double y)
        {
            x = (x - m_dx1) / ScaleX + m_wx1;
            y = (y - m_dy1) / ScaleY + m_wy1;
        }

        //-------------------------------------------------------------------
        public void InverseTransformScaleOnly(ref double x, ref double y)
        {
            x /= ScaleX;
            y /= ScaleY;
        }
        #endregion

        #region Affine
        public double Scale()
        {
            return (ScaleX + ScaleY) * 0.5;
        }

        public Affine ToAffine()
        {
            Affine mtx = Affine.NewTranslation(-m_wx1, -m_wy1);
            mtx *= Affine.NewScaling(ScaleX, ScaleY);
            mtx *= Affine.NewTranslation(m_dx1, m_dy1);

            return mtx;
        }

        public Affine ToAffineScaleOnly()
        {
            return Affine.NewScaling(ScaleX, ScaleY);
        }
        #endregion

        public void PreserveAspectRatio(double alignx, double aligny, aspect_ratio_e aspect)
        {
            AlignX = alignx;
            AlignY = aligny;
            AspectRatio = aspect;

            Update();
        }

        private void Update()
        {
            double epsilon = 1e-30;
            if (Math.Abs(fWorldX1 - fWorldX2) < epsilon ||
               Math.Abs(fWorldY1 - fWorldY2) < epsilon ||
               Math.Abs(m_device_x1 - m_device_x2) < epsilon ||
               Math.Abs(m_device_y1 - m_device_y2) < epsilon)
            {
                m_wx1 = fWorldX1;
                m_wy1 = fWorldY1;
                m_wx2 = fWorldX1 + 1.0;
                m_wy2 = fWorldY2 + 1.0;
                m_dx1 = m_device_x1;
                m_dy1 = m_device_y1;
                ScaleX = 1.0;
                ScaleY = 1.0;
                IsValid = false;

                return;
            }

            double world_x1 = fWorldX1;
            double world_y1 = fWorldY1;
            double world_x2 = fWorldX2;
            double world_y2 = fWorldY2;
            double device_x1 = m_device_x1;
            double device_y1 = m_device_y1;
            double device_x2 = m_device_x2;
            double device_y2 = m_device_y2;
            
            if (AspectRatio != aspect_ratio_e.Stretch)
            {
                double d;
                ScaleX = (device_x2 - device_x1) / (world_x2 - world_x1);
                ScaleY = (device_y2 - device_y1) / (world_y2 - world_y1);

                if ((AspectRatio == aspect_ratio_e.Meet) == (ScaleX < ScaleY))
                {
                    d = (world_y2 - world_y1) * ScaleY / ScaleX;
                    world_y1 += (world_y2 - world_y1 - d) * AlignY;
                    world_y2 = world_y1 + d;
                }
                else
                {
                    d = (world_x2 - world_x1) * ScaleX / ScaleY;
                    world_x1 += (world_x2 - world_x1 - d) * AlignX;
                    world_x2 = world_x1 + d;
                }
            }
 
            m_wx1 = world_x1;
            m_wy1 = world_y1;
            m_wx2 = world_x2;
            m_wy2 = world_y2;
            m_dx1 = device_x1;
            m_dy1 = device_y1;
            ScaleX = (device_x2 - device_x1) / (world_x2 - world_x1);
            ScaleY = (device_y2 - device_y1) / (world_y2 - world_y1);
            IsValid = true;
        }
    }
}