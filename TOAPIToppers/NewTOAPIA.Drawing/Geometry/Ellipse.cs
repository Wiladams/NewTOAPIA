

namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    //----------------------------------------------------------------ellipse
    public class Ellipse : IVertexSource
    {
        Point2D m_Origin;
        public double m_rx;
        public double m_ry;
        private double m_scale;
        private int m_num;
        private int m_step;
        private bool m_cw;

        public Ellipse()
        {
            m_Origin = new Point2D();
            m_rx = 1.0;
            m_ry = 1.0;
            m_scale = 1.0;
            m_num = 4;
            m_step = 0;
            m_cw = false;
        }

        public Ellipse(double OriginX, double OriginY, double RadiusX, double RadiusY)
            : this(OriginX, OriginY, RadiusX, RadiusY, 0, false)
        {
        }

        public Ellipse(double OriginX, double OriginY, double RadiusX, double RadiusY, int num_steps)
            : this(OriginX, OriginY, RadiusX, RadiusY, num_steps, false)
        {
        }

        public Ellipse(double OriginX, double OriginY, 
            double RadiusX, double RadiusY,
            int num_steps, bool cw)
        {
            init(OriginX, OriginY, RadiusX, RadiusY, num_steps, cw);

            m_scale = 1;            
        }

        public void init(double OriginX, double OriginY, double RadiusX, double RadiusY)
        {
            init(OriginX, OriginY, RadiusX, RadiusY, 0, false);
        }

        public void init(double OriginX, double OriginY, double RadiusX, double RadiusY, int num_steps)
        {
            init(OriginX, OriginY, RadiusX, RadiusY, num_steps, false);
        }

        public void init(double OriginX, double OriginY, double RadiusX, double RadiusY,
                  int num_steps, bool cw)
        {
            m_Origin = new Point2D(OriginX, OriginY);

            m_rx = RadiusX;
            m_ry = RadiusY;
            m_num = num_steps;
            m_step = 0;
            m_cw = cw;
            if (m_num == 0) calc_num_steps();
        }

        public void approximation_scale(double scale)
        {
            m_scale = scale;
            calc_num_steps();
        }

        public void rewind(int path_id)
        {
            m_step = 0;
        }

        public Path.FlagsAndCommand vertex(out double x, out double y)
        {
            x = 0;
            y = 0;
            if (m_step == m_num)
            {
                ++m_step;
                return Path.FlagsAndCommand.CommandEndPoly | Path.FlagsAndCommand.FlagClose | Path.FlagsAndCommand.FlagCCW;
            }
            if (m_step > m_num) return Path.FlagsAndCommand.CommandStop;
            double angle = (double)(m_step) / (double)(m_num) * 2.0 * Math.PI;
            if (m_cw) angle = 2.0 * Math.PI - angle;
            x = m_Origin.x + Math.Cos(angle) * m_rx;
            y = m_Origin.y + Math.Sin(angle) * m_ry;
            m_step++;
            return ((m_step == 1) ? Path.FlagsAndCommand.CommandMoveTo : Path.FlagsAndCommand.CommandLineTo);
        }

        private void calc_num_steps()
        {
            double ra = (Math.Abs(m_rx) + Math.Abs(m_ry)) / 2;
            double da = Math.Acos(ra / (ra + 0.125 / m_scale)) * 2;
            m_num = (int)Math.Round(2 * Math.PI / da);
        }
    }
}