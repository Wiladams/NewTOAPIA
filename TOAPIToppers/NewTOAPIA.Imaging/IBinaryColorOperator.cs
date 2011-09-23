
namespace NewTOAPIA.Graphics.Imaging
{
    using NewTOAPIA.Graphics;

    public interface IBinaryColorOperator
    {
        ColorRGBA ApplyBinaryOperator(ColorRGBA destination, ColorRGBA source);
    }
}
