
namespace NewTOAPIA.Graphics
{
    public class mat2x3 : Matrix
    {
        public mat2x3()
            :base(2, 3)
        {
        }

        public mat2x3(IMatrix x)
        :base(2,3)
        {
            CopyFrom(x);
        }
    }
}
