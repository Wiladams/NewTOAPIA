
namespace NewTOAPIA.Graphics.Imaging
{
    using NewTOAPIA.Graphics;

    public interface IApplyUnaryColorOperator
    {
        void ApplyUnaryColorOperator(ITransformColor op);
    }

    public interface IApplyBinaryColorOperator
    {
        void ApplyBinaryColorOperator(IBinaryColorOperator op, IPixelArray srcAccess);
    }

}
