namespace NewTOAPIA.Graphics
{
    public struct BoundingBox
    {

        public static readonly BoundingBox Default = new BoundingBox(-1, -1, -1, 1, 1, 1);

        public double x0, x1, y0, y1, z0, z1;

        #region Constructors
        public BoundingBox(double x0_, double y0_, double z0_,
            double x1_, double y1_, double z1_)
        {
            x0 = x0_;
            y0 = y0_;
            z0 = z0_;

            x1 = x1_;
            y1 = y1_;
            z1 = z1_;
        }

        public BoundingBox(Point3D p0, Point3D p1)
        {
            x0 = p0.x;
            y0 = p0.y;
            z0 = p0.z;

            x1 = p1.x;
            y1 = p1.y;
            z1 = p1.z;
        }
        #endregion

        bool Hit(Ray ray)
        {
            double ox = ray.origin.x; double oy = ray.origin.y; double oz = ray.origin.z;
            double dx = ray.direction.x; double dy = ray.direction.y; double dz = ray.direction.z;

            double tx_min, ty_min, tz_min;
            double tx_max, ty_max, tz_max;

            double a = 1.0 / dx;
            if (a >= 0)
            {
                tx_min = (x0 - ox) * a;
                tx_max = (x1 - ox) * a;
            }
            else
            {
                tx_min = (x1 - ox) * a;
                tx_max = (x0 - ox) * a;
            }

            double b = 1.0 / dy;
            if (b >= 0)
            {
                ty_min = (y0 - oy) * b;
                ty_max = (y1 - oy) * b;
            }
            else
            {
                ty_min = (y1 - oy) * b;
                ty_max = (y0 - oy) * b;
            }

            double c = 1.0 / dz;
            if (c >= 0)
            {
                tz_min = (z0 - oz) * c;
                tz_max = (z1 - oz) * c;
            }
            else
            {
                tz_min = (z1 - oz) * c;
                tz_max = (z0 - oz) * c;
            }

            double t0, t1;

            // find largest entering t value

            if (tx_min > ty_min)
                t0 = tx_min;
            else
                t0 = ty_min;

            if (tz_min > t0)
                t0 = tz_min;

            // find smallest exiting t value

            if (tx_max < ty_max)
                t1 = tx_max;
            else
                t1 = ty_max;

            if (tz_max < t1)
                t1 = tz_max;

            return (t0 < t1 && t1 > Maths.kEpsilon);
        }


        // --------------------------------------------------------------------- inside
        // used to test if a ray starts inside a grid

        bool IsInside(Point3D p)
        {
            return ((p.x > x0 && p.x < x1) && (p.y > y0 && p.y < y1) && (p.z > z0 && p.z < z1));
        }
    }
}