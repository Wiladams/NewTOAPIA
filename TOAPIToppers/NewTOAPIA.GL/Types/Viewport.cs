

namespace NewTOAPIA.GL
{
    /// <summary>
    /// A Viewport contains the parameters necessary to define a section of the Backing Buffer.  
    /// The coordinates are in screen space.  The minZ and maxZ values represent values in the
    /// Depth buffer.
    /// </summary>
    public struct Viewport
    {
        public int x;
        public int y;
        public int width;
        public int height;
        public int minZ;
        public int maxZ;
    }
}
