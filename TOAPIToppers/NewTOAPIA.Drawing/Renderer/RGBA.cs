﻿
namespace NewTOAPIA.Drawing
{
    using System;
    using NewTOAPIA.Graphics;

    // Supported byte orders for RGB and RGBA pixel formats
    //=======================================================================
    struct order_rgb { enum rgb_e { R = 0, G = 1, B = 2, rgb_tag }; };       //----order_rgb
    struct order_bgr { enum bgr_e { B = 0, G = 1, R = 2, rgb_tag }; };       //----order_bgr
    struct order_rgba { enum rgba_e { R = 0, G = 1, B = 2, A = 3, rgba_tag }; }; //----order_rgba
    struct order_argb { enum argb_e { A = 0, R = 1, G = 2, B = 3, rgba_tag }; }; //----order_argb
    struct order_abgr { enum abgr_e { A = 0, B = 1, G = 2, R = 3, rgba_tag }; }; //----order_abgr
    struct order_bgra { enum bgra_e { B = 0, G = 1, R = 2, A = 3, rgba_tag }; }; //----order_bgra

    //====================================================================rgba
    public struct RGBA_Doubles : IColorType
    {
        const int base_shift = 8;
        const int base_scale = (int)(1 << base_shift);
        const int base_mask = base_scale - 1;

        public double m_r;
        public double m_g;
        public double m_b;
        public double m_a;

        public int R_Byte { get { return (int)agg_basics.uround(m_r * (double)base_mask); } set { m_r = (double)value / (double)base_mask; } }
        public int G_Byte { get { return (int)agg_basics.uround(m_g * (double)base_mask); } set { m_g = (double)value / (double)base_mask; } }
        public int B_Byte { get { return (int)agg_basics.uround(m_b * (double)base_mask); } set { m_b = (double)value / (double)base_mask; } }
        public int A_Byte { get { return (int)agg_basics.uround(m_a * (double)base_mask); } set { m_a = (double)value / (double)base_mask; } }

        //--------------------------------------------------------------------
        public RGBA_Doubles(double r_, double g_, double b_)
            : this(r_, g_, b_, 1.0)
        {
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles(double r_, double g_, double b_, double a_)
        {
            m_r = r_;
            m_g = g_;
            m_b = b_;
            m_a = a_;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles(RGBA_Doubles c)
            : this(c, 1)
        {
        }

        public RGBA_Doubles(RGBA_Doubles c, double a_)
        {
            m_r = c.m_r;
            m_g = c.m_g;
            m_b = c.m_b;
            m_a = a_;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles(double wavelen)
            : this(wavelen, 1.0)
        {

        }

        public RGBA_Doubles(double wavelen, double gamma)
        {
            this = from_wavelength(wavelen, gamma);
        }

        public RGBA_Bytes GetAsRGBA_Bytes()
        {
            return new RGBA_Bytes(R_Byte, G_Byte, B_Byte, A_Byte);
        }

        public RGBA_Doubles GetAsRGBA_Doubles()
        {
            return this;
        }

        //--------------------------------------------------------------------
        public void clear()
        {
            m_r = m_g = m_b = m_a = 0;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles transparent()
        {
            m_a = 0.0;
            return this;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles opacity(double a_)
        {
            if (a_ < 0.0) a_ = 0.0;
            if (a_ > 1.0) a_ = 1.0;
            m_a = a_;
            return this;
        }

        //--------------------------------------------------------------------
        public double opacity()
        {
            return m_a;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles premultiply()
        {
            m_r *= m_a;
            m_g *= m_a;
            m_b *= m_a;
            return this;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles premultiply(double a_)
        {
            if (m_a <= 0.0 || a_ <= 0.0)
            {
                m_r = m_g = m_b = m_a = 0.0;
                return this;
            }
            a_ /= m_a;
            m_r *= a_;
            m_g *= a_;
            m_b *= a_;
            m_a = a_;
            return this;
        }

        //--------------------------------------------------------------------
        public RGBA_Doubles demultiply()
        {
            if (m_a == 0)
            {
                m_r = m_g = m_b = 0;
                return this;
            }
            double a_ = 1.0 / m_a;
            m_r *= a_;
            m_g *= a_;
            m_b *= a_;
            return this;
        }


        //--------------------------------------------------------------------
        public RGBA_Bytes gradient(RGBA_Bytes c_8, double k)
        {
            RGBA_Doubles c = c_8.GetAsRGBA_Doubles();
            RGBA_Doubles ret;
            ret.m_r = m_r + (c.m_r - m_r) * k;
            ret.m_g = m_g + (c.m_g - m_g) * k;
            ret.m_b = m_b + (c.m_b - m_b) * k;
            ret.m_a = m_a + (c.m_a - m_a) * k;
            return ret.GetAsRGBA_Bytes();
        }

        //--------------------------------------------------------------------
        public static IColorType no_color() { return (IColorType)new RGBA_Doubles(0, 0, 0, 0); }

        //--------------------------------------------------------------------
        public static RGBA_Doubles from_wavelength(double wl)
        {
            return from_wavelength(wl, 1.0);
        }

        public static RGBA_Doubles from_wavelength(double wl, double gamma)
        {
            RGBA_Doubles t = new RGBA_Doubles(0.0, 0.0, 0.0);

            if (wl >= 380.0 && wl <= 440.0)
            {
                t.m_r = -1.0 * (wl - 440.0) / (440.0 - 380.0);
                t.m_b = 1.0;
            }
            else
                if (wl >= 440.0 && wl <= 490.0)
                {
                    t.m_g = (wl - 440.0) / (490.0 - 440.0);
                    t.m_b = 1.0;
                }
                else
                    if (wl >= 490.0 && wl <= 510.0)
                    {
                        t.m_g = 1.0;
                        t.m_b = -1.0 * (wl - 510.0) / (510.0 - 490.0);
                    }
                    else
                        if (wl >= 510.0 && wl <= 580.0)
                        {
                            t.m_r = (wl - 510.0) / (580.0 - 510.0);
                            t.m_g = 1.0;
                        }
                        else
                            if (wl >= 580.0 && wl <= 645.0)
                            {
                                t.m_r = 1.0;
                                t.m_g = -1.0 * (wl - 645.0) / (645.0 - 580.0);
                            }
                            else
                                if (wl >= 645.0 && wl <= 780.0)
                                {
                                    t.m_r = 1.0;
                                }

            double s = 1.0;
            if (wl > 700.0) s = 0.3 + 0.7 * (780.0 - wl) / (780.0 - 700.0);
            else if (wl < 420.0) s = 0.3 + 0.7 * (wl - 380.0) / (420.0 - 380.0);

            t.m_r = Math.Pow(t.m_r * s, gamma);
            t.m_g = Math.Pow(t.m_g * s, gamma);
            t.m_b = Math.Pow(t.m_b * s, gamma);
            return t;
        }

        public static RGBA_Doubles rgba_pre(double r, double g, double b)
        {
            return rgba_pre(r, g, b, 1.0);
        }

        public static RGBA_Doubles rgba_pre(double r, double g, double b, double a)
        {
            return new RGBA_Doubles(r, g, b, a).premultiply();
        }

        public static RGBA_Doubles rgba_pre(RGBA_Doubles c)
        {
            return new RGBA_Doubles(c).premultiply();
        }

        public static RGBA_Doubles rgba_pre(RGBA_Doubles c, double a)
        {
            return new RGBA_Doubles(c, a).premultiply();
        }

        public static RGBA_Doubles GetTweenColor(RGBA_Doubles Color1, RGBA_Doubles Color2, double RatioOf2)
        {
            if (RatioOf2 <= 0)
            {
                return new RGBA_Doubles(Color1);
            }

            if (RatioOf2 >= 1.0)
            {
                return new RGBA_Doubles(Color2);
            }

            // figure out how much of each color we should be.
            double RatioOf1 = 1.0 - RatioOf2;
            return new RGBA_Doubles(
                Color1.m_r * RatioOf1 + Color2.m_r * RatioOf2,
                Color1.m_g * RatioOf1 + Color2.m_g * RatioOf2,
                Color1.m_b * RatioOf1 + Color2.m_b * RatioOf2);
        }
    };

    //===================================================================rgba8
    public struct RGBA_Bytes : IColorType
    {
        public const int cover_shift = 8;
        public const int cover_size = 1 << cover_shift;  //----cover_size 
        public const int cover_mask = cover_size - 1;    //----cover_mask 
        //public const int cover_none  = 0,                 //----cover_none 
        //public const int cover_full  = cover_mask         //----cover_full 

        public const int base_shift = 8;
        public const int base_scale = (int)(1 << base_shift);
        public const int base_mask = base_scale - 1;

        public byte m_R;
        public byte m_G;
        public byte m_B;
        public byte m_A;

        public int R_Byte { get { return (int)m_R; } set { m_R = (byte)value; } }
        public int G_Byte { get { return (int)m_G; } set { m_G = (byte)value; } }
        public int B_Byte { get { return (int)m_B; } set { m_B = (byte)value; } }
        public int A_Byte { get { return (int)m_A; } set { m_A = (byte)value; } }

        //--------------------------------------------------------------------
        public RGBA_Bytes(int r_, int g_, int b_)
            : this(r_, g_, b_, base_mask)
        { }

        //--------------------------------------------------------------------
        public RGBA_Bytes(int r_, int g_, int b_, int a_)
        {
            m_R = (byte)r_;
            m_G = (byte)g_;
            m_B = (byte)b_;
            m_A = (byte)a_;
        }

        //--------------------------------------------------------------------
        public RGBA_Bytes(double r_, double g_, double b_, double a_)
        {
            m_R = ((byte)agg_basics.uround(r_ * (double)base_mask));
            m_G = ((byte)agg_basics.uround(g_ * (double)base_mask));
            m_B = ((byte)agg_basics.uround(b_ * (double)base_mask));
            m_A = ((byte)agg_basics.uround(a_ * (double)base_mask));
        }

        //--------------------------------------------------------------------
        public RGBA_Bytes(double r_, double g_, double b_)
        {
            m_R = ((byte)agg_basics.uround(r_ * (double)base_mask));
            m_G = ((byte)agg_basics.uround(g_ * (double)base_mask));
            m_B = ((byte)agg_basics.uround(b_ * (double)base_mask));
            m_A = (byte)base_mask;
        }

        //--------------------------------------------------------------------
        RGBA_Bytes(RGBA_Doubles c, double a_)
        {
            m_R = ((byte)agg_basics.uround(c.m_r * (double)base_mask));
            m_G = ((byte)agg_basics.uround(c.m_g * (double)base_mask));
            m_B = ((byte)agg_basics.uround(c.m_b * (double)base_mask));
            m_A = ((byte)agg_basics.uround(a_ * (double)base_mask));
        }

        //--------------------------------------------------------------------
        RGBA_Bytes(RGBA_Bytes c, int a_)
        {
            m_R = (byte)c.m_R;
            m_G = (byte)c.m_G;
            m_B = (byte)c.m_B;
            m_A = (byte)a_;
        }

        public RGBA_Bytes(RGBA_Doubles c)
        {
            m_R = ((byte)agg_basics.uround(c.m_r * (double)base_mask));
            m_G = ((byte)agg_basics.uround(c.m_g * (double)base_mask));
            m_B = ((byte)agg_basics.uround(c.m_b * (double)base_mask));
            m_A = ((byte)agg_basics.uround(c.m_a * (double)base_mask));
        }

        public RGBA_Doubles GetAsRGBA_Doubles()
        {
            return new RGBA_Doubles((double)m_R / (double)base_mask, (double)m_G / (double)base_mask, (double)m_B / (double)base_mask, (double)m_A / (double)base_mask);
        }

        public RGBA_Bytes GetAsRGBA_Bytes()
        {
            return this;
        }

        //--------------------------------------------------------------------
        void clear()
        {
            m_R = m_G = m_B = m_A = 0;
        }

        //--------------------------------------------------------------------
        public RGBA_Bytes gradient(RGBA_Bytes c, double k)
        {
            RGBA_Bytes ret = new RGBA_Bytes();
            int ik = agg_basics.uround(k * base_scale);
            ret.R_Byte = (byte)((int)(R_Byte) + ((((int)(c.R_Byte) - R_Byte) * ik) >> base_shift));
            ret.G_Byte = (byte)((int)(G_Byte) + ((((int)(c.G_Byte) - G_Byte) * ik) >> base_shift));
            ret.B_Byte = (byte)((int)(B_Byte) + ((((int)(c.B_Byte) - B_Byte) * ik) >> base_shift));
            ret.A_Byte = (byte)((int)(A_Byte) + ((((int)(c.A_Byte) - A_Byte) * ik) >> base_shift));
            return ret;
        }

        //--------------------------------------------------------------------
        public void add(RGBA_Bytes c, int cover)
        {
            int cr, cg, cb, ca;
            if (cover == cover_mask)
            {
                if (c.A_Byte == base_mask)
                {
                    this = c;
                }
                else
                {
                    cr = R_Byte + c.R_Byte; R_Byte = (cr > (int)(base_mask)) ? (int)(base_mask) : cr;
                    cg = G_Byte + c.G_Byte; G_Byte = (cg > (int)(base_mask)) ? (int)(base_mask) : cg;
                    cb = B_Byte + c.B_Byte; B_Byte = (cb > (int)(base_mask)) ? (int)(base_mask) : cb;
                    ca = A_Byte + c.A_Byte; A_Byte = (ca > (int)(base_mask)) ? (int)(base_mask) : ca;
                }
            }
            else
            {
                cr = R_Byte + ((c.R_Byte * cover + cover_mask / 2) >> cover_shift);
                cg = G_Byte + ((c.G_Byte * cover + cover_mask / 2) >> cover_shift);
                cb = B_Byte + ((c.B_Byte * cover + cover_mask / 2) >> cover_shift);
                ca = A_Byte + ((c.A_Byte * cover + cover_mask / 2) >> cover_shift);
                R_Byte = (cr > (int)(base_mask)) ? (int)(base_mask) : cr;
                G_Byte = (cg > (int)(base_mask)) ? (int)(base_mask) : cg;
                B_Byte = (cb > (int)(base_mask)) ? (int)(base_mask) : cb;
                A_Byte = (ca > (int)(base_mask)) ? (int)(base_mask) : ca;
            }
        }

        //--------------------------------------------------------------------
        public void apply_gamma_dir(GammaLookUpTable gamma)
        {
            R_Byte = gamma.dir((byte)R_Byte);
            G_Byte = gamma.dir((byte)G_Byte);
            B_Byte = gamma.dir((byte)B_Byte);
        }

        public static IColorType no_color() { return new RGBA_Bytes(0, 0, 0, 0); }

        //-------------------------------------------------------------rgb8_packed
        static public RGBA_Bytes rgb8_packed(int v)
        {
            return new RGBA_Bytes((v >> 16) & 0xFF, (v >> 8) & 0xFF, v & 0xFF);
        }
    }
}