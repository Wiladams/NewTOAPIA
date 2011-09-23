
namespace NewTOAPIA.Drawing
{
    using NewTOAPIA;

    /// <summary>
    /// The structure to represent ellipse geometry.
    /// </summary>
    public struct EllipseG
    {
        /// <summary>
        /// Indicates the center of the ellipse.
        /// </summary>
        Point3D Center;

        /// <summary>
        /// Radius of ellipse along X-Axis.
        /// </summary>
        public int XRadius;

        /// <summary>
        /// Radius of ellipse along Y-Axis.
        /// </summary>
        public int YRadius;

        /// <summary>
        /// Structure constructor.
        /// </summary>
        /// <param name="acenter">Center of Ellipse.</param>
        /// <param name="xrad">X-Axis Radius</param>
        /// <param name="yrad">Y-Axis Radius</param>
        public EllipseG(Point3D acenter, int xrad, int yrad)
        {
            this.Center = acenter;
            this.XRadius = xrad;
            this.YRadius = yrad;
        }
    }
}
