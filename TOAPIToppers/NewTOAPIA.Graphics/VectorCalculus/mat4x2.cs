
namespace NewTOAPIA.Graphics
{
    using NewTOAPIA;

    public class mat4x2 : Matrix
    {
        public mat4x2()
            : base(4, 2)
        {
        }

        public mat4x2(IMatrix x)
            :base(4,2)
        {
            CopyFrom(x);
        }
    }
}
