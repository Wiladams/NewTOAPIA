
namespace NewTOAPIA.Graphics
{
    using NewTOAPIA;

    public class mat2x4 : Matrix
    {
        public mat2x4()
            : base(2, 4)
        {
        }

        public mat2x4(IMatrix x)
            : base(2, 4)
        {
            CopyFrom(x);
        }
    }
}
