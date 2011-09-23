namespace NewTOAPIA.Drawing
{
    public interface IVertexSource
    {
        void rewind(int path_id);
        Path.FlagsAndCommand vertex(out double x, out double y);
    }
}