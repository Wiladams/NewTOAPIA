namespace NewTOAPIA
{
    public struct double4
    {
        public double x, y, z, w;

        public double4(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

                public double4(double3 a, float w)
        {
            this.x = a.x;
            this.y = a.y;
            this.z = a.z;
            this.w = w;
        }

    }
}