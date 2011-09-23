namespace NewTOAPIA.Drawing
{
    using NewTOAPIA.Graphics;

    public interface IVertexDest
    {
        void Clear();

        int size();
        void add(Vector2D vertex);

        Vector2D this[int i]
        {
            get;
        }
    }
}