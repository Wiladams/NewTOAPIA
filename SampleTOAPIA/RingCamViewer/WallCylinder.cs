
namespace RingCamView
{
    using NewTOAPIA.GL;

    class WallCylinder : MasterCylinder
    {
        public WallCylinder(GraphicsInterface gi, GLTexture wallTexture, float expanseFactor)
            :base(gi, wallTexture, 5, 14.0f, 12.0f, 10, 10, expanseFactor)
        {
        }
    }
}
