
namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    public class GammaLookUpTable
    {
        enum gamma_scale_e
        {
            gamma_shift = 8,
            gamma_size = 1 << gamma_shift,
            gamma_mask = gamma_size - 1
        }

        private double m_gamma;
        private byte[] m_dir_gamma;
        private byte[] m_inv_gamma;


        public GammaLookUpTable()
        {
            m_gamma = (1.0);
            m_dir_gamma = new byte[(int)gamma_scale_e.gamma_size];
            m_inv_gamma = new byte[(int)gamma_scale_e.gamma_size];
        }

        public GammaLookUpTable(double g)
        {
            m_gamma = g;
            m_dir_gamma = new byte[(int)gamma_scale_e.gamma_size];
            m_inv_gamma = new byte[(int)gamma_scale_e.gamma_size];
            SetGamma(m_gamma);
        }

        public void SetGamma(double g)
        {
            m_gamma = g;

            for (uint i = 0; i < (uint)gamma_scale_e.gamma_size; i++)
            {
                m_dir_gamma[i] = (byte)agg_basics.uround(Math.Pow(i / (double)gamma_scale_e.gamma_mask, m_gamma) * (double)gamma_scale_e.gamma_mask);
            }

            double inv_g = 1.0 / g;
            for (uint i = 0; i < (uint)gamma_scale_e.gamma_size; i++)
            {
                m_inv_gamma[i] = (byte)agg_basics.uround(Math.Pow(i / (double)gamma_scale_e.gamma_mask, inv_g) * (double)gamma_scale_e.gamma_mask);
            }
        }

        public double GetGamma()
        {
            return m_gamma;
        }

        public byte dir(int v)
        {
            return m_dir_gamma[v];
        }

        public byte inv(int v)
        {
            return m_inv_gamma[v];
        }
    }
}

