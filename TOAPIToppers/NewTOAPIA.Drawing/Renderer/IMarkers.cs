
namespace NewTOAPIA.Drawing
{
    public interface IMarkers
    {
        void remove_all();
        void add_vertex(double x, double y, Path.FlagsAndCommand unknown);
    };
}
