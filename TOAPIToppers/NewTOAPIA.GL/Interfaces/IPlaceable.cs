
namespace NewTOAPIA.GL
{
    using NewTOAPIA.Drawing;
    using NewTOAPIA.Graphics;

    public interface IPlaceable
    {
        Point3D Position { get; set; }
    }

    public interface IPlaceable3f
    {
        float3 Location { get; set; }
    }

    public interface IPlaceable3d
    {
        Vector3d Location { get; set; }
    }

    public interface IPlaceable4f
    {
        float4 Location { get; set; }
    }
}
