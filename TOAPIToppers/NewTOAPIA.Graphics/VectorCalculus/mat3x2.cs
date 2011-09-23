
namespace NewTOAPIA.Graphics
{

    public class mat3x2 : Matrix
    {
        public mat3x2()
            : base(3, 2)
        {
        }

        public mat3x2(IMatrix x)
            :base(3,2)
        {
            CopyFrom(x);
        }

    }
}
